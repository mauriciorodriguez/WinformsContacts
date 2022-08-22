using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class MainWindow : Form
    {
        private BussinessLogicLayer _bussinessLogicLayer;
        public MainWindow()
        {            
            InitializeComponent();
            _bussinessLogicLayer = new BussinessLogicLayer();
        }
        #region EVENTS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailsDialog(new ContactDetails());
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }
        #endregion

        #region PRIVATE METHODS
        private void OpenContactDetailsDialog(ContactDetails cd)
        {
            cd.ShowDialog(this);
        }
        #endregion
        public void PopulateContacts(string searchText = null)
        {
            List<Contact> contacts = _bussinessLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }

        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString(),
                }) ;
                OpenContactDetailsDialog(contactDetails);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }
        }

        private void DeleteContact(int id)
        {
            _bussinessLogicLayer.DeleteContact(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }
    }
}
