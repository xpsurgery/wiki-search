using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class Request
	{
		internal readonly string Uri;
		internal readonly Dictionary<string, string> Data;
		internal readonly string RequestType;

		public Request(string request_type, string uri, Dictionary<string,string> hashMap)
		{
			this.RequestType = request_type;
			this.Uri = uri;
			this.Data = hashMap;
		}

		public Request(string request_type, string uri) : this(request_type, uri, new Dictionary<string, string>())
		{
		}
	}
}

