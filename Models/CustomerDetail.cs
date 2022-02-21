using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_SERVICE.Models
{
    public class CustomerDetail
    {
        [Key]
        [Required]
        public int Cust_DLNumber{get; set;}
        public string FirstName{get; set;}
        public string LastName{get; set;}
       // public string LName{get; set;}
        public int PhoneNumber{get; set;}
        //public string EmailId{get;set;}
        //public string Street {get; set;}
        public string City{get; set;}
        public string StateName{get; set;}
        public int Zipcode{get; set;}
    }

}
