using System.Data;
using AutoMapper;
using DataTransferObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entities;

public class CarRepository:ICarRepository
{
    private readonly ILogger<CarRepository> _logger;
    private readonly AppCarContext _appContext;
    private readonly IMapper _mapper;

    public CarRepository(ILogger<CarRepository> logger, AppCarContext appContext, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task CreateAsync(CarDto carDto)
    {
        CheckCarExist(carDto);
        var car = _mapper.Map<Car>(carDto);
        car.CreatedData = DateTime.UtcNow;
        await _appContext.Cars.AddAsync(car);
        await _appContext.SaveChangesAsync();
        _logger.LogInformation($"Create new car Model:{carDto.ModelName}, Brand{carDto.Brand.BrandName}");
    }
    public async Task DeleteAsync(CarDto carDto)
    {
        var car = _mapper.Map<Car>(carDto);
        _appContext.Cars.Remove(car);
        await _appContext.SaveChangesAsync();
        _logger.LogInformation($"Delete car Model:{carDto.ModelName}, Brand{carDto.Brand.BrandName}");

    }
    public async Task DeleteAsync(Guid guid)
    {
        var car = Get(guid);
        var b = _appContext.Cars.First(n=> n.Id.Equals(guid));
        _appContext.Cars.Remove(_appContext.Cars.First(n=> n.Id.Equals(guid)));
        await _appContext.SaveChangesAsync();
        _logger.LogInformation($"Create new car Model:{car.ModelName}, Brand{car.Brand.BrandName}");

    }
    public async Task UpdateAsync(CarDto carDtoFrom, CarDto carDtoTo)
    {
        CheckCarExist(carDtoTo);
        var carFromDB = _appContext.Cars
            .Include(n=>n.Brand)
            .Include(n=>n.BodyType)
            .FirstOrDefault(n => n.Id.Equals(carDtoFrom.Id));
        carFromDB.Brand = _mapper.Map<Brand>(carDtoTo.Brand);
        carFromDB.Image = carDtoTo.Image;
        carFromDB.Url = carDtoTo.Url;
        carFromDB.BodyType = _mapper.Map<BodyType>(carDtoTo.BodyType);
        carFromDB.BrandId = _appContext.Brands.First(n=>n.BrandName.Equals(carDtoTo.Brand.BrandName)).BrandId;
        carFromDB.ModelName = carDtoTo.ModelName;
        carFromDB.SeatsCount = carDtoTo.SeatsCount;
        carFromDB.BodyTypeId = _appContext.BodyTypes.First(n=>n.BodyTypeName.Equals(carDtoTo.BodyType.BodyTypeName)).BodyTypeId;
        await _appContext.SaveChangesAsync();
        _logger.LogInformation($"Update car from Model:{carDtoFrom.ModelName}, Brand{carDtoFrom.Brand.BrandName} To Model:{carDtoTo.ModelName}, Brand{carDtoTo.Brand.BrandName}");

    }
    public List<CarDto> GetAll()
    {
        var listCars = _appContext.Cars
            .Include(n=>n.Brand)
            .Include(n=>n.BodyType)
            .Select(n=> _mapper.Map<CarDto>(n)).ToList();
        return listCars;
    }
    public CarDto Get(Guid guid)
    {
        var car = _appContext.Cars
            .Include(n=>n.Brand)
            .Include(n=>n.BodyType)
            .FirstOrDefault(n => n.Id.Equals(guid));
        return _mapper.Map<CarDto>(car);
    }
    public List<string> GetAllBodyType()
    {
        return _appContext.BodyTypes
            .Select(n => n.BodyTypeName)
            .Distinct()
            .ToList();
    }

    public List<string> GetAllBrandName()
    {
        return _appContext.Brands
            .Select(n => n.BrandName)
            .Distinct()
            .ToList();
    }

    private void CheckCarExist(CarDto carDto)
    {
        var car = _mapper.Map<Car>(carDto);
        if (_appContext.Cars.Any(n=> n.Brand.BrandName.Equals(car.Brand.BrandName) 
                                     && n.ModelName.Equals(car.ModelName) 
                                     &&n.BodyType.BodyTypeName.Equals(car.BodyType.BodyTypeName) 
                                     && n.SeatsCount.Equals(car.SeatsCount)))
        {
            throw new DataException("This Car already has existed");
        }
    }
}