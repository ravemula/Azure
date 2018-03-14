using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonApp.Constants
{
    public static class AppConstants
    {
        public const string ClientId = "App Id [update]";
        public const string ClientSecret = "App password [update]";
        public const string GraphV2Uri = "https://login.microsoftonline.com/{0}/v2.0";
        public const string TenentName = "AD Tenant name [update] ";
        public const string RedirectUrl = "App Redirect URL [update]";
        public const string ApplicationDefaultScope = "https://graph.microsoft.com/.default";
        public const string Bearer = "Bearer";
        public const string AuthorizatioHeader = "Authorization";
        public const string GraphApiV1Url = "https://graph.microsoft.com/v1.0/users/{0}/memberOf";
        public const string ExtensionAttachedForAd = "#EXT#@";
    }
}
