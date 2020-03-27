using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APBD_Tutorial2.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Study
    {
        [JsonProperty("name")]
        [XmlElement(ElementName = "name")]
        public string name { get; set; }

        [JsonProperty("mode")]
        [XmlElement(ElementName = "mode")]
        public string mode { get; set; }

        public Study(string name, string mode)
        {
            this.name = name;
            this.mode = mode;
        }

        public Study()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Study study &&
                   name == study.name &&
                   mode == study.mode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, mode);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }


}
