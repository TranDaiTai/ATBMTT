namespace QLDeAn.VIEW_NHANVIEN
{
    partial class NhanVienUI_ChinhsuaTT
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_CẬP_NHẬT = new System.Windows.Forms.Button();
            this.BTN_HUỶ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 75);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "NHẬP SỐ ĐIỆN THOẠI MỚI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "SỐ ĐIỆN THOẠI MỚI";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // BTN_CẬP_NHẬT
            // 
            this.BTN_CẬP_NHẬT.Location = new System.Drawing.Point(216, 213);
            this.BTN_CẬP_NHẬT.Name = "BTN_CẬP_NHẬT";
            this.BTN_CẬP_NHẬT.Size = new System.Drawing.Size(110, 23);
            this.BTN_CẬP_NHẬT.TabIndex = 3;
            this.BTN_CẬP_NHẬT.Text = "CẬP NHẬT";
            this.BTN_CẬP_NHẬT.UseVisualStyleBackColor = true;
            // 
            // BTN_HUỶ
            // 
            this.BTN_HUỶ.Location = new System.Drawing.Point(348, 213);
            this.BTN_HUỶ.Name = "BTN_HUỶ";
            this.BTN_HUỶ.Size = new System.Drawing.Size(84, 23);
            this.BTN_HUỶ.TabIndex = 4;
            this.BTN_HUỶ.Text = "HUỶ";
            this.BTN_HUỶ.UseVisualStyleBackColor = true;
            // 
            // NhanVienUI_ChinhsuaTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 279);
            this.Controls.Add(this.BTN_HUỶ);
            this.Controls.Add(this.BTN_CẬP_NHẬT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "NhanVienUI_ChinhsuaTT";
            this.Text = "NhanVienUI_ChinhsuaTT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_CẬP_NHẬT;
        private System.Windows.Forms.Button BTN_HUỶ;
    }
}