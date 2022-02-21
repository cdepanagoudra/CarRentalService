using CAR_RENTAL_SERVICE.Models;
using System.Collections.Generic;
using System.Linq;

namespace CAR_RENTAL_SERVICE.Infrastructure
{
    public class CarRepository : ICRUDRepository<Car, int>
    {
        CarRentalContext _db;
        public CarRepository(CarRentalContext db)
        {
            _db = db;
        }
        public void Create(Car item)
        {
            //throw new NotImplementedException();
            _db.Cars.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var obj = _db.Cars.FirstOrDefault(c=>c.RegistrationNumber==id);
            if(obj==null)
                return;
            _db.Cars.Remove(obj);
            _db.SaveChanges();
        }

        public IEnumerable<Car> GetAll()
        {
            //throw new NotImplementedException();
            return _db.Cars.ToList();
        }

        public Car GetDetails(int id)
        {
            //throw new NotImplementedException();
            return _db.Cars.FirstOrDefault(c=>c.RegistrationNumber==id);
        }

        public void Update(Car item)
        {
            //throw new NotImplementedException();
            var obj = _db.Cars.FirstOrDefault(c=>c.RegistrationNumber==item.RegistrationNumber);
            if(obj==null)
                return;
            obj.Make = item.Make;
            obj.Mileage = item.Mileage;
            obj.ModelName = item.ModelName;
            obj.ModelYear = item.ModelYear;
            obj.RegistrationNumber = item.RegistrationNumber;
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }

}