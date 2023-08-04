using HubPoint.Services.Common.Abstractions.Domain;

namespace HubPoint.Services.Accounting.Api.Domain.Accounts;

public class Account : EntityBase
{
    public Guid AccountId { get; set; }

    public Guid PartyId { get; set; }

    public Money Balance { get; set; } = default!;
}