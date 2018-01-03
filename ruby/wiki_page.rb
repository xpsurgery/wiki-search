class WikiPage

  def initialize(title:, text: '', tags: [], uri: nil)
    @title = title
    @text = text
    @tags = tags
    @uri = uri || title
    @parents = []
    @children = []
  end

  def children
    @children
  end

  def title
    @title
  end

  def parents
    @parents
  end

  def uri
    @uri
  end

  def text
    @text
  end

  def text=(new_text)
    @text = new_text
  end

  def tags
    @tags
  end
    
  def add_child(page)
    @children << page
    page.add_parent(self)
  end
    
  def add_parent(page)
    @parents << page
    @uri = "#{page.uri}/#{@uri}".gsub(/\/\//, '/')
  end
end
