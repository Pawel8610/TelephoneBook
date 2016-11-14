namespace TelephoneBook
{
    partial class BigPicture
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1ZoomIn = new System.Windows.Forms.Button();
            this.button2ZoomOut = new System.Windows.Forms.Button();
            this.button1CenterImage = new System.Windows.Forms.Button();
            this.button1oryginalSize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1056, 588);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel1.Location = new System.Drawing.Point(41, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1074, 613);
            this.panel1.TabIndex = 0;
            // 
            // button1ZoomIn
            // 
            this.button1ZoomIn.Location = new System.Drawing.Point(106, 1);
            this.button1ZoomIn.Name = "button1ZoomIn";
            this.button1ZoomIn.Size = new System.Drawing.Size(75, 23);
            this.button1ZoomIn.TabIndex = 1;
            this.button1ZoomIn.Text = "Zoom In +";
            this.button1ZoomIn.UseVisualStyleBackColor = true;
            this.button1ZoomIn.Click += new System.EventHandler(this.button1ZoomIn_Click);
            // 
            // button2ZoomOut
            // 
            this.button2ZoomOut.Location = new System.Drawing.Point(187, 1);
            this.button2ZoomOut.Name = "button2ZoomOut";
            this.button2ZoomOut.Size = new System.Drawing.Size(75, 23);
            this.button2ZoomOut.TabIndex = 2;
            this.button2ZoomOut.Text = "Zoom Out -";
            this.button2ZoomOut.UseVisualStyleBackColor = true;
            this.button2ZoomOut.Click += new System.EventHandler(this.button2ZoomOut_Click);
            // 
            // button1CenterImage
            // 
            this.button1CenterImage.Location = new System.Drawing.Point(268, 1);
            this.button1CenterImage.Name = "button1CenterImage";
            this.button1CenterImage.Size = new System.Drawing.Size(75, 23);
            this.button1CenterImage.TabIndex = 3;
            this.button1CenterImage.Text = "Center Image";
            this.button1CenterImage.UseVisualStyleBackColor = true;
            this.button1CenterImage.Click += new System.EventHandler(this.button1CenterImage_Click);
            // 
            // button1oryginalSize
            // 
            this.button1oryginalSize.Location = new System.Drawing.Point(349, 1);
            this.button1oryginalSize.Name = "button1oryginalSize";
            this.button1oryginalSize.Size = new System.Drawing.Size(75, 23);
            this.button1oryginalSize.TabIndex = 4;
            this.button1oryginalSize.Text = "100%";
            this.button1oryginalSize.UseVisualStyleBackColor = true;
            this.button1oryginalSize.Click += new System.EventHandler(this.button1oryginalSize_Click);
            // 
            // BigPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 668);
            this.Controls.Add(this.button1oryginalSize);
            this.Controls.Add(this.button1CenterImage);
            this.Controls.Add(this.button1ZoomIn);
            this.Controls.Add(this.button2ZoomOut);
            this.Controls.Add(this.panel1);
            this.Name = "BigPicture";
            this.Text = "BigPicture";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BigPicture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1ZoomIn;
        private System.Windows.Forms.Button button2ZoomOut;
        private System.Windows.Forms.Button button1CenterImage;
        private System.Windows.Forms.Button button1oryginalSize;
    }
}