using APBD_Tutorial2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace APBD_Tutorial2
{
    [XmlType("student")]
    [JsonObject(MemberSerialization.OptOut)]

    public class Student
    {
        [XmlAttribute("indexNumber")]
        [JsonProperty("indexNumber")]
        public int Id { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "mothersName")]
        public string MothersName { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "fathersName")]
        public string FathersName { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "studies")]
        public Study Study { get; set; }


        public Student()
        {
        }

        public Student(int id, string firstName, string lastName, 
            DateTime birthDate, string email, string mothersName, 
            string fathersName, Study study)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            MothersName = mothersName;
            FathersName = fathersName;
            Study = study;
        }

        public override string ToString()
        {
            return "id = " + Id + ", firstName = " + FirstName + ", lastName = " + LastName;
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   Id == student.Id &&
                   FirstName == student.FirstName &&
                   LastName == student.LastName &&
                   BirthDate == student.BirthDate &&
                   Email == student.Email &&
                   MothersName == student.MothersName &&
                   FathersName == student.FathersName &&
                   EqualityComparer<Study>.Default.Equals(Study, student.Study);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, BirthDate, Email, MothersName, FathersName, Study);
        }
    }
}
