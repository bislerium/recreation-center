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
            initialize();
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
                this.Hide();
                var v = new Login();
                v.FormClosed += (s, args) => this.Close();
                v.ShowDialog();
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
            if (checkoutDateTime.Value <= DateTime.Now)
            {
                MessageBox.Show("DateTime must be Recent!", "Error: Old DateTime", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            visitor = visitorProcess.GetVisitor(visitorTicketCode);
            if (visitor.Bill.HasValue)
            {
                MessageBox.Show("Given User Ticket ID was already Checked-out!", "Error: Checked-out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            visitor.OutTime = checkoutDateTime.Value;
            Bill bill = ticketProcess.GenerateBill(visitor);
            visitor.Bill = bill;

            bPrice.Text = bill.InitialPrice.ToString();
            cRE.Text = bill.C_AgeRate.ToString();
            cRG.Text = bill.C_AgeRating.ToString();
            cRP.Text = bill.AfterCAR.ToString();
            yRE.Text = bill.Y_AgeRate.ToString();
            yRG.Text = bill.Y_AgeRating.ToString();
            yRP.Text = bill.AfterYAR.ToString();
            mRE.Text = bill.M_AgeRate.ToString();
            mRG.Text = bill.M_AgeRating.ToString();
            mRP.Text = bill.AfterMAR.ToString();
            oRE.Text = bill.O_AgeRate.ToString();
            oRG.Text = bill.O_AgeRating.ToString();
            oRP.Text = bill.AfterOAR.ToString();
            aGP.Text = bill.TotalAgeGroupPrice.ToString();
            gRE.Text = bill.GroupRate.ToString();
            gRG.Text = bill.GroupRating.ToString();
            gRP.Text = bill.AfterGR.ToString();
            dRE.Text = bill.DurationRate.ToString();
            dRG.Text = bill.DurationRating.ToString();
            dRP.Text = bill.AfterDNR.ToString();
            dYR.Text = bill.DayRate.ToString();
            dYG.Text = bill.DayRating.ToString();
            dYP.Text = bill.AfterDYR.ToString();
            tBill.Text = bill.FinalPrice.ToString();

            nameTB.Text = visitor.Name;
            childTB.Text = visitor.GroupOf[AgeGroupE.CHILD].ToString();
            youngAdultTB.Text = visitor.GroupOf[AgeGroupE.YOUNG_ADULT].ToString();
            middleAdultTB.Text = visitor.GroupOf[AgeGroupE.MIDDLE_ADULT].ToString();
            oldAdultTB.Text = visitor.GroupOf[AgeGroupE.OLD_ADULT].ToString();
            TimeSpan ts = ((TimeSpan)(visitor.OutTime - visitor.InTime));
            durationTB.Text = $"{ts.Hours}:{ts.Minutes}:{ts.Seconds}";
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
            loadVisitors();
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
            nameTB.Text = "";
            childTB.Text = "";
            youngAdultTB.Text = "";
            middleAdultTB.Text = "";
            oldAdultTB.Text = "";
            durationTB.Text = "";
            durationFromTB.Text = "";
            durationToTB.Text = "";
            dayTB.Text = "";
            bPrice.Text = "";
            cRE.Text = "";
            cRG.Text = "";
            cRP.Text = "";
            yRE.Text = "";
            yRG.Text = "";
            yRP.Text = "";
            mRE.Text = "";
            mRG.Text = "";
            mRP.Text = "";
            oRE.Text = "";
            oRG.Text = "";
            oRP.Text = "";
            aGP.Text = "";
            gRE.Text = "";
            gRG.Text = "";
            gRP.Text = "";
            dRE.Text = "";
            dRG.Text = "";
            dRP.Text = "";
            dYR.Text = "";
            dYG.Text = "";
            dYP.Text = "";
            tBill.Text = "";
            visitor = null;

        }

        private void syncDateTime_Click(object sender, EventArgs e)
        {
            checkoutDateTime.Value = DateTime.Now;
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

        private void initialize()
        {
            checkoutDateTime.Format = DateTimePickerFormat.Custom;
            checkoutDateTime.CustomFormat = "dd/MM/yy, hh:mm tt ";
            if (!visitorProcess.ReadVisitors())
            {
                MessageBox.Show($"Could'nt Read Visitors!\n\"New {visitorProcess.getFileSource()} will be Created\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!ticketProcess.ReadTicket())
            {
                MessageBox.Show($"Could'nt Read Ticket!\n\"New {ticketProcess.getFileSource()} will be Created\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadVisitors();
        }



        private void loadVisitors()
        {
            checkedOutDataGrid.DataSource = visitorProcess.GetVisitors()
                                                          .Values
                                                          .Where(x => x.Bill.HasValue)
                                                          .ToArray();
                                                          
        }

        private void syncDataGrid_Click(object sender, EventArgs e)
        {
            loadVisitors();
        }
    }
}
