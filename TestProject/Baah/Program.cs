namespace Baah
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Driver;

    class Program
    {
        static void Main(string[] args)
        {
            const string ConnectionString = "mongodb://localhost";
            var client = new MongoClient(ConnectionString);
            IMongoDatabase db = client.GetDatabase("OJS");

            var collection = db.GetCollection<BsonDocument>("SubmissionsForProcessing");

            var doc = new BsonDocument { { "Item", "hehe" } };
            collection.InsertOne(doc);
            Console.WriteLine("kur");
        }
    }
}
