namespace VolSizeCalc
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ata0 = new System.Windows.Forms.CheckedListBox();
            this.lata0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_nd = new System.Windows.Forms.TextBox();
            this.rbX86 = new System.Windows.Forms.RadioButton();
            this.rbARM = new System.Windows.Forms.RadioButton();
            this.rbSPARC = new System.Windows.Forms.RadioButton();
            this.nSnap = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tssl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nSnap)).BeginInit();
            this.SuspendLayout();
            // 
            // ata0
            // 
            this.ata0.FormattingEnabled = true;
            this.ata0.Location = new System.Drawing.Point(18, 97);
            this.ata0.Name = "ata0";
            this.ata0.Size = new System.Drawing.Size(107, 154);
            this.ata0.TabIndex = 0;
            this.ata0.SelectedIndexChanged += new System.EventHandler(this.ata0_SelectedIndexChanged);
            // 
            // lata0
            // 
            this.lata0.AutoSize = true;
            this.lata0.Location = new System.Drawing.Point(18, 78);
            this.lata0.Name = "lata0";
            this.lata0.Size = new System.Drawing.Size(35, 13);
            this.lata0.TabIndex = 1;
            this.lata0.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "# of Drives (0 to 60):";
            // 
            // tb_nd
            // 
            this.tb_nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_nd.Location = new System.Drawing.Point(147, 10);
            this.tb_nd.Name = "tb_nd";
            this.tb_nd.Size = new System.Drawing.Size(56, 20);
            this.tb_nd.TabIndex = 0;
            this.tb_nd.TextChanged += new System.EventHandler(this.tb_nd_TextChanged);
            // 
            // rbX86
            // 
            this.rbX86.AutoSize = true;
            this.rbX86.Checked = true;
            this.rbX86.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbX86.Location = new System.Drawing.Point(21, 35);
            this.rbX86.Name = "rbX86";
            this.rbX86.Size = new System.Drawing.Size(84, 17);
            this.rbX86.TabIndex = 2;
            this.rbX86.TabStop = true;
            this.rbX86.Text = "Intel / x86";
            this.rbX86.UseVisualStyleBackColor = true;
            this.rbX86.CheckedChanged += new System.EventHandler(this.rbX86_CheckedChanged);
            // 
            // rbARM
            // 
            this.rbARM.AutoSize = true;
            this.rbARM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbARM.Location = new System.Drawing.Point(106, 35);
            this.rbARM.Name = "rbARM";
            this.rbARM.Size = new System.Drawing.Size(52, 17);
            this.rbARM.TabIndex = 3;
            this.rbARM.Text = "ARM";
            this.rbARM.UseVisualStyleBackColor = true;
            this.rbARM.CheckedChanged += new System.EventHandler(this.rbARM_CheckedChanged);
            // 
            // rbSPARC
            // 
            this.rbSPARC.AutoSize = true;
            this.rbSPARC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSPARC.Location = new System.Drawing.Point(156, 35);
            this.rbSPARC.Name = "rbSPARC";
            this.rbSPARC.Size = new System.Drawing.Size(58, 17);
            this.rbSPARC.TabIndex = 4;
            this.rbSPARC.Text = "Sparc";
            this.rbSPARC.UseVisualStyleBackColor = true;
            this.rbSPARC.CheckedChanged += new System.EventHandler(this.rbSPARC_CheckedChanged);
            // 
            // nSnap
            // 
            this.nSnap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nSnap.Location = new System.Drawing.Point(306, 10);
            this.nSnap.Name = "nSnap";
            this.nSnap.Size = new System.Drawing.Size(42, 20);
            this.nSnap.TabIndex = 1;
            this.nSnap.ValueChanged += new System.EventHandler(this.nSnap_ValueChanged);
            this.nSnap.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nSnap_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(211, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Snapshot (GB):";
            // 
            // tssl
            // 
            this.tssl.AutoSize = true;
            this.tssl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssl.ForeColor = System.Drawing.Color.Red;
            this.tssl.Location = new System.Drawing.Point(18, 58);
            this.tssl.Name = "tssl";
            this.tssl.Size = new System.Drawing.Size(26, 13);
            this.tssl.TabIndex = 9;
            this.tssl.Text = "tssl";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(366, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 10;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(366, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "READ THIS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cms
            // 
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(61, 4);
            this.cms.Opening += new System.ComponentModel.CancelEventHandler(this.cms_Opening);
            this.cms.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cms_ItemClicked);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(443, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 45);
            this.button3.TabIndex = 12;
            this.button3.Text = "Set Baseline for Comparison";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(219, 36);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(142, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Show More #s from Calc";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 577);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tssl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nSnap);
            this.Controls.Add(this.rbSPARC);
            this.Controls.Add(this.rbARM);
            this.Controls.Add(this.rbX86);
            this.Controls.Add(this.tb_nd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lata0);
            this.Controls.Add(this.ata0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "X-RAID Calculator (By: kossboss)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Click += new System.EventHandler(this.Form1_Click);
            ((System.ComponentModel.ISupportInitialize)(this.nSnap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ata0;
        private System.Windows.Forms.Label lata0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_nd;
        private System.Windows.Forms.RadioButton rbX86;
        private System.Windows.Forms.RadioButton rbARM;
        private System.Windows.Forms.RadioButton rbSPARC;
        private System.Windows.Forms.NumericUpDown nSnap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tssl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

