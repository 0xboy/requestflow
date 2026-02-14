using AutoMapper;
using RequestFlow.Application.Features.Requests.Create;
using RequestFlow.Application.Features.Requests.GetList;
using RequestFlow.Application.Models.Filters;
using RequestFlow.Models;
using RequestFlow.Shared.Constants;

namespace RequestFlow.Mapping;

/// <summary>
/// Presentation: Application filter → Query, ViewModel → Command.
/// </summary>
public class PresentationMappingProfile : Profile
{
    public PresentationMappingProfile()
    {
        // Application filter → MediatR query (controller'da UserId/IsManager doldurulur)
        CreateMap<RequestListFilterModel, GetRequestListQuery>();

        // Create: ViewModel → Command (UserId controller'da eklenir)
        CreateMap<RequestCreateViewModel, CreateRequestCommand>()
            .ForMember(d => d.Priority, o => o.MapFrom(s => ParsePriority(s.Priority)))
            .ForMember(d => d.UserId, o => o.Ignore());
    }

    private static Priority ParsePriority(string? value)
    {
        return Enum.TryParse<Priority>(value, true, out var p) ? p : Priority.Medium;
    }
}
