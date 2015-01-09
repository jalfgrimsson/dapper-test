using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Id", typeof (int)));
            dt.Columns.Add(new DataColumn("First name", typeof (string)));
            dt.Columns.Add(new DataColumn("Last name", typeof (string)));
            dt.Columns.Add(new DataColumn("Birthdate", typeof (string)));

            Repository repository = new Repository();
            var customers = repository.GetAllCustomers();
            foreach (Customer customer in customers)
            {
                dt.Rows.Add(customer.Id, customer.FirstName, customer.LastName, customer.DateOfBirth.ToShortDateString());
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            Repository repository = new Repository();
            repository.CreateDatabase();
            MessageBox.Show("Database created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button2.Enabled = true;
        }

        private void clearEntryButton_Click(object sender, EventArgs e)
        {
            firstNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            birthDatePicker.Value = DateTime.Now;
        }

        private void newEntryButton_Click(object sender, EventArgs e)
        {
            Repository repository = new Repository();
            Customer customer = new Customer()
            {
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                DateOfBirth = birthDatePicker.Value
            };
            repository.SaveCustomer(customer);
            MessageBox.Show("Customer saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            int soughtId = (int) idNumericUpDown.Value;
            Repository repository = new Repository();
            Customer foundCustomer = repository.FindCustomer(soughtId);
            if (foundCustomer == null)
            {
                MessageBox.Show("Customer with ID " + soughtId + " not found!", "Not found", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            CustomerInformation customerInformationForm = new CustomerInformation(foundCustomer);
            customerInformationForm.Show();
        }
    }
}
