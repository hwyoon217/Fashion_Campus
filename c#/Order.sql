SELECT 
    p.id,
    p.productDisplayName,
    AVG(tp.item_price) AS avg_price -- 또는 MAX/MIN 등도 가능
FROM 
    products p
LEFT JOIN 
    transaction_product tp ON p.id = tp.product_id
GROUP BY 
    p.id, p.productDisplayName
ORDER BY 
    avg_price 


SELECT 
    p.id,
    p.productDisplayName,
    AVG(tp.item_price) AS avg_price -- 또는 MAX/MIN 등도 가능
FROM 
    products p
LEFT JOIN 
    transaction_product tp ON p.id = tp.product_id
GROUP BY 
    p.id, p.productDisplayName
ORDER BY 
    avg_price ASC

-- 상품테이블의 모든 상품이 거래내역에 포함
SELECT p.*
FROM products p
WHERE NOT EXISTS (
    SELECT 1
    FROM transaction_product tp
    WHERE tp.product_id = p.id
)


-- 판매순 쿼리
SELECT 
    p.id,
    p.productDisplayName,
    p.mastercategory,
    p.subcategory,
    p.year,
    SUM(tp.quantity) AS quantity,             -- 총 판매량
    AVG(tp.item_price) AS item_price          -- 평균 가격
FROM 
    products p
JOIN 
    transaction_product tp ON p.id = tp.product_id
GROUP BY 
    p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year
ORDER BY 
    quantity DESC


-- 낮은 가격순
SELECT 
    p.id,
    p.productDisplayName,
    p.mastercategory,
    p.subcategory,
    p.year,
    SUM(tp.quantity) AS quantity,             -- 참고용으로 같이 표시
    AVG(tp.item_price) AS item_price          -- 기준 컬럼
FROM 
    products p
JOIN 
    transaction_product tp ON p.id = tp.product_id
GROUP BY 
    p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year
ORDER BY 
    item_price ASC
    --item_price DESC  -- 높은 가격순

-- 최신순
SELECT 
    p.id,
    p.productDisplayName,
    p.mastercategory,
    p.subcategory,
    p.year,
    SUM(tp.quantity) AS quantity,             -- 참고용으로 같이 표시
    AVG(tp.item_price) AS item_price          -- 기준 컬럼
FROM 
    products p
JOIN 
    transaction_product tp ON p.id = tp.product_id
GROUP BY 
    p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year
ORDER BY 
    year DESC


