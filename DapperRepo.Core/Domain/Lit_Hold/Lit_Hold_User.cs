using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperRepo.Core.Domain.Lit_Hold
{
    [Table(nameof(Lit_Hold_User))]
    public class Lit_Hold_User : BaseEntity
    {
        public string display_name { get; set; }

        public string email_address { get; set; }

    }
}
