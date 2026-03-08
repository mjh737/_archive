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
    public partial class AuthorsForm : Form
    {
        BooksDSDataContext ctx = new BooksDSDataContext();
        List<Author> authors = new List<Author>();

        public AuthorsForm()
        {
            InitializeComponent();
        }

        private void AuthorsForm_Load(object sender, EventArgs e)
        {
            authors = (from a in ctx.Authors
                       select a).ToList();

            listAuthors.View = View.Details;
            listAuthors.Columns.Add("Name", 100);

            foreach (Author author in authors)
            {
                ListViewItem item = new ListViewItem(author.Name);
                item.Tag = author.ID;
                listAuthors.Items.Add(item);
            }
        }

        private void Add(object sender, EventArgs e)
        {
            AuthorForm form = new AuthorForm(0);
            form.ShowDialog();

            listAuthors.Items.Clear();
            AuthorsForm_Load(null, null);
        }

        private void Edit(object sender, EventArgs e)
        {
            int id = (int)listAuthors.SelectedItems[0].Tag;

            AuthorForm form = new AuthorForm(id);
            form.ShowDialog();

            listAuthors.Items.Clear();
            AuthorsForm_Load(null, null);
        }

        private void Delete(object sender, EventArgs e)
        {
            if (listAuthors.SelectedItems.Count < 1) return;

            ListViewItem item = listAuthors.SelectedItems[0];

            Author author = (from a in ctx.Authors
                             where a.ID == (int)item.Tag
                             select a).First();

            ctx.Authors.DeleteOnSubmit(author);

            ctx.SubmitChanges();

            listAuthors.Items.Remove(item);

            listAuthors.Items.Clear();
            AuthorsForm_Load(null, null);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
