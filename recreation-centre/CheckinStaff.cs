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
    public partial class CheckinStaff : Form
    {

        private readonly VisitorProcess visitorProcess;

        public CheckinStaff(VisitorProcess visitorProcess, String loginUser)
        {
            InitializeComponent();
            this.visitorProcess = visitorProcess;
            loginUserTB.Text = loginUser;
        }

        private void syncCurTimeB_Click(object sender, EventArgs e)
        {
            syncDateTime();
        }

        private void allclearB_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void checkoutB_Click(object sender, EventArgs e)
        {
            String name = nameTB.Text.Trim();
            String phone = phoneMTB.Text.Trim();
            String age = ageMTB.Text.Trim();
            String numChild = childMTB.Text.Trim();
            String numYAdult = youngAdultMTB.Text.Trim();
            String numMAdult = middleAdultMTB.Text.Trim();
            String numOAdult = oldAdultMTB.Text.Trim();
            DateTime inTime = inDateTimeDTP.Value;
            String day = dayMTB.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(age) || (string.IsNullOrWhiteSpace(numChild)
                && string.IsNullOrWhiteSpace(numYAdult) && string.IsNullOrWhiteSpace(numMAdult) && string.IsNullOrWhiteSpace(numOAdult)))
            {
                MessageBox.Show("Please, don't leave required Field Empty!", "Error: Empty Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (phone.Length != 10) {
                MessageBox.Show("Please, enter Valid PhoneNumber!", "Error: Invalid PhoneNumber)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (inTime.AddMinutes(5) < DateTime.Now)
            {
                MessageBox.Show("DateTime must be Recent!", "Error: Old DateTime", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var ageGroup = new Dictionary<AgeGroupE, short>() {
                { AgeGroupE.CHILD, short.Parse(string.IsNullOrWhiteSpace(numChild) ? "0" : numChild) },
                { AgeGroupE.YOUNG_ADULT, short.Parse(string.IsNullOrWhiteSpace(numYAdult) ? "0" : numYAdult) },
                { AgeGroupE.MIDDLE_ADULT, short.Parse(string.IsNullOrWhiteSpace(numMAdult) ? "0" : numMAdult) },
                { AgeGroupE.OLD_ADULT, short.Parse(string.IsNullOrWhiteSpace(numOAdult) ? "0" : numOAdult) },
            };
            Visitor v = new Visitor()
            {
                TicketCode = visitorProcess.GenerateID(),
                Name = name,
                Phone = phone,
                Age = short.Parse(age),
                GroupOf = ageGroup,
                Day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day),
                InTime = inTime,
                OutTime = null,
                Bill = null,
            };
            if (MessageBox.Show($"Check-in {name.Split(' ')[0]}?\n\"Make sure the Check-in details are correct!\"\n\n[ Visitor Ticket Code: {v.TicketCode} ]",
                "Check-in Confirm",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (!visitorProcess.WriteVisitor(v))
                { 
                    MessageBox.Show("Could'nt Write!", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                clearFields();
            }            
        }

        private void minimizeB_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void logoutB_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Run(new Login());
                this.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void calenderDate_click(object sender, EventArgs e) 
        {
            dayMTB.Text = inDateTimeDTP.Value.DayOfWeek.ToString();
        }

        private void syncDateTime()
        {
            DateTime now = DateTime.Now;
            inDateTimeDTP.Value = now;
            dayMTB.Text = now.DayOfWeek.ToString();
        }

        private void clearFields()
        {
            nameTB.Text = "";
            phoneMTB.Text = "";
            ageMTB.Text = "";
            childMTB.Text = "";
            youngAdultMTB.Text = "";
            middleAdultMTB.Text = "";
            oldAdultMTB.Text = "";
            syncDateTime();
        }

        private void closeB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initializeVisitors()
        {
            if (visitorProcess.ReadVisitors())
            {
                MessageBox.Show($"Could'nt Read Visitors!\n\"Try deleting {visitorProcess.getFileSource()}\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            inDateTimeDTP.Format = DateTimePickerFormat.Custom;
            inDateTimeDTP.CustomFormat = "dd/MM/yyyy, hh:mm tt ";
            dayMTB.Text = DateTime.Now.DayOfWeek.ToString();
        }
    }
}
