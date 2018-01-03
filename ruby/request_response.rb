class Request
  def initialize(request_type, uri, data={})
    @request_type = request_type
    @uri = uri
    @data = data
  end

  def request_type
    @request_type
  end

  def data
    @data
  end

  def uri
    @uri
  end
end
    
class Response
  def initialize(page, http_code='200')
    @http_code = http_code
    @page = page
  end

  def page
    @page
  end
end
      
class RequestContext
  def initialize(root_page)
    @page = root_page
  end

  def root_page
    @page
  end
end
