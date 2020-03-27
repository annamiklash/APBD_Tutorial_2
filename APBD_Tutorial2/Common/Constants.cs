namespace APBD_Tutorial2
{
    public class Constants
    {
        public const string dataFile = @"C:\Users\aniam\Desktop\4 semester\APBD\APBD_Tutorial2\APBD_Tutorial2\data.csv";
        public const string logFile = @"log.txt";

        public const string outputXml = @"xmldata.xml";
        public const string outputJson = @"jsondata.json";

        public const string xmlFormat = "xml";
        public const string jsonFormat = "json";

        public const string emailRegex = "[a-z]+[a-z0-9]*@[a-z]+\\.[a-z]+";
        public const string singleWordRegex = "^[a-zA-Z]+$";
        public const string multipleWordsRegex = "[a-zA-Z\\s]+";
        public const string idRegex = "\\d+";
        public const string dateRegex = "^\\d{4}-((0[1-9])|(1[012]))-((0[1-9]|[12]\\d)|3[01])$";
        public const string hyphenatedRegex = "^[a-zA-Z]+(?:-[a-zA-Z]+)+$";

        public const int properLineLength = 9;


    }
}
