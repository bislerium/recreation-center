
namespace recreation_centre
{
    partial class Login
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
            this.minimizeB = new System.Windows.Forms.Button();
            this.closeB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 20);
            this.label1.TabIndex = 29;
            this.label1.Text = "Recreational Center | Login";
            // 
            // minimizeB
            // 
            this.minimizeB.BackgroundImage = global::recreation_centre.Properties.Resources.download;
            this.minimizeB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeB.Location = new System.Drawing.Point(702, 12);
            this.minimizeB.Name = "minimizeB";
            this.minimizeB.Size = new System.Drawing.Size(40, 40);
            this.minimizeB.TabIndex = 28;
            this.minimizeB.UseVisualStyleBackColor = true;
            // 
            // closeB
            // 
            this.closeB.BackgroundImage = global::recreation_centre.Properties.Resources._31631915371lyniu2zkjrlmbmhkqxc9kvtfx68cnz2xlt2rjuj76epxi2rwewm7g83rnuzcvyqnedbb3dxjrxiqtvtbdegg7gqjqanaebkz3zb4_removebg_preview__1_;
            this.closeB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeB.Location = new System.Drawing.Point(748, 12);
            this.closeB.Name = "closeB";
            this.closeB.Size = new System.Drawing.Size(40, 40);
            this.closeB.TabIndex = 27;
            this.closeB.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minimizeB);
            this.Controls.Add(this.closeB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button minimizeB;
        private System.Windows.Forms.Button closeB;
    }
}