using AutoMapper;
using RequestFlow.Models;

namespace RequestFlow.Mapping;

/// <summary>
/// AutoMapper profile for Presentation layer - DTO to ViewModel mappings.
/// Add mappings when Application DTOs are used.
/// </summary>
public class PresentationMappingProfile : Profile
{
    public PresentationMappingProfile()
    {
        // TODO: Add mappings when Application DTOs exist
        // CreateMap<RequestDto, RequestDetailViewModel>();
        // CreateMap<RequestDto, RequestListViewModel>();
        // CreateMap<RequestCreateViewModel, CreateRequestCommand>();
    }
}
