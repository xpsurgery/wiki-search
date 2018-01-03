import java.util.List;

import sun.reflect.generics.reflectiveObjects.NotImplementedException;


class WikiPageResponder extends ResultResponder {
	public Response make_response(Request request, RequestContext context) {
		List<WikiPage> all_pages = new DepthFirstTraverser(context.root_page())
				.traverse();
		for (WikiPage p : all_pages) {
			if (p.uri().equals(request.uri()))
				return new Response(p, "200");
		}
		return new Response(new WikiPage("404 page"), "404");
	}

	@Override
	protected String title() {
		throw new NotImplementedException();
	}

	@Override
	protected boolean traverse(WikiPage page) {
		throw new NotImplementedException();
	}
}