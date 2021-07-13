using MongoDB.Driver;
using ServerTemplate.Models;
using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static ServerTemplate.Services.AdminService;

namespace ServerTemplate.Services
{
    public interface IAdminService
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(string customerId);
        string AddCustomer(DTO_IN_Customer customerData);
        string AddCustomers(List<DTO_IN_Customer> customersData);
        string UpdateCustomer(string customerId, DTO_IN_Customer update);
        string DeleteCustomer(string customerId);
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
            try
            {
                var result = customerCollection.Find(customer => true).ToList();
                return result;
            }
            catch(Exception e)
            {
                return new List<Customer>();
            }
        }

        public Customer GetCustomer(string customerId)
        {
            try
            {
                var result = customerCollection.Find(customer => customer.Id == customerId).FirstOrDefault();
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
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

        public string AddCustomers(List<DTO_IN_Customer> customersData)
        {
            var customers = new List<Customer>();

            foreach (var customer in customersData)
            {
                customers.Add(new Customer
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    TelephoneNumber = customer.TelephoneNumber
                });
            }

            IEnumerable<Customer> customerIE = customers;

            try
            {
                customerCollection.InsertMany(customerIE);
                return "Successfully inserted customer";
            }
            catch(Exception e)
            {
                return $"Failed to insert customers. Reason: {e}";
            }
        }


        public string UpdateCustomer(string customerId, DTO_IN_Customer update)
        {

            var customerUpdateDefinition = Builders<Customer>.Update
                .Set(c => c.Name, update.Name)
                .Set(c => c.Email , update.Email)
                .Set(c => c.TelephoneNumber, update.TelephoneNumber);

            try
            {
                //customerCollection.FindOneAndUpdate();
                customerCollection.UpdateOne<Customer>(customer => customer.Id == customerId, customerUpdateDefinition);
                return "Successfully updated Customer";
            }
            catch(Exception e)
            {
                return $"Error updating Customer. Message: {e}";
            }

        }
        
        public string DeleteCustomer(string customerId)
        {
            try
            {
                customerCollection.DeleteOne<Customer>(customer => customer.Id == customerId);
                return "Successfully deleted Customer";
            }
            catch(Exception e)
            {
                return $"Error deleting Customer. Message: {e}";
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
