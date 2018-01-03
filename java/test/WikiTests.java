import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

import java.util.ArrayList;
import java.util.List;

import org.junit.Test;

public class WikiTests {
	@Test
	public void test_create_pages() {
		WikiPage rootPage = new WikiPage("FrontPage",
				"some text on the root page", new String[] { "foo", "bar" });
		WikiPage child_page = new WikiPage("Child1", "a child page",
				new String[] { "foo" });
		rootPage.add_child(child_page);
		assertEquals("FrontPage", rootPage.title());
		List<String> children = new ArrayList<String>();
		for (WikiPage p : rootPage.children())
			children.add(p.title());
		assertTrue(children.contains("Child1"));
		List<String> parents = new ArrayList<String>();
		for (WikiPage p : child_page.parents())
			parents.add(p.title());
		assertTrue(parents.contains("FrontPage"));
	}

	@Test
	public void test_uri() {
		WikiPage rootPage = new WikiPage("FrontPage",
				"some text on the root page", new String[] { "foo", "bar" },
				"/blah");
		WikiPage child_page = new WikiPage("Child1", "a child page",
				new String[] { "foo" });
		rootPage.add_child(child_page);
		WikiPage grandchild_page = new WikiPage("Child2", "a child page",
				new String[] { "foo" });
		child_page.add_child(grandchild_page);
		assertEquals("/blah", rootPage.uri());
		assertEquals("/blah/Child1", child_page.uri());
		assertEquals("/blah/Child1/Child2", grandchild_page.uri());
	}
}
