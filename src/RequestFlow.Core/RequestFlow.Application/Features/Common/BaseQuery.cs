using MediatR;

namespace RequestFlow.Application.Features.Common;

/// <summary>
/// Base for CQRS queries - IRequest returns TResponse
/// </summary>
public abstract record BaseQuery<TResponse> : IRequest<TResponse>;
