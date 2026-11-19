<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public class Command_Database{
    
        private Dictionary<string, Delegate> database = new Dictionary<string, Delegate>();
        public bool hasCommand(string commandName) => database.ContainsKey(commandName.ToLower());

        public void Add_Command(string commandName, Delegate command){

            commandName = commandName.ToLower();

            if(!database.ContainsKey(commandName)){

                database.Add(commandName, command);

            }else{

                Debug.LogError($"Command already exists in the database '{commandName}'");

            }

        }

        public Delegate Get_Command(string commandName){

            commandName = commandName.ToLower();

            if(!database.ContainsKey(commandName)){

                Debug.LogError($"Command '{commandName}' does not exist in the database.");
                return null;

            }
            
            return database[commandName];

        }

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public class Command_Database{
    
        private Dictionary<string, Delegate> database = new Dictionary<string, Delegate>();
        public bool hasCommand(string commandName) => database.ContainsKey(commandName.ToLower());

        public void Add_Command(string commandName, Delegate command){

            commandName = commandName.ToLower();

            if(!database.ContainsKey(commandName)){

                database.Add(commandName, command);

            }else{

                Debug.LogError($"Command already exists in the database '{commandName}'");

            }

        }

        public Delegate Get_Command(string commandName){

            commandName = commandName.ToLower();

            if(!database.ContainsKey(commandName)){

                Debug.LogError($"Command '{commandName}' does not exist in the database.");
                return null;

            }
            
            return database[commandName];

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
