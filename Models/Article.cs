using System;
using Microsoft.AspNetCore.Identity;

namespace PressAgency.Models
{

    public enum ArticleType { Sports, Cinema, Politics, Religion };

    public class Article
    {
        public string Id { get; set; }
        public ApplicationUser Author { get; set; }
        public string Title { get; set; }
        public string body { get; set; }
        public DateTime PostCreationDate { get; set; }
        public ArticleType Type { get; set; }
        public int NumberOfViewers { get; set; }
        public string Image { get; set; }    

    }
}
