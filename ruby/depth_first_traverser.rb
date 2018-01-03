class DepthFirstTraverser
  def initialize(root)
    @root = root
  end

  def traverse
    visited = []
    to_visit = [@root]
    while to_visit.length > 0
      node = to_visit.pop
      unless visited.include?(node)
        visited << node
        to_visit += node.children
      end
    end
    visited
  end
end
