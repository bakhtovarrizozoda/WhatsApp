using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services.StatusService;

public interface IStatusService
{
    public Task<List<GetStatusFullInfoDto>> GetStatusFullInfo();
    public Task<Response<List<GetStatusDto>>> GetStatus();
    public Task<Response<GetStatusDto>> GetStatusById(int id);
    public Task<Response<GetStatusDto>> AddStatus(AddStatusDto status);
    public Task<Response<GetStatusDto>> UpdateStatus(AddStatusDto status);
    public Task<Response<bool>> DeleteStatus(int id);
}