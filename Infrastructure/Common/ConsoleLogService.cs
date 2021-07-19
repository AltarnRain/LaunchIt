using Domain.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.Common
{
    public class ConsoleLogService : Logic.Common.ILogger
    {
        private readonly List<object[]> logCache = new();
        
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Show(string[]? headers = null)
        {
            foreach(var line in this.logCache.FormatTable(headers))
            {
                this.Log(line);
            }
        }

        public void Push(params object[] messages)
        {
            this.logCache.Add(messages);
        }

        public string[] Get(string[]? headers = null)
        {
            return this.logCache.FormatTable(headers);
        }
    }
}
