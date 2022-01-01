using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend;

namespace recreation_centre
{
    public partial class Admin : Form
    {
        private readonly VisitorProcess visitorProcess;
        private readonly TicketProcess ticketProcess;
        private Ticket ticket;

        public Admin(VisitorProcess visitorProcess, TicketProcess ticketProcess)
        {
            InitializeComponent();
            this.visitorProcess = visitorProcess;
            this.ticketProcess = ticketProcess;
        }

        private void logoutB_Click(object sender, EventArgs e)
        {
            new Login();
            this.Dispose();
        }

        private void closeB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeB_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        bool flag = false;

        private bool mouseDown;
        private Point lastLocation;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void weekdaysTB_KeyUp(object sender, KeyEventArgs e)
        {
            String value = weekdaysTB.Text.Trim();
            mondayTB.Text = value;
            tuesdayTB.Text = value;
            wednesdayTB.Text = value;
            thrusdayTB.Text = value;
            fridayTB.Text = value;
        }

        private void holidaysTB_KeyUp(object sender, KeyEventArgs e)
        {
            String value = holidaysTB.Text.Trim();
            saturdayTB.Text = value;
            sundayTB.Text = value;
        }
    }
}
