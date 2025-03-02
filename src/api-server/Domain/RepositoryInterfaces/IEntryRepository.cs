using Domain.Entities;
using Domain.Entities.JMDict;

namespace Domain.RepositoryInterfaces;

public interface IEntryRepository : IBaseRepository<Entry>
{
    Task<List<Entry>> BulkReadAllAsync();
    Task BulkInsertAsync(List<Entry> entries);
    Task<Entry?> ReadByEntSeq(string ent_seq);
    Task<List<Entry>> Search(string query);
}