using Microsoft.EntityFrameworkCore;
using TestTaskParcer.Data;
using TestTaskParcer.Data.Models;
using TestTaskParcer.Repositories.Interfaces;

namespace TestTaskParcer.Repositories;

public class SubGroupRepository : ISubGroupRepository
{
    private readonly AppDbContext _context;

    public SubGroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SubGroup> Add(string name, string image, List<Detail> details)
    {
        var item = await _context.SubGroups.AddAsync(new SubGroup
        {
            Name = name,
            ImageName = image,
            Details = details,
        });

        await _context.SaveChangesAsync();
        return item.Entity;
    }

    public async Task<SubGroup?> Get(int id)
    {
        return await _context.SubGroups.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<SubGroup?> GetWith(int id)
    {
        return await _context.SubGroups.Include(i => i.Details).FirstOrDefaultAsync(f => f.Id == id);
    }
}
