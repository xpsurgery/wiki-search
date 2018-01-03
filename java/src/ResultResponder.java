import java.util.ArrayList;
import java.util.List;


public abstract class ResultResponder {
	protected Request request;

	public Response make_response(Request request, RequestContext context) {
		this.request = request;
		List<WikiPage> allPages = new DepthFirstTraverser(context.root_page())
				.traverse();
		List<WikiPage> matchingPages = new ArrayList<WikiPage>();
		for (WikiPage p : allPages) {
			if (traverse(p))
				matchingPages.add(p);
		}
		String result = "found term in pages:<ul>";
		for (WikiPage found : matchingPages) {
			result += "<li>" + found.title() + "</li>";
		}
		result += "</ul>";
		WikiPage results_page = new WikiPage(title(), result, new String[] {});
		return new Response(results_page);
	}

	protected abstract String title();

	protected abstract boolean traverse(WikiPage page);

	public Request request() {
		return this.request;
	}
}