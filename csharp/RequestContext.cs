using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class RequestContext
	{
		internal readonly WikiPage RootPage;

		public RequestContext(WikiPage rootPage)
		{
			this.RootPage = rootPage;
		}
	}
}

