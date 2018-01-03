using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WikiSearch
{
	[TestFixture()]
	public class RequestResponseTests
	{
		[Test]
		public void test_request_response_cycle()
		{
			WikiPage root_page = new WikiPage("FrontPage", "/");
			Request request = new Request("GET", "/");
			Response response = new WikiApp(root_page).HandleRequest(request);
			Assert.AreEqual("FrontPage", response.Page.Title);
		}

		[Test]
		public void test_request_a_page()
		{
			WikiPage root_page = new WikiPage("FrontPage", "/");
			WikiPage child_page = new WikiPage("Child1", "a child page", new List<string> { "foo" });
			root_page.AddChild(child_page);
			WikiApp myapp = new WikiApp(root_page);
			Request request = new Request("GET", "/Child1");
			Response response = myapp.HandleRequest(request);
			Assert.AreEqual("Child1", response.Page.Title);
		}

		[Test]
		public void test_request_a_search()
		{
			WikiPage root_page = new WikiPage("FrontPage", "/");
			WikiPage child_page = new WikiPage("Child1", "a child page",
			                                   new List<string> { "foo" });
			root_page.AddChild(child_page);
			WikiApp myapp = new WikiApp(root_page);
			Request request = new Request("POST", "/", new Dictionary<string, string>() { {"search_text", "child"} });
			Response response = myapp.HandleRequest(request);
			Assert.AreEqual("Search Results", response.Page.Title);
			Assert.IsTrue(response.Page.Text.Contains("Child1"));
		}

		[Test]
		public void test_request_where_used()
		{
			WikiPage root_page = new WikiPage("FrontPage", "/");
			WikiPage child_page = new WikiPage("Child1",
			                                   "a child page referencing FrontPage", new List<string> { "foo" });
			root_page.AddChild(child_page);
			WikiApp myapp = new WikiApp(root_page);
			Request request = new Request("POST", "/", new Dictionary<string, string>() { {"where_used", "FrontPage"} });
			Response response = myapp.HandleRequest(request);
			Assert.IsTrue(response.Page.Title.Contains("Where Used"));
			Assert.IsTrue(response.Page.Text.Contains("Child1"));
		}

		[Test]
		public void test_request_property_search()
		{
			WikiPage root_page = new WikiPage("FrontPage", "/");
			WikiPage child_page = new WikiPage("Child1", "a child page",
			                                   new List<string> { "foo", "bar" });
			WikiPage child2_page = new WikiPage("Child2", "a second child page",
			                                    new List<string> { "foo" });
			root_page.AddChild(child_page);
			root_page.AddChild(child2_page);
			Request request = new Request("POST", "/", new Dictionary<string, string>() {{"tags", "bar"}});
			Response response = new WikiApp(root_page).HandleRequest(request);
			Assert.IsTrue(response.Page.Title.Contains("Property Search"));
			Assert.IsTrue(response.Page.Text.Contains("Child1"));
			Assert.IsFalse(response.Page.Text.Contains("Child2"));
		}

		[Test]
		public void test_search_replace()
		{
			WikiPage root_page = new WikiPage("FrontPage", "/");
			WikiPage child_page = new WikiPage("Child1",
			                                   "a child page with text baz", new List<string> {});
			root_page.AddChild(child_page);
			WikiApp myapp = new WikiApp(root_page);
			Request request = new Request("POST", "/", new Dictionary<string, string>() {
				{"search_text", "baz"},
				{"replace", "foo"},
			});
			Response response = myapp.HandleRequest(request);
			Assert.IsTrue(response.Page.Title.Contains("Search/Replace"));
			Assert.IsTrue(response.Page.Text.Contains("Child1"));
			Response child_page_response = myapp.HandleRequest(new Request("GET", "/Child1"));
			Assert.IsTrue(child_page_response.Page.Text.Contains("foo"));
			Assert.IsFalse(child_page_response.Page.Text.Contains("baz"));
		}
	}
}

