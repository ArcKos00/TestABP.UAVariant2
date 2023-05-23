using Microsoft.EntityFrameworkCore;
using TestTaskParcer.Data;
using TestTaskParcer.Data.Models;
using TestTaskParcer.Repositories.Interfaces;

namespace TestTaskParcer.Repositories;

public class DetailRepository : IDetailRepository
{
    private readonly AppDbContext _context;

    public DetailRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Detail> Add(string treeCode, string tree, List<Infos> info)
    {
        var item = await _context.Details.AddAsync(new Detail
        {
            Id = treeCode,
            Name = tree,
            Infos = info
        });

        await _context.SaveChangesAsync();
        return item.Entity;
    }

    public async Task<Detail?> Get(string id)
    {
        return await _context.Details.FirstOrDefaultAsync(f => f.Id == id);
    }
}
