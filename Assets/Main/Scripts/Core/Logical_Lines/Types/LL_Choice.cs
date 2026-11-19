<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu.Config;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Encapsulation;

namespace Dialog.Logical_Lines.Choices{

    public class LL_Choice : ILogical_Lines{
        public string keyword => "choice";
        private const char choiceIdentifier = '#';
        private const string titleEmptyID = "EMP%";

        public IEnumerator Execute(Dialog_Line line){

            var currentConversation = Dialog_System.instance.conversationManager.conversation;
            var progress = Dialog_System.instance.conversationManager.Conversation_Progress;
            
            Encapsulated_Data data = Rip_Encapsulation_Data(currentConversation, progress, true, parentStartingIndex: currentConversation.fileStartIndex);

            List<Choice> choices = Get_Choices_From_Data(data);

            string title = line.dialog.rawData;

            if(title == titleEmptyID){

                title = string.Empty;
                
            }

            Choice_Panel panel = Choice_Panel.instance;
            string[] choiceTitles = choices.Select(c => c.title).ToArray();

            panel.Show(title, choiceTitles);

            while(panel.isWaitingOnPlayerChoice){

                yield return null;

            }

            Choice selectedChoice = choices[panel.lastDecision.answerIndex];

            Conversation newConversation = new Conversation(selectedChoice.resultLines, file: currentConversation.file, fileStartIndex: selectedChoice.startIndex, fileEndIndex: selectedChoice.endIndex);
            Dialog_System.instance.conversationManager.conversation.Set_Progress(data.endingIndex - currentConversation.fileStartIndex);
            Dialog_System.instance.conversationManager.Enqueue_Priority(newConversation);

            Auto_Reader autoReader = Dialog_System.instance.autoReader;

            if(autoReader != null && autoReader.isOn && autoReader.skip){

                if(VN_Configuration.activeConfig != null && !VN_Configuration.activeConfig.continueSkippingAfterChoice){
                    
                    autoReader.Disable();

                }

            }

        }

        public bool Matches(Dialog_Line line){
            
            return line.hasSpeaker && line.speaker.name.ToLower() == keyword;

        }

        private List<Choice> Get_Choices_From_Data(Encapsulated_Data data){

            List<Choice> choices = new List<Choice>();
            int encapsulationDepth = 0;
            bool isFirstChoice = true;

            Choice choice = new Choice{

                title = string.Empty,
                resultLines = new List<string>(),

            };

            int choiceIndex = 0, i = 0;

            for(i = 1; i < data.lines.Count; i++){

                var line = data.lines[i];
                
                if(Is_Choice_Start(line) && encapsulationDepth == 1){

                    if(!isFirstChoice){

                        choice.startIndex = data.startingIndex + choiceIndex + 1;
                        choice.endIndex = data.startingIndex + (i - 1);
                        choices.Add(choice);
                        
                        choice = new Choice{

                            title = string.Empty,
                            resultLines = new List<string>(),

                        };

                    }

                    choiceIndex = i;

                    choice.title = line.Trim().Substring(1);
                    choice.title = Tag_Manager.Inject(choice.title, true, true);
                    isFirstChoice = false;
                    continue;

                }

                Add_Line_To_Results(line, ref choice, ref encapsulationDepth);

            }

            if(!choices.Contains(choice)){

                choice.startIndex = data.startingIndex + choiceIndex + 1;
                choice.endIndex = data.startingIndex + (i - 2);
                choices.Add(choice);

            }

            return choices;
            
        }

        private void Add_Line_To_Results(string line, ref Choice choice, ref int encapsulationDepth){

            line.Trim();

            if(Is_Encapsulation_Start(line)){

                if(encapsulationDepth > 0){

                    choice.resultLines.Add(line);

                }

                encapsulationDepth++;
                return;

            }

            if(Is_Encapsulation_End(line)){

                encapsulationDepth--;

                if(encapsulationDepth > 0){

                    choice.resultLines.Add(line);

                }

                return;

            }

            choice.resultLines.Add(line);

        }
        private bool Is_Choice_Start(string line) => line.Trim().StartsWith(choiceIdentifier);

        private struct Choice{

            public string title;
            public List<string> resultLines;
            public int startIndex;
            public int endIndex;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu.Config;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Encapsulation;

namespace Dialog.Logical_Lines.Choices{

    public class LL_Choice : ILogical_Lines{
        public string keyword => "choice";
        private const char choiceIdentifier = '#';
        private const string titleEmptyID = "EMP%";

        public IEnumerator Execute(Dialog_Line line){

            var currentConversation = Dialog_System.instance.conversationManager.conversation;
            var progress = Dialog_System.instance.conversationManager.Conversation_Progress;
            
            Encapsulated_Data data = Rip_Encapsulation_Data(currentConversation, progress, true, parentStartingIndex: currentConversation.fileStartIndex);

            List<Choice> choices = Get_Choices_From_Data(data);

            string title = line.dialog.rawData;

            if(title == titleEmptyID){

                title = string.Empty;
                
            }

            Choice_Panel panel = Choice_Panel.instance;
            string[] choiceTitles = choices.Select(c => c.title).ToArray();

            panel.Show(title, choiceTitles);

            while(panel.isWaitingOnPlayerChoice){

                yield return null;

            }

            Choice selectedChoice = choices[panel.lastDecision.answerIndex];

            Conversation newConversation = new Conversation(selectedChoice.resultLines, file: currentConversation.file, fileStartIndex: selectedChoice.startIndex, fileEndIndex: selectedChoice.endIndex);
            Dialog_System.instance.conversationManager.conversation.Set_Progress(data.endingIndex - currentConversation.fileStartIndex);
            Dialog_System.instance.conversationManager.Enqueue_Priority(newConversation);

            Auto_Reader autoReader = Dialog_System.instance.autoReader;

            if(autoReader != null && autoReader.isOn && autoReader.skip){

                if(VN_Configuration.activeConfig != null && !VN_Configuration.activeConfig.continueSkippingAfterChoice){
                    
                    autoReader.Disable();

                }

            }

        }

        public bool Matches(Dialog_Line line){
            
            return line.hasSpeaker && line.speaker.name.ToLower() == keyword;

        }

        private List<Choice> Get_Choices_From_Data(Encapsulated_Data data){

            List<Choice> choices = new List<Choice>();
            int encapsulationDepth = 0;
            bool isFirstChoice = true;

            Choice choice = new Choice{

                title = string.Empty,
                resultLines = new List<string>(),

            };

            int choiceIndex = 0, i = 0;

            for(i = 1; i < data.lines.Count; i++){

                var line = data.lines[i];
                
                if(Is_Choice_Start(line) && encapsulationDepth == 1){

                    if(!isFirstChoice){

                        choice.startIndex = data.startingIndex + choiceIndex + 1;
                        choice.endIndex = data.startingIndex + (i - 1);
                        choices.Add(choice);
                        
                        choice = new Choice{

                            title = string.Empty,
                            resultLines = new List<string>(),

                        };

                    }

                    choiceIndex = i;

                    choice.title = line.Trim().Substring(1);
                    choice.title = Tag_Manager.Inject(choice.title, true, true);
                    isFirstChoice = false;
                    continue;

                }

                Add_Line_To_Results(line, ref choice, ref encapsulationDepth);

            }

            if(!choices.Contains(choice)){

                choice.startIndex = data.startingIndex + choiceIndex + 1;
                choice.endIndex = data.startingIndex + (i - 2);
                choices.Add(choice);

            }

            return choices;
            
        }

        private void Add_Line_To_Results(string line, ref Choice choice, ref int encapsulationDepth){

            line.Trim();

            if(Is_Encapsulation_Start(line)){

                if(encapsulationDepth > 0){

                    choice.resultLines.Add(line);

                }

                encapsulationDepth++;
                return;

            }

            if(Is_Encapsulation_End(line)){

                encapsulationDepth--;

                if(encapsulationDepth > 0){

                    choice.resultLines.Add(line);

                }

                return;

            }

            choice.resultLines.Add(line);

        }
        private bool Is_Choice_Start(string line) => line.Trim().StartsWith(choiceIdentifier);

        private struct Choice{

            public string title;
            public List<string> resultLines;
            public int startIndex;
            public int endIndex;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
