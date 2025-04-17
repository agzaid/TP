﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }

        public int? RoleId { get; set; }
    }
}
