using OracleHR.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OracleHR.Repository.interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetRegionsAsync();

        Task<Region> UpdateRegionAsync(Region region, int regionId);

        Task<Region> AddNewRegionAsync(Region region);

        Task<int> DeleteRegionAsync(int regionId);
    }
}
