using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;


namespace fashion_campus {
    internal class MainClass {
        public static readonly string con_string = "Data Source=PC100,1434; Initial Catalog=Fashion_Campus; " +
            "Persist Security Info=True; User ID=sa; Password=std001;";

        public static SqlConnection con = new SqlConnection(con_string);

        //check user
        public static bool IsvalidUser(int userId) {
            bool isValid = false;

            string qry = "select * from customer where customer_id = @customer_id";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("customer_id", userId);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0) {
                isValid = true;
                USER = dt.Rows[0]["name"].ToString();
            }
            return isValid;
        }

        public static string CustomerGrade(int customerID) {
            string grade = "";
            string qry = "select grade from customer where customer_id = @customerID";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@customerID", customerID);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0) {
                grade = dt.Rows[0]["grade"].ToString();
            }
            return grade;
        }

        public static string user;
        public static string USER { 
            get { return user; }
            private set { user = value; }
        }
        
        public static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) {
                e.Value = e.RowIndex + 1;
            }
        }

        public static void LoadData(string qry, DataGridView gv, ListBox lb) {
            
            try {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++) {
                    string colName = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colName].DataPropertyName = dt.Columns[i].ToString();
                }
                gv.AllowUserToAddRows = false;
                gv.DataSource = dt;
                gv.CellFormatting -= gv_CellFormatting;
                gv.CellFormatting += gv_CellFormatting;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }

        public static void CBFill(string qry, ComboBox cb, string displayMember, string valueMember) { 
            SqlCommand cmd = new SqlCommand(qry, con); 
            cmd.CommandType = CommandType.Text; 
            SqlDataAdapter da = new SqlDataAdapter(cmd); 
            DataTable dt = new DataTable(); 
            da.Fill(dt);
            
            cb.DisplayMember = displayMember; 
            cb.ValueMember = valueMember; 
            cb.DataSource = dt; 
            cb.SelectedIndex = -1; 
        }

        public static void CBFill(SqlCommand cmd, ComboBox cb, string displayMember, string valueMember) {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = displayMember;
            cb.ValueMember = valueMember;
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }

    }
}
