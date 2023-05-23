using Microsoft.EntityFrameworkCore;
using TestTaskParcer.Data;
using TestTaskParcer.Data.Models;
using TestTaskParcer.Repositories.Interfaces;

namespace TestTaskParcer.Repositories;

public class CarComplectationRepository : ICarComplectationRepsoitory
{
    private readonly AppDbContext _context;

    public CarComplectationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CarComplectation> Add(string complectation, string date, string engine, string body, string grade, string atm, string gear, string position, string doors, string destination, List<PartGroup> group)
    {
        var item = await _context.CarsComplectations.AddAsync(new CarComplectation
        {
            ComplectationId = complectation,
            Date = date,
            Engine = engine,
            ATM = atm,
            Body = body,
            Destination = destination,
            Doors = doors,
            Gear = grade,
            Grade = grade,
            Position = position,
            PartGroups = group
        });
        await _context.SaveChangesAsync();
        return item.Entity;
    }

    public async Task<CarComplectation?> Get(int id)
    {
        return await _context.CarsComplectations.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<CarComplectation?> GetWith(int id)
    {
        return await _context.CarsComplectations.Include(i => i.PartGroups).FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<bool> UpdateCarId(int id, int carId)
    {
        var item = await Get(id);
        if (item == null)
        {
            return false;
        }

        item.CarId = carId;
        _context.Entry(item).CurrentValues.SetValues(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
