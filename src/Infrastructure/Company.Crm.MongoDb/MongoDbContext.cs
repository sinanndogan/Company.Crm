using MongoDB.Driver;

namespace Company.Crm.MongoDb;

public abstract class MongoDbContext
{
    private readonly MongoClient client;
    private readonly IMongoDatabase db;

    public readonly IMongoCollection<AuditLog> auditLogs;
    public readonly IMongoCollection<AuditLog> exceptionLogs;

    public MongoDbContext()
    {
        client = new MongoClient("mongodb://localhost:27017");
        db = client.GetDatabase("company-crm");

        auditLogs = db.GetCollection<AuditLog>("auditlogs");
        exceptionLogs = db.GetCollection<AuditLog>("exceptionlogs");
    }
}