<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace Testing{
    public class Test_Conversations : MonoBehaviour{
        
        [SerializeField] private TextAsset fileToRead = null;
        void Start(){

            StartConversation();

        }

        void StartConversation(){

            List<string> lines = File_Manager.Read_Text_Asset(fileToRead);

            //To test dialog segmenting
            // foreach(string line in lines){

            //     if(string.IsNullOrEmpty(line)){

            //         continue;

            //     }

            //     Debug.Log($"Segmenting: '{line}'");
            //     Dialog_Line dlLine = Dialog_Parser.Parse(line);

            //     int i = 0;
            //     foreach(DL_Dialog_Data.Dialog_Segment segment in dlLine.dialog.segments){

            //         Debug.Log($"Segment [{i++}] = '{segment.dialog}' [signal={segment.startSignal.ToString()}{(segment.signalDelay > 0 ? $" {segment.signalDelay}" : $"")}]");

            //     }

            // }

            //To test speaker segmenting
            // for(int i = 0; i < lines.Count; i++){

            //     string line = lines[i];

            //     if(string.IsNullOrWhiteSpace(line)){

            //         continue;

            //     }

            //     Dialog_Line dl = Dialog_Parser.Parse(line);

            //     Debug.Log($"{dl.speaker.name} as [{(dl.speaker.castName != string.Empty ? dl.speaker.castName : dl.speaker.name)}]at {dl.speaker.castPosition}");
            //     List<(int l, string ex)> expression = dl.speaker.CastExpressions;

            //     for(int c = 0; c < expression.Count; c++){

            //         Debug.Log($"[Layer[{expression[c].l}] = '{expression[c].ex}']");

            //     }

            //     Debug.Log(dl.speaker.displayName);

            // }

            //To test command segmenting
            // foreach(string line in lines){

            //     if(string.IsNullOrEmpty(line)){

            //         continue;

            //     }

            //     Dialog_Line dlLine = Dialog_Parser.Parse(line);

            //     for(int i = 0; i < dlLine.commands.commands.Count; i++){

            //         DL_Command_Data.Command command = dlLine.commands.commands[i];
            //         Debug.Log($"Command [{i}] = '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");

            //     }

            //  }

            Dialog_System.instance.Say(lines);

        }

    }

=======
using System.Collections;
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace Testing{
    public class Test_Conversations : MonoBehaviour{
        
        [SerializeField] private TextAsset fileToRead = null;
        void Start(){

            StartConversation();

        }

        void StartConversation(){

            List<string> lines = File_Manager.Read_Text_Asset(fileToRead);

            //To test dialog segmenting
            // foreach(string line in lines){

            //     if(string.IsNullOrEmpty(line)){

            //         continue;

            //     }

            //     Debug.Log($"Segmenting: '{line}'");
            //     Dialog_Line dlLine = Dialog_Parser.Parse(line);

            //     int i = 0;
            //     foreach(DL_Dialog_Data.Dialog_Segment segment in dlLine.dialog.segments){

            //         Debug.Log($"Segment [{i++}] = '{segment.dialog}' [signal={segment.startSignal.ToString()}{(segment.signalDelay > 0 ? $" {segment.signalDelay}" : $"")}]");

            //     }

            // }

            //To test speaker segmenting
            // for(int i = 0; i < lines.Count; i++){

            //     string line = lines[i];

            //     if(string.IsNullOrWhiteSpace(line)){

            //         continue;

            //     }

            //     Dialog_Line dl = Dialog_Parser.Parse(line);

            //     Debug.Log($"{dl.speaker.name} as [{(dl.speaker.castName != string.Empty ? dl.speaker.castName : dl.speaker.name)}]at {dl.speaker.castPosition}");
            //     List<(int l, string ex)> expression = dl.speaker.CastExpressions;

            //     for(int c = 0; c < expression.Count; c++){

            //         Debug.Log($"[Layer[{expression[c].l}] = '{expression[c].ex}']");

            //     }

            //     Debug.Log(dl.speaker.displayName);

            // }

            //To test command segmenting
            // foreach(string line in lines){

            //     if(string.IsNullOrEmpty(line)){

            //         continue;

            //     }

            //     Dialog_Line dlLine = Dialog_Parser.Parse(line);

            //     for(int i = 0; i < dlLine.commands.commands.Count; i++){

            //         DL_Command_Data.Command command = dlLine.commands.commands[i];
            //         Debug.Log($"Command [{i}] = '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");

            //     }

            //  }

            Dialog_System.instance.Say(lines);

        }

    }

>>>>>>> c8d4f422 (Whole Template)
}