using Database.Entities;
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
    public partial class SingIn : Form
    {
        public SingIn()
        {
            InitializeComponent();
        }
        public Usuarios usuarios { get; set;}
        private void setProfilePic(bool portrait)
        {
            if (portrait)
            {
                DialogResult dr = Photo.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    usuarios.Portrait = Photo.FileName;
                    pictureBox1.ImageLocation = usuarios.Portrait;
                }
            }
            else
            {
                DialogResult dr = Photo.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    usuarios.ProfilePicture = Photo.FileName;
                    pictureBox2.ImageLocation = usuarios.ProfilePicture;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            setProfilePic(true);
        }

        private void btnNC_Click(object sender, EventArgs e)
        {
            if(pictureBox1.ImageLocation != "" | pictureBox2.ImageLocation != "")
            {
                if(txtPass.Text != "" | txtPassconfirm.Text != "" )
                {
                    if (txtPass.Text == txtPass.Text)
                    {
                        if (txtName.Text != "" | txtLastName.Text != "" | txtMail.Text != "" | txtUsername.Text != "")
                        {
                            UsersRepository usersRepository = new UsersRepository();
                            DataTable dt = usersRepository.Get("SELECT * FROM USERS WHERE USERNAME = '"+txtUsername.Text+"'");
                            if (dt.Rows.Count == 0)
                            {
                                Usuarios user = new Usuarios();
                                user.Name = txtName.Text;
                                user.Lastname = txtLastName.Text;
                                user.Mail = txtMail.Text;
                                user.UserName = txtUsername.Text;
                                user.Password = txtPass.Text;
                                user.ProfilePicture = pictureBox2.ImageLocation;
                                user.Portrait = pictureBox1.ImageLocation;
                                if (usersRepository.Add(user))
                                {
                                    MessageBox.Show("Usuario agregado correctamente");
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("El usuario no se pudo agregar");
                                }
                            }
                            else
                            {
                                MessageBox.Show("El nombre de usuario no esta disponible");
                            }
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            setProfilePic(false);
        }

        private void SingIn_Load(object sender, EventArgs e)
        {
            usuarios = new Usuarios();
        }
    }
}
