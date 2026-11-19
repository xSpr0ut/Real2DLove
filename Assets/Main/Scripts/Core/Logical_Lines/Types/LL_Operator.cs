<<<<<<< HEAD
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Expression;

namespace Dialog.Logical_Lines{

    public class LL_Operator : ILogical_Lines
    {
        public string keyword => throw new System.NotImplementedException();

        public IEnumerator Execute(Dialog_Line line){
            
            string trimmedLine = line.rawData.Trim();
            string[] parts = Regex.Split(trimmedLine, regexMath);

            if(parts.Length < 3){

                Debug.Log($"Ya can't do {trimmedLine}.");
                yield break;

            }

            string variable = parts[0].Trim().TrimStart(Variable_Store.variableID);
            string op = parts[1].Trim();
            string[] remainingParts = new string[parts.Length - 2];
            Array.Copy(parts, 2, remainingParts, 0, parts.Length -2);

            object value = Calculate_Value(remainingParts);

            if(value == null){

                yield break;

            }

            Process_Operator(variable, op, value);

        }

        private void Process_Operator(string variable, string op, object value){

            if(Variable_Store.Try_Get_Value(variable, out object currentValue)){

                Process_Operator_On_Variable(variable, op ,value, currentValue);

            }else if(op == "="){

                Variable_Store.Create_Variable(variable, value);

            }

        }

        private void Process_Operator_On_Variable(string variable, string op, object value, object currentValue){

            switch(op){

                case "=":

                    Variable_Store.Try_Set_Value(variable, value);
                    break;

                case"+=":

                    Variable_Store.Try_Set_Value(variable, ConcatenateOrAdd(value, currentValue));
                    break;

                case"-=":

                    Variable_Store.Try_Set_Value(variable, Convert.ToDouble(currentValue) - Convert.ToDouble(value));
                    break;

                case"*=":

                    Variable_Store.Try_Set_Value(variable, Convert.ToDouble(currentValue) * Convert.ToDouble(value));
                    break;

                case"/=":

                    Variable_Store.Try_Set_Value(variable, Convert.ToDouble(currentValue) / Convert.ToDouble(value));
                    break;

                default:

                    Debug.LogError($"{op} is wrong.");
                    break;

            }

        }

        private object ConcatenateOrAdd(object value, object currentValue){

            if(value is string){

                return currentValue.ToString() + value;

            }

            return Convert.ToDouble(currentValue) + Convert.ToDouble(value);

        }

        public bool Matches(Dialog_Line line){
            
            Match match = Regex.Match(line.rawData.Trim(), regexOperatorLine);
            return match.Success;

        }

    }

}
=======
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using static Dialog.Logical_Lines.Logical_Line_Utilities.Expression;

namespace Dialog.Logical_Lines{

    public class LL_Operator : ILogical_Lines
    {
        public string keyword => throw new System.NotImplementedException();

        public IEnumerator Execute(Dialog_Line line){
            
            string trimmedLine = line.rawData.Trim();
            string[] parts = Regex.Split(trimmedLine, regexMath);

            if(parts.Length < 3){

                Debug.Log($"Ya can't do {trimmedLine}.");
                yield break;

            }

            string variable = parts[0].Trim().TrimStart(Variable_Store.variableID);
            string op = parts[1].Trim();
            string[] remainingParts = new string[parts.Length - 2];
            Array.Copy(parts, 2, remainingParts, 0, parts.Length -2);

            object value = Calculate_Value(remainingParts);

            if(value == null){

                yield break;

            }

            Process_Operator(variable, op, value);

        }

        private void Process_Operator(string variable, string op, object value){

            if(Variable_Store.Try_Get_Value(variable, out object currentValue)){

                Process_Operator_On_Variable(variable, op ,value, currentValue);

            }else if(op == "="){

                Variable_Store.Create_Variable(variable, value);

            }

        }

        private void Process_Operator_On_Variable(string variable, string op, object value, object currentValue){

            switch(op){

                case "=":

                    Variable_Store.Try_Set_Value(variable, value);
                    break;

                case"+=":

                    Variable_Store.Try_Set_Value(variable, ConcatenateOrAdd(value, currentValue));
                    break;

                case"-=":

                    Variable_Store.Try_Set_Value(variable, Convert.ToDouble(currentValue) - Convert.ToDouble(value));
                    break;

                case"*=":

                    Variable_Store.Try_Set_Value(variable, Convert.ToDouble(currentValue) * Convert.ToDouble(value));
                    break;

                case"/=":

                    Variable_Store.Try_Set_Value(variable, Convert.ToDouble(currentValue) / Convert.ToDouble(value));
                    break;

                default:

                    Debug.LogError($"{op} is wrong.");
                    break;

            }

        }

        private object ConcatenateOrAdd(object value, object currentValue){

            if(value is string){

                return currentValue.ToString() + value;

            }

            return Convert.ToDouble(currentValue) + Convert.ToDouble(value);

        }

        public bool Matches(Dialog_Line line){
            
            Match match = Regex.Match(line.rawData.Trim(), regexOperatorLine);
            return match.Success;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
