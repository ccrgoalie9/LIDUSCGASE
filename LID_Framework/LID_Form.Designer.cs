namespace LID_Framework {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FilesButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.earthButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DecimalButton = new System.Windows.Forms.Button();
            this.DegreeButton = new System.Windows.Forms.Button();
            this.BulletinButton = new System.Windows.Forms.Button();
            this.ChartButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ResBulletinButton = new System.Windows.Forms.Button();
            this.DoItButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(344, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "LID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Line Iceberg Display";
            // 
            // FilesButton
            // 
            this.FilesButton.Location = new System.Drawing.Point(459, 172);
            this.FilesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilesButton.Name = "FilesButton";
            this.FilesButton.Size = new System.Drawing.Size(133, 50);
            this.FilesButton.TabIndex = 3;
            this.FilesButton.Text = "Show All Files";
            this.FilesButton.UseVisualStyleBackColor = true;
            this.FilesButton.Click += new System.EventHandler(this.FilesButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(360, 383);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(100, 28);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // earthButton
            // 
            this.earthButton.Location = new System.Drawing.Point(229, 172);
            this.earthButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.earthButton.Name = "earthButton";
            this.earthButton.Size = new System.Drawing.Size(133, 50);
            this.earthButton.TabIndex = 2;
            this.earthButton.Text = "Open Google Earth";
            this.earthButton.UseVisualStyleBackColor = true;
            this.earthButton.Click += new System.EventHandler(this.EarthButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Today\'s Files";
            // 
            // DecimalButton
            // 
            this.DecimalButton.Location = new System.Drawing.Point(528, 272);
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
            this.DegreeButton.Location = new System.Drawing.Point(447, 272);
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
            this.BulletinButton.Location = new System.Drawing.Point(447, 306);
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
            this.ChartButton.Location = new System.Drawing.Point(215, 272);
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
            this.label4.Location = new System.Drawing.Point(227, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Today\'s Resources";
            // 
            // ResBulletinButton
            // 
            this.ResBulletinButton.Location = new System.Drawing.Point(296, 272);
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
            this.DoItButton.Location = new System.Drawing.Point(371, 172);
            this.DoItButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoItButton.Name = "DoItButton";
            this.DoItButton.Size = new System.Drawing.Size(81, 50);
            this.DoItButton.TabIndex = 11;
            this.DoItButton.Text = "DO IT";
            this.DoItButton.UseVisualStyleBackColor = true;
            this.DoItButton.Click += new System.EventHandler(this.DoItButton_Click);
            // 
            // LID_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DoItButton);
            this.Controls.Add(this.ResBulletinButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ChartButton);
            this.Controls.Add(this.BulletinButton);
            this.Controls.Add(this.DegreeButton);
            this.Controls.Add(this.DecimalButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.earthButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.FilesButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LID_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FilesButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button earthButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DecimalButton;
        private System.Windows.Forms.Button DegreeButton;
        private System.Windows.Forms.Button BulletinButton;
        private System.Windows.Forms.Button ChartButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ResBulletinButton;
        private System.Windows.Forms.Button DoItButton;
    }
}