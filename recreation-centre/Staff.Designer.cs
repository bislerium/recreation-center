
namespace recreation_centre
{
    partial class Staff
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.checkInTab = new System.Windows.Forms.TabPage();
            this.checkOutTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTF = new System.Windows.Forms.TextBox();
            this.phoneTF = new System.Windows.Forms.TextBox();
            this.ageTF = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.checkInTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 426);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.checkInTab);
            this.tabControl1.Controls.Add(this.checkOutTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // checkInTab
            // 
            this.checkInTab.Controls.Add(this.ageTF);
            this.checkInTab.Controls.Add(this.phoneTF);
            this.checkInTab.Controls.Add(this.nameTF);
            this.checkInTab.Controls.Add(this.label4);
            this.checkInTab.Controls.Add(this.label3);
            this.checkInTab.Controls.Add(this.label2);
            this.checkInTab.Controls.Add(this.label1);
            this.checkInTab.Location = new System.Drawing.Point(4, 25);
            this.checkInTab.Name = "checkInTab";
            this.checkInTab.Padding = new System.Windows.Forms.Padding(3);
            this.checkInTab.Size = new System.Drawing.Size(768, 397);
            this.checkInTab.TabIndex = 0;
            this.checkInTab.Text = "Check-in";
            this.checkInTab.UseVisualStyleBackColor = true;
            // 
            // checkOutTab
            // 
            this.checkOutTab.Location = new System.Drawing.Point(4, 25);
            this.checkOutTab.Name = "checkOutTab";
            this.checkOutTab.Padding = new System.Windows.Forms.Padding(3);
            this.checkOutTab.Size = new System.Drawing.Size(768, 397);
            this.checkOutTab.TabIndex = 1;
            this.checkOutTab.Text = "Check-out";
            this.checkOutTab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Phone";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Age";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Group";
            // 
            // nameTF
            // 
            this.nameTF.Location = new System.Drawing.Point(92, 17);
            this.nameTF.Name = "nameTF";
            this.nameTF.Size = new System.Drawing.Size(210, 22);
            this.nameTF.TabIndex = 4;
            // 
            // phoneTF
            // 
            this.phoneTF.Location = new System.Drawing.Point(92, 51);
            this.phoneTF.Name = "phoneTF";
            this.phoneTF.Size = new System.Drawing.Size(210, 22);
            this.phoneTF.TabIndex = 5;
            // 
            // ageTF
            // 
            this.ageTF.Location = new System.Drawing.Point(92, 85);
            this.ageTF.Name = "ageTF";
            this.ageTF.Size = new System.Drawing.Size(210, 22);
            this.ageTF.TabIndex = 6;
            // 
            // Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Staff";
            this.Text = "Staff";
            this.tabControl1.ResumeLayout(false);
            this.checkInTab.ResumeLayout(false);
            this.checkInTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage checkInTab;
        private System.Windows.Forms.TabPage checkOutTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ageTF;
        private System.Windows.Forms.TextBox phoneTF;
        private System.Windows.Forms.TextBox nameTF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}