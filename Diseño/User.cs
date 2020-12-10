using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Procedures;
using Procedures.Items_Repository;

namespace Diseño
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        public DataTable UserDt { get; set; }
        public EventHandler options;
        private int ID_User { get; set; }

        private void User_Load(object sender, EventArgs e)
        {
            #region UserThings
            loadUser();
            #endregion
            if (panelLoader.Controls.Count > 0)
            {
                panelLoader.Controls.RemoveAt(0);
            }
            PostsRepository postsRepository = new PostsRepository();
            DataTable PostDt = postsRepository.Get("exec GET_POST_USER @id_user = " + ID_User + ", @id_user_logged = " + UserLogged.Instance.user.ID_USER);
            Post post = new Post();
            post.PostDt = PostDt;
            post.Dock = DockStyle.Fill;
            post.TopLevel = false;
            post.user += none;
            panelLoader.Controls.Add(post);
            post.Show();
        }
        private void loadUser()
        {
            pbPortrait.ImageLocation = UserDt.Rows[0][6].ToString();
            pbProfilePic.ImageLocation = UserDt.Rows[0][5].ToString();
            lblUser.Text = UserDt.Rows[0][7].ToString();
            lblName.Text = UserDt.Rows[0][1].ToString() + " " + UserDt.Rows[0][2].ToString();
            ID_User = (int)UserDt.Rows[0][0];
            if (ID_User == UserLogged.Instance.user.ID_USER)
            {
                pbOptions.Visible = true;
            }
        }
        private void none(object sender, EventArgs e)
        {
            loadUser();
        }

        private void pbOptions_Click(object sender, EventArgs e)
        {
            var handler = options;
            if (options != null)
                handler(this, e);
        }
    }
}
