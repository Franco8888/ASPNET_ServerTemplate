using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerTemplate.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("customerName")]       // Method for invoking different name in DB than the model.  
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string TelephoneNumber { get; set; } = string.Empty;
    }
}
