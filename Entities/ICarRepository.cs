using DataTransferObject;

namespace Entities;

public interface ICarRepository
{
     Task CreateAsync(CarDto carDto);
     Task DeleteAsync(CarDto carDto);
     Task DeleteAsync(Guid guid);
     Task UpdateAsync(CarDto carDtoFrom, CarDto carDtoTo);
     List<CarDto> GetAll();
     CarDto Get(Guid guid);
     List<string> GetAllBodyType();
     List<string> GetAllBrandName();
}