using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class WhereUsedResponder : ResultResponder
	{
		public override string Title()
		{
			return "Where Used: " + SearchForPage();
		}

		public override bool Traverse(WikiPage page)
		{
			return page.Text.Contains(SearchForPage());
		}

		private string SearchForPage()
		{
			return request.Data["where_used"];
		}
	}
}

