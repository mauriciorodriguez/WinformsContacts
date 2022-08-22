using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class ContactDetails : Form
    {
        private BussinessLogicLayer _bussinessLogicLayer;
        private Contact _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _bussinessLogicLayer = new BussinessLogicLayer();
        }
        #region EVENTS
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseContactDetails();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
        }
        #endregion

        #region PRIVATE METHODS
        private void CloseContactDetails()
        {
            this.Close();
        }

        private void SaveContact()
        {
            Contact contact = new Contact();
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;
            contact.Id = _contact != null? _contact.Id : 0;
            _bussinessLogicLayer.SaveContact(contact);
            ((MainWindow)this.Owner).PopulateContacts();
            CloseContactDetails();
        }
        #endregion

        public void LoadContact(Contact c)
        {
            ClearForm();
            _contact = c;
            if (c != null)
            {
                txtFirstName.Text = c.FirstName;
                txtLastName.Text = c.LastName;
                txtPhone.Text = c.Phone;
                txtAddress.Text = c.Address;
            }
        }

        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
    }
}
