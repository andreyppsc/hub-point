using HubPoint.Services.Common.Abstractions.Commands;

namespace HubPoint.Services.Identity.Api;

public class TestCommand : ICommand
{
    public string Message { get; set; } = default!;
}