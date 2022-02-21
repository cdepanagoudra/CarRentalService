using Microsoft.EntityFrameworkCore;

using CAR_RENTAL_SERVICE.Models;


namespace CAR_RENTAL_SERVICE.Infrastructure
{
     public class CarRentalContext : DbContext

    {



         public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options) {  }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

         {

           

        }
        public DbSet<Car> Cars {get; set;}
        public DbSet<CarCategory> CarCategories {get; set;}
        public DbSet<CustomerDetail> CustomerDetails {get; set;}
        public DbSet<DriverDetail> DriverDetails {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
        public DbSet<UserRole> UserRoles {get;set;}

    //     public DbSet<BillingDetail> BILLING_DETAILS{ get; set; }
    //     // public DbSet<CustomerDetail> CustomerDetails {get; set;}
    //     public DbSet<BookingDetail> BOOKING_DETAILS {get; set;}
    //     // public DbSet<CarCategory> CarCategorys {get; set;}
    //     // public DbSet<Car> Cars {get; set;}
    //     // public DbSet<DiscountDetail> DiscountDetails {get; set;}
    //     // public DbSet<DriverDetail> DriverDetails {get; set;}
    //     // public DbSet<LocationDetail> LocationDetails {get; set;}
    //     // public DbSet<RentalCarInsurance> RentalCarInsurances {get; set;}
        



    // }
    }
}
