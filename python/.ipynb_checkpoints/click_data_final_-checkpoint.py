#!/usr/bin/env python
# coding: utf-8

# In[2]:



# In[ ]:


import pandas as pd
import numpy as np


# In[ ]:


product = pd.read_csv('/content/drive/MyDrive/fashion campus2/product.csv',error_bad_lines=False)

# 유저 세그멘테이션 처리한 파일
transaction_new = pd.read_csv('/content/drive/MyDrive/fashion campus2/transaction_new2.csv')

# product와 transaction을 합친 파일
m_df_p_2 = pd.read_csv('/content/drive/MyDrive/fashion campus2/m_df_p2.csv')

# CB 모델 처리를 위해 만들어진 파일
cb_data = pd.read_csv('/content/drive/MyDrive/fashion campus2/cb_data.csv')

customer_merge = pd.read_csv('/content/drive/MyDrive/fashion campus2/customer_merge.csv')


# ### rfm결과 각각의 사분위수 기준
# 
# 빈도수
# - 0 ~3
# - 3 ~ 12
# - 12 ~ 40
# - 40 ~
# 
# 총금액
# - 0 ~ 909450.0
# - 909450.0 ~ 5144328.0
# - 5144328.0 ~ 26911910.0
# - 26911910.0 ~
# 
# 최근구매일
# - 20210810 이전
# - 20210810 ~ 20220410 사이
# - 20220410 ~ 20220620 사이
# - 20220620 이후
# 
# 
# 각각의 기준에 맞춰서 사용자마다 해당기준에 맞으면 1 ~ 4점의 점수를 줘서 분리함 이때 분리를 한 결과가 rfm이고 이후에
# 그룹화 된것을 transaction_new에 붙혀서 transaction_new4가 됨

# In[ ]:


#위에서 읽은 transaction_new에서 그룹에 따라서 거래를 4개로 나눈다.

trans_extinct_customer = transaction_new[transaction_new['group'] == 'extinct customer']

trans_customer = transaction_new[transaction_new['group'] == 'customer']

trans_vip = transaction_new[transaction_new['group'] == 'vip']

trans_vvip = transaction_new[transaction_new['group'] == 'vvip']


# In[ ]:


#그중에 중요한 특성만 따로 저장한다.

trans_extinct_customer_1 = trans_extinct_customer[['customer_id','product_id','quantity','group','articleType','productDisplayName']]
trans_customer_1 = trans_customer[['customer_id','product_id','quantity','group','articleType','productDisplayName']]
trans_vip_1 = trans_vip[['customer_id','product_id','quantity','group','articleType','productDisplayName']]
trans_vvip_1 = trans_vvip[['customer_id','product_id','quantity','group','articleType','productDisplayName']]


# In[ ]:


# 용량을 줄이기 위해 새 데이터프레임 생성

cb_product = cb_data.groupby(['product_id','articleType','productDisplayName'])['quantity'].count().reset_index()
cart_product = cb_product[cb_product.quantity >= 30]

# 중복 데이터 삭제
cart_product.productDisplayName=cart_product.productDisplayName.drop_duplicates()
cart_product=cart_product.dropna().reset_index(drop=True)


# In[ ]:


# # program1
# - 사용자가 비회원이거나, 거래내역이 없을때에 추천해주는 통계적 추천시스템
# - 검색창에 상품을 입력하고 이후에 옵션을 설정하면 추천해줌
# - 여기 사이에 자연어처리를 하여서 비슷한 상품명을 추천해주는 것을 넣으면 될 것 같다.

# In[ ]:


# 판매량순
def total_amout(s):
    temp = cb_data[cb_data.articleType=='Tshirts']
    temp = temp.groupby('productDisplayName')['quantity'].sum().reset_index().sort_values('quantity',ascending = False).head(10)
    return temp

# 할인순
def promote_amout(s):
    b = m_df_p_2[m_df_p_2['articleType'] == s].sort_values('promo_amount',ascending = False).head(10)

    c= product.columns.tolist()
    c.append('promo_amount')
    c.append('total_amount')

    df_promote = b[c]
    return  pd.DataFrame(df_promote)

# 높은가격순
def item_price_top(s):
    d = m_df_p_2[m_df_p_2['articleType'] == s].sort_values('item_price',ascending = False).head(10)

    c= product.columns.tolist()
    c.append('item_price')
    df_item = d[c]
    return  pd.DataFrame(df_item)

# 낮은가격순
def item_price_bot(s):
    d = m_df_p_2[m_df_p_2['articleType'] == s].sort_values('item_price',ascending = True).head(10)

    c= product.columns.tolist()
    c.append('item_price')
    df_item = d[c]
    return  pd.DataFrame(df_item)

# 최근일순
def item_year(s):
    temp = cb_data[cb_data.articleType==s]
    temp = temp.groupby(['productDisplayName','year'])['quantity'].sum().reset_index().sort_values(['year','quantity'],ascending=[False,False]).head(10)
    return temp


# In[ ]:


def program():
  print("-" * 250)
  s = input('원하는 상품 입력 :')

  #검색어와 비슷한 상품 추천을 해준다



  k = input('| 판매량순 | 할인순 | 높은가격순 | 낮은가격순 | 최근일순 |')

  if k == '판매량순':
    return total_amout(s)

  elif k == '할인순':
    return promote_amout(s)

  elif k == '높은가격순':
    return item_price_top(s)

  elif k == '낮은가격순':
    return item_price_bot(s)

  elif k == '최근일순':
    return item_year(s)


# # 프로그램2
# - 사용자가 회원이면 implicit라이브러리를 이용해서 추천해줌
# - 사용자 id를 분석해서 어떤 세그멘테이션에 들어가는지 파악 후에
# - extinct_customer이거나 customer이면 최근 구매내역과 비슷한 상품을 자연어 처리해서 추천하고
# - vip나 vvip면 implicit로 추천

# In[ ]:


# userid 넣으면 해당 유저의 최근 거래 물품을 토대로 자연어 처리해서 물품 추천

def data_similarity(df, user_id,top=10):

    from sklearn.feature_extraction.text import TfidfVectorizer
    from sklearn.metrics.pairwise import cosine_similarity

    tfidf = TfidfVectorizer()
    product_tfidf = tfidf.fit_transform(df['productDisplayName'].values.astype('U'))
    product_similarity = cosine_similarity(product_tfidf,product_tfidf)


    product_sim = {}
    for i, c in enumerate(df['productDisplayName']):
        product_sim[i] = c

    # id와 movie title를 매핑할 dictionary를 생성
    idproduct = {}
    for i, c in product_sim.items():
        idproduct[c] = i

    product_name = customer_merge[customer_merge['customer_id']==user_id]['productDisplayName'].values[0]

    idx = idproduct[product_name]
    sim_scores = [(i, c) for i, c in enumerate(product_similarity[idx]) if i != idx] # 자기 자신을 제외한 유사도 및 인덱스를 추출
    sim_scores = sorted(sim_scores, key = lambda x: x[1], reverse=True) # 유사도가 높은 순서대로 정렬
    sim_scores = [(product_sim[i], score) for i, score in sim_scores[0:top]]
    product_topsim = pd.DataFrame(sim_scores)
    product_topsim.columns = ['productDisplayName','similarity']

    return product_topsim


# In[ ]:


#pip install implicit


# In[ ]:


# 상품을 입력하면 그 상품 거래내역이 있는 고객으로만 매트릭스를 만들어서 약간 걸림
# 함수에 사용자id와 검색단어(ex Shirts) 그리고 고객 세그멘티이션을 매개변수로 넣는다.

import implicit
import scipy

def matrix(user_id,query,group):
  if group == "vip":
    temp_df = trans_vip_1
  else:
    temp_df = trans_vvip_1


  a = set(temp_df['customer_id'])
  b = set(temp_df[temp_df['articleType'] == query]['product_id'].tolist())
  temp_matrix = np.zeros((len(a),len(b)))

  temp_matrix = pd.DataFrame(temp_matrix)

  temp_matrix.index = sorted(list(a))
  temp_matrix.columns = sorted(list(b))

  user_idx = sorted(list(a)).index(user_id)


  #따로 쓸 검색어가 지정된 데이터
  shirt_df = temp_df[temp_df['articleType'] == query]

  for i in shirt_df.index:
    temp_user = shirt_df.loc[i]['customer_id']
    temp_product = shirt_df.loc[i]['product_id']
    temp_q = shirt_df.loc[i]['quantity']

    if temp_product in product.index:
      temp_matrix.loc[temp_user,temp_product] += temp_q

  temp_matrix.index = list(range(0,len(a)))

  return temp_matrix,user_idx


# In[ ]:


# 모델을 als로 만들고 fit하는함수
# 이때 위에서 만들어진 matrix를 넣는게 아니라 coo_matrix를 사용해서 희소행렬로 만들어서 fit한다.
# 모델과 희소행렬을 리턴함
def model(matrix):
  temp_matrix = scipy.sparse.coo_matrix(matrix)
  model = implicit.als.AlternatingLeastSquares(factors=50,iterations=10,regularization=0.001)

  model.fit(temp_matrix)

  return model,temp_matrix


#만든 모델로 추천해주는 함수
def recommend_product(model,matrix,user_id):
  a,_ = model.recommend(user_id, matrix.tocsr()[user_id])

  li = []
  for i in a:
    if not product[product['product_id'] ==i].empty:
      li.append(product[product['product_id'] == i].index[0])

  result = product.loc[li,:]

  return result


# In[ ]:


# 로그인 id를 입력하고 아닐시 비회원이라고 치면됨
# 비회원이거나 거래내역에 없으면 통계적 프로그램 program실행
# 회원이면 세그멘테이션을 통해서 고객이 어떤 그룹에 속했나 확인함
# 1,2면 자연어처리를 이용한 모델을 사용하고 3,4면 implicit를 사용한 모델로 추천을 해주자

import implicit

def program2():
  user_id = input('로그인 id를 입력하세요/비회원시 비회원 입력')
  if user_id == '비회원':
      result = program()#통계적 알고리즘 적용하기
      return result

  else: #회원이라면 추천해주기
      user_id = int(user_id)
      if user_id in transaction_new['customer_id'].tolist():
          group = list(set(transaction_new[transaction_new['customer_id'] ==user_id ]['group']))[0]

          if group == 'extinct customer' or group == 'customer':
              result = data_similarity(cart_product,user_id)
              return result

          else:
            #원하는 상품 검색하기
            query = input('검색창 : ')

            #vip,vvip면 svd를 사용하고 소멸고객이나 일반고객이면 자연어처리기반모델을 사용한다.

            #user_item_matrix = 행렬 만드는 함수
            user_item_matrix,user_idx = matrix(user_id,query,group)

            # #모델 생성 및 학습하는 함수 만들기
            model_re,temp_matrix = model(user_item_matrix)


            #만들어진 모델을 이용해서 추천해주기
            result = recommend_product(model_re,temp_matrix,user_idx)
            return result

      else:
          result = program()
          return result


# 잘 되나 테스트
# 0. trans_customer_1['customer_id'].unique()이런식으로 입력하면 customer에 속하는 사용자의 id가 나옴 그중에 id를 하나 고른다. ex) 95962
# 1. 로그인 id 95962 입력
# 2. 검색창에 Tshirts 입력
# 
# 

# In[ ]:


# 5868
# 99645
# 24915
# 4774


# In[ ]:


program2()

