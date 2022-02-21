using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAR_RENTAL_SERVICE.Infrastructure;
using CAR_RENTAL_SERVICE.Models;
using System.Security.Claims;

namespace Car_Rental_service.Controllers
{

    public class CarController : Controller
    {
        string userName;
        int userId;
        ICRUDRepository<Car, int> _repository;
        public SendServiceBusMessage _SendServiceBusMessage;
        public CarController(ICRUDRepository<Car, int> repository ,SendServiceBusMessage sendServiceBusMessage)

        {
            _repository = repository;
            _SendServiceBusMessage = sendServiceBusMessage;
        }
        public ActionResult<IEnumerable<Car>> Get()
        {
            try
            {
                var item = _repository.GetAll();
                return View(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult<Car> GetDetails(int id)
        {
            // userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            // userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            // var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            // if(role!="Admin" || role!="Operator") return Unauthorized();
            try
            {
                var item = _repository.GetDetails(id);
                if (item == null)
                    return NotFound();
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async Task< ActionResult<Car>> Create(Car emp)
        {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role != "Admin" && role != "Operator")
            {
                return Unauthorized();
            }
            if (emp == null)
                return BadRequest();
            try
            {
                _repository.Create(emp);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {

                    RegistrationNumber = emp.RegistrationNumber,
                    ModelName = emp.ModelName,
                    Action = "Create",
                    ActionMessage = "Car Detail Succesfully Created"


                });
                ViewBag.Message = string.Format("Car Details Added");
                return View();
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Update(int Id)
        {
            Car emp = _repository.GetDetails(Id);
            if (emp == null)
            {
                return BadRequest();
            }
            else
            {
                return View(emp);
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async Task<ActionResult<Car>> Update(Car emp)
        {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role != "Admin" && role != "Operator")
            {
                return Unauthorized();
            }
            if (emp == null)
                return BadRequest();
            try
            {
                _repository.Update(emp);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {

                    RegistrationNumber = emp.RegistrationNumber,
                    ModelName = emp.ModelName,
                    Action = "Update",
                    ActionMessage = "Car Detail Succesfully Updated"


                });

                ViewBag.Message = string.Format("Car Details Upadated");
                return View();
                //return emp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IActionResult delete(int Id)
        {
            Car emp = _repository.GetDetails(Id);
            if (emp == null)
            {
                return BadRequest();
            }
            else
            {
                return View(emp);
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async Task< ActionResult> Delete(int id)
        {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role != "Admin")
            {
                return Unauthorized();
            }
            try
            {
                _repository.Delete(id);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {

                    
                    Action = "Deleted",
                    ActionMessage = "Car Detail Succesfully Deleted"


                });
                ViewBag.Message = string.Format("Car Details Deleted");
                return View();
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}