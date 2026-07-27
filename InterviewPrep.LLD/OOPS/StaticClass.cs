namespace InterviewPrep.LLD.OOPS
{
    public static class StaticClass
    {
        //Note: this is an example of  logger
        //===================================================
        // Static Fields
        //===================================================

        private static int _logCount = 0;

        private static readonly DateTime _applicationStartTime;

        // Constant
        public const string ApplicationName = "Hospital Management System";

        //===================================================
        // Static Property
        //===================================================

        public static int LogCount
        {
            get
            {
                return _logCount;
            }
        }

        //===================================================
        // Static Constructor
        //===================================================

        static StaticClass()
        {
            _applicationStartTime = DateTime.Now;

            Console.WriteLine("StaticClass Initialized");
        }

        //===================================================
        // Static Methods
        //===================================================

        public static void Info(string message)
        {
            _logCount++;

            Console.WriteLine($"[INFO] {DateTime.Now} : {message}");
        }

        public static void Warning(string message)
        {
            _logCount++;

            Console.WriteLine($"[WARNING] {DateTime.Now} : {message}");
        }

        public static void Error(string message)
        {
            _logCount++;

            Console.WriteLine($"[ERROR] {DateTime.Now} : {message}");
        }

        public static void ShowSummary()
        {
            Console.WriteLine();
            Console.WriteLine("Application : " + ApplicationName);
            Console.WriteLine("Started At  : " + _applicationStartTime);
            Console.WriteLine("Total Logs  : " + _logCount);
        }
    }
}
