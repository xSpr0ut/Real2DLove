<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dialog.Logical_Lines{

    public static class Logical_Line_Utilities{
        
        public static class Encapsulation{

            public struct Encapsulated_Data{

                public bool isNull => lines == null;
                public List<string> lines;
                public int startingIndex;
                public int endingIndex;

            }

            private const char encapsulationStart = '{';
            private const char encapsulationEnd = '}';

            public static bool Is_Encapsulation_Start(string line) => line.Trim().StartsWith(encapsulationStart);
            public static bool Is_Encapsulation_End(string line) => line.Trim().StartsWith(encapsulationEnd);

            public static Encapsulated_Data Rip_Encapsulation_Data(Conversation conversation, int startingIndex, bool ripHeaderAndEncapsulators = false, int parentStartingIndex = 0){

                int encapsulationDepth = 0;
                Encapsulated_Data data = new Encapsulated_Data{ lines = new List<string>(), startingIndex = (startingIndex + parentStartingIndex), endingIndex = 0 };

                for(int i = startingIndex; i < conversation.Count; i++){

                    string line = conversation.Get_Lines()[i];

                    if(ripHeaderAndEncapsulators || (encapsulationDepth > 0 && !Is_Encapsulation_End(line)))

                    data.lines.Add(line);

                    if(Is_Encapsulation_Start(line)){

                        encapsulationDepth++;
                        continue;

                    }

                    if(Is_Encapsulation_End(line)){

                        encapsulationDepth--;

                        if(encapsulationDepth == 0){

                            data.endingIndex = i + parentStartingIndex;
                            break;

                        }

                    }

                }

                return data;

            }

        }

        public static class Expression{

            public static HashSet<string> operators = new HashSet<string>(){"-", "-=", "+", "+=", "*", "*=", "/", "/=", "="};

            public static readonly string regexMath = @"([-+*/=]=?)";
            public static readonly string regexOperatorLine = @"^\$\w+\s*(=|\+=|-=|\*=|/=|)\s*";
            
            public static object Calculate_Value(string[] expressionParts){

                List<string> operandStrings = new List<string>();
                List<string> operatorStrings = new List<string>();
                List<object> operands = new List<object>();

                for(int i = 0; i <expressionParts.Length; i++){

                    string part = expressionParts[i].Trim();

                    if(part == string.Empty){

                        continue;

                    }

                    if(operators.Contains(part)){

                        operatorStrings.Add(part);

                    }else{

                        operandStrings.Add(part);

                    }

                }

                foreach(string operandString in operandStrings){

                    operands.Add(Extract_Value(operandString));

                }

                Calculate_Value_Division_And_Multiplication(operatorStrings, operands);
                Calculate_Value_Addition_And_Subtraction(operatorStrings, operands);

                return operands[0];

            }

            private static void Calculate_Value_Division_And_Multiplication(List<string> operatorStrings, List<object> operands){

                for(int i =0; i <operatorStrings.Count; i++){

                    string operatorString = operatorStrings[i];

                    if(operatorString == "*" || operatorString == "/"){

                        double leftOperand = Convert.ToDouble(operands[i]);
                        double rightOperand = Convert.ToDouble(operands[i + 1]);

                        if(operatorString == "*"){

                            operands[i] = leftOperand * rightOperand;

                        }else{

                            if(rightOperand == 0){

                                Debug.LogError("Everyone knows you cannot divide by zeor.");
                                return;

                            }

                            operands[i] = leftOperand / rightOperand;

                        }

                        operands.RemoveAt(i + 1);
                        operatorStrings.RemoveAt(i);
                        i--;

                    }

                }

            }

            private static void Calculate_Value_Addition_And_Subtraction(List<string> operatorStrings, List<object> operands){

                for(int i =0; i <operatorStrings.Count; i++){

                    string operatorString = operatorStrings[i];

                    if(operatorString == "+" || operatorString == "-"){

                        double leftOperand = Convert.ToDouble(operands[i]);
                        double rightOperand = Convert.ToDouble(operands[i + 1]);

                        if(operatorString == "+"){

                            operands[i] = leftOperand + rightOperand;

                        }else{

                            operands[i] = leftOperand - rightOperand;

                        }

                        operands.RemoveAt(i + 1);
                        operatorStrings.RemoveAt(i);
                        i--;

                    }

                }

            }

            private static object Extract_Value(string value){

                bool negate = false;

                if(value.StartsWith('!')){

                    negate = true;
                    value = value.Substring(0);

                }

                if(value.StartsWith(Variable_Store.variableID)){

                    string variableName = value.TrimStart(Variable_Store.variableID);

                    if(!Variable_Store.Has_Variable(variableName)){

                        Debug.LogError($"What am I supposed to do with {variableName}?");
                        return null;

                    }

                    Variable_Store.Try_Get_Value(variableName, out object val);

                    if(val is bool boolVal && negate){

                        return !boolVal;

                    }

                    return val;

                }else if(value.StartsWith('\"') && value.EndsWith('\"')){

                    value = Tag_Manager.Inject(value, true, true);
                    return value.Trim('"');

                }else{

                    if(int.TryParse(value, out int intValue)){

                        return intValue;

                    }else if(float.TryParse(value, out float floatValue)){

                        return floatValue;

                    }else if(bool.TryParse(value, out bool boolValue)){

                        return negate ? !boolValue : boolValue;

                    }else{

                        value = Tag_Manager.Inject(value, true, true);
                        return value;

                    }

                }

            }

        }

        public static class Condition{

            public static readonly string regexConditionalOperators = @"(==|!=|<=|>=|<|>|&&|\|\|)";

            public static bool Evaluate_Condition(string condition){

                condition = Tag_Manager.Inject(condition, injectTags: true, injectVariables: true);
                string[] parts = Regex.Split(condition, regexConditionalOperators)
                    .Select(p => p.Trim()).ToArray();

                for(int i = 0; i < parts.Length; i++){

                    if(parts[i].StartsWith("\"") && parts[i].EndsWith("\"")){

                        parts[i] = parts[i].Substring(1, parts[i].Length - 2);

                    }

                }

                if(parts.Length == 1){

                    if(bool.TryParse(parts[0], out bool result)){

                        return result;

                    }else{

                        Debug.LogError($"How am I supposed to know what condition: {condition} is?");
                        return false;

                    }

                }else if(parts.Length == 3){

                    return Evaluate_Expression(parts[0], parts[1], parts[2]);
                        
                }else{

                    Debug.LogError($"What the hell is this formatting? I don't know what {condition} means.");
                    return false;

                }

            } 

            private delegate bool Operator_Func<T>(T left, T right);

            private static Dictionary<string, Operator_Func<bool>> boolOperators = new Dictionary<string, Operator_Func<bool>>(){

                {"&&", (left, right) => left && right},
                {"||", (left, right) => left || right},
                {"==", (left, right) => left == right},
                {"!=", (left, right) => left != right}

            };

            private static Dictionary<string, Operator_Func<float>> floatOperators = new Dictionary<string, Operator_Func<float>>(){

                {"==", (left, right) => left == right},
                {"!=", (left, right) => left != right},
                {">", (left, right) => left > right},
                {">=", (left, right) => left >= right},
                {"<", (left, right) => left < right},
                {"<=", (left, right) => left <= right}

            };

            private static Dictionary<string, Operator_Func<int>> intOperators = new Dictionary<string, Operator_Func<int>>(){

                {"==", (left, right) => left == right},
                {"!=", (left, right) => left != right},
                {">", (left, right) => left > right},
                {">=", (left, right) => left >= right},
                {"<", (left, right) => left < right},
                {"<=", (left, right) => left <= right}

            };

            private static bool Evaluate_Expression(string left, string op, string right){

                if(bool.TryParse(left, out bool leftBool) && bool.TryParse(right, out bool rightBool)){

                    return boolOperators[op](leftBool, rightBool);

                }

                if(float.TryParse(left, out float leftFloat) && float.TryParse(right, out float rightFloat)){

                    return floatOperators[op](leftFloat, rightFloat);

                }

                if(int.TryParse(left, out int leftInt) && int.TryParse(right, out int rightInt)){

                    return intOperators[op](leftInt, rightInt);

                }

                switch(op){

                    case "==": return left == right;
                    case "!=": return left != right;
                    default: throw new InvalidOperationException($"Nope, can't do {op}.");

                }

            }

        }

    }

}
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dialog.Logical_Lines{

    public static class Logical_Line_Utilities{
        
        public static class Encapsulation{

            public struct Encapsulated_Data{

                public bool isNull => lines == null;
                public List<string> lines;
                public int startingIndex;
                public int endingIndex;

            }

            private const char encapsulationStart = '{';
            private const char encapsulationEnd = '}';

            public static bool Is_Encapsulation_Start(string line) => line.Trim().StartsWith(encapsulationStart);
            public static bool Is_Encapsulation_End(string line) => line.Trim().StartsWith(encapsulationEnd);

            public static Encapsulated_Data Rip_Encapsulation_Data(Conversation conversation, int startingIndex, bool ripHeaderAndEncapsulators = false, int parentStartingIndex = 0){

                int encapsulationDepth = 0;
                Encapsulated_Data data = new Encapsulated_Data{ lines = new List<string>(), startingIndex = (startingIndex + parentStartingIndex), endingIndex = 0 };

                for(int i = startingIndex; i < conversation.Count; i++){

                    string line = conversation.Get_Lines()[i];

                    if(ripHeaderAndEncapsulators || (encapsulationDepth > 0 && !Is_Encapsulation_End(line)))

                    data.lines.Add(line);

                    if(Is_Encapsulation_Start(line)){

                        encapsulationDepth++;
                        continue;

                    }

                    if(Is_Encapsulation_End(line)){

                        encapsulationDepth--;

                        if(encapsulationDepth == 0){

                            data.endingIndex = i + parentStartingIndex;
                            break;

                        }

                    }

                }

                return data;

            }

        }

        public static class Expression{

            public static HashSet<string> operators = new HashSet<string>(){"-", "-=", "+", "+=", "*", "*=", "/", "/=", "="};

            public static readonly string regexMath = @"([-+*/=]=?)";
            public static readonly string regexOperatorLine = @"^\$\w+\s*(=|\+=|-=|\*=|/=|)\s*";
            
            public static object Calculate_Value(string[] expressionParts){

                List<string> operandStrings = new List<string>();
                List<string> operatorStrings = new List<string>();
                List<object> operands = new List<object>();

                for(int i = 0; i <expressionParts.Length; i++){

                    string part = expressionParts[i].Trim();

                    if(part == string.Empty){

                        continue;

                    }

                    if(operators.Contains(part)){

                        operatorStrings.Add(part);

                    }else{

                        operandStrings.Add(part);

                    }

                }

                foreach(string operandString in operandStrings){

                    operands.Add(Extract_Value(operandString));

                }

                Calculate_Value_Division_And_Multiplication(operatorStrings, operands);
                Calculate_Value_Addition_And_Subtraction(operatorStrings, operands);

                return operands[0];

            }

            private static void Calculate_Value_Division_And_Multiplication(List<string> operatorStrings, List<object> operands){

                for(int i =0; i <operatorStrings.Count; i++){

                    string operatorString = operatorStrings[i];

                    if(operatorString == "*" || operatorString == "/"){

                        double leftOperand = Convert.ToDouble(operands[i]);
                        double rightOperand = Convert.ToDouble(operands[i + 1]);

                        if(operatorString == "*"){

                            operands[i] = leftOperand * rightOperand;

                        }else{

                            if(rightOperand == 0){

                                Debug.LogError("Everyone knows you cannot divide by zeor.");
                                return;

                            }

                            operands[i] = leftOperand / rightOperand;

                        }

                        operands.RemoveAt(i + 1);
                        operatorStrings.RemoveAt(i);
                        i--;

                    }

                }

            }

            private static void Calculate_Value_Addition_And_Subtraction(List<string> operatorStrings, List<object> operands){

                for(int i =0; i <operatorStrings.Count; i++){

                    string operatorString = operatorStrings[i];

                    if(operatorString == "+" || operatorString == "-"){

                        double leftOperand = Convert.ToDouble(operands[i]);
                        double rightOperand = Convert.ToDouble(operands[i + 1]);

                        if(operatorString == "+"){

                            operands[i] = leftOperand + rightOperand;

                        }else{

                            operands[i] = leftOperand - rightOperand;

                        }

                        operands.RemoveAt(i + 1);
                        operatorStrings.RemoveAt(i);
                        i--;

                    }

                }

            }

            private static object Extract_Value(string value){

                bool negate = false;

                if(value.StartsWith('!')){

                    negate = true;
                    value = value.Substring(0);

                }

                if(value.StartsWith(Variable_Store.variableID)){

                    string variableName = value.TrimStart(Variable_Store.variableID);

                    if(!Variable_Store.Has_Variable(variableName)){

                        Debug.LogError($"What am I supposed to do with {variableName}?");
                        return null;

                    }

                    Variable_Store.Try_Get_Value(variableName, out object val);

                    if(val is bool boolVal && negate){

                        return !boolVal;

                    }

                    return val;

                }else if(value.StartsWith('\"') && value.EndsWith('\"')){

                    value = Tag_Manager.Inject(value, true, true);
                    return value.Trim('"');

                }else{

                    if(int.TryParse(value, out int intValue)){

                        return intValue;

                    }else if(float.TryParse(value, out float floatValue)){

                        return floatValue;

                    }else if(bool.TryParse(value, out bool boolValue)){

                        return negate ? !boolValue : boolValue;

                    }else{

                        value = Tag_Manager.Inject(value, true, true);
                        return value;

                    }

                }

            }

        }

        public static class Condition{

            public static readonly string regexConditionalOperators = @"(==|!=|<=|>=|<|>|&&|\|\|)";

            public static bool Evaluate_Condition(string condition){

                condition = Tag_Manager.Inject(condition, injectTags: true, injectVariables: true);
                string[] parts = Regex.Split(condition, regexConditionalOperators)
                    .Select(p => p.Trim()).ToArray();

                for(int i = 0; i < parts.Length; i++){

                    if(parts[i].StartsWith("\"") && parts[i].EndsWith("\"")){

                        parts[i] = parts[i].Substring(1, parts[i].Length - 2);

                    }

                }

                if(parts.Length == 1){

                    if(bool.TryParse(parts[0], out bool result)){

                        return result;

                    }else{

                        Debug.LogError($"How am I supposed to know what condition: {condition} is?");
                        return false;

                    }

                }else if(parts.Length == 3){

                    return Evaluate_Expression(parts[0], parts[1], parts[2]);
                        
                }else{

                    Debug.LogError($"What the hell is this formatting? I don't know what {condition} means.");
                    return false;

                }

            } 

            private delegate bool Operator_Func<T>(T left, T right);

            private static Dictionary<string, Operator_Func<bool>> boolOperators = new Dictionary<string, Operator_Func<bool>>(){

                {"&&", (left, right) => left && right},
                {"||", (left, right) => left || right},
                {"==", (left, right) => left == right},
                {"!=", (left, right) => left != right}

            };

            private static Dictionary<string, Operator_Func<float>> floatOperators = new Dictionary<string, Operator_Func<float>>(){

                {"==", (left, right) => left == right},
                {"!=", (left, right) => left != right},
                {">", (left, right) => left > right},
                {">=", (left, right) => left >= right},
                {"<", (left, right) => left < right},
                {"<=", (left, right) => left <= right}

            };

            private static Dictionary<string, Operator_Func<int>> intOperators = new Dictionary<string, Operator_Func<int>>(){

                {"==", (left, right) => left == right},
                {"!=", (left, right) => left != right},
                {">", (left, right) => left > right},
                {">=", (left, right) => left >= right},
                {"<", (left, right) => left < right},
                {"<=", (left, right) => left <= right}

            };

            private static bool Evaluate_Expression(string left, string op, string right){

                if(bool.TryParse(left, out bool leftBool) && bool.TryParse(right, out bool rightBool)){

                    return boolOperators[op](leftBool, rightBool);

                }

                if(float.TryParse(left, out float leftFloat) && float.TryParse(right, out float rightFloat)){

                    return floatOperators[op](leftFloat, rightFloat);

                }

                if(int.TryParse(left, out int leftInt) && int.TryParse(right, out int rightInt)){

                    return intOperators[op](leftInt, rightInt);

                }

                switch(op){

                    case "==": return left == right;
                    case "!=": return left != right;
                    default: throw new InvalidOperationException($"Nope, can't do {op}.");

                }

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
