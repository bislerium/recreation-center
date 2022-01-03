namespace recreation_centre
{
    partial class CheckinStaff
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
            this.label1 = new System.Windows.Forms.Label();
            this.allclearB = new System.Windows.Forms.Button();
            this.checkoutB = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.oldAdultMTB = new System.Windows.Forms.MaskedTextBox();
            this.middleAdultMTB = new System.Windows.Forms.MaskedTextBox();
            this.youngAdultMTB = new System.Windows.Forms.MaskedTextBox();
            this.childMTB = new System.Windows.Forms.MaskedTextBox();
            this.ageMTB = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dayMTB = new System.Windows.Forms.TextBox();
            this.syncCurTimeB = new System.Windows.Forms.Button();
            this.inDateTimeDTP = new System.Windows.Forms.DateTimePicker();
            this.phoneMTB = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.loginUserTB = new System.Windows.Forms.TextBox();
            this.logoutB = new System.Windows.Forms.Button();
            this.minimizeB = new System.Windows.Forms.Button();
            this.closeB = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Recreational Center | Check-out";
            // 
            // allclearB
            // 
            this.allclearB.BackColor = System.Drawing.Color.Red;
            this.allclearB.ForeColor = System.Drawing.SystemColors.Window;
            this.allclearB.Location = new System.Drawing.Point(270, 279);
            this.allclearB.Name = "allclearB";
            this.allclearB.Size = new System.Drawing.Size(226, 40);
            this.allclearB.TabIndex = 13;
            this.allclearB.Text = "AC";
            this.allclearB.UseVisualStyleBackColor = false;
            this.allclearB.Click += new System.EventHandler(this.allclearB_Click);
            // 
            // checkoutB
            // 
            this.checkoutB.BackColor = System.Drawing.SystemColors.Highlight;
            this.checkoutB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkoutB.ForeColor = System.Drawing.Color.White;
            this.checkoutB.Location = new System.Drawing.Point(270, 325);
            this.checkoutB.Name = "checkoutB";
            this.checkoutB.Size = new System.Drawing.Size(226, 40);
            this.checkoutB.TabIndex = 12;
            this.checkoutB.Text = "Check-in";
            this.checkoutB.UseVisualStyleBackColor = false;
            this.checkoutB.Click += new System.EventHandler(this.checkoutB_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.oldAdultMTB);
            this.panel2.Controls.Add(this.middleAdultMTB);
            this.panel2.Controls.Add(this.youngAdultMTB);
            this.panel2.Controls.Add(this.childMTB);
            this.panel2.Controls.Add(this.checkoutB);
            this.panel2.Controls.Add(this.allclearB);
            this.panel2.Controls.Add(this.ageMTB);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.dayMTB);
            this.panel2.Controls.Add(this.syncCurTimeB);
            this.panel2.Controls.Add(this.inDateTimeDTP);
            this.panel2.Controls.Add(this.phoneMTB);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.nameTB);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(16, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 380);
            this.panel2.TabIndex = 11;
            // 
            // oldAdultMTB
            // 
            this.oldAdultMTB.BeepOnError = true;
            this.oldAdultMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldAdultMTB.Location = new System.Drawing.Point(272, 225);
            this.oldAdultMTB.Mask = "000";
            this.oldAdultMTB.Name = "oldAdultMTB";
            this.oldAdultMTB.Size = new System.Drawing.Size(224, 27);
            this.oldAdultMTB.TabIndex = 29;
            this.oldAdultMTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // middleAdultMTB
            // 
            this.middleAdultMTB.BeepOnError = true;
            this.middleAdultMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.middleAdultMTB.Location = new System.Drawing.Point(270, 168);
            this.middleAdultMTB.Mask = "000";
            this.middleAdultMTB.Name = "middleAdultMTB";
            this.middleAdultMTB.Size = new System.Drawing.Size(226, 27);
            this.middleAdultMTB.TabIndex = 28;
            this.middleAdultMTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // youngAdultMTB
            // 
            this.youngAdultMTB.BeepOnError = true;
            this.youngAdultMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.youngAdultMTB.Location = new System.Drawing.Point(270, 117);
            this.youngAdultMTB.Mask = "000";
            this.youngAdultMTB.Name = "youngAdultMTB";
            this.youngAdultMTB.Size = new System.Drawing.Size(226, 27);
            this.youngAdultMTB.TabIndex = 27;
            this.youngAdultMTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // childMTB
            // 
            this.childMTB.BeepOnError = true;
            this.childMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.childMTB.Location = new System.Drawing.Point(270, 66);
            this.childMTB.Mask = "000";
            this.childMTB.Name = "childMTB";
            this.childMTB.Size = new System.Drawing.Size(226, 27);
            this.childMTB.TabIndex = 26;
            this.childMTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ageMTB
            // 
            this.ageMTB.BeepOnError = true;
            this.ageMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageMTB.Location = new System.Drawing.Point(17, 162);
            this.ageMTB.Mask = "000";
            this.ageMTB.Name = "ageMTB";
            this.ageMTB.Size = new System.Drawing.Size(227, 27);
            this.ageMTB.TabIndex = 25;
            this.ageMTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 20);
            this.label10.TabIndex = 24;
            this.label10.Text = "Age:";
            // 
            // dayMTB
            // 
            this.dayMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayMTB.Location = new System.Drawing.Point(17, 338);
            this.dayMTB.Name = "dayMTB";
            this.dayMTB.ReadOnly = true;
            this.dayMTB.Size = new System.Drawing.Size(227, 27);
            this.dayMTB.TabIndex = 23;
            this.dayMTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // syncCurTimeB
            // 
            this.syncCurTimeB.BackColor = System.Drawing.SystemColors.Highlight;
            this.syncCurTimeB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syncCurTimeB.ForeColor = System.Drawing.Color.White;
            this.syncCurTimeB.Location = new System.Drawing.Point(17, 262);
            this.syncCurTimeB.Name = "syncCurTimeB";
            this.syncCurTimeB.Size = new System.Drawing.Size(227, 40);
            this.syncCurTimeB.TabIndex = 22;
            this.syncCurTimeB.Text = "Sync Current Time";
            this.syncCurTimeB.UseVisualStyleBackColor = false;
            this.syncCurTimeB.Click += new System.EventHandler(this.syncCurTimeB_Click);
            // 
            // inDateTimeDTP
            // 
            this.inDateTimeDTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inDateTimeDTP.Location = new System.Drawing.Point(17, 229);
            this.inDateTimeDTP.Name = "inDateTimeDTP";
            this.inDateTimeDTP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inDateTimeDTP.Size = new System.Drawing.Size(227, 27);
            this.inDateTimeDTP.TabIndex = 21;
            this.inDateTimeDTP.ValueChanged += new System.EventHandler(this.calenderDate_click);
            // 
            // phoneMTB
            // 
            this.phoneMTB.BeepOnError = true;
            this.phoneMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneMTB.Location = new System.Drawing.Point(17, 99);
            this.phoneMTB.Mask = "\\98|00000000";
            this.phoneMTB.Name = "phoneMTB";
            this.phoneMTB.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.phoneMTB.Size = new System.Drawing.Size(227, 27);
            this.phoneMTB.TabIndex = 20;
            this.phoneMTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Phone:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 20);
            this.label12.TabIndex = 17;
            this.label12.Text = "Day:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "In-DateTime:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(268, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = "Old Adult (above 45)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(267, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 5;
            this.label7.Text = "Middle Adult (31-45)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(267, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Young Adult (17-30)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(268, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "Child (0-16)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(266, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Group:";
            // 
            // nameTB
            // 
            this.nameTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTB.Location = new System.Drawing.Point(17, 37);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(227, 27);
            this.nameTB.TabIndex = 1;
            this.nameTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(172, 447);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 16);
            this.label13.TabIndex = 15;
            this.label13.Text = "Logged-in User:";
            // 
            // loginUserTB
            // 
            this.loginUserTB.Enabled = false;
            this.loginUserTB.Location = new System.Drawing.Point(281, 444);
            this.loginUserTB.Name = "loginUserTB";
            this.loginUserTB.Size = new System.Drawing.Size(249, 22);
            this.loginUserTB.TabIndex = 14;
            this.loginUserTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // logoutB
            // 
            this.logoutB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.logoutB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutB.Image = global::recreation_centre.Properties.Resources.icons8_logout_24;
            this.logoutB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.logoutB.Location = new System.Drawing.Point(331, 12);
            this.logoutB.Name = "logoutB";
            this.logoutB.Size = new System.Drawing.Size(110, 40);
            this.logoutB.TabIndex = 9;
            this.logoutB.Text = "LOGOUT";
            this.logoutB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.logoutB.UseVisualStyleBackColor = false;
            this.logoutB.Click += new System.EventHandler(this.logoutB_Click);
            // 
            // minimizeB
            // 
            this.minimizeB.BackgroundImage = global::recreation_centre.Properties.Resources.download;
            this.minimizeB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeB.Location = new System.Drawing.Point(447, 12);
            this.minimizeB.Name = "minimizeB";
            this.minimizeB.Size = new System.Drawing.Size(40, 40);
            this.minimizeB.TabIndex = 8;
            this.minimizeB.UseVisualStyleBackColor = true;
            this.minimizeB.Click += new System.EventHandler(this.minimizeB_Click);
            // 
            // closeB
            // 
            this.closeB.BackgroundImage = global::recreation_centre.Properties.Resources._31631915371lyniu2zkjrlmbmhkqxc9kvtfx68cnz2xlt2rjuj76epxi2rwewm7g83rnuzcvyqnedbb3dxjrxiqtvtbdegg7gqjqanaebkz3zb4_removebg_preview__1_;
            this.closeB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeB.Location = new System.Drawing.Point(493, 12);
            this.closeB.Name = "closeB";
            this.closeB.Size = new System.Drawing.Size(40, 40);
            this.closeB.TabIndex = 7;
            this.closeB.UseVisualStyleBackColor = true;
            this.closeB.Click += new System.EventHandler(this.closeB_Click);
            // 
            // CheckinStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 482);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logoutB);
            this.Controls.Add(this.minimizeB);
            this.Controls.Add(this.closeB);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.loginUserTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CheckinStaff";
            this.Text = "CheckinStaff";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button logoutB;
        private System.Windows.Forms.Button minimizeB;
        private System.Windows.Forms.Button closeB;
        private System.Windows.Forms.Button allclearB;
        private System.Windows.Forms.Button checkoutB;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox loginUserTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox phoneMTB;
        private System.Windows.Forms.Button syncCurTimeB;
        private System.Windows.Forms.DateTimePicker inDateTimeDTP;
        private System.Windows.Forms.MaskedTextBox oldAdultMTB;
        private System.Windows.Forms.MaskedTextBox middleAdultMTB;
        private System.Windows.Forms.MaskedTextBox youngAdultMTB;
        private System.Windows.Forms.MaskedTextBox childMTB;
        private System.Windows.Forms.MaskedTextBox ageMTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox dayMTB;
    }
}