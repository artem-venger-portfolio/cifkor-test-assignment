namespace WebClient
{
    public interface IProjectLogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}