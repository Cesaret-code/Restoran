﻿using System.ComponentModel.DataAnnotations;

namespace Restoran.ViewModels.Account
{
    public class LoginVm
    {

        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
