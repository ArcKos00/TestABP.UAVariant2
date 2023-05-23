using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Repositories.Interfaces;

public interface ICarComplectationRepsoitory
{
    public Task<CarComplectation> Add(string complectation, string date, string engine, string body, string grade, string atm, string gear, string position, string doors, string destination, List<PartGroup> groups);
    public Task<CarComplectation?> Get(int id);
    public Task<CarComplectation?> GetWith(int id);
    public Task<bool> UpdateCarId(int id, int carId);
}
