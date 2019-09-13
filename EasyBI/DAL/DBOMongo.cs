using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EasyBI.DAL
{
    public class DBOMongo
    {
		public static BsonDocument GetBsonDocument(Object obj)
		{
			BsonDocument document = obj.ToBsonDocument();
			var jsonDocument = document.ToJson();
			return document;
		}


		public static async void InsertAsync(Object obj, string collectionName , string connectionString
			 , string databaseName)
		{
			// Create a MongoClient object by using the connection string
			var client = new MongoClient(connectionString);

			//Use the MongoClient to access the server
			var database = client.GetDatabase(databaseName);

			//get mongodb collection
			var collection = database.GetCollection<BsonDocument>(collectionName);

			await collection.InsertOneAsync(GetBsonDocument(obj));

		}

		public static async void UpdateObject(ObjectId objectId, Object obj, string collectionName, string connectionString 
			, string databaseName)
		{
			// Create a MongoClient object by using the connection string
			var client = new MongoClient(connectionString);

			//Use the MongoClient to access the server
			var database = client.GetDatabase(databaseName);

			//get mongodb collection
			var collection = database.GetCollection<BsonDocument>(collectionName);
			var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
			await collection.ReplaceOneAsync(filter, GetBsonDocument(obj));
		}


		public static async void DeleteObject(ObjectId objectId, string collectionName , string connectionString 
			, string databaseName )
		{
			// Create a MongoClient object by using the connection string
			var client = new MongoClient(connectionString);

			//Use the MongoClient to access the server
			var database = client.GetDatabase(databaseName);

			//get mongodb collection
			var collection = database.GetCollection<BsonDocument>(collectionName);

			await collection.DeleteOneAsync(
							 Builders<BsonDocument>.Filter.Eq("_id", objectId));
		}
	}
}
