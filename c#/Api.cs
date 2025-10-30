using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace fashion_campus {
    public class ProductsDto {
        public int Id { get; set; }
        public string ProductDisplayName { get; set; }
        public string MasterCategory { get; set; }  // 추천 모델 카테고리별로 저장

        public string ArticleType { get; set; }    // 상품 정보
    }

    public class RecommendRequestDto {
        public int CustomerId { get; set; }
        public string Grade { get; set; }
        // Query => 검색어
        public string Query { get; set; }
        // ArticleType => 필터링용 (오타나, 유사한products - articleType 값 입력시에도 적용되게끔
        // public string ArticleType { get; set; }    
        public List<ProductsDto> ProductList { get; set; }
    }

    // 응답 받는 dto
    public class RecommendResultDto {
        public int ProductId { get; set; }
        public double Score { get; set; }
    }


    internal class Api {
        private readonly HttpClient _client;

        public Api() {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8000/");
        }

        public async Task<List<ProductsDto>> GetProductAsync() {
            var products = new List<ProductsDto>();

            using (SqlConnection con = new SqlConnection(MainClass.con_string)) {
                string sql = "select id, productDisplayName, masterCategory, articleType from products";

                using (SqlCommand cmd = new SqlCommand(sql, con)) {
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            products.Add(new ProductsDto {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                ProductDisplayName = reader.GetString(reader.GetOrdinal("productDisplayName")),
                                MasterCategory = reader.GetString(reader.GetOrdinal("masterCategory")),
                                ArticleType = reader.GetString(reader.GetOrdinal("articleType")),
                            });
                        }
                    }
                }
            }
            return products;
        }

        public async Task<List<RecommendResultDto>> RecommendRequestAsync(RecommendRequestDto dto) {
            
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("recommend", content);
            response.EnsureSuccessStatusCode();    // 성공여부 확인

            var responsebody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<RecommendResultDto>>(responsebody);

            return result;
            
        }
        

    }
    
}
