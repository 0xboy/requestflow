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
        // Application filter → MediatR query (UserId/IsManager set in controller)
        CreateMap<RequestListFilterModel, GetRequestListQuery>();

        // Create: ViewModel → Command (UserId added in controller with { UserId = ... })
        // Record has only positional constructor; ConstructUsing tells AutoMapper how to build it.
        CreateMap<RequestCreateViewModel, CreateRequestCommand>()
            .ConstructUsing(s => new CreateRequestCommand(
                s.Title,
                s.Description,
                s.RequestTypeId,
                ParsePriority(s.Priority),
                string.Empty));
    }

    private static Priority ParsePriority(string? value)
    {
        return Enum.TryParse<Priority>(value, true, out var p) ? p : Priority.Medium;
    }
}
