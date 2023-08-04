using HubPoint.Services.Common.Abstractions.Domain;

namespace HubPoint.Services.Accounting.Api.Domain.Accounts;

public class ChartOfAccounts : EntityBase
{
    public Guid Type { get; set; }
}