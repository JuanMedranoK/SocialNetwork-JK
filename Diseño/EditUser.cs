using Database.Entities;
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
    public partial class EditUser : Form
    {
        public EditUser()
        {
            InitializeComponent();
        }
        public Usuarios user { get; set; }
        public EventHandler back;
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (pictureBox1.ImageLocation != "" | pictureBox2.ImageLocation != "")
            {
                if (txtPass.Text != "" | txtPassconfirm.Text != "")
                {
                    if (txtPass.Text == txtPass.Text)
                    {
                        if (txtName.Text != "" | txtLastName.Text != "" | txtMail.Text != "" | txtUsername.Text != "")
                        {
                            UsersRepository usersRepository = new UsersRepository();
                            
                                Usuarios user = new Usuarios();
                                user.ID_USER = this.user.ID_USER;
                                user.Name = txtName.Text;
                                user.Lastname = txtLastName.Text;
                                user.Mail = txtMail.Text;
                                user.UserName = txtUsername.Text;
                                user.Password = txtPass.Text;
                                user.ProfilePicture = pictureBox2.ImageLocation;
                                user.Portrait = pictureBox1.ImageLocation;
                                usersRepository.Edit(user);
                                MessageBox.Show("Informacion modificada correctamente");
                                UserLogged.Instance.logOut();
                                UserLogged.Instance.logIn(usersRepository.Get("SELECT * FROM USERS WHERE ID_USER = " + user.ID_USER));
                        }
                        else
                        {
                            MessageBox.Show("Debe llenar todos los campos");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                    }
                }
                else
                {
                    MessageBox.Show("Debe llenar todos los campos");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
        }

        private void EditUser_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = user.Portrait;
            pictureBox2.ImageLocation = user.ProfilePicture;
            txtName.Text = user.Name;
            txtLastName.Text = user.Lastname;
            txtMail.Text = user.Mail;
            txtUsername.Text = user.UserName;
            txtUsername.Enabled = false;
            txtPass.Text = user.Password;
            txtPassconfirm.Text = user.Password;
        }
        private void OnBack(EventArgs e)
        {
            var handler = back;
            if (back != null)
                handler(this, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            setProfilePic(true);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            setProfilePic(false);
        }
        private void setProfilePic(bool portrait)
        {
            if (portrait)
            {
                DialogResult dr = Photo.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    user.Portrait = Photo.FileName;
                    pictureBox1.ImageLocation = user.Portrait;
                }
            }
            else
            {
                DialogResult dr = Photo.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    user.ProfilePicture = Photo.FileName;
                    pictureBox2.ImageLocation = user.ProfilePicture;
                }
            }
        }
    }
}
