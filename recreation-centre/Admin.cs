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
        private VisitorProcess visitorProcess;
        private TicketProcess ticketProcess;
        private Ticket ticket;
        private List<Visitor> visitors;

        public Admin(VisitorProcess visitorProcess, TicketProcess ticketProcess)
        {
            InitializeComponent();
            this.visitorProcess = visitorProcess;
            this.ticketProcess = ticketProcess;
            initializeTicket();
            initializeVisitor();
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
            if (checkEmptyFields("Please, fill the Base Price Field!", basePrice))
            {
                return;
            }
            ticket.BasePrice = int.Parse( basePrice);
            MessageBox.Show($"BasePrice added successfully!", "Error: Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setAgeGroupB_Click(object sender, EventArgs e)
        {
            string childPriceRate = childTB.Text.Trim();
            string youngAdultPriceRate = youngAdultTB.Text.Trim();
            string middleAdultPriceRate = middleAdultTB.Text.Trim();
            string oldAdultPriceRate = oldAdultTB.Text.Trim();
            if (checkEmptyFields("Please, fill the AgeGroup Fields!", childPriceRate, youngAdultPriceRate,
                middleAdultPriceRate, oldAdultPriceRate))
            {
                return;
            }
            decimal[] values = checkDecimalFields(childTB, youngAdultTB, middleAdultTB, oldAdultTB);
            if (values == null)
            {
                return;
            }
            ticket.Age[AgeGroupE.CHILD] = values[0];
            ticket.Age[AgeGroupE.YOUNG_ADULT] = values[1];
            ticket.Age[AgeGroupE.MIDDLE_ADULT] = values[2];
            ticket.Age[AgeGroupE.OLD_ADULT] = values[3];
            setDefaultFieldBackColor(childTB, youngAdultTB, middleAdultTB, oldAdultTB);
            MessageBox.Show($"PriceRate for AgeGroups added successfully!", "Error: Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private Boolean checkEmptyFields(string message, params String[] stringValues) 
        {
            foreach (String value in stringValues)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show(message, "Error: Empty Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            return false;
        }

        private decimal[] checkDecimalFields(params TextBox[] testBoxList)
        {
            bool error = false;
            decimal[] values = new decimal[testBoxList.Length];
            for (int i = 0; i < values.Length; i++)
            {

                if (decimal.TryParse(testBoxList[i].Text.Trim(), out decimal value))
                {
                    values[i] = value;
                }
                else
                {
                    error = true;
                    testBoxList[i].BackColor = Color.Red;
                    testBoxList[i].ForeColor = Color.White;
                }
            }
            if (error)
            {
                values = null;
            }
            return values;
        }

        private void setGroupB_Click(object sender, EventArgs e)
        {
            string peopleCount = peopleCountCB.Text.Trim();
            string priceRate = groupRateTB.Text.Trim();
            if (checkEmptyFields("Please, fill the Group Fields!", peopleCount, priceRate))
            {
                return;
            }
            bool error = false;
            if (!short.TryParse(peopleCount, out short shortPeopleCount)) { 
                peopleCountCB.BackColor = Color.Red;
                peopleCountCB.ForeColor = Color.White;
                error = true;
            }
            if (!decimal.TryParse(priceRate, out decimal decimalPricerate))
            {
                groupRateTB.BackColor = Color.Red;
                groupRateTB.ForeColor = Color.White;
                error = true;                
            }
            if (error) return;
            ticket.Group[shortPeopleCount] = decimalPricerate;
            loadGroupCB();
            setDefaultFieldBackColor(peopleCountCB, groupRateTB);
            MessageBox.Show($"PriceRate for Group: {peopleCount} peoples added successfully!", "Error: Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteGroupB_Click(object sender, EventArgs e)
        {
            string peopleCount = peopleCountCB.Text;
            if (checkEmptyFields("Please, Select or Enter the People Count value!", peopleCount))
            {
                return ;
            }
            short id = short.Parse(peopleCount);
            if (!ticket.Group.ContainsKey(id))
            {
                MessageBox.Show("Sorry, The Group entry was not Found!", "Error: Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show($"Delete PriceRate for Group: {id} Peoples",
                "Delete Confirm",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                ticket.Group.Remove(id);
                loadGroupCB();
                peopleCountCB.Text = "";
                groupRateTB.Text = "";
            }

        }

        private void setDurationB_Click(object sender, EventArgs e)
        {
            string hourDuration = hourDurationCB.Text.Trim();
            string priceRate = durationRateTB.Text.Trim();
            if (checkEmptyFields("Please, fill the Duration Fields!", hourDuration, priceRate))
            {
                return;
            }
            bool error = false;
            if (!short.TryParse(hourDuration, out short shortHourDuration))
            {
                hourDurationCB.BackColor = Color.Red;
                hourDurationCB.ForeColor = Color.White;
                error = true;
            }
            if (!decimal.TryParse(priceRate, out decimal decimalPriceRate))
            {
                durationRateTB.BackColor = Color.Red;
                durationRateTB.ForeColor = Color.White;
                error = true;
            }
            if (error) return;
            ticket.Duration[shortHourDuration] = decimalPriceRate;
            loadDurationCB();
            setDefaultFieldBackColor(hourDurationCB, durationRateTB);
            MessageBox.Show($"PriceRate for Duration: {hourDuration} hours added successfully!", "Error: Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteDurationB_Click(object sender, EventArgs e)
        {
            string hourDuration = hourDurationCB.Text;
            if (checkEmptyFields("Please, Select or Enter the Hour Duration value!", hourDuration))
            {
                return;
            }
            short id = short.Parse(hourDuration);
            if (!ticket.Duration.ContainsKey(id)) 
            {
                MessageBox.Show("Sorry, The Duration entry was not Found!", "Error: Invalid Duration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show($"Delete PriceRate for Duration: {id} Hours",
                "Delete Confirm",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                ticket.Duration.Remove(id);
                loadDurationCB();
                durationRateTB.Text = "";
                hourDurationCB.Text = "";
            }
        }

        private void hourDurationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            durationRateTB.Text=ticket.Duration[short.Parse(hourDurationCB.Text)].ToString();
        }

        private void loadDurationCB()
        {
            hourDurationCB.Items.Clear();
            hourDurationCB.Text = "";
            durationRateTB.Text = "";
            foreach (var key in ticket.Duration.Keys)
            {
                hourDurationCB.Items.Add(key.ToString());
            }
        }
        private void loadGroupCB()
        {
            peopleCountCB.Items.Clear();
            peopleCountCB.Text = "";
            groupRateTB.Text = "";
            foreach (var key in ticket.Group.Keys)
            {
                peopleCountCB.Items.Add(key.ToString());
            }
        }

        private void setDefaultFieldBackColor(params Control[] controlList)
        {
            foreach (var control in controlList) 
            {
                control.ForeColor = Color.Black;
                control.BackColor = Color.White;
            }
        }

        private void peopleCountCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupRateTB.Text = ticket.Group[short.Parse(peopleCountCB.Text)].ToString();
        }

        private void flushTicketB_Click(object sender, EventArgs e)
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

        private void setDayB_Click(object sender, EventArgs e)
        {
            String sundayPriceRate = sundayTB.Text.Trim();
            String mondayPriceRate = mondayTB.Text.Trim();
            String tuesdayPriceRate = tuesdayTB.Text.Trim();
            String wednesdayPriceRate = wednesdayTB.Text.Trim();
            String thrusdayPriceRate = thrusdayTB.Text.Trim();
            String fridayPriceRate = fridayTB.Text.Trim();
            String saturdayPriceRate = saturdayTB.Text.Trim();
            if (checkEmptyFields("Please, fill the Day Fields!", sundayPriceRate, mondayPriceRate, tuesdayPriceRate, wednesdayPriceRate,
                thrusdayPriceRate, fridayPriceRate, saturdayPriceRate))
            {
                return;
            }
            decimal[] values = checkDecimalFields(sundayTB, mondayTB, tuesdayTB, wednesdayTB,
                thrusdayTB, fridayTB, saturdayTB);
            if (values == null)
            {
                return;
            }
            ticket.Day[DayOfWeek.Sunday] = values[0];
            ticket.Day[DayOfWeek.Monday] = values[1];
            ticket.Day[DayOfWeek.Tuesday] = values[2];
            ticket.Day[DayOfWeek.Wednesday] = values[3];
            ticket.Day[DayOfWeek.Thursday] = values[4];
            ticket.Day[DayOfWeek.Friday] = values[5];
            ticket.Day[DayOfWeek.Saturday] = values[6];
            setDefaultFieldBackColor(sundayTB, mondayTB, tuesdayTB, wednesdayTB,
                thrusdayTB, fridayTB, saturdayTB);
            MessageBox.Show($"PriceRate for Days added successfully!", "Error: Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void initializeTicket() 
        {
            if (!ticketProcess.ReadTicket())
            {
                MessageBox.Show($"Could'nt Read!\n\"Try deleting {ticketProcess.getFileSource()}\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ticket = ticketProcess.GetTicket();
            loadDurationCB();
            loadGroupCB();
            basePriceMTB.Text = ticket.BasePrice.ToString();
            childTB.Text = ticket.Age[AgeGroupE.CHILD].ToString();
            youngAdultTB.Text = ticket.Age[AgeGroupE.YOUNG_ADULT].ToString();
            middleAdultTB.Text = ticket.Age[AgeGroupE.MIDDLE_ADULT].ToString();
            oldAdultTB.Text = ticket.Age[AgeGroupE.OLD_ADULT].ToString();
            sundayTB.Text = ticket.Day[DayOfWeek.Sunday].ToString();
            mondayTB.Text = ticket.Day[DayOfWeek.Monday].ToString();
            tuesdayTB.Text = ticket.Day[DayOfWeek.Tuesday].ToString();
            wednesdayTB.Text = ticket.Day[DayOfWeek.Wednesday].ToString();
            thrusdayTB.Text = ticket.Day[DayOfWeek.Thursday].ToString();
            fridayTB.Text = ticket.Day[DayOfWeek.Friday].ToString();
            saturdayTB.Text = ticket.Day[DayOfWeek.Saturday].ToString();
        }

        private void exportTicketB_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportTicketDialogBox = new SaveFileDialog();

            exportTicketDialogBox.Filter = "json file (*.json)|*.json";
            exportTicketDialogBox.InitialDirectory = @"C:\";
            exportTicketDialogBox.Title = "Export Ticket";

            if (exportTicketDialogBox.ShowDialog() == DialogResult.OK)
            {
                TicketProcess tk = new TicketProcess(exportTicketDialogBox.FileName, ticket);
                if (!tk.WriteTicket())
                { 
                MessageBox.Show("Could'nt Write!", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void importTicketB_Click(object sender, EventArgs e)
        {
            commonDialog(RWMode.ticket);
        }

        enum RWMode
        { 
            visitors,
            ticket
        }

        private void commonDialog(RWMode mode)
        {
            OpenFileDialog importTicketDialogBox = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "json file (*.json)|*.json",
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            importTicketDialogBox.Title = (mode == RWMode.ticket) ? "Import Ticket" : "Import Visitors";
            if (importTicketDialogBox.ShowDialog() == DialogResult.OK)
            {
                if (mode == RWMode.ticket)
                {
                    ticketProcess = new TicketProcess(importTicketDialogBox.FileName);
                    initializeTicket();
                }
                else
                {
                    visitorProcess = new VisitorProcess(importTicketDialogBox.FileName);
                    initializeTicket();
                }                
            }
        }

        private void syncDataGrid_Click(object sender, EventArgs e)
        {
            visitorDataGrid.DataSource = visitors;
        }

        private void initializeVisitor()
        {
            if (!visitorProcess.ReadVisitors())
            {
                MessageBox.Show($"Could'nt Read!\n\"Try deleting {visitorProcess.getFileSource()}\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            visitors = new List<Visitor>(visitorProcess.GetVisitors().Values);
        }

        private void autoSyncVisitorData_Tick(object sender, EventArgs e)
        {
            visitorDataGrid.DataSource = visitors;
        }

        private void importVisitorsB_Click(object sender, EventArgs e)
        {
            commonDialog(RWMode.visitors);
        }

        private void exportVisitorsB_Click(object sender, EventArgs e)
        {

        }
    }
}
