using StatusWS.Dtos;

namespace StatusWS.Services
{
    public interface IStatusTypeService
    {
        Task<IEnumerable<StatusTypeDto>> GetAllStatusTypesAsync();
        Task<StatusTypeDto> GetStatusTypeByIdAsync(int id);
        Task<StatusTypeDto> CreateStatusTypeAsync(StatusTypeCreateDto statusTypeCreateDto);
        Task UpdateStatusTypeAsync(int id, StatusTypeCreateDto statusTypeCreateDto);
        Task DeleteStatusTypeAsync(int id);
    }
}
