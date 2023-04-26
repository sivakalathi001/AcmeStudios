using AcmeStudiosApi.DbContexts;
using AcmeStudiosApi.Entities;
using AcmeStudiosApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeStudiosApi.Services
{
    public class InterfaceWithDatabase : IInterfaceWithDatabase
    {
        private readonly AcmeStudiosContext _context;
        private readonly IMapper _mapper;


        public InterfaceWithDatabase(AcmeStudiosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> AddStudioItemAsync(AddStudioItemDto newStudioItem)
        {

            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            var studioItemTypeEntity = await _context.StudioItemTypes
                .FirstOrDefaultAsync(c => c.StudioItemTypeId == newStudioItem.StudioItemTypeId);

            if (studioItemTypeEntity == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"StudioItemTypeId : {newStudioItem.StudioItemTypeId} is not found";
                return serviceResponse;
            }

            var studioItemEntity = _mapper.Map<StudioItem>(newStudioItem);
            await _context.StudioItems.AddAsync(studioItemEntity);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItemEntity);
            serviceResponse.Message = $"New item added.  Id: {studioItemEntity.StudioItemId}";
            serviceResponse.Success = true;

            return serviceResponse;

        }

        public async Task<ServiceResponse<IEnumerable<GetStudioItemDto>>> GetAllStudioItemsAsync()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetStudioItemDto>>();

            var studioItemEntity = await _context.StudioItems
                .OrderBy(a => a.Name)
                .ToListAsync();

            if (studioItemEntity == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "StudioItem list is empty";
                return serviceResponse;
            }

            serviceResponse.Data = _mapper.Map<IEnumerable<GetStudioItemDto>>(studioItemEntity);
            serviceResponse.Message = "Here's all the items in studio";
            serviceResponse.Success = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemByIdAsync(int id)
        {
            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            var studioItemEntity = await _context.StudioItems
            .Where(item => item.StudioItemId == id)
            .Include(type => type.StudioItemType)
            .FirstOrDefaultAsync();

            if (studioItemEntity == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"StudioItemId : {id} is not found";
                return serviceResponse;
            }

            serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItemEntity);
            serviceResponse.Message = $"Here's your selected studio item Id: {studioItemEntity.StudioItemId}";
            serviceResponse.Success = true;

            return serviceResponse;

        }

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItemAsync(UpdateStudioItemDto updatedStudioItem)
        {

            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            var studioItemEntity = await _context.StudioItems
                .FirstOrDefaultAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);

            if (studioItemEntity == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"StudioItemId : {updatedStudioItem.StudioItemId} is not found";
                return serviceResponse;
            }

            var studioItemTypeEntity = await _context.StudioItemTypes
                .FirstOrDefaultAsync(c => c.StudioItemTypeId == updatedStudioItem.StudioItemTypeId);

            if (studioItemTypeEntity == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"StudioItemTypeId : {updatedStudioItem.StudioItemTypeId} is not found";
                return serviceResponse;
            }

            _mapper.Map(updatedStudioItem, studioItemEntity);

            _context.StudioItems.Update(studioItemEntity);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItemEntity);
            serviceResponse.Message = $"Update successful for studio item Id: {studioItemEntity.StudioItemId}";
            serviceResponse.Success = true;

            return serviceResponse;

        }

        public async Task<ServiceResponse<GetStudioItemDto>> DeleteStudioItemAsync(int id)
        {

            var serviceResponse = new ServiceResponse<GetStudioItemDto>();

            var studioItemEntity = await _context.StudioItems
                .FirstOrDefaultAsync(c => c.StudioItemId == id);

            if (studioItemEntity == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"StudioItemId : {id} is not found";
                return serviceResponse;
            }

            _context.Remove(studioItemEntity);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItemEntity);
            serviceResponse.Success = true;
            serviceResponse.Message = $"Item deleted for studio item Id: {studioItemEntity.StudioItemId}";

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetStudioItemTypeDto>>> GetAllStudioItemTypesAsync()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetStudioItemTypeDto>>();

            var studioItemTypeEntity = await _context.StudioItemTypes
                .OrderBy(s => s.Value)
                .ToListAsync();

            if (studioItemTypeEntity == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "StudioItemType list is empty";
                return serviceResponse;
            }

            serviceResponse.Data = _mapper.Map<IEnumerable<GetStudioItemTypeDto>>(studioItemTypeEntity);
            serviceResponse.Message = "Here's all the item in Studio Types";
            serviceResponse.Success = true;

            return serviceResponse;

        }
    }

    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = false;

        public string Message { get; set; } = null;
    }

}
