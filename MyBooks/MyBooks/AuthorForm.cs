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
    public partial class AuthorForm : Form
    {
        int id;
        Author author;
        BooksDSDataContext ctx = new BooksDSDataContext();

        public AuthorForm(int id)
        {
            InitializeComponent();

            this.id = id;
        }

        private void AuthorForm_Load(object sender, EventArgs e)
        {
            if (id == 0) author = new Author();
            else
            {
                author = (from a in ctx.Authors
                          where a.ID == id
                          select a).First();
            }

            NameTextBox.Text = author.Name;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            author.Name = NameTextBox.Text;

            if (id == 0) ctx.Authors.InsertOnSubmit(author);

            ctx.SubmitChanges();

            this.Close();
        }
    }
}
