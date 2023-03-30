using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientManagement.Interface.Helper
{
    [Serializable]
    public static class NotificationHelper
    {
        public static string Notification(string title, string text, bool sticky)
        {
            string script;

            script = string.Format(@"addNotification('{0}','{1}',{2});", title, text, sticky ? "true" : "false");

            return script;
        }

        public static string showAlertPopup(string header, string content, cssStyle.cssStyleEnum style, bool RemoveAllBackdropsOnClose = false, string executeAddionalJavascript = null)
        {
            string script;

            script = string.Format(@"showAlertPopup('{0}','{1}','{2}',{3});{4}", header, content, cssStyle.GetStyle(style), RemoveAllBackdropsOnClose ? "true" : "false", (!string.IsNullOrEmpty(executeAddionalJavascript) ? executeAddionalJavascript : string.Empty));

            return script;
        }

        public static string showErrorPopup()
        {
            string script;

            script = string.Format(@"showErrorPopup();");

            return script;
        }       
    }

    [Serializable]
    public class cssStyle
    {
        public enum cssStyleEnum
        {
            Success = 1,
            Info = 2,
            Warning = 3,
            Danger = 4
        }

        public static string GetStyle(cssStyleEnum style)
        {
            switch (style)
            {
                case cssStyleEnum.Success:
                    return "alert alert-success";
                case cssStyleEnum.Info:
                    return "alert alert-info";
                case cssStyleEnum.Warning:
                    return "alert alert-warning";
                case cssStyleEnum.Danger:
                    return "alert alert-danger";
            }

            return "";
        }
    }
}