using APBD_Tutorial2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace APBD_Tutorial2
{
    [XmlType(TypeName = "university")]    
    public class University
    {
        [XmlAttribute("createdAt")]
        [JsonProperty]
        public DateTime dateTime;

        [XmlAttribute("author")]
        [JsonProperty]
        public string author;

        [XmlArray(ElementName ="students")]
        [JsonProperty("students")]
        public List<Student> studentList;

        [XmlArray(ElementName = "activeStudies")]
        public List<ActiveStudy> activeStudies;

        public University(DateTime dateTime, string author, List<Student> studentList, List<ActiveStudy> activeStudies)
        {
            this.dateTime = dateTime;
            this.author = author;
            this.studentList = studentList;
            this.activeStudies = activeStudies;
        }

        public University()
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
