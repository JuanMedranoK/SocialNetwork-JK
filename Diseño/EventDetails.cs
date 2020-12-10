using DataBaseProcedure.Entities;
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
    public partial class EventDetails : Form
    {
        public EventDetails()
        {
            InitializeComponent();
        }
        public bool Edit { get; set; }
        public bool Friend { get; set; }
        public Eventos evento { get; set; }

        private void loadEvento()
        {
            if (!Friend)
            {
                txtHost.Text = UserLogged.Instance.user.Name + " " + UserLogged.Instance.user.Lastname;
                txtName.Text = evento.NAME_EVENT;
                txtDescription.Text = evento.DESCRIPTION_EVENT;
                txtAddress.Text = evento.ADDRESS_EVENT;
                dtpDate.Value = evento.DATE_EVENT;
                getFriendsEvent();
            }
            else
            {
                UsersRepository usersRepository = new UsersRepository();
                DataTable user = usersRepository.Get("SELECT * FROM USERS WHERE ID_USER = " + evento.ID_HOST);
                txtHost.Text = user.Rows[0][1].ToString() + " " + user.Rows[0][2].ToString();
                txtName.Text = evento.NAME_EVENT;
                txtName.ReadOnly = true;
                txtDescription.Text = evento.DESCRIPTION_EVENT;
                txtDescription.ReadOnly = true;
                txtAddress.Text = evento.ADDRESS_EVENT;
                txtAddress.ReadOnly = true;
                dtpDate.Value = evento.DATE_EVENT;
                dtpDate.Enabled = false;
                btnDeleteRow.Visible = false;
                tlpBuscar.Visible = false;
                Friend = true;
                btnSave.Text = "Aceptar";
                getFriendsEvent();
            }
        }
        bool EliminaronColumnas = false;
        private bool ReviewFriends(int ID_User)
        {
            FriendsRepository friendsRepository = new FriendsRepository();
            DataTable FriendDt = friendsRepository.Get("SELECT ID_FRIEND FROM FRIENDS WHERE ID_FRIEND = " + ID_User + " AND ID_USER = " + UserLogged.Instance.user.ID_USER);
            if (FriendDt.Rows.Count != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void getFriendsEvent()
        {
            EventUsersRepository eventUsersRepository = new EventUsersRepository();
            if (!EliminaronColumnas)
            {
                dgvFriends.Columns.Remove(ID_Usuario);
                dgvFriends.Columns.Remove(Name_User);
                EliminaronColumnas = true;
            }
            dgvFriends.DataSource = eventUsersRepository.Get("SELECT U.ID_USER, U.NAME_USER + ' ' + U.LASTNAME_USER AS NOMBRE, CASE WHEN EU.STATUS_EVENT_USER = 0 THEN 'No ha respondido' WHEN EU.STATUS_EVENT_USER = 1 THEN 'Confirmado' WHEN EU.STATUS_EVENT_USER = 2 THEN 'Rechazado' END AS STATUS FROM USERS U INNER JOIN EVENT_USER EU ON U.ID_USER = EU.ID_USER WHERE ID_EVENT = " + evento.ID_EVENT);
        }
        private void addUserEvent(int idUser, bool muchos, int id_evento)
        {
            EventUsersRepository eventUsersRepository = new EventUsersRepository();
            UsuariosEventos user = new UsuariosEventos();
            user.ID_USER = idUser;
            user.STATUS = 0;
            user.ID_EVENT = id_evento;
            if (eventUsersRepository.Add(user))
            {
                if (!muchos)
                {
                    getFriendsEvent();
                }
            }
            else
            {
                MessageBox.Show("No se pudo agregar el amigo");
            }
        }
        private void deleteUserEvent(int idUser)
        {
            EventUsersRepository eventUsersRepository = new EventUsersRepository();
            if (eventUsersRepository.DeleteEventUser(evento.ID_EVENT, idUser))
            {
                getFriendsEvent();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el amigo");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtFriend.Text == "")
            {
                MessageBox.Show("Escriba el nombre del usuario");
            }
            else
            {
                UsersRepository usersRepository = new UsersRepository();
                DataTable friendDt = usersRepository.Get("SELECT ID_USER, NAME_USER + ' ' + LASTNAME_USER AS NOMBRE FROM USERS WHERE USERNAME = '" + txtFriend.Text + "'");
                if (friendDt.Rows.Count == 1)
                {
                    if (ReviewFriends((int)friendDt.Rows[0][0]))
                    {
                        if (Edit)
                        {
                            addUserEvent((int)friendDt.Rows[0][0], false, evento.ID_EVENT);
                            getFriendsEvent();
                            txtFriend.Text = "";
                        }
                        else
                        {
                            dgvFriends.Rows.Add(friendDt.Rows[0][0], friendDt.Rows[0][1]);
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
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (dgvFriends.SelectedRows.Count == 1)
            {
                if (Edit)
                {
                    deleteUserEvent((int)dgvFriends.CurrentRow.Cells[0].Value);
                }
                else
                {
                    dgvFriends.Rows.RemoveAt(dgvFriends.CurrentRow.Index);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EventsRepository eventsRepository = new EventsRepository();
            if (Edit)
            {
                if (Friend)
                {
                    this.Hide();
                }
                else
                {
                    if (todoLleno())
                    {
                        evento.DESCRIPTION_EVENT = txtDescription.Text;
                        evento.ADDRESS_EVENT = txtAddress.Text;
                        evento.DATE_EVENT = dtpDate.Value;
                        evento.NAME_EVENT = txtName.Text;
                        if (eventsRepository.Edit(evento))
                        {
                            MessageBox.Show("Evento editado correctamente");
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo editar el evento");
                        }
                    }
                }
            }
            else
            {
                if (todoLleno())
                {
                    Eventos evento = new Eventos();
                    evento.ID_HOST = UserLogged.Instance.user.ID_USER;
                    evento.DESCRIPTION_EVENT = txtDescription.Text;
                    evento.ADDRESS_EVENT = txtAddress.Text;
                    evento.DATE_EVENT = dtpDate.Value;
                    evento.NAME_EVENT = txtName.Text;
                    if (eventsRepository.Add(evento))
                    {
                        MessageBox.Show("Evento agregado correctamente");
                        EventsRepository eventsRepository1 = new EventsRepository();
                        DataTable dt = eventsRepository1.Get("SELECT MAX(ID_EVENT) FROM EVENT WHERE ID_HOST_USER = " + UserLogged.Instance.user.ID_USER);
                        for (int i = 0; i < dgvFriends.Rows.Count - 1; i++)
                        {
                            addUserEvent((int)dgvFriends.Rows[i].Cells[0].Value, true, (int)dt.Rows[0][0]);
                        }
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el evento");
                    }
                }
            }
        }

        private void EventDetails_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                loadEvento();
            }
            else
            {
                txtHost.Text = UserLogged.Instance.user.Name + " " + UserLogged.Instance.user.Lastname;
            }
        }

        private bool todoLleno()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos");
                return false;
            }
            if (txtDescription.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos");
                return false;
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos");
                return false;
            }
            return true;
        }
    }
}
