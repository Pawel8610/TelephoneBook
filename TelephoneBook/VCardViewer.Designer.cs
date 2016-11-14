namespace TelephoneBook
{
    partial class VCardViewer
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3AddAlloToDB = new System.Windows.Forms.Button();
            this.checkBox1AddImage = new System.Windows.Forms.CheckBox();
            this.button4Remove = new System.Windows.Forms.Button();
            this.checkBox1SkipEmail = new System.Windows.Forms.CheckBox();
            this.button3SaveVcard = new System.Windows.Forms.Button();
            this.button3ClearList = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3Percentage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add selected to database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(452, 251);
            this.listBox1.TabIndex = 1;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(484, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Contacts:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(584, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(458, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 161);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Open vCard file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3AddAlloToDB
            // 
            this.button3AddAlloToDB.Location = new System.Drawing.Point(155, 340);
            this.button3AddAlloToDB.Name = "button3AddAlloToDB";
            this.button3AddAlloToDB.Size = new System.Drawing.Size(157, 23);
            this.button3AddAlloToDB.TabIndex = 6;
            this.button3AddAlloToDB.Text = "Add all positions to database";
            this.button3AddAlloToDB.UseVisualStyleBackColor = true;
            this.button3AddAlloToDB.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1AddImage
            // 
            this.checkBox1AddImage.AutoSize = true;
            this.checkBox1AddImage.Location = new System.Drawing.Point(22, 369);
            this.checkBox1AddImage.Name = "checkBox1AddImage";
            this.checkBox1AddImage.Size = new System.Drawing.Size(80, 17);
            this.checkBox1AddImage.TabIndex = 7;
            this.checkBox1AddImage.Text = "With Image";
            this.checkBox1AddImage.UseVisualStyleBackColor = true;
            // 
            // button4Remove
            // 
            this.button4Remove.Location = new System.Drawing.Point(12, 299);
            this.button4Remove.Name = "button4Remove";
            this.button4Remove.Size = new System.Drawing.Size(146, 23);
            this.button4Remove.TabIndex = 8;
            this.button4Remove.Text = "Remove selected from list";
            this.button4Remove.UseVisualStyleBackColor = true;
            this.button4Remove.Click += new System.EventHandler(this.button4Remove_Click);
            // 
            // checkBox1SkipEmail
            // 
            this.checkBox1SkipEmail.AutoSize = true;
            this.checkBox1SkipEmail.Location = new System.Drawing.Point(141, 369);
            this.checkBox1SkipEmail.Name = "checkBox1SkipEmail";
            this.checkBox1SkipEmail.Size = new System.Drawing.Size(122, 17);
            this.checkBox1SkipEmail.TabIndex = 9;
            this.checkBox1SkipEmail.Text = "Skip email validation";
            this.checkBox1SkipEmail.UseVisualStyleBackColor = true;
            // 
            // button3SaveVcard
            // 
            this.button3SaveVcard.Location = new System.Drawing.Point(318, 340);
            this.button3SaveVcard.Name = "button3SaveVcard";
            this.button3SaveVcard.Size = new System.Drawing.Size(75, 23);
            this.button3SaveVcard.TabIndex = 10;
            this.button3SaveVcard.Text = "Save vCard";
            this.button3SaveVcard.UseVisualStyleBackColor = true;
            this.button3SaveVcard.Click += new System.EventHandler(this.button3SaveVcard_Click);
            // 
            // button3ClearList
            // 
            this.button3ClearList.Location = new System.Drawing.Point(164, 299);
            this.button3ClearList.Name = "button3ClearList";
            this.button3ClearList.Size = new System.Drawing.Size(75, 23);
            this.button3ClearList.TabIndex = 11;
            this.button3ClearList.Text = "Clear list";
            this.button3ClearList.UseVisualStyleBackColor = true;
            this.button3ClearList.Click += new System.EventHandler(this.button3ClearList_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(96, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(297, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // label3Percentage
            // 
            this.label3Percentage.AutoSize = true;
            this.label3Percentage.Location = new System.Drawing.Point(408, 9);
            this.label3Percentage.Name = "label3Percentage";
            this.label3Percentage.Size = new System.Drawing.Size(21, 13);
            this.label3Percentage.TabIndex = 13;
            this.label3Percentage.Text = "0%";
            // 
            // VCardViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 392);
            this.Controls.Add(this.label3Percentage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button3ClearList);
            this.Controls.Add(this.button3SaveVcard);
            this.Controls.Add(this.checkBox1SkipEmail);
            this.Controls.Add(this.button4Remove);
            this.Controls.Add(this.checkBox1AddImage);
            this.Controls.Add(this.button3AddAlloToDB);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "VCardViewer";
            this.Text = "VCardViewer";
            this.Load += new System.EventHandler(this.VCardViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3AddAlloToDB;
        private System.Windows.Forms.CheckBox checkBox1AddImage;
        private System.Windows.Forms.Button button4Remove;
        private System.Windows.Forms.CheckBox checkBox1SkipEmail;
        private System.Windows.Forms.Button button3SaveVcard;
        private System.Windows.Forms.Button button3ClearList;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3Percentage;
    }
}