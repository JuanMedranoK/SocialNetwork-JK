using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Procedures;
using System.Runtime.CompilerServices;
using Database.Entities;
using Procedures.Items_Repository;

namespace Diseño
{
    public partial class PostControl : UserControl
    {
        public PostControl()
        {
            InitializeComponent();
        }
        
        CommentsRepository commentServices = new CommentsRepository();
        private DataTable CommentsDt { get; set; }
        private DataTable AnswersDt { get; set; }
        private int SelectedComment { get; set; }
        private bool CommentSelected { get; set; }
        private string User_Answered { get; set; }

        protected virtual void OnRefresh(EventArgs e, int Case)
        {
            switch (Case)
            {
                case 1:
                    var handler = options;
                    if (handler != null)
                        handler(this, e);
                    break;
                case 2:
                    var handler2 = user;
                    if (handler2 != null)
                        handler2(this, e);
                    break;
            }
        }
        protected virtual void gotoUser(EventArgs e)
        {
            var handler = user;
            if (user != null)
            handler(this, e);
        }

        #region Public Properties

        public event EventHandler options;
        public event EventHandler user;
        public bool goUser { get; set; }


        [Category("Post Information")]
        public int ID_Post { get; set; }

        [Category("Post Information")]
        public string Route_Post { get; set; }

        [Category("Post Information")]
        public int ID_User_Post { get; set; }

        [Category("Post Information")]
        public string Name_User { get; set; }

        [Category("Post Information")]
        public int Likes_Post { get; set; }

        [Category("Post Information")]
        public int Dislikes_Post { get; set; }

        [Category("Post Information")]
        public string Date_Post { get; set; }

        [Category("Post Information")]
        public string Route_User { get; set; }

        [Category("Post Information")]
        public string Caption_Post { get; set; }
        [Category("Post Information")]
        public bool Liked { get; set; }
        [Category("Post Information")]
        public bool Disliked { get; set; }
        [Category("Comments")]
        public DataTable Comments { get; set; }

        #endregion 


        private void PostControl_Load(object sender, EventArgs e)
        {
            PostImage.ImageLocation = Route_Post;
            pbUserProfile.ImageLocation = Route_User;
            NameUser.Text = Name_User;
            TimeDate.Text = Date_Post;
            lbLikesCount.Text = Likes_Post.ToString();
            lbDislikesCount.Text = Dislikes_Post.ToString();
            if (Caption_Post != null)
            {
                Caption.Text = Caption_Post;
            }
            else
            {
                Caption.Visible = false;
            }
            if (Liked)
            {
                pbLikes.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (Disliked)
            {
                pbDislikes.BorderStyle = BorderStyle.Fixed3D;
            }
            GetComments();
            if(CommentsDt.Rows.Count > 0)
            LoadComments(CommentsDt.Rows.Count);
        }
        #region Get Comments and Answers
        private void GetAnswers(int ID_Comment) 
        {
            AnswersDt = commentServices.Get("SELECT C.ID_COMMENT, C.ID_POST_COMMENT, C.ID_USER_COMMENT, C.COMMENT_TYPE, ISNULL(C.ID_COMMENT_ANSWERED,0) AS ANSWER_OF,"
            + " C.COMMENT, C.TIMEDATE, U.NAME_USER + ' ' +U.LASTNAME_USER, U.PROFILE_PICTURE_USER, C.NAME_USER_ANSWERED"
            + " FROM COMMENTS C INNER JOIN USERS U ON C.ID_USER_COMMENT = U.ID_USER WHERE ID_POST_COMMENT = " + ID_Post + " AND COMMENT_TYPE = 1 AND C.ID_COMMENT_ANSWERED =" + ID_Comment);
        }
        private void GetComments()
        {
            CommentsDt = commentServices.Get("SELECT C.ID_COMMENT, C.ID_POST_COMMENT, C.ID_USER_COMMENT, C.COMMENT_TYPE, ISNULL(C.ID_COMMENT_ANSWERED,0) AS ANSWER_OF,"
            + " C.COMMENT, C.TIMEDATE, U.NAME_USER + ' ' +U.LASTNAME_USER, U.PROFILE_PICTURE_USER"
            + " FROM COMMENTS C INNER JOIN USERS U ON C.ID_USER_COMMENT = U.ID_USER WHERE ID_POST_COMMENT = " + ID_Post + " AND COMMENT_TYPE = 0");
        }
        private void LoadComments(int CommentIndex)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls.Clear();
            }
            CommentsControl[] control = new CommentsControl[CommentIndex];
            for (int i = 0; i < control.Length; i++)
            {
                CommentsControl commentsControl = new CommentsControl();
                commentsControl.ID_Comment = (int)CommentsDt.Rows[i][0];
                commentsControl.ID_Post = (int)CommentsDt.Rows[i][1];
                commentsControl.UserID = (int)CommentsDt.Rows[i][2];
                commentsControl.Respuesta = false;
                commentsControl.Comment = CommentsDt.Rows[i][5].ToString();
                commentsControl.DateComment = CommentsDt.Rows[i][6].ToString();
                commentsControl.UserName = CommentsDt.Rows[i][7].ToString();
                commentsControl.UserPp = CommentsDt.Rows[i][8].ToString();
                commentsControl.getComment += commentSelected; //PARA OBTENER ID DEL COMENTARIO SELECCIONADO
                control[i] = commentsControl;
                flowLayoutPanel1.Controls.Add(control[i]);
                GetAnswers((int)CommentsDt.Rows[i][0]);
                LoadAnswers(AnswersDt.Rows.Count);
            }
        }
        private void LoadAnswers(int AnswersIndex)
        {
            CommentsControl[] control = new CommentsControl[AnswersIndex];
            for (int i = 0; i < control.Length; i++)
            {
                CommentsControl commentsControl = new CommentsControl();
                commentsControl.ID_Comment = (int)AnswersDt.Rows[i][0];
                commentsControl.ID_Post = (int)AnswersDt.Rows[i][1];
                commentsControl.UserID = (int)AnswersDt.Rows[i][2];
                commentsControl.Respuesta = true;
                commentsControl.ID_Comment_Answered = (int)AnswersDt.Rows[i][4];
                commentsControl.Comment = AnswersDt.Rows[i][5].ToString();
                commentsControl.DateComment = AnswersDt.Rows[i][6].ToString();
                commentsControl.UserName = AnswersDt.Rows[i][7].ToString();
                commentsControl.UserPp = AnswersDt.Rows[i][8].ToString();
                commentsControl.User_Answered = AnswersDt.Rows[i][9].ToString();
                commentsControl.getComment += commentSelected; //PARA OBTENER ID DEL COMENTARIO SELECCIONADO
                control[i] = commentsControl;
                flowLayoutPanel1.Controls.Add(control[i]);
            }
        }
        #endregion

        #region Add Likes, Dislikes, Comments

        private void AddComment(bool commentSelected)
        {
            if (!commentSelected)
            {
                Database.Entities.Comentarios comentarios = new Database.Entities.Comentarios();
                comentarios.Comment = txtComment.Text;
                comentarios.ID_Post = ID_Post;
                comentarios.ID_User = UserLogged.Instance.user.ID_USER;
                comentarios.Type_Comment = 0; //ES UN COMENTARIO
                comentarios.TimeDate = DateTime.UtcNow.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                comentarios.User_Answered = "";
                Procedures.Items_Repository.CommentsRepository commentsRepository = new Procedures.Items_Repository.CommentsRepository();

                if (commentsRepository.Add(comentarios))
                {
                    GetComments();
                    LoadComments(CommentsDt.Rows.Count);
                    txtComment.Text = "";
                }
                else
                {
                    MessageBox.Show("El comentario no pudo ser agregado");
                }
            }
            else
            {
                Database.Entities.Comentarios comentarios = new Database.Entities.Comentarios();
                comentarios.Comment = txtComment.Text;
                comentarios.ID_Post = ID_Post;
                comentarios.ID_User = UserLogged.Instance.user.ID_USER;
                comentarios.Type_Comment = 1; //ES UNA RESPUESTA DE COMENTARIO
                comentarios.TimeDate = DateTime.UtcNow.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                comentarios.User_Answered = User_Answered;
                comentarios.Comment_Answered = SelectedComment;
                Procedures.Items_Repository.CommentsRepository commentsRepository = new Procedures.Items_Repository.CommentsRepository();

                if (commentsRepository.Add(comentarios))
                {
                    GetComments();
                    LoadComments(CommentsDt.Rows.Count);
                    txtComment.Text = "";
                }
                else
                {
                    MessageBox.Show("La respuesta no pudo ser agregada");
                }
            }
        }
        private void RefreshCount() //LIKES AND DISLIKES
        {
            string query = "SELECT P.ID_POST AS ID, COUNT(L.ID_USER_LIKE) AS COUNT_LIKES,"
            + " COUNT(DL.ID_USER_DISLIKE) AS COUNT_DISLIKES FROM POST P"
            + " LEFT JOIN LIKES L ON P.ID_POST = L.ID_POST_LIKE"
            + " LEFT JOIN DISLIKES DL ON P.ID_POST = DL.ID_POST_DISLIKE"
            + " WHERE P.ID_POST = "+ID_Post+" GROUP BY P.ID_POST";
            LikesRepository likes = new LikesRepository();
            DataTable LikesDt = likes.Get(query);
            lbLikesCount.Text = LikesDt.Rows[0][1].ToString();
            lbDislikesCount.Text = LikesDt.Rows[0][2].ToString();

        }
        private void Like(bool liked)
        {
            if (liked)
            {
                addLike();
            }
            else
            {
                deleteLike();
            }
        } 
        private void addLike()
        {
            pbLikes.BorderStyle = BorderStyle.Fixed3D;
            Procedures.Items_Repository.LikesRepository likesRepository = new Procedures.Items_Repository.LikesRepository();
            Likes likes = new Likes();
            likes.ID_POST = ID_Post;
            likes.ID_USER = UserLogged.Instance.user.ID_USER;
            if (!likesRepository.Add(likes))
            {
                pbLikes.BorderStyle = BorderStyle.None;
            }
            if(pbDislikes.BorderStyle == BorderStyle.Fixed3D)
            {
                deleteDislike();
            }
        }
        private void deleteLike()
        {
            pbLikes.BorderStyle = BorderStyle.None;
            Procedures.Items_Repository.LikesRepository likesRepository = new Procedures.Items_Repository.LikesRepository();
            Likes likes = new Likes();
            likes.ID_POST = ID_Post;
            likes.ID_USER = UserLogged.Instance.user.ID_USER; 
            if (!likesRepository.DeleteLike(likes))
            {
                pbLikes.BorderStyle = BorderStyle.None;
            }
        }
        private void Dislike(bool disliked)
        {
            if (disliked)
            {
                addDislike();
            }
            else
            {
                deleteDislike();
            }
        }
        private void addDislike()
        {
            pbDislikes.BorderStyle = BorderStyle.Fixed3D;
            Procedures.Items_Repository.DislikesRepository dislikesRepository = new Procedures.Items_Repository.DislikesRepository();
            Dislikes dislikes = new Dislikes();
            dislikes.ID_POST = ID_Post;
            dislikes.ID_USER = UserLogged.Instance.user.ID_USER; 
            if (!dislikesRepository.Add(dislikes))
            {
                pbDislikes.BorderStyle = BorderStyle.None;
            }
            if (pbLikes.BorderStyle == BorderStyle.Fixed3D)
            {
                deleteLike();
            }

        }
        private void deleteDislike()
        {
            pbDislikes.BorderStyle = BorderStyle.None;
            Procedures.Items_Repository.DislikesRepository dislikesRepository = new Procedures.Items_Repository.DislikesRepository();
            Dislikes dislikes = new Dislikes();
            dislikes.ID_POST = ID_Post;
            dislikes.ID_USER = UserLogged.Instance.user.ID_USER; 
            if (!dislikesRepository.DeleteLike(dislikes))
            {
                pbDislikes.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        #endregion

        #region Clicks Events
        private void pbMore_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            Publicaciones post = new Publicaciones();
            post.ID_Post = ID_Post;
            post.Route = Route_Post;
            post.UserId = UserLogged.Instance.user.ID_USER;
            post.Description = Caption_Post;
            post.Date = Date_Post;
            options.Postdt = post;
            options.refresh += refresh_Post;
            options.ShowDialog();
            OnRefresh(e,1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddComment(CommentSelected);
            CommentSelected = false;

        }
        private void pbUserProfile_Click(object sender, EventArgs e)
        {
            goUser = true;
            gotoUser(e);
        }

        private void NameUser_Click(object sender, EventArgs e)
        {
            goUser = true;
            gotoUser(e);
        }

        private void pbLikes_Click(object sender, EventArgs e)
        {
            if(pbLikes.BorderStyle == BorderStyle.None)
            {
                pbLikes.BorderStyle = BorderStyle.Fixed3D;
                Like(true);
            }
            else
            {
                pbLikes.BorderStyle = BorderStyle.None;
                Like(false);
            }
            RefreshCount();
        }

        private void pbDislikes_Click(object sender, EventArgs e)
        {
            if (pbDislikes.BorderStyle == BorderStyle.None)
            {
                pbDislikes.BorderStyle = BorderStyle.Fixed3D;
                Dislike(true);
            }
            else
            {
                pbDislikes.BorderStyle = BorderStyle.None;
                Dislike(false);
            }
            RefreshCount();
        }

        #endregion
        #region HandlerEvents Comments
        private void commentSelected(object sender, EventArgs e)
        {
            foreach (var userControl in flowLayoutPanel1.Controls.OfType<CommentsControl>())
            {
                if (userControl.Selecciono)
                {
                    if (userControl.Respuesta)
                    {
                        SelectedComment = userControl.ID_Comment_Answered;
                        User_Answered = userControl.UserName;
                        CommentSelected = true;
                        break;
                    }
                    else
                    {
                        SelectedComment = userControl.ID_Comment;
                        User_Answered = userControl.UserName;
                        CommentSelected = true;
                        break;
                    }
                }
            }
        }
        #endregion
        #region HandlerEvents Options
        private void refresh_Post(object sender, EventArgs e)
        {
            OnRefresh(e, 0);
        }
        #endregion
    }
}
