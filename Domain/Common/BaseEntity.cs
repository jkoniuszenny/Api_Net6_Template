namespace Domain.Common;

public class BaseEntity : CreateBaseEntity
{
    public DateTime? ModifiedTmsTmp { get; set; }
    public string? ModifiedUser { get; set; }
}
