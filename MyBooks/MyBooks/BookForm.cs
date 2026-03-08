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
    public partial class BookForm : Form
    {
        BooksDSDataContext ctx = new BooksDSDataContext();
        List<Author> authors;
        List<Genre> genres;
        Book book;
        int id;

        public BookForm(int id)
        {
            InitializeComponent();

            this.id = id;
        }

        private void AuthorButton_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        private void BookForm_Load(object sender, EventArgs e)
        {
            authors = (from a in ctx.Authors
                       select a).ToList();

            genres = (from g in ctx.Genres
                      select g).ToList();

            if (id == 0)
            {
                book = new Book();
            }
            else
            {
                book = (from b in ctx.Books
                        where b.ID == id
                        select b).First();
            }

            cboAuthor.DataSource = authors;
            cboAuthor.DisplayMember = "Name";
            cboAuthor.ValueMember = "ID";
            cboAuthor.SelectedValue = book.AuthorID;

            cboGenre.DataSource = genres;
            cboGenre.DisplayMember = "Name";
            cboGenre.ValueMember = "ID";
            if (book.GenreID != null)
                cboGenre.SelectedValue = book.GenreID;

            TitleTextBox.Text = book.Title;
        }

        private void GenreButton_Click(object sender, EventArgs e)
        {
            GenresForm form = new GenresForm();
            form.ShowDialog();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            book.Rating = (int)numericUpDown1.Value;
            if (cboGenre.SelectedItem != null)
                book.GenreID = ((Genre)(cboGenre.SelectedItem)).ID;
            book.AuthorID = ((Author)(cboAuthor.SelectedItem)).ID;
            book.ThousandOne = cb1001.Checked;
            book.Title = TitleTextBox.Text;

            if (book.ID == 0) ctx.Books.InsertOnSubmit(book);

            ctx.SubmitChanges();

            this.Close();
        }
    }
}
