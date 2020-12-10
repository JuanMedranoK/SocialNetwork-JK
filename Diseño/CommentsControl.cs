using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diseño
{
    public partial class CommentsControl : UserControl
    {
        public CommentsControl()
        {
            InitializeComponent();
        }

        [Category ("Comments Prop")]
        public int ID_Post { get; set; }
        public int ID_Comment { get; set; }
        public string Comment { get; set; }
        public string DateComment { get; set;}
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPp { get; set; } //Pp means profile picture
        public int ID_Comment_Answered { get; set; }
        public bool Respuesta { get; set; }
        public string User_Answered { get; set; }

        public event EventHandler getComment;

        public bool Selecciono;
       

        private void CommentsControl_Load(object sender, EventArgs e)
        {
            txtComment.Text = Comment;
            lbDate.Text = DateComment;
            lbUserName.Text = UserName;
            pictureBox1.ImageLocation = UserPp;
            Selecciono = false;
            if (Respuesta)
            {
                this.Margin = new System.Windows.Forms.Padding(60,0,0,0);
                lbUserAnswer.Visible = true;
                lbUserAnswer.Text += User_Answered;
            }
        }
        int click = 0;
        private void label1_Click(object sender, EventArgs e)
        {
            if (click == 0)
            {
                label1.BorderStyle = BorderStyle.Fixed3D;
                Selecciono = true;
                OnGetComment(e);
                click++;
            }
            else
            {
                label1.BorderStyle = BorderStyle.None;
                Selecciono = false;
                OnGetComment(e);
                click--;
            }
        }
        
        protected virtual void OnGetComment(EventArgs e)
        {
            var handler = getComment;
            if (handler != null)
                handler(this, e);

        }
    }
}
