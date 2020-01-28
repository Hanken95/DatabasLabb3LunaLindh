using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Labb3
{
    class DatabaseClass
    {
        MongoClient client;
        string _databaseName;
        IMongoDatabase database;
        IMongoCollection<Restaurant> collection;
        List<Restaurant> restaurants;
        FilterDefinitionBuilder<Restaurant> filterBuilder;
        public DatabaseClass(string databaseName, string collectionName)
        {
            client = new MongoClient();
            _databaseName = databaseName;
            database = client.GetDatabase(databaseName);
            collection = database.GetCollection<Restaurant>(collectionName);
        }
        public void InsertDocuments()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = new ObjectId("5c39f9b5df831369c19b6bca"),
                    Name = "Sun Bakery Trattoria",
                    Stars = 4,
                    Categories = new List<string>
                    {
                        "Pizza",
                        "Pasta",
                        "Italian",
                        "Coffee",
                        "Sandwiches"
                    }
                },
                new Restaurant
                {
                    Id = new ObjectId("5c39f9b5df831369c19b6bcb"),
                    Name = "Blue Bagels Grill",
                    Stars = 3,
                    Categories = new List<string>
                    {
                        "Bagels",
                        "Cookies",
                        "Sandwiches"
                    }
                },
                new Restaurant
                {
                    Id = new ObjectId("5c39f9b5df831369c19b6bcc"),
                    Name = "Hot Bakery Cafe",
                    Stars = 4,
                    Categories = new List<string>
                    {
                        "Bakery",
                        "Cafe",
                        "Coffee",
                        "Dessert"
                    }
                },
                new Restaurant
                {
                    Id = new ObjectId("5c39f9b5df831369c19b6bcd"),
                    Name = "XYZ Coffee Bar",
                    Stars = 5,
                    Categories = new List<string>
                    {
                        "Coffee",
                        "Cafe",
                        "Cafe",
                        "Chocolates"
                    }
                },
                new Restaurant
                {
                    Id = new ObjectId("5c39f9b5df831369c19b6bce"),
                    Name = "456 Cookies Shop",
                    Stars = 4,
                    Categories = new List<string>
                    {
                        "Bakery",
                        "Cookies",
                        "Cake",
                        "Coffee",
                    }
                }
            };
            foreach (var restaurant in restaurants)
            {
                collection.InsertOne(restaurant);
            }
        }
        public void FindAllDocuments()
        {
            filterBuilder = Builders<Restaurant>.Filter;
            var empty = filterBuilder.Empty;
            var cursor = collection.Find(empty);
            var restaurants = cursor.ToList();
            foreach (var restaurant in restaurants)
            { 
                Console.WriteLine(restaurant.ToBsonDocument());
            }
        }
        public void FindCafeDocuments()
        {
            filterBuilder = Builders<Restaurant>.Filter;
            var cafeFilter = filterBuilder.Eq("Categories", "Cafe");
            var projectionBuilder = Builders<Restaurant>.Projection;
            var project = projectionBuilder.Include("Name").Exclude("_id");  
            var cursor = collection.Find(cafeFilter).Project(project);
            
            foreach (var restaurant in cursor.ToList())
            {
                Console.WriteLine(restaurant.ToBsonDocument());
            }
        }
        public void IncrementXYZStars()
        {
            var updateBuilder = Builders<Restaurant>.Update;
            var incrementStars = updateBuilder.Inc("Stars", 1);
            filterBuilder = Builders<Restaurant>.Filter;
            var filter = filterBuilder.Eq("Name", "XYZ Coffee Bar");
            collection.UpdateOne(filter, incrementStars);
        }
        public void UpdateName()
        {
            var updateBuilder = Builders<Restaurant>.Update;
            var setName = updateBuilder.Set("Name", "123 Cookies Heaven");
            filterBuilder = Builders<Restaurant>.Filter;
            var filter = filterBuilder.Eq("Name", "456 Cookies Shop");
            collection.UpdateOne(filter, setName);
        }
        public void AggregateAllFourOrMoreStarsRestaurants()
        {
            filterBuilder = Builders<Restaurant>.Filter;
            var starFilter = filterBuilder.Gt("Stars", 3);
            var projectBuilder = Builders<Restaurant>.Projection;
            var projection = projectBuilder.Include("Name").Include("Stars").Exclude("_id");
            var restaurants = collection.Aggregate().Match(starFilter).Project(projection).ToList();
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant);
            }
        }
        public void DeleteDatabase()
        {
            client.DropDatabase(_databaseName);
        }

    }
}
