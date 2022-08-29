﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Products.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }

    }
}
