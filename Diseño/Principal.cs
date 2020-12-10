using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database.Entities;
using Procedures;
using Procedures.Items_Repository;

namespace Diseño
{
    public partial class Principal : Form
    {

        public Principal()
        {
            InitializeComponent();
        }
        bool Maximized;

        private void Event()
        {
            if (this.FormsLoader.Controls.Count > 0)
            {
                this.FormsLoader.Controls.RemoveAt(0);
            }
            Events events = new Events();
            events.Dock = DockStyle.Fill;
            events.TopLevel = false;
            FormsLoader.Controls.Add(events);
            events.Show();
        }
        private void Home()
        {
            PostsRepository postsRepository = new PostsRepository();
            Post post = new Post();
            if (this.FormsLoader.Controls.Count > 0)
            {
                this.FormsLoader.Controls.RemoveAt(0);
            }
            DataTable Postdt = postsRepository.Get("exec GET_POST_PRINCIPAL @id_user = " + UserLogged.Instance.user.ID_USER);
            post.PostDt = Postdt;
            post.user += gotoUsers;
            post.Dock = DockStyle.Fill;
            post.TopLevel = false;
            FormsLoader.Controls.Add(post);
            post.Show();
        }
        private void User(int id_user)
        {

            if (this.FormsLoader.Controls.Count > 0)
            {
                this.FormsLoader.Controls.RemoveAt(0);
            }
            UsersRepository usersRepository = new UsersRepository();
            User user = new User();
            user.UserDt = usersRepository.Get("SELECT * FROM USERS WHERE ID_USER = " + id_user);
            user.options += btnSettings_Click;
            user.Dock = DockStyle.Fill;
            user.TopLevel = false;
            FormsLoader.Controls.Add(user);
            user.Show();
        }
        private void Settings()
        {
            if (this.FormsLoader.Controls.Count > 0)
            {
                this.FormsLoader.Controls.RemoveAt(0);
            }
            EditUser editUser = new EditUser();
            editUser.user = UserLogged.Instance.user;
            editUser.back += backHome;
            editUser.Dock = DockStyle.Fill;
            editUser.TopLevel = false;
            FormsLoader.Controls.Add(editUser);
            editUser.Show();

        }
        private void Friend()
        {
            if (this.FormsLoader.Controls.Count > 0)
            {
                this.FormsLoader.Controls.RemoveAt(0);
            }
            Friends friends = new Friends();
            friends.user += gotoUsersFriend;
            friends.Dock = DockStyle.Fill;
            friends.TopLevel = false;
            FormsLoader.Controls.Add(friends);
            friends.Show();
        }
        private void Add()
        {
            if (this.FormsLoader.Controls.Count > 0)
            {
                this.FormsLoader.Controls.RemoveAt(0);
            }
            AddPost post = new AddPost();
            post.refresh += backHome;
            post.Dock = DockStyle.Fill;
            post.TopLevel = false;
            FormsLoader.Controls.Add(post);
            post.Show();

        }

        private void LogOut()
        {
            UserLogged.Instance.logOut();
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        #region EnventsHandler
        private void backHome(object sender, EventArgs e)
        {
            Home();
        }
        private void gotoUsersFriend(object sender, EventArgs e)
        {
            foreach (var userControl in FormsLoader.Controls.OfType<Friends>())
            {
                
                if (userControl.goUser)
                {
                    User user = new User();
                    UsersRepository usersRepository = new UsersRepository();
                    DataTable userDt = usersRepository.Get("SELECT * FROM USERS WHERE ID_USER = " + userControl.ID_User_Go);
                    user.UserDt = userDt;
                    this.FormsLoader.Controls.RemoveAt(0);
                    user.TopLevel = false;
                    user.Dock = DockStyle.Fill;;
                    FormsLoader.Controls.Add(user);
                    user.Show();
                }
            }
        }
        private void gotoUsers(object sender, EventArgs e)
        {
            foreach (var userControl in FormsLoader.Controls.OfType<PostControl>())
            {
                User user = new User();
                UsersRepository usersRepository = new UsersRepository();
                if (userControl.goUser)
                {
                    DataTable userDt = usersRepository.Get("SELECT * FROM USERS WHERE ID_USER = " + userControl.ID_User_Post);
                    user.UserDt = userDt;
                    this.FormsLoader.Controls.RemoveAt(0);
                    FormsLoader.Controls.Add(user);
                    user.Show();
                }
            }
        }
        #endregion
        private void btnHome_Click(object sender, EventArgs e)
        {
            Home();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Home();

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Settings();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            User(UserLogged.Instance.user.ID_USER);
        }

        private void btnFriends_Click(object sender, EventArgs e)
        {
            Friend();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LogOut();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            if (Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Maximized = false;
            }
            else
            {

                this.WindowState = FormWindowState.Maximized;
                Maximized = true;
            }
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            Event();
        }
    }
}
