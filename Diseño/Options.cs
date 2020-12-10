using Database.Entities;
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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }
        public Publicaciones Postdt { get; set; }
        public EventHandler refresh;
        
        private void OnRefresh(EventArgs e)
        {
            var handler = refresh;
            if(handler != null)
            handler(this, e);
        }
        #region Post
        private void EditPost()
        {
            AddPost post = new AddPost();
            post.Edit = true;
            post.ID_Post = Postdt.ID_Post;
            post.Route = Postdt.Route;
            post.TimeDate = Postdt.Date;
            post.Description = Postdt.Description;
            DialogResult dr = post.ShowDialog();
            if(dr == DialogResult.Yes)
            {

            }
            this.Hide();
        }
        private void DeletePost()
        {
            DialogResult dr = MessageBox.Show("Esta seguro de que desea eliminar esta publicacion?", "Atencion", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Procedures.Items_Repository.PostsRepository postsRepository = new Procedures.Items_Repository.PostsRepository();
                if (postsRepository.Delete(Postdt.ID_Post))
                {
                    MessageBox.Show("La publicacion fue eliminada correctamente");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("La publicacion no pudo ser eliminada");
                }
            }
        }
        #endregion
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditPost();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            DeletePost();
            OnRefresh(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }
    }
}
