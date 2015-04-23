using System;
using System.Runtime.InteropServices;
using SchedulerManager.Mechanism;

namespace SchedulerManager.Jobs
{
    class RepeatableJob2Seconds : Job
    {
        /// <summary>
        /// Counter used to count the number of times this job has been
        /// executed.
        /// </summary>
        private int _counter;

        /// <summary>
        /// Get the Job Name, which reflects the class name.
        /// </summary>
        /// <returns>The class Name.</returns>
        public override string GetName()
        {
            return GetType().Name;
        }

        /// <summary>
        /// Execute the Job itself. Just print a message.
        /// </summary>
        public override void DoJob()
        {
            for (var i = 0; i < 10000; i++)
            {
                var number = i;
            }
            Console.WriteLine("This is the execution number \"{0}\" of the Job \"{1}\" for the client Id: {2} and Name: {3}.", _counter, GetName(), Client.Id, Client.Name);
            _counter++;
        }

        /// <summary>
        /// Determines this job is repeatable.
        /// </summary>
        /// <returns>Returns true because this job is repeatable.</returns>
        public override bool IsRepeatable()
        {
            return true;
        }

        /// <summary>
        /// Determines that this job is to be executed again after
        /// some amount of seconds.
        /// </summary>
        /// <returns>1 sec, which is the interval this job is to be
        /// executed repeatedly.</returns>
        public override int GetRepetitionIntervalTime()
        {
            //milliseconds * number of seconds
            return 1000 * 2;
        }
    }
}
