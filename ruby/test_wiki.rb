require 'minitest/autorun'
require_relative 'wiki_page'

class TestMeme < Minitest::Test
  def test_create_pages
    root_page = WikiPage.new(title: 'FrontPage', text: 'some text on the root page', tags: ['foo', 'bar'])
    child_page = WikiPage.new(title: 'Child1', text: 'a child page', tags: ['foo'])
    root_page.add_child(child_page)
    assert root_page.title == 'FrontPage'
    assert root_page.children.map { |p| p.title }.include?('Child1')
    assert child_page.parents.map { |p| p.title }.include?('FrontPage')
  end    
    
  def test_uri
    root_page = WikiPage.new(title: 'FrontPage', text: 'some text on the root page', tags: ['foo', 'bar'], uri: '/blah')
    child_page = WikiPage.new(title: 'Child1', text: 'a child page', tags: ['foo'])
    root_page.add_child(child_page)
    grandchild_page = WikiPage.new(title: 'Child2', text: 'a child page', tags: ['foo'])
    child_page.add_child(grandchild_page)
    assert_equal('/blah', root_page.uri)
    assert_equal('/blah/Child1', child_page.uri)
    assert grandchild_page.uri == '/blah/Child1/Child2'
  end
end
