using Microsoft.EntityFrameworkCore;
using TestTaskParcer.Data;
using TestTaskParcer.Data.Models;
using TestTaskParcer.Repositories.Interfaces;

namespace TestTaskParcer.Repositories;

public class CarModelRepository : ICarModelRepository
{
    private readonly AppDbContext _context;

    public CarModelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CarModel> Add(int id, string name, string date, string complectation, List<CarComplectation> complectations)
    {
        var item = await _context.Cars.AddAsync(new CarModel
        {
            CarId = id,
            Name = name,
            Date = date,
            Complectation = complectation,
            Complectations = complectations
        });

        await _context.SaveChangesAsync();
        return item.Entity;
    }

    public async Task<CarModel?> Get(int id)
    {
        return await _context.Cars.FirstOrDefaultAsync(f => f.CarId == id);
    }

    public async Task<CarModel?> GetWith(int id)
    {
        return await _context.Cars.Include(i => i.Complectations).FirstOrDefaultAsync(f => f.CarId == id);
    }
}
