using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Customer
    {
        public static int InstanceCount { get; set; }

        private string _lastName; //Backing field

        public string LastName //Property delcaration
        {
            get
            {
                //Any code here
                return _lastName;
            }
            set
            {
                //Any code here
                _lastName = value;
            }
        }

        public string FirstName { get; set; } // Auto-Implemented property; complier creates above for you.

        public string EmailAddress { get; set; }

        public int CustomerId { get; private set; } // A private setter restricts what can overwrite data.

        public string FullName  // Because we derive this from our first and last name properties
        {                       // No code should be able to directly set this, hence no setter.
            get
            {
                string fullName = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName += ", ";
                    }
                    fullName += FirstName;
                }
                return fullName;
            }
        }


    }
}
