using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("OfferStatus", Schema = "LST")]
public class OfferStatus : BaseEntity
{
    public string Name { get; set; }
}