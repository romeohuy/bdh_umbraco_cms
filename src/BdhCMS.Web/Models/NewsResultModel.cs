using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.PublishedModels;

namespace BdhCMS.Web.Models
{
    public class NewsResultModel
    {
        public NewsResultModel()
        {
            BlogPosts = new List<Blogpost>();
            PageSize = 10;
        }
        public string PageTitle { get; set; }
        public int CurrentPageId { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<Blogpost> BlogPosts { get; set; }
    }
}