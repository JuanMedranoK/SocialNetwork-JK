using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database.Entities;

namespace Diseño
{
    public partial class FriendsControl : UserControl
    {
        public bool goUser { get; set; }
        public int ID_Friend { get; set; }
        public FriendsControl()
        {
            InitializeComponent();
        }
        public Usuarios user { get; set; }
        public EventHandler gotoUser;
        private void FriendsControl_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = user.ProfilePicture;
            label1.Text = user.Name + " "+ user.Lastname;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            goUser = true;
            ID_Friend = user.ID_USER;
            OngotoUsers(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            goUser = true;
            ID_Friend = user.ID_USER;
            OngotoUsers(e);
        }
        private void OngotoUsers(EventArgs e)
        {
            var handler = gotoUser;
            if (gotoUser != null)
                handler(this, e);
        }
    }
}
