
namespace recreation_centre
{
    partial class Admin
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
            this.logoutB = new System.Windows.Forms.Button();
            this.minimizeB = new System.Windows.Forms.Button();
            this.closeB = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Recreational Center | Admin";
            // 
            // logoutB
            // 
            this.logoutB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.logoutB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutB.Image = global::recreation_centre.Properties.Resources.icons8_logout_24;
            this.logoutB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.logoutB.Location = new System.Drawing.Point(586, 12);
            this.logoutB.Name = "logoutB";
            this.logoutB.Size = new System.Drawing.Size(110, 40);
            this.logoutB.TabIndex = 25;
            this.logoutB.Text = "LOGOUT";
            this.logoutB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.logoutB.UseVisualStyleBackColor = false;
            this.logoutB.Click += new System.EventHandler(this.logoutB_Click);
            // 
            // minimizeB
            // 
            this.minimizeB.BackgroundImage = global::recreation_centre.Properties.Resources.download;
            this.minimizeB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeB.Location = new System.Drawing.Point(702, 12);
            this.minimizeB.Name = "minimizeB";
            this.minimizeB.Size = new System.Drawing.Size(40, 40);
            this.minimizeB.TabIndex = 24;
            this.minimizeB.UseVisualStyleBackColor = true;
            this.minimizeB.Click += new System.EventHandler(this.minimizeB_Click);
            // 
            // closeB
            // 
            this.closeB.BackgroundImage = global::recreation_centre.Properties.Resources._31631915371lyniu2zkjrlmbmhkqxc9kvtfx68cnz2xlt2rjuj76epxi2rwewm7g83rnuzcvyqnedbb3dxjrxiqtvtbdegg7gqjqanaebkz3zb4_removebg_preview__1_;
            this.closeB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeB.Location = new System.Drawing.Point(748, 12);
            this.closeB.Name = "closeB";
            this.closeB.Size = new System.Drawing.Size(40, 40);
            this.closeB.TabIndex = 23;
            this.closeB.UseVisualStyleBackColor = true;
            this.closeB.Click += new System.EventHandler(this.closeB_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(141, 478);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 20);
            this.label12.TabIndex = 27;
            this.label12.Text = "Day:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(391, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Group:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(158, 282);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(656, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(800, 673);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logoutB);
            this.Controls.Add(this.minimizeB);
            this.Controls.Add(this.closeB);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Admin";
            this.Text = "Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button logoutB;
        private System.Windows.Forms.Button minimizeB;
        private System.Windows.Forms.Button closeB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
    }
}