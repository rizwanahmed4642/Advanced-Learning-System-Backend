using System;
using System.Collections.Generic;

namespace Auth.DAL.Models.DbModels;

public partial class Library
{
    public Guid LibraryId { get; set; }

    public string BookName { get; set; } = null!;

    public Guid SubjectTypeProfileId { get; set; }

    public string WriterName { get; set; } = null!;

    public Guid ClassTypeProfileId { get; set; }

    public string? IdNo { get; set; }

    public DateTime PublishingDate { get; set; }

    public DateTime UploadDate { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ActionTypeId { get; set; }
}
