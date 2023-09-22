using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Demo.WebUI.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<Claim> claims)
        {
            Claims = claims ?? throw new ArgumentNullException(nameof(claims));
        }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
