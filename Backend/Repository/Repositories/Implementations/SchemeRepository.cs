using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations
{
    public class SchemeRepository : ISchemeRepository
    {
        private readonly AppDbContext _context;
        public SchemeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateSchemeAsync(Scheme scheme)
        {
            await _context.Schemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
        }

        public async Task<Scheme> GetSchemeAsync(int schemeId)
        {
            return await _context.Schemes
                .Include(s => s.User)
                .Include(s => s.Tables)
                    .ThenInclude(t => t.Attributes)
                        .ThenInclude(t => t.ConnectionsTo)
                .Include(s => s.Tables)
                    .ThenInclude(t => t.Attributes)
                        .ThenInclude(t => t.ConnectionsFrom)
                .Include(s => s.Tables)
                    .ThenInclude(t => t.Attributes)
                        .ThenInclude(t => t.DataType)

                .FirstOrDefaultAsync(s => s.Id == schemeId);
        }

        public async Task<IEnumerable<Scheme>> GetUserSchemesAsync(int userId)
        {
            return await _context.Schemes
                .Include(s => s.User)
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task DeleteSchemeasync(Scheme scheme)
        {
            _context.Schemes.Remove(scheme);
            await _context.SaveChangesAsync();
        }
    }
}
