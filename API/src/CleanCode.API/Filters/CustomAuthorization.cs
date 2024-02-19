using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using CleanCode.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanCode.Api.Filters
{
    public class CustomAuthorization : IAsyncAuthorizationFilter
    {
        private readonly ISsoService _ssoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CustomAuthorization> _logger;

        public CustomAuthorization(ISsoService ssoService, IHttpContextAccessor httpContextAccessor,
            ILogger<CustomAuthorization> logger)
        {
            _ssoService = ssoService ?? throw new ArgumentNullException(nameof(ssoService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>  
        /// Authorize User  
        /// </summary>  
        /// <returns></returns>  
        public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null) return;

            var hasAllowAnonymous = filterContext.ActionDescriptor.EndpointMetadata
                .Any(em => em.GetType() == typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute));

            if (hasAllowAnonymous) return;

            filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var authTokens);

            var token = authTokens.FirstOrDefault();

            token = token?.Replace("Bearer ", "");

            if (token != null)
            {
                if (await _ssoService.ValidateToken(token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var tokenInformation = handler.ReadToken(token) as JwtSecurityToken;
                    _httpContextAccessor.HttpContext?.Request.HttpContext.Items.Add("UserId",
                        (tokenInformation?.Claims ?? Array.Empty<Claim>()).FirstOrDefault(x => x.Type == "pid")
                        ?.Value);
                    _httpContextAccessor.HttpContext?.Request.HttpContext.Items.Add("UserToken", token);
                }
                else
                {
                    //_logger.LogUnauthorizedAccess("Authentication", "Invalid Token");
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.Result = new UnauthorizedResult();
                }
            }
            else
            {
                //_logger.LogUnauthorizedAccess("Authentication", "Token is NULL");
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                filterContext.Result = new UnauthorizedResult();
            }
        }
    }
}
