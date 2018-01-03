
from wikiapp import *
from wiki import WikiPage
from request_response import Request, Response


def test_request_response_cycle():
    root_page = WikiPage(title="FrontPage", uri="/")
    myapp = WikiApp(root_page)
    request = Request(request_type="GET", uri="/")
    response = myapp.handle_request(request)
    assert response.page.title == "FrontPage"
    
def test_request_a_page():
    root_page = WikiPage(title="FrontPage", uri="/")
    child_page = WikiPage(title="Child1", text="a child page", tags=["foo"])
    root_page.add_child(child_page)
    myapp = WikiApp(root_page)
    request = Request(request_type="GET", uri="/Child1")
    response = myapp.handle_request(request)
    assert response.page.title == "Child1"
    
def test_request_a_search():
    root_page = WikiPage(title="FrontPage", uri="/")
    child_page = WikiPage(title="Child1", text="a child page", tags=["foo"])
    root_page.add_child(child_page)
    myapp = WikiApp(root_page)
    request = Request(request_type="POST", uri="/", data={"search_text": "child"})
    response = myapp.handle_request(request)
    assert response.page.title == "Search Results"
    assert "Child1" in response.page.text
 
def test_request_where_used():
    root_page = WikiPage(title="FrontPage", uri="/")
    child_page = WikiPage(title="Child1", text="a child page referencing FrontPage", tags=["foo"])
    root_page.add_child(child_page)
    myapp = WikiApp(root_page)
    request = Request(request_type="POST", uri="/", data={"where_used": "FrontPage"})
    response = myapp.handle_request(request)
    assert "Where Used" in response.page.title
    assert "Child1" in response.page.text
    
def test_request_property_search():
    root_page = WikiPage(title="FrontPage", uri="/")
    child_page = WikiPage(title="Child1", text="a child page", tags={"foo", "bar"})
    child2_page = WikiPage(title="Child2", text="a second child page", tags={"foo"})
    root_page.add_child(child_page)
    root_page.add_child(child2_page)
    myapp = WikiApp(root_page)
    request = Request(request_type="POST", uri="/", data={"tags": {"bar"}})
    response = myapp.handle_request(request)
    assert "Property Search" in response.page.title
    assert "Child1" in response.page.text
    assert not "Child2" in response.page.text

def test_search_replace():
    root_page = WikiPage(title="FrontPage", uri="/")
    child_page = WikiPage(title="Child1", text="a child page with text baz")
    root_page.add_child(child_page)
    myapp = WikiApp(root_page)
    request = Request(request_type="POST", uri="/", data={"search_text": "baz", "replace": "foo"})
    response = myapp.handle_request(request)
    assert "Search/Replace" in response.page.title
    assert "Child1" in response.page.text
    child_page_response = myapp.handle_request(Request(request_type="GET", uri="/Child1"))
    assert "foo" in child_page_response.page.text
    assert not "baz" in child_page_response.page.text