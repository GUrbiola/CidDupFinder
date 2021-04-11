using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CidDupFinder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            List<string> curFilters = new List<string>();

            foreach (object item in listFilters.Items)
                curFilters.Add(item.ToString().ToUpper());

            if (!String.IsNullOrEmpty(txtFilter.Text) && !curFilters.Contains(txtFilter.Text.ToUpper()))
                listFilters.Items.Add(txtFilter.Text);
        }

        private void btnRemoveFIlter_Click(object sender, EventArgs e)
        {
            if (listFilters.SelectedIndex >= 0)
                listFilters.Items.RemoveAt(listFilters.SelectedIndex);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Searcher frm;
            if(!String.IsNullOrEmpty(txtPath.Text))
            {
                List<string> curFilters = new List<string>();

                foreach (object item in listFilters.Items)
                    curFilters.Add(item.ToString().ToUpper());
                frm = new Searcher(txtPath.Text, curFilters);

                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("At least select a path to search for duplicated files.");
            }

        }
    }
}
