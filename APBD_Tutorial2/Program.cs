namespace APBD_Tutorial2
{
    public class Program
    {
        public static void Main(string[] args)
        {

            University university = Util.FileUtil.ParseCsvFileToDifferentFormat(Constants.dataFile);
       
            Util.FileUtil.ParseToFormat(Constants.xmlFormat, university, Constants.outputXml);
            Util.FileUtil.ParseToFormat(Constants.jsonFormat, university, Constants.outputJson);
        }


    }
}
