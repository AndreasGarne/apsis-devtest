using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BowlingSimulator.Models
{
    public class Frame
    {
        private int roll1;
        private int roll2;
        private int roll3;
        private int score;

        public Frame(int roll1, int roll2)
        {
            this.Roll1 = roll1;
            this.Roll2 = roll2;
        }

        public int Roll1
        {
            get { return this.roll1; }
            set { this.roll1 = value; }
        }

        public int Roll2
        {
            get { return this.roll2; }
            set { this.roll2 = value; }
        }

        public int Roll3
        {
            get { return this.roll3; }
            set { this.roll3 = value; }
        }
 
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
    }
}