using CAR_RENTAL_SERVICE.Infrastructure;
using CAR_RENTAL_SERVICE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAR_RENTAL_SERVICE.Controllers
{

    public class DriverDetailController : Controller
    {
        string userName;
        int userId;

        ICRUDRepository<DriverDetail, int> _repository;
        public SendServiceBusMessage _SendServiceBusMessage;
     

        public DriverDetailController(ICRUDRepository<DriverDetail,int> repository, SendServiceBusMessage sendServiceBusMessage)
        {
            _repository = repository;
            _SendServiceBusMessage = sendServiceBusMessage;
        }



        public ActionResult<IEnumerable<DriverDetail>> Get()
        {
            try
            {
                var item = _repository.GetAll();
                return View(item);
               }catch (Exception ex)
            {
                throw;
            }
        }

        

            [HttpGet]
            public ActionResult<DriverDetail> GetDetails(int id)
            {
                var item = _repository.GetDetails(id);
                if(item == null)
                    return NotFound();
                return item;
            }
        public IActionResult Create()
        {
            return View();
        }



            [Microsoft.AspNetCore.Authorization.Authorize()]
            [HttpPost]
            public async Task<ActionResult<DriverDetail>> Create(DriverDetail emp)
            {
           userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="Admin" && role!="Operator") 
            {
                return Unauthorized(); 
            }
                if(emp == null)
                    return BadRequest();
                _repository.Create(emp);
            await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
            {

                FirstName = emp.FirstName,
                PhoneNumber = emp.PhoneNumber,
                Action = "Create",
                ActionMessage = "Driver Detail Succesfully Created"


            });
            ViewBag.Message = string.Format("Driver Details Added");
            return View();
            }



        public IActionResult update(int Id)
        {
            DriverDetail emp = _repository.GetDetails(Id);
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
            public async Task<ActionResult<DriverDetail>> Update(DriverDetail emp)
            {
                userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="Admin" && role!="Operator")
            { 
                return Unauthorized();
            }
                if(emp == null)
                    return BadRequest();
                _repository.Update(emp);
            await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
            {

                FirstName = emp.FirstName,
                PhoneNumber = emp.PhoneNumber,
                Action = "Update",
                ActionMessage = "Driver Detail Succesfully Updated"


            });
            ViewBag.Message = string.Format("Driver Details Upadated");
                return View();
            }
        public IActionResult delete(int Id)
        {
            DriverDetail emp = _repository.GetDetails(Id);
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
            public async Task<ActionResult> Delete(int id)
            {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="Admin")
            {
                 return Unauthorized();
            }
            if (id == null)
                return BadRequest();
            _repository.Delete(id);
            await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
            {

                
                Action = "Delete",
                ActionMessage = "Driver Detail Succesfully deleted"


            });
            ViewBag.Message = string.Format("Driver Details Deleted");
            return View();
            return Ok();
            }
    }
}