using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [Serializable]
    public class Count
    {
        private int minimum;
        private int maximun;

        public Count(int minimum, int maximum)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
        }
        public int Maximum
        {
            get { return maximun; }
            set { maximun = value; }
        }

        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; }
        }
    }
}
