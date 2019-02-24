using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BowlingSimulator.Models
{
    public class Player
    {
        private string name;
        private string greatness;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Greatness
        {
            get { return this.greatness; }
            set { this.greatness = value; }
        }
    }
}