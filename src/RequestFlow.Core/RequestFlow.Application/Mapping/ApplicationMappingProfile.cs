using AutoMapper;
using RequestFlow.Application.DTOs;
using RequestFlow.Application.Features.Requests.Create;
using RequestFlow.Application.Features.Dashboard;
using RequestFlow.Application.Features.Requests.GetDetail;
using RequestFlow.Application.Features.Requests.GetList;
using RequestFlow.Application.Models;
using RequestFlow.Application.Models.Filters;
using RequestFlow.Application.Models.Requests;
using RequestFlow.Application.Models.RequestTypes;

namespace RequestFlow.Application.Mapping;

/// <summary>
/// Application layer: Model -> DTO mappings, Command -> Model, Query -> Filter.
/// </summary>
public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<RequestStatusHistoryItemModel, RequestStatusHistoryItemDto>();
        CreateMap<RequestModel, RequestDto>().ReverseMap();
        CreateMap<RequestListModel, RequestListDto>().ReverseMap();
        CreateMap<RequestTypeModel, RequestTypeDto>().ReverseMap();
        CreateMap<DashboardModel, DashboardDto>().ReverseMap();

        CreateMap<CreateRequestCommand, CreateRequestModel>();

        // Query -> Filter (via generic base UserContextFilterModel)
        CreateMap<GetRequestListQuery, RequestListFilterModel>();
        CreateMap<GetDashboardQuery, DashboardFilterModel>();
        CreateMap<GetRequestDetailQuery, RequestDetailFilterModel>();
    }
}
