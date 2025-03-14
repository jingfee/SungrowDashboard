using SungrowDashboard.Models;

namespace SungrowDashboard.Services;

public interface IDataTableService
{
    public Task<List<BatteryData>> GetAllDataValues();
}