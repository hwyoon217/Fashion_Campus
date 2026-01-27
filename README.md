# Fashion Campus
검색 기반 개인화 상품 추천 시스템 프로젝트
<br>
## 목차
- [소개](#소개)
- [프로젝트 목표](#프로젝트-목표)
- [데이터](#데이터)
- [폴더 구조](#폴더-구조)
- [기술 스택](#기술-스택)
- [설치 및 실행 방법](#설치-및-실행-방법)

<br>

## 소개
C#과 Python을 병행하여 개발한 검색 기반 개인화 상품 추천 시스템입니다.  
사용자의 클릭스트림 데이터를 학습하고, 사용자가 입력한 검색어에 맞춰 FastAPI를 통해 추천 결과를 제공하는 실시간 추천 구조로 설계되었습니다.

<br>

## 프로젝트 목표
- 검색 기반 상품 추천
- RFM 분석을 통해 고객 등급을 나누고, 등급에 따라 Tfidf 및 Implicit ALS 모델을 적용한 개인화 추천
- C#과 Python 연동을 통한 데이터 처리 및 추천 로직 구현

<br>

## 데이터
- 원본 데이터: Kaggle - Fashion Campus CSV 파일 사용
- 고객 등급: 원본 데이터에는 포함되지 않으며, RFM 분석을 통해 프로젝트 내에서 생성(등급별로 TF-IDF/ALS 추천 모델 적용)

<br>

## 폴더 구조
```
- c#          : C# 프로젝트 코드
- python      : Python 추천 모델 코드
- README.md   : 설명 파일
```

<br>

## 기술 스택
- C# (.NET) : 데이터 처리 및 UI
- Python : pandas, numpy, scikit-learn, implicit (Python 라이브러리)
  - TF-IDF + Cosine Similarity → 등급별 추천
  - ALS → 등급별 추천
- SQL Server : 데이터 저장

<br>

## 설치 및 실행 방법

1. **Python 가상환경 설정 (PyCharm 기준)**
    - PyCharm에서 새 venv 생성 후 프로젝트 인터프리터로 지정
  
2. **필요 라이브러리 설치**
    - pip install -r python/requirements.txt
    
3. **FastAPI 서버 실행 (Uvicorn)**
    - 터미널에서 python -m uvicorn main:app --reload 실행

4. **C# 애플리케이션 실행**
    - C# 프로젝트에서 회원으로 로그인 후 검색어 입력 시 FastAPI를 호출하여 추천 결과 반환  
