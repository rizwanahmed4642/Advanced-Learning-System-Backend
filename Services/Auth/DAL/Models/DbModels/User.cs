using System;
using System.Collections.Generic;

namespace DAL.Models.DbModels;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
}
