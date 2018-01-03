using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class SearchResponder : ResultResponder
	{
		public override string Title()
		{
			return "Search Results";
		}

		public override bool Traverse(WikiPage page)
		{
			string searchTerm = request.Data["search_text"];
			return page.Text.Contains(searchTerm);
		}
	}
}

