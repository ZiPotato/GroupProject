
namespace LähetysSeurantaConsole.Model.Package
{
    /// <summary>
    /// This is what we use to actually mold all of the companyDTO:s into same shape so it will be easier to use later.
    /// It is a basic record that we just use to get the desired variables later when we are for example presenting data to the user.
    /// </summary>
    public sealed record Parcel
    {
        public required string TrackingId { get; init; }
        public required string Company { get; init; }
        public string URL { get; set; }
        public string? CurrentStatus { get; init; }
        public string? StatusDescription { get; init; }

        public DateTimeOffset? EstimatedDelivery { get; init; }
        public DateTimeOffset? DeliveredAt { get; init; }

        public string? RecipientName { get; init; }
        public string? ServiceName { get; init; }

        public IReadOnlyList<ParcelEvent> Events { get; init; } = [];

        public bool IsDelivered => DeliveredAt is not null;
        public override string ToString() { return $"{TrackingId},\n{CurrentStatus}, \n{StatusDescription}"; }
    }

    public sealed record ParcelEvent
    {
        public DateTimeOffset? Timestamp { get; init; }
        public string? Status { get; init; }
        public string? Description { get; init; }
        public string? Location { get; init; }
    }
}
