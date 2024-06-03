namespace Shopping_Management_System
{
    partial class hello_window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hello_window));
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.button_sign_in = new System.Windows.Forms.Button();
            this.button_register = new System.Windows.Forms.Button();
            this.pic_pas = new System.Windows.Forms.PictureBox();
            this.pic_user = new System.Windows.Forms.PictureBox();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_user)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_username
            // 
            this.textBox_username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.textBox_username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_username.Font = new System.Drawing.Font("Arial", 13F);
            this.textBox_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(173)))), ((int)(((byte)(91)))));
            this.textBox_username.HideSelection = false;
            this.textBox_username.Location = new System.Drawing.Point(100, 333);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(242, 25);
            this.textBox_username.TabIndex = 2;
            this.textBox_username.TabStop = false;
            this.textBox_username.Text = "Username";
            this.textBox_username.Click += new System.EventHandler(this.textBox_username_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(173)))), ((int)(((byte)(91)))));
            this.panel1.Location = new System.Drawing.Point(57, 364);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 1);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(173)))), ((int)(((byte)(91)))));
            this.panel2.Location = new System.Drawing.Point(57, 410);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(290, 1);
            this.panel2.TabIndex = 6;
            // 
            // textBox_pass
            // 
            this.textBox_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.textBox_pass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_pass.Font = new System.Drawing.Font("Arial", 13F);
            this.textBox_pass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(173)))), ((int)(((byte)(91)))));
            this.textBox_pass.HideSelection = false;
            this.textBox_pass.Location = new System.Drawing.Point(100, 379);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.Size = new System.Drawing.Size(242, 25);
            this.textBox_pass.TabIndex = 5;
            this.textBox_pass.TabStop = false;
            this.textBox_pass.Text = "Password";
            this.textBox_pass.Click += new System.EventHandler(this.textBox_pass_Click);
            // 
            // button_sign_in
            // 
            this.button_sign_in.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(173)))), ((int)(((byte)(91)))));
            this.button_sign_in.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_sign_in.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_sign_in.Font = new System.Drawing.Font("Arial", 14F);
            this.button_sign_in.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.button_sign_in.Location = new System.Drawing.Point(57, 493);
            this.button_sign_in.Name = "button_sign_in";
            this.button_sign_in.Size = new System.Drawing.Size(290, 40);
            this.button_sign_in.TabIndex = 7;
            this.button_sign_in.Text = "Sign in";
            this.button_sign_in.UseVisualStyleBackColor = false;
            this.button_sign_in.Click += new System.EventHandler(this.button_sign_in_Click);
            // 
            // button_register
            // 
            this.button_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_register.Font = new System.Drawing.Font("Arial", 14F);
            this.button_register.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(173)))), ((int)(((byte)(91)))));
            this.button_register.Location = new System.Drawing.Point(57, 546);
            this.button_register.Name = "button_register";
            this.button_register.Size = new System.Drawing.Size(290, 40);
            this.button_register.TabIndex = 8;
            this.button_register.Text = "Register";
            this.button_register.UseVisualStyleBackColor = true;
            this.button_register.Click += new System.EventHandler(this.button_register_Click);
            // 
            // pic_pas
            // 
            this.pic_pas.Image = ((System.Drawing.Image)(resources.GetObject("pic_pas.Image")));
            this.pic_pas.Location = new System.Drawing.Point(42, 371);
            this.pic_pas.Name = "pic_pas";
            this.pic_pas.Size = new System.Drawing.Size(65, 33);
            this.pic_pas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_pas.TabIndex = 4;
            this.pic_pas.TabStop = false;
            // 
            // pic_user
            // 
            this.pic_user.Image = ((System.Drawing.Image)(resources.GetObject("pic_user.Image")));
            this.pic_user.Location = new System.Drawing.Point(42, 325);
            this.pic_user.Name = "pic_user";
            this.pic_user.Size = new System.Drawing.Size(65, 33);
            this.pic_user.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_user.TabIndex = 1;
            this.pic_user.TabStop = false;
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_logo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_logo.Image")));
            this.pictureBox_logo.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_logo.InitialImage")));
            this.pictureBox_logo.Location = new System.Drawing.Point(69, 69);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(261, 213);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            // 
            // hello_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.BackgroundImage = global::Shopping_Management_System.Properties.Resources.background1;
            this.ClientSize = new System.Drawing.Size(398, 689);
            this.Controls.Add(this.button_register);
            this.Controls.Add(this.button_sign_in);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox_pass);
            this.Controls.Add(this.pic_pas);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.pic_user);
            this.Controls.Add(this.pictureBox_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "hello_window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Online Shopping-Sign In";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.hello_window_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pic_pas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_user)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.PictureBox pic_user;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.PictureBox pic_pas;
        private System.Windows.Forms.Button button_sign_in;
        private System.Windows.Forms.Button button_register;
    }
}