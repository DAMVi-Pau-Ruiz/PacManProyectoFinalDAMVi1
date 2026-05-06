using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

public class MongoDBManager : MonoBehaviour
{
    private IMongoCollection<BsonDocument> collection;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        string connectionString =
            "mongodb+srv://a25pauruibar_db_user:MongoDB-PACMAN@pacman.mp7s18p.mongodb.net/?appName=pacman";

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("PacmanDB");
        collection = database.GetCollection<BsonDocument>("Sessions");

        Debug.Log("Conectado a MongoDB Atlas");
    }

    public async Task SaveSession(string player, float duracion, int bolasComidas, int fantasmasComidos, int nivelesCompletados)
    {
        var doc = new BsonDocument
        {
            { "player", player },
            { "duracion", duracion },
            { "bolasComidas", bolasComidas },
            { "fantasmasComidos", fantasmasComidos },
            { "nivelesCompletados", nivelesCompletados },
            { "timestamp", System.DateTime.UtcNow }
        };

        await collection.InsertOneAsync(doc);

        Debug.Log("Sesión guardada en MongoDB");
    }
}
