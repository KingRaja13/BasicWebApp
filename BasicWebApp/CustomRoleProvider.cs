using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BasicWebApp.Data;
namespace BasicWebApp
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        // custom get logged in user role from db
        public override string[] GetRolesForUser(string username)
        {
            var Customrole= new string[] { " " };
            using (var context = new WebAppLogin())
            {

                var res = context.LoginDetails.Where(x => x.Username == username).FirstOrDefault();
                if (res != null)
                {

                     Customrole = new string[] { res.Userrole };
                }

                return Customrole;
            }

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}