using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Role
{
    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
      

    }
}
