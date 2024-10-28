﻿using System;
using System.Collections.Generic;

namespace Auth.DAL.Models.DbModels;

public partial class ProfileType
{
    public Guid ProfileTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public Guid? ParentProfileTypeId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual ICollection<Profile> Profiles { get; } = new List<Profile>();
}
