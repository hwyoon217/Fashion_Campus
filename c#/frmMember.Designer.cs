namespace fashion_campus {
    partial class frmMember {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbGrade = new System.Windows.Forms.Label();
            this.guna2DataGridView3 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.cbMemberSort = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMasterCat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSubCat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSearch.DefaultText = "";
            this.tbSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbSearch.IconLeft = global::fashion_campus.Properties.Resources.search;
            this.tbSearch.Location = new System.Drawing.Point(530, 99);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(4);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.PasswordChar = '\0';
            this.tbSearch.PlaceholderText = "Search Here";
            this.tbSearch.SelectedText = "";
            this.tbSearch.Size = new System.Drawing.Size(310, 47);
            this.tbSearch.TabIndex = 9;
            // 
            // lbGrade
            // 
            this.lbGrade.AutoSize = true;
            this.lbGrade.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrade.Location = new System.Drawing.Point(332, 38);
            this.lbGrade.Name = "lbGrade";
            this.lbGrade.Size = new System.Drawing.Size(66, 30);
            this.lbGrade.TabIndex = 14;
            this.lbGrade.Text = "grade";
            // 
            // guna2DataGridView3
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.guna2DataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2DataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.guna2DataGridView3.BackgroundColor = System.Drawing.Color.White;
            this.guna2DataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.guna2DataGridView3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.guna2DataGridView3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.guna2DataGridView3.ColumnHeadersHeight = 45;
            this.guna2DataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvSno,
            this.dgvID,
            this.dgvName,
            this.dgvMasterCat,
            this.dgvSubCat,
            this.dgvPrice,
            this.dgvQuantity,
            this.dgvYear,
            this.dgvScore});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.guna2DataGridView3.DefaultCellStyle = dataGridViewCellStyle3;
            this.guna2DataGridView3.EnableHeadersVisualStyles = false;
            this.guna2DataGridView3.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView3.Location = new System.Drawing.Point(54, 176);
            this.guna2DataGridView3.Name = "guna2DataGridView3";
            this.guna2DataGridView3.RowHeadersVisible = false;
            this.guna2DataGridView3.RowTemplate.Height = 35;
            this.guna2DataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.guna2DataGridView3.Size = new System.Drawing.Size(1086, 421);
            this.guna2DataGridView3.TabIndex = 15;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView3.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView3.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.Height = 45;
            this.guna2DataGridView3.ThemeStyle.ReadOnly = false;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView3.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.guna2DataGridView3.ThemeStyle.RowsStyle.Height = 35;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView3.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // cbMemberSort
            // 
            this.cbMemberSort.BackColor = System.Drawing.Color.Transparent;
            this.cbMemberSort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMemberSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMemberSort.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMemberSort.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMemberSort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbMemberSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbMemberSort.ItemHeight = 30;
            this.cbMemberSort.Items.AddRange(new object[] {
            "추천순",
            "판매량순",
            "최신순",
            "낮은가격순",
            "높은가격순"});
            this.cbMemberSort.Location = new System.Drawing.Point(284, 104);
            this.cbMemberSort.Name = "cbMemberSort";
            this.cbMemberSort.Size = new System.Drawing.Size(161, 36);
            this.cbMemberSort.TabIndex = 17;
            this.cbMemberSort.SelectedIndexChanged += new System.EventHandler(this.CbMemberSort_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(214, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 16;
            this.label4.Text = "정렬순";
            // 
            // dgvSno
            // 
            this.dgvSno.HeaderText = "sr#";
            this.dgvSno.Name = "dgvSno";
            // 
            // dgvID
            // 
            this.dgvID.DataPropertyName = "Id";
            this.dgvID.FillWeight = 70F;
            this.dgvID.HeaderText = "id";
            this.dgvID.Name = "dgvID";
            this.dgvID.Visible = false;
            // 
            // dgvName
            // 
            this.dgvName.DataPropertyName = "ProductDisplayName";
            this.dgvName.FillWeight = 340F;
            this.dgvName.HeaderText = "Product_name";
            this.dgvName.Name = "dgvName";
            // 
            // dgvMasterCat
            // 
            this.dgvMasterCat.DataPropertyName = "MasterCategory";
            this.dgvMasterCat.FillWeight = 230F;
            this.dgvMasterCat.HeaderText = "Category";
            this.dgvMasterCat.Name = "dgvMasterCat";
            // 
            // dgvSubCat
            // 
            this.dgvSubCat.DataPropertyName = "SubCategory";
            this.dgvSubCat.FillWeight = 260F;
            this.dgvSubCat.HeaderText = "SubCategory";
            this.dgvSubCat.Name = "dgvSubCat";
            // 
            // dgvPrice
            // 
            this.dgvPrice.DataPropertyName = "Price";
            this.dgvPrice.HeaderText = "Price";
            this.dgvPrice.Name = "dgvPrice";
            // 
            // dgvQuantity
            // 
            this.dgvQuantity.DataPropertyName = "Quantity";
            this.dgvQuantity.HeaderText = "Quantity";
            this.dgvQuantity.Name = "dgvQuantity";
            // 
            // dgvYear
            // 
            this.dgvYear.DataPropertyName = "Year";
            this.dgvYear.HeaderText = "Year";
            this.dgvYear.Name = "dgvYear";
            // 
            // dgvScore
            // 
            this.dgvScore.DataPropertyName = "Score";
            this.dgvScore.FillWeight = 80F;
            this.dgvScore.HeaderText = "Score";
            this.dgvScore.Name = "dgvScore";
            this.dgvScore.Visible = false;
            // 
            // frmMember
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1212, 694);
            this.Controls.Add(this.cbMemberSort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.guna2DataGridView3);
            this.Controls.Add(this.lbGrade);
            this.Controls.Add(this.tbSearch);
            this.Name = "frmMember";
            this.Text = "frmMember";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMember_Load);
            this.Controls.SetChildIndex(this.tbSearch, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.lbGrade, 0);
            this.Controls.SetChildIndex(this.guna2DataGridView3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cbMemberSort, 0);
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Guna.UI2.WinForms.Guna2TextBox tbSearch;
        private System.Windows.Forms.Label lbGrade;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView3;
        private Guna.UI2.WinForms.Guna2ComboBox cbMemberSort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMasterCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSubCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvScore;
    }
}