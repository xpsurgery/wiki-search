using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class Response
	{
		internal readonly WikiPage Page;
		internal readonly string HttpCode;

		public Response(WikiPage page, string httpCode)
		{
			this.HttpCode = httpCode;
			this.Page = page;
		}

		public Response(WikiPage page) : this(page, "200")
		{
		}
	}
}

