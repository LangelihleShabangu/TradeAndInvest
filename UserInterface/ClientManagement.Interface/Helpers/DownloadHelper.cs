using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.Interface
{
	public class DownloadHelper : ActionResult
	{
		public string FileName { get; set; }
		public string FileType { get; set; }
        public string No { get; set; }
		public byte[] File { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.Buffer = true;
			context.HttpContext.Response.Clear();
			context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
			context.HttpContext.Response.ContentType = FileType;
			context.HttpContext.Response.BinaryWrite(File);
		}
	}
}

