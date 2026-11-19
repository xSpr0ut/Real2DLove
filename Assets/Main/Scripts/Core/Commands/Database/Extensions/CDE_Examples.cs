<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Commands;

namespace Testing{

    public class CDE_Examples : Command_Database_Extension{
        
        new public static void Extend(Command_Database database){ 

            //To add commands to the database, they must be static

            //To add an Action with no parameters
            database.Add_Command("print", new Action(Print_Default_Message));

            //To add an Action with parameters
            database.Add_Command("print_1parameter", new Action<string>(Print_Player_Message));
            database.Add_Command("print_2parameter", new Action<string[]>(Print_Lines));

            //To add a lamda without parameters
            database.Add_Command("lambda", new Action(() => {Debug.Log("Nuclear fish");} ));
            
            //To add lamdas with parameters
            database.Add_Command("lambda_1parameter", new Action<string>((argument) => {Debug.Log($"Player Message: '{argument}'");}));
            database.Add_Command("lambda_2parameter", new Action<string[]>((arguments) => {Debug.Log(string.Join(", ", arguments));}));

            //To add coroutines without parameters
            database.Add_Command("process", new Func<IEnumerator>(Example_Process));

            //To add coroutines with parameters
            database.Add_Command("process_1parameter", new Func<string, IEnumerator>(Example_Process1));
            database.Add_Command("process_2parameter", new Func<string[], IEnumerator>(Example_Process2));

            //Special examples
            
        }

        private static void Print_Default_Message(){

            Debug.Log("Nuclear fish.");

        }

        private static void Print_Player_Message(string message){

            Debug.Log($"Player Message: '{message}'");

        }

        private static void Print_Lines(string[] lines){

            int i = 1;

            foreach(string line in lines){
                
                Debug.Log($"{i++}. '{line}'");

            }

        }

        private static IEnumerator Example_Process(){

            for(int i = 1; i <= 5; i++){

                Debug.Log($"Process Running...[{i}]");
                yield return new WaitForSeconds(1);

            }

        }

        private static IEnumerator Example_Process1(string data){

            if(int.TryParse(data, out int num)){

                for(int i = 1; i <= num; i++){

                    Debug.Log($"Process Running...[{i}]");
                    yield return new WaitForSeconds(1);

                }

            }

        }

        private static IEnumerator Example_Process2(string[] data){

            foreach(string line in data){

                Debug.Log($"Process Message: '{line}'");
                yield return new WaitForSeconds(0.5f);

            }

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Commands;

namespace Testing{

    public class CDE_Examples : Command_Database_Extension{
        
        new public static void Extend(Command_Database database){ 

            //To add commands to the database, they must be static

            //To add an Action with no parameters
            database.Add_Command("print", new Action(Print_Default_Message));

            //To add an Action with parameters
            database.Add_Command("print_1parameter", new Action<string>(Print_Player_Message));
            database.Add_Command("print_2parameter", new Action<string[]>(Print_Lines));

            //To add a lamda without parameters
            database.Add_Command("lambda", new Action(() => {Debug.Log("Nuclear fish");} ));
            
            //To add lamdas with parameters
            database.Add_Command("lambda_1parameter", new Action<string>((argument) => {Debug.Log($"Player Message: '{argument}'");}));
            database.Add_Command("lambda_2parameter", new Action<string[]>((arguments) => {Debug.Log(string.Join(", ", arguments));}));

            //To add coroutines without parameters
            database.Add_Command("process", new Func<IEnumerator>(Example_Process));

            //To add coroutines with parameters
            database.Add_Command("process_1parameter", new Func<string, IEnumerator>(Example_Process1));
            database.Add_Command("process_2parameter", new Func<string[], IEnumerator>(Example_Process2));

            //Special examples
            
        }

        private static void Print_Default_Message(){

            Debug.Log("Nuclear fish.");

        }

        private static void Print_Player_Message(string message){

            Debug.Log($"Player Message: '{message}'");

        }

        private static void Print_Lines(string[] lines){

            int i = 1;

            foreach(string line in lines){
                
                Debug.Log($"{i++}. '{line}'");

            }

        }

        private static IEnumerator Example_Process(){

            for(int i = 1; i <= 5; i++){

                Debug.Log($"Process Running...[{i}]");
                yield return new WaitForSeconds(1);

            }

        }

        private static IEnumerator Example_Process1(string data){

            if(int.TryParse(data, out int num)){

                for(int i = 1; i <= num; i++){

                    Debug.Log($"Process Running...[{i}]");
                    yield return new WaitForSeconds(1);

                }

            }

        }

        private static IEnumerator Example_Process2(string[] data){

            foreach(string line in data){

                Debug.Log($"Process Message: '{line}'");
                yield return new WaitForSeconds(0.5f);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
