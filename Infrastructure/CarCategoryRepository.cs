using CAR_RENTAL_SERVICE.Models;
using System.Collections.Generic;
using System.Linq;

namespace CAR_RENTAL_SERVICE.Infrastructure
{
    public class CarCategoryRepository : ICRUDRepository<CarCategory,string>
    {
        CarRentalContext _db;
        public CarCategoryRepository(CarRentalContext db)
        {
            _db = db;
        }
        public IEnumerable<CarCategory> GetAll()
        {
            //throw new NotImplementedException();
            return _db.CarCategories.ToList();
        }

        public CarCategory GetDetails(string id)
        {
            //throw new NotImplementedException();
         return _db.CarCategories.FirstOrDefault(c=>c.CategoryName==id);

        }

        public void Create(CarCategory item)
        {
            //throw new NotImplementedException();
            _db.CarCategories.Add(item);
            _db.SaveChanges();
        }
        

        public void Update(CarCategory item)
        {
            //throw new NotImplementedException();
            var obj = _db.CarCategories.FirstOrDefault(c=>c.CategoryName==item.CategoryName);
            if(obj==null)
                return ;
            obj.CategoryName = item.CategoryName;
            obj.CostPerDay = item.CostPerDay;
            obj.LateFeePerHour = item.LateFeePerHour;
            obj.NoOfLuggage = item.NoOfLuggage;
            obj.NoOfPerson = item.NoOfPerson;
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }

        

        public void Delete(string id)
        {
           // throw new NotImplementedException();
            var obj = _db.CarCategories.FirstOrDefault(c=>c.CategoryName==id);
            if(obj==null)
                return;  
            _db.CarCategories.Remove(obj);
            _db.SaveChanges();
        }
    }
}