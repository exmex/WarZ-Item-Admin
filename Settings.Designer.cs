namespace WarZLocal_Admin
{
    partial class Settings
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Quick Config");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Fonts & Colors");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Threading");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Advanced", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.colorConfig = new System.Windows.Forms.Panel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.quickConfig = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.loadingCircle1 = new WarZLocal_Admin.LoadingCircle(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.threading = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.colorConfig.SuspendLayout();
            this.quickConfig.SuspendLayout();
            this.threading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.quickConfig);
            this.splitContainer1.Panel2.Controls.Add(this.colorConfig);
            this.splitContainer1.Panel2.Controls.Add(this.threading);
            this.splitContainer1.Size = new System.Drawing.Size(662, 390);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode5.Name = "Knoten0";
            treeNode5.Text = "Quick Config";
            treeNode6.Name = "Knoten2";
            treeNode6.Text = "Fonts & Colors";
            treeNode7.Name = "Knoten1";
            treeNode7.Text = "Threading";
            treeNode8.Name = "Knoten1";
            treeNode8.Text = "Advanced";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode8});
            this.treeView1.Size = new System.Drawing.Size(127, 390);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // colorConfig
            // 
            this.colorConfig.BackColor = System.Drawing.Color.White;
            this.colorConfig.Controls.Add(this.checkBox3);
            this.colorConfig.Controls.Add(this.label13);
            this.colorConfig.Controls.Add(this.label15);
            this.colorConfig.Controls.Add(this.label12);
            this.colorConfig.Controls.Add(this.label9);
            this.colorConfig.Controls.Add(this.label7);
            this.colorConfig.Controls.Add(this.label5);
            this.colorConfig.Controls.Add(this.label10);
            this.colorConfig.Controls.Add(this.label14);
            this.colorConfig.Controls.Add(this.label11);
            this.colorConfig.Controls.Add(this.button11);
            this.colorConfig.Controls.Add(this.label8);
            this.colorConfig.Controls.Add(this.button10);
            this.colorConfig.Controls.Add(this.button9);
            this.colorConfig.Controls.Add(this.button8);
            this.colorConfig.Controls.Add(this.label6);
            this.colorConfig.Controls.Add(this.button7);
            this.colorConfig.Controls.Add(this.label3);
            this.colorConfig.Controls.Add(this.button5);
            this.colorConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorConfig.Location = new System.Drawing.Point(0, 0);
            this.colorConfig.Name = "colorConfig";
            this.colorConfig.Size = new System.Drawing.Size(531, 390);
            this.colorConfig.TabIndex = 2;
            this.colorConfig.Visible = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox3.Location = new System.Drawing.Point(14, 12);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(206, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Show colors in \"Everything\" Category:";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(213, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 26);
            this.label13.TabIndex = 2;
            this.label13.Text = "Categories:\r\n   19 - Attachment";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(222, 257);
            this.label15.MaximumSize = new System.Drawing.Size(200, 400);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Current font:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(213, 155);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 39);
            this.label12.TabIndex = 2;
            this.label12.Text = "Categories:\r\n   30 - Food\r\n   33 - Water";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 130);
            this.label9.TabIndex = 2;
            this.label9.Text = "Categories:\r\n   20 - Assault Rifle\r\n   21 - Sniper Rifle\r\n   22 - Shotgun\r\n   23 " +
    "- Machine gun\r\n   25 - Handgun\r\n   26 - Submachinegun\r\n   27 - Grenade\r\n   28 - " +
    "Usable Item\r\n   29 - Melee";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 65);
            this.label7.TabIndex = 2;
            this.label7.Text = "Categories:\r\n   11 - Armor\r\n   12 - Backpack\r\n   13 - Helmet\r\n   16 - Hero";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 52);
            this.label5.TabIndex = 2;
            this.label5.Text = "Categories:\r\n   1 Account\r\n   2 Boost\r\n   7 LootBox";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Attachment Items Color:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(222, 234);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Itemlist Font:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(169, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Consumable Items Color:";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(294, 229);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(32, 23);
            this.button11.TabIndex = 0;
            this.button11.Text = "...";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Weapon Items Color:";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(298, 127);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(32, 23);
            this.button10.TabIndex = 0;
            this.button10.Text = "...";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.colorChange_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(295, 32);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(32, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "...";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.colorChange_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(128, 229);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(32, 23);
            this.button8.TabIndex = 0;
            this.button8.Text = "...";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.colorChange_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Wearable Items Color:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(128, 127);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(32, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "...";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.colorChange_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Internal Items Color:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(128, 32);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(32, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.colorChange_Click);
            // 
            // quickConfig
            // 
            this.quickConfig.Controls.Add(this.checkBox1);
            this.quickConfig.Controls.Add(this.label4);
            this.quickConfig.Controls.Add(this.loadingCircle1);
            this.quickConfig.Controls.Add(this.button6);
            this.quickConfig.Controls.Add(this.button4);
            this.quickConfig.Controls.Add(this.button3);
            this.quickConfig.Controls.Add(this.label2);
            this.quickConfig.Controls.Add(this.label1);
            this.quickConfig.Controls.Add(this.textBox2);
            this.quickConfig.Controls.Add(this.textBox1);
            this.quickConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quickConfig.Location = new System.Drawing.Point(0, 0);
            this.quickConfig.Name = "quickConfig";
            this.quickConfig.Size = new System.Drawing.Size(531, 390);
            this.quickConfig.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(19, 85);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(148, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Load Shop XML from File:";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.InnerCircleRadius = 10;
            this.loadingCircle1.LineWidth = 3;
            this.loadingCircle1.Location = new System.Drawing.Point(197, 140);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.OutnerCircleRadius = 30;
            this.loadingCircle1.Size = new System.Drawing.Size(64, 64);
            this.loadingCircle1.Speed = 100;
            this.loadingCircle1.SpokesMember = 12;
            this.loadingCircle1.TabIndex = 4;
            this.loadingCircle1.ThemeColor = System.Drawing.Color.DodgerBlue;
            this.loadingCircle1.Visible = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(23, 50);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 3;
            this.button6.Text = "Download";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(488, 106);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button3_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(488, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Shop XML File:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data Folder:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(134, 108);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(348, 20);
            this.textBox2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(348, 20);
            this.textBox1.TabIndex = 0;
            // 
            // threading
            // 
            this.threading.Controls.Add(this.checkBox2);
            this.threading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.threading.Location = new System.Drawing.Point(0, 0);
            this.threading.Name = "threading";
            this.threading.Size = new System.Drawing.Size(531, 390);
            this.threading.TabIndex = 1;
            this.threading.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Location = new System.Drawing.Point(19, 23);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(194, 17);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Use UI Thread for populating Items:";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Size = new System.Drawing.Size(662, 419);
            this.splitContainer2.SplitterDistance = 390;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(500, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(581, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(662, 419);
            this.Controls.Add(this.splitContainer2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.Resize += new System.EventHandler(this.Settings_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.colorConfig.ResumeLayout(false);
            this.colorConfig.PerformLayout();
            this.quickConfig.ResumeLayout(false);
            this.quickConfig.PerformLayout();
            this.threading.ResumeLayout(false);
            this.threading.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel quickConfig;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private LoadingCircle loadingCircle1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel threading;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Panel colorConfig;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}