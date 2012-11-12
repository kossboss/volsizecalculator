using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolSizeCalc.Properties;

namespace VolSizeCalc
{
    

    public partial class Form2 : Form
    {
        bool apply = false;
        //bool cancel = false;

        public string mess { get; set; }
       
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.ico1;
            apply = false;
            this.KeyPreview = true;
           // bapp.Select();
            //cancel = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            applyclick();
        }
        private void applyclick() {
            // cancel = false;
            apply = true;
            this.mess = tb.Text.ToLower().Trim();
            this.Close();
            //Form1.add
        }

        private void bcan_Click(object sender, EventArgs e)
        {
            apply = false;
            //cancel = true;
            this.mess = "cancel";
            this.Close();
            //Form1.add
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {

        }

        //private void Form2_Leave(object sender, EventArgs e)
        //{
           
      //  }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mess = "cancel";
            if (apply == true) { this.mess = tb.Text.ToLower().Trim(); }

      

        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            //e.KeyValue 
           //MessageBox.Show(e.KeyValue.ToString());
            if (e.KeyValue == 13) { applyclick(); }
            
        }
    }
}
