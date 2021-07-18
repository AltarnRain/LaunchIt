namespace Logic.Common
{
    public interface ILogger
    {
        public void Push(params string[] messages);
        public void Log(string message);
        public void Show();
    }
}
