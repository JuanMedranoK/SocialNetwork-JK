using Database.Entities;
using Procedures.Items_Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diseño
{
    public partial class RecoverAccount : Form
    {
        public RecoverAccount()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Aceptar")
            {
                this.Hide();
            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Debe ingresar un usuario");
                }
                else
                {
                    getUser(textBox1.Text);
                }
            }
            
        }
        private void getUser(string username)
        {
            UsersRepository usersRepository = new UsersRepository();
            Usuarios user = new Usuarios();
            DataTable userdt = usersRepository.Get("SELECT * FROM USERS WHERE USERNAME = '"+username+"'");
            if(userdt.Rows.Count > 0)
            {
                user.ID_USER = (int)userdt.Rows[0][0];
                user.Name = userdt.Rows[0][1].ToString();
                user.Lastname = userdt.Rows[0][2].ToString();
                user.Mail = userdt.Rows[0][3].ToString();
                user.Password = createPassword(userdt.Rows[0][4].ToString());
                user.ProfilePicture = userdt.Rows[0][5].ToString();
                user.Portrait = userdt.Rows[0][6].ToString();
                user.UserName = userdt.Rows[0][7].ToString();
                if (usersRepository.Edit(user))
                {
                    sendMail(user.Mail, user.Password);
                    button1.Text = "Aceptar";
                    label2.Text = "Se le ha enviado un mensaje al correo electronico con el que creo la cuenta con su nueva contraseña";
                    textBox1.Visible = false;
                }
                else
                {
                    button1.Text = "Aceptar";
                    label2.Text = "No podemos recuperar su cuenta en estos momentos";
                    textBox1.Visible = false;
                }
                
            }
            else
            {
                MessageBox.Show("El usuario ingresado no existe");
            }
        }

        private string createPassword(string lastPassword)
        {
            char[] l = new char[lastPassword.Length];
            int index = 0;
            using (StringReader sr = new StringReader(lastPassword))
            {
                sr.Read(l, 0, lastPassword.Length);
            }
            index = lastPassword.Length;
            lastPassword = "";            
            for(int i = index; i > 0; i--)
            {
                lastPassword += l[i - 1];
            }
            return lastPassword;
        }
        private void sendMail(string mail_user, string password)
        {
            Mail.EmailSender emailSender = new Mail.EmailSender();
            string message = "Su nueva contraseña es: "+ password;
            emailSender.SendEmail(mail_user, "Recuperacion de cuenta", message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
