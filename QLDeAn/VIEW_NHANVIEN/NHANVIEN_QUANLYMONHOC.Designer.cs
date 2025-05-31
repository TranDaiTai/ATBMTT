namespace QLDeAn.VIEW_NHANVIEN
{
    partial class NHANVIEN_QUANLYMONHOC
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
            this.groupMONHOC = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTN_DANGKYMON = new System.Windows.Forms.Button();
            this.BTN_THÊM = new System.Windows.Forms.Button();
            this.TB_MAHOCPHAN = new System.Windows.Forms.TextBox();
            this.TB_MAGIAOVIEN = new System.Windows.Forms.TextBox();
            this.TB_HOCKI = new System.Windows.Forms.TextBox();
            this.TB_NAM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BTN_CAPNHAT = new System.Windows.Forms.Button();
            this.BTN_XOA = new System.Windows.Forms.Button();
            this.TB_MAMONHOC = new System.Windows.Forms.TextBox();
            this.GB_MONHOCDUOCCHON = new System.Windows.Forms.GroupBox();
            this.groupMONHOC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GB_MONHOCDUOCCHON.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMONHOC
            // 
            this.groupMONHOC.Controls.Add(this.dataGridView1);
            this.groupMONHOC.Location = new System.Drawing.Point(14, 17);
            this.groupMONHOC.Name = "groupMONHOC";
            this.groupMONHOC.Size = new System.Drawing.Size(575, 414);
            this.groupMONHOC.TabIndex = 0;
            this.groupMONHOC.TabStop = false;
            this.groupMONHOC.Text = "DANH SÁCH MÔN HỌC";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.BTN_DANGKYMON);
            this.panel1.Controls.Add(this.BTN_THÊM);
            this.panel1.Location = new System.Drawing.Point(14, 437);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 48);
            this.panel1.TabIndex = 6;
            // 
            // BTN_DANGKYMON
            // 
            this.BTN_DANGKYMON.Location = new System.Drawing.Point(166, 12);
            this.BTN_DANGKYMON.Name = "BTN_DANGKYMON";
            this.BTN_DANGKYMON.Size = new System.Drawing.Size(115, 23);
            this.BTN_DANGKYMON.TabIndex = 0;
            this.BTN_DANGKYMON.Text = "Đăng ký môn";
            this.BTN_DANGKYMON.UseVisualStyleBackColor = true;
            this.BTN_DANGKYMON.Click += new System.EventHandler(this.BTN_DANGKYMON_Click);
            // 
            // BTN_THÊM
            // 
            this.BTN_THÊM.Location = new System.Drawing.Point(18, 12);
            this.BTN_THÊM.Name = "BTN_THÊM";
            this.BTN_THÊM.Size = new System.Drawing.Size(115, 23);
            this.BTN_THÊM.TabIndex = 0;
            this.BTN_THÊM.Text = "THÊM MÔN HỌC";
            this.BTN_THÊM.UseVisualStyleBackColor = true;
            this.BTN_THÊM.Click += new System.EventHandler(this.BTN_THÊM_Click);
            // 
            // TB_MAHOCPHAN
            // 
            this.TB_MAHOCPHAN.Location = new System.Drawing.Point(22, 100);
            this.TB_MAHOCPHAN.Name = "TB_MAHOCPHAN";
            this.TB_MAHOCPHAN.ReadOnly = true;
            this.TB_MAHOCPHAN.Size = new System.Drawing.Size(263, 22);
            this.TB_MAHOCPHAN.TabIndex = 1;
            // 
            // TB_MAGIAOVIEN
            // 
            this.TB_MAGIAOVIEN.Location = new System.Drawing.Point(22, 153);
            this.TB_MAGIAOVIEN.Name = "TB_MAGIAOVIEN";
            this.TB_MAGIAOVIEN.ReadOnly = true;
            this.TB_MAGIAOVIEN.Size = new System.Drawing.Size(263, 22);
            this.TB_MAGIAOVIEN.TabIndex = 1;
            // 
            // TB_HOCKI
            // 
            this.TB_HOCKI.Location = new System.Drawing.Point(22, 209);
            this.TB_HOCKI.Name = "TB_HOCKI";
            this.TB_HOCKI.ReadOnly = true;
            this.TB_HOCKI.Size = new System.Drawing.Size(134, 22);
            this.TB_HOCKI.TabIndex = 1;
            // 
            // TB_NAM
            // 
            this.TB_NAM.Location = new System.Drawing.Point(162, 209);
            this.TB_NAM.Name = "TB_NAM";
            this.TB_NAM.ReadOnly = true;
            this.TB_NAM.Size = new System.Drawing.Size(127, 22);
            this.TB_NAM.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "MÃ MÔN HỌC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "MÃ HỌC PHẦN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "MÃ GIÁO VIÊN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "HỌC KÌ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "NĂM";
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
            this.BTN_XOA.Click += new System.EventHandler(this.BTN_XOA_Click);
            // 
            // TB_MAMONHOC
            // 
            this.TB_MAMONHOC.Location = new System.Drawing.Point(22, 49);
            this.TB_MAMONHOC.Name = "TB_MAMONHOC";
            this.TB_MAMONHOC.ReadOnly = true;
            this.TB_MAMONHOC.Size = new System.Drawing.Size(263, 22);
            this.TB_MAMONHOC.TabIndex = 1;
            this.TB_MAMONHOC.TextChanged += new System.EventHandler(this.TB_MANHANVIEN_TextChanged);
            // 
            // GB_MONHOCDUOCCHON
            // 
            this.GB_MONHOCDUOCCHON.Controls.Add(this.TB_MAMONHOC);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.panel2);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.label6);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.label5);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.label3);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.label2);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.label1);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.TB_NAM);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.TB_HOCKI);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.TB_MAGIAOVIEN);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.TB_MAHOCPHAN);
            this.GB_MONHOCDUOCCHON.Location = new System.Drawing.Point(621, 17);
            this.GB_MONHOCDUOCCHON.Name = "GB_MONHOCDUOCCHON";
            this.GB_MONHOCDUOCCHON.Size = new System.Drawing.Size(312, 468);
            this.GB_MONHOCDUOCCHON.TabIndex = 5;
            this.GB_MONHOCDUOCCHON.TabStop = false;
            this.GB_MONHOCDUOCCHON.Text = "MON HOC ĐƯỢC CHỌN";
            this.GB_MONHOCDUOCCHON.Enter += new System.EventHandler(this.GB_MONHOCDUOCCHON_Enter);
            // 
            // NHANVIEN_QUANLYMONHOC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GB_MONHOCDUOCCHON);
            this.Controls.Add(this.groupMONHOC);
            this.Name = "NHANVIEN_QUANLYMONHOC";
            this.Size = new System.Drawing.Size(950, 509);
            this.groupMONHOC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.GB_MONHOCDUOCCHON.ResumeLayout(false);
            this.GB_MONHOCDUOCCHON.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMONHOC;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTN_THÊM;
        private System.Windows.Forms.TextBox TB_MAHOCPHAN;
        private System.Windows.Forms.TextBox TB_MAGIAOVIEN;
        private System.Windows.Forms.TextBox TB_HOCKI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BTN_CAPNHAT;
        private System.Windows.Forms.Button BTN_XOA;
        private System.Windows.Forms.TextBox TB_MAMONHOC;
        private System.Windows.Forms.GroupBox GB_MONHOCDUOCCHON;
        private System.Windows.Forms.TextBox TB_NAM;
        private System.Windows.Forms.Button BTN_DANGKYMON;
    }
}
