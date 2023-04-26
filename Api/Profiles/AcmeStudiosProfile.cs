using AutoMapper;

namespace AcmeStudiosApi.Profiles
{
    public class AcmeStudiosProfile : Profile
    {
        public AcmeStudiosProfile()
        {
            CreateMap<Entities.StudioItem, Models.GetStudioItemDto>();
            CreateMap<Models.AddStudioItemDto, Entities.StudioItem>();
            CreateMap<Models.UpdateStudioItemDto, Entities.StudioItem>();
            CreateMap<Entities.StudioItemType, Models.GetStudioItemTypeDto>();
        }
    }
}