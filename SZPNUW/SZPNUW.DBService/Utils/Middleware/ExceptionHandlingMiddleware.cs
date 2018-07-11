using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SZPNUW.Base.Resources;
using SZPNUW.Data;

namespace SZPNUW.DBService
{ 
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Service service = new Service();

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                try
                {
                    SysLogModel sysLogModel = new SysLogModel();
                    sysLogModel.Name = ex.GetType().FullName;
                    if (ex.InnerException != null)
                        sysLogModel.Details = string.Join(". ", ex.Message, ex.InnerException.Message, ex.StackTrace);
                    sysLogModel.Details = string.Join(". ", ex.Message, ex.StackTrace);
                    sysLogModel.Date = DateTime.Now;
                    service.InsertSysLog(sysLogModel);
                }
                catch
                {
                }
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(PortalMessages.InternalError);
            }
            
        }
    }
}
