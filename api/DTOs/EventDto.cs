public class EventDto
{
    public Guid? Id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Date { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Location { get; set; }
    public bool AdminApproval { get; set; }
}
