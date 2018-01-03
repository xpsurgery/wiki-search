using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class DepthFirstTraverser
	{
		private WikiPage root;

		public DepthFirstTraverser(WikiPage root)
		{
			this.root = root;
		}

		public List<WikiPage> Traverse()
		{
			List<WikiPage> visited = new List<WikiPage>();
			List<WikiPage> toVisit = new List<WikiPage>();
			toVisit.Add(root);
			while (toVisit.Count > 0) {
				WikiPage node = toVisit[toVisit.Count - 1];
				toVisit.Remove(node);
				if (!visited.Contains(node)) {
					visited.Add(node);
					toVisit.AddRange(node.Children());
				}
			}
			return visited;
		}
	}
}

