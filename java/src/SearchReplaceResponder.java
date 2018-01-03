
class SearchReplaceResponder extends ResultResponder {
	public String title() {
		return "Search/Replace: " + search_text() + "/" + replace_text();
	}

	public boolean traverse(WikiPage page) {
		if (page.text().contains(search_text())) {
			page.setText(page.text().replaceAll(search_text(), replace_text()));
			return true;
		}
		return false;
	}

	private String search_text() {
		return request.data().get("search_text");
	}

	private String replace_text() {
		return request.data().get("replace");
	}
}