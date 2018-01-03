using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	[TestFixture()]
	public class TraverseTests
	{
		private WikiPage root;
		private WikiPage child1;
		private WikiPage child2;
		private WikiPage child3;
		private DepthFirstTraverser traverser;

		[SetUp]
		public void setup() {
			this.root = new WikiPage("Root");
			this.child1 = new WikiPage("Child1");
			this.child2 = new WikiPage("Child2");
			this.child3 = new WikiPage("Child3");

			this.root.AddChild(this.child2);
			this.root.AddChild(this.child1);
			this.child1.AddChild(this.child3);

			this.traverser = new DepthFirstTraverser(this.root);
		}

		[Test]
		public void test_traverse_pages()
		{
			List<WikiPage> pages = this.traverser.Traverse();
			List<string> visitedInOrder = new List<string>();
			foreach (WikiPage p in pages)
				visitedInOrder.Add(p.Title);
			Assert.IsTrue(visitedInOrder.IndexOf("Child3") < visitedInOrder.IndexOf("Child2"));
		}

		[Test]
		public void test_traverse_with_loops()
		{
			this.child3.AddChild(this.root);
			List<WikiPage> pages = this.traverser.Traverse();
			List<string> visitedInOrder = new List<string>();
			foreach (WikiPage p in pages)
				visitedInOrder.Add(p.Title);
			Assert.AreEqual(4, visitedInOrder.Count);
		}
	}
}

