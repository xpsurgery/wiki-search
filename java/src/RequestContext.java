
class RequestContext {
	private WikiPage page;

	public RequestContext(WikiPage root_page) {
		this.page = root_page;
	}

	public WikiPage root_page() {
		return page;
	}
}