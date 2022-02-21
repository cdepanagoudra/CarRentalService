using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAR_RENTAL_SERVICE.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAR_RENTAL_SERVICE.Models;
using System.Security.Claims;

namespace CAR_RENTAL_SERVICE.Controllers
{
  
    public class CustomerDetailController : Controller
    {
        string userName;
        int userId;
         ICRUDRepository<CustomerDetail, int> _repository;

        public SendServiceBusMessage _SendServiceBusMessage;
        public CustomerDetailController(ICRUDRepository<CustomerDetail, int> repository , SendServiceBusMessage sendServiceBusMessage)
        
        {
            _repository = repository;
            _SendServiceBusMessage = sendServiceBusMessage;
        }



        public ActionResult<IEnumerable<CustomerDetail>> Get()
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
        public ActionResult<CustomerDetail> GetDetails(int id)
        {
            var item = _repository.GetDetails(id);
            if( item==null )
                return NotFound();
            return item;    
        }

        public IActionResult Create()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
       
        public async Task<ActionResult<CustomerDetail>> Create( CustomerDetail cust)
        {

            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="Admin") return Unauthorized();
            if(cust==null)
                return BadRequest();

            _repository.Create(cust);
            await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
            {

                FirstName = cust.FirstName,
                PhoneNumber = cust.PhoneNumber,
                Action = "Create",
                ActionMessage = "Customer Detail Succesfully Created"


            });
            ViewBag.Message = string.Format("Customer Details Added");
            return View();
            //return cust; 
        }

        public IActionResult update(int Id)
        {
            CustomerDetail emp = _repository.GetDetails(Id);
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
        public async Task< ActionResult<CustomerDetail>> Update(CustomerDetail  cust)
        {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="Admin") return Unauthorized();

            if(cust == null)

                     return BadRequest();
                 _repository.Update(cust);
            await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
            {

                FirstName = cust.FirstName,
                PhoneNumber = cust.PhoneNumber,
                Action = "Update",
                ActionMessage = "Customer Detail Succesfully Updated"
            });

            ViewBag.Message = string.Format("Customer Details Updated");
            return View();

            // return cust;
        }

        public IActionResult delete(int Id)
        {
            CustomerDetail emp = _repository.GetDetails(Id);
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
            if(role!="Admin") return Unauthorized();
            _repository.Delete(id);
            await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
            {

                
                Action = "Delete",
                ActionMessage = "Customer Detail Succesfully Deleted"
            });
            ViewBag.Message = string.Format("Customer Details Deleted");
            return View();
           // return Ok();

        }
    }
}