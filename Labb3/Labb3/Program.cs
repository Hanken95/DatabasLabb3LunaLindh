using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Labb3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var dataBase = new DatabaseClass("RestaurantDB", "Restaurants");
            dataBase.DeleteDatabase();
            dataBase.InsertDocuments();
            dataBase.FindAllDocuments();
            dataBase.FindCafeDocuments();
            dataBase.IncrementXYZStars();
            Console.WriteLine("");
            dataBase.UpdateName();
            dataBase.FindAllDocuments();
            dataBase.AggregateAllFourOrMoreStarsRestaurants();
        }
    }
}
