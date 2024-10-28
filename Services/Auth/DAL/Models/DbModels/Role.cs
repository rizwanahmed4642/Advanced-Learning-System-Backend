﻿using System;
using System.Collections.Generic;

namespace Auth.DAL.Models.DbModels;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public bool IsShowAdmin { get; set; }

    public string? RoleType { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public string? RoutingUrl { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
