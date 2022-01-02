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
    public partial class CheckoutStaff : Form
    {

        private readonly VisitorProcess visitorProcess;
        private readonly TicketProcess ticketProcess;
        private Visitor visitor;

        public CheckoutStaff(VisitorProcess visitorProcess, TicketProcess ticketProcess, String loginUser)
        {
            InitializeComponent();
            this.visitorProcess = visitorProcess;
            this.ticketProcess = ticketProcess;
            loginUserTB.Text = loginUser;
        }

        private void closeB_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void calculateB_Click(object sender, EventArgs e)
        {
            String ticketCode = vistorTicketCodeMTB.Text;
            if (string.IsNullOrWhiteSpace(ticketCode)) {
                MessageBox.Show("Please, enter a Visitor Ticket Id!", "Error: Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int visitorTicketCode = int.Parse(ticketCode);
            if (!visitorProcess.HasVisitor(visitorTicketCode)) 
            {
                MessageBox.Show("Given User Ticet ID not Found!", "Error: Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            visitor = visitorProcess.GetVisitor(visitorTicketCode);
            if (visitor.Bill.HasValue)
            {
                MessageBox.Show("Given User Ticket ID was already Checked-out!", "Error: Checked-out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            visitor.Bill = ticketProcess.GenerateBill(visitor);
            String bill = $@"

                
                ";
            billTBM.Text = bill;
            nameTB.Text = visitor.Name;
            childTB.Text = visitor.GroupOf[AgeGroupE.CHILD].ToString();
            youngAdultTB.Text = visitor.GroupOf[AgeGroupE.YOUNG_ADULT].ToString();
            middleAdultTB.Text = visitor.GroupOf[AgeGroupE.MIDDLE_ADULT].ToString();
            oldAdultTB.Text = visitor.GroupOf[AgeGroupE.OLD_ADULT].ToString();
            durationTB.Text = (visitor.InTime - visitor.OutTime).ToString();
            durationFromTB.Text = visitor.InTime.ToString();
            durationToTB.Text = visitor.OutTime.ToString();
            dayTB.Text = visitor.Day.ToString();
        }

        private void checkoutB_Click(object sender, EventArgs e)
        {
            if (visitor == null)
            {
                MessageBox.Show("Please, Calculate Bill before you Check-out!", "Error: No Bill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            visitorProcess.WriteVisitor(visitor);
            clearFields();
        }

        private void allclearB_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void userTicketIDMTB_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            clearFields();
        }

        private void clearFields()
        {
            vistorTicketCodeMTB.Text = "";
            billTBM.Text = "";
            nameTB.Text = "";
            childTB.Text = "";
            youngAdultTB.Text = "";
            middleAdultTB.Text = "";
            oldAdultTB.Text = "";
            durationTB.Text = "";
            durationFromTB.Text = "";
            durationToTB.Text = "";
            dayTB.Text = "";
            visitor = null;
        }
    }
}
