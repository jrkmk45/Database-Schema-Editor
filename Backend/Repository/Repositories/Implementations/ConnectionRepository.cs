using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly AppDbContext _context;
        public ConnectionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateConnectionAsync(Connection connection)
        {
            await _context.AddAsync(connection);
            await _context.SaveChangesAsync();
        }

        public async Task<Connection> GetConnectionAsync(int connectionId)
        {
            return await _context.Connections.FirstOrDefaultAsync(c => c.Id == connectionId);
        }

        public async Task DeleteConnectionAsync(Connection connection)
        {
            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();
        }
    }
}
