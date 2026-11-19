<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Dialog{
    public class Dialog_Parser{

        private const string commandRegexPattern = @"[\w\[\]]*[^\s]\(";

        public static Dialog_Line Parse(string rawLine){

            //Debug.Log($"Parsing: '{rawLine}'");

            (string speaker, string dialog, string commands) = RipContent(rawLine);

            //Debug.Log($"Speaker is '{speaker}'\nDialog is '{dialog}'\nCommands are '{commands}'.");

            commands = Tag_Manager.Inject(commands);
            return new Dialog_Line(rawLine, speaker, dialog, commands);

        }

        private static (string, string, string) RipContent(string rawLine){

            string speaker = "", dialog = "", commands = "";

            int dialogStart = -1;
            int dialogEnd = -1;
            bool isEscaped = false;

            //Ripping dialog

            for(int i =0; i < rawLine.Length; i++){

                char current = rawLine[i];

                if(current == '\\'){

                    isEscaped = !isEscaped;

                }else if (current == '"' && !isEscaped){

                    if(dialogStart == -1){

                        dialogStart = i;

                    }else if(dialogEnd == -1){

                        dialogEnd = i;

                    }

                }else{

                    isEscaped = false;

                }

            }

            //Ripping commands

            //Finding command pattern
            Regex commandRegex = new Regex(commandRegexPattern);
            MatchCollection matches = commandRegex.Matches(rawLine);
            int commandStart = -1;

            foreach(Match match in matches){

                if(match.Index < dialogStart || match.Index > dialogEnd){

                    commandStart = match.Index;
                    break;

                }

            }

            if(commandStart != -1 && dialogStart == -1 && dialogEnd == -1){

                return("", "", rawLine.Trim());

            }

            //Figuring out whether there is a speaker, dialog or a long command
            if(dialogStart != -1 && dialogEnd != -1 && (commandStart == -1 || commandStart > dialogEnd)){

                //dialog
                speaker = rawLine.Substring(0, dialogStart).Trim();
                dialog = rawLine.Substring(dialogStart +1, dialogEnd - dialogStart -1).Replace("\\\"","\"");

                if(commandStart != -1){

                    commands = rawLine.Substring(commandStart).Trim();

                }

            }else if (commandStart != -1 && dialogStart > commandStart){

                commands = rawLine;

            }else{

                dialog = rawLine;

            }

            return(speaker, dialog, commands);

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Dialog{
    public class Dialog_Parser{

        private const string commandRegexPattern = @"[\w\[\]]*[^\s]\(";

        public static Dialog_Line Parse(string rawLine){

            //Debug.Log($"Parsing: '{rawLine}'");

            (string speaker, string dialog, string commands) = RipContent(rawLine);

            //Debug.Log($"Speaker is '{speaker}'\nDialog is '{dialog}'\nCommands are '{commands}'.");

            commands = Tag_Manager.Inject(commands);
            return new Dialog_Line(rawLine, speaker, dialog, commands);

        }

        private static (string, string, string) RipContent(string rawLine){

            string speaker = "", dialog = "", commands = "";

            int dialogStart = -1;
            int dialogEnd = -1;
            bool isEscaped = false;

            //Ripping dialog

            for(int i =0; i < rawLine.Length; i++){

                char current = rawLine[i];

                if(current == '\\'){

                    isEscaped = !isEscaped;

                }else if (current == '"' && !isEscaped){

                    if(dialogStart == -1){

                        dialogStart = i;

                    }else if(dialogEnd == -1){

                        dialogEnd = i;

                    }

                }else{

                    isEscaped = false;

                }

            }

            //Ripping commands

            //Finding command pattern
            Regex commandRegex = new Regex(commandRegexPattern);
            MatchCollection matches = commandRegex.Matches(rawLine);
            int commandStart = -1;

            foreach(Match match in matches){

                if(match.Index < dialogStart || match.Index > dialogEnd){

                    commandStart = match.Index;
                    break;

                }

            }

            if(commandStart != -1 && dialogStart == -1 && dialogEnd == -1){

                return("", "", rawLine.Trim());

            }

            //Figuring out whether there is a speaker, dialog or a long command
            if(dialogStart != -1 && dialogEnd != -1 && (commandStart == -1 || commandStart > dialogEnd)){

                //dialog
                speaker = rawLine.Substring(0, dialogStart).Trim();
                dialog = rawLine.Substring(dialogStart +1, dialogEnd - dialogStart -1).Replace("\\\"","\"");

                if(commandStart != -1){

                    commands = rawLine.Substring(commandStart).Trim();

                }

            }else if (commandStart != -1 && dialogStart > commandStart){

                commands = rawLine;

            }else{

                dialog = rawLine;

            }

            return(speaker, dialog, commands);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
