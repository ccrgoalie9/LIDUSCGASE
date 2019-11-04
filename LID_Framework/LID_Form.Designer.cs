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
            this.filesButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.earthButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DecimalButton = new System.Windows.Forms.Button();
            this.DegreeButton = new System.Windows.Forms.Button();
            this.BulletinButton = new System.Windows.Forms.Button();
            this.ChartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(258, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "LID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Line Iceberg Display";
            // 
            // filesButton
            // 
            this.filesButton.Location = new System.Drawing.Point(344, 140);
            this.filesButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.filesButton.Name = "filesButton";
            this.filesButton.Size = new System.Drawing.Size(100, 41);
            this.filesButton.TabIndex = 2;
            this.filesButton.Text = "Show All Files";
            this.filesButton.UseVisualStyleBackColor = true;
            this.filesButton.Click += new System.EventHandler(this.filesButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(270, 311);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // earthButton
            // 
            this.earthButton.Location = new System.Drawing.Point(172, 140);
            this.earthButton.Name = "earthButton";
            this.earthButton.Size = new System.Drawing.Size(100, 41);
            this.earthButton.TabIndex = 4;
            this.earthButton.Text = "Open Google Earth";
            this.earthButton.UseVisualStyleBackColor = true;
            this.earthButton.Click += new System.EventHandler(this.EarthButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 205);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Today\'s Files";
            // 
            // DecimalButton
            // 
            this.DecimalButton.Location = new System.Drawing.Point(396, 221);
            this.DecimalButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DecimalButton.Name = "DecimalButton";
            this.DecimalButton.Size = new System.Drawing.Size(56, 24);
            this.DecimalButton.TabIndex = 6;
            this.DecimalButton.Text = "Decimal";
            this.DecimalButton.UseVisualStyleBackColor = true;
            this.DecimalButton.Click += new System.EventHandler(this.DecimalButton_Click);
            // 
            // DegreeButton
            // 
            this.DegreeButton.Location = new System.Drawing.Point(335, 221);
            this.DegreeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DegreeButton.Name = "DegreeButton";
            this.DegreeButton.Size = new System.Drawing.Size(56, 24);
            this.DegreeButton.TabIndex = 7;
            this.DegreeButton.Text = "Degree";
            this.DegreeButton.UseVisualStyleBackColor = true;
            this.DegreeButton.Click += new System.EventHandler(this.DegreeButton_Click);
            // 
            // BulletinButton
            // 
            this.BulletinButton.Location = new System.Drawing.Point(335, 249);
            this.BulletinButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BulletinButton.Name = "BulletinButton";
            this.BulletinButton.Size = new System.Drawing.Size(56, 24);
            this.BulletinButton.TabIndex = 8;
            this.BulletinButton.Text = "Bulletin";
            this.BulletinButton.UseVisualStyleBackColor = true;
            this.BulletinButton.Click += new System.EventHandler(this.BulletinButton_Click);
            // 
            // ChartButton
            // 
            this.ChartButton.Location = new System.Drawing.Point(396, 249);
            this.ChartButton.Margin = new System.Windows.Forms.Padding(2);
            this.ChartButton.Name = "ChartButton";
            this.ChartButton.Size = new System.Drawing.Size(56, 24);
            this.ChartButton.TabIndex = 9;
            this.ChartButton.Text = "Chart";
            this.ChartButton.UseVisualStyleBackColor = true;
            this.ChartButton.Click += new System.EventHandler(this.ChartButton_Click);
            // 
            // LID_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.ChartButton);
            this.Controls.Add(this.BulletinButton);
            this.Controls.Add(this.DegreeButton);
            this.Controls.Add(this.DecimalButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.earthButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.filesButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LID_Form";
            this.Text = "LID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button filesButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button earthButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DecimalButton;
        private System.Windows.Forms.Button DegreeButton;
        private System.Windows.Forms.Button BulletinButton;
        private System.Windows.Forms.Button ChartButton;
    }
}