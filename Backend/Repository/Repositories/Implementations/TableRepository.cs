using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _context;
        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task<Table> GetTableAsync(int tableId)
        {
            return await _context.Tables
                .Include(t => t.Attributes)
                    .ThenInclude(a => a.User)
                .Include(t => t.Attributes)
                    .ThenInclude(a => a.ConnectionsTo)
                .Include(t => t.Attributes)
                    .ThenInclude(a => a.ConnectionsFrom)
                .Include(t => t.Attributes)
                    .ThenInclude(a => a.DataType)
                .Include(t => t.User)
                .Include(t => t.Scheme)
                .FirstOrDefaultAsync(t => t.Id == tableId);
        }

        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(Table table)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
}
