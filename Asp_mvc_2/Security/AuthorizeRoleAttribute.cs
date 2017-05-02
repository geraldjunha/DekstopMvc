using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp_mvc_2.Models.DB;
using Asp_mvc_2.Models.EntityManager;
using Asp_mvc_2.Security;

namespace Asp_mvc_2.Security
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;
        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            using (Demo1DBEntitiesEntities db = new Demo1DBEntitiesEntities())
            {
                UserManager UM = new UserManager();
                foreach (var roles in userAssignedRoles)
                {
                    authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
        }
    }
}