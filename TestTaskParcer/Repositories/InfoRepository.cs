using TestTaskParcer.Data;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Repositories;

public class InfoRepository
{
    private readonly AppDbContext _context;

    public InfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Infos> Add(string count, string date, string info)
    {
        var item = await _context.Info.AddAsync(new Infos
        {
            Count = count,
            Date = date,
            Info = info
        });

        await _context.SaveChangesAsync();
        return item.Entity;
    }
}
