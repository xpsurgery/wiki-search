
class Response {
	private WikiPage page;
	private String http_code;

	public Response(WikiPage page, String http_code) {
		this.http_code = http_code;
		this.page = page;
	}

	public Response(WikiPage page) {
		this(page, "200");
	}

	public WikiPage page() {
		return page;
	}
}