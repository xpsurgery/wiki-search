using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class WikiApp
	{
		WikiPage rootPage;

		public WikiApp(WikiPage root_page)
		{
			this.rootPage = root_page;
		}

		public Response HandleRequest(Request request) {
			ResultResponder responder = ResponderBuilder.ResponderFor(request);
			return responder.MakeResponse(request, new RequestContext(rootPage));
		}
	}
}

