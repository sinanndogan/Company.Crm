using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Framework.Entity;

public abstract class BaseEntity<TKey> : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}