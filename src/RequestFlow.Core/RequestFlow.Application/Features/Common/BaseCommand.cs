using MediatR;

namespace RequestFlow.Application.Features.Common;

/// <summary>
/// Base for CQRS commands - IRequest returns TResponse
/// </summary>
public abstract record BaseCommand<TResponse> : IRequest<TResponse>;
