using HubPoint.Services.Common.Abstractions.Domain;

namespace HubPoint.Services.Accounting.Api.Domain.ChartOfAccounts;

public class ChartOfAccount : EntityBase
{
    public int Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;
}