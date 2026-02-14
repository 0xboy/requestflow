using AutoMapper;
using RequestFlow.Application.Models;
using RequestFlow.Application.Models.Requests;
using RequestFlow.Application.Models.RequestTypes;
using RequestFlow.Domain.Entities;

namespace RequestFlow.Infrastructure.Mapping;

/// <summary>
/// Infrastructure: Entity ↔ Application Model mappings.
/// </summary>
public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<Request, RequestModel>()
            .ForMember(d => d.RequestTypeName, o => o.MapFrom(s => s.RequestType.Name))
            .ForMember(d => d.CanEdit, o => o.Ignore())
            .ForMember(d => d.CanApprove, o => o.Ignore());

        CreateMap<Request, RequestListModel>();

        CreateMap<RequestModel, Request>()
            .ForMember(d => d.RequestType, o => o.Ignore())
            .ForMember(d => d.StatusHistory, o => o.Ignore())
            .ForMember(d => d.ModifiedDate, o => o.Ignore());

        CreateMap<RequestType, RequestTypeModel>();
        CreateMap<RequestTypeModel, RequestType>()
            .ForMember(d => d.Description, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Requests, o => o.Ignore());

        CreateMap<CreateRequestModel, Request>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.RequestNo, o => o.Ignore())
            .ForMember(d => d.Status, o => o.Ignore())
            .ForMember(d => d.CreatedDate, o => o.Ignore())
            .ForMember(d => d.ModifiedDate, o => o.Ignore())
            .ForMember(d => d.RequestType, o => o.Ignore())
            .ForMember(d => d.StatusHistory, o => o.Ignore());

        // Repo/servisten gelen veriyi model'e map et; elle new DashboardModel yazmamak için
        CreateMap<DashboardData, DashboardModel>();
    }
}
