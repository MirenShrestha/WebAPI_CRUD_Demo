using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI_CRUD_Demo.Models;

namespace WebAPI_CRUD_Demo.Controllers
{
    public class CustomerController : ApiController
    {
        // GET --> Retrieve Data
        public IHttpActionResult GetAllCustomer()
        {
            IList<CustomerViewModel> customers = null;
            using (var x = new WebAPI_CRUD_DemoEntities())
            {
                customers = x.Customes
                             .Select(c => new CustomerViewModel()
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Email = c.Email,
                                 Address = c.Address,
                                 Phone = c.Phone
                             }).ToList<CustomerViewModel>();



                if (customers.Count == 0)
                    return NotFound();

                return Ok(customers);
            }
        }


        // GET --> Retrieve Data by Id
        public IHttpActionResult GetCustomer(int id)
        {
          CustomerViewModel customer = null;
            using (var x = new WebAPI_CRUD_DemoEntities())
            {
                customer = x.Customes
                    .Where(i => i.Id == id)
                             .Select(c => new CustomerViewModel()
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Email = c.Email,
                                 Address = c.Address,
                                 Phone = c.Phone
                             }).FirstOrDefault();

           

                return Ok(customer);
            }
        }


        // POST --> Insert new record

        public IHttpActionResult PostNewCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.Please recheck!");
            using (var x = new WebAPI_CRUD_DemoEntities())
            {
                x.Customes.Add(new Custome()
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Address = customer.Address,
                    Phone = customer.Phone

                });

                x.SaveChanges();
            }

            return Ok();
        }

        // Put --> Update datebase  on id
        //for consuming PUT method we need Get data by ID

        public IHttpActionResult PutCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("This is invalid model.Please recheck!");

            using (var x = new WebAPI_CRUD_DemoEntities())
            {
                var checkExistingCustomer = x.Customes.Where(c => c.Id == customer.Id)
                                            .FirstOrDefault<Custome>();

                if (checkExistingCustomer != null)
                {
                    checkExistingCustomer.Name = customer.Name;
                    checkExistingCustomer.Address = customer.Address;
                    checkExistingCustomer.Phone = customer.Phone;

                    x.SaveChanges();
                }
                else

                    return NotFound();
            }
            return Ok();
        }

        // Delete --> Delete a record based in id
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Please enter valid Customer id");

                using (var x = new WebAPI_CRUD_DemoEntities())
                {
                    var customer = x.Customes
                        .Where(c => c.Id == id)
                        .FirstOrDefault();


                    x.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                    x.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw;

            }

            return Ok();
        }








    }

}
