using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAR_RENTAL_SERVICE.Infrastructure;
using CAR_RENTAL_SERVICE.Models;

namespace Car_Rental_service.Controllers
{
    
    public class CarCategoryController : Controller
    {
        ICRUDRepository<CarCategory, int> _repository;
        public CarCategoryController(ICRUDRepository<CarCategory,int> repository) => _repository = repository;
        [HttpGet]
        public ActionResult<IEnumerable<CarCategory>> Get()
        {
            var item = _repository.GetAll();
                return item.ToList();
        }
        [HttpDelete]
        public ActionResult<CarCategory> GetDetails(int id)
        {
             var item = _repository.GetDetails(id);
                if(item == null)
                    return NotFound();
                return item;
        }
        [HttpPost("AddNewCarCategory")]
        public ActionResult<CarCategory> Create(CarCategory emp)
        {
             if(emp == null)
                    return BadRequest();
                _repository.Create(emp);
                return emp;
        }

        [HttpPut("UpdateCarCategory/{id}")]
        public ActionResult<CarCategory> Update(CarCategory emp)
        {
            if(emp == null)
                    return BadRequest();
                _repository.Update(emp);
                return emp;
        }
        [HttpDelete("RemoveCarCategory/{id}")]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}