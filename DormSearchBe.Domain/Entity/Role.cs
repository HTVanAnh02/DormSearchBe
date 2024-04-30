using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
