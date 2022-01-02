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

        private void loginB_Click(object sender, EventArgs e)
        {
            string userID = userIDTB.Text.Trim();
            string password = passwordTB.Text.Trim();
            if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please, fill the required Fields!", "Error: Empty Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (int.TryParse(userID, out int id))
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
                
                switch (user.UserGroup) 
                {
                    case UserGroup.Admin:
                        Application.Run(new Admin(visitorProcess, ticketProcess));
                        break;
                    case UserGroup.CheckinStaff:
                        Application.Run(new CheckinStaff(visitorProcess, user.UserName));
                        break;
                    case UserGroup.CheckoutStaff:
                        Application.Run(new CheckoutStaff(visitorProcess, ticketProcess, user.UserName));
                        break;
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong UserID or Password", "Wrong Credential", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clearB_Click(object sender, EventArgs e)
        {
            passwordTB.Text = "";
            userIDTB.Text = "";
        }

        private void initializeAuth() {
            auth = new Authy();
            if (!auth.ReadCredential()) {
                MessageBox.Show($"Could'nt Read Auth!\n\"Try deleting {auth.getFileSource()}\"", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            visitorProcess = new VisitorProcess();
            ticketProcess = new TicketProcess();
        }
    }
}
