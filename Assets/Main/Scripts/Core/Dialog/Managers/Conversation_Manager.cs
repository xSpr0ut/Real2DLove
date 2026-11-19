<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;
using Characters;
using Dialog.Logical_Lines;

namespace Dialog{

    public class Conversation_Manager{

        private Dialog_System dialogSystem => Dialog_System.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;
        public bool isOnLogicalLine { get; private set; } = false;
        public Text_System textSystem = null;
        private bool playerInput = false;

        private Logical_Line_Manager logicalLineManager;

        public Conversation conversation => conversationQueue.isEmpty() ? null : conversationQueue.top;
        public int Conversation_Progress => conversationQueue.isEmpty() ? -1 : conversationQueue.top.Get_Progress();
        private Conversation_Queue conversationQueue;

        public bool allowInput = true;

        public Conversation_Manager(Text_System textSystem){

            this.textSystem = dialogSystem.textSystem;
            dialogSystem.next_On_Player_Input += Next_On_Player_Input;

            logicalLineManager = new Logical_Line_Manager();

            conversationQueue = new Conversation_Queue();

        }

        public Conversation[] Get_Conversation_Queue() => conversationQueue.Get_Readonly();

        public void Enqueue(Conversation conversation) => conversationQueue.Enqueue(conversation);
        public void Enqueue_Priority(Conversation conversation) => conversationQueue.Enqueue_Priority(conversation);

        private void Next_On_Player_Input(){

            if(allowInput){

                playerInput = true;

            }

        }
        
        public Coroutine Start_Conversation(Conversation conversation){

            Stop_Conversation();
            conversationQueue.Clear();

            Enqueue(conversation);
            process = dialogSystem.StartCoroutine(Running_Conversation());

            return process;

        }

        public void Stop_Conversation(){

            if(!isRunning){

                return;

            }

            dialogSystem.StopCoroutine(process);
            process = null;

        }

        IEnumerator Running_Conversation(){

            while(!conversationQueue.isEmpty()){

                Conversation currentConversation = conversation;

                if(currentConversation.Has_Reached_End()){

                    conversationQueue.Dequeue();
                    continue;

                }

                string rawline = currentConversation.Current_Line();

                //Skip blank lines
                if(string.IsNullOrWhiteSpace(rawline)){

                    Try_Advance_Conversation(currentConversation);
                    continue;

                }
                
                Dialog_Line line = Dialog_Parser.Parse(rawLine: rawline);

                if(logicalLineManager.Try_Get_Logic(line, out Coroutine logic)){

                    isOnLogicalLine = true;
                    yield return logic;

                }else{

                    //Show dialog
                    if(line.hasDialog){

                        yield return Line_Run_Dialog(line);

                    }

                    //Run commands
                    if(line.hasCommands){

                        yield return Line_Run_Commands(line);

                    }

                    //User input
                    if(line.hasDialog){
                    
                        yield return Wait_For_Player_Input();
                        Command_Manager.instance.Stop_All_Processes();

                        dialogSystem.On_System_Clear();

                    }

                }

                Try_Advance_Conversation(currentConversation);
                isOnLogicalLine = false;

            }

            process = null;

        }

        private void Try_Advance_Conversation(Conversation conversation){

            conversation.Increment_Progress();

            if(conversation != conversationQueue.top){

                return;

            }

            if(conversation.Has_Reached_End()){

                conversationQueue.Dequeue();

            }

        }

        IEnumerator Line_Run_Dialog(Dialog_Line line){

            //show & hide speakers
            if(line.hasSpeaker){

                Handle_Speaker_Logic(line.speaker);

            }

            if(!dialogSystem.dialogContainer.isVisible){

                dialogSystem.dialogContainer.Show();

            }

            //Building dialog
            yield return Build_Line_Segments(line.dialog);

        }

        private void Handle_Speaker_Logic(DL_Speaker_Data speaker){

            bool characterMustBeCReated = speaker.makeCharacterEnter || speaker.isCastingPosition || speaker.isCastingExpressions;

            Character character = Character_Manager.instance.Get_Character(speaker.name, createIfNonexistent: characterMustBeCReated);

            if(speaker.makeCharacterEnter && (!character.isVisible && !character.isRevealing)){

                character.Show();

            }

            if(dialogSystem.textBox == dialogSystem.dialogContainer.textDefault){

                //Adding character name to the UI
                dialogSystem.Show_Speaker_Name(Tag_Manager.Inject(speaker.displayName));

            }else{

                dialogSystem.Hide_Speaker_Name();

            }

            //Applying configurations to the screen
            Dialog_System.instance.Apply_Speaker_Data_To_Dialog_Container(speaker.name);

            //Casting positiom
            if(speaker.isCastingPosition){

                character.Set_Position(speaker.castPosition);

            }

            //Casting expressions
            if(speaker.isCastingExpressions){

                foreach(var ce in speaker.CastExpressions){

                    character.On_Recieve_Casting_Expression(ce.layer, ce.expression);

                }

            }

        }

        IEnumerator Line_Run_Commands(Dialog_Line line){

            //Debug.Log(line.commands);
            List<DL_Command_Data.Command> commands = line.commands.commands;

            foreach(DL_Command_Data.Command command in commands){

                if(command.waitForComplete || command.name == "wait"){

                    Coroutine_Wrapper coroutineWrapper = Command_Manager.instance.Execute(command.name, command.arguments);
                    
                    while(!coroutineWrapper.isDone){

                        if(playerInput){

                            Command_Manager.instance.Stop_Process();
                            playerInput = false;

                        }

                        yield return null;

                    }

                }else{

                    Command_Manager.instance.Execute(command.name, command.arguments);

                }

            }
            
            yield return null;

        }

        IEnumerator Build_Line_Segments(DL_Dialog_Data line){

            for(int i = 0; i < line.segments.Count; i++){

                DL_Dialog_Data.Dialog_Segment segment = line.segments[i];
                yield return Wait_For_Segment_Signal(segment);
                yield return Build_Dialog(segment.dialog, segment.appendText);

            }

        }

        public bool isWaitingOnAutoTimer { get; private set; } = false;

        IEnumerator Wait_For_Segment_Signal(DL_Dialog_Data.Dialog_Segment segment){

            switch(segment.startSignal){

                case DL_Dialog_Data.Dialog_Segment.StartSignal.C:

                    yield return Wait_For_Player_Input();
                    dialogSystem.On_System_Clear();
                    break;

                case DL_Dialog_Data.Dialog_Segment.StartSignal.A:

                    yield return Wait_For_Player_Input();
                    break;
                
                case DL_Dialog_Data.Dialog_Segment.StartSignal.WC:

                    isWaitingOnAutoTimer = true;
                    yield return new WaitForSeconds(segment.signalDelay);
                    isWaitingOnAutoTimer = false;
                    dialogSystem.On_System_Clear();
                    break;

                case DL_Dialog_Data.Dialog_Segment.StartSignal.WA:

                    isWaitingOnAutoTimer = true;
                    yield return new WaitForSeconds(segment.signalDelay);
                    isWaitingOnAutoTimer = false;
                    break;

                default:

                    break;

            }

        }

        IEnumerator Build_Dialog(string dialog, bool append = false){

            dialog = Tag_Manager.Inject(dialog);

            //Build or append dialog
            if(!append){
                
                textSystem.Build(dialog);

            }else{

                textSystem.Append(dialog);

            }

            //IF player makes an action while dialog is building/appending
            while(textSystem.isBuilding){

                if(playerInput){

                    if(!textSystem.speedUp){

                        textSystem.speedUp = true;

                    }else{

                        textSystem.Force_Complete();

                    }

                    playerInput = false;

                }

                yield return null;

            }

        }

        IEnumerator Wait_For_Player_Input(){

            dialogSystem.prompt.Show();

            while(!playerInput){

                yield return null;

            }

            dialogSystem.prompt.Hide();

            playerInput = false;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;
using Characters;
using Dialog.Logical_Lines;

namespace Dialog{

    public class Conversation_Manager{

        private Dialog_System dialogSystem => Dialog_System.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;
        public bool isOnLogicalLine { get; private set; } = false;
        public Text_System textSystem = null;
        private bool playerInput = false;

        private Logical_Line_Manager logicalLineManager;

        public Conversation conversation => conversationQueue.isEmpty() ? null : conversationQueue.top;
        public int Conversation_Progress => conversationQueue.isEmpty() ? -1 : conversationQueue.top.Get_Progress();
        private Conversation_Queue conversationQueue;

        public bool allowInput = true;

        public Conversation_Manager(Text_System textSystem){

            this.textSystem = dialogSystem.textSystem;
            dialogSystem.next_On_Player_Input += Next_On_Player_Input;

            logicalLineManager = new Logical_Line_Manager();

            conversationQueue = new Conversation_Queue();

        }

        public Conversation[] Get_Conversation_Queue() => conversationQueue.Get_Readonly();

        public void Enqueue(Conversation conversation) => conversationQueue.Enqueue(conversation);
        public void Enqueue_Priority(Conversation conversation) => conversationQueue.Enqueue_Priority(conversation);

        private void Next_On_Player_Input(){

            if(allowInput){

                playerInput = true;

            }

        }
        
        public Coroutine Start_Conversation(Conversation conversation){

            Stop_Conversation();
            conversationQueue.Clear();

            Enqueue(conversation);
            process = dialogSystem.StartCoroutine(Running_Conversation());

            return process;

        }

        public void Stop_Conversation(){

            if(!isRunning){

                return;

            }

            dialogSystem.StopCoroutine(process);
            process = null;

        }

        IEnumerator Running_Conversation(){

            while(!conversationQueue.isEmpty()){

                Conversation currentConversation = conversation;

                if(currentConversation.Has_Reached_End()){

                    conversationQueue.Dequeue();
                    continue;

                }

                string rawline = currentConversation.Current_Line();

                //Skip blank lines
                if(string.IsNullOrWhiteSpace(rawline)){

                    Try_Advance_Conversation(currentConversation);
                    continue;

                }
                
                Dialog_Line line = Dialog_Parser.Parse(rawLine: rawline);

                if(logicalLineManager.Try_Get_Logic(line, out Coroutine logic)){

                    isOnLogicalLine = true;
                    yield return logic;

                }else{

                    //Show dialog
                    if(line.hasDialog){

                        yield return Line_Run_Dialog(line);

                    }

                    //Run commands
                    if(line.hasCommands){

                        yield return Line_Run_Commands(line);

                    }

                    //User input
                    if(line.hasDialog){
                    
                        yield return Wait_For_Player_Input();
                        Command_Manager.instance.Stop_All_Processes();

                        dialogSystem.On_System_Clear();

                    }

                }

                Try_Advance_Conversation(currentConversation);
                isOnLogicalLine = false;

            }

            process = null;

        }

        private void Try_Advance_Conversation(Conversation conversation){

            conversation.Increment_Progress();

            if(conversation != conversationQueue.top){

                return;

            }

            if(conversation.Has_Reached_End()){

                conversationQueue.Dequeue();

            }

        }

        IEnumerator Line_Run_Dialog(Dialog_Line line){

            //show & hide speakers
            if(line.hasSpeaker){

                Handle_Speaker_Logic(line.speaker);

            }

            if(!dialogSystem.dialogContainer.isVisible){

                dialogSystem.dialogContainer.Show();

            }

            //Building dialog
            yield return Build_Line_Segments(line.dialog);

        }

        private void Handle_Speaker_Logic(DL_Speaker_Data speaker){

            bool characterMustBeCReated = speaker.makeCharacterEnter || speaker.isCastingPosition || speaker.isCastingExpressions;

            Character character = Character_Manager.instance.Get_Character(speaker.name, createIfNonexistent: characterMustBeCReated);

            if(speaker.makeCharacterEnter && (!character.isVisible && !character.isRevealing)){

                character.Show();

            }

            if(dialogSystem.textBox == dialogSystem.dialogContainer.textDefault){

                //Adding character name to the UI
                dialogSystem.Show_Speaker_Name(Tag_Manager.Inject(speaker.displayName));

            }else{

                dialogSystem.Hide_Speaker_Name();

            }

            //Applying configurations to the screen
            Dialog_System.instance.Apply_Speaker_Data_To_Dialog_Container(speaker.name);

            //Casting positiom
            if(speaker.isCastingPosition){

                character.Set_Position(speaker.castPosition);

            }

            //Casting expressions
            if(speaker.isCastingExpressions){

                foreach(var ce in speaker.CastExpressions){

                    character.On_Recieve_Casting_Expression(ce.layer, ce.expression);

                }

            }

        }

        IEnumerator Line_Run_Commands(Dialog_Line line){

            //Debug.Log(line.commands);
            List<DL_Command_Data.Command> commands = line.commands.commands;

            foreach(DL_Command_Data.Command command in commands){

                if(command.waitForComplete || command.name == "wait"){

                    Coroutine_Wrapper coroutineWrapper = Command_Manager.instance.Execute(command.name, command.arguments);
                    
                    while(!coroutineWrapper.isDone){

                        if(playerInput){

                            Command_Manager.instance.Stop_Process();
                            playerInput = false;

                        }

                        yield return null;

                    }

                }else{

                    Command_Manager.instance.Execute(command.name, command.arguments);

                }

            }
            
            yield return null;

        }

        IEnumerator Build_Line_Segments(DL_Dialog_Data line){

            for(int i = 0; i < line.segments.Count; i++){

                DL_Dialog_Data.Dialog_Segment segment = line.segments[i];
                yield return Wait_For_Segment_Signal(segment);
                yield return Build_Dialog(segment.dialog, segment.appendText);

            }

        }

        public bool isWaitingOnAutoTimer { get; private set; } = false;

        IEnumerator Wait_For_Segment_Signal(DL_Dialog_Data.Dialog_Segment segment){

            switch(segment.startSignal){

                case DL_Dialog_Data.Dialog_Segment.StartSignal.C:

                    yield return Wait_For_Player_Input();
                    dialogSystem.On_System_Clear();
                    break;

                case DL_Dialog_Data.Dialog_Segment.StartSignal.A:

                    yield return Wait_For_Player_Input();
                    break;
                
                case DL_Dialog_Data.Dialog_Segment.StartSignal.WC:

                    isWaitingOnAutoTimer = true;
                    yield return new WaitForSeconds(segment.signalDelay);
                    isWaitingOnAutoTimer = false;
                    dialogSystem.On_System_Clear();
                    break;

                case DL_Dialog_Data.Dialog_Segment.StartSignal.WA:

                    isWaitingOnAutoTimer = true;
                    yield return new WaitForSeconds(segment.signalDelay);
                    isWaitingOnAutoTimer = false;
                    break;

                default:

                    break;

            }

        }

        IEnumerator Build_Dialog(string dialog, bool append = false){

            dialog = Tag_Manager.Inject(dialog);

            //Build or append dialog
            if(!append){
                
                textSystem.Build(dialog);

            }else{

                textSystem.Append(dialog);

            }

            //IF player makes an action while dialog is building/appending
            while(textSystem.isBuilding){

                if(playerInput){

                    if(!textSystem.speedUp){

                        textSystem.speedUp = true;

                    }else{

                        textSystem.Force_Complete();

                    }

                    playerInput = false;

                }

                yield return null;

            }

        }

        IEnumerator Wait_For_Player_Input(){

            dialogSystem.prompt.Show();

            while(!playerInput){

                yield return null;

            }

            dialogSystem.prompt.Hide();

            playerInput = false;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
