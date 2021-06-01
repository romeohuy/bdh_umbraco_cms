using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace BdhCMS.Web.Helpers
{
    public class NodeHelper
    {
        private readonly HttpContextBase _httpContext;
        private readonly UmbracoHelper _umbracoHelper;

        public NodeHelper(
            HttpContextBase httpContext,
            UmbracoHelper umbracoHelper)
        {
            this._httpContext = httpContext;
            this._umbracoHelper = umbracoHelper;
        }
        public Home GetFrontpage()
        {
            Uri url = this._httpContext.Request.Url;

            var registeredDomains = Umbraco.Core.Composing.Current.Services.DomainService.GetAll(false).ToList();

            var domains = (from d in registeredDomains
                where d.DomainName.StartsWith($"{url.Scheme}://{url.Authority}") ||
                      d.DomainName.StartsWith($"{url.Scheme}://{url.Host}") ||
                      d.DomainName.Contains(url.Authority) ||
                      d.DomainName.Contains(url.Host)
                select d).ToList();

            var domain = domains.FirstOrDefault();

            return this.GetFrontpage(domain);
        }

        public Home GetFrontpage(IDomain domain)
        {
            if (domain == null || domain.RootContentId.HasValue == false)
            {
                return null;
            }
            else
            {
                var content = this._umbracoHelper.Content(domain.RootContentId.Value) as Home;
                return content;
            }
        }
    }
}