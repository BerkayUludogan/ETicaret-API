using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ETicaret.API.Attributes
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public JwtAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
        public JwtAuthorizeAttribute(string roles)
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
            Roles = roles;
        }
    }
}
