using MongoDB.Driver;

namespace Company.Crm.MongoDb
{
    public class AuditLogRepository : MongoDbContext
    {
        public async Task<List<AuditLog>> GetAsync() =>
          await auditLogs.Find(_ => true).ToListAsync();

        public async Task<AuditLog?> GetAsync(string id) =>
            await auditLogs.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(AuditLog log) =>
            await auditLogs.InsertOneAsync(log);

        public async Task UpdateAsync(string id, AuditLog log) =>
            await auditLogs.ReplaceOneAsync(x => x.Id == id, log);

        public async Task RemoveAsync(string id) =>
            await auditLogs.DeleteOneAsync(x => x.Id == id);
    }
}
