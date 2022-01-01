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
            DateTime inTime = intimeDTP.Value;
            String day = dayMTB.Text;
            if (name == "" || phone == "" || age == "" || numChild == "" 
                || numYAdult == "" || numMAdult == "" || numOAdult == "")
            {
                MessageBox.Show("Please, don't leave any Field Empty!", "Error: Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var ageGroup = new Dictionary<AgeGroupE, short>() {
                { AgeGroupE.CHILD, short.Parse(numChild) },
                { AgeGroupE.YOUNG_ADULT, short.Parse(numYAdult) },
                { AgeGroupE.MIDDLE_ADULT, short.Parse(numMAdult) },
                { AgeGroupE.OLD_ADULT, short.Parse(numOAdult) },
            };
            Visitor v = new Visitor()
            {
                TicketCode = visitorProcess.GenerateID(10),
                Name = name,
                Phone = phone,
                Age = short.Parse(age),
                GroupOf = ageGroup,
                Day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day),
                InTime = inTime,
                OutTime = null,
                Bill = null,
            };
            if (MessageBox.Show($"Check-in {name.Split(' ')[0]}?\n\"Make sure the Check-in details are correct!\"\n\n[Visitor Ticket Code: {v.TicketCode}]",
                "Check-in Confirm",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                visitorProcess.WriteVisitors(v);
                clearFields();
            }            
        }

        private void syncDateTime()
        {
            DateTime now = DateTime.Now;
            intimeDTP.Value = now;
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
    }
}
