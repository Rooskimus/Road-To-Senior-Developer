using ACM.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACM.Win
{
    public partial class CustomerWin : Form
    {
        CustomerRepository customerRepository = new CustomerRepository();

        public CustomerWin()
        {
            InitializeComponent();
        }

        public void CustomerWin_Load(object sender, EventArgs e)
        {
            var customerList = customerRepository.Retrieve();

            CustomerComboBox.DisplayMember = "Name";
            CustomerComboBox.ValueMember = "CustomerId";
            CustomerComboBox.DataSource = customerRepository.GetNamesAndId(customerList);
        }

        private void GetCustomersButton_Click(object sender, EventArgs e)
        {
            var customerList = customerRepository.Retrieve();
            //CustomerGridView.DataSource = customerRepository.SortByName(customerList).ToList();

            //CustomerGridView.DataSource = customerRepository.GetOverdueCustomers(customerList).ToList();

            //var unpaidCustomerList = customerRepository.GetOverdueCustomers(customerList);
            //CustomerGridView.DataSource = customerRepository.SortByName(unpaidCustomerList).ToList();
            //// Even though it seems though there are two LINQ queries involved in creating this sorted list of customers
            //// with unpaid invoices, there is truly only one due to deferred execution.  The queries are not run until
            //// a result is required.  So assigning a variable or even a property won't execute the query.
            //// The ToList() is what actually executes the query, and C# creates and executes just one query.

            CustomerTypeRepository customerTypeRepository = new CustomerTypeRepository();
            var customerTypeList = customerTypeRepository.Retrieve();

            CustomerGridView.DataSource = customerRepository.GetNamesAndType(customerList, customerTypeList);
            // To get this to work, we had to go to GetNamesAndTypes and change return query to return query.ToList().
            // C# doesn't know how to make an anonymous type to a list, but it can operate on query before you send it
            // as an anonymous out of the method.  Also, this now makes the results in the view uneditable.  Anonymous
            // types are a one-way beast.  You can take the data and move forward, but there's no going back, hence no
            // changes can be made to the source (the source is anonymous, how can it change what it doesn't know?)
        }

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CustomerComboBox.SelectedValue != null)
            {
                int customerId;
                if (int.TryParse(CustomerComboBox.SelectedValue.ToString(), out customerId))
                {
                    var customerList = customerRepository.Retrieve();

                    CustomerGridView.DataSource = new List<Customer>()
                        {customerRepository.Find(customerList, customerId)};
                }
            }
        }
    }
}
