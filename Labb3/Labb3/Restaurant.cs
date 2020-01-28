using MongoDB.Bson;
using System.Collections.Generic;

namespace Labb3
{
    internal class Restaurant
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public List<string> Categories { get; set; }
        public Restaurant()
        {
        }
    }
}