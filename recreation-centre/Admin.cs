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
        private readonly Ticket ticket;

        public Admin(VisitorProcess visitorProcess, TicketProcess ticketProcess)
        {
            InitializeComponent();
            this.visitorProcess = visitorProcess;
            this.ticketProcess = ticketProcess;
            ticket = ticketProcess.GetTicket();
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

        private void setBasePriceB_Click(object sender, EventArgs e)
        {
            String basePrice = basePriceMTB.Text.Trim();
            ticket.BasePrice = int.Parse( basePrice);
        }

        private void setAgeGroupB_Click(object sender, EventArgs e)
        {
            string childPriceRate = childTB.Text.Trim();
            string youngAdultPriceRate = youngAdultTB.Text.Trim();
            string middleAdultPriceRate = middleAdultTB.Text.Trim();
            string oldAdultPriceRate = oldAdultTB.Text.Trim();
            if (string.IsNullOrWhiteSpace(childPriceRate) || string.IsNullOrWhiteSpace(youngAdultPriceRate)
                || string.IsNullOrWhiteSpace(middleAdultPriceRate) || string.IsNullOrWhiteSpace(oldAdultPriceRate))
            {
                MessageBox.Show("Please, fill All the AgeGroup Fields!", "Error: Empty Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ticket.Age[AgeGroupE.CHILD] = decimal.Parse(childPriceRate);
            ticket.Age[AgeGroupE.YOUNG_ADULT] = decimal.Parse(youngAdultPriceRate);
            ticket.Age[AgeGroupE.MIDDLE_ADULT] = decimal.Parse(middleAdultPriceRate);
            ticket.Age[AgeGroupE.OLD_ADULT] = decimal.Parse(oldAdultPriceRate);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ticketProcess.WriteTicket())
            {
                MessageBox.Show($"Successfully, flushed Ticket Data locally at:\n\"{ticketProcess.getFileSource()}\"", "Ticket Flush", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("Something went Wrong!", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String sundayPriceRate = sundayTB.Text.Trim();
            String mondayPriceRate = sundayTB.Text.Trim();
            String tuesdayPriceRate = sundayTB.Text.Trim();
            String wednesdayPriceRate = sundayTB.Text.Trim();
            String thrusdayPriceRate = sundayTB.Text.Trim();
            String fridayPriceRate = sundayTB.Text.Trim();
            String saturdayPriceRate = sundayTB.Text.Trim();
            if (string.IsNullOrWhiteSpace(sundayPriceRate) || string.IsNullOrWhiteSpace(mondayPriceRate)
                || string.IsNullOrWhiteSpace(tuesdayPriceRate) || string.IsNullOrWhiteSpace(wednesdayPriceRate)
                || string.IsNullOrWhiteSpace(thrusdayPriceRate) || string.IsNullOrWhiteSpace(fridayPriceRate)
                || string.IsNullOrWhiteSpace(saturdayPriceRate))
            {
                MessageBox.Show("Please, don't leave any Day-Fields Empty", "Error: Empty Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ticket.Day[DayOfWeek.Sunday] = decimal.Parse(sundayPriceRate);
            ticket.Day[DayOfWeek.Monday] = decimal.Parse(mondayPriceRate);
            ticket.Day[DayOfWeek.Tuesday] = decimal.Parse(tuesdayPriceRate);
            ticket.Day[DayOfWeek.Wednesday] = decimal.Parse(wednesdayPriceRate);
            ticket.Day[DayOfWeek.Thursday] = decimal.Parse(thrusdayPriceRate);
            ticket.Day[DayOfWeek.Friday] = decimal.Parse(fridayPriceRate);
            ticket.Day[DayOfWeek.Saturday] = decimal.Parse(saturdayPriceRate);
        }
    }
}
