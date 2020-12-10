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
    public partial class Friends : Form
    {
        public Friends()
        {
            InitializeComponent();
        }
        public EventHandler user;
        public bool goUser;
        public int ID_User_Go;
        private void getFriends()
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                this.flowLayoutPanel1.Controls.RemoveAt(0);
            }
            UsersRepository usersRepository = new UsersRepository();
            DataTable FriendsDt = usersRepository.Get("SELECT * FROM USERS U INNER JOIN FRIENDS F ON F.ID_FRIEND = U.ID_USER WHERE F.ID_USER = " + UserLogged.Instance.user.ID_USER);
            FriendsControl[] friends = new FriendsControl[FriendsDt.Rows.Count];
            for (int i = 0; i < FriendsDt.Rows.Count; i++)
            {
                FriendsControl control = new FriendsControl();
                Usuarios user = new Usuarios();
                user.ID_USER = Convert.ToInt32(FriendsDt.Rows[i].ItemArray[0].ToString());
                user.Name = FriendsDt.Rows[i].ItemArray[1].ToString();
                user.Lastname = FriendsDt.Rows[i].ItemArray[2].ToString();
                user.Mail = FriendsDt.Rows[i].ItemArray[3].ToString();
                user.Password = FriendsDt.Rows[i].ItemArray[4].ToString();
                user.ProfilePicture = FriendsDt.Rows[i].ItemArray[5].ToString();
                user.Portrait = FriendsDt.Rows[i].ItemArray[6].ToString();
                user.UserName = FriendsDt.Rows[i].ItemArray[7].ToString();
                control.user = user;
                control.gotoUser += OnGotoUser;
                friends[i] = control;
                flowLayoutPanel1.Controls.Add(friends[i]);
            }
        }
        private void OnGotoUser(object sender, EventArgs e)
        {
            foreach (var userControl in flowLayoutPanel1.Controls.OfType<FriendsControl>())
            {

                if (userControl.goUser)
                {
                    goUser = true;
                    ID_User_Go = userControl.ID_Friend;
                }
            }
            var handler = user;
            if (user != null)
                handler(this, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                UsersRepository usersRepository = new UsersRepository();
                DataTable friendDt = usersRepository.Get("SELECT ID_USER FROM USERS WHERE USERNAME = '" + richTextBox1.Text + "'");
                if (friendDt.Rows.Count == 1)
                {
                    if (!ReviewFriends((int)friendDt.Rows[0][0]))
                    {
                        if (!ReviewRequest((int)friendDt.Rows[0][0]))
                        {
                            if (!ReviewRequested((int)friendDt.Rows[0][0]))
                            {
                                if ((int)friendDt.Rows[0][0] != UserLogged.Instance.user.ID_USER)
                                {
                                    SendRequest((int)friendDt.Rows[0][0]);
                                    if (flowLayoutPanel1.Controls.Count > 0)
                                    {
                                        this.flowLayoutPanel1.Controls.RemoveAt(0);
                                    }
                                    getFriends();
                                }
                                else
                                {
                                    MessageBox.Show("No te puedes enviar una solicitud de amistad a ti mismo");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Este usuario ya te mando una solicitud");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe esperar a que el usuario acepte su solicitud");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este usuario y tu ya son amigos");
                    }
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado");
                }
            }
        }

        private bool ReviewFriends(int ID_User)
        {
            FriendsRepository friendsRepository = new FriendsRepository();
            DataTable FriendDt = friendsRepository.Get("SELECT ID_FRIEND FROM FRIENDS WHERE ID_FRIEND = "+ID_User+" AND ID_USER = "+ UserLogged.Instance.user.ID_USER);
            if(FriendDt.Rows.Count != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ReviewRequested(int ID_User)
        {
            FriendRequestsRepository friendRequestsRepository = new FriendRequestsRepository();
            DataTable RequestDt = friendRequestsRepository.Get("SELECT ID_USER_REQUESTED FROM REQUEST WHERE ID_USER_REQUEST = "+ID_User+" AND ID_USER_REQUESTED = "+UserLogged.Instance.user.ID_USER);
            if (RequestDt.Rows.Count != 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private bool ReviewRequest(int ID_User)
        {
            FriendRequestsRepository friendRequestsRepository = new FriendRequestsRepository();
            DataTable RequestDt = friendRequestsRepository.Get("SELECT ID_USER_REQUESTED FROM REQUEST WHERE ID_USER_REQUEST = " + UserLogged.Instance.user.ID_USER + " AND ID_USER_REQUESTED = " + ID_User);
            if (RequestDt.Rows.Count != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void SendRequest(int ID_User)
        {
            FriendRequestsRepository friendRequestsRepository = new FriendRequestsRepository();
            Solicitudes_Amistad solicitudes_Amistad = new Solicitudes_Amistad();
            solicitudes_Amistad.ID_Friend = ID_User;
            solicitudes_Amistad.ID_User = UserLogged.Instance.user.ID_USER;
            solicitudes_Amistad.Request_Date = System.DateTime.UtcNow.ToShortDateString().ToString() + " "
        + System.DateTime.Now.ToShortTimeString().ToString();
            if (!friendRequestsRepository.Add(solicitudes_Amistad))
            {
                MessageBox.Show("La solicitud no pudo ser enviada");
            }
            else
            {
                richTextBox1.Text = "";
                MessageBox.Show("Solicitud Enviada");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FriendsRequests friendsRequests = new FriendsRequests();
            friendsRequests.ShowDialog();
            if(flowLayoutPanel1.Controls.Count > 0) 
            {
                flowLayoutPanel1.Controls.RemoveAt(0);
            }
            richTextBox1.Text = "";
            getFriends();
        }

        private void Friends_Load(object sender, EventArgs e)
        {
            getFriends();
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                UsersRepository usersRepository = new UsersRepository();
                DataTable friendDt = usersRepository.Get("SELECT ID_USER FROM USERS WHERE USERNAME = '" + richTextBox1.Text + "'");
                if (friendDt.Rows.Count == 1)
                {
                    if (ReviewFriends((int)friendDt.Rows[0][0]))
                    {
                        FriendsRepository friendsRepository = new FriendsRepository();
                        if (friendsRepository.DeleteFriendship(UserLogged.Instance.user.ID_USER, (int)friendDt.Rows[0][0]))
                        {
                            if (friendsRepository.DeleteFriendship((int)friendDt.Rows[0][0], UserLogged.Instance.user.ID_USER))
                            {

                                MessageBox.Show("Usuario eliminado correctamente");
                                this.flowLayoutPanel1.Controls.RemoveAt(0);
                                getFriends();
                                richTextBox1.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("La accion fallo exitosamente");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el amigo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este usuario no es su amigo");
                    }
                }
                else
                {
                    MessageBox.Show("Este usuario no existe");
                }
            }
            else
            {
                MessageBox.Show("Debe de ingresar el nombre de usuario");
            }
        }
    }
}
