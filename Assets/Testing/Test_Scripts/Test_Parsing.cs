<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace Testing{

    public class Test_Parsing : MonoBehaviour{
        
        void Start(){

            string line = "John \"My nuclear fish is called \\\"Nuke\\\".\" Command(string)";
            Dialog_Parser.Parse(line);

            Send_File_To_Parse();
            
        }

        void Send_File_To_Parse(){

            List<string> lines = File_Manager.Read_Text_Asset("testFile");

            foreach(string line in lines){

                if(line == string.Empty){

                    continue;

                }

                Dialog_Line dl = Dialog_Parser.Parse(line);

            }

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace Testing{

    public class Test_Parsing : MonoBehaviour{
        
        void Start(){

            string line = "John \"My nuclear fish is called \\\"Nuke\\\".\" Command(string)";
            Dialog_Parser.Parse(line);

            Send_File_To_Parse();
            
        }

        void Send_File_To_Parse(){

            List<string> lines = File_Manager.Read_Text_Asset("testFile");

            foreach(string line in lines){

                if(line == string.Empty){

                    continue;

                }

                Dialog_Line dl = Dialog_Parser.Parse(line);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
