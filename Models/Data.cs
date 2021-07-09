using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WoWinfo.Models
{
    public class Data
    {
        [JsonProperty("realms")]
        public List<Realms> realms { get; set; }

        public Data()
        {
        }

        public Data(List<Realms> realms)
        {
            this.realms = realms;
        }

    }
}