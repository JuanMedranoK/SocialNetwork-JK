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
    public partial class Post : Form
    {
        public DataTable PostDt { get; set; }
        public Post()
        {
            InitializeComponent();
        }
        public int ID_User_Go { get; set; }
        public bool goUser { get; set; }
        public event EventHandler user;
        private void loadPost(int PostIndex)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls.Clear();
            }
            PostControl[] control = new PostControl[PostIndex];
            for (int i = 0; i < control.Length; i++)
            {
                PostControl postControl = new PostControl();
                postControl.ID_Post = (int) PostDt.Rows[i][0];
                postControl.Route_Post = PostDt.Rows[i][1].ToString();
                postControl.ID_User_Post =(int) PostDt.Rows[i][2];
                postControl.Name_User = PostDt.Rows[i][3].ToString();
                postControl.Date_Post = PostDt.Rows[i][4].ToString();
                postControl.Route_User = PostDt.Rows[i][5].ToString();
                postControl.Caption_Post = PostDt.Rows[i][6].ToString();
                postControl.Likes_Post = (int)PostDt.Rows[i][7];
                postControl.Dislikes_Post = (int)PostDt.Rows[i][8];
                if ((int)PostDt.Rows[i][9] == 0)
                {
                    postControl.Liked = false;
                }
                else
                {
                    postControl.Liked = true;
                }
                if ((int)PostDt.Rows[i][10] == 0)
                {
                    postControl.Disliked = false;
                }
                else
                {
                    postControl.Disliked = true;
                }
                postControl.user += gouser;
                postControl.options += refresh;
                if((int)PostDt.Rows[i][2] == UserLogged.Instance.user.ID_USER)
                {
                    postControl.pbMore.Visible = true;
                }
                control[i] = postControl;
                flowLayoutPanel1.Controls.Add(control[i]);
            }
        }

        private void Post_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.AutoScroll = true;
            loadPost(PostDt.Rows.Count);
        }

        private void refresh(object sender, EventArgs e)
        {
            PostsRepository postsRepository = new PostsRepository();
            PostDt = postsRepository.Get("exec GET_POST_USER @id_user = " + UserLogged.Instance.user.ID_USER + ", @id_user_logged = " + UserLogged.Instance.user.ID_USER);
            loadPost(PostDt.Rows.Count);
        }
        private void gouser(object sender, EventArgs e)
        {
            foreach (var userControl in Controls.OfType<PostControl>())
            {
                ID_User_Go = userControl.ID_User_Post;
                goUser = true;
            }
            var handler = user;
            if (user != null)
                handler(this, e);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
