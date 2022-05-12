using DataTransferObject;
using Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic;

public class CarServices:ICarServices
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<ICarRepository> _logger;

    public CarServices(ICarRepository carRepository, ILogger<ICarRepository> logger)
    {
        _carRepository = carRepository;
        _logger = logger;
    }

    public async Task CreateAsync(CarDto carDto)
    {
        await _carRepository.CreateAsync(carDto);
    }
    public async Task DeleteAsync(CarDto carDto)
    {
        await _carRepository.DeleteAsync(carDto);
    }
    public async Task DeleteAsync(Guid guid)
    {
        await _carRepository.DeleteAsync(guid);
    }
    public async Task UpdateAsync(CarDto carDtoFrom, CarDto carDtoTo)
    {
        await _carRepository.UpdateAsync(carDtoFrom, carDtoTo);
    }
    public List<CarDto> GetAll()
    {
        return _carRepository.GetAll();
    }
    public CarDto Get(Guid guid)
    {
        return _carRepository.Get(guid);
    }
    public List<string> GetAllBodyType()
    {
        return _carRepository.GetAllBodyType();
    }
    public List<string> GetAllBrandName()
    {
        return _carRepository.GetAllBrandName();
    }
}