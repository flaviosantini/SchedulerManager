using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SchedulerManager.Configuration;

namespace SchedulerManager.Mechanism
{
    /// <summary>
    /// Job mechanism manager.
    /// </summary>
    public class JobManager
    {
        //private ILog log = LogManager.GetLogger(Log4NetConstants.SCHEDULER_LOGGER);

        /// <summary>
        /// Execute all Jobs.
        /// </summary>
        public void ExecuteAllJobs()
        {
            Console.WriteLine("Begin Method");

            try
            {
                // get all job implementations of this assembly.
                var jobs = GetAllTypesImplementingInterface(typeof(Job));
                // execute each job
                var enumerable = jobs as Type[] ?? jobs.ToArray();
                if (jobs != null && enumerable.Any())
                {
                    foreach (var job in enumerable)
                    {
                        // only instantiate the job its implementation is "real"
                        if (IsRealClass(job))
                        {
                            try
                            {
                                // instantiate job by reflection
                                // Create a new Job per Client
                                foreach (var client in Clients.GetClients())
                                {
                                    var instanceJob = (Job)Activator.CreateInstance(job);
                                    instanceJob.Client = client;
                                    Console.WriteLine("The Job \"{0}\" has been instantiated successfully for client {1}.", instanceJob.GetName(), client.Id);
                                    // create thread for this job execution method
                                    var thread = new Thread(instanceJob.ExecuteJob);
                                    // start thread executing the job
                                    thread.Start();
                                    Console.WriteLine("The Job \"{0}\" has its thread started successfully for client {1}.", instanceJob.GetName(), client.Id);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(
                                    $"Error! The Job \"{job.Name}\" could not " + "be instantiated or executed.", ex);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not running this Job [\"{0}\"]. It is not a concrete object.", job.FullName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! An error has occurred while instantiating " +
                  "or executing Jobs for the Scheduler Framework. {0}", ex);
            }

            Console.WriteLine("End Method");
        }

        /// <summary>
        /// Returns all types in the current AppDomain implementing the interface or inheriting the type. 
        /// </summary>
        private static IEnumerable<Type> GetAllTypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(desiredType.IsAssignableFrom);

        }

        /// <summary>
        /// Determine whether the object is real - non-abstract, non-generic-needed, non-interface class.
        /// </summary>
        /// <param name="testType">Type to be verified.</param>
        /// <returns>True in case the class is real, false otherwise.</returns>
        public static bool IsRealClass(Type testType)
        {
            return testType.IsAbstract == false
                && testType.IsGenericTypeDefinition == false
                && testType.IsInterface == false;
        }
    }
} 
