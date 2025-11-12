import pandas as pd
import numpy as np
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
import re
import implicit
import scipy


# 유저의 거래내역을 기반으로 유저의 관심 상품을 벡터화하는 함수
def get_query_biased_user_vector(user_id, cleaned_query, tfidf_vectorizer, customer_merge_copy):

    user_data = customer_merge_copy[customer_merge_copy['customer_id'] == user_id].copy()
    filtered_data = user_data[user_data['articleType'] == cleaned_query].copy()

    if filtered_data.empty:
        filtered_data = user_data

    filtered_data['combined_text'] = filtered_data['productDisplayName'].astype(str) + ' ' + filtered_data[
        'articleType']
    user_tfidf_vectors = tfidf_vectorizer.transform(filtered_data['combined_text'].values.astype('U'))

    return user_tfidf_vectors


## Tfidf 모델
def recommend_for_user(user_id, query=None, top=20, tfidf_vectorizer=None, customer_merge_copy=None,
                       article_to_master_category=None, category_models=None):
    # 쿼리 ArticleType을 MasterCategory로 매핑
    cleaned_query = query.strip() if query else None
    if not cleaned_query:
        return pd.DataFrame()

    master_category_to_use = article_to_master_category.get(cleaned_query)
    if not master_category_to_use:
        return pd.DataFrame()

    # MasterCategory 모델 로딩 (master_category_to_use 사용)
    if master_category_to_use not in category_models:
        return pd.DataFrame()

    # 유저 벡터 및 아이템 벡터 로딩
    user_product_vectors = get_query_biased_user_vector(user_id, cleaned_query, tfidf_vectorizer,customer_merge_copy)
    category_model = category_models[master_category_to_use]
    item_vectors = category_model['tfidf_vectors']

    # 유사도 계산 및 추천
    similarity_matrix = cosine_similarity(user_product_vectors, item_vectors)
    max_sim_scores = similarity_matrix.max(axis=0)

    recommended_product_ids = category_model['product_ids']

    top_recommendations = sorted(
        zip(recommended_product_ids, max_sim_scores), key=lambda x: x[1], reverse=True)[:top]

    recommended_products_df = pd.DataFrame([
        {
            'product_id': product_id,
            'score': similarity,
            'masterCategory': master_category_to_use,  # 쿼리가 아닌, 사용된 마스터 카테고리 기입
            'productDisplayName': category_model['product_info'][product_id]['productDisplayName']
        }
        for product_id, similarity in top_recommendations
    ])

    # 유저가 이미 구매한 상품은 제외
    purchased_ids = customer_merge_copy[customer_merge_copy['customer_id'] == user_id]['product_id'].tolist()
    recommended_products_df = recommended_products_df[~recommended_products_df['product_id'].isin(purchased_ids)]

    # productDisplayName을 기준으로 그룹화
    # 각 그룹에서 similarity 점수가 가장 높은 행을 선택
    recommended_products_df = recommended_products_df.sort_values(by='score', ascending=False)
    recommended_products_df = recommended_products_df.drop_duplicates(
        subset=['productDisplayName'], keep='first'
    )

    # 중복 제거 후 다시 similarity 순으로 정렬
    recommended_products_df = recommended_products_df.sort_values(
        by='score', ascending=False
    ).head(top)

    return recommended_products_df



## ALS 모델
def recommend_product(model, full_matrix_csr, als_user_idx, item_list, cart_product, query=None, N=100):
    # 1. 유효성 검사 (ALS 인덱스 사용)
    if als_user_idx is None or als_user_idx >= full_matrix_csr.shape[0] or als_user_idx < 0:
        return pd.DataFrame()

    # 유저가 평가한 아이템 추출
    user_items = full_matrix_csr[als_user_idx]
    if user_items.sum() == 0:
        return pd.DataFrame()

    # 추천 실행 (als_user_idx 사용)
    recommended, scores = model.recommend(als_user_idx, user_items, N=N)

    # 추천된 인덱스를 실제 상품 ID로 변환
    recommended_product_ids = [item_list[i] for i in recommended]

    # 상품명과 점수를 가져오기 위해 cart_product와 결합
    recommended_data = []
    for product_id, score in zip(recommended_product_ids, scores):
        matching_rows = cart_product[cart_product['product_id'] == product_id]
        if not matching_rows.empty:
            product_name = matching_rows.iloc[0]['productDisplayName']
            article_type = matching_rows.iloc[0]['articleType']  # articleType 추가
            if query is None or article_type == query:  # query와 일치하는 상품만
                recommended_data.append({
                    'product_id': product_id,
                    'product_name': product_name,
                    'score': score,
                    'articleType': article_type  # articleType도 포함시킬 수 있음
                })

    recommended_products_df = pd.DataFrame(recommended_data)

    return recommended_products_df



def get_final_recommendation(user_id: int, user_grade: str, query: str = None, **loaded_assets):
    """
        고객 등급에 따라 TF-IDF 또는 ALS 추천 시스템 추천
    """

    if user_grade == 'customer':
        result = recommend_for_user(
            user_id, query,
            tfidf_vectorizer=loaded_assets['global_tfidf_vectorizer'],
            customer_merge_copy=loaded_assets['customer_merge_copy'],
            article_to_master_category=loaded_assets['article_to_master_category'],
            category_models=loaded_assets['category_models'],
        )
    else:
        als_user_idx = loaded_assets['user_map'].get(user_id)

        result = recommend_product(
            model=loaded_assets['als_model_fitted'],
            full_matrix_csr=loaded_assets['full_als_matrix_csr'],
            als_user_idx=als_user_idx,
            item_list=loaded_assets['item_list'],
            query=query,
            cart_product=loaded_assets['cart_product']
        )

    return result