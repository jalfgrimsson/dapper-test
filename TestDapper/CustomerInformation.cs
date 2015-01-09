using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDapper
{
    public partial class CustomerInformation : Form
    {
        public CustomerInformation(Customer customer)
        {
            InitializeComponent();
            idTextBox.Text = customer.Id.ToString();
            firstNameTextBox.Text = customer.FirstName;
            lastNameTextBox.Text = customer.LastName;
            birthdateTextBox.Text = customer.DateOfBirth.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
