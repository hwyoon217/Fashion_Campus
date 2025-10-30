using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fashion_campus {
    public partial class frmLogin : Form {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGuestLogin_Click(object sender, EventArgs e) {
            this.Hide();
            frmGuest frm = new frmGuest();
            frm.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            if (!int.TryParse(tbCustomerID.Text, out int customerId)) {
                guna2MessageDialog1.Show("ID가 잘못되었습니다.");
                return;
            }

            if (MainClass.IsvalidUser(customerId) == false) {
                guna2MessageDialog1.Show("존재하지 않는 ID입니다.");
            }
            else {
                string username = MainClass.USER;
                string grade = MainClass.CustomerGrade(customerId);

                // 거래내역 없으면 게스트폼으로 이동
                if (grade == "no_transaction") {
                    this.Hide();
                    frmGuest frmguest = new frmGuest(username);
                    frmguest.Show();
                }
                else {
                    this.Hide();
                    frmMember frm = new frmMember(customerId, username, grade);
                    frm.Show();
                }
                    
            }
            
        }
    }
}
