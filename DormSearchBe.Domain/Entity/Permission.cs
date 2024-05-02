using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Permission:BaseEntity
    {
        public string? PermissionId { get; set; }

        public string? PermissionName { get; set; }

    }
}
