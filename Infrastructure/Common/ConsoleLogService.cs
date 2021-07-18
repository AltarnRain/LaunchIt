using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Common
{
    public class ConsoleLogService : Logic.Common.ILogger
    {
        private List<string[]> logCache = new List<string[]>();
        
        public void Log(string message)
        {
            this.Push(message);
            this.Show();
        }

        public void Show()
        {
            var columnLengths = this.logCache.GetMaxArrayContent();
        }

        public void Push(params string[] messages)
        {
            this.logCache.Add(messages);
        }
    }
}
