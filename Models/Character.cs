using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WoWinfo.Models
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ID { get; set; }


        public Character()
        {

        }

        public Character(int ID, string Name, int Level)
        {
            this.ID = ID;
            this.Name = Name;
            this.Level = Level;
        }

    }
}