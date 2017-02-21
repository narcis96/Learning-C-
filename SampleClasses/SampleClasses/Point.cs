using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleClasses
{
    class Point < T > : IComparable<Point<T>> where T : IComparable<T>
    {
        public Point (T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        public T X
        {
            get { return x; }
            set {x = value; }
        }
        
        public T Y
        {
            get { return y; }
            set { y = value; }
        }


        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public int CompareTo(Point<T> other)
        {
            if (other.X.CompareTo(X) == 0)
                return other.Y.CompareTo(Y);
            return other.X.CompareTo(X);
        }

        private T x;
        private T y;
    }
}
