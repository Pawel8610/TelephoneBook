namespace TelephoneBook
{
    partial class Email
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1From = new System.Windows.Forms.TextBox();
            this.textBox2Subject = new System.Windows.Forms.TextBox();
            this.textBox3UserName = new System.Windows.Forms.TextBox();
            this.textBox4To = new System.Windows.Forms.TextBox();
            this.textBox6Password = new System.Windows.Forms.TextBox();
            this.comboBox1SmtpServer = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2Port = new System.Windows.Forms.ComboBox();
            this.button1Send = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 123);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(603, 325);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Subject:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "User Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "To:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Smtp Server:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Password:";
            // 
            // textBox1From
            // 
            this.textBox1From.Location = new System.Drawing.Point(64, 6);
            this.textBox1From.Name = "textBox1From";
            this.textBox1From.Size = new System.Drawing.Size(223, 20);
            this.textBox1From.TabIndex = 7;
            // 
            // textBox2Subject
            // 
            this.textBox2Subject.Location = new System.Drawing.Point(64, 97);
            this.textBox2Subject.Name = "textBox2Subject";
            this.textBox2Subject.Size = new System.Drawing.Size(551, 20);
            this.textBox2Subject.TabIndex = 8;
            // 
            // textBox3UserName
            // 
            this.textBox3UserName.Location = new System.Drawing.Point(64, 41);
            this.textBox3UserName.Name = "textBox3UserName";
            this.textBox3UserName.Size = new System.Drawing.Size(223, 20);
            this.textBox3UserName.TabIndex = 9;
            // 
            // textBox4To
            // 
            this.textBox4To.Location = new System.Drawing.Point(335, 6);
            this.textBox4To.Name = "textBox4To";
            this.textBox4To.Size = new System.Drawing.Size(270, 20);
            this.textBox4To.TabIndex = 10;
            // 
            // textBox6Password
            // 
            this.textBox6Password.Location = new System.Drawing.Point(64, 67);
            this.textBox6Password.Name = "textBox6Password";
            this.textBox6Password.Size = new System.Drawing.Size(223, 20);
            this.textBox6Password.TabIndex = 12;
            this.textBox6Password.UseSystemPasswordChar = true;
            // 
            // comboBox1SmtpServer
            // 
            this.comboBox1SmtpServer.FormattingEnabled = true;
            this.comboBox1SmtpServer.Items.AddRange(new object[] {
            "mail.blulink.pl",
            "smtp.gmail.com",
            "poczta.o2.pl",
            "poczta.interia.pl",
            "poczta.onet.pl",
            "smtp.wp.pl"});
            this.comboBox1SmtpServer.Location = new System.Drawing.Point(380, 40);
            this.comboBox1SmtpServer.Name = "comboBox1SmtpServer";
            this.comboBox1SmtpServer.Size = new System.Drawing.Size(188, 21);
            this.comboBox1SmtpServer.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(345, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Port:";
            // 
            // comboBox2Port
            // 
            this.comboBox2Port.FormattingEnabled = true;
            this.comboBox2Port.Items.AddRange(new object[] {
            "587",
            "465",
            "25"});
            this.comboBox2Port.Location = new System.Drawing.Point(380, 66);
            this.comboBox2Port.Name = "comboBox2Port";
            this.comboBox2Port.Size = new System.Drawing.Size(121, 21);
            this.comboBox2Port.TabIndex = 15;
            // 
            // button1Send
            // 
            this.button1Send.ForeColor = System.Drawing.Color.Red;
            this.button1Send.Location = new System.Drawing.Point(507, 66);
            this.button1Send.Name = "button1Send";
            this.button1Send.Size = new System.Drawing.Size(108, 23);
            this.button1Send.TabIndex = 16;
            this.button1Send.Text = "Send";
            this.button1Send.UseVisualStyleBackColor = true;
            this.button1Send.Click += new System.EventHandler(this.button1Send_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.OliveDrab;
            this.label8.Location = new System.Drawing.Point(612, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(307, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Or send email via your phone by scanning this QRcode:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(635, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(258, 233);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 451);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1Send);
            this.Controls.Add(this.comboBox2Port);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox1SmtpServer);
            this.Controls.Add(this.textBox6Password);
            this.Controls.Add(this.textBox4To);
            this.Controls.Add(this.textBox3UserName);
            this.Controls.Add(this.textBox2Subject);
            this.Controls.Add(this.textBox1From);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Email";
            this.Text = "Email";
            this.Load += new System.EventHandler(this.Email_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1From;
        private System.Windows.Forms.TextBox textBox2Subject;
        private System.Windows.Forms.TextBox textBox3UserName;
        private System.Windows.Forms.TextBox textBox4To;
        private System.Windows.Forms.TextBox textBox6Password;
        private System.Windows.Forms.ComboBox comboBox1SmtpServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2Port;
        private System.Windows.Forms.Button button1Send;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}