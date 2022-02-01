using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace asp.net_api_employe_sql
{
    public class basicauth : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
              string authtojen=  actionContext.Request.Headers.Authorization.Parameter;
               string decodtoken =Encoding.UTF8.GetString( Convert.FromBase64String(authtojen));
                string[] usernamepassword= decodtoken.Split(':');
               string username= usernamepassword[0];
                string password = usernamepassword[1];
                if (employesecurti.login(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);

                }

                else
                {

                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);

                }
            }
        }
    }
}