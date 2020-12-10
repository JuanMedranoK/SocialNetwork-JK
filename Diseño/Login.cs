using Procedures;
using Procedures.Items_Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diseño
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "" && txtPassword.Text != "")
            {
                UsersRepository usersRepository = new UsersRepository();
                DataTable UserDt = usersRepository.Get("SELECT * FROM USERS WHERE USERNAME = '" + txtUser.Text + "' AND PASSWORD_USER = '" + txtPassword.Text + "'");
                if(UserDt.Rows.Count == 1)
                {
                    UserLogged.Instance.logIn(UserDt);
                    Principal principal = new Principal();
                    principal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Datos incorrectos");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecoverAccount recover = new RecoverAccount();
            recover.ShowDialog();
        }

        private void btnNC_Click(object sender, EventArgs e)
        {
            SingIn singIn = new SingIn();
            singIn.ShowDialog();
        }
    }
}
