using System.Threading;
using SchedulerManager.Configuration;

namespace SchedulerManager.Mechanism
{
    /// <summary>
    /// Classes which extend this abstract class are Jobs which will be
    /// started as soon as the application starts. These Jobs are executed
    /// asynchronously from the Web Application.
    /// </summary>
    public abstract class Job
    {

        /// <summary>
        /// Gets and Sets the Client´s Name.
        /// </summary>
        /// <returns>Job´s name.</returns>
        public Client Client { get; set; }

        /// <summary>
        /// Execute the Job itself, one ore repeatedly, depending on
        /// the job implementation.
        /// </summary>
        public void ExecuteJob()
        {
            if (IsRepeatable())
            {
                // execute the job in intervals determined by the method
                // GetRepetionIntervalTime()
                while (true)
                {
                    DoJob();
                    Thread.Sleep(GetRepetitionIntervalTime());
                }
            }
            // since there is no repetition, simply execute the job
            DoJob();
        }

        /// <summary>
        /// If this method is overridden, on can get within the job
        /// parameters set just before the job is started. In this
        /// situation the application is running and the use may have
        /// access to resources which he/she has not during the thread
        /// execution. For instance, in a web application, the user has
        /// no access to the application context, when the thread is running.
        /// Note that this method must not be overridden. It is optional.
        /// </summary>
        /// <returns>Parameters to be used in the job.</returns>
        public virtual object GetParameters()
        {
            return null;
        }

        /// <summary>
        /// Get the Job´s Name. This name uniquely identifies the Job.
        /// </summary>
        /// <returns>Job´s name.</returns>
        public abstract string GetName();

        

        /// <summary>
        /// The job to be executed.
        /// </summary>
        public abstract void DoJob();

        /// <summary>
        /// Determines whether a Job is to be repeated after a
        /// certain amount of time.
        /// </summary>
        /// <returns>True in case the Job is to be repeated, false otherwise.</returns>
        public abstract bool IsRepeatable();

        /// <summary>
        /// The amount of time, in milliseconds, which the Job has to wait until it is started
        /// over. This method is only useful if IJob.IsRepeatable() is true, otherwise
        /// its implementation is ignored.
        /// </summary>
        /// <returns>Interval time between this job executions.</returns>
        public abstract int GetRepetitionIntervalTime();
    }
}