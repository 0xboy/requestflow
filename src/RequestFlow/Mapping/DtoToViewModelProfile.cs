using AutoMapper;
using RequestFlow.Application.DTOs;
using RequestFlow.Models;

namespace RequestFlow.Mapping;

/// <summary>
/// Application DTOs → Razor ViewModels (enum'lar string'e map edilir).
/// </summary>
public class DtoToViewModelProfile : Profile
{
    public DtoToViewModelProfile()
    {
        CreateMap<RequestListDto, RequestListViewModel>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Priority, o => o.MapFrom(s => s.Priority.ToString()));

        CreateMap<RequestDto, RequestDetailViewModel>()
            .ForMember(d => d.RequestType, o => o.MapFrom(s => s.RequestTypeName))
            .ForMember(d => d.CreatedBy, o => o.MapFrom(s => s.CreatedByUserId))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Priority, o => o.MapFrom(s => s.Priority.ToString()))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description ?? string.Empty));

        CreateMap<DashboardDto, DashboardViewModel>()
            .ForMember(d => d.RecentRequests, o => o.MapFrom(s => s.RecentRequests));
    }
}
