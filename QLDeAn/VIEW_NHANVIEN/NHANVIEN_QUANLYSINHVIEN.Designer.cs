namespace QLDeAn.VIEW_NHANVIEN
{
    partial class NHANVIEN_QUANLYSINHVIEN
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BTN_THÊM = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupMONHOC = new System.Windows.Forms.GroupBox();
            this.GB_THÔNGTINCANHAN_GV = new System.Windows.Forms.GroupBox();
            this.CBB_DONVI = new System.Windows.Forms.ComboBox();
            this.CBB_PHAI = new System.Windows.Forms.ComboBox();
            this.CBB_COSO = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BTN_CAPNHAT = new System.Windows.Forms.Button();
            this.BTN_XOA = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_TINHTRANG = new System.Windows.Forms.TextBox();
            this.TB_SDT = new System.Windows.Forms.TextBox();
            this.TB_DIACHI = new System.Windows.Forms.TextBox();
            this.TB_NGAYSINH = new System.Windows.Forms.TextBox();
            this.TB_HOTEN = new System.Windows.Forms.TextBox();
            this.TB_MASINHVIEN = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupMONHOC.SuspendLayout();
            this.GB_THÔNGTINCANHAN_GV.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_THÊM
            // 
            this.BTN_THÊM.Location = new System.Drawing.Point(18, 12);
            this.BTN_THÊM.Name = "BTN_THÊM";
            this.BTN_THÊM.Size = new System.Drawing.Size(75, 23);
            this.BTN_THÊM.TabIndex = 0;
            this.BTN_THÊM.Text = "THÊM";
            this.BTN_THÊM.UseVisualStyleBackColor = true;
            this.BTN_THÊM.Click += new System.EventHandler(this.BTN_THÊM_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BTN_THÊM);
            this.panel1.Location = new System.Drawing.Point(3, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 48);
            this.panel1.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(568, 387);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupMONHOC
            // 
            this.groupMONHOC.Controls.Add(this.dataGridView1);
            this.groupMONHOC.Location = new System.Drawing.Point(3, 3);
            this.groupMONHOC.Name = "groupMONHOC";
            this.groupMONHOC.Size = new System.Drawing.Size(575, 414);
            this.groupMONHOC.TabIndex = 7;
            this.groupMONHOC.TabStop = false;
            this.groupMONHOC.Text = "DANH SÁCH SINH VIÊN";
            // 
            // GB_THÔNGTINCANHAN_GV
            // 
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.CBB_DONVI);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.CBB_PHAI);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.CBB_COSO);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label10);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.panel2);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label8);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label6);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label4);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label7);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label5);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label3);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label2);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.label1);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.TB_TINHTRANG);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.TB_SDT);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.TB_DIACHI);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.TB_NGAYSINH);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.TB_HOTEN);
            this.GB_THÔNGTINCANHAN_GV.Controls.Add(this.TB_MASINHVIEN);
            this.GB_THÔNGTINCANHAN_GV.Location = new System.Drawing.Point(594, 3);
            this.GB_THÔNGTINCANHAN_GV.Name = "GB_THÔNGTINCANHAN_GV";
            this.GB_THÔNGTINCANHAN_GV.Size = new System.Drawing.Size(312, 468);
            this.GB_THÔNGTINCANHAN_GV.TabIndex = 10;
            this.GB_THÔNGTINCANHAN_GV.TabStop = false;
            this.GB_THÔNGTINCANHAN_GV.Text = "SINH VIÊN ĐƯỢC CHỌN";
            // 
            // CBB_DONVI
            // 
            this.CBB_DONVI.Enabled = false;
            this.CBB_DONVI.FormattingEnabled = true;
            this.CBB_DONVI.Items.AddRange(new object[] {
            "CNTT",
            "HOA",
            "TOAN",
            "PDT",
            "PTV",
            "VLY",
            "PQTTB"});
            this.CBB_DONVI.Location = new System.Drawing.Point(160, 209);
            this.CBB_DONVI.Name = "CBB_DONVI";
            this.CBB_DONVI.Size = new System.Drawing.Size(121, 24);
            this.CBB_DONVI.TabIndex = 12;
            // 
            // CBB_PHAI
            // 
            this.CBB_PHAI.Enabled = false;
            this.CBB_PHAI.FormattingEnabled = true;
            this.CBB_PHAI.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.CBB_PHAI.Location = new System.Drawing.Point(22, 153);
            this.CBB_PHAI.Name = "CBB_PHAI";
            this.CBB_PHAI.Size = new System.Drawing.Size(121, 24);
            this.CBB_PHAI.TabIndex = 14;
            // 
            // CBB_COSO
            // 
            this.CBB_COSO.Enabled = false;
            this.CBB_COSO.FormattingEnabled = true;
            this.CBB_COSO.Items.AddRange(new object[] {
            "Cơ sở 1",
            "Cơ sở 2"});
            this.CBB_COSO.Location = new System.Drawing.Point(24, 322);
            this.CBB_COSO.Name = "CBB_COSO";
            this.CBB_COSO.Size = new System.Drawing.Size(121, 24);
            this.CBB_COSO.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 303);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 16);
            this.label10.TabIndex = 1;
            this.label10.Text = "Cơ Sở";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BTN_CAPNHAT);
            this.panel2.Controls.Add(this.BTN_XOA);
            this.panel2.Location = new System.Drawing.Point(6, 366);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 48);
            this.panel2.TabIndex = 1;
            // 
            // BTN_CAPNHAT
            // 
            this.BTN_CAPNHAT.Location = new System.Drawing.Point(162, 12);
            this.BTN_CAPNHAT.Name = "BTN_CAPNHAT";
            this.BTN_CAPNHAT.Size = new System.Drawing.Size(123, 23);
            this.BTN_CAPNHAT.TabIndex = 0;
            this.BTN_CAPNHAT.Text = "CẬP NHẬT";
            this.BTN_CAPNHAT.UseVisualStyleBackColor = true;
            this.BTN_CAPNHAT.Click += new System.EventHandler(this.BTN_CAPNHAT_Click);
            // 
            // BTN_XOA
            // 
            this.BTN_XOA.Location = new System.Drawing.Point(18, 12);
            this.BTN_XOA.Name = "BTN_XOA";
            this.BTN_XOA.Size = new System.Drawing.Size(138, 23);
            this.BTN_XOA.TabIndex = 0;
            this.BTN_XOA.Text = "XOÁ";
            this.BTN_XOA.UseVisualStyleBackColor = true;
            this.BTN_XOA.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(159, 248);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "TÌNH TRẠNG";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "KHOA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Ngày sinh";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Điện Thoại";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "ĐỊA CHỈ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Phái";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Họ Tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "MÃ SINH VIÊN";
            // 
            // TB_TINHTRANG
            // 
            this.TB_TINHTRANG.Location = new System.Drawing.Point(162, 267);
            this.TB_TINHTRANG.Name = "TB_TINHTRANG";
            this.TB_TINHTRANG.ReadOnly = true;
            this.TB_TINHTRANG.Size = new System.Drawing.Size(127, 22);
            this.TB_TINHTRANG.TabIndex = 1;
            // 
            // TB_SDT
            // 
            this.TB_SDT.Location = new System.Drawing.Point(22, 267);
            this.TB_SDT.Name = "TB_SDT";
            this.TB_SDT.ReadOnly = true;
            this.TB_SDT.Size = new System.Drawing.Size(134, 22);
            this.TB_SDT.TabIndex = 1;
            // 
            // TB_DIACHI
            // 
            this.TB_DIACHI.Location = new System.Drawing.Point(22, 209);
            this.TB_DIACHI.Name = "TB_DIACHI";
            this.TB_DIACHI.ReadOnly = true;
            this.TB_DIACHI.Size = new System.Drawing.Size(134, 22);
            this.TB_DIACHI.TabIndex = 1;
            // 
            // TB_NGAYSINH
            // 
            this.TB_NGAYSINH.Location = new System.Drawing.Point(160, 155);
            this.TB_NGAYSINH.Name = "TB_NGAYSINH";
            this.TB_NGAYSINH.ReadOnly = true;
            this.TB_NGAYSINH.Size = new System.Drawing.Size(125, 22);
            this.TB_NGAYSINH.TabIndex = 1;
            // 
            // TB_HOTEN
            // 
            this.TB_HOTEN.Location = new System.Drawing.Point(22, 100);
            this.TB_HOTEN.Name = "TB_HOTEN";
            this.TB_HOTEN.ReadOnly = true;
            this.TB_HOTEN.Size = new System.Drawing.Size(263, 22);
            this.TB_HOTEN.TabIndex = 1;
            // 
            // TB_MASINHVIEN
            // 
            this.TB_MASINHVIEN.Location = new System.Drawing.Point(22, 49);
            this.TB_MASINHVIEN.Name = "TB_MASINHVIEN";
            this.TB_MASINHVIEN.ReadOnly = true;
            this.TB_MASINHVIEN.Size = new System.Drawing.Size(263, 22);
            this.TB_MASINHVIEN.TabIndex = 1;
            // 
            // NHANVIEN_QUANLYSINHVIEN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GB_THÔNGTINCANHAN_GV);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupMONHOC);
            this.Name = "NHANVIEN_QUANLYSINHVIEN";
            this.Size = new System.Drawing.Size(932, 483);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupMONHOC.ResumeLayout(false);
            this.GB_THÔNGTINCANHAN_GV.ResumeLayout(false);
            this.GB_THÔNGTINCANHAN_GV.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BTN_THÊM;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupMONHOC;
        private System.Windows.Forms.GroupBox GB_THÔNGTINCANHAN_GV;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BTN_CAPNHAT;
        private System.Windows.Forms.Button BTN_XOA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_TINHTRANG;
        private System.Windows.Forms.TextBox TB_SDT;
        private System.Windows.Forms.TextBox TB_DIACHI;
        private System.Windows.Forms.TextBox TB_NGAYSINH;
        private System.Windows.Forms.TextBox TB_HOTEN;
        private System.Windows.Forms.TextBox TB_MASINHVIEN;
        private System.Windows.Forms.ComboBox CBB_DONVI;
        private System.Windows.Forms.ComboBox CBB_PHAI;
        private System.Windows.Forms.ComboBox CBB_COSO;
    }
}
