using CAR_RENTAL_SERVICE.Models;
using System.Collections.Generic;
using System.Linq;

namespace CAR_RENTAL_SERVICE.Infrastructure
{
    public class CustomerDetailsRepository : ICRUDRepository<CustomerDetail, int>
    {
        CarRentalContext _db;
        public CustomerDetailsRepository(CarRentalContext db)
        {
            _db = db;
        }
        public IEnumerable<CustomerDetail> GetAll()
        {
            return _db.CustomerDetails.ToList();
        }
        public CustomerDetail GetDetails(int id)
        {
            return _db.CustomerDetails.FirstOrDefault(c=>c.Cust_DLNumber==id);
        }
        public void Create(CustomerDetail item)
        {
            _db.CustomerDetails.Add(item);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
             var obj = _db.CustomerDetails.FirstOrDefault(c=>c.Cust_DLNumber==id);
            if(obj==null)
                return;  
            _db.CustomerDetails.Remove(obj);
            _db.SaveChanges();
        }
        public void Update(CustomerDetail item)
        {
             var obj = _db.CustomerDetails.FirstOrDefault(c=>c.Cust_DLNumber==item.Cust_DLNumber);

            if(obj==null)

                return;

            obj.FirstName = item.FirstName;

            

            obj.LastName = item.LastName;

            obj.PhoneNumber = item.PhoneNumber;

            

            obj.City= item.City;

            obj.StateName = item.StateName;

            obj.Zipcode = item.Zipcode;

            

            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _db.SaveChanges();

        }

    }

   
}