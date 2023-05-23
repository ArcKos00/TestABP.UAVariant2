using Microsoft.EntityFrameworkCore;
using TestTaskParcer.Data;
using TestTaskParcer.Data.Models;
using TestTaskParcer.Repositories.Interfaces;

namespace TestTaskParcer.Repositories;

public class PartGroupRepository : IPartGroupRepository
{
    private readonly AppDbContext _context;

    public PartGroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PartGroup> Add(string name, List<SubGroup> subGropus)
    {
        var item = await _context.PartGroups.AddAsync(new PartGroup
        {
            Name = name,
            Groups = subGropus
        });

        await _context.SaveChangesAsync();
        return item.Entity;
    }

    public async Task<PartGroup?> Get(int id)
    {
        return await _context.PartGroups.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<PartGroup?> GetWith(int id)
    {
        return await _context.PartGroups.Include(i => i.Groups).FirstOrDefaultAsync(f => f.Id == id);
    }
}
