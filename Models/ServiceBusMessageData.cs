using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAR_RENTAL_SERVICE.Models
{
    public class ServiceBusMessageData
    {
        public int Driver_DLNumber { get; set; }
        public int RegistrationNumber { get; set; }
        public string ModelName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public int Zipcode { get; set; }
        public string Action { get; set;}
        public string ActionMessage { get; set;}

    }
}
