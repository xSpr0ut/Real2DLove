<<<<<<< HEAD
using System.Collections;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Encapsulation;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Condition;

namespace Dialog.Logical_Lines{

    public class LL_Condition : ILogical_Lines{

        public string keyword => "if";
        private const string elseID = "else"; 
        private readonly string[] containers = new string[]{ "(", ")" };

        public IEnumerator Execute(Dialog_Line line){
            
            string rawCondition = Extract_Condition(line.rawData.Trim());
            bool conditionResult = Evaluate_Condition(rawCondition);

            Conversation currentConversation = Dialog_System.instance.conversationManager.conversation;
            int currentProgress = Dialog_System.instance.conversationManager.Conversation_Progress;

            Encapsulated_Data ifData = Rip_Encapsulation_Data(currentConversation, currentProgress, false, parentStartingIndex: currentConversation.fileStartIndex);
            Encapsulated_Data elseData = new Encapsulated_Data();

            if(ifData.endingIndex + 1 < currentConversation.Count){

                string nextLine = currentConversation.Get_Lines()[ifData.endingIndex + 1].Trim();

                if(nextLine == elseID){

                    elseData = Rip_Encapsulation_Data(currentConversation, ifData.endingIndex + 1, false, parentStartingIndex: currentConversation.fileStartIndex);
                    ifData.endingIndex = elseData.endingIndex;

                }

            }

            currentConversation.Set_Progress(elseData.isNull ? ifData.endingIndex : elseData.endingIndex);

            Encapsulated_Data selData = conditionResult ? ifData : elseData;

            if(!selData.isNull && selData.lines.Count > 0){

                //Removing the header & encapsulator lines
                selData.startingIndex += 2;
                selData.endingIndex -= 1;

                Conversation newConversation = new Conversation(selData.lines, file: currentConversation.file, fileStartIndex: selData.startingIndex, fileEndIndex: selData.endingIndex);
                Dialog_System.instance.conversationManager.Enqueue_Priority(newConversation);

            }

            yield return null;

        }

        public bool Matches(Dialog_Line line){
            
            return line.rawData.Trim().StartsWith(keyword);

        }

        private string Extract_Condition(string line){

            int startIndex = line.IndexOf(containers[0]) + 1;
            int endIndex = line.IndexOf(containers[1]);

            return line.Substring(startIndex, endIndex - startIndex).Trim();

        }

    }

}
=======
using System.Collections;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Encapsulation;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Condition;

namespace Dialog.Logical_Lines{

    public class LL_Condition : ILogical_Lines{

        public string keyword => "if";
        private const string elseID = "else"; 
        private readonly string[] containers = new string[]{ "(", ")" };

        public IEnumerator Execute(Dialog_Line line){
            
            string rawCondition = Extract_Condition(line.rawData.Trim());
            bool conditionResult = Evaluate_Condition(rawCondition);

            Conversation currentConversation = Dialog_System.instance.conversationManager.conversation;
            int currentProgress = Dialog_System.instance.conversationManager.Conversation_Progress;

            Encapsulated_Data ifData = Rip_Encapsulation_Data(currentConversation, currentProgress, false, parentStartingIndex: currentConversation.fileStartIndex);
            Encapsulated_Data elseData = new Encapsulated_Data();

            if(ifData.endingIndex + 1 < currentConversation.Count){

                string nextLine = currentConversation.Get_Lines()[ifData.endingIndex + 1].Trim();

                if(nextLine == elseID){

                    elseData = Rip_Encapsulation_Data(currentConversation, ifData.endingIndex + 1, false, parentStartingIndex: currentConversation.fileStartIndex);
                    ifData.endingIndex = elseData.endingIndex;

                }

            }

            currentConversation.Set_Progress(elseData.isNull ? ifData.endingIndex : elseData.endingIndex);

            Encapsulated_Data selData = conditionResult ? ifData : elseData;

            if(!selData.isNull && selData.lines.Count > 0){

                //Removing the header & encapsulator lines
                selData.startingIndex += 2;
                selData.endingIndex -= 1;

                Conversation newConversation = new Conversation(selData.lines, file: currentConversation.file, fileStartIndex: selData.startingIndex, fileEndIndex: selData.endingIndex);
                Dialog_System.instance.conversationManager.Enqueue_Priority(newConversation);

            }

            yield return null;

        }

        public bool Matches(Dialog_Line line){
            
            return line.rawData.Trim().StartsWith(keyword);

        }

        private string Extract_Condition(string line){

            int startIndex = line.IndexOf(containers[0]) + 1;
            int endIndex = line.IndexOf(containers[1]);

            return line.Substring(startIndex, endIndex - startIndex).Trim();

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
