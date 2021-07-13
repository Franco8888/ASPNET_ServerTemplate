using Microsoft.Extensions.Configuration;
using ServerTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

// This service handles getting the DB and storing DB collection names used in other services
namespace ServerTemplate.Services
{
    public interface IDatabaseService
    {
        // =========Store collection names ================
        IMongoCollection<Customer> CustomerCollection { get; set; } 

        IMongoCollection<WineProduct> WineProductCollection { get; set; }
        // ================================================

        IMongoDatabase Database { get; set; }   //Variable stores DB name
    }

    public class DatabaseService : IDatabaseService
    {
        // Variables that gets DB settings data from appsettings.json =========
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly string _customerCollection;
        private readonly string _wineProductCollection;
        // ====================================================================


        // These properties will hold our collections/tables (represent our collections)
        // WE use properties so that we can export them to other Services

        public IMongoCollection<Customer> CustomerCollection { get; set; }

        public IMongoCollection<WineProduct> WineProductCollection { get; set; }

        public IMongoDatabase Database { get; set; }


        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // CTOR
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------ //

        //======Populate your collections using constructor=============
        public DatabaseService(IConfiguration configuration)
        {
            // Populate our settings with appsettings.json===============
            _connectionString = configuration.GetSection("WineSalesDBSettings").GetValue<string>("ConnectionString");
            _databaseName = configuration.GetSection("WineSalesDBSettings").GetValue<string>("DatabaseName");
            _customerCollection = configuration.GetSection("WineSalesDBSettings").GetValue<string>("CustomerCollectionName");
            _wineProductCollection = configuration.GetSection("WineSalesDBSettings").GetValue<string>("WineProductCollectionName");

            //===========================================================
            // Use connection string and DB name to get our DB
            var client = new MongoClient(_connectionString);
            Database = client.GetDatabase(_databaseName);

            CustomerCollection = Database.GetCollection<Customer>(_customerCollection);
            WineProductCollection = Database.GetCollection<WineProduct>(_wineProductCollection);
        }

        //==============================================================

    }
}
