using System;
using System.Collections.Generic;

namespace LXP.Common;

public partial class Profile
{
    public Guid ProfileId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public string? Domain { get; set; }

    public Guid? UserId { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Updatedby { get; set; }

    public DateTime? Updateddate { get; set; }
}
