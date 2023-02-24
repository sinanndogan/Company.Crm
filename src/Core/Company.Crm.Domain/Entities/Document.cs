using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities;

public class Document : BaseEntity
{
    public int UserId { get; set; }
    public int RequestId { get; set; }
    public string DocumentFileName { get; set; }
    public int DocumentTypeId { get; set; }
}