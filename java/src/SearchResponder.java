
class SearchResponder extends ResultResponder {
	public String title() {
		return "Search Results";
	}

	public boolean traverse(WikiPage page) {
		String search_term = request.data().get("search_text");
		return page.text().contains(search_term);
	}
}