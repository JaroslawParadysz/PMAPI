using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.LinksBuildings
{
    public interface ILinksBuilder
    {
        ILinksBuilder AddLink(string href, string rel, string method);
        IList<LinkDto> Build();
    }
}
