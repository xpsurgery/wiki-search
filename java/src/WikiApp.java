class WikiApp {
	WikiPage root_page;

	public WikiApp(WikiPage root_page) {
		this.root_page = root_page;
	}

	public Response handle_request(Request request) {
		ResultResponder responder = ResponderBuilder.responder_for(request);
		return responder.make_response(request, new RequestContext(root_page));
	}
}
