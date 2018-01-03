import java.util.ArrayList;
import java.util.List;

class DepthFirstTraverser {
	private WikiPage root;

	public DepthFirstTraverser(WikiPage root) {
		this.root = root;
	}

	public List<WikiPage> traverse() {
		List<WikiPage> visited = new ArrayList<WikiPage>();
		List<WikiPage> to_visit = new ArrayList<WikiPage>();
		to_visit.add(root);
		while (to_visit.size() > 0) {
			WikiPage node = to_visit.remove(to_visit.size() - 1);
			if (!visited.contains(node)) {
				visited.add(node);
				to_visit.addAll(node.children());
			}
		}
		return visited;
	}
}
