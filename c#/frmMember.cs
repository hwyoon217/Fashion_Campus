using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fashion_campus {
    // 파이썬으로부터 받을 결과값 저장하고 정렬에 사용할 dto
    
    public partial class frmMember : Sample {
        private int _customerid;
        private string _username;
        private string _grade;
        private List<DisplayDto> _currentDisplayList = new List<DisplayDto>();

        public frmMember(int customerid, string username, string grade) {
            InitializeComponent();
            _customerid = customerid;
            _username = username;
            _grade = grade;
        }

        private void frmMember_Load(object sender, EventArgs e) {
            label2.Text = _username;
            lbGrade.Text = _grade;

            guna2DataGridView3.AutoGenerateColumns = false;
            cbMemberSort.SelectedIndexChanged += CbMemberSort_SelectedIndexChanged;

            guna2DataGridView3.AllowUserToAddRows = false;
            guna2DataGridView3.CellFormatting += MainClass.gv_CellFormatting;
        }


        public override void btnExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        public override async void btnSearch_Click(object sender, EventArgs e) {
            Api api = new Api(); // Api 인스턴스 생성
            string query = tbSearch.Text;
            int customerId = _customerid;
            string grade = _grade;

            // var productlist = await api.GetProductAsync();
            var dto = new RecommendRequestDto {
                CustomerId = customerId,
                Grade = grade,
                Query = query,
                ProductList = new List<ProductsDto>(),
            };

            var recommendresult = await api.RecommendRequestAsync(dto);

            // 추천결과 전달
            DisplayRecommend(recommendresult);

        }

        private async void DisplayRecommend(List<RecommendResultDto> recommendresult) {
            if (recommendresult == null || recommendresult.Count == 0) {
                guna2DataGridView3.DataSource = null;
                
                return;
            }

            // IN 절에 들어갈 파라미터 리스트를 안전하게 생성
            var idList = recommendresult.Select(r => r.ProductId).ToList();

            // @p0, @p1, @p2... 형태의 파라미터 문자열을 생성
            string parameterNames = string.Join(",", idList.Select((id, index) => $"@p{index}"));

            string qry = $@"
                SELECT 
                    p.id,
                    p.productDisplayName,
                    p.mastercategory,
                    p.subcategory,            
                    AVG(tp.item_price) AS price,
                    SUM(tp.quantity) AS quantity, 
                    p.year
                FROM 
                    products p
                JOIN 
                    transaction_product tp ON p.id = tp.product_id
                WHERE
                     p.id IN ({parameterNames})
                GROUP BY 
                    p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year";

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(MainClass.con_string)) {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(qry, con)) {
                    // 각 ID를 SQL 파라미터로 추가
                    for (int i = 0; i < idList.Count; i++) {
                        cmd.Parameters.AddWithValue($"@p{i}", idList[i]);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // dt >> DisplayDto로 변환
            var productlist = dt.AsEnumerable().Select(row =>  {
                decimal priceDecimal = Convert.ToDecimal(row["Price"]);
                long quantityLong = Convert.ToInt64(row["Quantity"]);
                
                return new DisplayDto {
                    Id = row.Field<int>("id"),
                    ProductDisplayName = row.Field<string>("productDisplayName"),
                    MasterCategory = row.Field<string>("masterCategory"),
                    SubCategory = row.Field<string>("subCategory"),
                    Price = priceDecimal,
                    Quantity = (int)quantityLong,
                    Year = row.Field<int>("year"),
                    Score = 0
                };
            }).ToList();

            // score 채우기
            var displaylist = (from p in productlist
                              join r in recommendresult on p.Id equals r.ProductId
                              select new DisplayDto {
                                  Id = p.Id,
                                  ProductDisplayName = p.ProductDisplayName,
                                  MasterCategory = p.MasterCategory,
                                  SubCategory = p.SubCategory,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Year = p.Year,
                                  Score = r.Score
                              }).ToList();

            _currentDisplayList = displaylist;

            // 정렬(디폴트 = 추천점수)
            SortAndDisplayRecommendation(SortCriteria.RecommendScore);
        }

        // LINQ를 이용한 인 메모리 정렬 함수 (콤보박스에서 호출)
        private void SortAndDisplayRecommendation(SortCriteria criteria) {
            if (_currentDisplayList.Count == 0) return;

            IOrderedEnumerable<DisplayDto> sortedList;

            switch (criteria) {
                case SortCriteria.LatestYear:
                    sortedList = _currentDisplayList.OrderByDescending(p => p.Year);
                    break;
                case SortCriteria.MostSold:
                    sortedList = _currentDisplayList.OrderByDescending(p => p.Quantity);
                    break;
                case SortCriteria.HighestPrice:
                    sortedList = _currentDisplayList.OrderByDescending(p => p.Price);
                    break;
                case SortCriteria.LowestPrice:
                    sortedList = _currentDisplayList.OrderBy(p => p.Price);
                    break;
                case SortCriteria.RecommendScore:
                default:
                    // 기본값: 추천 점수가 높은 순서 (API에서 받은 순서를 유지)
                    sortedList = _currentDisplayList.OrderByDescending(p => p.Score);
                    break;
            }

            guna2DataGridView3.DataSource = sortedList.ToList();
        }

        private void CbMemberSort_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox cbsort = (ComboBox)sender;

            if (cbMemberSort.SelectedItem != null && _currentDisplayList.Count > 0) {
                string selectedText = cbMemberSort.SelectedItem.ToString();
                SortCriteria criteria = GetSortCriteriaFromText(selectedText);

                // 메모리정렬 함수호출
                SortAndDisplayRecommendation(criteria);
            }

        }

        private SortCriteria GetSortCriteriaFromText(string text) {
            switch (text) {
                case "최신순": return SortCriteria.LatestYear;
                case "판매량순": return SortCriteria.MostSold; 
                case "높은가격순": return SortCriteria.HighestPrice;
                case "낮은가격순": return SortCriteria.LowestPrice;
                case "추천순": return SortCriteria.RecommendScore;
                default: return SortCriteria.RecommendScore;
            }
        }

        public class DisplayDto {
            public int Id { get; set; }
            public string ProductDisplayName { get; set; }
            public string MasterCategory { get; set; }
            public string SubCategory { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public int Year { get; set; }
            public double Score { get; set; } // 추천점수
        }

        // 정렬 기준 정의
        public enum SortCriteria {
            RecommendScore,
            LatestYear,
            MostSold,
            HighestPrice,
            LowestPrice
        }


    }
}
