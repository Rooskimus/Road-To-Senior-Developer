using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadsAndDelegates
{
    public partial class DirectorySearcherForm : Form
    {
        public DirectorySearcherForm()
        {
            InitializeComponent();
        }

        static void Main()
        {
            Application.Run(new DirectorySearcherForm());
        }

        private void directorySearcher_SearchComplete(object sender, System.EventArgs e)
        {
            SearchLabel.Text = string.Empty;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DirectorySearcher directorySearcher = new DirectorySearcher();
            directorySearcher.SearchCriteria = SearchBar.Text;
            SearchLabel.Text = "Searching...";
            directorySearcher.BeginSearch();

        }
    }
}
