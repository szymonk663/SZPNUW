using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SZPNUW.Base;
using SZPNUW.Base.Consts;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public class RefreshSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public RefreshSessionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.User.IsLogedIn() && context.Session.GetItem<UserModel>() == null)
            {
                int userId = Convert.ToInt32(context.User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                try
                {
                    Service service = new Service();
                    UserModel user = service.GetUser(userId);
                    if (user != null)
                    {
                        context.Session.AddItem<UserModel>(user);
                    }
                }
                catch
                {
                    try
                    {
                        context.SignOutAsync(Consts.AuthenticateScheme);
                        context.Session.Clear();
                    }
                    catch
                    {
                    }
                }
            }
            return this._next(context);
        }
    }
}
