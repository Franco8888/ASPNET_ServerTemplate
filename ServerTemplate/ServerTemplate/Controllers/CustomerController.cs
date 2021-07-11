﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerTemplate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ServerTemplate.Services.AdminService;

namespace ServerTemplate.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : ControllerBase    //let controller inherit from ControllerBase
    {
        //=======to bring in a service do it through dependency injection=========
        private readonly IAdminService _adminService;

        public CustomerController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        //========================================================================

        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // GET getAll
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //

        [HttpGet("getAll")]
        public IActionResult GetCustomers()
        {
            var result = _adminService.GetCustomers();

            return Ok(result);
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // GET customer/{customerId}
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //

        [HttpGet("customer/{customerId}")]
        public IActionResult GetCustomers(string customerId)
        {
            return Ok();
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // POST addCustomer
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //

        [HttpPost("addCustomer")]
        public IActionResult AddCustomer([FromBody] DTO_IN_Customer customerData)
        {
            var result = _adminService.AddCustomer(customerData);

            return Ok(result);
        }

    }
}