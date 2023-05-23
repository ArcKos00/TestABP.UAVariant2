using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Service.Interfaces;

public interface IGuideService
{
    public Task<CarComplectation> AddComplectation(string complectation, string date, string engine, string body, string grade, string atm, string gear, string position, string doors, string destination, List<PartGroup> groups);
    public Task<CarModel> AddCar(int id, string name, string date, string complectation, List<CarComplectation> complectations);
    public Task<Detail> AddDetail(string treeCode, string tree, List<Infos> infos);
    public Task<PartGroup> AddParentGroup(string name, List<SubGroup> groups);
    public Task<SubGroup> AddSubGroup(string name, string imageName, List<Detail> details);
}
