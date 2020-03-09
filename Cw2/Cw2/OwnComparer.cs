using System;
using System.Collections.Generic;

namespace Cw2
{
    public class OwnComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.FName} {x.LName} {x.IndexNumber}", $"{y.FName} {y.LName} {y.IndexNumber}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer
                .CurrentCultureIgnoreCase
                .GetHashCode($"{obj.FName} {obj.LName} {obj.IndexNumber}");
        }
    }
}
