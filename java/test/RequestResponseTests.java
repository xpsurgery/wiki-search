import static org.junit.Assert.*;

import java.util.HashMap;

import org.junit.Test;

public class RequestResponseTests {

	@Test
	public void test_request_response_cycle() {
		WikiPage root_page = new WikiPage("FrontPage", "/");
		Request request = new Request("GET", "/");
		Response response = new WikiApp(root_page).handle_request(request);
		assertEquals("FrontPage", response.page().title());
	}

	@Test
	public void test_request_a_page() {
		WikiPage root_page = new WikiPage("FrontPage", "/");
		WikiPage child_page = new WikiPage("Child1", "a child page", new String[] { "foo" });
		root_page.add_child(child_page);
		WikiApp myapp = new WikiApp(root_page);
		Request request = new Request("GET", "/Child1");
		Response response = myapp.handle_request(request);
		assertEquals("Child1", response.page().title());
	}

	@Test
	public void test_request_a_search() {
		WikiPage root_page = new WikiPage("FrontPage", "/");
		WikiPage child_page = new WikiPage("Child1", "a child page",
				new String[] { "foo" });
		root_page.add_child(child_page);
		WikiApp myapp = new WikiApp(root_page);
		Request request = new Request("POST", "/",
				new HashMap<String, String>() {
					{
						put("search_text", "child");
					}
				});
		Response response = myapp.handle_request(request);
		assertEquals("Search Results", response.page().title());
		assertTrue(response.page().text().contains("Child1"));
	}

	@Test
	public void test_request_where_used() {
		WikiPage root_page = new WikiPage("FrontPage", "/");
		WikiPage child_page = new WikiPage("Child1",
				"a child page referencing FrontPage", new String[] { "foo" });
		root_page.add_child(child_page);
		WikiApp myapp = new WikiApp(root_page);
		Request request = new Request("POST", "/",
				new HashMap<String, String>() {
					{
						put("where_used", "FrontPage");
					}
				});
		Response response = myapp.handle_request(request);
		assertTrue(response.page().title().contains("Where Used"));
		assertTrue(response.page().text().contains("Child1"));
	}

	@Test
	public void test_request_property_search() {
		WikiPage root_page = new WikiPage("FrontPage", "/");
		WikiPage child_page = new WikiPage("Child1", "a child page",
				new String[] { "foo", "bar" });
		WikiPage child2_page = new WikiPage("Child2", "a second child page",
				new String[] { "foo" });
		root_page.add_child(child_page);
		root_page.add_child(child2_page);
		Request request = new Request("POST", "/",
				new HashMap<String, String>() {
					{
						put("tags", "bar");
					}
				});
		Response response = new WikiApp(root_page).handle_request(request);
		assertTrue(response.page().title().contains("Property Search"));
		assertTrue(response.page().text().contains("Child1"));
		assertFalse(response.page().text().contains("Child2"));
	}

	@Test
	public void test_search_replace() {
		WikiPage root_page = new WikiPage("FrontPage", "/");
		WikiPage child_page = new WikiPage("Child1",
				"a child page with text baz", new String[] {});
		root_page.add_child(child_page);
		WikiApp myapp = new WikiApp(root_page);
		Request request = new Request("POST", "/",
				new HashMap<String, String>() {
					{
						put("search_text", "baz");
						put("replace", "foo");
					}
				});
		Response response = myapp.handle_request(request);
		assertTrue(response.page().title().contains("Search/Replace"));
		assertTrue(response.page().text().contains("Child1"));
		Response child_page_response = myapp.handle_request(new Request("GET", "/Child1"));
		assertTrue(child_page_response.page().text().contains("foo"));
		assertFalse(child_page_response.page().text().contains("baz"));
	}
}