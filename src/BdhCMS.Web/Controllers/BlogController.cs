using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BdhCMS.Web.Models;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence.Querying;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace BdhCMS.Web.Controllers
{
    public class BlogController : RenderMvcController
    {
        // GET: News
        public ActionResult Index(int parentId,string categoryName)
        {
            var contents = Umbraco.Content(parentId).Children.Cast<Blogpost>()
                .Where(_=>_.Categories != null && _.Categories.Contains(categoryName)).ToList();
            return View("Index", new NewsResultModel()
            {
                BlogPosts = contents,
                CurrentPageId = parentId
            });
        }
    }
}