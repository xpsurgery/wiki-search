require_relative 'responder'
require_relative 'request_response'

class WikiApp
  def initialize(root_page)
    @root_page = root_page
  end

  def handle_request(request)
    responder = ResponderBuilder.responder_for(request)
    responder.make_response(request, RequestContext.new(@root_page))
  end
end
