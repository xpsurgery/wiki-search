require_relative 'depth_first_traverser'

class ResponderBuilder
  def self.responder_for(request)
    return WikiPageResponder.new if request.request_type == 'GET'
    if request.request_type == 'POST'
      if request.data[:search_text]
        if request.data[:replace]
          return SearchReplaceResponder.new
        else
          return SearchResponder.new
        end
      end
      if request.data[:where_used]
        return WhereUsedResponder.new
      end
      if request.data[:tags]
        return PropertySearchResponder.new
      end
    end
  end
end

class WikiPageResponder
  def make_response(request, context)
    all_pages = DepthFirstTraverser.new(context.root_page).traverse.select {|page| page.uri == request.uri }
    if all_pages.empty?
      return Response.new(WikiPage.new(title: '404 page'), '404')
    else
      return Response.new(all_pages[0], '200')
    end
  end
end
 
 
class ResultResponder
  def make_response(request, context)
    @request = request
    matching_pages = DepthFirstTraverser.new(context.root_page).traverse.select {|page| traverse(page) }
    result = 'found term in pages:<ul>'
    matching_pages.each {|found| result += '<li>'+ found.title + '</li>' }
    result += '</ul>'
    results_page = WikiPage.new(title: title, text: result)
    Response.new(results_page)
  end

  def request
    @request
  end
end
       
  
class SearchResponder < ResultResponder
  def title
    return 'Search Results'
  end
  
  def traverse(page)
    search_term = request.data[:search_text]
    page.text.include?(search_term)
  end
end


class WhereUsedResponder < ResultResponder
  def title
    return 'Where Used: ' + search_for_page
  end
  
  def traverse(page)
    page.text.include?(search_for_page)
  end

  private

  def search_for_page
    request.data[:where_used]
  end
end
    

class PropertySearchResponder < ResultResponder
  def title
    'Property Search: ' + search_for_tags.to_s
  end
  
  def traverse(page)
    common_tags = search_for_tags & page.tags
    common_tags.length > 0
  end

  private

  def search_for_tags
    request.data[:tags]
  end
end

class SearchReplaceResponder < ResultResponder
  def title
    "Search/Replace: #{search_text}/#{replace_text}"
  end
    
  def traverse(page)
    if page.text.include?(search_text)
      page.text = page.text.gsub(search_text, replace_text)
      return true
    end
    return false
  end

  private
    
  def search_text
    request.data[:search_text]
  end
    
  def replace_text
    request.data[:replace]
  end
end
