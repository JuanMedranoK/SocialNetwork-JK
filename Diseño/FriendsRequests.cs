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
    public partial class FriendsRequests : Form
    {
        public FriendsRequests()
        {
            InitializeComponent();
        }
        private void getRequests()
        {
            FriendRequestsRepository friendRequestsRepository = new FriendRequestsRepository();
            DataTable RequestDt = friendRequestsRepository.Get("SELECT U.ID_USER, U.NAME_USER + ' ' + U.LASTNAME_USER AS NOMBRE, U.USERNAME FROM USERS U INNER JOIN REQUEST R ON U.ID_USER = R.ID_USER_REQUEST WHERE R.ID_USER_REQUESTED = " + UserLogged.Instance.user.ID_USER);
            dataGridView1.DataSource = RequestDt;
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                FriendRequestsRepository friendRequestsRepository = new FriendRequestsRepository();
                FriendsRepository friendsRepository = new FriendsRepository();
                if (friendRequestsRepository.DeleteRequest((int)dataGridView1.CurrentRow.Cells[0].Value, UserLogged.Instance.user.ID_USER))
                {
                    Amigos amigos = new Amigos();

                    amigos.ID_User = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    amigos.ID_Friend = UserLogged.Instance.user.ID_USER;
                    amigos.Friendship_Date = System.DateTime.UtcNow.ToShortDateString().ToString() + " "
                + System.DateTime.Now.ToShortTimeString().ToString();
                    if (friendsRepository.Add(amigos))
                    {
                        amigos.ID_Friend = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        amigos.ID_User = UserLogged.Instance.user.ID_USER;
                        amigos.Friendship_Date = System.DateTime.UtcNow.ToShortDateString().ToString() + " "
                        + System.DateTime.Now.ToShortTimeString().ToString();
                        if (!friendsRepository.Add(amigos))
                        {
                            MessageBox.Show("No se pudo aceptar la solicitud");
                        }
                        getRequests();

                    }
                    else
                    {
                        MessageBox.Show("No se pudo aceptar la solicitud");
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("No se pudo aceptar la solicitud");
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila");
            }
        }

        private void FriendsRequests_Load(object sender, EventArgs e)
        {
            getRequests();
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            FriendRequestsRepository friendRequestsRepository = new FriendRequestsRepository();
            if (friendRequestsRepository.DeleteRequest((int)dataGridView1.CurrentRow.Cells[0].Value, UserLogged.Instance.user.ID_USER))
            {
                getRequests();
            }
            else
            {
                MessageBox.Show("No se pudo rechazar la solicitud");
            }
        }
    }
}
