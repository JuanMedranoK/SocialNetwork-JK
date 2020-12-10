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
    public partial class Events : Form
    {
        public Events()
        {
            InitializeComponent();
        }

        private void Events_Load(object sender, EventArgs e)
        {
            getEvents();
            getMyEvents();
        }
        private bool status { get; set; }
        private void getEvents()
        {
            string query = "SELECT E.ID_EVENT, E.NAME_EVENT, E.DATE_EVENT, E.ADDRESS, U.NAME_USER + ' ' + U.LASTNAME_USER AS NAME_USER, EU.STATUS_EVENT_USER"
            + " FROM EVENT E INNER JOIN USERS U ON E.ID_HOST_USER = U.ID_USER LEFT JOIN EVENT_USER EU ON E.ID_EVENT = EU.ID_EVENT WHERE EU.ID_USER = " + UserLogged.Instance.user.ID_USER;
            EventsRepository eventsRepository = new EventsRepository();
            dgvEvents.DataSource = eventsRepository.Get(query);
        }
        private void getMyEvents()
        {
            string query = "SELECT E.ID_EVENT, E.NAME_EVENT, E.DATE_EVENT, E.ADDRESS, COUNT(EU.ID_USER) AS 'CANTIDAD DE INVITADOS'"
            +" FROM EVENT E INNER JOIN USERS U ON E.ID_HOST_USER = U.ID_USER LEFT JOIN EVENT_USER EU ON E.ID_EVENT = EU.ID_EVENT WHERE E.ID_HOST_USER = " + UserLogged.Instance.user.ID_USER
            + " GROUP BY E.ID_EVENT, E.NAME_EVENT, E.DATE_EVENT, E.ADDRESS, E.DATE_EVENT";
            EventsRepository eventsRepository = new EventsRepository();
            dataGridView2.DataSource = eventsRepository.Get(query);
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            
            if(dgvEvents.SelectedRows.Count == 1)
            {
                EventUsersRepository eventUsersRepository = new EventUsersRepository();
                UsuariosEventos usuariosEventos = new UsuariosEventos();
                usuariosEventos.ID_EVENT = (int)dgvEvents.CurrentRow.Cells[0].Value;
                usuariosEventos.ID_USER = UserLogged.Instance.user.ID_USER;
                if (status)
                {
                    usuariosEventos.STATUS = 2;
                    if (eventUsersRepository.Edit(usuariosEventos))
                    {
                        lbStatus.Text = "Rechazado";
                        btnAceptar.Text = "Aceptar invitacion";
                        status = false;
                        getEvents();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo cambiar su estado");
                    }
                }
                else
                {
                    usuariosEventos.STATUS = 1;
                    if (eventUsersRepository.Edit(usuariosEventos))
                    {
                        lbStatus.Text = "Confirmado";
                        btnAceptar.Text = "Rechazar invitacion";
                        status = true;
                        getEvents();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo cambiar su estado");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila");
            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 1) 
            {
                EventsRepository eventsRepository = new EventsRepository();
                DataTable dt = eventsRepository.Get("SELECT * FROM EVENT WHERE ID_EVENT = " + (int)dgvEvents.CurrentRow.Cells[0].Value);
                Eventos eventos = new Eventos();
                eventos.ID_EVENT = (int)dt.Rows[0][0];
                eventos.ID_HOST = (int)dt.Rows[0][1];
                eventos.NAME_EVENT = dt.Rows[0][2].ToString();
                eventos.DESCRIPTION_EVENT = dt.Rows[0][3].ToString();
                eventos.DATE_EVENT = Convert.ToDateTime(dt.Rows[0][4]);
                eventos.ADDRESS_EVENT = dt.Rows[0][5].ToString();
                EventDetails eventDetails = new EventDetails();
                eventDetails.Edit = true;
                eventDetails.evento = eventos;
                eventDetails.Friend = true;
                eventDetails.ShowDialog();
                getEvents();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila");
            }

        }

        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAceptar.Enabled = true;
            if ((int)dgvEvents.CurrentRow.Cells[5].Value == 2)
            {
                lbStatus.Text = "Rechazado";
                btnAceptar.Text = "Aceptar invitacion";
            }
            else if((int)dgvEvents.CurrentRow.Cells[5].Value == 1)
            {
                lbStatus.Text = "Confirmado";
                btnAceptar.Text = "Rechazar invitacion";
            }
            else
            {
                lbStatus.Text = "No confirmado";
                btnAceptar.Text = "Aceptar invitacion";
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            EventDetails eventDetails = new EventDetails();
            eventDetails.Edit = false;
            eventDetails.ShowDialog();
            getMyEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count == 1)
            {
                if(Convert.ToDateTime(dataGridView2.CurrentRow.Cells[2].Value) < System.DateTime.Now)
                {
                    EventsRepository eventsRepository = new EventsRepository();
                    DataTable dt = eventsRepository.Get("SELECT * FROM EVENT WHERE ID_EVENT = " + (int)dataGridView2.CurrentRow.Cells[0].Value);
                    Eventos eventos = new Eventos();
                    eventos.ID_EVENT = (int)dt.Rows[0][0];
                    eventos.ID_HOST = (int)dt.Rows[0][1];
                    eventos.NAME_EVENT = dt.Rows[0][2].ToString();
                    eventos.DESCRIPTION_EVENT = dt.Rows[0][3].ToString();
                    eventos.DATE_EVENT = Convert.ToDateTime(dt.Rows[0][4]);
                    eventos.ADDRESS_EVENT = dt.Rows[0][5].ToString();
                    EventDetails eventDetails = new EventDetails();
                    eventDetails.Edit = true;
                    eventDetails.evento = eventos;
                    eventDetails.Friend = true;
                    eventDetails.ShowDialog();
                    getMyEvents();
                }
                else
                {
                    EventsRepository eventsRepository = new EventsRepository();
                    DataTable dt = eventsRepository.Get("SELECT * FROM EVENT WHERE ID_EVENT = " + (int)dataGridView2.CurrentRow.Cells[0].Value);
                    Eventos eventos = new Eventos();
                    eventos.ID_EVENT = (int)dt.Rows[0][0];
                    eventos.ID_HOST = (int)dt.Rows[0][1];
                    eventos.NAME_EVENT = dt.Rows[0][2].ToString();
                    eventos.DESCRIPTION_EVENT = dt.Rows[0][3].ToString();
                    eventos.DATE_EVENT = Convert.ToDateTime(dt.Rows[0][4]);
                    eventos.ADDRESS_EVENT = dt.Rows[0][5].ToString();
                    EventDetails eventDetails = new EventDetails();
                    eventDetails.Edit = true;
                    eventDetails.evento = eventos;
                    eventDetails.Friend = false;
                    eventDetails.ShowDialog();
                    getMyEvents();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                EventsRepository eventsRepository = new EventsRepository();
                if (eventsRepository.Delete((int)dataGridView2.CurrentRow.Cells[0].Value))
                {
                    getMyEvents();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el evento");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        private void dgvEvents_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((int)dgvEvents.CurrentRow.Cells[5].Value == 2)
            {
                lbStatus.Text = "Rechazado";
                btnAceptar.Text = "Aceptar invitacion";
            }
            else if ((int)dgvEvents.CurrentRow.Cells[5].Value == 1)
            {
                lbStatus.Text = "Confirmado";
                btnAceptar.Text = "Rechazar invitacion";
            }
            else
            {
                lbStatus.Text = "No confirmado";
                btnAceptar.Text = "Aceptar invitacion";
            }
        }
    }
}
