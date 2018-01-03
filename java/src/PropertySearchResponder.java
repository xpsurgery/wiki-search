import java.util.ArrayList;
import java.util.List;


class PropertySearchResponder extends ResultResponder {
	public String title() {
		return "Property Search: " + search_for_tags().toString();
	}

	public boolean traverse(WikiPage page) {
		List<String> commonTags = new ArrayList<String>();
		for (String tag : search_for_tags()) {
			if (page.tags().contains(tag))
				commonTags.add(tag);
		}
		return commonTags.size() > 0;
	}

	private String[] search_for_tags() {
		return request.data().get("tags").split("/");
	}
}