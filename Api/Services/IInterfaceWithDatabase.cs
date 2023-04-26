using AcmeStudiosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeStudiosApi.Services
{
    public interface IInterfaceWithDatabase
    {
        Task<ServiceResponse<GetStudioItemDto>> AddStudioItemAsync(AddStudioItemDto newStudioItem);
        Task<ServiceResponse<GetStudioItemDto>> DeleteStudioItemAsync(int id);
        Task<ServiceResponse<IEnumerable<GetStudioItemDto>>> GetAllStudioItemsAsync();
        Task<ServiceResponse<IEnumerable<GetStudioItemTypeDto>>> GetAllStudioItemTypesAsync();
        Task<ServiceResponse<GetStudioItemDto>> GetStudioItemByIdAsync(int id);
        Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem);
    }
}