using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class WikiPageResponder : ResultResponder
	{
		public override Response MakeResponse(Request request, RequestContext context)
		{
			List<WikiPage> allPages = new DepthFirstTraverser(context.RootPage).Traverse();
			foreach (WikiPage p in allPages)
				if (p.Uri == request.Uri)
					return new Response(p, "200");
			return new Response(new WikiPage("404 page"), "404");
		}

		public override string Title()
		{
			throw new NotImplementedException();
		}

		public override bool Traverse(WikiPage page)
		{
			throw new NotImplementedException();
		}
	}
}

