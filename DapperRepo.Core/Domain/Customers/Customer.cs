﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperRepo.Core.Domain.Customers
{
    [Table(nameof(Customer))]
    public class Customer : BaseEntity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
