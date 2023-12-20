using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VTBlog.Core.Domain.Identity
{
   [Table("AppRoles")]
    public class AppRole: IdentityRole<Guid>
    {
        [Required]
        [MaxLength(200)]
        public required string DisplayName { get; set; }
    }
}
