using System.Collections.Generic;

namespace API.LinksBuildings
{
    public class LinksBuilder : ILinksBuilder
    {
        private IList<LinkDto> _links;

        public LinksBuilder()
        {
            _links = new List<LinkDto>();
        }

        public ILinksBuilder AddLink(string href, string rel, string method)
        {
            _links.Add(new LinkDto(href, rel, method));
            return this;
        }

        public IList<LinkDto> Build()
        {
            return _links;
        }
    }
}
