import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

class WikiPage {

	private List<WikiPage> children;
	private String title;
	private List<WikiPage> parents;
	private String uri;
	private String[] tags;
	private String text;

	public WikiPage(String title, String text, String[] tags, String uri) {
		this.title = title;
		this.text = text;
		this.tags = tags;
		this.uri = (uri == null) ? title : uri;
		this.parents = new ArrayList<WikiPage>();
		this.children = new ArrayList<WikiPage>();
	}

	public WikiPage(String title, String text, String[] tags) {
		this(title, text, tags, null);
	}

	public WikiPage(String title, String uri) {
		this(title, "", new String[] {}, uri);
	}

	public WikiPage(String title) {
		this(title, null);
	}

	public List<WikiPage> children() {
		return children;
	}

	public String title() {
		return title;
	}

	public List<WikiPage> parents() {
		return parents;
	}

	public String uri() {
		return uri;
	}

	public String text() {
		return text;
	}

	public String setText(String new_text) {
		text = new_text;
		return new_text;
	}

	public List<String> tags() {
		return Arrays.asList(tags);
	}

	public void add_child(WikiPage page) {
		children.add(page);
		page.add_parent(this);
	}

	public void add_parent(WikiPage page) {
		parents.add(page);
		uri = (page.uri() + "/" + uri).replaceAll("//", "/");
	}
}
