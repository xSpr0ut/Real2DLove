<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public class Command_Parameters{
        
        private const char parameterIdentifier = '#';
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private List<string> unlabeledParameters = new List<string>();

        public Command_Parameters(string[] parameterArray, int startingIndex = 0){

            for(int i = startingIndex; i < parameterArray.Length; i++){

                if(parameterArray[i].StartsWith(parameterIdentifier) && !float.TryParse(parameterArray[i], out _)){

                    string parameterName = parameterArray[i];
                    string parameterValue = "";

                    if(i + 1 < parameterArray.Length && !parameterArray[i + 1].StartsWith(parameterIdentifier)){

                        parameterValue = parameterArray[i + 1];
                        i++;

                    }

                    parameters.Add(parameterName, parameterValue);

                }else{

                    unlabeledParameters.Add(parameterArray[i]);

                }

            }

        }

        public bool Try_Get_Value<T>(string parameterName, out T value, T defaultValue = default(T)) => Try_Get_Value(new string[] { parameterName }, out value, defaultValue);
        public bool Try_Get_Value<T>(string[] parameterNames, out T value, T defaultValue = default(T)){

            foreach(string parameterName in parameterNames){

                if(parameters.TryGetValue(parameterName, out string parameterValue)){

                    if(Try_Cast_Parameter(parameterValue, out value)){

                        return true;

                    }

                }

            }

            //Nothing in identified parameters, so checking the unlabeled ones
            foreach(string parameterName in unlabeledParameters){

                if(Try_Cast_Parameter(parameterName, out value)){

                    unlabeledParameters.Remove(parameterName);
                    return true;

                }

            }

            value = defaultValue;
            return false;

        }

        public bool Try_Cast_Parameter<T>(string parameterValue, out T value){

            if(typeof(T) == typeof(bool)){

                if(bool.TryParse(parameterValue, out bool boolValue)){

                    value = (T)(object)boolValue;
                    return true;

                }

            }else if(typeof(T) == typeof(int)){

                if(int.TryParse(parameterValue, out int intValue)){

                    value = (T)(object)intValue;
                    return true;

                }

            }else if(typeof(T) == typeof(float)){

                if(float.TryParse(parameterValue, out float floatValue)){

                    value = (T)(object)floatValue;
                    return true;

                }

            }else if(typeof(T) == typeof(string)){

                value = (T)(object)parameterValue;
                return true;

            }

            value = default(T);
            return false;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public class Command_Parameters{
        
        private const char parameterIdentifier = '#';
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private List<string> unlabeledParameters = new List<string>();

        public Command_Parameters(string[] parameterArray, int startingIndex = 0){

            for(int i = startingIndex; i < parameterArray.Length; i++){

                if(parameterArray[i].StartsWith(parameterIdentifier) && !float.TryParse(parameterArray[i], out _)){

                    string parameterName = parameterArray[i];
                    string parameterValue = "";

                    if(i + 1 < parameterArray.Length && !parameterArray[i + 1].StartsWith(parameterIdentifier)){

                        parameterValue = parameterArray[i + 1];
                        i++;

                    }

                    parameters.Add(parameterName, parameterValue);

                }else{

                    unlabeledParameters.Add(parameterArray[i]);

                }

            }

        }

        public bool Try_Get_Value<T>(string parameterName, out T value, T defaultValue = default(T)) => Try_Get_Value(new string[] { parameterName }, out value, defaultValue);
        public bool Try_Get_Value<T>(string[] parameterNames, out T value, T defaultValue = default(T)){

            foreach(string parameterName in parameterNames){

                if(parameters.TryGetValue(parameterName, out string parameterValue)){

                    if(Try_Cast_Parameter(parameterValue, out value)){

                        return true;

                    }

                }

            }

            //Nothing in identified parameters, so checking the unlabeled ones
            foreach(string parameterName in unlabeledParameters){

                if(Try_Cast_Parameter(parameterName, out value)){

                    unlabeledParameters.Remove(parameterName);
                    return true;

                }

            }

            value = defaultValue;
            return false;

        }

        public bool Try_Cast_Parameter<T>(string parameterValue, out T value){

            if(typeof(T) == typeof(bool)){

                if(bool.TryParse(parameterValue, out bool boolValue)){

                    value = (T)(object)boolValue;
                    return true;

                }

            }else if(typeof(T) == typeof(int)){

                if(int.TryParse(parameterValue, out int intValue)){

                    value = (T)(object)intValue;
                    return true;

                }

            }else if(typeof(T) == typeof(float)){

                if(float.TryParse(parameterValue, out float floatValue)){

                    value = (T)(object)floatValue;
                    return true;

                }

            }else if(typeof(T) == typeof(string)){

                value = (T)(object)parameterValue;
                return true;

            }

            value = default(T);
            return false;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
