

class ResponderBuilder {
	public static ResultResponder responder_for(Request request) {
		if (request.request_type() == "GET")
			return new WikiPageResponder();
		if (request.request_type() == "POST") {
			if (request.data().containsKey("search_text")) {
				if (request.data().containsKey("replace")) {
					return new SearchReplaceResponder();
				} else {
					return new SearchResponder();
				}
			}
			if (request.data().containsKey("where_used")) {
				return new WhereUsedResponder();
			}
			if (request.data().containsKey("tags")) {
				return new PropertySearchResponder();
			}
		}
		throw new IllegalArgumentException();
	}
}
