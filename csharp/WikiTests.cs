using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	[TestFixture()]
	public class WikiTests
	{
		[Test()]
		public void test_create_pages()
		{
			WikiPage rootPage = new WikiPage("FrontPage", "some text on the root page",
			                                 new List<string> { "foo", "bar" });
			WikiPage child_page = new WikiPage("Child1", "a child page", new List<string> { "foo" });
			rootPage.AddChild(child_page);
			Assert.AreEqual("FrontPage", rootPage.Title);
			List<string> children = new List<string>();
			foreach (WikiPage p in rootPage.Children())
				children.Add(p.Title);
			Assert.IsTrue(children.Contains("Child1"));
			List<string> parents = new List<string>();
			foreach (WikiPage p in child_page.Parents())
				parents.Add(p.Title);
			Assert.IsTrue(parents.Contains("FrontPage"));
		}

		[Test()]
		public void test_uri()
		{
			WikiPage rootPage = new WikiPage("FrontPage", "some text on the root page",
			                                 new List<string> { "foo", "bar" }, "/blah");
			WikiPage child_page = new WikiPage("Child1", "a child page", new List<string> { "foo" });
			rootPage.AddChild(child_page);
			WikiPage grandchild_page = new WikiPage("Child2", "a child page", new List<string> { "foo" });
			child_page.AddChild(grandchild_page);
			Assert.AreEqual("/blah", rootPage.Uri);
			Assert.AreEqual("/blah/Child1", child_page.Uri);
			Assert.AreEqual("/blah/Child1/Child2", grandchild_page.Uri);
		}

	}
}

