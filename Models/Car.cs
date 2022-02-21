using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_SERVICE.Models
{
    public class Car
    {
        [Key]
        [Required(ErrorMessage = "Registration Number is required")]
        //[Range(minimum:6,maximum:8,ErrorMessage ="Registration Number is invalid")]
        public int RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Model Name is Required")]
        //[StringLength(maximumLength:20,ErrorMessage ="Maximum Length exceeded")]
        public string ModelName { get; set; }
        [Required(ErrorMessage = "Make is Required")]
        //[StringLength(maximumLength:20,ErrorMessage ="Maximum Length exceeded")]
        public string Make { get; set; }
        //[Required(ErrorMessage ="model year is required")]
        //[StringLength(maximumLength:20,ErrorMessage = "Maximum Length exceeded")]
        public int ModelYear { get; set; }
        [Required(ErrorMessage = "Mileage is Required")]
        [Range(minimum: 15, maximum: 55, ErrorMessage = "Maximum Length exceeded")]
        public int Mileage { get; set; }

        // public string CategoryName {get; set;}

        // public string LocId {get; set;}

        // public string AvailabilityFlag {get; set;}
    }
}