using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.UI
{
    public class SunnyLog
    {
        public static string ReadTextFile(string file)
        {
            return File.Exists(file) ? File.ReadAllText(file).Trim() : "";
        }
        private static object lockFile = new object();
        private static object lockFileAction = new object();

        public static string ApiLocation { get; internal set; } = "https://admin.wemake.vn";


        #region WRITE LOG
        public static void Logs(Exception ex, [CallerMemberName] string methodName = "")
        {
            Log(methodName, ex);
        }
        public static void Log(string function, Exception ex, string UID = "", string Note = "")
        {
            lock (lockFile)
            {
                try
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string logDirectory = Path.Combine(baseDirectory, "Log");
                    string logFile = Path.Combine(logDirectory, $"log{DateTime.Now:ddMMyyyy}.txt");
                    WriteLog(logFile, function, ex, UID, Note);

                    int retentionDays = 30;
                    DeleteOldLogFiles(logDirectory, retentionDays);
                }
                catch (Exception e)
                {
                    // Có thể thêm ghi log cho exception tại đây nếu cần
                }
            }
        }

        public static void LogDB(string function, Exception ex, string UID = "", string Note = "")
        {
            lock (lockFile)
            {
                try
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string logDirectory = Path.Combine(baseDirectory, "Log");
                    string logFile = Path.Combine(logDirectory, $"logdatabase{DateTime.Now:ddMMyyyy}.txt");
                    WriteLog(logFile, function, ex, UID, Note);

                    int retentionDays = 30;
                    DeleteOldLogFiles(logDirectory, retentionDays);
                }
                catch (Exception e)
                {
                    // Có thể thêm ghi log cho exception tại đây nếu cần
                }
            }
        }


        private static void WriteLog(string file, string function, Exception ex, string UID, string Note)
        {
            lock (lockFile)
            {
                try
                {
                    string logDirectory = Path.GetDirectoryName(file);
                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                    }

                    using (StreamWriter streamWriter = new StreamWriter(file, append: true))
                    {
                        streamWriter.WriteLine("-----------------------------------------------------------------------------");
                        streamWriter.WriteLine("Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        streamWriter.WriteLine("Function: " + function);
                        if (!string.IsNullOrEmpty(UID))
                        {
                            streamWriter.WriteLine("UID: " + UID);
                        }
                        if (!string.IsNullOrEmpty(Note))
                        {
                            streamWriter.WriteLine("Note: " + Note);
                        }
                        if (ex != null)
                        {
                            streamWriter.WriteLine("Type: " + ex.GetType().FullName);
                            streamWriter.WriteLine("Message: " + ex.Message);
                            streamWriter.WriteLine("StackTrace: " + ex.StackTrace);
                            while (ex.InnerException != null)
                            {
                                ex = ex.InnerException;
                                streamWriter.WriteLine("InnerException Type: " + ex.GetType().FullName);
                                streamWriter.WriteLine("InnerException Message: " + ex.Message);
                                streamWriter.WriteLine("InnerException StackTrace: " + ex.StackTrace);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // Có thể thêm ghi log cho exception tại đây nếu cần
                }
            }
        }

        public static void LogAction(string function, string log, string uid = "", string namefile = "")
        {
            lock (lockFileAction)
            {
                if (string.IsNullOrEmpty(namefile))
                {
                    namefile = !string.IsNullOrEmpty(uid) ? uid : DateTime.Now.ToString("ddMMyyyy");
                }

                try
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string logActionDirectory = Path.Combine(baseDirectory, "LogAction");

                    string logFile = Path.Combine(logActionDirectory, $"log{namefile}.txt");

                    WriteLogAction(logFile, log, uid, function);
                }
                catch (Exception e)
                {
                    // Có thể thêm ghi log cho exception tại đây nếu cần
                }
            }
        }

        private static void WriteLogAction(string file, string log, string uid, string actionName)
        {
            lock (lockFileAction)
            {
                try
                {
                    string logActionDirectory = Path.GetDirectoryName(file);
                    if (!Directory.Exists(logActionDirectory))
                    {
                        Directory.CreateDirectory(logActionDirectory);
                    }

                    using (StreamWriter streamWriter = new StreamWriter(file, append: true))
                    {
                        streamWriter.WriteLine("-----------------------------------------------------------------------------");
                        streamWriter.WriteLine("Date: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                        if (!string.IsNullOrEmpty(uid))
                        {
                            streamWriter.WriteLine("UID: " + uid);
                        }
                        streamWriter.WriteLine("Action: " + actionName);
                        streamWriter.WriteLine("Log: " + log);
                    }
                }
                catch (Exception e)
                {
                    // Có thể thêm ghi log cho exception tại đây nếu cần
                }
            }
        }

        private static void DeleteOldLogFiles(string directory, int days)
        {
            try
            {
                var dirInfo = new DirectoryInfo(directory);
                foreach (var file in dirInfo.GetFiles("*.txt"))
                {
                    if (file.CreationTime < DateTime.Now.AddDays(-days))
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception e)
            {
                // Có thể thêm ghi log cho exception tại đây nếu cần
            }
        }

        public static void LogChrome(string function, Exception ex, string UID = "", string Note = "")
        {
            lock (lockFile)
            {
                try
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string logDirectory = Path.Combine(baseDirectory, "LogChrome");
                    string logFile = Path.Combine(logDirectory, $"logchrome{DateTime.Now:ddMMyyyy}.txt");
                    WriteLog(logFile, function, ex, UID, Note);

                    int retentionDays = 30;
                    DeleteOldLogFiles(logDirectory, retentionDays);
                }
                catch (Exception e)
                {
                    // Có thể thêm ghi log cho exception tại đây nếu cần
                }
            }
        }


        #endregion

    }
}
