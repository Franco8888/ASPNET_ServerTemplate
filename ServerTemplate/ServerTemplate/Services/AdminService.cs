using MongoDB.Driver;
using ServerTemplate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ServerTemplate.Services.AdminService;

namespace ServerTemplate.Services
{
    public interface IAdminService
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(string customerId);
        string AddCustomer(DTO_IN_Customer customerData);
    }

    public class AdminService: IAdminService
    {
        // For each service using the DB we have to bring in our DB service================================
        private readonly IDatabaseService _databaseService;

        private readonly IMongoDatabase db; //these are the variable we set in each service to use them
        private readonly IMongoCollection<Customer> customerCollection; //these are the variable we set in each service to use them
        private readonly IMongoCollection<WineProduct> wineProductCollection; //these are the variable we set in each service to use them

        public AdminService(
            IDatabaseService databaseService
            )
        {
            _databaseService = databaseService;

            // using DB service set the collection and db values
            db = _databaseService.Database; //now db represents our db and we can use it in here
            customerCollection = _databaseService.CustomerCollection;
            wineProductCollection = _databaseService.WineProductCollection;
        }

        // ===============================================================================================

        public List<Customer> GetCustomers()
        {

            return new List<Customer>();
        }

        public Customer GetCustomer(string customerId)
        {

            return new Customer();
        }

        public string AddCustomer(DTO_IN_Customer customerData)
        {
            var customer = new Customer
            {
                Email = customerData.Email,
                Name = customerData.Name,
                TelephoneNumber = customerData.TelephoneNumber
            };

            try
            {
                customerCollection.InsertOne(customer);
                return "Successfully inserted Customer";
            }
            catch(Exception e)
            {
                return $"Error inserting Customer. Message: {e}";
            }

        }


        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // DTO
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //

        public class DTO_IN_Customer
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string TelephoneNumber { get; set; }
        }
    }
}
