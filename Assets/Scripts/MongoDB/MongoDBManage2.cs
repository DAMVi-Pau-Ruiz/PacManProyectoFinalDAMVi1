using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

public class MongoDBManage : MonoBehaviour
{
    private IMongoCollection<BsonDocument> collection;

    private async void Start()
    {
        string connectionString =
            "mongodb+srv://a25pauruibar_db_user:MongoDB-PACMAN@pacman.mp7s18p.mongodb.net/?appName=pacman";

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("PacmanDB");
        collection = database.GetCollection<BsonDocument>("Sessions");

        Debug.Log("Conectado a MongoDB Atlas");

        await InsertDummyData();
    }

    private async Task InsertDummyData()
    {
        var doc = new BsonDocument
        {
            { "player", "Pau" },
            { "testValue", Random.Range(0, 9999) },
            { "timestamp", System.DateTime.UtcNow }
        };

        await collection.InsertOneAsync(doc);

        Debug.Log("Documento insertado en Atlas");
    }
}