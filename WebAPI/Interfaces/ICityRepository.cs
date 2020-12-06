using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICityRepository
    {
         Task<IEnumerable<City>> GetCitiesAsync();

         void AddCity(City city);

         void DeletCity(int cityId);
         
         Task<City> FindCity(int id);  
    }
}