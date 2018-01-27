using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Persistence.Tests
{
    static class MongoTestDbProvider
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string Database = "NotaBlog_TEST";

        private static MongoClient _mongoClient = new MongoClient(ConnectionString);

        public static IMongoDatabase GetDatabase()
        {
            return _mongoClient.GetDatabase(Database);
        }
    }
}
