using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WoWinfo.Models
{
    public class Realms
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        
        [JsonProperty("slug")]
        public string Slug { get; set; }

        public Realms()
        {
        }

        public Realms(string Slug, int ID)
        {
            this.Slug = Slug;
            this.ID = ID;
        }
    }
}