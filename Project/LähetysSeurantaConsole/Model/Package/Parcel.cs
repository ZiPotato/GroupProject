
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
        public string? URL { get; set; }
        public string? StatusDescription { get; init; }
        public DateTimeOffset? DeliveredAt{ get; init; }
        public DateTime LastUpdated = DateTime.Now;
        public IReadOnlyList<ParcelEvent> Events { get; init; } = [];
        public bool IsDelivered => DeliveredAt is not null;
        public override string ToString() 
        {
             return $"" +            // Yes. There's probably a better way to do this...
            $"ID              : {TrackingId}\n" +
            $"Carrier company : {Company}\n" +
            $"Current status  : {StatusDescription}\n" +
            $"Current city    : {Events.Last().Location}\n"; 
        }
    }

    public sealed record ParcelEvent
    {
        public DateTimeOffset? Timestamp { get; init; }
        public string? Description { get; init; }
        public string? Location { get; init; }
        public override string ToString()
        {
            return $"" +
                $"Time     : {Timestamp}\n" +
                $"Status   : {Description}\n" +
                $"Location : {Location}\n";
        }
    }
}
