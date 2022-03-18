namespace Domain.Common;

public class CreateBaseEntity
{
    public int Id { get; set; }
    public byte[] RowGuid { get; set; }
    public DateTime CreateTmsTmp { get; set; }
    public string CreateUser { get; set; }
}
