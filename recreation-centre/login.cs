using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend.Auth;
using Backend;

namespace recreation_centre
{

    //This form performs login process
    public partial class Login : Form
    {
        private Authy auth;
        private VisitorProcess visitorProcess;
        private TicketProcess ticketProcess;

        public Login()
        {
            InitializeComponent();
            initializeAuth();
        }

        //Performs Login
        private void loginB_Click(object sender, EventArgs e)
        {
            string userID = userIDTB.Text.Trim();
            string password = passwordTB.Text.Trim();
            if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please, fill the required Fields!", "Error: Empty Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(userID, out int id))
            {
                MessageBox.Show("UserId must be Numeric Integer!", "Error: Improper UserID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Backend.Auth.User user = new Backend.Auth.User()
            {
                UserID = id,
                HashedPassword = password.GetHashCode(),
            };
            if (auth.IsAuthenticated(user))
            {
                user = auth.GetUser(id);
                this.Hide();
                
                switch (user.UserGroup) 
                {
                    case UserGroup.Admin:
                        (new Admin(visitorProcess, ticketProcess)).ShowDialog();
                        break;
                    case UserGroup.CheckinStaff:
                        (new CheckinStaff(visitorProcess, user.UserName)).ShowDialog();
                        break;
                    case UserGroup.CheckoutStaff:
                        (new CheckoutStaff(visitorProcess, ticketProcess, user.UserName)).ShowDialog();
                        break;
                }
                this.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Wrong UserID or Password", "Wrong Credential", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void clearB_Click(object sender, EventArgs e)
        {
            passwordTB.Text = "";
            userIDTB.Text = "";
        }

        //Initializes the Authentication process like loading credential data.
        private void initializeAuth() {
            auth = new Authy();
            if (!auth.ReadCredential()) {
                MessageBox.Show($"Could'nt Read Auth!\n\"Try deleting {auth.getFileSource()}\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                /*return;*/
            }
            visitorProcess = new VisitorProcess();
            ticketProcess = new TicketProcess();
        }

        private void minimizeB_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closeB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Generate User for testing
        private void generateUser() {
            Authy a = new Authy();
            string aa = a.GenerateRecoveryCode(16);
            string bb = a.GenerateRecoveryCode(16);
            string cc = a.GenerateRecoveryCode(16);
            Backend.Auth.User u = new Backend.Auth.User
            {
                UserID = 777777,
                UserName = "Abhi Neupane",
                HashedPassword = "checkout".GetHashCode(),
                UserGroup = UserGroup.CheckoutStaff,
                HashedRecoveryCode = aa.GetHashCode()
            };
            Backend.Auth.User uu = new Backend.Auth.User
            {
                UserID = 888888,
                UserName = "Saroj Singh",
                HashedPassword = "checkin".GetHashCode(),
                UserGroup = UserGroup.CheckinStaff,
                HashedRecoveryCode = bb.GetHashCode()
            };
            Backend.Auth.User uuu = new Backend.Auth.User
            {
                UserID = 999999,
                UserName = "Shivam Chhetri",
                HashedPassword = "admin".GetHashCode(),
                UserGroup = UserGroup.Admin,
                HashedRecoveryCode = cc.GetHashCode()
            };
            a.WriteCredential(u);
            a.WriteCredential(uu);
            a.WriteCredential(uuu);


            foreach (var kv in a.GetCredentials())
            {
                Console.Write(kv.Key + " -> ");
                Console.WriteLine(kv.Value);
            }
        }
    }
}
