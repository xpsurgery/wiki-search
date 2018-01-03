import static org.junit.Assert.*;

import java.util.ArrayList;
import java.util.List;

import org.junit.Before;
import org.junit.Test;

public class TraverseTests {

	private WikiPage root;
	private WikiPage child1;
	private WikiPage child2;
	private WikiPage child3;
	private DepthFirstTraverser traverser;

	@Before
	public void setup() {
		this.root = new WikiPage("Root");
		this.child1 = new WikiPage("Child1");
		this.child2 = new WikiPage("Child2");
		this.child3 = new WikiPage("Child3");

		this.root.add_child(this.child2);
		this.root.add_child(this.child1);
		this.child1.add_child(this.child3);

		this.traverser = new DepthFirstTraverser(this.root);
	}

	@Test
	public void test_traverse_pages() {
		List<WikiPage> pages = this.traverser.traverse();
		List<String> visited_in_order = new ArrayList<String>();
		for (WikiPage p : pages)
			visited_in_order.add(p.title());
		assertTrue(visited_in_order.indexOf("Child3") < visited_in_order.indexOf("Child2"));
	}

	@Test
	public void test_traverse_with_loops() {
		this.child3.add_child(this.root);
		List<WikiPage> pages = this.traverser.traverse();
		List<String> visited_in_order = new ArrayList<String>();
		for (WikiPage p : pages)
			visited_in_order.add(p.title());
		assertEquals(4, visited_in_order.size());
	}
}
