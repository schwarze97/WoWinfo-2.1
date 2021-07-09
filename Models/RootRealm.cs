using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WoWinfo.Models
{
    public class RootRealm
    {
        [JsonProperty("results")]
        public List<Results> results { get; set; }

        public RootRealm()
        {
        }

        public RootRealm(List<Results> results)
        {
            this.results = results;
        }
    }
}