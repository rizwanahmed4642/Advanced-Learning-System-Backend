using System;
using System.Collections.Generic;

namespace LMS.DAL.Models.DbModels
{
    public partial class User
    {
        public User()
        {
            Teachers = new HashSet<Teacher>();
            UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public Guid? GenderTypeProfileId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? ActionTypeId { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
