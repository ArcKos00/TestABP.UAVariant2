using TestTaskParcer.Data.Models;
using TestTaskParcer.Repositories.Interfaces;
using TestTaskParcer.Service.Interfaces;

namespace TestTaskParcer.Service;

public class GuideService : IGuideService
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly ICarComplectationRepsoitory _carComplectationRepsoitory;
    private readonly IPartGroupRepository _partGroupRepository;
    private readonly ISubGroupRepository _subGroupRepository;
    private readonly IDetailRepository _detailRepository;

    public GuideService(ICarModelRepository carModelRepository, ICarComplectationRepsoitory carComplectationRepsoitory, IPartGroupRepository partGroupRepository, ISubGroupRepository subGroupRepository, IDetailRepository detailRepository)
    {
        _carModelRepository = carModelRepository;
        _carComplectationRepsoitory = carComplectationRepsoitory;
        _partGroupRepository = partGroupRepository;
        _subGroupRepository = subGroupRepository;
        _detailRepository = detailRepository;
    }

    public async Task<CarComplectation> AddComplectation(string complectation, string date, string engine, string body, string grade, string atm, string gear, string position, string doors, string destination, List<PartGroup> groups)
    {
        return await _carComplectationRepsoitory.Add(complectation, date, engine, body, grade, atm, gear, position, doors, destination, groups);
    }

    public async Task<CarModel> AddCar(int id, string name, string date, string complectation, List<CarComplectation> complectations)
    {
        return await _carModelRepository.Add(id, name, date, complectation, complectations);
    }

    public async Task<Detail> AddDetail(string treeCode, string tree, List<Infos> infos)
    {
        return await _detailRepository.Add(treeCode, tree, infos);
    }

    public async Task<PartGroup> AddParentGroup(string name, List<SubGroup> groups)
    {
        return await _partGroupRepository.Add(name, groups);
    }

    public async Task<SubGroup> AddSubGroup(string name, string imageName, List<Detail> details)
    {
        return await _subGroupRepository.Add(name, imageName, details);
    }
}
