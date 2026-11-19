<<<<<<< HEAD
using System.Collections;

namespace Dialog.Logical_Lines.Input{

    public class LL_Input : ILogical_Lines{

        public string keyword => "input";
        private const string titleEmptyID = "EMP%";
        
        public IEnumerator Execute(Dialog_Line line){
            
            string title = line.dialog.rawData;

            if(title == titleEmptyID){

                title = string.Empty;
                
            }

            Input_Panel panel = Input_Panel.instance;
            panel.Show(title);

            while(panel.isWaitingOnPlayerInput){

                yield return null;

            }

        }

        public bool Matches(Dialog_Line line){
            
            return line.hasSpeaker && line.speaker.name.ToLower() == keyword;
            
        }

    }

}
=======
using System.Collections;

namespace Dialog.Logical_Lines.Input{

    public class LL_Input : ILogical_Lines{

        public string keyword => "input";
        private const string titleEmptyID = "EMP%";
        
        public IEnumerator Execute(Dialog_Line line){
            
            string title = line.dialog.rawData;

            if(title == titleEmptyID){

                title = string.Empty;
                
            }

            Input_Panel panel = Input_Panel.instance;
            panel.Show(title);

            while(panel.isWaitingOnPlayerInput){

                yield return null;

            }

        }

        public bool Matches(Dialog_Line line){
            
            return line.hasSpeaker && line.speaker.name.ToLower() == keyword;
            
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
