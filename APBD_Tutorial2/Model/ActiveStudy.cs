using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace APBD_Tutorial2.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ActiveStudy
    {
        [XmlAttribute("name")]
        public String StudyName { get; set; }

        [XmlAttribute("numberOfStudents")]
        public int Counter { get; set; }

        public ActiveStudy(string studyName, int counter)
        {
            StudyName = studyName;
            Counter = counter;
        }

        public ActiveStudy()
        {
        }
    }
}
