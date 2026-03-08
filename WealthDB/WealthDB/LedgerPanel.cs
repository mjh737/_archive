using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WealthDB
{
    public partial class LedgerPanel : UserControl
    {
        WealthDataContext wealth;
        List<Transaction> transactions;

        public LedgerPanel()
        {
            InitializeComponent();

            wealth = new WealthDataContext();
            transactions = (from t in wealth.Transactions
                            select t).ToList();

            InitializeListView();

            PopulateListView();
        }

        private void InitializeListView()
        {
            listTransactions.View = View.Details;
            listTransactions.Columns.Add("Date", 70);
            listTransactions.Columns.Add("Description", 200);
            listTransactions.Columns.Add("Amount", 70);
        }

        private void PopulateListView()
        {
            foreach (Transaction transaction in transactions)
            {
                ListViewItem item = new ListViewItem(transaction.Date.ToShortDateString());
                item.SubItems.Add(transaction.Description);
                item.SubItems.Add(transaction.Amount.ToString());
                item.Tag = transaction.ID;
                listTransactions.Items.Add(item);
            }
        }


    }
}
