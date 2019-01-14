using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public sealed class Log : ILog
    {        
        public void ExceptionLogger(string mssg)
        {
            string fileName = $"Error_{DateTime.Now.ToShortDateString()}";
            string logFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\{fileName}";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==========================================================================================");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(mssg);
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}
