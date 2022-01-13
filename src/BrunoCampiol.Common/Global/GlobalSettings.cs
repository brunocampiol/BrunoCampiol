using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.CrossCutting.Common.Global
{
    /*
        This approach ensures that only one instance is created and only when the instance is needed.
        Also, the variable is declared to be volatile to ensure that assignment to the instance variable 
        completes before the instance variable can be accessed.Lastly, this approach uses a syncRoot 
        instance to lock on, rather than locking on the type itself, to avoid deadlocks.

        This double-check locking approach solves the thread concurrency problems while avoiding an 
        exclusive lock in every call to the Instance property method.
        It also allows you to delay instantiation until the object is first accessed. In practice, 
        an application rarely requires this type of implementation.In most cases, the static initialization 
        approach is sufficient.
        https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Singleton?term=SOLID%20Design%20Patterns
        */

    /// <summary>
    /// Purpose of this class is to hold global settings for solution
    /// </summary>
    public sealed class GlobalSettings : IDisposable
    {
        private bool _disposed;
        //the volatile keyword ensures that the instantiation is complete 
        //before it can be accessed further helping with thread safety.
        private static volatile GlobalSettings _instance;
        private static readonly object _syncLock = new object();

        // No one but itself can instanciate
        private GlobalSettings()
        {
        }

        // Uses a pattern known as double check locking
        public static GlobalSettings Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new GlobalSettings();
                    }
                }
                return _instance;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by Dispose method.
            // Therefore, you should call GG.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the methos das been called directly
        // or indirectly by a uses's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged rerousces can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (_disposed) return;
            // If disposing equals true, dispose all managed
            // and unmanaged resources
            if (disposing)
            {
                _instance = null;
                // Dispose managed resources.
            }

            // Call the appropriate methods to clean up
            // unmanaged 
            _disposed = true;
        }

        /// <summary>
        /// The current build type (Debug/Release)
        /// </summary>
        public string BuildFlavor { get; set; }

        /// <summary>
        /// The current appSettings.json file type (Development/Production)
        /// </summary>
        public string AppSettingsFlavor { get; set; }

        /// <summary>
        /// Current database connection string
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
