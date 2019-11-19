namespace LID_WinForm {
    partial class About_Form {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About_Form));
            this.backButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ChangelogLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(263, 288);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Close";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(171, 314);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(258, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "FOR RESEARCH AND EDUCATIONAL PURPOSES";
            // 
            // ChangelogLabel
            // 
            this.ChangelogLabel.AutoSize = true;
            this.ChangelogLabel.Location = new System.Drawing.Point(12, 9);
            this.ChangelogLabel.Name = "ChangelogLabel";
            this.ChangelogLabel.Size = new System.Drawing.Size(117, 13);
            this.ChangelogLabel.TabIndex = 21;
            this.ChangelogLabel.Text = "Version and Changelog";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 78);
            this.label1.TabIndex = 22;
            this.label1.Text = "Developed By:\r\n2/c Luke Arsenault\r\n2/c Hayden Carter\r\n2/c Thomas Hardy\r\n2/c Chris" +
    "topher Rosselot\r\n2/c Maylis Yepez";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 130);
            this.label2.TabIndex = 23;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // About_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(600, 338);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChangelogLabel);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "About_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About LID";
            this.Load += new System.EventHandler(this.About_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ChangelogLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}