require 'minitest/autorun'
require_relative 'wiki_page'
require_relative 'depth_first_traverser'

class TestTraverse < Minitest::Test
  def setup
    @root = WikiPage.new(title: 'Root')
    @child1 = WikiPage.new(title: 'Child1')
    @child2 = WikiPage.new(title: 'Child2')
    @child3 = WikiPage.new(title: 'Child3')

    @root.add_child(@child2)
    @root.add_child(@child1)
    @child1.add_child(@child3)

    @traverser = DepthFirstTraverser.new(@root)
  end

  def test_traverse_pages
    pages = @traverser.traverse
    visited_in_order = pages.map {|p| p.title }
    assert visited_in_order.index('Child3') < visited_in_order.index('Child2')
  end
    
  def test_traverse_with_loops
    @child3.add_child(@root)
    pages = @traverser.traverse
    visited_in_order = pages.map {|p| p.title }
    assert_equal(4, visited_in_order.length)
  end
end
