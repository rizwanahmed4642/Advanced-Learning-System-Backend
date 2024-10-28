using System;
using System.Collections.Generic;

namespace Auth.DAL.Models.DbModels;

public partial class User
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ActionTypeId { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
