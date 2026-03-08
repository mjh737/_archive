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
    public partial class BooksForm : Form
    {
        BooksDSDataContext ctx = new BooksDSDataContext();
        List<Book> books = new List<Book>();

        public BooksForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBooks.View = View.Details;
            listBooks.Columns.Add("Author", 100);
            listBooks.Columns.Add("Title", 200);
            listBooks.Columns.Add("Rating", 50);
            listBooks.Columns.Add("Genre", 200);

            books = (from b in ctx.Books
                     select b).ToList();

            foreach (Book b in books)
            {
                ListViewItem item = new ListViewItem();
                item.Text = GetAuthor(b.AuthorID);
                item.SubItems.Add(b.Title);
                item.SubItems.Add(b.Rating.ToString());
                item.SubItems.Add(b.GenreID.ToString());
                item.Tag = b.ID;

                listBooks.Items.Add(item);
            }
        }

        private string GetAuthor(int id)
        {
            return (from a in ctx.Authors
                    where a.ID == id
                    select a).First().Name;
        }

        private void Add(object sender, EventArgs e)
        {
            int id = 0;
            BookForm form = new BookForm(id);
            form.ShowDialog();
        }

        private void Edit(object sender, EventArgs e)
        {
            if (listBooks.SelectedItems.Count < 1) return;
            int id = (int)listBooks.SelectedItems[0].Tag;
            BookForm form = new BookForm(id);
            form.ShowDialog();
        }

        private void Delete(object sender, EventArgs e)
        {
            if (listBooks.SelectedItems.Count < 1) return;
            int id = (int)listBooks.SelectedItems[0].Tag;

            Book book = (from b in ctx.Books
                         where b.ID == id
                         select b).First();

            ctx.Books.DeleteOnSubmit(book);

            ctx.SubmitChanges();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
