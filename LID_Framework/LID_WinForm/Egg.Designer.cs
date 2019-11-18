namespace LID_WinForm {
    partial class Egg {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Egg));
            this.GameSpace = new System.Windows.Forms.Label();
            this.UpButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.LeftButton = new System.Windows.Forms.Button();
            this.RightButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.MovesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameSpace
            // 
            this.GameSpace.AutoSize = true;
            this.GameSpace.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameSpace.Location = new System.Drawing.Point(319, 46);
            this.GameSpace.Name = "GameSpace";
            this.GameSpace.Size = new System.Drawing.Size(160, 170);
            this.GameSpace.TabIndex = 0;
            this.GameSpace.Text = resources.GetString("GameSpace.Text");
            this.GameSpace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UpButton
            // 
            this.UpButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.UpButton.Font = new System.Drawing.Font("Orbitron", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpButton.ForeColor = System.Drawing.SystemColors.Control;
            this.UpButton.Location = new System.Drawing.Point(375, 296);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(50, 50);
            this.UpButton.TabIndex = 1;
            this.UpButton.Text = "UP";
            this.UpButton.UseVisualStyleBackColor = false;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DownButton.Font = new System.Drawing.Font("Orbitron", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownButton.ForeColor = System.Drawing.SystemColors.Control;
            this.DownButton.Location = new System.Drawing.Point(375, 352);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(50, 50);
            this.DownButton.TabIndex = 2;
            this.DownButton.Text = "DN";
            this.DownButton.UseVisualStyleBackColor = false;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // LeftButton
            // 
            this.LeftButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LeftButton.Font = new System.Drawing.Font("Orbitron", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftButton.ForeColor = System.Drawing.SystemColors.Control;
            this.LeftButton.Location = new System.Drawing.Point(319, 352);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(50, 50);
            this.LeftButton.TabIndex = 3;
            this.LeftButton.Text = "LT";
            this.LeftButton.UseVisualStyleBackColor = false;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // RightButton
            // 
            this.RightButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RightButton.Font = new System.Drawing.Font("Orbitron", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightButton.ForeColor = System.Drawing.SystemColors.Control;
            this.RightButton.Location = new System.Drawing.Point(431, 352);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(50, 50);
            this.RightButton.TabIndex = 4;
            this.RightButton.Text = "RT";
            this.RightButton.UseVisualStyleBackColor = false;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "MOVE USING THE BUTTONS BELLOW";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "CHARACTER INDICATED BY \'C\'";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Montserrat Subrayada", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreLabel.Location = new System.Drawing.Point(100, 46);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(92, 20);
            this.ScoreLabel.TabIndex = 7;
            this.ScoreLabel.Text = "Score: #";
            // 
            // MovesLabel
            // 
            this.MovesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MovesLabel.AutoSize = true;
            this.MovesLabel.Font = new System.Drawing.Font("Montserrat Subrayada", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovesLabel.Location = new System.Drawing.Point(606, 46);
            this.MovesLabel.Name = "MovesLabel";
            this.MovesLabel.Size = new System.Drawing.Size(95, 20);
            this.MovesLabel.TabIndex = 8;
            this.MovesLabel.Text = "Moves: #";
            this.MovesLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Egg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 416);
            this.Controls.Add(this.MovesLabel);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RightButton);
            this.Controls.Add(this.LeftButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.GameSpace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Egg";
            this.Text = "Egg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameSpace;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button LeftButton;
        private System.Windows.Forms.Button RightButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label MovesLabel;
    }
}