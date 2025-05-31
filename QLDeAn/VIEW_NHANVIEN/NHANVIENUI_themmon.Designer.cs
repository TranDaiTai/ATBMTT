namespace QLDeAn.VIEW_NHANVIEN
{
    partial class NHANVIENUI_themmon
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GB_MONHOCDUOCCHON = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BTN_CAPNHAT = new System.Windows.Forms.Button();
            this.BTN_XOA = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_MAGIAOVIEN = new System.Windows.Forms.TextBox();
            this.TB_MAHOCPHAN = new System.Windows.Forms.TextBox();
            this.GB_MONHOCDUOCCHON.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB_MONHOCDUOCCHON
            // 
            this.GB_MONHOCDUOCCHON.Controls.Add(this.panel2);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.label3);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.label2);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.TB_MAGIAOVIEN);
            this.GB_MONHOCDUOCCHON.Controls.Add(this.TB_MAHOCPHAN);
            this.GB_MONHOCDUOCCHON.Location = new System.Drawing.Point(42, 12);
            this.GB_MONHOCDUOCCHON.Name = "GB_MONHOCDUOCCHON";
            this.GB_MONHOCDUOCCHON.Size = new System.Drawing.Size(312, 468);
            this.GB_MONHOCDUOCCHON.TabIndex = 6;
            this.GB_MONHOCDUOCCHON.TabStop = false;
            this.GB_MONHOCDUOCCHON.Text = "MON HOC ĐƯỢC CHỌN";
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
            this.BTN_CAPNHAT.Text = "HUỶ";
            this.BTN_CAPNHAT.UseVisualStyleBackColor = true;
            this.BTN_CAPNHAT.Click += new System.EventHandler(this.BTN_huy_Click);
            // 
            // BTN_XOA
            // 
            this.BTN_XOA.Location = new System.Drawing.Point(18, 12);
            this.BTN_XOA.Name = "BTN_XOA";
            this.BTN_XOA.Size = new System.Drawing.Size(138, 23);
            this.BTN_XOA.TabIndex = 0;
            this.BTN_XOA.Text = "THÊM";
            this.BTN_XOA.UseVisualStyleBackColor = true;
            this.BTN_XOA.Click += new System.EventHandler(this.BTN_THEM_Click);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "MÃ HỌC PHẦN";
            // 
            // TB_MAGIAOVIEN
            // 
            this.TB_MAGIAOVIEN.Location = new System.Drawing.Point(22, 153);
            this.TB_MAGIAOVIEN.Name = "TB_MAGIAOVIEN";
            this.TB_MAGIAOVIEN.Size = new System.Drawing.Size(263, 22);
            this.TB_MAGIAOVIEN.TabIndex = 1;
            // 
            // TB_MAHOCPHAN
            // 
            this.TB_MAHOCPHAN.Location = new System.Drawing.Point(22, 100);
            this.TB_MAHOCPHAN.Name = "TB_MAHOCPHAN";
            this.TB_MAHOCPHAN.Size = new System.Drawing.Size(263, 22);
            this.TB_MAHOCPHAN.TabIndex = 1;
            // 
            // NHANVIENUI_themmon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 450);
            this.Controls.Add(this.GB_MONHOCDUOCCHON);
            this.Name = "NHANVIENUI_themmon";
            this.Text = "NHANVIENUI_themmon";
            this.GB_MONHOCDUOCCHON.ResumeLayout(false);
            this.GB_MONHOCDUOCCHON.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_MONHOCDUOCCHON;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BTN_CAPNHAT;
        private System.Windows.Forms.Button BTN_XOA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_MAGIAOVIEN;
        private System.Windows.Forms.TextBox TB_MAHOCPHAN;
    }
}