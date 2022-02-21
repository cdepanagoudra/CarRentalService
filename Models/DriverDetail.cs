using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_SERVICE.Models
{
    public class DriverDetail
    {
        [Key]
        ////[Required(Errormessage="driver dl number is required")]
        //  [Range(minimum:6,maximum:8,ErrorMessage="Limit exceeds not in the given range")]

        public int Driver_DLNumber { get; set; }
        // [Required(ErrorMessage="First name should be given")]

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public int Zipcode { get; set; }
        // public string STATE_NAME{get;set;}
        // public int ZIPCODE{get;set;}
    }
}