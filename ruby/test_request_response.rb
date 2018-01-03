require 'minitest/autorun'
require_relative 'wiki_page'
require_relative 'wikiapp'

class TestRequestResponse < Minitest::Test

  def test_request_response_cycle
    root_page = WikiPage.new(title: 'FrontPage', uri: '/')
    request = Request.new('GET', '/')
    response = WikiApp.new(root_page).handle_request(request)
    assert_equal('FrontPage', response.page.title)
  end

  def test_request_a_page
    root_page = WikiPage.new(title: 'FrontPage', uri: '/')
    child_page = WikiPage.new(title: 'Child1', text: 'a child page', tags: ['foo'])
    root_page.add_child(child_page)
    myapp = WikiApp.new(root_page)
    request = Request.new('GET', '/Child1')
    response = myapp.handle_request(request)
    assert_equal('Child1', response.page.title)
  end

  def test_request_a_search
    root_page = WikiPage.new(title: 'FrontPage', uri: '/')
    child_page = WikiPage.new(title: 'Child1', text: 'a child page', tags: ['foo'])
    root_page.add_child(child_page)
    myapp = WikiApp.new(root_page)
    request = Request.new('POST', '/', {search_text: 'child'})
    response = myapp.handle_request(request)
    assert response.page.title == 'Search Results'
    assert_includes response.page.text, 'Child1'
  end

  def test_request_where_used
    root_page = WikiPage.new(title: 'FrontPage', uri: '/')
    child_page = WikiPage.new(title: 'Child1', text: 'a child page referencing FrontPage', tags: ['foo'])
    root_page.add_child(child_page)
    myapp = WikiApp.new(root_page)
    request = Request.new('POST', '/', {where_used: 'FrontPage'})
    response = myapp.handle_request(request)
    assert_includes response.page.title, 'Where Used'
    assert_includes response.page.text, 'Child1'
  end
    
  def test_request_property_search
    root_page = WikiPage.new(title: 'FrontPage', uri: '/')
    child_page = WikiPage.new(title: 'Child1', text: 'a child page', tags: ['foo', 'bar'])
    child2_page = WikiPage.new(title: 'Child2', text: 'a second child page', tags: ['foo'])
    root_page.add_child(child_page)
    root_page.add_child(child2_page)
    request = Request.new('POST', '/', {tags: ['bar']})
    response = WikiApp.new(root_page).handle_request(request)
    assert_includes response.page.title, 'Property Search'
    assert_includes response.page.text, 'Child1'
    assert !response.page.text.include?('Child2')
  end

  def test_search_replace
    root_page = WikiPage.new(title: 'FrontPage', uri: '/')
    child_page = WikiPage.new(title: 'Child1', text: 'a child page with text baz')
    root_page.add_child(child_page)
    myapp = WikiApp.new(root_page)
    request = Request.new('POST', uri='/', data={search_text: 'baz', replace: 'foo'})
    response = myapp.handle_request(request)
    assert_includes response.page.title, 'Search/Replace'
    assert_includes response.page.text, 'Child1'
    child_page_response = myapp.handle_request(Request.new('GET', '/Child1'))
    assert_includes child_page_response.page.text, 'foo'
    assert !child_page_response.page.text.include?('baz')
  end
end