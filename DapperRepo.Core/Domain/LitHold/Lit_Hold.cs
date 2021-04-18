using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperRepo.Core.Domain.LitHold
{
    [Table(nameof(Lit_Hold))]
    public class Lit_Hold : BaseEntity
    {
        public string Work_Order { get; set; }
        public string Matter_No { get; set; }

        public DateTime? Begin_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public string Case_Name { get; set; }

        public string Notes { get; set; }

        public List<Lit_Hold_User> Lit_Hold_Users { get; set;}
    }
}
