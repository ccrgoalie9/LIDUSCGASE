namespace LID_WinForm {
    partial class LID_Form {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LID_Form));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FilesButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.EarthButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DecimalButton = new System.Windows.Forms.Button();
            this.DegreeButton = new System.Windows.Forms.Button();
            this.BulletinButton = new System.Windows.Forms.Button();
            this.ChartButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ResBulletinButton = new System.Windows.Forms.Button();
            this.DoItButton = new System.Windows.Forms.Button();
            this.BulletinHistoryButton = new System.Windows.Forms.Button();
            this.BulletinChooser = new System.Windows.Forms.OpenFileDialog();
            this.BulletinHistButton = new System.Windows.Forms.Button();
            this.DegreeHistButton = new System.Windows.Forms.Button();
            this.DecimalHistButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.CoordHistoryButton = new System.Windows.Forms.Button();
            this.CoordChooser = new System.Windows.Forms.OpenFileDialog();
            this.ErrorTimer = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.PolarButton = new System.Windows.Forms.Button();
            this.PolarHistButton = new System.Windows.Forms.Button();
            this.AboutButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TestSiteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(335, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "LID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(332, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Line Iceberg Display";
            // 
            // FilesButton
            // 
            this.FilesButton.Location = new System.Drawing.Point(404, 188);
            this.FilesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilesButton.Name = "FilesButton";
            this.FilesButton.Size = new System.Drawing.Size(133, 50);
            this.FilesButton.TabIndex = 3;
            this.FilesButton.Text = "All Files";
            this.FilesButton.UseVisualStyleBackColor = true;
            this.FilesButton.Click += new System.EventHandler(this.FilesButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(351, 354);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(100, 28);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // EarthButton
            // 
            this.EarthButton.Location = new System.Drawing.Point(264, 188);
            this.EarthButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EarthButton.Name = "EarthButton";
            this.EarthButton.Size = new System.Drawing.Size(133, 50);
            this.EarthButton.TabIndex = 2;
            this.EarthButton.Text = "Open Google Earth";
            this.EarthButton.UseVisualStyleBackColor = true;
            this.EarthButton.Click += new System.EventHandler(this.EarthButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(355, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Today\'s Files";
            // 
            // DecimalButton
            // 
            this.DecimalButton.Location = new System.Drawing.Point(404, 266);
            this.DecimalButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DecimalButton.Name = "DecimalButton";
            this.DecimalButton.Size = new System.Drawing.Size(75, 30);
            this.DecimalButton.TabIndex = 7;
            this.DecimalButton.Text = "Decimal";
            this.DecimalButton.UseVisualStyleBackColor = true;
            this.DecimalButton.Click += new System.EventHandler(this.DecimalButton_Click);
            // 
            // DegreeButton
            // 
            this.DegreeButton.Location = new System.Drawing.Point(323, 266);
            this.DegreeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DegreeButton.Name = "DegreeButton";
            this.DegreeButton.Size = new System.Drawing.Size(75, 30);
            this.DegreeButton.TabIndex = 6;
            this.DegreeButton.Text = "Degree";
            this.DegreeButton.UseVisualStyleBackColor = true;
            this.DegreeButton.Click += new System.EventHandler(this.DegreeButton_Click);
            // 
            // BulletinButton
            // 
            this.BulletinButton.Location = new System.Drawing.Point(323, 302);
            this.BulletinButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BulletinButton.Name = "BulletinButton";
            this.BulletinButton.Size = new System.Drawing.Size(75, 30);
            this.BulletinButton.TabIndex = 8;
            this.BulletinButton.Text = "Bulletin";
            this.BulletinButton.UseVisualStyleBackColor = true;
            this.BulletinButton.Click += new System.EventHandler(this.BulletinButton_Click);
            // 
            // ChartButton
            // 
            this.ChartButton.Location = new System.Drawing.Point(92, 266);
            this.ChartButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChartButton.Name = "ChartButton";
            this.ChartButton.Size = new System.Drawing.Size(75, 30);
            this.ChartButton.TabIndex = 4;
            this.ChartButton.Text = "Chart";
            this.ChartButton.UseVisualStyleBackColor = true;
            this.ChartButton.Click += new System.EventHandler(this.ChartButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Today\'s Resources";
            // 
            // ResBulletinButton
            // 
            this.ResBulletinButton.Location = new System.Drawing.Point(173, 266);
            this.ResBulletinButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResBulletinButton.Name = "ResBulletinButton";
            this.ResBulletinButton.Size = new System.Drawing.Size(75, 30);
            this.ResBulletinButton.TabIndex = 5;
            this.ResBulletinButton.Text = "Bulletin";
            this.ResBulletinButton.UseVisualStyleBackColor = true;
            this.ResBulletinButton.Click += new System.EventHandler(this.ResBulletinButton_Click);
            // 
            // DoItButton
            // 
            this.DoItButton.Location = new System.Drawing.Point(360, 130);
            this.DoItButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoItButton.Name = "DoItButton";
            this.DoItButton.Size = new System.Drawing.Size(81, 50);
            this.DoItButton.TabIndex = 11;
            this.DoItButton.Text = "Fetch Data";
            this.DoItButton.UseVisualStyleBackColor = true;
            this.DoItButton.Click += new System.EventHandler(this.DoItButton_Click);
            // 
            // BulletinHistoryButton
            // 
            this.BulletinHistoryButton.Location = new System.Drawing.Point(264, 130);
            this.BulletinHistoryButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BulletinHistoryButton.Name = "BulletinHistoryButton";
            this.BulletinHistoryButton.Size = new System.Drawing.Size(81, 50);
            this.BulletinHistoryButton.TabIndex = 12;
            this.BulletinHistoryButton.Text = "Fetch Bulletin";
            this.BulletinHistoryButton.UseVisualStyleBackColor = true;
            this.BulletinHistoryButton.Click += new System.EventHandler(this.BulletinHistoryButton_Click);
            // 
            // BulletinChooser
            // 
            this.BulletinChooser.DefaultExt = "txt";
            this.BulletinChooser.FileName = "BulletinChooser";
            // 
            // BulletinHistButton
            // 
            this.BulletinHistButton.Location = new System.Drawing.Point(553, 302);
            this.BulletinHistButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BulletinHistButton.Name = "BulletinHistButton";
            this.BulletinHistButton.Size = new System.Drawing.Size(75, 30);
            this.BulletinHistButton.TabIndex = 16;
            this.BulletinHistButton.Text = "Bulletin";
            this.BulletinHistButton.UseVisualStyleBackColor = true;
            this.BulletinHistButton.Click += new System.EventHandler(this.BulletinHistButton_Click);
            // 
            // DegreeHistButton
            // 
            this.DegreeHistButton.Location = new System.Drawing.Point(553, 266);
            this.DegreeHistButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DegreeHistButton.Name = "DegreeHistButton";
            this.DegreeHistButton.Size = new System.Drawing.Size(75, 30);
            this.DegreeHistButton.TabIndex = 14;
            this.DegreeHistButton.Text = "Degree";
            this.DegreeHistButton.UseVisualStyleBackColor = true;
            this.DegreeHistButton.Click += new System.EventHandler(this.DegreeHistButton_Click);
            // 
            // DecimalHistButton
            // 
            this.DecimalHistButton.Location = new System.Drawing.Point(633, 266);
            this.DecimalHistButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DecimalHistButton.Name = "DecimalHistButton";
            this.DecimalHistButton.Size = new System.Drawing.Size(75, 30);
            this.DecimalHistButton.TabIndex = 15;
            this.DecimalHistButton.Text = "Decimal";
            this.DecimalHistButton.UseVisualStyleBackColor = true;
            this.DecimalHistButton.Click += new System.EventHandler(this.DecimalHistButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(568, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Last Historic Files";
            // 
            // CoordHistoryButton
            // 
            this.CoordHistoryButton.Location = new System.Drawing.Point(456, 130);
            this.CoordHistoryButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CoordHistoryButton.Name = "CoordHistoryButton";
            this.CoordHistoryButton.Size = new System.Drawing.Size(81, 50);
            this.CoordHistoryButton.TabIndex = 17;
            this.CoordHistoryButton.Text = "Fetch Coord";
            this.CoordHistoryButton.UseVisualStyleBackColor = true;
            this.CoordHistoryButton.Click += new System.EventHandler(this.CoordHistoryButton_Click);
            // 
            // CoordChooser
            // 
            this.CoordChooser.DefaultExt = "txt";
            this.CoordChooser.FileName = "CoordChooser";
            // 
            // ErrorTimer
            // 
            this.ErrorTimer.Interval = 600000;
            this.ErrorTimer.Tick += new System.EventHandler(this.ErrorTimer_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(235, 386);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(330, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "FOR RESEARCH AND EDUCATIONAL PURPOSES";
            // 
            // ConfigButton
            // 
            this.ConfigButton.Location = new System.Drawing.Point(459, 354);
            this.ConfigButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(63, 28);
            this.ConfigButton.TabIndex = 19;
            this.ConfigButton.Text = "Config";
            this.ConfigButton.UseVisualStyleBackColor = true;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(53, 34);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 204);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::LID_WinForm.Properties.Resources.Iceberg;
            this.pictureBox2.Location = new System.Drawing.Point(553, 34);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(195, 204);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // PolarButton
            // 
            this.PolarButton.Location = new System.Drawing.Point(404, 302);
            this.PolarButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PolarButton.Name = "PolarButton";
            this.PolarButton.Size = new System.Drawing.Size(75, 30);
            this.PolarButton.TabIndex = 22;
            this.PolarButton.Text = "Polar";
            this.PolarButton.UseVisualStyleBackColor = true;
            this.PolarButton.Click += new System.EventHandler(this.PolarButton_Click);
            // 
            // PolarHistButton
            // 
            this.PolarHistButton.Location = new System.Drawing.Point(633, 302);
            this.PolarHistButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PolarHistButton.Name = "PolarHistButton";
            this.PolarHistButton.Size = new System.Drawing.Size(75, 30);
            this.PolarHistButton.TabIndex = 23;
            this.PolarHistButton.Text = "Polar";
            this.PolarHistButton.UseVisualStyleBackColor = true;
            this.PolarHistButton.Click += new System.EventHandler(this.PolarHistButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(281, 354);
            this.AboutButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(63, 28);
            this.AboutButton.TabIndex = 24;
            this.AboutButton.Text = "About";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(353, 130);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 17);
            this.label7.TabIndex = 25;
            this.label7.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(256, 188);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "2";
            // 
            // TestSiteButton
            // 
            this.TestSiteButton.Location = new System.Drawing.Point(92, 302);
            this.TestSiteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TestSiteButton.Name = "TestSiteButton";
            this.TestSiteButton.Size = new System.Drawing.Size(156, 30);
            this.TestSiteButton.TabIndex = 27;
            this.TestSiteButton.Text = "Test Website";
            this.TestSiteButton.UseVisualStyleBackColor = true;
            this.TestSiteButton.Click += new System.EventHandler(this.TestSiteButton_Click);
            // 
            // LID_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 416);
            this.Controls.Add(this.TestSiteButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.PolarHistButton);
            this.Controls.Add(this.PolarButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CoordHistoryButton);
            this.Controls.Add(this.BulletinHistButton);
            this.Controls.Add(this.DegreeHistButton);
            this.Controls.Add(this.DecimalHistButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BulletinHistoryButton);
            this.Controls.Add(this.DoItButton);
            this.Controls.Add(this.ResBulletinButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ChartButton);
            this.Controls.Add(this.BulletinButton);
            this.Controls.Add(this.DegreeButton);
            this.Controls.Add(this.DecimalButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EarthButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.FilesButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LID_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LID_Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FilesButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button EarthButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DecimalButton;
        private System.Windows.Forms.Button DegreeButton;
        private System.Windows.Forms.Button BulletinButton;
        private System.Windows.Forms.Button ChartButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ResBulletinButton;
        private System.Windows.Forms.Button DoItButton;
        private System.Windows.Forms.Button BulletinHistoryButton;
        private System.Windows.Forms.OpenFileDialog BulletinChooser;
        private System.Windows.Forms.Button BulletinHistButton;
        private System.Windows.Forms.Button DegreeHistButton;
        private System.Windows.Forms.Button DecimalHistButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CoordHistoryButton;
        private System.Windows.Forms.OpenFileDialog CoordChooser;
        private System.Windows.Forms.Timer ErrorTimer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button PolarButton;
        private System.Windows.Forms.Button PolarHistButton;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button TestSiteButton;
    }
}