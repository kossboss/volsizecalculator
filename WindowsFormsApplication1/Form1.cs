using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Reflection;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int pref_h, pref_w, pref_dy, plab_lx, plab_ly, pata_lx, pata_ly, pref_label_h;
        int ndef = 4;
        int numberofdrives;
        const double sparc_os = 2;  //gb
        const double sparc_swap = 0.256;  //gb
        const double intel_os = 4;  //gb
        const double intel_swap = 0.512; // gb
        const double arm_os = 4; //gb
        const double arm_swap = 0.512; //gb
        const double raid_overhead = 0.02; // percent
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Application.Cu
            //Assembly _assembly;
            //Stream _imageStream;
           
            //_assembly = Assembly.GetExecutingAssembly();
            //_imageStream = _assembly.GetManifestResourceStream("icon.ico");

            //this.Icon = new Icon(_imageStream);
         //
            //Icon ico = new Icon(
            // make 1 good sized one and then make it disappear
            init_vars();
            //
            tb_nd.Text = ndef.ToString();
            // make_all(ndef);
        }
        private void init_vars()
        {
            // lots of the controls on screen are predefined with settings, i could of put it here but instead its in the Designer.cs. mostly TabIndex and whats checked in the radioboxes
            // make 1 good sized one and then make it disappear
            
            lata0.Text = "Drive 0";
            ata0.Items.Add("500 GB", false);
            ata0.Items.Add("1000 GB", false);
            ata0.Items.Add("1500 GB", false);
            ata0.Items.Add("2000 GB", false);
            ata0.Items.Add("3000 GB", false);
            //ata0.Items.Add("4000 GB", false);
            //ata0.Items.Add("0.5 TB", false);
            //ata0.Items.Add("1.0 TB", false);
            //ata0.Items.Add("1.5 TB", false);
            //ata0.Items.Add("2.0 TB", false);
            //ata0.Items.Add("3.0 TB", false);
            //ata0.Items.Add("4000 GB", false);
            int toth = 0;
            for (int i = 0; i < ata0.Items.Count; i++) { toth += ata0.GetItemHeight(i); }
            ata0.Height = ata0.PreferredHeight;
            ata0.Width = ata0.PreferredSize.Width;
            // set them
            pref_h = ata0.PreferredHeight;
            pref_w = ata0.PreferredSize.Width;
            pref_dy = ata0.Location.Y - lata0.Location.Y;
            plab_lx = lata0.Location.X;
            plab_ly = lata0.Location.Y;
            pata_lx = ata0.Location.X;
            pata_ly = ata0.Location.Y;
            pref_label_h = lata0.Height;
            // disappear them
            ata0.Visible = false;
            lata0.Visible = false;
        }
        private void make_all(int num_of_drives)
        {
            int n = num_of_drives;
            int offx, offy, modd;
            offx = 0; offy = 0;
            // make them on the run
            if (n > 12) { modd = 12; } else { modd = 4; }
            for (int i = 1; i <= n; i++)
            {
                CheckedListBox ata = new CheckedListBox();
                Label lata = new Label();
                ata.Name = "ata" + i.ToString();
                lata.Name = "lata" + i.ToString();
                ata.Size = new Size(pref_w, pref_h);
                ata.Location = new Point(pata_lx + offx, pata_ly + offy);
                lata.Height = pref_h;
                lata.Width = pref_w;
                lata.Location = new Point(plab_lx + offx, plab_ly + offy);
                lata.Text = "Drive" + i.ToString();
                ata.Items.Add("500 GB", false);
                ata.Items.Add("1000 GB", false);
                ata.Items.Add("1500 GB", false);
                ata.Items.Add("2000 GB", false);
               ata.Items.Add("3000 GB", false);
                //ata.Items.Add("4000 GB", false);
                ata0.Items.Add("0.5 TB", false);
                ata0.Items.Add("1.0 TB", false);
                ata0.Items.Add("1.5 TB", false);
                ata0.Items.Add("2.0 TB", false);
                ata0.Items.Add("3.0 TB", false);
                ata.CheckOnClick = true;
                ata.SetSelected(0, false);
                ata.TabIndex = i + 4;
                this.Controls.Add(ata);
                this.Controls.Add(lata);
                ata.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(ata_ItemCheck);
                ata.MouseUp += new System.Windows.Forms.MouseEventHandler(ata_MouseUp);
                offx += pref_w + 10;
                if (((i % modd) == 0) & (i != 1))
                {  // first drive is 1 then 6th drive is 0
                    offx = 0;
                    offy += 110; // that equals 133
                }

            }
            int sw, sh, sw1, sh1;
            // get sizes after all controls besides the listbox are there to help size the listbox
            sw = this.PreferredSize.Width;
            sh = this.PreferredSize.Height;
            sw1 = 0;
            sh1 = 0;
            // make lbr
            ListBox lbr = new ListBox();
            lbr.Name = "lbr";
            lbr.Size = new Size(sw - 25, 390);
            lbr.Location = new Point(10, sh - 20);
            this.Controls.Add(lbr);
            // resize the window
            sw1 = this.PreferredSize.Width;
            sh1 = this.PreferredSize.Height;
            this.Width = sw1 + 10;
            this.Height = sh1 + 5;
            numberofdrives = n;
        }
        private void delete_all()
        {
            int num, num_cont;
            string itm;
            int t = 0;
            num = this.Controls.Count;
            num_cont = 0;
            do
            {
                foreach (var item in this.Controls)
                {
                    itm = ((Control)item).Name;
                    // MessageBox.Show(itm);
                    if (((itm.Contains("ata") | itm.Contains("lata")) & (itm.Contains("a0") == false)))
                    {
                        ++num_cont;
                    }
                    if (itm == "lbr") { ++num_cont; }

                }
                if (num_cont == 0) { break; } //if found no latas or atas then get out of loop

                foreach (var item in this.Controls)
                {
                    itm = ((Control)item).Name;
                    if (((itm.Contains("ata") | itm.Contains("lata")) & (itm.Contains("a0") == false)))
                    {
                        if (itm.Contains("lata") == false) // so if it is ata remove the event changed handler, label doesnt have events so we dont need to remove any from it
                        {
                            ((CheckedListBox)item).ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(ata_ItemCheck);
                            ((CheckedListBox)item).MouseUp -= new System.Windows.Forms.MouseEventHandler(ata_MouseUp);
                        }
                        this.Controls.Remove((Control)item);
                        ((Control)item).Dispose();
                    }
                    if (itm == "lbr")
                    {
                        this.Controls.Remove((Control)item);
                        ((Control)item).Dispose();
                    }
                }
                num_cont = 0;
                ++t;
            } while (true);
            //delete all variables values too
        }
        private void ata_ItemCheck(object sender, EventArgs e)
        {
            //string nameofcontrol = ((Control)sender).Name;
            //cwl(nameofcontrol);
            //calc(true);
            //addline(s);
        }
        private void ata_MouseUp(object sender, EventArgs e) //best time to calc when its good to calculate, not when itemcheck and not when click
        {
            calc(true);
        }
        private void cw(string msg) { System.Console.Write(msg); }
        private void cwl(string msg) { System.Console.WriteLine(msg); }
        private void tb_nd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tb_nd.Text == "")
                {
                    return;
                }
                int nd;
                nd = Convert.ToInt16(tb_nd.Text);
                delete_all();
                if ((nd <= 60) & (nd >= 0))
                {
                    make_all(nd);
                    calc(true);
                }
                else
                {
                    nd = 0;
                    make_all(nd);
                    calc(false);
                }
            }
            catch (Exception)
            {
            }
        }
        private void calc(bool works)
        {
            //  tssl.Text = "TESTTEST";
            string s;
            removelines();
          
            if (works == false)
            {
                s = "You must pick between 0 and 60 drives";
                addline(s);
            }
            else
            {
                actualcalc();
            }
        }
        private void actualcalc()
        {

            int snapshot;
            double os_size = 0;
            double swap_size = 0;
            double os_and_swap = 0;
            int unused = 0;
            string stype = " ";
            string itm = "";
            string biggestdrive = "";
            int r, r1;
            double r2;
            double size10 = 0;
            double final10 = 0;
            double final2 = 0;
            int c500, c1000, c1500, c2000, c3000;

            int ch5, ch10, ch15, ch20, ch30; //each chunk is 500 gb wide... so ch5 is from 0 to 500, ch10 is from 500 to 1000
            ch5 = 0; ch10 = 0; ch10 = 0; ch15 = 0; ch20 = 0; ch30 = 0;
            c500 = 0; c1000 = 0; c1500 = 0; c2000 = 0; c3000 = 0;
            ArrayList drives = new ArrayList();
            drives.Clear();
            snapshot = Convert.ToInt16(nSnap.Value);
            if (rbX86.Checked) { stype = "Intel / x86"; os_size = intel_os; swap_size = intel_swap; }
            else if (rbARM.Checked) { stype = "Arm"; os_size = arm_os; swap_size = arm_swap; }
            else if (rbSPARC.Checked) { stype = "Sparc"; os_size = sparc_os; swap_size = sparc_swap; }
            os_and_swap = os_size + swap_size;
            addline("Drives: " + numberofdrives);
            addline("Snapshot size [GB base 2]: " + snapshot);
            addline("Architecture: " + stype.ToString());
            // calc all drives
            foreach (var item in this.Controls)
            {
                r = -1; r1 = -1; //reset some variables
                biggestdrive = "NOTHING"; //size10 = 0; //reset some variables
                itm = ((Control)item).Name;
                if ((itm.Contains("ata") & (itm.Contains("a0") == false) & (itm.Contains("lata") == false)))
                {
                    CheckedListBox.CheckedItemCollection ii = ((CheckedListBox)item).CheckedItems;
                    if (ii.Count == 0)
                    {
                        ++unused;
                    }
                    foreach (string capd in ii)
                    {
                        biggestdrive = capd;

                    }
                    switch (biggestdrive)
                    {
                        case "0.5 TB": ++c500; break;
                        case "1.0 TB": ++c1000; break;
                        case "1.5 TB": ++c1500; break;
                        case "2.0 TB": ++c2000; break;
                        case "3.0 TB": ++c3000; break;
                    }

                    r = ((CheckedListBox)item).CheckedIndices.Count; // how many drives are selected there
                    r1 = ((CheckedListBox)item).Items.IndexOf(biggestdrive); //index of largest drive
                    if (biggestdrive != "NOTHING") // found a drive **
                    {
                        r2 = Convert.ToDouble(biggestdrive.Substring(0, biggestdrive.IndexOf("GB")));
                        ((CheckedListBox)item).SetSelected(r1, true);
                        if (r2 == 3000) { ++ch5; ++ch10; ++ch15; ++ch20; ++ch30; }
                        else if (r2 == 2000) { ++ch5; ++ch10; ++ch15; ++ch20; }
                        else if (r2 == 1500) { ++ch5; ++ch10; ++ch15; }
                        else if (r2 == 1000) { ++ch5; ++ch10; }
                        else if (r2 == 500) { ++ch5; }
                    }
                    else
                    {
                        r2 = 0;
                        ((CheckedListBox)item).SetSelected(0, false);

                    }
                    size10 += r2;
                }
            }
            final10 = size10;
            //double cf;            // cf = (1000 ^ 3) / (1024 ^ 3) = 09.93132257461;
            final2 = final10 * (1000 ^ 3) / (1024 ^ 3);
            int used = numberofdrives - unused;
            addline("Raid Overhead: " + raid_overhead * 100 + "%");
            addline("# of Drives - 500gb=" + c500 + " ,1000gb=" + c1000 + " ,1500gb=" + c1500 + " ,2000gb=" + c2000 + " ,3000gb=" + c3000);
            addline("Total Disk Size In Place [GB base 10]: " + String.Format("{0:0.000}", final10).ToString());
            addline("Total Disk Size In Place [GB base 2]: " + String.Format("{0:0.000}", final2).ToString());
            //// OLD ALGO HERE UN COMMENT IT FROM SELECTING FROM HERE TO THE END WHERE YOU SEE "THE END"
            addline("0-500 Chunks:" + ch5);
            addline("500-1000 Chunks:" + ch10);
            addline("1000-1500 Chunks:" + ch15);
            addline("1500-2000 Chunks:" + ch20);
            addline("2000-3000 Chunks:" + ch30);
            
            double toraid5, toraid6;
            toraid5 = 0;
            toraid6 = 0;
            // calc chunks into raid5 and 6 useable space (need pairs for raid5) and triplets for raid6
            if (ch5 == 1) { toraid5 += 0; toraid6 += 0; }
            else if (ch5 == 2) { toraid5 += 500; toraid6 += 0; }
            else if (ch5 == 3) { toraid5 += 1000; toraid6 += 500; }
            else if (ch5 > 3) { toraid5 += (ch5 - 1) * 500; toraid6 += (ch5 - 2) * 500; }
            if (ch10 == 1) { toraid5 += 0; toraid6 += 0; }
            else if (ch10 == 2) { toraid5 += 500; toraid6 += 0; }
            else if (ch10 == 3) { toraid5 += 1000; toraid6 += 500; }
            else if (ch10 > 3) { toraid5 += (ch10 - 1) * 500; toraid6 += (ch10 - 2) * 500; }
            if (ch15 == 1) { toraid5 += 0; toraid6 += 0; }
            else if (ch15 == 2) { toraid5 += 500; toraid6 += 0; }
            else if (ch15 == 3) { toraid5 += 1000; toraid6 += 500; }
            else if (ch15 > 3) { toraid5 += (ch15 - 1) * 500; toraid6 += (ch15 - 2) * 500; }
            if (ch20 == 1) { toraid5 += 0; toraid6 += 0; }
            else if (ch20 == 2) { toraid5 += 500; toraid6 += 0; }
            else if (ch20 == 3) { toraid5 += 1000; toraid6 += 500; }
            else if (ch20 > 3) { toraid5 += (ch20 - 1) * 500; toraid6 += (ch20 - 2) * 500; }
            if (ch30 == 1) { toraid5 += 0; toraid6 += 0; }
            else if (ch30 == 2) { toraid5 += 1000; toraid6 += 0; }
            else if (ch30 == 3) { toraid5 += 2000; toraid6 += 1000; }
            else if (ch30 > 3) { toraid5 += (ch30 - 1) * 1000; toraid6 += (ch30 - 2) * 1000; }
            double spaceR5tb, spaceR6tb;
            spaceR5tb = toraid5 / 1000;
            spaceR6tb = toraid6 / 1000;
            addline("Space Allotted in Base 10 - Raid5: " + String.Format("{0:0}", toraid5) + " GB, Raid6: " + String.Format("{0:0}", toraid6) + " GB");
            addline("Space Allotted in Base 10 - Raid5: " + String.Format("{0:0.000}", spaceR5tb) + " TB, Raid6: " + String.Format("{0:0.000}", spaceR6tb) + " TB");
            double spaceR5_2, spaceR6_2;
            double spaceR5_2_tb, spaceR6_2_tb;
            bool raid5 = false;
            bool raid6 = false;
            if (toraid5 > 0) { raid5 = true; }
            if (toraid6 > 0) { raid6 = true; }
            spaceR5_2 = toraid5 * 0.93132257461;
            spaceR6_2 = toraid6 * 0.93132257461;
            spaceR5_2_tb = spaceR5_2 / 1024;
            spaceR6_2_tb = spaceR6_2 / 1024;
            addline("Space Allotted in Base 2 - Raid5: " + String.Format("{0:0}", spaceR5_2) + " GB, Raid6: " + String.Format("{0:0}", spaceR6_2) + " GB");
            addline("Space Allotted in Base 2 - Raid5: " + String.Format("{0:0.000}", spaceR5_2_tb) + " TB, Raid6: " + String.Format("{0:0.000}", spaceR6_2_tb) + " TB");
            addline("NOTE: Every size value below is in base 2, 'real space'");
            double totalos, totalswap, totalosandswap;
            totalos = os_size * used;
            totalswap = swap_size * used;
            totalosandswap = totalos + totalswap;
            double afteros5, afteros6;
            afteros5 = spaceR5_2 - totalosandswap;
            afteros6 = spaceR6_2 - totalosandswap;
            double afterOverhead5, afterOverhead6;
            double ro5, ro6;
            afterOverhead5 = afteros5 * (1 - raid_overhead);
            afterOverhead6 = afteros6 * (1 - raid_overhead);
            double final5gb, final6gb;
            double final5tb, final6tb;
            final5gb = afterOverhead5 - snapshot;
            final6gb = afterOverhead6 - snapshot;
            final5tb = final5gb / 1024;
            final6tb = final6gb / 1024;
            if (raid5 == false)
            {
                afteros5 = 0;
                afterOverhead5 = 0;
                final5gb = 0;
                final5tb = 0;
            }
            if (raid6 == false)
            {
                afteros6 = 0;
                afterOverhead6 = 0;
                final6gb = 0;
                final6tb = 0;
            }
            ro5 = afteros5 * raid_overhead;
            ro6 = afteros6 * raid_overhead;
            addline("OS & SWAP will take up " + String.Format("{0:0.000}", os_size + swap_size) + " GB/Disk on " + used + " Disks, Totaling: " + String.Format("{0:0.000}", totalosandswap));
            addline("The raidover head at " + raid_overhead * 100 + "% will cost - Raid5: " + String.Format("{0:0}", ro5) + " GB Raid6: " + String.Format("{0:0}", ro6) + " GB");
            addline("After OS & SWAP - Raid5: " + String.Format("{0:0.000}", afteros5) + " GB, Raid6: " + String.Format("{0:0.000}", afteros6) + " GB");
            addline("After Overhead  - Raid5: " + String.Format("{0:0.000}", afterOverhead5) + " GB, Raid6: " + String.Format("{0:0.000}", afterOverhead6) + " GB");
            addline("After Snapshot  - Raid5: " + String.Format("{0:0.000}", final5gb) + " GB, Raid6: " + String.Format("{0:0.000}", final6gb) + " GB = FINAL VALUES");
            string pr5, pr6;
            pr5 = "NOT POSSIBLE";
            pr6 = "NOT POSSIBLE";
            if (raid5 == true) { pr5 = "POSSIBLE"; }
            if (raid6 == true) { pr6 = "POSSIBLE"; }
            addline("RAID5: " + pr5 + " - RAID6: " + pr6);
            addline("####### FINAL #######");
            addline("Raid5: " + String.Format("{0:0.000}", final5gb) + " GB, " + String.Format("{0:0.000}", final5tb) + " TB");
            addline("Raid6: " + String.Format("{0:0.000}", final6gb) + " GB, " + String.Format("{0:0.000}", final6tb) + " TB");
            tssl.Text = "Disks: " + used.ToString() + ", Given Space: " + (final10 / 1000).ToString() + " TB, ";// + "Final R5: " + String.Format("{0:0.000}", finalfs5tb) + " TB,  Final R6: " + String.Format("{0:0.000}", finalfs6tb) + " TB";
            if (used == 1)
            {
                double onediskgb, onedisktb;
                onediskgb = (final2 - totalosandswap) * (1 - raid_overhead) - snapshot;
                onedisktb = onediskgb / 1024;
                addline("####### FINAL WITH 1 DISK #######");
                addline("1 DISK FINAL: " + String.Format("{0:0.000}", onediskgb) + " GB, " + String.Format("{0:0.000}", onedisktb) + " TB");
                tssl.Text += "FINAL VOLUME SIZE: " + String.Format("{0:0.000}", onedisktb) + " TB";
            }
            else
            {
                tssl.Text += "FINAL R5: " + String.Format("{0:0.00}", final5tb) + " TB, FINAL R6: " + String.Format("{0:0.00}", final6tb) + " TB";
            }
        }
        private void addline(string str)
        {
            string itm;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    ((ListBox)item).Items.Add(str);
                }
            }
        }
        private void removelines()
        {
            string itm;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    ((ListBox)item).Items.Clear();

                }
            }
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            // testing stuff
        }

        private void rbSPARC_CheckedChanged(object sender, EventArgs e) { calc(true); }
        private void Form1_Shown(object sender, EventArgs e) { calc(true); }
        private void nSnap_ValueChanged(object sender, EventArgs e) { calc(true); }
        private void rbARM_CheckedChanged(object sender, EventArgs e) { calc(true); }
        private void rbX86_CheckedChanged(object sender, EventArgs e) { calc(true); }

        private void nSnap_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calc(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showhelp();
        }
        private void showhelp()
        {
            MessageBox.Show("NOTE: The numbers here are based on a calculation and may vary in the actual system. Also note this program is time independent meaning it does not care about the chronological order that you add or remove drives. In real-life each system preforms under its very own characteristics when drives are added and does depend on time(in a way), for example: if your first drive was a 2TB drive, I would not be able to add smaller drives there after to expand. So hence, each calculation is as if you have factory defaulted and built an XRAID system out of the drives that you have selected. Other factors determine what final size your system will be. Regards, Kostia Khlebopros of NETGEAR.");
        }
    }
}



//// OLD ALGO HERE UN COMMENT IT FROM SELECTING FROM HERE TO THE END WHERE YOU SEE "THE END"
//double totalos, totalswap, totalosandswap;
//double finalfs5gb, finalfs6gb;
//double finalfs5tb, finalfs6tb;
//double onediskgb, onedisktb;
//double finalimfs5gb, finalimfs6gb;
//double finalimfs5tb, finalimfs6tb;
////raid 5 calcs (need 2 disks of same size)
//double w5005, w10005, w15005, w20005, w30005, w5;
//double w5006, w10006, w15006, w20006, w30006, w6;
//w5005 = 0; w10005 = 0; w15005 = 0; w20005 = 0; w30005 = 0;
//w5006 = 0; w10006 = 0; w15006 = 0; w20006 = 0; w30006 = 0;
//double r5005, r10005, r15005, r20005, r30005, r5;
//double r5006, r10006, r15006, r20006, r30006, r6;
//r5005 = 0; r10005 = 0; r15005 = 0; r20005 = 0; r30005 = 0;
//r5006 = 0; r10006 = 0; r15006 = 0; r20006 = 0; r30006 = 0;
//w6 = 0; w5 = 0; r5 = 0; r6 = 0;
//if (c500 >= 2) { r5005 = (c500 - 1) * 0.93132257461 * 500; } else { w5005 = (c500) * 0.93132257461 * 500; }
//if (c1500 >= 2) { r15005 = (c1500 - 1) * 0.93132257461 * 1500; } else { w15005 = c1500 * 0.93132257461 * 1500; }
//if (c2000 >= 2) { r20005 = (c2000 - 1) * 0.93132257461 * 2000; } else { w20005 = c2000 * 0.93132257461 * 2000; }
//if (c3000 >= 2) { r30005 = (c3000 - 1) * 0.93132257461 * 3000; } else { w30005 = c3000 * 0.93132257461 * 3000; }
//w5 = w5005 + w10005 + w15005 + w20005 + w30005;
//r5 = r5005 + r10005 + r15005 + r20005 + r30005;
////raid 6 calcs (need 3 disks of same size)
//if (c500 >= 3) { r5006 = (c500 - 2) * 0.93132257461 * 500; } else { w5006 = c500 * 0.93132257461 * 500; }
//if (c1000 >= 3) { r10006 = (c1000 - 2) * 0.93132257461 * 1000; } else { w10006 = c1000 * 0.93132257461 * 1000; }
//if (c1500 >= 3) { r15006 = (c1500 - 2) * 0.93132257461 * 1500; } else { w15006 = c1500 * 0.93132257461 * 1500; }
//if (c2000 >= 3) { r20006 = (c2000 - 2) * 0.93132257461 * 2000; } else { w20006 = c2000 * 0.93132257461 * 2000; }
//if (c3000 >= 3) { r30006 = (c3000 - 2) * 0.93132257461 * 3000; } else { w30006 = c3000 * 0.93132257461 * 3000; }
//w6 = w5006 + w10006 + w15006 + w20006 + w30006;
//r6 = r5006 + r10006 + r15006 + r20006 + r30006;
//addline("# of 500 GB: " + c500.ToString());
//addline("# of 1000 GB: " + c1000.ToString());
//addline("# of 1500 GB: " + c1500.ToString());
//addline("# of 2000 GB: " + c2000.ToString());
//addline("# of 3000 GB: " + c3000.ToString());
//addline("Number of Unused/Used Disk [GB base 2]: " + unused + " / " + used);
//addline("Raid 5 Space Used (minus 1 disk) [GB base 2]: " + String.Format("{0:0.000}", r5));
//addline("Raid 6 Space Used (minus 2 disks) [GB base 2]: " + String.Format("{0:0.000}", r6));
//addline("Raid 5 Wasted Space (missing pairs of disks) [GB base 2]: " + String.Format("{0:0.000}", w5));
//addline("Raid 6 Wasted Space (missing pairs of disks) [GB base 2]: " + String.Format("{0:0.000}", w6));
//totalos = os_size * used;
//totalswap = swap_size * used;
//totalosandswap = totalos + totalswap;
//addline(os_size.ToString() + " GB os + " + swap_size.ToString() + " GB swap = " + os_and_swap.ToString() + " GB total [GBs IN BASE 2] per disk");
//addline(totalos + " GB os + " + totalswap + " GB swap = " + totalosandswap + " GB total [GBs IN BASE 2] TOTAL");
//finalfs5gb = (r5 - totalosandswap) * (1 - raid_overhead) - snapshot;
//finalfs6gb = (r6 - totalosandswap) * (1 - raid_overhead) - snapshot;
//finalfs5tb = finalfs5gb / 1024;
//finalfs6tb = finalfs6gb / 1024;
//onediskgb = (final2 - totalosandswap) * (1 - raid_overhead) - snapshot;
//onedisktb = onediskgb / 1024;
//finalimfs5gb = (final2 - (final2 / used) - totalosandswap) * (1 - raid_overhead) - snapshot;
//finalimfs6gb = (final2 - ((final2 / used) * 2) - totalosandswap) * (1 - raid_overhead) - snapshot;
//finalimfs5tb = finalimfs5gb / 1024;
//finalimfs6tb = finalimfs6gb / 1024;
//addline("** NOTE: All values below are the results and are in base 2. ");
//if (used == 1)
//{
//    addline("1 DISK FINAL USEABLE VOLUME SIZE: " + String.Format("{0:0.000}", onediskgb) + " GB = " + String.Format("{0:0.000}", onedisktb) + " TB");
//    tssl.Text = "Disks: 1, Given Space: " + (final10 / 1000).ToString() + " TB, Final: " + String.Format("{0:0.000}", onedisktb) + " TB";

//}
//else
//{
//    addline("** NOTE: IMAGINARY meaning if didnt need pairs. ");
//    addline("RAID 5 IMAGINARY VOL SIZE: " + String.Format("{0:0.000}", finalimfs5gb) + " GB = " + String.Format("{0:0.000}", finalimfs5tb) + " TB");

//    if (finalimfs6gb < 0)
//    {
//        addline("RAID 6 IMAGINARY VOL SIZE: N/A");
//        //addline("NOTE: even for imaginary no RAID 6");
//        //addline("      if no triplets, RAID 5 would take over");
//    }
//    else
//    {
//        addline("RAID 6 IMAGINARY VOL SIZE: " + String.Format("{0:0.000}", finalimfs6gb) + " GB = " + String.Format("{0:0.000}", finalimfs6tb) + " TB");
//    }
//    // BELOW ARE THE RESULT NUMBERS
//    // BELOW ARE THE RESULT NUMBERS
//    // BELOW ARE THE RESULT NUMBERS
//    // BELOW ARE THE RESULT NUMBERS
//    // BELOW ARE THE RESULT NUMBERS
//    addline("** RAID 5 FINAL USEABLE VOLUME SIZE: " + String.Format("{0:0.000}", finalfs5gb) + " GB = " + String.Format("{0:0.000}", finalfs5tb) + " TB");
//    if (finalfs6gb < 0)
//    {
//        addline("NOTE: No triplets, No Raid 6");
//        tssl.Text = "Disks: " + used.ToString() + ", Given Space: " + (final10 / 1000).ToString() + " TB, Final R5: " + String.Format("{0:0.000}", finalfs5tb) + " TB,  Final R6: N/A";
//     }
//    else
//    {
//        addline("** RAID 6 FINAL USEABLE VOLUME SIZE: " + String.Format("{0:0.000}", finalfs6gb) + " GB = " + String.Format("{0:0.000}", finalfs6tb) + " TB");
//        tssl.Text = "Disks: " + used.ToString() + ", Given Space: " + (final10 / 1000).ToString() + " TB, Final R5: " + String.Format("{0:0.000}", finalfs5tb) + " TB,  Final R6: " + String.Format("{0:0.000}", finalfs6tb) + " TB"; 
//    }
//}
////  "THE END"

