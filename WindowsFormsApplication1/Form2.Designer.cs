namespace VolSizeCalc
{
    partial class Form2
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
            this.bapp = new System.Windows.Forms.Button();
            this.bcan = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.Label();
            this.tb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bapp
            // 
            this.bapp.Location = new System.Drawing.Point(12, 55);
            this.bapp.Name = "bapp";
            this.bapp.Size = new System.Drawing.Size(75, 23);
            this.bapp.TabIndex = 1;
            this.bapp.Text = "Apply";
            this.bapp.UseVisualStyleBackColor = true;
            this.bapp.Click += new System.EventHandler(this.button1_Click);
            // 
            // bcan
            // 
            this.bcan.Location = new System.Drawing.Point(93, 55);
            this.bcan.Name = "bcan";
            this.bcan.Size = new System.Drawing.Size(75, 23);
            this.bcan.TabIndex = 2;
            this.bcan.Text = "Cancel";
            this.bcan.UseVisualStyleBackColor = true;
            this.bcan.Click += new System.EventHandler(this.bcan_Click);
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(13, 13);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(15, 13);
            this.lb.TabIndex = 2;
            this.lb.Text = "lb";
            // 
            // tb
            // 
            this.tb.Location = new System.Drawing.Point(12, 29);
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(156, 20);
            this.tb.TabIndex = 0;
            this.tb.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 87);
            this.Controls.Add(this.tb);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.bcan);
            this.Controls.Add(this.bapp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "Form2";
            this.Text = "Message";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button bapp;
        public System.Windows.Forms.Button bcan;
        public System.Windows.Forms.Label lb;
        public System.Windows.Forms.TextBox tb;
    }
}