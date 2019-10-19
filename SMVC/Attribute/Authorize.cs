using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SMVC
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AuthorizeAttribute : Attribute
    {
        private string role;
        private string user;
        private string[] roles;
        private string[] users;

        private string[] splitestring(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            return (from s in str.Split(',')
                    let u = s.Trim()
                    where u.Length > 0
                    select u).ToArray();
        }

        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                roles = splitestring(value);
            }
        }

        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                users = splitestring(value);
            }
        }

        internal bool IsAuthorizedARequest(HttpContext context)
        {
            if (!context.Request.IsAuthenticated)
                return false;
            if (users != null && users.Contains(context.User.Identity.Name) == false)
                return false;
            if (roles != null && roles.Count(context.User.IsInRole) == 0)
                return false;
            return true;
        }
    }
}
