using MediatR;

namespace RequestFlow.Application.Features.Common;

/// <summary>
/// Base for domain/cqrs events used with MediatR INotification
/// </summary>
public abstract record BaseEvent : INotification;
