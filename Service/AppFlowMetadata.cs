using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Gmail.v1;
using Google.Apis.Util.Store;

using System;
using System.Web.Mvc;

namespace GmailOAuth.Service
{
    public class AppFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "180602927859-ehluudns2b03kpffstnvfkhg7hoqifak.apps.googleusercontent.com",
                    ClientSecret = "iy4_5FbDCPzFuE5fcPySlLQR"
                },
                Scopes = new[] { GmailService.Scope.GmailModify, GmailService.Scope.GmailReadonly },
                DataStore = new FileDataStore(System.Web.HttpContext.Current.Server.MapPath("/json-file"), true)
            });

        public override string GetUserId(Controller controller)
        {
            // In this sample we use the session to store the user identifiers.
            // That's not the best practice, because you should have a logic to identify
            // a user. You might want to use "OpenID Connect".
            // You can read more about the protocol in the following link:
            // https://developers.google.com/accounts/docs/OAuth2Login.
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();

        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}
