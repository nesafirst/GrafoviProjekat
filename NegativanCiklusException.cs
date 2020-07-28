using System;
using System.Collections.Generic;
using System.Text;

namespace Grafovi
{
    class NegativanCiklusException : Exception
    {
        public NegativanCiklusException() : base("Graf sadrzi negativan ciklus")
        {

        }
    }
}
