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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(301, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "LID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Line Iceberg Display";
            // 
            // filesButton
            // 
            this.filesButton.Location = new System.Drawing.Point(300, 249);
            this.filesButton.Name = "filesButton";
            this.filesButton.Size = new System.Drawing.Size(133, 42);
            this.filesButton.TabIndex = 2;
            this.filesButton.Text = "Open Files";
            this.filesButton.UseVisualStyleBackColor = true;
            this.filesButton.Click += new System.EventHandler(this.filesButton_Click);
            // 
            // LID_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.filesButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LID_Form";
            this.Text = "LID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button filesButton;
    }
}