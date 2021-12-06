using AutoMapper;
using WarehouseBackend.DataAccess.Entities;
using WarehouseBackend.DataAccess.Entities.Results;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;
using WarehouseBackend.Models.ApiResponses;

namespace WarehouseBackend.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContainerEntity, Container>().ReverseMap();
            CreateMap<AddContainerRequest, ContainerEntity>();

            CreateMap<ContainerEntityResults, ContainerResults>().ReverseMap();
        }
    }
}
