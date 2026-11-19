<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Dialog.Logical_Lines{

    public class Variable_Store{

        private const string defaultDatabaseName = "Default";
        public const char databaseVariableRelationalID = '.';
        public static readonly string regexVariableIDs = @"[!]?\$[a-zA-Z0-9_.]+";
        public const char variableID = '$';
        
        public class Database{

            public Database(string name){

                this.name = name;
                variables = new Dictionary<string, Variable>();

            }

            public string name;
            public Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

        }

        public abstract class Variable{

            public abstract object Get();
            public abstract void Set(object value);

        }

        public class Variable<T> : Variable{

            private T value;

            private Func<T> getter;
            private Action<T> setter;

            public Variable(T defaultValue = default, Func<T> getter = null, Action<T> setter = null){

                value = defaultValue;

                if(getter == null){

                    this.getter = () => value;

                }else{

                    this.getter = getter;

                }

                if(setter == null){

                    this.setter = newValue => value = newValue;

                }else{

                    this.setter = setter;

                }

            }

            public override object Get() => getter();

            public override void Set(object newValue) => setter((T)newValue);

        }

        public static Dictionary<string, Database> databases = new Dictionary<string, Database>() { { defaultDatabaseName, new Database(defaultDatabaseName) } };
        private static Database defaultDatabase => databases[defaultDatabaseName];

        public static bool Create_Database(string name){

            if(!databases.ContainsKey(name)){

                databases[name] = new Database(name);
                return true;

            }

            return false;

        }

        private static Database Get_Database(string name){

            if(name == string.Empty){

                return defaultDatabase;

            }

            if(!databases.ContainsKey(name)){

                Create_Database(name);

            }

            return databases[name];

        }

        public static bool Create_Variable<T>(string name, T defaultValue, Func<T> getter = null, Action<T> setter = null){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(database.variables.ContainsKey(variableName)){

                return false;

            }

            database.variables[variableName] = new Variable<T>(defaultValue, getter, setter);

            return true;

        }

        public static bool Try_Get_Value(string name, out object variable){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(!database.variables.ContainsKey(variableName)){

                variable = null;
                return false;

            }

            variable = database.variables[variableName].Get();
            return true;

        }

        public static bool Try_Set_Value<T>(string name, T value){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(!database.variables.ContainsKey(variableName)){

                return false;

            }

            database.variables[variableName].Set(value);
            return true;

        }

        private static(string[], Database, string) Extract_Info(string name){

            string[] parts = name.Split(databaseVariableRelationalID);
            Database database = parts.Length > 1 ? Get_Database(parts[0]) : defaultDatabase;
            string variableName = parts.Length > 1 ? parts[1] : parts[0];

            return (parts, database, variableName);

        }

        public static bool Has_Variable(string name){

            string[] parts = name.Split(databaseVariableRelationalID);
            Database database = parts.Length > 1 ? Get_Database(parts[0]) : defaultDatabase;
            string variableName = parts.Length > 1 ? parts[1] : parts[0];

            return database.variables.ContainsKey(variableName);

        }

        public static void Remove_Variable(string name){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(database.variables.ContainsKey(variableName)){

                database.variables.Remove(variableName);

            }

        }

        public static void Remove_All_Variables(){

            databases.Clear();
            databases[defaultDatabaseName] = new Database(defaultDatabaseName);

        }

        public static void Print_All_Databases(){

            foreach(KeyValuePair<string, Database> dbEntry in databases){

                Debug.Log($"Database: <color=red>{dbEntry.Key}</color>");

            }

        }

        public static void Print_All_Variables(Database database = null){

            if(database != null){

                Print_All_Database_Variables(database);
                return;

            }

            foreach(var dbEntry in databases){

                Print_All_Database_Variables(dbEntry.Value);

            }

        }

        private static void Print_All_Database_Variables(Database database){

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Database: <color=#F38544>{database.name}</color>");

            foreach(KeyValuePair<string, Variable> variablePair in database.variables){

                string variableName = variablePair.Key;
                object variableValue = variablePair.Value.Get();
                stringBuilder.AppendLine($"\t<color=cyan>Variable Name: {variableName}</color> = <color=red>{variableValue}</color>");

            }

            Debug.Log(stringBuilder.ToString());

        }

    }

}
=======
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Dialog.Logical_Lines{

    public class Variable_Store{

        private const string defaultDatabaseName = "Default";
        public const char databaseVariableRelationalID = '.';
        public static readonly string regexVariableIDs = @"[!]?\$[a-zA-Z0-9_.]+";
        public const char variableID = '$';
        
        public class Database{

            public Database(string name){

                this.name = name;
                variables = new Dictionary<string, Variable>();

            }

            public string name;
            public Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

        }

        public abstract class Variable{

            public abstract object Get();
            public abstract void Set(object value);

        }

        public class Variable<T> : Variable{

            private T value;

            private Func<T> getter;
            private Action<T> setter;

            public Variable(T defaultValue = default, Func<T> getter = null, Action<T> setter = null){

                value = defaultValue;

                if(getter == null){

                    this.getter = () => value;

                }else{

                    this.getter = getter;

                }

                if(setter == null){

                    this.setter = newValue => value = newValue;

                }else{

                    this.setter = setter;

                }

            }

            public override object Get() => getter();

            public override void Set(object newValue) => setter((T)newValue);

        }

        public static Dictionary<string, Database> databases = new Dictionary<string, Database>() { { defaultDatabaseName, new Database(defaultDatabaseName) } };
        private static Database defaultDatabase => databases[defaultDatabaseName];

        public static bool Create_Database(string name){

            if(!databases.ContainsKey(name)){

                databases[name] = new Database(name);
                return true;

            }

            return false;

        }

        private static Database Get_Database(string name){

            if(name == string.Empty){

                return defaultDatabase;

            }

            if(!databases.ContainsKey(name)){

                Create_Database(name);

            }

            return databases[name];

        }

        public static bool Create_Variable<T>(string name, T defaultValue, Func<T> getter = null, Action<T> setter = null){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(database.variables.ContainsKey(variableName)){

                return false;

            }

            database.variables[variableName] = new Variable<T>(defaultValue, getter, setter);

            return true;

        }

        public static bool Try_Get_Value(string name, out object variable){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(!database.variables.ContainsKey(variableName)){

                variable = null;
                return false;

            }

            variable = database.variables[variableName].Get();
            return true;

        }

        public static bool Try_Set_Value<T>(string name, T value){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(!database.variables.ContainsKey(variableName)){

                return false;

            }

            database.variables[variableName].Set(value);
            return true;

        }

        private static(string[], Database, string) Extract_Info(string name){

            string[] parts = name.Split(databaseVariableRelationalID);
            Database database = parts.Length > 1 ? Get_Database(parts[0]) : defaultDatabase;
            string variableName = parts.Length > 1 ? parts[1] : parts[0];

            return (parts, database, variableName);

        }

        public static bool Has_Variable(string name){

            string[] parts = name.Split(databaseVariableRelationalID);
            Database database = parts.Length > 1 ? Get_Database(parts[0]) : defaultDatabase;
            string variableName = parts.Length > 1 ? parts[1] : parts[0];

            return database.variables.ContainsKey(variableName);

        }

        public static void Remove_Variable(string name){

            (string[] parts, Database database, string variableName) = Extract_Info(name);

            if(database.variables.ContainsKey(variableName)){

                database.variables.Remove(variableName);

            }

        }

        public static void Remove_All_Variables(){

            databases.Clear();
            databases[defaultDatabaseName] = new Database(defaultDatabaseName);

        }

        public static void Print_All_Databases(){

            foreach(KeyValuePair<string, Database> dbEntry in databases){

                Debug.Log($"Database: <color=red>{dbEntry.Key}</color>");

            }

        }

        public static void Print_All_Variables(Database database = null){

            if(database != null){

                Print_All_Database_Variables(database);
                return;

            }

            foreach(var dbEntry in databases){

                Print_All_Database_Variables(dbEntry.Value);

            }

        }

        private static void Print_All_Database_Variables(Database database){

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Database: <color=#F38544>{database.name}</color>");

            foreach(KeyValuePair<string, Variable> variablePair in database.variables){

                string variableName = variablePair.Key;
                object variableValue = variablePair.Value.Get();
                stringBuilder.AppendLine($"\t<color=cyan>Variable Name: {variableName}</color> = <color=red>{variableValue}</color>");

            }

            Debug.Log(stringBuilder.ToString());

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
