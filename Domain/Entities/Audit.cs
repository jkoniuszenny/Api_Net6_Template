using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Audit", Schema = "Log")]
public class Audit : CreateBaseEntity
{
    [MaxLength(128)]
    public string TableName { get; set; }
    [MaxLength(50)]
    public string Action { get; set; }
    public string KeyValues { get; set; }
    public string OldValues { get; set; }
    public string NewValues { get; set; }
}

