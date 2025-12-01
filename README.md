# Fashion Campus
검색 기반 개인화 상품 추천 시스템 프로젝트

## 목차
- [소개](#소개)
- [프로젝트 목표](#프로젝트-목표)
- [폴더 구조](#폴더-구조)
- [데이터](#데이터)
- [기술 스택](#기술-스택)
- [설치 및 실행 방법](#설치-및-실행-방법)


## 소개
C#과 Python을 병행하여 개발한 검색 기반 개인화 상품 추천 시스템입니다.  
사용자의 클릭스트림 데이터를 학습하고, FastAPI를 통해 검색어 입력 시 즉시 추천 결과를 제공하도록 설계되었습니다.


## 프로젝트 목표
- 검색 기반 상품 추천
- RFM 분석을 통해 고객 등급을 나누고, 등급에 따라 Tfidf 및 Implicit ALS 모델을 적용한 개인화 추천
- C#과 Python 연동을 통한 데이터 처리 및 추천 로직 구현



## 폴더 구조
c#          # C# 프로젝트 코드
python      # Python 추천 모델 코드
README.md   # 설명 파일


## 데이터
- 원본 데이터: Kaggle - Fashion Campus CSV 파일 사용
- 고객 등급: 원본 데이터에는 포함되지 않으며, RFM 분석을 통해 프로젝트 내에서 생성


## 기술 스택
- C# (.NET) : 데이터 처리 및 UI
- Python : 추천 모델 구현, 분석
- pandas, numpy, scikit-learn, implicit (Python 라이브러리)
- SQL Server : 데이터 저장


## 설치 및 실행 방법

1. **Python 가상환경 설정 (PyCharm 기준)**
    - PyCharm에서 새 venv 생성
  
2. **필요 라이브러리 설치**
    - pip install -r python/requirements.txt
    
3. **FastAPI 서버 실행 (Uvicorn)**
    - python -m uvicorn main:app --reload



  
