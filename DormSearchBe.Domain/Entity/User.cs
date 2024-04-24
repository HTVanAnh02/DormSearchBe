using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public Guid? RoleId { get; set; }
        public bool? Is_Active { get; set; }
        public string? Role { get; set; }
        public Role? Roles { get; set; }
        public virtual ICollection<Refresh_Token>? Refresh_Tokens { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; }
        public ICollection<Houses>? Houses { get; set; }
        public User() 
        {
            Role = " Admin";
            Avatar = "";
        }
    }
}
