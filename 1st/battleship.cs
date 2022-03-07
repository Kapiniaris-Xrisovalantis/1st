using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1st
{
    public  class Battleship
    {
        String name;
        int nobuttons;
        int hit;
        public  Battleship( String name, int nobuttons)
        {
            this.name = name; 
            this.nobuttons = nobuttons;
            hit = 0;
           

        }
        
        public String Name
        {
            get { return name; }
            set { name = value; }
            
        }
        public int NoButtons
        {
            get { return nobuttons; }
            set { nobuttons = value; }
        }
        public int Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        public bool check_if_destroyed()
        {
            if(hit == nobuttons)
                return true;
            return false;
        }

    }
}
