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
using VolSizeCalc.Properties;
//using Oyster.Math;



namespace VolSizeCalc
{
    public partial class Form1 : Form
    {
        int pref_h, pref_w, pref_dy, plab_lx, plab_ly, pata_lx, pata_ly, pref_label_h;
        int ndef = 4;
        int numberofdrives;
        ArrayList drives = new ArrayList(60);
        const double sparc_os = 2;  //gb
        const double sparc_swap = 0.256;  //gb
        const double intel_os = 4;  //gb
        const double intel_swap = 0.512; // gb
        const double arm_os = 4; //gb
        const double arm_swap = 0.512; //gb
        const double raid_overhead = 0.02; // percent
        // Declare the ContextMenuStrip control. 
        // for difference
        //old
        int snapshot0;
        string architecture0;
        int drives0; //total drives in place basically used0 
        double totalgb100;
        double totalgb102;
        double osper0, ostotal0;
        double ro10, ro20;
        double afteros50, afteros50d;
        double afterro50, afterro60;
        double aftersnap50, aftersnap60;
        string r5poss0, r6poss0;
        double final5gb0, final6gb0, final5tb0, final6tb0;
        double onediskgb0, onedisktb0;
        //differences, note the 2 strings lines dont get differences
        int snapshot0d;
        int drives0d; //total drives in place basically used0 
        double totalgb100d;
        double totalgb102d;
        double osper0d, ostotal0d;
        double ro10d, ro20d;
        double afteros60, afteros60d;
        double afterro50d, afterro60d;
        double aftersnap50d, aftersnap60d;
        double final5gb0d, final6gb0d, final5tb0d, final6tb0d;
        double onediskgb0d, onedisktb0d;
        //
        bool torun;
        //
        const double cfTBtoTB = 0.90949470177; //on google: 1000*1000*1000*1000/1024/1024/1024
        const double cfGBtoGB = 0.93132257461; //on google: 1000*1000*1000/1024/1024/1024
        bool comparison_enabled = true;
        private bool comparision1st = false;
        private bool MoreInfo;

        private const int fsizereg = 10;

        private bool original_is_one_disk = false;
        private double correctional1 = 1.01594;
        private double correctional5 = 1.01594;
        private double correctional6 = 1.01594;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.ico1;
            torun = false;
            clear_baseline();
            // make 1 good sized one and then make it disappear
            init_vars();
            //
            tb_nd.Text = ndef.ToString();
            // make_all(ndef);
            if (comparison_enabled)
            { button3.Enabled = true; button3.Visible = true; }
            else
            { button3.Enabled = false; button3.Visible = false; }
        }
        private void clear_baseline()
        {
            //original
            snapshot0 = 0;
            architecture0 = "";
            drives0 = 0; //total drives in place basically used0 
            totalgb100 = 0;
            totalgb102 = 0;
            osper0 = 0;
            ostotal0 = 0;
            ro10 = 0;
            ro20 = 0;
            afterro50 = 0;
            afterro60 = 0;
            aftersnap50 = 0;
            aftersnap60 = 0;
            r5poss0 = "";
            r6poss0 = "";
            final5gb0 = 0;
            final6gb0 = 0;
            final5tb0 = 0;
            final6tb0 = 0;
            onediskgb0 = 0;
            onedisktb0 = 0;
            //            MessageBox.Show("Cleared");
            //difference
            clear_diffs();
            //key is to calculate the difference before setting the new 0
            //forgot
            afteros50 = 0;

            afteros60 = 0;

        }
        private void clear_diffs()
        {
            snapshot0d = 0;
            drives0d = 0;
            totalgb100d = 0;
            totalgb102d = 0;
            osper0d = 0;
            ostotal0d = 0;
            ro10d = 0;
            ro20d = 0;
            afterro50d = 0;
            afterro60d = 0;
            aftersnap50d = 0;
            aftersnap60d = 0;
            final5gb0d = 0;
            final6gb0d = 0;
            final5tb0d = 0;
            final6tb0d = 0;
            onediskgb0d = 0;
            onedisktb0d = 0;
            afteros60d = 0;
            afteros50d = 0;
        }

        private void init_drives()
        {
            double d = 500;
            drives.Add(d);
            d = 750; drives.Add(d);
            d = 1000; drives.Add(d);
            d = 1500; drives.Add(d);
            d = 2000; drives.Add(d);
            d = 3000; drives.Add(d);
            drives.Sort();
        }
        private void init_vars()
        {
            init_drives();
            // lots of the controls on screen are predefined with settings, i could of put it here but instead its in the Designer.cs. mostly TabIndex and whats checked in the radioboxes
            // make 1 good sized one and then make it disappear
            lata0.Text = "Drive 0";
            ata0.Font = new Font(ata0.Font.FontFamily, 7);
            ata0.Items.Add("500 GB", false);
            ata0.Items.Add("750 GB", false);
            ata0.Items.Add("1000 GB", false);
            ata0.Items.Add("1500 GB", false);
            ata0.Items.Add("2000 GB", false);
            ata0.Items.Add("3000 GB", false);
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

            // cms.Items.Add("Add Drive size");
            cms.Items.Add("Clear Selection");
            cms.Items.Add("Select All");
            cms.Items.Add("--------------");
            cms.Items.Add("Add Drive Size");
            cms.Items.Add("Remove Drive Size");
            cms.Items.Add("DEFAULT");
        }
        private void make_all(int num_of_drives)
        {
            comparision1st = false;
            clear_baseline();
            int n = num_of_drives;
            int offx, offy, modd;
            int d = drives.Count;
            offx = 0; offy = 0;
            // make them on the run
            if (n > 12) { modd = 12; } else { modd = 4; }
            for (int i = 1; i <= n; i++)
            {
                CheckedListBox ata = new CheckedListBox();
                Label lata = new Label();
                ContextMenu mtt = new ContextMenu();
                ContextMenuStrip mt = new ContextMenuStrip();
                ata.Name = "ata" + i.ToString();
                lata.Name = "lata" + i.ToString();
                ata.Size = new Size(pref_w, pref_h);
                ata.Font = new Font(ata.Font.FontFamily, 7);
                ata.Location = new Point(pata_lx + offx, pata_ly + offy);
                lata.Height = pref_h;
                lata.Width = pref_w;
                lata.Location = new Point(plab_lx + offx, plab_ly + offy + 4);
                lata.Text = "Drive" + i.ToString();
                for (int j = 0; j < d; j++)
                {
                    ata.Items.Add(drives[j] + " GB", false);
                }
                ata.CheckOnClick = true;
                ata.SetSelected(0, false);
                ata.TabIndex = i + 4;
                ata.ContextMenuStrip = cms;
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
            //ListBox lbr = new ListBox();
            RichTextBox lbr = new RichTextBox();
            lbr.Name = "lbr";
            //  lbr.HorizontalScrollbar = true;
            lbr.Size = new Size(sw - 25, 280); //used to be 390 then 350 then 280
            lbr.Location = new Point(10, sh - 20);
            // lbr.ZoomFactor = 0.5;// convert.float(zoomfactor);
            lbr.Font = new Font(lbr.Font.FontFamily, fsizereg, lbr.Font.Style);
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
            premakeall();
        }
        private void premakeall()
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
            removelines();

            if (works == false)
            {
                string s = "You must pick between 0 and 60 drives";
                addline(s);
            }
            else
            {
                actualcalc();
            }
        }
        private void actualcalc()
        {
            double six_to_one = 0;
            double five_to_one = 0;

            double six_to_one_percent = 0;
            double five_to_one_percent = 0;
            int drivessize = drives.Count;
            int[] hist = new int[drivessize];
            int[] chist = new int[drivessize];
            int[] chist5 = new int[drivessize];
            int[] chist6 = new int[drivessize];
            for (int g = 0; g < drivessize; g++)
            {
                hist[g] = 0;
                chist[g] = 0;
                chist5[g] = -1;
                chist6[g] = -2;
            }
            double osSize = 0;
            double swapSize = 0;
            // double os_and_swap = 0;
            int unused = 0;
            string stype = " ";
            string itm = "";
            string biggestdrive = "";
            int r, r1;
            double r2;
            double size10 = 0;
            double final10 = 0;
            double final2 = 0;
            int snapshot = Convert.ToInt16(nSnap.Value);
            if (rbX86.Checked)
            {
                stype = "Intel / x86";
                osSize = intel_os;
                swapSize = intel_swap;
            }
            else if (rbARM.Checked)
            {
                stype = "Arm";
                osSize = arm_os;
                swapSize = arm_swap;
            }
            else if (rbSPARC.Checked)
            {
                stype = "Sparc";
                osSize = sparc_os;
                swapSize = sparc_swap;
            }
            //os_and_swap = osSize + swapSize;

            // addline("Snapshot size [GB base 2]: " + snapshot);
            //   addline("Architecture: " + stype.ToString());
            foreach (var item in this.Controls)
            {
                r = -1;
                r1 = -1; //reset some variables
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
                    r = ((CheckedListBox)item).CheckedIndices.Count; // how many drives are selected there
                    r1 = ((CheckedListBox)item).Items.IndexOf(biggestdrive); //index of largest drive
                    if (biggestdrive != "NOTHING") // found a drive **
                    {
                        r2 = Convert.ToDouble(biggestdrive.Substring(0, biggestdrive.IndexOf("GB")));
                        ((CheckedListBox)item).SetSelected(r1, true);
                        //NEW WAY
                        int ind = drives.IndexOf(r2);
                        hist[ind]++;
                        for (int v = 0; v <= ind; v++)
                        {
                            chist[v]++;
                            chist5[v]++;
                            chist6[v]++;
                        }
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
            final2 = final10 * cfGBtoGB;
            int used = numberofdrives - unused;
            double z10 = final10;
            double z2 = final2;
            string ss = "";
            string se = "";
            double q10 = 0;
            double q2 = 0;
            double q = 0;
            double toraid5 = 0;
            double toraid6 = 0;
            double de, diffd;
            double ds = 0;
            de = 0;
            diffd = 0;
            for (int c = 0; c < drivessize; c++)
            {

                if (c == 0)
                {
                    ds = 0;
                }
                else
                {
                    ds = Convert.ToDouble(drives[c - 1]);
                }
                de = Convert.ToDouble(drives[c]);
                diffd = de - ds;
                //
                if (c == 0)
                {
                    ss = "0";
                    se = drives[c].ToString();
                }
                else
                {
                    ss = drives[c - 1].ToString();
                    se = drives[c].ToString();
                }
                q = Convert.ToDouble(chist[c]);
                q10 = q * diffd / 1000;
                q2 = (q * diffd * cfGBtoGB) / 1024;
                if ((chist5[c]) > 0) //if negative or zero dont count
                {
                    toraid5 += Convert.ToDouble(chist5[c]) * diffd;
                }
                if ((chist6[c]) > 0) //if negative or zero dont count
                {
                    toraid6 += Convert.ToDouble(chist6[c]) * diffd;
                }
            }
            double spaceR5Tb = toraid5 / 1000;
            double spaceR6Tb = toraid6 / 1000;
            bool raid5 = false;
            bool raid6 = false;
            if (toraid5 > 0)
            {
                raid5 = true;
            }
            if (toraid6 > 0)
            {
                raid6 = true;
            }

            double spaceR52 = toraid5 * cfGBtoGB;
            double spaceR52Tb = spaceR52 / 1024;
            // cwl("GB: " + toraid5.ToString() + " --> " + spaceR52.ToString());
            //  cwl("TB: " + spaceR5Tb.ToString() + " --> " + spaceR52Tb.ToString());

            double spaceR62 = toraid6 * cfGBtoGB;
            double spaceR62Tb = spaceR62 / 1024;
            double totalos = osSize * used;
            double totalswap = swapSize * used;
            double totalosandswap = totalos + totalswap;
            double afteros5 = spaceR52 - totalosandswap;
            double afteros6 = spaceR62 - totalosandswap;
            double afterOverhead5 = afteros5 * (1 - raid_overhead);
            double afterOverhead6 = afteros6 * (1 - raid_overhead);
            double final5Gb = (afterOverhead5 - snapshot) * correctional5;
            double final6Gb = (afterOverhead6 - snapshot) * correctional6;
            double final5Tb = (final5Gb / 1024) * correctional5;
            double final6Tb = (final6Gb / 1024) * correctional6;
            if (raid5 == false)
            {
                afteros5 = 0;
                afterOverhead5 = 0;
                final5Gb = 0;
                final5Tb = 0;
            }
            if (raid6 == false)
            {
                afteros6 = 0;
                afterOverhead6 = 0;
                final6Gb = 0;
                final6Tb = 0;
            }
            double ro5 = afteros5 * raid_overhead;
            double ro6 = afteros6 * raid_overhead;
            double osperdisk = osSize + swapSize;
            string pr5, pr6;
            pr5 = "NOT POSSIBLE";
            pr6 = "NOT POSSIBLE";
            if (raid5 == true)
            {
                pr5 = "POSSIBLE";
            }
            if (raid6 == true)
            {
                pr6 = "POSSIBLE";
            }
            tssl.Text = "Disks: " + used.ToString() + ", Given Space: " + (final10 / 1000).ToString() + " TB, ";
            // + "Final R5: " + String.Format("{0:0.000}", finalfs5tb) + " TB,  Final R6: " + String.Format("{0:0.000}", finalfs6tb) + " TB";
            double onediskgb, onedisktb;
            onediskgb = 0;
            onedisktb = 0;
            // TEST
            //////////////////////////////////////// 1.6 fix DECIDED THIS IS NOT SUPPORTED
            bool showanotherline = false;
            if (drives0 > 1 && used == 1)
            {

                // onediskgb0=final5gb0;

                //   onediskgb05 = final5gb0;
                //  onedisktb06 = final6tb0;
               // cwl("WTF WTF WTF WTF!!! HEY HEY");
                showanotherline = true;
                //  onedisktb0 = final6tb0;

                // final5gb0 = onediskgb0; 
                //final6gb0 = onediskgb0; 
                // final5tb0 = onedisktb0; 
                // final6tb0 = onedisktb0; 
                // original_is_one_disk = true;
            }

            //







            if (used == 1)
            {
                onediskgb = ((final2 - totalosandswap) * (1 - raid_overhead) - snapshot) * correctional1; // factor of 1.0.1594 was calculated during call
                // correctional factor =(1.01594);
                // before that factor
                //                ===WITH 1 DISK===
                //0.887 TB    <- my calc 1 
                //or
                //0.883 TB (with raid)
                //923 GB   <-actual = 0.901 TB
                //my calculations
                //908.288 TB  <- my calc 1
                //
                //difference:  actual-estimate= 923gb - 908.288gb = 14.712 GB off == percent off from actual   14.712/923 = 1.594% off
                //difference:  actual-estimate= 0.901tb - 0.887 TB = 0.014 TB off ~ 14.336 TB == percent off from actual = 1.55% off
                //
                //:ADJUST MY CALC BY:
                //My value * 1.01594 = real value

                onedisktb = onediskgb / 1024;
                tssl.Text += "FINAL VOLUME SIZE: " + String.Format("{0:0.000}", onedisktb) + " TB";
            }
            else
            {
                tssl.Text += "FINAL R5: " + String.Format("{0:0.00}", final5Tb) + " TB, FINAL R6: " +
                             String.Format("{0:0.00}", final6Tb) + " TB";
            }
            if (torun == true)
            {
                //difference stuff here
                //original
                // differences are already 0 at this point
                snapshot0 = snapshot;
                architecture0 = stype;
                drives0 = used; //total drives in place basically used0 
                totalgb100 = z10;
                totalgb102 = z2;
                osper0 = osperdisk;
                ostotal0 = totalosandswap;
                ro10 = ro5;
                ro20 = ro6;
                afteros50 = afteros5;
                afteros60 = afteros6;
                afterro50 = afterOverhead5;
                afterro60 = afterOverhead6;
                aftersnap50 = final5Gb;
                aftersnap60 = final6Gb;
                r5poss0 = pr5;
                r6poss0 = pr6;
                final5gb0 = final5Gb;
                final6gb0 = final6Gb;
                final5tb0 = final5Tb;
                final6tb0 = final6Tb;

                onediskgb0 = onediskgb;
                onedisktb0 = onedisktb;


                // cwl("HOW ARE YOU?");
                //  if (used==1) { final5gb0 = onediskgb0; final6gb0 = onediskgb0; cwl("GOOD"); }
            }
            else
            {
                if (comparision1st)
                {

                    //difference
                    snapshot0d = snapshot - snapshot0;
                    drives0d = used - drives0;
                    totalgb100d = z10 - totalgb100;
                    totalgb102d = z2 - totalgb102;
                    osper0d = osperdisk - osper0;
                    ostotal0d = totalosandswap - ostotal0;
                    ro10d = ro5 - ro10;
                    ro20d = ro6 - ro20;
                    afteros50d = afteros5 - afteros50;
                    afteros60d = afteros6 - afteros60;
                    afterro50d = afterOverhead5 - afterro50;
                    afterro60d = afterOverhead6 - afterro60;
                    aftersnap50d = final5Gb - aftersnap50;
                    aftersnap60d = final6Gb - aftersnap60;
                    final5gb0d = final5Gb - final5gb0;
                    final6gb0d = final6Gb - final6gb0;
                    final5tb0d = final5Tb - final5tb0;
                    final6tb0d = final6Tb - final6tb0;
                    onediskgb0d = onediskgb - onediskgb0;
                    onedisktb0d = onedisktb - onedisktb0;
                    onedisktb0d.ToString();

                }
            }

            //key is to calculate the difference before setting the new 0

            //1.6 fix 1 disk original

            if (torun && used == 1)
            {
                final5gb0 = onediskgb0;
                final6gb0 = onediskgb0;
                final5tb0 = onedisktb0;
                final6tb0 = onedisktb0;
                original_is_one_disk = true;
            }

            //////////////////////////////////////// 1.6 fix DECIDED THIS IS NOT SUPPORTED
            //////////////////////////////////////bool showanotherline = false;
            //////////////////////////////////////if ( drives0 > 1 && used == 1 ) 
            //////////////////////////////////////{

            //////////////////////////////////////   // onediskgb0=final5gb0;
            //////////////////////////////////////    onediskgb0 = final5gb0;
            //////////////////////////////////////    onedisktb06 = final6tb0;
            //////////////////////////////////////    showanotherline = true;
            //////////////////////////////////////  //  onedisktb0 = final6tb0;

            //////////////////////////////////////   // final5gb0 = onediskgb0; 
            //////////////////////////////////////   // final6gb0 = onediskgb0; 
            //////////////////////////////////////   // final5tb0 = onedisktb0; 
            //////////////////////////////////////   // final6tb0 = onedisktb0; 
            //////////////////////////////////////   // original_is_one_disk = true;
            //////////////////////////////////////}



            torun = false;
            //show results
            addline("# of Slots: " + numberofdrives);
            addword_bold("Snapshot size [GB base 2]: ");
            addword_bred(snapshot.ToString());
            addword("  ");
            addword_bblue(snapshot0.ToString());
            addword("  ");
            addword_blue(snapshot0d.ToString());
            addnl();
            addword_bold("Architecture: ");
            addword_bred(stype);
            addword("  ");
            addword_bblue(architecture0);
            addnl();
            addword_bold("Used Drives: ");
            addword_bred(used.ToString()); //new
            addword("  ");
            addword_bblue(drives0.ToString()); //old
            addword("  ");
            addword_blue(drives0d.ToString()); //diff
            addnl();
            addline("Raid Overhead: " + raid_overhead * 100 + "%");
            addword("# of Drives -");
            for (int o = 0; o < drivessize; o++)
            {
                if (o == 0)
                {
                    addword(" ");
                }
                addword(drives[o].ToString() + "GB=");
                addword_bold(hist[o].ToString());
                if (o != (drivessize - 1))
                {
                    addword(", ");
                }
            }
            addnl();
            addword_bold("Total Disk Size In Place [GB base 10]: ");
            addword_bred(String.Format("{0:0.000}", final10).ToString());
            addword("  ");
            addword_bblue(String.Format("{0:0.000}", totalgb100).ToString());
            addword("  ");
            addword_blue(String.Format("{0:0.000}", totalgb100d).ToString());
            addnl();
            addword_bold("Total Disk Size In Place [GB base 2]: ");
            addword_bred((String.Format("{0:0.000}", final2).ToString()));
            addword("  ");
            addword_bblue((String.Format("{0:0.000}", totalgb102).ToString()));
            addword("  ");
            addword_blue(String.Format("{0:0.000}", totalgb102d).ToString());
            addnl();
            if (MoreInfo)
            {

                for (int c = 0; c < drivessize; c++)
                {

                    if (c == 0)
                    {
                        ds = 0;
                    }
                    else
                    {
                        ds = Convert.ToDouble(drives[c - 1]);
                    }
                    de = Convert.ToDouble(drives[c]);
                    diffd = de - ds;
                    //
                    if (c == 0)
                    {
                        ss = "0";
                        se = drives[c].ToString();
                    }
                    else
                    {
                        ss = drives[c - 1].ToString();
                        se = drives[c].ToString();
                    }
                    q = Convert.ToDouble(chist[c]);
                    q10 = q * diffd / 1000;
                    q2 = (q * diffd * 0.93132257461) / 1024;
                    addline(ss + "-" + se + " Chunks:" + q.ToString() + " ---> Chunk Space base-two: " +
                            String.Format("{0:0.000}", q10) + " TB --- base-ten: " + String.Format("{0:0.000}", q2) +
                            " TB");
                }
            }

            //addline("Space Allotted in Base 10 - Raid5: " + String.Format("{0:0}", toraid5) + " GB, Raid6: " + String.Format("{0:0}", toraid6) + " GB");
            addline("Space Allotted in Base 10 - Raid5: " + String.Format("{0:0.000}", spaceR5Tb) + " TB, Raid6: " + String.Format("{0:0.000}", spaceR6Tb) + " TB");
            //addline("Space Allotted in Base 2 - Raid5: " + String.Format("{0:0}", spaceR5_2) + " GB, Raid6: " + String.Format("{0:0}", spaceR6_2) + " GB");
            addline("Space Allotted in Base 2 - Raid5: " + String.Format("{0:0.000}", spaceR52Tb) + " TB, Raid6: " + String.Format("{0:0.000}", spaceR62Tb) + " TB");
            addline_italic("NOTE: Every size value below is in base 2, 'real space'");

            if (MoreInfo)
            {

                addword_bold("OS & SWAP "); addword(" will take up ");
                addword_bred(String.Format("{0:0.000}", osperdisk) + " GB/Disk");
                addword(" on ");
                addword_bred(used + " Disks");
                addword_bold(", Totaling: ");
                addword_bred(String.Format("{0:0.000}", totalosandswap));
                addnl();
                addword_bold("OS & SWAP "); addword(" will take up ");
                addword_bblue(String.Format("{0:0.000}", osper0) + " GB/Disk");
                addword(" on ");
                addword_bblue(drives0 + " Disks");
                addword_bold(", Totaling: ");
                addword_bblue(String.Format("{0:0.000}", ostotal0));
                addnl();
                addword_bold("Raid Overhead ");
                addword("at ");
                addword_bold(String.Format("{0:0.0}", raid_overhead * 100) + "%");
                addword(" will cost ");
                addword_bold("- Raid5: ");
                addword_bred(String.Format("{0:0}", ro5) + " GB");
                addword_bold(", Raid6: ");
                addword_bred(String.Format("{0:0}", ro6) + " GB");
                addnl();
                addword_bold("Raid Overhead ");
                addword("at ");
                addword_bold(String.Format("{0:0.0}", raid_overhead * 100) + "%");
                addword(" will cost ");
                addword_bold("- Raid5: ");
                addword_bblue(String.Format("{0:0}", ro10) + " GB");
                addword_bold(", Raid6: ");
                addword_bblue(String.Format("{0:0}", ro20) + " GB");
                addnl();
                addline_underline("After");
                addword_bold(" OS & SWAP");
                addword(" - ");
                addword_bold("Raid5: ");
                addword_bred(String.Format("{0:0.000}", afteros5) + " GB");
                addword_bold(", Raid6: ");
                addword_bred(String.Format("{0:0.000}", afteros6) + " GB");
                addnl();
                addline_underline("After");
                addword_bold(" OS & SWAP");
                addword(" - ");
                addword_bold("Raid5: ");
                addword_bblue(String.Format("{0:0.000}", afteros50) + " GB");
                addword_bold(", Raid6: ");
                addword_bblue(String.Format("{0:0.000}", afteros60) + " GB");
                addnl();

                addline_underline("After ");
                addword_bold("Raid Overhead ");
                addword(" - ");
                addword_bold("Raid5: ");
                addword_bred(String.Format("{0:0.000}", afterOverhead5) + " GB");
                addword_bold(", Raid6: ");
                addword_bred(String.Format("{0:0.000}", afterOverhead6) + " GB");
                addnl();
                addline_underline("After ");
                addword_bold("Raid Overhead ");
                addword(" - ");
                addword_bold("Raid5: ");
                addword_bblue(String.Format("{0:0.000}", afterro50) + " GB");
                addword_bold(", Raid6: ");
                addword_bblue(String.Format("{0:0.000}", afterro60) + " GB");
                addnl();
                addline_underline("After ");
                addword_bold("Snapshot ");
                addword(" - ");
                addword_bold("Raid5: ");
                addword_bred(String.Format("{0:0.000}", final5Gb) + " GB");
                addword_bold(", Raid6: ");
                addword_bred(String.Format("{0:0.000}", final6Gb) + " GB");
                addword_bold(" = FINAL VALUES");
                addnl();
                addline_underline("After ");
                addword_bold("Snapshot ");
                addword(" - ");
                addword_bold("Raid5: ");
                addword_bblue(String.Format("{0:0.000}", final5gb0) + " GB");
                addword_bold(", Raid6: ");
                addword_bblue(String.Format("{0:0.000}", final6gb0) + " GB");
                addword_bold(" = FINAL VALUES");
                addnl();
            }
            else
            {
                // addline(string.Format("Other factors - Os+Swap: {0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap,ro5,ro6,snapshot));
                addword(string.Format("Other factors - ")); //"Os+Swap: {0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap, ro5, ro6, snapshot));
                addword_bold(string.Format("Os&Swap: ")); //"{0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap, ro5, ro6, snapshot));
                addword_bred(string.Format("{0:0.000}", totalosandswap));
                addword(string.Format(" - "));
                addword_bold(string.Format("R5/R6 Overhead: "));
                addword_bred(string.Format("{0:0.0}", ro5));
                addword(string.Format("/"));
                addword_bred(string.Format("{0:0.0}", ro6));
                addword(string.Format(" - "));
                addword_bold(string.Format("Snapshot: "));
                addword_bred(string.Format("{0:0}", snapshot));
                addnl();
                if (comparison_enabled)
                {
                    addword(string.Format("Other factors - ")); //"Os+Swap: {0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap, ro5, ro6, snapshot));
                    addword_bold(string.Format("Os&Swap: ")); //"{0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap, ro5, ro6, snapshot));
                    addword_bblue(string.Format("{0:0.000}", ostotal0));
                    addword(string.Format(" - "));
                    addword_bold(string.Format("R5/R6 Overhead: "));
                    addword_bblue(string.Format("{0:0.0}", ro10));
                    addword(string.Format("/"));
                    addword_bblue(string.Format("{0:0.0}", ro20));
                    addword(string.Format(" - "));
                    addword_bold(string.Format("Snapshot: "));
                    addword_bblue(string.Format("{0:0}", snapshot0));
                    addnl();
                    addword(string.Format("Difference ----- ")); //"Os+Swap: {0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap, ro5, ro6, snapshot));
                    addword_bold(string.Format("Os&Swap: ")); //"{0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap, ro5, ro6, snapshot));
                    addword_blue(string.Format("{0:0.000}", ostotal0d));
                    addword(string.Format(" -- "));
                    addword_bold(string.Format("R5/R6 Overhead: "));
                    addword_blue(string.Format("{0:0.0}", ro10d));
                    addword(string.Format("/"));
                    addword_blue(string.Format("{0:0.0}", ro20d));
                    addword(string.Format(" -- "));
                    addword_bold(string.Format("Snapshot: "));
                    addword_blue(string.Format("{0:0}", snapshot0d));
                    addnl();
                }
                //addword(string.Format("Other factors - Os+Swap: {0:0.000} - R5/R6 Overhead: {1:0.0}/{2:0.0} - Snapshot: {3:0.0}", totalosandswap, ro5, ro6, snapshot));
            }
            addline_italic("RAID5: " + pr5 + " - RAID6: " + pr6);
            double percentfinal5diff;
            double percentfinal6diff;
            double percent1diskdiff;
            if (Convert.ToDouble(final5tb0) == Convert.ToDouble(0))
            {
                percentfinal5diff = 0;
            }
            else
            {
                percentfinal5diff = final5tb0d / final5tb0 * 100;
            }

            if (Convert.ToDouble(final6tb0) == Convert.ToDouble(0))
            {
                percentfinal6diff = 0;
            }
            else
            {
                percentfinal6diff = final6tb0d / final6tb0 * 100;

            }
            if (Convert.ToDouble(onedisktb0) == Convert.ToDouble(0))
            {
                percent1diskdiff = 0;
            }
            else
            {
                percent1diskdiff = onedisktb0d / onedisktb0 * 100;
            }


            if (used != 1)
            {
                addline_boldBIG("####### FINAL RESULTS #######");
                addword_boldBIG("Raid5:");
                addword(" " + String.Format("{0:0.000}", final5Gb) + " GB, ");
                addword_bredBIG(String.Format("{0:0.000}", final5Tb) + " TB");
                addword("  ");
                addword_bblue(String.Format("{0:0.000}", final5tb0) + " TB");
                addword("  Diff: ");
                addword_blue(String.Format("{0:0.000}", final5tb0d) + " TB");
                addword(" ");
                addword_blue("(" + String.Format("{0:0.00}", percentfinal5diff) + "%)");
                addnl();
                addword_boldBIG("Raid6:");
                addword(" " + String.Format("{0:0.000}", final6Gb) + " GB, ");
                addword_bredBIG(String.Format("{0:0.000}", final6Tb) + " TB");
                addword("  ");
                addword_bblue(String.Format("{0:0.000}", final6tb0) + " TB");
                addword("  Diff: ");
                addword_blue(String.Format("{0:0.000}", final6tb0d) + " TB");
                addword(" ");
                addword_blue("(" + String.Format("{0:0.00}", percentfinal6diff) + "%)");
                addnl();
                // from 1 disk to raid 5 and 6


            }
            else
            {
                addline_boldBIG("####### FINAL RESULTS WITH 1 DISK #######");
                addword_boldBIG("1 DISK FINAL:");
                addword(" " + String.Format("{0:0.000}", onediskgb) + " GB, ");
                addword_bredBIG(String.Format("{0:0.000}", onedisktb) + " TB");
                addword("  ");
                addword_bblue(String.Format("{0:0.000}", onedisktb0) + " TB");
                addword("  Diff from 1 disk: ");
                addword_blue(String.Format("{0:0.00}", onedisktb0d) + " TB");
                addword(" ");
                addword_blue("(" + String.Format("{0:0.00}", percent1diskdiff) + "%)");
                addnl();


                if (showanotherline)   // from raid5 and 6 to 1 disk
                {
                    five_to_one = onedisktb - final5tb0;
                    six_to_one = onedisktb - final6tb0;
                    five_to_one_percent = (five_to_one / onedisktb) * 100;
                    six_to_one_percent = (six_to_one / onedisktb) * 100;

                    addword_boldBIG("Old Values - RAID5: ");
                    addword_bblue(String.Format("{0:0.000}", final5tb0) + " TB "); // old raid 5
                    addword("Diffs from R5:");
                    addword_blue(" " + String.Format("{0:0.00}", five_to_one) + " TB (");
                    addword_blue(String.Format("{0:0.00}", five_to_one_percent));
                    addword_blue("%)");
                    addnl();

                    addword_boldBIG("Old Values - RAID6: ");
                    addword_bblue(String.Format("{0:0.000}", final6tb0) + " TB ");
                    addword("Diffs from R6:");
                    addword_blue(" " + String.Format("{0:0.000}", six_to_one) + " TB (");
                    addword_blue(String.Format("{0:0.000}", six_to_one_percent));
                    addword_blue("%)");
                    addnl();
                }



            }
            addword("===LEGEND below:===");
            addnl();
            addword_bredBIG("BOLD RED WITH YELLOW HIGHLIGHT: Current FINAL Value in TBs"); addnl();
            addword_bred("BOLD RED: Current Value"); addnl();
            addword_bblue("BOLD BLUE: Old Value (Set with Baseline)"); addnl();
            addword_blue("LIGHT BLUE: Difference from baseline old value and new"); addnl();
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            string itm;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    ((RichTextBox)item).Select(((RichTextBox)item).TextLength, 0);
                    ((RichTextBox)item).ScrollToCaret();
                }
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
                    ((RichTextBox)item).AppendText(str + Environment.NewLine);
                }
            }
        }
        private void addword(string str)
        {
            string itm;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    ((RichTextBox)item).AppendText(str);
                }
            }
        }
        private void addnl()
        {
            string itm;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    ((RichTextBox)item).AppendText(Environment.NewLine);
                }
            }
        }


        private void addline_red(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str + Environment.NewLine);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                   // cwl(before + " AFTER: " + after + " LENGTH:" + lengthh);
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionColor = Color.Red;
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionColor = Color.Black;
                }
            }
        }
        private void addline_blue(string str)
        {
            string itm;
            int before, after, lengthh;
            if (comparison_enabled)
            {
                foreach (var item in this.Controls)
                {
                    itm = ((Control)item).Name;
                    if (itm == "lbr")
                    {
                        before = ((RichTextBox)item).Text.Length;
                        ((RichTextBox)item).AppendText(str + Environment.NewLine);
                        after = ((RichTextBox)item).Text.Length;
                        lengthh = after - before;
                       // cwl(before + " AFTER: " + after + " LENGTH:" + lengthh);
                        ((RichTextBox)item).Select(before, lengthh);
                        ((RichTextBox)item).SelectionColor = Color.Blue;
                        ((RichTextBox)item).Select(after, 0);
                        ((RichTextBox)item).SelectionColor = Color.Black;
                    }
                }
            }
        }
        private void addword_red(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionColor = Color.Red;
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionColor = Color.Black;
                }
            }
        }
        private void addword_blue(string str)
        {
            string itm;
            int before, after, lengthh;
            if (comparison_enabled)
            {
                foreach (var item in this.Controls)
                {
                    itm = ((Control)item).Name;
                    if (itm == "lbr")
                    {
                        before = ((RichTextBox)item).Text.Length;
                        ((RichTextBox)item).AppendText(str);
                        after = ((RichTextBox)item).Text.Length;
                        lengthh = after - before;
                        ((RichTextBox)item).Select(before, lengthh);
                        ((RichTextBox)item).SelectionColor = Color.Blue;
                        ((RichTextBox)item).Select(after, 0);
                        ((RichTextBox)item).SelectionColor = Color.Black;
                    }
                }
            }
        }
        private void addword_bred(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Bold);
                    ((RichTextBox)item).SelectionColor = Color.Red;
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionColor = Color.Black;
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                }
            }
        }

        //addword_bredBIG
        private void addword_bredBIG(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Bold);
                    ((RichTextBox)item).SelectionBackColor = Color.Yellow;
                    ((RichTextBox)item).SelectionColor = Color.Red;
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionColor = Color.Black;
                    ((RichTextBox)item).SelectionBackColor = Color.White;

                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                }
            }
        }
        private void addword_boldBIG(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Bold);
                    ((RichTextBox)item).SelectionBackColor = Color.Yellow;

                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionBackColor = Color.White;
                    ((RichTextBox)item).SelectionColor = Color.Black;


                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                }
            }
        }
        private void addword_bblue(string str)
        {
            string itm;
            int before, after, lengthh;
            if (comparison_enabled)
            {
                foreach (var item in this.Controls)
                {
                    itm = ((Control)item).Name;
                    if (itm == "lbr")
                    {
                        before = ((RichTextBox)item).Text.Length;
                        ((RichTextBox)item).AppendText(str);
                        after = ((RichTextBox)item).Text.Length;
                        lengthh = after - before;
                        ((RichTextBox)item).Select(before, lengthh);
                        ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Bold);
                        ((RichTextBox)item).SelectionColor = Color.Blue;
                        ((RichTextBox)item).Select(after, 0);
                        ((RichTextBox)item).SelectionColor = Color.Black;
                        ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                    }
                }
            }
        }
        private void addword_bold(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Bold);
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                }
            }
        }
        private void addline_bold(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str + Environment.NewLine);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Bold);
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                }
            }
        }
        private void addline_boldBIG(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str + Environment.NewLine);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Bold);
                    ((RichTextBox)item).SelectionBackColor = Color.Yellow; //= new Font(((RichTextBox)item).Font, FontStyle.Bold);
                    //((RichTextBox).item).
                    //((RichTextBox)item).Selectio
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionBackColor = Color.White;
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                }
            }
        }
        private void addline_tiny(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str + Environment.NewLine);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font.FontFamily, 8, FontStyle.Bold);
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font.FontFamily, fsizereg, FontStyle.Regular);
                }
            }
        }
        private void addline_italic(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str + Environment.NewLine);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Italic);
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
                }
            }
        }


        private void addline_underline(string str)
        {
            string itm;
            int before, after, lengthh;
            foreach (var item in this.Controls)
            {
                itm = ((Control)item).Name;
                if (itm == "lbr")
                {
                    before = ((RichTextBox)item).Text.Length;
                    ((RichTextBox)item).AppendText(str);
                    after = ((RichTextBox)item).Text.Length;
                    lengthh = after - before;
                    ((RichTextBox)item).Select(before, lengthh);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Underline);
                    ((RichTextBox)item).Select(after, 0);
                    ((RichTextBox)item).SelectionFont = new Font(((RichTextBox)item).Font, FontStyle.Regular);
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
                    ((RichTextBox)item).Rtf = "";
                }
            }
        }
        private void Form1_Click(object sender, EventArgs e)
        {
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
            MessageBox.Show(Resources.HelpMessage);
        }
        private void cms_Opening(object sender, CancelEventArgs e)
        {
        }
        private void cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string optionselected = e.ClickedItem.Text;
            if (optionselected == "Add Drive Size")
            {
                add_drive();
            }
            else if (optionselected == "Remove Drive Size")
            {
                del_drive();
            }
            else if (optionselected == "DEFAULT")
            {
                def_drive();
            }
            else if (optionselected == "Clear Selection") { clear_all_selects(); }
            else if (optionselected == "Select All") { select_all(); }

        }

        private void clear_all_selects()
        {
            string itm;
  
        
            foreach (var item in this.Controls)
            {
               
                 itm = ((Control)item).Name;
           
                  if (((itm.Contains("ata") & (itm.Contains("lata")==false)) & (itm.Contains("a0") == false)))
                  {
                      ((CheckedListBox)item).ClearSelected();
                      for (int i = 0; i < ((CheckedListBox)item).Items.Count; i++)
                      {
                          CheckState is_item_checked = ((CheckedListBox)item).GetItemCheckState(i) ;


                          if ( is_item_checked == CheckState.Checked | is_item_checked == CheckState.Indeterminate) 
                          {
                              ((CheckedListBox)item).SetItemCheckState(i, CheckState.Unchecked);
                          }
                    
                      }
                     
                  }                  
             

            }
          
            calc(true);
        }

        private void select_all()
        {
            string itm = "";
            Form2 f = new Form2();
            f.Text = Resources.WhatToSelectTitle;
            f.lb.Text = Resources.WhatToSelect;
            f.Width = f.PreferredSize.Width + 10;
            f.Height = f.PreferredSize.Height + 10;
            f.ShowDialog();
            string mess = f.mess;
            // convert to useable number
            if (mess == "cancel") { return; }
            double x = -1;
            try
            {
                x = Convert.ToDouble(mess);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.InvalidDriveSize);
                return;
            }


            //MessageBox.Show(x + " GB");
            string entry = x + " GB";
            entry = entry.Trim();
            int where = -2;

            foreach (var item in this.Controls)
            {

                itm = ((Control)item).Name;

              




                if (((itm.Contains("ata") & (itm.Contains("lata") == false)) & (itm.Contains("a0") == false)))
                {
                    //before unchecking anything make sure looking for a valid number
                    where = ((CheckedListBox)item).Items.IndexOf(entry);
                    if (where == -1)
                    {
                        return;
                    }

                    // first unselect everything
                    ((CheckedListBox)item).ClearSelected();
                    //uncheck everything
                    for (int i = 0; i < ((CheckedListBox)item).Items.Count; i++)
                    {
                        CheckState is_item_checked = ((CheckedListBox)item).GetItemCheckState(i);


                        if (is_item_checked == CheckState.Checked | is_item_checked == CheckState.Indeterminate)
                        {
                            ((CheckedListBox)item).SetItemCheckState(i, CheckState.Unchecked);
                        }

                    }
                    //find the entry and check it

                    where = ((CheckedListBox)item).Items.IndexOf(entry);

                    if (where != -1)
                    {
                         ((CheckedListBox)item).SetItemCheckState(where, CheckState.Checked);
                         ((CheckedListBox)item).SetSelected(where, true);
                    }

                    //for (int i = 0; i < ((CheckedListBox)item).Items.Count; i++)
                    //{

                    //    //where=

                    //    //cwl(((CheckedListBox)item).Items.IndexOf(entry) + " === " + entry);

                    //    //if (((CheckedListBox)item).GetItemText(i).Equals(entry))
                    //    //{

                    //    //    ((CheckedListBox)item).SetItemCheckState(i, CheckState.Checked);
                    //    //    ((CheckedListBox)item).SetSelected(i, true);
                            
                    //    //}


                    //}



                }


            }

            calc(true);
        }
        private void add_drive()
        {
            if (drives.Count >= 60)
            {
                MessageBox.Show(Resources.CantAddMore);
                return;
            }
            Form2 f = new Form2();
            f.Text = Resources.AddDriveSize;
            f.lb.Text = Resources.WhatToAdd;
            f.Width = f.PreferredSize.Width + 10;
            f.Height = f.PreferredSize.Height + 10;
            f.ShowDialog();
            string mess = f.mess;
            // convert to useable number
            if (mess == "cancel") { return; }
            double x = -1;
            try
            {
                x = Convert.ToDouble(mess);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.InvalidDriveSize);
                return;
            }
            // check if exists
            double d = x;
            bool there;
            there = false;
            for (int i = 0; i <= drives.Count - 1; i++)
            {
                if (d.Equals(drives[i])) { there = true; }
            }
            if (there)
            { return; }
            else
            {
                drives.Add(x); drives.Sort();
            }
            premakeall();
        }
        private void del_drive()
        {
            if (drives.Count == 1)
            {
                MessageBox.Show(Resources.NeedAtLeast1Drive);
                return;
            }
            Form2 f = new Form2();
            f.Text = Resources.DeleteDriveSize;
            f.lb.Text = Resources.WhatToDelete;
            f.Width = f.PreferredSize.Width + 10;
            f.Height = f.PreferredSize.Height + 10;
            f.ShowDialog();
            string mess = f.mess;
            // convert to useable number
            if (mess == "cancel") { return; }
            double x = -1;
            try
            {
                x = Convert.ToDouble(mess);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.InvalidDriveSize);
                return;
            }
            double d = x;
            bool there;
            int where = -1;
            there = false;
            for (int i = 0; i <= drives.Count - 1; i++)
            {
                if (d.Equals(drives[i])) { there = true; where = i; }
            }
            // delete to list
            if (there)
            { drives.RemoveAt(where); drives.Sort(); }
            else
            {
                return;
            }
            premakeall();

        }
        private void def_drive()
        {
            if (MessageBox.Show(Resources.DefautMessage, "Default", MessageBoxButtons.YesNo).ToString().ToLower() == "yes")
            {
                init_drives();
                premakeall();
            }
            else
            {
                return;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // clear_baseline();

            comparision1st = true;
            torun = true;
            clear_diffs();
            original_is_one_disk = false;
            calc(true);

            //premakeall();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MoreInfo = checkBox1.Checked;
            calc(true);
        }

        private void ata0_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}


