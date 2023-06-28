using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Attribute = Domain.Models.Attribute;

namespace Repository.Repositories.Implementations
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly AppDbContext _context;
        public AttributeRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public async Task CreateAttributeAsync(Attribute attribute)
        {
            await _context.Attributes.AddAsync(attribute);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAttributeAsync(Attribute attribute)
        {
            _context.Attributes.Remove(attribute);
            await _context.SaveChangesAsync();
        }

        public async Task<Attribute> GetAttributeAsync(int attributeId)
        {
            return await _context.Attributes
                .Include(a => a.DataType)
                .FirstOrDefaultAsync(a => a.Id == attributeId);
        }

        public async Task DeleteAttributeConnectionsAsync(int attributeId)
        {
            var connections = _context.Connections.Where(c => c.AttributeFromId == attributeId).ToList();
            _context.Connections.RemoveRange(connections);
            await _context.SaveChangesAsync();
        }
    }
}
