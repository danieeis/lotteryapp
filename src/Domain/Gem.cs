using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Gem
    {
        public string Id { get; set; }
        public IList<Draw> Draws { get; set; }

        public string Name { get; set; }


        public Gem(string name)
        {
            Name = name;
            Draws = new List<Draw>();
        }

        public Gem()
        {

        }
    }
}
