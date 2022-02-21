using CAR_RENTAL_SERVICE.Models;
using System.Collections.Generic;
using System.Linq;

namespace CAR_RENTAL_SERVICE.Infrastructure
{
    public class DriverDetailRepository : ICRUDRepository<DriverDetail,int>
    {
        CarRentalContext _db;
        public DriverDetailRepository(CarRentalContext db)
        {
            _db = db;
        }
        
        public IEnumerable<DriverDetail> GetAll()
        {
            //throw new NotImplementedException();
            return _db.DriverDetails.ToList();
        }

        public DriverDetail GetDetails(int id)
        {
            //throw new NotImplementedException();
            return _db.DriverDetails.FirstOrDefault(c=>c.Driver_DLNumber==id);
        }

        public void Create(DriverDetail item)
        {
            //throw new NotImplementedException();
            _db.DriverDetails.Add(item);
            _db.SaveChanges();
        }

        public void Update(DriverDetail item)
        {
            //throw new NotImplementedException();
            var obj = _db.DriverDetails.FirstOrDefault(c=>c.Driver_DLNumber == item.Driver_DLNumber);
            if(obj==null)
                return;
            obj.City=item.City;
            obj.Driver_DLNumber=item.Driver_DLNumber;
            obj.FirstName=item.FirstName;
            obj.LastName=item.LastName;
            obj.PhoneNumber=item.PhoneNumber;
            obj.StateName = item.StateName;
            obj.Zipcode=item.Zipcode;
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var obj = _db.DriverDetails.FirstOrDefault(c=>c.Driver_DLNumber==id);
            if(obj==null)
                return;
            _db.DriverDetails.Remove(obj);
            _db.SaveChanges();
        }
    }
}