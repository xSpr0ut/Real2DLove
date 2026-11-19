<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using UnityEngine.Events;
using Characters;

namespace Commands{

    public class Command_Manager : MonoBehaviour{

        private const char subCommandID = '.';
        public const string databaseCharactersBase = "characters";
        public const string databaseCharactersSprite = "sprite_characters";
        public static Command_Manager instance {get; private set;}
        private Command_Database database;
        private Dictionary<string, Command_Database> subDatabases = new Dictionary<string, Command_Database>();
        private List<Command_Process> activeProcesses = new List<Command_Process>();
        private Command_Process topProcess => activeProcesses.Last();

        private void Awake(){
        
            if(instance == null){

                instance = this;
                database = new Command_Database();
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] extensionTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Command_Database_Extension))).ToArray();

                foreach(Type extension in extensionTypes){

                    MethodInfo extendMethod = extension.GetMethod("Extend");
                    extendMethod.Invoke(null, new object[] {database});

                }

            }else{

                DestroyImmediate(gameObject);

            }

        }

        public Coroutine_Wrapper Execute(string commandName, params string[] arguments){

            if(commandName.Contains(subCommandID)){

                return Execute_Sub_Command(commandName, arguments);

            }
            
            Delegate command = database.Get_Command(commandName);

            if(command == null){

                return null;

            }

            return Start_Process(commandName, command, arguments);

        }  

        private Coroutine_Wrapper Execute_Sub_Command(string commandName, string[] arguments){

            string[] parts = commandName.Split(subCommandID);
            string databaseName = string.Join(subCommandID, parts.Take(parts.Length - 1));
            string subCommandName = parts.Last();

            if(subDatabases.ContainsKey(databaseName)){

                Delegate command = subDatabases[databaseName].Get_Command(subCommandName);

                if(command != null){

                    return Start_Process(commandName, command, arguments);

                }else{

                    Debug.LogError($"{subCommandName} does not exist in sub database '{databaseName}'");
                    return null;

                }

            }

            //Running character command
            string characterName = databaseName;

            if(Character_Manager.instance.Has_Character(characterName)){

                List<string> newArguments = new List<string>(arguments);
                newArguments.Insert(0, databaseName);
                arguments = newArguments.ToArray();

                return Execute_Character_Command(subCommandName, arguments);

            }

            Debug.LogError($"Sub database '{databaseName}' does not exist.");
            return null;

        }

        private Coroutine_Wrapper Execute_Character_Command(string commandName, params string[] arguments){

            Delegate command = null;
            Command_Database database = subDatabases[databaseCharactersBase];

            if(database.hasCommand(commandName)){

                command = database.Get_Command(commandName);
                return Start_Process(commandName, command, arguments);

            }

            Character_Configuration_Data characterConfigurationData = Character_Manager.instance.Get_Character_Configuration_Data(arguments[0]);

            database = subDatabases[databaseCharactersSprite];
            command = database.Get_Command(commandName);

            if(command != null){

                return Start_Process(commandName, command, arguments);

            }

            Debug.LogError($"{commandName} could not be executed on {arguments[0]}. The character name or command is wrong.");
            return null;

        }

        private Coroutine_Wrapper Start_Process(string commandName, Delegate command, string[] arguments){

            Guid processID = Guid.NewGuid();
            Command_Process commandProcess = new Command_Process(processID, commandName, command, null, arguments, null);
            activeProcesses.Add(commandProcess);
            Coroutine coroutine = StartCoroutine(Running_Process(commandProcess));
            commandProcess.runningProcess = new Coroutine_Wrapper(this, coroutine);
            
            return commandProcess.runningProcess;

        }

        public void Stop_Process(){

            if(topProcess != null){

                Kill_Process(topProcess);

            }

        }

        public void Stop_All_Processes(){

            foreach(var commandProcess in activeProcesses){

                if(commandProcess.runningProcess != null && !commandProcess.runningProcess.isDone){

                    commandProcess.runningProcess.Stop();

                }

                commandProcess.onTerminateAction?.Invoke();

            }

            activeProcesses.Clear();

        }

        private IEnumerator Running_Process(Command_Process process){

            yield return Waiting_For_End_of_Process(process.command, process.arguments);
            Kill_Process(process);

        }

        private void Kill_Process(Command_Process commandProcess){

            activeProcesses.Remove(commandProcess);

            if(commandProcess.runningProcess != null && !commandProcess.runningProcess.isDone){

                commandProcess.runningProcess.Stop();

            }

            commandProcess.onTerminateAction?.Invoke();

        }

        private IEnumerator Waiting_For_End_of_Process(Delegate command, string[] arguments){

            if(command is Action){

                command.DynamicInvoke();

            }else if(command is Action<string>){

                command.DynamicInvoke(arguments.Length == 0 ? string.Empty : arguments[0]);

            }else if(command is Action<string[]>){

                command.DynamicInvoke((object)arguments);

            }else if(command is Func<IEnumerator>){

                yield return ((Func<IEnumerator>)command)();

            }else if(command is Func<string, IEnumerator>){

                yield return ((Func<string, IEnumerator>)command)(arguments.Length == 0 ? string.Empty : arguments[0]);

            }else if(command is Func<string[], IEnumerator>){

                yield return ((Func<string[], IEnumerator>)command)(arguments);

            }

        }

        public void Add_Termination_Action_To_Current_Process(UnityAction action){

            Command_Process process = topProcess;

            if(process == null){

                return;

            }

            process.onTerminateAction = new UnityEvent();
            process.onTerminateAction.AddListener(action);

        }

        public Command_Database Create_Sub_Database(string name){

            name = name.ToLower();

            if(subDatabases.TryGetValue(name, out Command_Database database)){

                Debug.LogWarning($"{name} already exists :P");
                return database;

            }

            Command_Database newDatabase = new Command_Database();
            subDatabases.Add(name, newDatabase);

            return newDatabase;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using UnityEngine.Events;
using Characters;

namespace Commands{

    public class Command_Manager : MonoBehaviour{

        private const char subCommandID = '.';
        public const string databaseCharactersBase = "characters";
        public const string databaseCharactersSprite = "sprite_characters";
        public static Command_Manager instance {get; private set;}
        private Command_Database database;
        private Dictionary<string, Command_Database> subDatabases = new Dictionary<string, Command_Database>();
        private List<Command_Process> activeProcesses = new List<Command_Process>();
        private Command_Process topProcess => activeProcesses.Last();

        private void Awake(){
        
            if(instance == null){

                instance = this;
                database = new Command_Database();
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] extensionTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Command_Database_Extension))).ToArray();

                foreach(Type extension in extensionTypes){

                    MethodInfo extendMethod = extension.GetMethod("Extend");
                    extendMethod.Invoke(null, new object[] {database});

                }

            }else{

                DestroyImmediate(gameObject);

            }

        }

        public Coroutine_Wrapper Execute(string commandName, params string[] arguments){

            if(commandName.Contains(subCommandID)){

                return Execute_Sub_Command(commandName, arguments);

            }
            
            Delegate command = database.Get_Command(commandName);

            if(command == null){

                return null;

            }

            return Start_Process(commandName, command, arguments);

        }  

        private Coroutine_Wrapper Execute_Sub_Command(string commandName, string[] arguments){

            string[] parts = commandName.Split(subCommandID);
            string databaseName = string.Join(subCommandID, parts.Take(parts.Length - 1));
            string subCommandName = parts.Last();

            if(subDatabases.ContainsKey(databaseName)){

                Delegate command = subDatabases[databaseName].Get_Command(subCommandName);

                if(command != null){

                    return Start_Process(commandName, command, arguments);

                }else{

                    Debug.LogError($"{subCommandName} does not exist in sub database '{databaseName}'");
                    return null;

                }

            }

            //Running character command
            string characterName = databaseName;

            if(Character_Manager.instance.Has_Character(characterName)){

                List<string> newArguments = new List<string>(arguments);
                newArguments.Insert(0, databaseName);
                arguments = newArguments.ToArray();

                return Execute_Character_Command(subCommandName, arguments);

            }

            Debug.LogError($"Sub database '{databaseName}' does not exist.");
            return null;

        }

        private Coroutine_Wrapper Execute_Character_Command(string commandName, params string[] arguments){

            Delegate command = null;
            Command_Database database = subDatabases[databaseCharactersBase];

            if(database.hasCommand(commandName)){

                command = database.Get_Command(commandName);
                return Start_Process(commandName, command, arguments);

            }

            Character_Configuration_Data characterConfigurationData = Character_Manager.instance.Get_Character_Configuration_Data(arguments[0]);

            database = subDatabases[databaseCharactersSprite];
            command = database.Get_Command(commandName);

            if(command != null){

                return Start_Process(commandName, command, arguments);

            }

            Debug.LogError($"{commandName} could not be executed on {arguments[0]}. The character name or command is wrong.");
            return null;

        }

        private Coroutine_Wrapper Start_Process(string commandName, Delegate command, string[] arguments){

            Guid processID = Guid.NewGuid();
            Command_Process commandProcess = new Command_Process(processID, commandName, command, null, arguments, null);
            activeProcesses.Add(commandProcess);
            Coroutine coroutine = StartCoroutine(Running_Process(commandProcess));
            commandProcess.runningProcess = new Coroutine_Wrapper(this, coroutine);
            
            return commandProcess.runningProcess;

        }

        public void Stop_Process(){

            if(topProcess != null){

                Kill_Process(topProcess);

            }

        }

        public void Stop_All_Processes(){

            foreach(var commandProcess in activeProcesses){

                if(commandProcess.runningProcess != null && !commandProcess.runningProcess.isDone){

                    commandProcess.runningProcess.Stop();

                }

                commandProcess.onTerminateAction?.Invoke();

            }

            activeProcesses.Clear();

        }

        private IEnumerator Running_Process(Command_Process process){

            yield return Waiting_For_End_of_Process(process.command, process.arguments);
            Kill_Process(process);

        }

        private void Kill_Process(Command_Process commandProcess){

            activeProcesses.Remove(commandProcess);

            if(commandProcess.runningProcess != null && !commandProcess.runningProcess.isDone){

                commandProcess.runningProcess.Stop();

            }

            commandProcess.onTerminateAction?.Invoke();

        }

        private IEnumerator Waiting_For_End_of_Process(Delegate command, string[] arguments){

            if(command is Action){

                command.DynamicInvoke();

            }else if(command is Action<string>){

                command.DynamicInvoke(arguments.Length == 0 ? string.Empty : arguments[0]);

            }else if(command is Action<string[]>){

                command.DynamicInvoke((object)arguments);

            }else if(command is Func<IEnumerator>){

                yield return ((Func<IEnumerator>)command)();

            }else if(command is Func<string, IEnumerator>){

                yield return ((Func<string, IEnumerator>)command)(arguments.Length == 0 ? string.Empty : arguments[0]);

            }else if(command is Func<string[], IEnumerator>){

                yield return ((Func<string[], IEnumerator>)command)(arguments);

            }

        }

        public void Add_Termination_Action_To_Current_Process(UnityAction action){

            Command_Process process = topProcess;

            if(process == null){

                return;

            }

            process.onTerminateAction = new UnityEvent();
            process.onTerminateAction.AddListener(action);

        }

        public Command_Database Create_Sub_Database(string name){

            name = name.ToLower();

            if(subDatabases.TryGetValue(name, out Command_Database database)){

                Debug.LogWarning($"{name} already exists :P");
                return database;

            }

            Command_Database newDatabase = new Command_Database();
            subDatabases.Add(name, newDatabase);

            return newDatabase;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
