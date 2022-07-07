namespace TCAdminApiSharp.Entities.Service;

public enum EServiceStatus
{
    Processing = -3, // 0xFFFFFFFD
    StartError = -2, // 0xFFFFFFFE
    Unknown = -1, // 0xFFFFFFFF
    Disabled = 0,
    Stopped = 1,
    Starting = 2,
    Stopping = 3,
    Running = 4,
    Resuming = 5,
    Pausing = 6,
    Paused = 7
}