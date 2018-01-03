using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class SearchReplaceResponder : ResultResponder
	{
		public override string Title()
		{
			return "Search/Replace: " + SearchText() + "/" + ReplaceText();
		}

		public override bool Traverse(WikiPage page)
		{
			if (page.Text.Contains(SearchText())) {
				page.SetText(page.Text.Replace(SearchText(), ReplaceText()));
				return true;
			}
			return false;
		}

		private string SearchText()
		{
			return request.Data["search_text"];
		}

		private string ReplaceText()
		{
			return request.Data["replace"];
		}
	}
}

