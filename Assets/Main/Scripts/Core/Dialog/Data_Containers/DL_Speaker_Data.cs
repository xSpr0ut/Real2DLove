<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dialog{

    public class DL_Speaker_Data{
        
        public string rawData { get; private set; } = string.Empty;
        public string name, castName;
        public string displayName => isCastingName ? castName : name;
        public Vector2 castPosition;
        public List<(int layer, string expression)> CastExpressions {get; set;}

        public bool isCastingName => castName != string.Empty;
        public bool isCastingPosition = false;
        public bool isCastingExpressions => CastExpressions.Count > 0;

        public bool makeCharacterEnter = false;
        private const string nameCastID = " as ";
        private const string positionCastID = " at ";
        private const string expressionCastID = " [";
        private const char axisDelimiter = ':';
        private const char expressionLayerJoiner = ',';
        private const char expressionLayerDelimiter = ':';
        private const string enterKeyword = "enter ";
        private const string switchBoxID = "[switch]";
        private const string setBoxDefaultID = "[default]";
        private const string setBoxKeepID = "[keep]";
        private const string makeNewBoxID = "[new]";

        private string Process_Keywords(string rawSpeaker){

            if(rawSpeaker.StartsWith(enterKeyword)){

                rawSpeaker = rawSpeaker.Substring(enterKeyword.Length);
                makeCharacterEnter = true;

            }

            if(rawSpeaker.ToLower().StartsWith(makeNewBoxID)){

                rawSpeaker = rawSpeaker.Substring(makeNewBoxID.Length);
                Dialog_System.instance.makeNewDialogBox = true;

            }

            if(rawSpeaker.ToLower().StartsWith(setBoxDefaultID)){

                rawSpeaker = rawSpeaker.Substring(setBoxDefaultID.Length);

                if(rawSpeaker.ToLower().StartsWith(setBoxKeepID)){

                    rawSpeaker = rawSpeaker.Substring(setBoxKeepID.Length);
                    Dialog_System.instance.Switch_Textbox("default", true);

                }else{

                    Dialog_System.instance.Switch_Textbox("default");

                }

            }

            if(rawSpeaker.ToLower().StartsWith(switchBoxID)){

                rawSpeaker = rawSpeaker.Substring(switchBoxID.Length);

                if(rawSpeaker.ToLower().StartsWith(setBoxKeepID)){

                    rawSpeaker = rawSpeaker.Substring(setBoxKeepID.Length);

                    string pattern = @$"{nameCastID}|{positionCastID}|{expressionCastID.Insert(expressionCastID.Length -1, @"\")}";
                    MatchCollection matches = Regex.Matches(rawSpeaker, pattern);

                    if(matches.Count == 0){

                        name = rawSpeaker;

                    }else{

                        int index = matches[0].Index;
                        name = rawSpeaker.Substring(0, index);

                    }

                    Dialog_System.instance.Switch_Textbox(name, true);

                }else{

                    string pattern = @$"{nameCastID}|{positionCastID}|{expressionCastID.Insert(expressionCastID.Length -1, @"\")}";
                    MatchCollection matches = Regex.Matches(rawSpeaker, pattern);

                    if(matches.Count == 0){

                        name = rawSpeaker;

                    }else{

                        int index = matches[0].Index;
                        name = rawSpeaker.Substring(0, index);

                    }

                    Dialog_System.instance.Switch_Textbox(name);

                }

            }

            return rawSpeaker;

        }

        public DL_Speaker_Data(string rawSpeaker){

            rawData = rawSpeaker;
            rawSpeaker = Process_Keywords(rawSpeaker);

            string pattern = @$"{nameCastID}|{positionCastID}|{expressionCastID.Insert(expressionCastID.Length -1, @"\")}";
            MatchCollection matches = Regex.Matches(rawSpeaker, pattern);
            castName = "";
            castPosition = Vector2.zero;
            CastExpressions = new List<(int layer, string expression)>();

            //If there aren't matches, the entire line is the speaker name
            if(matches.Count == 0){

                name = rawSpeaker;
                return;

            }

            //Otherwise, rip out the speaker name
            int index = matches[0].Index;
            name = rawSpeaker.Substring(0, index);

            for(int i = 0; i < matches.Count; i++){

                Match match = matches[i];
                int startIndex = 0, endIndex = 0;

                if(match.Value == nameCastID){

                    startIndex = match.Index + nameCastID.Length;
                    endIndex = (i < matches.Count -1) ? matches[i +1].Index : rawSpeaker.Length;
                    castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                }else if(match.Value == positionCastID){

                    isCastingPosition = true;

                    startIndex = match.Index + positionCastID.Length;
                    endIndex = (i < matches.Count -1) ? matches[i +1].Index : rawSpeaker.Length;
                    string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                    string[] axis = castPos.Split(axisDelimiter, System.StringSplitOptions.RemoveEmptyEntries);
                    float.TryParse(axis[0], out castPosition.x);

                    if(axis.Length > 1){

                        float.TryParse(axis[1], out castPosition.y);

                    }

                }else if(match.Value == expressionCastID){

                    startIndex = match.Index + expressionCastID.Length;
                    endIndex = (i < matches.Count -1) ? matches[i +1].Index : rawSpeaker.Length;
                    string castExp = rawSpeaker.Substring(startIndex, endIndex - (startIndex +1));

                    CastExpressions = castExp.Split(expressionLayerJoiner)
                    .Select(x => {

                        var parts = x.Trim().Split(expressionLayerDelimiter);

                        if(parts.Length == 2){

                            return(int.Parse(parts[0]), parts[1]);

                        }else{

                            return (0, parts[0]);

                        }

                    }).ToList();

                }

            }

        }

    }

}
=======
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dialog{

    public class DL_Speaker_Data{
        
        public string rawData { get; private set; } = string.Empty;
        public string name, castName;
        public string displayName => isCastingName ? castName : name;
        public Vector2 castPosition;
        public List<(int layer, string expression)> CastExpressions {get; set;}

        public bool isCastingName => castName != string.Empty;
        public bool isCastingPosition = false;
        public bool isCastingExpressions => CastExpressions.Count > 0;

        public bool makeCharacterEnter = false;
        private const string nameCastID = " as ";
        private const string positionCastID = " at ";
        private const string expressionCastID = " [";
        private const char axisDelimiter = ':';
        private const char expressionLayerJoiner = ',';
        private const char expressionLayerDelimiter = ':';
        private const string enterKeyword = "enter ";
        private const string switchBoxID = "[switch]";
        private const string setBoxDefaultID = "[default]";
        private const string setBoxKeepID = "[keep]";
        private const string makeNewBoxID = "[new]";

        private string Process_Keywords(string rawSpeaker){

            if(rawSpeaker.StartsWith(enterKeyword)){

                rawSpeaker = rawSpeaker.Substring(enterKeyword.Length);
                makeCharacterEnter = true;

            }

            if(rawSpeaker.ToLower().StartsWith(makeNewBoxID)){

                rawSpeaker = rawSpeaker.Substring(makeNewBoxID.Length);
                Dialog_System.instance.makeNewDialogBox = true;

            }

            if(rawSpeaker.ToLower().StartsWith(setBoxDefaultID)){

                rawSpeaker = rawSpeaker.Substring(setBoxDefaultID.Length);

                if(rawSpeaker.ToLower().StartsWith(setBoxKeepID)){

                    rawSpeaker = rawSpeaker.Substring(setBoxKeepID.Length);
                    Dialog_System.instance.Switch_Textbox("default", true);

                }else{

                    Dialog_System.instance.Switch_Textbox("default");

                }

            }

            if(rawSpeaker.ToLower().StartsWith(switchBoxID)){

                rawSpeaker = rawSpeaker.Substring(switchBoxID.Length);

                if(rawSpeaker.ToLower().StartsWith(setBoxKeepID)){

                    rawSpeaker = rawSpeaker.Substring(setBoxKeepID.Length);

                    string pattern = @$"{nameCastID}|{positionCastID}|{expressionCastID.Insert(expressionCastID.Length -1, @"\")}";
                    MatchCollection matches = Regex.Matches(rawSpeaker, pattern);

                    if(matches.Count == 0){

                        name = rawSpeaker;

                    }else{

                        int index = matches[0].Index;
                        name = rawSpeaker.Substring(0, index);

                    }

                    Dialog_System.instance.Switch_Textbox(name, true);

                }else{

                    string pattern = @$"{nameCastID}|{positionCastID}|{expressionCastID.Insert(expressionCastID.Length -1, @"\")}";
                    MatchCollection matches = Regex.Matches(rawSpeaker, pattern);

                    if(matches.Count == 0){

                        name = rawSpeaker;

                    }else{

                        int index = matches[0].Index;
                        name = rawSpeaker.Substring(0, index);

                    }

                    Dialog_System.instance.Switch_Textbox(name);

                }

            }

            return rawSpeaker;

        }

        public DL_Speaker_Data(string rawSpeaker){

            rawData = rawSpeaker;
            rawSpeaker = Process_Keywords(rawSpeaker);

            string pattern = @$"{nameCastID}|{positionCastID}|{expressionCastID.Insert(expressionCastID.Length -1, @"\")}";
            MatchCollection matches = Regex.Matches(rawSpeaker, pattern);
            castName = "";
            castPosition = Vector2.zero;
            CastExpressions = new List<(int layer, string expression)>();

            //If there aren't matches, the entire line is the speaker name
            if(matches.Count == 0){

                name = rawSpeaker;
                return;

            }

            //Otherwise, rip out the speaker name
            int index = matches[0].Index;
            name = rawSpeaker.Substring(0, index);

            for(int i = 0; i < matches.Count; i++){

                Match match = matches[i];
                int startIndex = 0, endIndex = 0;

                if(match.Value == nameCastID){

                    startIndex = match.Index + nameCastID.Length;
                    endIndex = (i < matches.Count -1) ? matches[i +1].Index : rawSpeaker.Length;
                    castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                }else if(match.Value == positionCastID){

                    isCastingPosition = true;

                    startIndex = match.Index + positionCastID.Length;
                    endIndex = (i < matches.Count -1) ? matches[i +1].Index : rawSpeaker.Length;
                    string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                    string[] axis = castPos.Split(axisDelimiter, System.StringSplitOptions.RemoveEmptyEntries);
                    float.TryParse(axis[0], out castPosition.x);

                    if(axis.Length > 1){

                        float.TryParse(axis[1], out castPosition.y);

                    }

                }else if(match.Value == expressionCastID){

                    startIndex = match.Index + expressionCastID.Length;
                    endIndex = (i < matches.Count -1) ? matches[i +1].Index : rawSpeaker.Length;
                    string castExp = rawSpeaker.Substring(startIndex, endIndex - (startIndex +1));

                    CastExpressions = castExp.Split(expressionLayerJoiner)
                    .Select(x => {

                        var parts = x.Trim().Split(expressionLayerDelimiter);

                        if(parts.Length == 2){

                            return(int.Parse(parts[0]), parts[1]);

                        }else{

                            return (0, parts[0]);

                        }

                    }).ToList();

                }

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
