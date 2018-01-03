using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class ResponderBuilder
	{
		public static ResultResponder ResponderFor(Request request)
		{
			if (request.RequestType == "GET")
				return new WikiPageResponder();
			if (request.RequestType == "POST")
			{
				if (request.Data.ContainsKey("search_text"))
				{
					if (request.Data.ContainsKey("replace"))
						return new SearchReplaceResponder();
					else
						return new SearchResponder();
				}
				if (request.Data.ContainsKey("where_used"))
					return new WhereUsedResponder();
				if (request.Data.ContainsKey("tags"))
					return new PropertySearchResponder();
			}
			throw new InvalidOperationException();
		}
	}
}

