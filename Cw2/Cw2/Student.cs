using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2
{
    [Serializable]
    public class Student

    {
        [XmlElement]
        public string IndexNumber { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string MothersName { get; set; }
        public string FathersName { get; set; }
        public StudentStudies stStudies { get; set; }
        override
        public String ToString()
        {
            return $"{this.IndexNumber} {this.FName} {this.LName}";
        }
    }
}
