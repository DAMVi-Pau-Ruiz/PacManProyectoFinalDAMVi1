using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

public class MongoDBManage1 : MonoBehaviour
{
    private IMongoCollection<BsonDocument> collection;

    private void Start()
    {
        string connectionString =
            "mongodb+srv://a25pauruibar_db_user:MongoDB-PACMAN@pacman.mp7s18p.mongodb.net/?retryWrites=true&w=majority&appName=pacman";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("PacmanDB");
        collection = database.GetCollection<BsonDocument>("Sessions");

        Debug.Log("Conectado a MongoDB Atlas");
    }

    public async Task SaveSession(SessionData data)
    {
        var doc = data.ToBsonDocument();
        await collection.InsertOneAsync(doc);
        Debug.Log("Sesión guardada en Atlas");
    }
}
