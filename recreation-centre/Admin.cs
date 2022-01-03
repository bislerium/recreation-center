using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend;

namespace recreation_centre
{
    //This form is used by admin for performing tasks like ticket creation and report analysis
    public partial class Admin : Form
    {
        private VisitorProcess visitorProcess;
        private TicketProcess ticketProcess;
        private Ticket ticket;
        private Dictionary<int, Visitor> visitors;

        public Admin(VisitorProcess visitorProcess, TicketProcess ticketProcess)
        {
            InitializeComponent();
            this.visitorProcess = visitorProcess;
            this.ticketProcess = ticketProcess;
            initializeTicket(true);
            initializeVisitors();
        }

        //Disposes this frame and unhides Login frame.
        private void logoutB_Click(object sender, EventArgs e)
        {
            try {
                this.Hide();
                var v = new Login();
                v.FormClosed += (s, args) => this.Close();
                v.ShowDialog();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }           
        }

        private void closeB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeB_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        // Used to make borderless frame moovable.
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

        //Checks Empty field per given string values.
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

        //Checks Whether the textbox argument contains decimal values or not.
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
            if (!short.TryParse(peopleCount, out short shortPeopleCount) || shortPeopleCount == 0) { 
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
            if (!ticket.Group.ContainsKey(id) || id == 0)
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
            if (!short.TryParse(hourDuration, out short shortHourDuration) || shortHourDuration == 0)
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
            if (!ticket.Duration.ContainsKey(id) || id == 0) 
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

        //Renders price rate per duration in corresponding textbox.
        private void hourDurationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            durationRateTB.Text=ticket.Duration[short.Parse(hourDurationCB.Text)].ToString();
        }

        //loads all the applicable duration from Dictionary and feeds to combobox
        private void loadDurationCB()
        {
            hourDurationCB.Items.Clear();
            hourDurationCB.Text = "";
            durationRateTB.Text = "";
            foreach (var key in ticket.Duration.Keys)
            {
                if (key == 0) continue;
                hourDurationCB.Items.Add(key.ToString());
            }
        }

        //loads all the applicable Group from Dictionary and feeds to combobox
        private void loadGroupCB()
        {
            peopleCountCB.Items.Clear();
            peopleCountCB.Text = "";
            groupRateTB.Text = "";
            foreach (var key in ticket.Group.Keys)
            {
                if (key == 0) continue;
                peopleCountCB.Items.Add(key.ToString());
            }
        }

        //Sets foreground and background color for eaach wrong fields that are subclasses of Control Class.
        private void setDefaultFieldBackColor(params Control[] controlList)
        {
            foreach (var control in controlList) 
            {
                control.ForeColor = Color.Black;
                control.BackColor = Color.White;
            }
        }

        //Renders the price rate per people count/group in a textbox.
        private void peopleCountCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupRateTB.Text = ticket.Group[short.Parse(peopleCountCB.Text)].ToString();
        }

        //Saves/Persists ticket locally.
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
        private void initializeTicket(bool isNewTicket = false) 
        {
            if (!ticketProcess.ReadTicket())
            {
                MessageBox.Show($"Could'nt Read Ticket : {ticketProcess.getFileSource()}", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (!isNewTicket) return;
                MessageBox.Show($"New Ticket:{ticketProcess.getFileSource()} Can be created!", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            commonExportDialog(RWMode.ticket);
        }

        private void importTicketB_Click(object sender, EventArgs e)
        {
            commonImportDialog(RWMode.ticket);
        }

        //For supporting filechooser Dialog window.
        enum RWMode
        { 
            visitors,
            ticket
        }

        //Renders a filechooser Dialog window for importing vistiors or ticket data.
        private void commonImportDialog(RWMode mode)
        {
            OpenFileDialog importDialogBox = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "json file (*.json)|*.json",
                ReadOnlyChecked = true,
                ShowReadOnly = true,
                Title = (mode == RWMode.ticket) ? "Import Ticket" : "Import Visitors",
            };
            if (importDialogBox.ShowDialog() == DialogResult.OK)
            {
                if (mode == RWMode.ticket)
                {
                    ticketProcess = new TicketProcess(importDialogBox.FileName);
                    initializeTicket();
                }
                else
                {
                    visitorProcess = new VisitorProcess(importDialogBox.FileName);
                    initializeVisitors();
                }                
            }
        }

        //Renders a filechooser Dialog window for exporting vistiors or ticket data.
        private void commonExportDialog(RWMode mode)
        {
            SaveFileDialog exportDialogBox = new SaveFileDialog
            {
                Filter = "json file (*.json)|*.json",
                InitialDirectory = @"C:\",
                Title = mode == RWMode.ticket ? "Export Ticket" : "Export Visitors"
            };

            if (exportDialogBox.ShowDialog() == DialogResult.OK)
            {
                bool success;
                if (mode == RWMode.ticket)
                {
                    TicketProcess tp = new TicketProcess(exportDialogBox.FileName, ticket);
                    success = tp.WriteTicket();
                }
                else
                {
;                   VisitorProcess vp = new VisitorProcess(exportDialogBox.FileName, visitors);
                    success = vp.WriteVisitors();
                }
                if (!success)
                {
                    MessageBox.Show("Could'nt Write!", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Sync the Visitor data table with update changes.
        private void syncDataGrid_Click(object sender, EventArgs e)
        {
            visitorDataGrid.DataSource = visitors.Values.ToArray();
        }

        //Initialize VisitorProcess instance evertime this class instantiates and visitors data imported.
        private void initializeVisitors()
        {
            if (!visitorProcess.ReadVisitors())
            {
                MessageBox.Show($"Could'nt Read Visitors!\n\"Try deleting {visitorProcess.getFileSource()}\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            visitors = visitorProcess.GetVisitors();
            visitorDataGrid.DataSource = visitors.Values.ToArray();
            foreach (var v in visitors.Values)
            {
                try
                {
                    Console.WriteLine(v.Bill);
                    Console.WriteLine(v.Bill.HasValue);
                    Console.WriteLine(v.Bill.Value.DurationRating);
                    Console.WriteLine(v.Bill.Value.FinalPrice);
                } catch (Exception ex) { }

            }
        }

        private void importVisitorsB_Click(object sender, EventArgs e)
        {
            commonImportDialog(RWMode.visitors);
        }

        private void exportVisitorsB_Click(object sender, EventArgs e)
        {
            commonExportDialog(RWMode.visitors);
        }

        //Generates Daily Report
        private void generateDailyReport()
        {
            var currList = visitors.Values.ToList().Where(x => x.Bill.HasValue && x.InTime.Date.Equals(DateTime.Now.Date));
            var groupByGroup = currList.GroupBy(x => x.Bill?.GroupRate)
                                      .Select(g => new { visitorGroupOf = g.Key,
                                          Total_Visitors = g.Count()})
                                      .ToList();
/*            var groupByAge = new List<dynamic>(currList.Select(g => g.GroupOf
                                                    .Select(x => new {
                                                                    ageGroup = x.Key,
                                                                    groupCount = x.Value})).ToList())
                                                    .GroupBy(x => x.ageGroup)
                                                    .Select(g => new
                                                    {
                                                        ageGroup = g.Key,
                                                        total_age = g.Sum(x => x.groupCount)
                                                    })
                                                    .ToList();*/
            dailyReportDataGrid.DataSource = groupByGroup;
        }

        // Generates Weekly Repory as per the Sorting parameters: visitor, earning
        private void generateWeeklyReport(bool sortByVisitor = true)
        {
            DateTime date = DateTime.Now;
            int year = date.Date.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DayOfWeek day = date.DayOfWeek;
            CultureInfo cul = CultureInfo.CurrentCulture;
            int weekNo = cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int days = (weekNo - 1) * 7;
            DateTime dt1 = firstDay.AddDays(days);
            DayOfWeek dow = dt1.DayOfWeek;
            DateTime startDateOfWeek = dt1.AddDays(-(int)dow);
            DateTime endDateOfWeek = startDateOfWeek.AddDays(6);

            DateTime current = DateTime.Now;
            var weeklyList = visitors.Values.Where(x => startDateOfWeek <= DateTime.Parse(x.InTime.ToLongDateString()) && endDateOfWeek >= DateTime.Parse(x.InTime.ToLongDateString()))
                                            .Where(x => x.Bill.HasValue)
                                            .GroupBy(x => DateTime.Parse(x.InTime.ToLongDateString()).DayOfWeek)
                                            .Select(grp => new
                                            {
                                                Day = grp.Key,
                                                Total_Visitors = grp.Sum(sum => sum.GroupOf.Sum(s => s.Value)),
                                                Total_Earnings = grp.Sum(sum => sum.BillPrice)
                                            })
                                            .ToList();

            int max = weeklyList.Count() - 1;

            if (sortByVisitor)
            {
                //Implementation of Bubble Sort
                for (int i = 0; i < max; i++)
                {
                    int nrLeft = max - i;
                    for (int j = 0; j < nrLeft; j++)
                    {
                        if (weeklyList[j + 1] == null)
                        {
                            break;
                        }
                        else if (weeklyList[j].Total_Visitors > weeklyList[j + 1].Total_Visitors)
                        {
                            var temp = weeklyList[j];
                            weeklyList[j] = weeklyList[j + 1];
                            weeklyList[j + 1] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < max; i++)
                {
                    int nrLeft = max - i;
                    for (int j = 0; j < nrLeft; j++)
                    {
                        if (weeklyList[j + 1] == null)
                        {
                            break;
                        }
                        else if (weeklyList[j].Total_Earnings > weeklyList[j + 1].Total_Earnings)
                        {
                            var temp = weeklyList[j];
                            weeklyList[j] = weeklyList[j + 1];
                            weeklyList[j + 1] = temp;
                        }
                    }
                }
            }
            var sortedWeeklyList = weeklyList.ToList();
                sortedWeeklyList.Reverse();
            weeklyReportDataGrid.DataSource = sortedWeeklyList;
        }

        private void dailyReportB_Click(object sender, EventArgs e)
        {
            generateDailyReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            generateWeeklyReport();
            sortWeeklyReportCB.SelectedIndex = 0;
        }

        private void sortWeeklyReportCB_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (sortWeeklyReportCB.SelectedIndex==0)
            {
                generateWeeklyReport(true);
            }
            if (sortWeeklyReportCB.SelectedIndex==1)
            {
                generateWeeklyReport(false);
            }
        }
    }
}
