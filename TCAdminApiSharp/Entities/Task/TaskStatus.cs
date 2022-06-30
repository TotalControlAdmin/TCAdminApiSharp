namespace TCAdminApiSharp.Entities.Task;

public enum TaskStatus
{
    NotExecuted,
    Executing,
    Scheduled,
    Completed,
    Canceled,
    TaskError
}