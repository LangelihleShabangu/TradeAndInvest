using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.Interface.Helper
{
    [Serializable]
    public static class PartialViewResultExtensions
    {
        public static JavascriptResult AppendJavascript(this PartialViewResult partial, params string[] scriptArray)
        {
            return new JavascriptResult(partial, string.Join("\r\n", scriptArray));
        }
    }

    public class JavascriptResult : PartialViewResult
    {
        public JavascriptResult(PartialViewResult innerPartial, string script)
        {
            this.InnerPartialViewResult = innerPartial;
            this.Script = script;
        }

        protected PartialViewResult InnerPartialViewResult { get; set; }
        protected string Script { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            TextWriter old = context.HttpContext.Response.Output;
            StringBuilder sb = new StringBuilder();

            using (var tw = new StringWriter(sb))
            {
                context.HttpContext.Response.Output = tw;
                this.InnerPartialViewResult.ExecuteResult(context);
                context.HttpContext.Response.Output = old;
            }

            var scriptElement = new TagBuilder("script");
            scriptElement.InnerHtml = Script;

            context.HttpContext.Response.ContentType = "text/html";
            context.HttpContext.Response.Write(String.Format(@"{0} {1} ", sb.ToString(), scriptElement.ToString()));
        }
    }
}