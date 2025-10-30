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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace fashion_campus {
    public partial class frmGuest : Sample {
        private string _username;

        public frmGuest(string username="비회원") {
            InitializeComponent();
            _username = username;
        }

        private void frmGuest_Load(object sender, EventArgs e) {
            label2.Text = _username;
            cbSub.Enabled = false;
            cbMaster.SelectedIndexChanged -= cbMaster_SelectedIndexChanged; // 이벤트 잠시 제거

            string qrymaster = "select distinct mastercategory from products";
            MainClass.CBFill(qrymaster, cbMaster, "masterCategory", "masterCategory");

            cbMaster.SelectedIndex = -1; // 선택 초기화
            cbMaster.SelectedIndexChanged += cbMaster_SelectedIndexChanged; // 이벤트 다시 연결
        }

        public override void btnExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void cbMaster_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbMaster.SelectedIndex != -1) {
                cbSub.Enabled = true;

                string submaster = @"select distinct subCategory from products where masterCategory = @master";

                SqlCommand cmd = new SqlCommand(submaster, MainClass.con);
                cmd.Parameters.AddWithValue("@master", cbMaster.SelectedValue.ToString());

                MainClass.CBFill(cmd, cbSub, "subCategory", "subCategory");
            }
            
        }

        public void GetData() {
            string sort = cbSort.SelectedItem?.ToString();  // null 조건 연산자
            string qry;
            
            ListBox lb = new ListBox();
            lb.Items.Add(dgvID);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvMasterCat);
            lb.Items.Add(dgvSubCat);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvQuantity);
            lb.Items.Add(dgvYear);

            if (cbMaster.SelectedIndex == -1 || cbSub.SelectedIndex == -1) {
                guna2MessageDialog1.Show("카테고리를　모두　선택해주세요！");
                return;
            }

            string master = cbMaster.SelectedValue.ToString();
            string sub = cbSub.SelectedValue.ToString();

            switch (sort) {
                case "판매량순":
                    qry = $@"
                    SELECT 
                        p.id,
                        p.productDisplayName,
                        p.mastercategory,
                        p.subcategory,            
                        AVG(tp.item_price) AS item_price,
                        SUM(tp.quantity) AS quantity, 
                        p.year
                    FROM 
                        products p
                    JOIN 
                        transaction_product tp ON p.id = tp.product_id
                    WHERE
                        p.mastercategory = '{master}' and
                        p.subcategory = '{sub}'
                    GROUP BY 
                        p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year
                    ORDER BY 
                        quantity DESC";
                    break;
                
                case "최신순":
                    qry = $@"
                    SELECT 
                        p.id,
                        p.productDisplayName,
                        p.mastercategory,
                        p.subcategory,
                        AVG(tp.item_price) AS item_price,
                        SUM(tp.quantity) AS quantity, 
                        p.year      
                    FROM 
                        products p
                    JOIN 
                        transaction_product tp ON p.id = tp.product_id
                    WHERE
                        p.mastercategory = '{master}' and
                        p.subcategory = '{sub}'
                    GROUP BY 
                        p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year
                    ORDER BY 
                        year DESC";
                    break;

                case "낮은가격순":
                    qry = $@"
                    SELECT 
                        p.id,
                        p.productDisplayName,
                        p.mastercategory,
                        p.subcategory,
                        AVG(tp.item_price) AS item_price,
                        SUM(tp.quantity) AS quantity, 
                        p.year       
                    FROM 
                        products p
                    JOIN 
                        transaction_product tp ON p.id = tp.product_id
                    WHERE
                        p.mastercategory = '{master}' and
                        p.subcategory = '{sub}'
                    GROUP BY 
                        p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year
                    ORDER BY 
                        item_price ASC";
                    break;

                case "높은가격순":
                    qry = $@"
                    SELECT 
                        p.id,
                        p.productDisplayName,
                        p.mastercategory,
                        p.subcategory,
                        AVG(tp.item_price) AS item_price,
                        SUM(tp.quantity) AS quantity, 
                        p.year        
                    FROM 
                        products p
                    JOIN 
                        transaction_product tp ON p.id = tp.product_id
                    WHERE
                        p.mastercategory = '{master}' and
                        p.subcategory = '{sub}'
                    GROUP BY 
                        p.id, p.productDisplayName, p.mastercategory, p.subcategory, p.year
                    ORDER BY 
                        item_price DESC";
                    break;

                default:
                    guna2MessageDialog1.Show("정렬기준을 선택하세요!");
                    return;
            }
            MainClass.LoadData(qry, guna2DataGridView2, lb);
        }
        public override void btnSearch_Click(object sender, EventArgs e) {
            GetData();
        }

    }
}
