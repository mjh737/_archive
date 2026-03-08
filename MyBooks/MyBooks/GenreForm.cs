using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
    public partial class GenreForm : Form
    {
        int id;
        Genre genre;
        BooksDSDataContext ctx = new BooksDSDataContext();

        public GenreForm(int id)
        {
            InitializeComponent();

            this.id = id;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            genre.Name = GenreTextBox.Text;

            if (id == 0) ctx.Genres.InsertOnSubmit(genre);

            ctx.SubmitChanges();

            this.Close();
        }

        private void GenreForm_Load(object sender, EventArgs e)
        {
            if (id == 0) genre = new Genre();
            else
            {
                genre = (from g in ctx.Genres
                          where g.ID == id
                          select g).First();
            }

            GenreTextBox.Text = genre.Name;
        }
    }
}
