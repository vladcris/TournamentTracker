using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  EmailAdress { get; set; }
        public string CellphoneNumber { get; set; }

        public PersonModel()
        { }

        public PersonModel(string firstName, string lastName, string emailAddress, string cellPhoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAdress = emailAddress;
            CellphoneNumber = cellPhoneNumber;
        }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

    }
}
