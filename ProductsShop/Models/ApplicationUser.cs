﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("ApplicationUser")]
    public class ApplicationUser : IdentityUser
    {

    }
}
