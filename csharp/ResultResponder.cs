using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public abstract class ResultResponder
	{
		protected Request request;

		public virtual Response MakeResponse(Request request, RequestContext context)
		{
			this.request = request;
			List<WikiPage> allPages = new DepthFirstTraverser(context.RootPage).Traverse();
			List<WikiPage> matchingPages = new List<WikiPage>();
			foreach (WikiPage p in allPages)
				if (Traverse(p))
					matchingPages.Add(p);
			string result = "found term in pages:<ul>";
			foreach (WikiPage found in matchingPages)
				result += "<li>" + found.Title + "</li>";
			result += "</ul>";
			WikiPage results_page = new WikiPage(Title(), result, new List<string>());
			return new Response(results_page);
		}

		public abstract string Title();

		public abstract bool Traverse(WikiPage page);

		public Request Request()
		{
			return this.request;
		}
	}
}

