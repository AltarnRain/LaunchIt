namespace Logic.Common
{
    public interface ILogger
    {
        public void Push(params object[] messages);
        public void Log(string message);
        public void Show(params string[]? headers);
        public string[] Get(params string[]? headers);
    }
}
