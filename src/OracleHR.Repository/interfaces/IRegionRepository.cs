using OracleHR.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OracleHR.Repository.interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetRegions();

        Task<Region> GetRegion(int id);

        Task<Region> UpdateRegion(Region region, int regionId);

        Task<Region> AddNewRegion(Region region);

        Task<int> DeleteRegion(int regionId);
    }
}
