
class WhereUsedResponder extends ResultResponder {
	public String title() {
		return "Where Used: " + search_for_page();
	}

	public boolean traverse(WikiPage page) {
		return page.text().contains(search_for_page());
	}

	private String search_for_page() {
		return request.data().get("where_used");
	}
}