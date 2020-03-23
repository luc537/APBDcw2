using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    [Serializable]
    public class Uczelnia
    {
        public DateTime createdAt { get; set; }
        public string author { get; set; }

        public List<Student> Studenci { get; set; }

        public List<Studies> activeStudies {get; set;}
    }
}
