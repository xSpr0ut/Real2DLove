<<<<<<< HEAD
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Dialog.Logical_Lines.Input;
using Dialog.Logical_Lines;
using System.Linq;
using VN;

public class Tag_Manager{

    private static readonly Dictionary<string, Func<string>> tags = new Dictionary<string, Func<string>>(){

        //Add more when have more variables
        {"<playerName>", () => VN_Save_Game.activeFile.playerName},
        {"<time>", () => DateTime.Now.ToString("hh:mm tt")},
        {"<input>", () => Input_Panel.instance.lastInput},
        {"<otherVariableCanAddMore>", () => "42"}

    };
    private static readonly Regex tagRegex = new Regex("<\\w+>");

    public static string Inject(string text, bool injectTags = true, bool injectVariables = true){

        if(injectTags){

            text = Inject_Tags(text);

        }

        if(injectVariables){

            text = Inject_Variables(text);

        }

        return text;

    }

    private static string Inject_Tags(string value){

        if(tagRegex.IsMatch(value)){

            foreach(Match match in tagRegex.Matches(value)){

                if(tags.TryGetValue(match.Value, out var tagValueRequest)){

                    value = value.Replace(match.Value, tagValueRequest());

                }

            }

        }

        return value;

    }

    private static string Inject_Variables(string value){

        var matches = Regex.Matches(value, Variable_Store.regexVariableIDs);
        var matchesList = matches.Cast<Match>().ToList();

        for(int i = matchesList.Count - 1; i >= 0; i--){

            var match = matchesList[i];
            string variableName = match.Value.TrimStart(Variable_Store.variableID, '!');
            bool negate = match.Value.StartsWith('!');

            bool endsWithIllegalCharacter = variableName.EndsWith(Variable_Store.databaseVariableRelationalID);

            if(endsWithIllegalCharacter){

                variableName = variableName.Substring(0, variableName.Length - 1);

            }

            if(!Variable_Store.Try_Get_Value(variableName, out object variableValue)){

                UnityEngine.Debug.LogError($"I can't find {variableName}.");
                continue;

            }

            if(negate && variableValue is bool){

                variableValue = !(bool)variableValue;

            }

            int lengthToBeRemoved = match.Index + match.Length > value.Length ? value.Length - match.Index : match.Length;

            if(endsWithIllegalCharacter){

                lengthToBeRemoved -= 1;

            }

            value = value.Remove(match.Index, lengthToBeRemoved);
            value = value.Insert(match.Index, variableValue.ToString());

        }

        return value;

    }

}
=======
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Dialog.Logical_Lines.Input;
using Dialog.Logical_Lines;
using System.Linq;
using VN;

public class Tag_Manager{

    private static readonly Dictionary<string, Func<string>> tags = new Dictionary<string, Func<string>>(){

        //Add more when have more variables
        {"<playerName>", () => VN_Save_Game.activeFile.playerName},
        {"<time>", () => DateTime.Now.ToString("hh:mm tt")},
        {"<input>", () => Input_Panel.instance.lastInput},
        {"<otherVariableCanAddMore>", () => "42"}

    };
    private static readonly Regex tagRegex = new Regex("<\\w+>");

    public static string Inject(string text, bool injectTags = true, bool injectVariables = true){

        if(injectTags){

            text = Inject_Tags(text);

        }

        if(injectVariables){

            text = Inject_Variables(text);

        }

        return text;

    }

    private static string Inject_Tags(string value){

        if(tagRegex.IsMatch(value)){

            foreach(Match match in tagRegex.Matches(value)){

                if(tags.TryGetValue(match.Value, out var tagValueRequest)){

                    value = value.Replace(match.Value, tagValueRequest());

                }

            }

        }

        return value;

    }

    private static string Inject_Variables(string value){

        var matches = Regex.Matches(value, Variable_Store.regexVariableIDs);
        var matchesList = matches.Cast<Match>().ToList();

        for(int i = matchesList.Count - 1; i >= 0; i--){

            var match = matchesList[i];
            string variableName = match.Value.TrimStart(Variable_Store.variableID, '!');
            bool negate = match.Value.StartsWith('!');

            bool endsWithIllegalCharacter = variableName.EndsWith(Variable_Store.databaseVariableRelationalID);

            if(endsWithIllegalCharacter){

                variableName = variableName.Substring(0, variableName.Length - 1);

            }

            if(!Variable_Store.Try_Get_Value(variableName, out object variableValue)){

                UnityEngine.Debug.LogError($"I can't find {variableName}.");
                continue;

            }

            if(negate && variableValue is bool){

                variableValue = !(bool)variableValue;

            }

            int lengthToBeRemoved = match.Index + match.Length > value.Length ? value.Length - match.Index : match.Length;

            if(endsWithIllegalCharacter){

                lengthToBeRemoved -= 1;

            }

            value = value.Remove(match.Index, lengthToBeRemoved);
            value = value.Insert(match.Index, variableValue.ToString());

        }

        return value;

    }

}
>>>>>>> c8d4f422 (Whole Template)
