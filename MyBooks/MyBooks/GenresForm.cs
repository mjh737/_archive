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
    public partial class GenresForm : Form
    {
        BooksDSDataContext ctx = new BooksDSDataContext();
        List<Genre> genres;

        public GenresForm()
        {
            InitializeComponent();
        }

        private void GenresForm_Load(object sender, EventArgs e)
        {
            genres = (from g in ctx.Genres
                      select g).ToList();

            listGenres.View = View.Details;
            listGenres.Columns.Add("Name");

            foreach (Genre genre in genres)
            {
                ListViewItem item = new ListViewItem();
                item.Text = genre.Name;
                item.Tag = genre.ID;
                listGenres.Items.Add(item);
            }
        }

        private void Add(object sender, EventArgs e)
        {
            GenreForm form = new GenreForm(0);
            form.ShowDialog();

            listGenres.Items.Clear();
            GenresForm_Load(null, null);
        }

        private void Edit(object sender, EventArgs e)
        {
            if (listGenres.SelectedItems.Count < 1) return;
            int id = (int)listGenres.SelectedItems[0].Tag;

            GenreForm form = new GenreForm(id);
            form.ShowDialog();

            listGenres.Items.Clear();
            GenresForm_Load(null, null);
        }

        private void Delete(object sender, EventArgs e)
        {
            ListViewItem item = listGenres.SelectedItems[0];

            Genre genre = (from g in ctx.Genres
                             where g.ID == (int)item.Tag
                             select g).First();

            ctx.Genres.DeleteOnSubmit(genre);

            ctx.SubmitChanges();

            listGenres.Items.Remove(item);

            listGenres.Items.Clear();
            GenresForm_Load(null, null);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
