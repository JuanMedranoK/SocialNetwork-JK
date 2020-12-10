using Procedures;
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
    public partial class AddPost : Form
    {
        public AddPost()
        {
            InitializeComponent();
        }

        #region PUBLIC PROPERTIES
        public bool Edit { get; set; }
        public string Route { get; set; }
        public string Description { get; set; }
        public string TimeDate { get; set; }
        public int ID_Post { get; set; }
        public EventHandler refresh;
        #endregion

        private void LoadPost()
        {
            pbPost.ImageLocation = Route;
            txtCaption.Text = Description;
        }

        private void OnRefresh(EventArgs e)
        {
            var handler = refresh;
            if (refresh != null)
                handler(this, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Edit)
            {
                if (edit())
                {
                    OnRefresh(e);
                }
                else
                {
                    MessageBox.Show("La publicacion no pudo ser editada correctamente");
                }
                
            }
            else
            {
                if (save())
                {
                    OnRefresh(e);
                }
                else
                {
                    MessageBox.Show("La publicacion no pudo ser guardada correctamente");
                }
            }
        }
        private bool save()
        {
            Database.Entities.Publicaciones publicaciones = new Database.Entities.Publicaciones();
            publicaciones.Route = pbPost.ImageLocation;
            publicaciones.Description = txtCaption.Text;
            publicaciones.UserId = UserLogged.Instance.user.ID_USER; 
            publicaciones.Date = DateTime.UtcNow.ToShortDateString() + " " +DateTime.Now.ToShortTimeString();
            Procedures.Items_Repository.PostsRepository postsRepository = new Procedures.Items_Repository.PostsRepository();
            return postsRepository.Add(publicaciones);
        }
        private bool edit()
        {
            Database.Entities.Publicaciones publicaciones = new Database.Entities.Publicaciones();
            publicaciones.Route = pbPost.ImageLocation;
            publicaciones.Description = txtCaption.Text;
            publicaciones.UserId = UserLogged.Instance.user.ID_USER;
            publicaciones.Date = DateTime.UtcNow.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            publicaciones.ID_Post = ID_Post;
            Procedures.Items_Repository.PostsRepository postsRepository = new Procedures.Items_Repository.PostsRepository();
            return postsRepository.Edit(publicaciones);
        }

        private void AddPost_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                LoadPost();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pbPost_Click(object sender, EventArgs e)
        {
            setRoute();
        }
        private void setRoute()
        {
            DialogResult dr = Photo.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string photo = Photo.FileName;
                pbPost.ImageLocation = photo;
            }
        }
    }
}
