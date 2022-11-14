using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Find_The_Path
{
    public class pair<T, U>
    {
        public pair()
        {
            first = default(T);
            second = default(U);
        }

        public pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }

        public T first { get; set; }
        public U second { get; set; }
    };

}
