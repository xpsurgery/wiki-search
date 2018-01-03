using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class PropertySearchResponder : ResultResponder
	{
		public override string Title()
		{
			return "Property Search: " + SearchForTags().ToString();
		}

		public override bool Traverse(WikiPage page)
		{
			List<string> commonTags = new List<string>();
			foreach (string tag in SearchForTags())
				if (page.Tags().Contains(tag))
					commonTags.Add(tag);
			return commonTags.Count > 0;
		}

		private string[] SearchForTags() {
			return request.Data["tags"].Split('/');
		}
	}
}

