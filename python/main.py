from contextlib import asynccontextmanager

from fastapi import FastAPI
from pydantic import BaseModel
from typing import List, Optional
import pickle
import logging
from api.recommend_core import get_final_recommendation

# C# DTO 대응
class ProductDto(BaseModel):
    Id: int
    ProductDisplayName: str
    MasterCategory: str
    ArticleType: str

class RecommendRequestDto(BaseModel):
    CustomerId: int
    Grade: str
    Query: Optional[str] = None
    ArticleType: Optional[str] = None
    ProductList: List[ProductDto]

class RecommendResultDto(BaseModel):
    ProductId: int
    Score: float



# 전역 변수 딕셔너리: 모든 로드된 모델과 데이터를 저장
LOADED_ASSETS = {}

def load_pickle(path):
    with open(path, "rb") as f:
        return pickle.load(f)


@asynccontextmanager
async def lifespan(app: FastAPI):
    logging.info("--- Starting Model and Data Loading ---")

    global LOADED_ASSETS
    LOADED_ASSETS['als_model_fitted'] = load_pickle("models/als_model.pkl")
    LOADED_ASSETS['full_als_matrix_csr'] = load_pickle("models/als_full_matrix.pkl")
    LOADED_ASSETS['user_map'] = load_pickle("models/als_vip_user_map.pkl")
    LOADED_ASSETS['item_list'] = load_pickle("models/als_item_list.pkl")
    LOADED_ASSETS['global_tfidf_vectorizer'] = load_pickle("models/tfidf_vectorizer.pkl")
    LOADED_ASSETS['customer_merge_copy'] = load_pickle("models/tfidf_customer_merge.pkl")
    LOADED_ASSETS['article_to_master_category'] = load_pickle("models/tfidf_article_to_master_category.pkl")
    LOADED_ASSETS['category_models'] = load_pickle("models/tfidf_category_models.pkl")
    LOADED_ASSETS['cart_product'] = load_pickle("models/cart_product.pkl")

    logging.info("모델 적재 성공")

    yield   # 서버가 요청을 처리하도록 제어권을 넘김

app = FastAPI(lifespan=lifespan)

# C#의 RecommendRequestAsync 함수가 POST 요청을 보내므로, POST를 사용
@app.post("/recommend", response_model=List[RecommendResultDto])
def recommend_endpoint(request_dto: RecommendRequestDto):
    # 요청 데이터 추출
    user_id = request_dto.CustomerId
    grade = request_dto.Grade
    query = request_dto.Query

    # 추천 로직 실행 (recommend_core.py)
    recommendation_results_df = get_final_recommendation(
        user_id=user_id,
        user_grade=grade,
        query=query,
        **LOADED_ASSETS  # 로드된 모든 데이터를 인수로 전달
    )

    # 결과를 DTO 형식(List[RecommendResultDto])으로 변환하여 반환
    result_list = [
        {"ProductId": row['product_id'], "Score": row.get('score')}
        for index, row in recommendation_results_df.iterrows()
    ]

    return result_list