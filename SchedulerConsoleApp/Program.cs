using SchedulerManager.Mechanism;

namespace SchedulerConsoleApp
{
    class Program
    {
        static void Main()
        {
            var jobManager = new JobManager();
            jobManager.ExecuteAllJobs();
        }
    }
}
