using APBD_Tutorial2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace APBD_Tutorial2.Util
{
    public class FileUtil
    {
        public static University ParseCsvFileToDifferentFormat(string csvFile)
        {

            List<string> lineStringList = new List<string>();
            List<Student> studentList = new List<Student>();
            int lineCounter = 1;

            try
            {
                if (File.Exists(Constants.logFile))
                {
                    File.Delete(Constants.logFile);
                }

                FileStream writer = new FileStream(Constants.logFile, FileMode.Create);
                writer.Close();

   
                using (StreamReader reader = new StreamReader(File.OpenRead(csvFile)))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            lineStringList = line.Split(',').ToList();
                            if (IsLineElementCountValid(lineStringList, lineCounter))
                            {
                                if (ContainsNoWhitespaceValues(lineStringList, lineCounter))
                                {
                                    Student student = ParseToStudent(lineStringList, lineCounter);

                                    if (student != null)
                                    {
                                        if (!HasSameStudents(studentList, student, lineCounter))
                                        {
                                            studentList.Add(student);
                                        }
                                        else
                                        {
                                            string message = "student with id " + student.Id + " already exists";
                                            ErrorUtil.GenerateError(lineCounter, message);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ErrorUtil.GenerateError(lineCounter, "empty line");
                        }
                        lineCounter++;
                    }
                }
            
            } catch(FileNotFoundException e)
            {
                ErrorUtil.GenerateError(lineCounter, "file does not exist or the path is wrong");
                Console.WriteLine(e.Message);
            }

            Dictionary<string, int> studiesMap = createStudiesMap(studentList);
            List<ActiveStudy> studiesList = TransformMapToStudiesList(studiesMap);

            return new University(DateTime.Now, "Anna Miklash", studentList, studiesList);
        }



        private static List<ActiveStudy> TransformMapToStudiesList(Dictionary<string, int> studiesMap)
        {
            return studiesMap.Select(kvp => new ActiveStudy
            {
                StudyName = kvp.Key,
                Counter = kvp.Value
            })
                .ToList();
        }

        private static Dictionary<string, int> createStudiesMap(List<Student> studentList)
        {
            List<String> studiesNamesList = CreateStudyNamesList(studentList);
            Dictionary<String, int> studyNamesMap = InitStudyNamesMap(studiesNamesList);
            Dictionary<string, int> studiesMap = InitStudyMapWithValues(studyNamesMap, studiesNamesList);
            return studiesMap;
        }

        private static Dictionary<string, int> InitStudyMapWithValues(Dictionary<string, int> studyNamesMap, List<string> studiesNamesList)
        {
            foreach (var study in studiesNamesList)
            {
                if (studyNamesMap.ContainsKey(study))
                {
                    int value = studyNamesMap[study];
                    value++;

                    studyNamesMap[study] = value;
                }
            }
            return studyNamesMap;
        }

        private static Dictionary<string, int> InitStudyNamesMap(List<string> studiesNamesList)
        {
            Dictionary<string, int> studyNamesMap = new Dictionary<string, int>();
            foreach (var study in studiesNamesList)
            {
                if (!studyNamesMap.ContainsKey(study))
                {
                    studyNamesMap.Add(study, 0);
                }

            }
            return studyNamesMap;
        }

        private static List<string> CreateStudyNamesList(List<Student> studentList)
        {
            List<string> studiesNamesList = new List<string>();
            foreach (var student in studentList)
            {
                string studyName = student.Study.name;
                studiesNamesList.Add(studyName);

            }
            return studiesNamesList;
        }


        public static void ParseToFormat(string format, University university, String output)
        {
            if (format.Equals("xml"))
            {
                ParseToXml(university, output);
            }
            if (format.Equals("json"))
            {
                ParseToJson(university, output);
            }
        }

        private static void ParseToJson(University university, string output)
        {
            if (File.Exists(output))
            {
                File.Delete(output);
            }

           
            dynamic universityWrapper = new
            {
                university = university
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(universityWrapper);

            //write string to file
            File.WriteAllText(output, json);
        }

        private static void ParseToXml(University university, string output)
        {
            if (File.Exists(output))
            {
                File.Delete(output);
            }
            FileStream writer = new FileStream(output, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(University));
            serializer.Serialize(writer, university);
        }

      

        private static Student ParseToStudent(List<string> lineStringList, int lineCounter)
        {
            string id = lineStringList.ElementAt(0);
            string firstName = lineStringList.ElementAt(1);
            string lastName = lineStringList.ElementAt(2);
            string birthDate = lineStringList.ElementAt(3);
            string email = lineStringList.ElementAt(4);
            string mothersName = lineStringList.ElementAt(5);
            string fathersName = lineStringList.ElementAt(6);
            string studyName = lineStringList.ElementAt(7);
            string studyMode = lineStringList.ElementAt(8);


            if (IsRegexValid(lineStringList, lineCounter))
            {
                return new Student(int.Parse(id),
                    firstName,
                    lastName,
                    DateTime.Parse(birthDate),
                    email,
                    mothersName,
                    fathersName,
                    new Study(studyName, studyMode));
            }

            return null;
        }

        private static bool IsRegexValid(List<string> lineStringList, int lineCounter)
        {
            Regex emailRegex = new Regex(Constants.emailRegex);
            Regex dateRegex = new Regex(Constants.dateRegex);
            Regex idRegex = new Regex(Constants.idRegex);
            Regex wordRegex = new Regex(Constants.singleWordRegex);
            Regex multipleWordsRegex = new Regex(Constants.multipleWordsRegex);
            Regex hyphenatedRegex = new Regex(Constants.hyphenatedRegex);



            if (idRegex.IsMatch(lineStringList.ElementAt(0)) &&
                wordRegex.IsMatch(lineStringList.ElementAt(1)) &&
                wordRegex.IsMatch(lineStringList.ElementAt(2)) &&
                dateRegex.IsMatch(lineStringList.ElementAt(3)) &&
                emailRegex.IsMatch(lineStringList.ElementAt(4)) &&
                wordRegex.IsMatch(lineStringList.ElementAt(5)) &&
                wordRegex.IsMatch(lineStringList.ElementAt(6)) &&
                multipleWordsRegex.IsMatch(lineStringList.ElementAt(7)) &&
                hyphenatedRegex.IsMatch(lineStringList.ElementAt(8)))

            {
                return true;
            }
            else
            {
                ErrorUtil.GenerateError(lineCounter, "wrong input format");
                return false;
            }

        }

        private static bool HasSameStudents(List<Student> studentList, Student student, int lineCounter)
        {
            if (studentList.Count == 0)
            {
                return false;
            }

            foreach (Student currentStudent in studentList)
            {
                if (currentStudent.Id == student.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsLineElementCountValid(List<string> lineStringList, int line)
        {
            if (lineStringList.Count < Constants.properLineLength)
            {
                ErrorUtil.GenerateError(line, "not enough data. Should be " + Constants.properLineLength + " elements, but has " + lineStringList.Count);
                return false;
            }
            return true;

        }

        private static bool ContainsNoWhitespaceValues(List<string> lineStringList, int line)
        {
            foreach (string element in lineStringList)
            {
                if (string.IsNullOrEmpty(element))
                {
                    ErrorUtil.GenerateError(line, "part of the data is missing");
                    return false;
                }
            }
            return true;
        }
    }
}
