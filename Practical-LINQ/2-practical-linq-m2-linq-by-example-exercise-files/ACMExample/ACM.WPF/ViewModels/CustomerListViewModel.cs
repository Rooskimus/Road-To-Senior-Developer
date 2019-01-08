using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ACM.BL;
using ACM.WPF.Models;

namespace ACM.WPF.ViewModels
{
    public class CustomerListViewModel : ViewModelBase
    {
        private ObservableCollection<CustomerModel> _Customers;
        /// <summary>
        /// Gets or sets the list of customers to bind to the View.
        /// </summary>
        public ObservableCollection<CustomerModel> Customers
        {
            get { return _Customers; }
            set
            {
                if (_Customers != value)
                {
                    _Customers = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private List<KeyValuePair<string, decimal>> _ChartData;
        /// <summary>
        /// Gets or sets the list of data to bind to the chart
        /// </summary>
        public List<KeyValuePair<string, decimal>> ChartData
        {
            get { return _ChartData; }
            set
            {
                if (_ChartData != value)
                {
                    _ChartData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        CustomerRepository customerRepository = new CustomerRepository();

        public CustomerListViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            _Customers = new ObservableCollection<CustomerModel>();

            var customerList = customerRepository.Retrieve();

            CustomerTypeRepository customerTypeRepository = new CustomerTypeRepository();
            var customerTypeList = customerTypeRepository.Retrieve();

            //var sortedList = customerRepository.SortByName(customerList);
           // var itemList = customerRepository.GetNamesAndId(customerList);

            //var itemList = customerRepository.GetNamesAndType(customerList,
            //                                        customerTypeList);


            var query = customerList.Join(customerTypeList,
                                c => c.CustomerTypeId,
                                ct => ct.CustomerTypeId,
                                (c, ct) => new CustomerModel
                                {
                                    Name = c.LastName + ", " + c.FirstName,
                                    CustomerTypeName = ct.TypeName
                                });

            foreach (var customerInstance in query.OrderBy(c=>c.Name))
            {
                _Customers.Add(customerInstance);
            }

            ChartData = customerRepository.GetInvoiceTotalByCustomerType(customerList,
                                                                         customerTypeList).ToList();
        }
    }
}
