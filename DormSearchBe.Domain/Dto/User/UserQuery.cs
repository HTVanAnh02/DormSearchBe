﻿using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.User
{
    public class UserQuery : BaseEntity
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public Guid? RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool? Is_Active { get; set; }
    }
}