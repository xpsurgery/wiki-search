using System;
using System.Collections.Generic;

namespace WikiSearch
{
	public class WikiPage
	{
		private List<WikiPage> children;
		internal string Title;
		private List<WikiPage> parents;
		internal string Uri;
		private List<string> tags;
		internal string Text;

		public WikiPage(string title, string text, List<string> tags, string uri) {
			this.Title = title;
			this.Text = text;
			this.tags = tags;
			this.Uri = (uri == null) ? title : uri;
			this.parents = new List<WikiPage>();
			this.children = new List<WikiPage>();
		}

		public WikiPage(string title, string text, List<string> tags) : this(title, text, tags, null)
		{
		}

		public WikiPage(string title, string uri) : this(title, "", new List<string>(), uri)
		{
		}

		public WikiPage(string title) : this(title, null)
		{
		}

		public List<WikiPage> Children()
		{
			return children;
		}

		public List<WikiPage> Parents()
		{
			return parents;
		}

		public string SetText(string new_text)
		{
			Text = new_text;
			return new_text;
		}

		public List<string> Tags()
		{
			return tags;
		}

		public void AddChild(WikiPage page)
		{
			children.Add(page);
			page.AddParent(this);
		}

		public void AddParent(WikiPage page)
		{
			parents.Add(page);
			Uri = (page.Uri + "/" + Uri).Replace("//", "/");
		}
	}
}

