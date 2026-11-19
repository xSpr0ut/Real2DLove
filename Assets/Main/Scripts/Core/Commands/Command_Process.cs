<<<<<<< HEAD
using System;
using UnityEngine.Events;

namespace Commands{

    public class Command_Process{
        
        public Guid ID;
        public string processName;
        public Delegate command;
        public Coroutine_Wrapper runningProcess;
        public string[] arguments;
        public UnityEvent onTerminateAction;

        public Command_Process(Guid iD, string processName, Delegate command, Coroutine_Wrapper runningProcess, string[] arguments, UnityEvent onTerminateAction = null){

            ID = iD;
            this.processName = processName;
            this.command = command;
            this.runningProcess = runningProcess;
            this.arguments = arguments;
            this.onTerminateAction = onTerminateAction;

        }

    }

}
=======
using System;
using UnityEngine.Events;

namespace Commands{

    public class Command_Process{
        
        public Guid ID;
        public string processName;
        public Delegate command;
        public Coroutine_Wrapper runningProcess;
        public string[] arguments;
        public UnityEvent onTerminateAction;

        public Command_Process(Guid iD, string processName, Delegate command, Coroutine_Wrapper runningProcess, string[] arguments, UnityEvent onTerminateAction = null){

            ID = iD;
            this.processName = processName;
            this.command = command;
            this.runningProcess = runningProcess;
            this.arguments = arguments;
            this.onTerminateAction = onTerminateAction;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
