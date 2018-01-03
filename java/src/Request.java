import java.util.HashMap;

class Request {
	private String uri;
	private HashMap<String, String> data;
	private String request_type;

	public Request(String request_type, String uri, HashMap<String,String> hashMap) {
		this.request_type = request_type;
		this.uri = uri;
		this.data = hashMap;
	}

	public Request(String request_type, String uri) {
		this(request_type, uri, new HashMap<String, String>());
	}

	public String request_type() {
		return this.request_type;
	}

	public HashMap<String, String> data() {
		return this.data;
	}

	public String uri() {
		return this.uri;
	}
}
