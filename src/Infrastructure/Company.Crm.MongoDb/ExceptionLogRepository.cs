using MongoDB.Driver;

namespace Company.Crm.MongoDb
{
    public class ExceptionLogRepository : MongoDbContext
    {
        public async Task<List<AuditLog>> GetAsync() =>
          await exceptionLogs.Find(_ => true).ToListAsync();

        public async Task<AuditLog?> GetAsync(string id) =>
            await exceptionLogs.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(AuditLog log) =>
            await exceptionLogs.InsertOneAsync(log);

        public async Task UpdateAsync(string id, AuditLog log) =>
            await exceptionLogs.ReplaceOneAsync(x => x.Id == id, log);

        public async Task RemoveAsync(string id) =>
            await exceptionLogs.DeleteOneAsync(x => x.Id == id);
    }
}
