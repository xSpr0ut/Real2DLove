<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog{
    
    public class Dialog_Line{

        public string rawData { get; private set; } = string.Empty;
        public DL_Speaker_Data speaker;
        public DL_Dialog_Data dialog;
        public DL_Command_Data commands;

        public bool hasDialog => dialog != null;
        public bool hasCommands => commands != null;
        public bool hasSpeaker => speaker != null;

        public Dialog_Line(string rawLine, string speaker, string dialog, string commands){

            rawData = rawLine;
            this.speaker = (string.IsNullOrWhiteSpace(speaker) ? null : new DL_Speaker_Data(speaker));
            this.dialog = (string.IsNullOrWhiteSpace(dialog) ? null : new DL_Dialog_Data(dialog));
            this.commands = (string.IsNullOrWhiteSpace(commands) ? null : new DL_Command_Data(commands));

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog{
    
    public class Dialog_Line{

        public string rawData { get; private set; } = string.Empty;
        public DL_Speaker_Data speaker;
        public DL_Dialog_Data dialog;
        public DL_Command_Data commands;

        public bool hasDialog => dialog != null;
        public bool hasCommands => commands != null;
        public bool hasSpeaker => speaker != null;

        public Dialog_Line(string rawLine, string speaker, string dialog, string commands){

            rawData = rawLine;
            this.speaker = (string.IsNullOrWhiteSpace(speaker) ? null : new DL_Speaker_Data(speaker));
            this.dialog = (string.IsNullOrWhiteSpace(dialog) ? null : new DL_Dialog_Data(dialog));
            this.commands = (string.IsNullOrWhiteSpace(commands) ? null : new DL_Command_Data(commands));

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
