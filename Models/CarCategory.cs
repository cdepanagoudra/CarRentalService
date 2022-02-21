using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_SERVICE.Models
{
    public class CarCategory
    {
        [Key]
        public string CategoryName { get; set; }

        public int NoOfLuggage { get; set; }

        public int NoOfPerson { get; set; }

        public int CostPerDay { get; set; }

        public int LateFeePerHour { get; set; }
    }
}