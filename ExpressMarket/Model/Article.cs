using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpressMarket.Model
{
    public class Article
    {
        public Article(int id, string name, string imageUrl, string code, string creator, string type)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Code = code;
            Creator = creator;
            Type = type;
        }

        public Article() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Code { get; set; }
        public string Creator { get; set; }
        public string Type { get; set; }
    }

  
}
