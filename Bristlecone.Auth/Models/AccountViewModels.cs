using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bristlecone.Auth.Models
{
    // Models returned by AccountController actions.
    [ExcludeFromCodeCoverage]
    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
