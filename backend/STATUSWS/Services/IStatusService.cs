using StatusWS.Dtos;

namespace StatusWS.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync();
        Task<StatusDto> GetStatusByIdAsync(int id);
        Task DeleteStatusAsync(int id);
    }
}
