<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dialog;
using Dialog.Logical_Lines;
using History;
using Menu.Config;
using UnityEngine;

namespace VN{

    [System.Serializable]

    public class VN_Save_Game{
        
        public static VN_Save_Game activeFile = null;

        public const string fileType = ".vns";
        public const string screenshotFileType = ".jpg";
        public const bool encryptFiles = true;

        private const float screenshotDownscale = 0.25f;

        public string filePath => $"{File_Paths.gameSaves}{slotNumber}{fileType}";
        public string screenshotPath => $"{File_Paths.gameSaves}{slotNumber}{screenshotFileType}";

        //Add variables here
        public string playerName = "name";
        public int num = 1;

        public string progress = "Beginning";
        public int slotNumber = 1;
        public string quickSaveFilePath = null;

        public bool newGame = true;
        public string[] activeConversation;
        public History_State activeState;
        public History_State[] historyLogs;
        public VN_Variable_Data[] variables;

        public string timestamp;
        public string title;
        
        public void Quick_Save(){
           
            slotNumber = 0;
            activeState = History_State.Capture();
            historyLogs = History_Manager.instance.history.ToArray();
            activeConversation = Get_Conversation_Data();
            variables = Get_Variable_Data();

            timestamp = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            title = "Quick Save";

            Screenshot_Manager.Capture_Screenshot(VN_Manager.instance.mainCamera, Screen.width, Screen.height, screenshotDownscale, screenshotPath);

            string saveJSON = JsonUtility.ToJson(this);
            File_Manager.Save(filePath, saveJSON, encryptFiles);
            
        }
        
        public void Quick_Load(){
            
            VN_Save_Game save = File_Manager.Load<VN_Save_Game>(filePath, encryptFiles);

            activeFile = save;
            save.Reapply();
            
        }

        public static VN_Save_Game Load(string filePath, bool activateOnLoad = false, bool popDetails = false){

            VN_Save_Game save = File_Manager.Load<VN_Save_Game>(filePath, encryptFiles);

            if(!popDetails){

                activeFile = save;

            }

            if(activateOnLoad){

                save.Reapply();

            }

            return save;

        }

        public void Save(bool autosave = false){

            newGame = false;

            activeState = History_State.Capture();
            historyLogs = History_Manager.instance.history.ToArray();
            activeConversation = Get_Conversation_Data();
            variables = Get_Variable_Data();

            timestamp = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            title = progress;

            if(autosave){

                title = "Autosave";
                
            }

            Screenshot_Manager.Capture_Screenshot(VN_Manager.instance.mainCamera, Screen.width, Screen.height, screenshotDownscale, screenshotPath);

            string saveJSON = JsonUtility.ToJson(this);
            File_Manager.Save(filePath, saveJSON, encryptFiles);

        }

        public void Autosave(){
            
            int slotNumber = 0;

            for(int i = 0; i < 99; i++){

                VN_Configuration.activeConfig.Save();

                string filePath = $"{File_Paths.gameSaves}{i}{fileType}";

                if(!File.Exists(filePath)){

                    this.slotNumber = slotNumber;

                    Save(autosave: true);
                    return;

                }

                slotNumber++;

                if(slotNumber > 99){

                    return;

                }

            }
            
        }

        public void Reapply(){

            if(activeState != null){

                activeState.Load();

            }

            History_Manager.instance.history = historyLogs.ToList();
            History_Manager.instance.logManager.Clear();
            History_Manager.instance.logManager.Rebuild();

            Set_Variable_Data();
            Set_Conversation_Data();

            Dialog_System.instance.prompt.Hide();

        }

        private string[] Get_Conversation_Data(){

            List<string> retData = new List<string>();

            var conversations = Dialog_System.instance.conversationManager.Get_Conversation_Queue();

            for(int i = 0; i < conversations.Length; i++){

                var conversation = conversations[i];
                string data = "";

                if(conversation.file != string.Empty){

                    var compressedData = new VN_Conversation_Data_Compressed();
                    compressedData.fileName = conversation.file;
                    compressedData.progress = conversation.Get_Progress();
                    compressedData.startIndex = conversation.fileStartIndex;
                    compressedData.endIndex = conversation.fileEndIndex;
                    data = JsonUtility.ToJson(compressedData);

                }else{

                    var fullData = new VN_Conversation_Data();
                    fullData.conversation = conversation.Get_Lines();
                    fullData.progress = conversation.Get_Progress();
                    data = JsonUtility.ToJson(fullData);

                }

                retData.Add(data);

            }

            return retData.ToArray();

        }
        
        private void Set_Conversation_Data(){

            for(int i =0; i < activeConversation.Length; i++){

                try{

                    string data = activeConversation[i];
                    Conversation conversation = null;

                    var fullData = JsonUtility.FromJson<VN_Conversation_Data>(data);

                    if(fullData != null && fullData.conversation != null && fullData.conversation.Count > 0){

                        conversation = new Conversation(fullData.conversation, fullData.progress);

                    }else{

                        var compressedData = JsonUtility.FromJson<VN_Conversation_Data_Compressed >(data);

                        if(compressedData != null && compressedData.fileName != string.Empty){

                            TextAsset file = Resources.Load<TextAsset>(compressedData.fileName);
                            int count = compressedData.endIndex - compressedData.startIndex;
                            
                            List<string> lines = File_Manager.Read_Text_Asset(file).Skip(compressedData.startIndex).Take(count + 1).ToList();

                            conversation = new Conversation(lines, compressedData.progress, compressedData.fileName, compressedData.startIndex, compressedData.endIndex);

                        }else{

                            Debug.LogError($"This convo format's all wring. Can't load {data}.");

                        }

                    }

                    if(conversation != null && conversation.Get_Lines().Count > 0){

                        if(i == 0){

                            Dialog_System.instance.conversationManager.Start_Conversation(conversation);

                        }else{

                            Dialog_System.instance.conversationManager.Enqueue(conversation);

                        }

                    }
                    
                }catch(System.Exception e){

                    Debug.LogError($"Fuck, {e} encountered when extracting convo data.");
                    continue;

                }

            }

        }

        private VN_Variable_Data[] Get_Variable_Data(){

            List<VN_Variable_Data> retData = new List<VN_Variable_Data>();

            foreach(var database in Variable_Store.databases.Values){

                foreach(var variable in database.variables){

                    VN_Variable_Data variableData = new VN_Variable_Data();
                    variableData.name = $"{database.name}.{variable.Key}";
                    string val = $"{variable.Value.Get()}";
                    variableData.value = val;
                    variableData.type = val == string.Empty ? "System.String" : variable.Value.Get().GetType().ToString();
                    retData.Add(variableData);

                }

            }

            return retData.ToArray();

        }

        private void Set_Variable_Data(){

            foreach(var variable in variables){

                string val = variable.value;

                switch(variable.type){

                    case "System.Boolean":
                        
                        if(bool.TryParse(val, out bool boolVal)){

                            Variable_Store.Try_Set_Value(variable.name, boolVal);
                            continue;

                        }

                        break;

                    case "System.Int32":
                        if(int.TryParse(val, out int intVal)){

                            Variable_Store.Try_Set_Value(variable.name, intVal);
                            continue;

                        }

                        break;

                    case "System.Single":
                    case "System.Double":

                        if(float.TryParse(val, out float floatVal)){

                            Variable_Store.Try_Set_Value(variable.name, floatVal);
                            continue;

                        }

                        break;

                    case "System.String":

                        Variable_Store.Try_Set_Value(variable.name, val);
                        continue;

                }

                Debug.LogError($"I have no idea what kind of variable {variable.name} is. It's {variable.type}, which is unknown.");

            }

        }

    }

}
=======
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dialog;
using Dialog.Logical_Lines;
using History;
using Menu.Config;
using UnityEngine;

namespace VN{

    [System.Serializable]

    public class VN_Save_Game{
        
        public static VN_Save_Game activeFile = null;

        public const string fileType = ".vns";
        public const string screenshotFileType = ".jpg";
        public const bool encryptFiles = true;

        private const float screenshotDownscale = 0.25f;

        public string filePath => $"{File_Paths.gameSaves}{slotNumber}{fileType}";
        public string screenshotPath => $"{File_Paths.gameSaves}{slotNumber}{screenshotFileType}";

        //Add variables here
        public string playerName = "name";
        public int num = 1;

        public string progress = "Beginning";
        public int slotNumber = 1;
        public string quickSaveFilePath = null;

        public bool newGame = true;
        public string[] activeConversation;
        public History_State activeState;
        public History_State[] historyLogs;
        public VN_Variable_Data[] variables;

        public string timestamp;
        public string title;
        
        public void Quick_Save(){
           
            slotNumber = 0;
            activeState = History_State.Capture();
            historyLogs = History_Manager.instance.history.ToArray();
            activeConversation = Get_Conversation_Data();
            variables = Get_Variable_Data();

            timestamp = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            title = "Quick Save";

            Screenshot_Manager.Capture_Screenshot(VN_Manager.instance.mainCamera, Screen.width, Screen.height, screenshotDownscale, screenshotPath);

            string saveJSON = JsonUtility.ToJson(this);
            File_Manager.Save(filePath, saveJSON, encryptFiles);
            
        }
        
        public void Quick_Load(){
            
            VN_Save_Game save = File_Manager.Load<VN_Save_Game>(filePath, encryptFiles);

            activeFile = save;
            save.Reapply();
            
        }

        public static VN_Save_Game Load(string filePath, bool activateOnLoad = false, bool popDetails = false){

            VN_Save_Game save = File_Manager.Load<VN_Save_Game>(filePath, encryptFiles);

            if(!popDetails){

                activeFile = save;

            }

            if(activateOnLoad){

                save.Reapply();

            }

            return save;

        }

        public void Save(bool autosave = false){

            newGame = false;

            activeState = History_State.Capture();
            historyLogs = History_Manager.instance.history.ToArray();
            activeConversation = Get_Conversation_Data();
            variables = Get_Variable_Data();

            timestamp = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            title = progress;

            if(autosave){

                title = "Autosave";
                
            }

            Screenshot_Manager.Capture_Screenshot(VN_Manager.instance.mainCamera, Screen.width, Screen.height, screenshotDownscale, screenshotPath);

            string saveJSON = JsonUtility.ToJson(this);
            File_Manager.Save(filePath, saveJSON, encryptFiles);

        }

        public void Autosave(){
            
            int slotNumber = 0;

            for(int i = 0; i < 99; i++){

                VN_Configuration.activeConfig.Save();

                string filePath = $"{File_Paths.gameSaves}{i}{fileType}";

                if(!File.Exists(filePath)){

                    this.slotNumber = slotNumber;

                    Save(autosave: true);
                    return;

                }

                slotNumber++;

                if(slotNumber > 99){

                    return;

                }

            }
            
        }

        public void Reapply(){

            if(activeState != null){

                activeState.Load();

            }

            History_Manager.instance.history = historyLogs.ToList();
            History_Manager.instance.logManager.Clear();
            History_Manager.instance.logManager.Rebuild();

            Set_Variable_Data();
            Set_Conversation_Data();

            Dialog_System.instance.prompt.Hide();

        }

        private string[] Get_Conversation_Data(){

            List<string> retData = new List<string>();

            var conversations = Dialog_System.instance.conversationManager.Get_Conversation_Queue();

            for(int i = 0; i < conversations.Length; i++){

                var conversation = conversations[i];
                string data = "";

                if(conversation.file != string.Empty){

                    var compressedData = new VN_Conversation_Data_Compressed();
                    compressedData.fileName = conversation.file;
                    compressedData.progress = conversation.Get_Progress();
                    compressedData.startIndex = conversation.fileStartIndex;
                    compressedData.endIndex = conversation.fileEndIndex;
                    data = JsonUtility.ToJson(compressedData);

                }else{

                    var fullData = new VN_Conversation_Data();
                    fullData.conversation = conversation.Get_Lines();
                    fullData.progress = conversation.Get_Progress();
                    data = JsonUtility.ToJson(fullData);

                }

                retData.Add(data);

            }

            return retData.ToArray();

        }
        
        private void Set_Conversation_Data(){

            for(int i =0; i < activeConversation.Length; i++){

                try{

                    string data = activeConversation[i];
                    Conversation conversation = null;

                    var fullData = JsonUtility.FromJson<VN_Conversation_Data>(data);

                    if(fullData != null && fullData.conversation != null && fullData.conversation.Count > 0){

                        conversation = new Conversation(fullData.conversation, fullData.progress);

                    }else{

                        var compressedData = JsonUtility.FromJson<VN_Conversation_Data_Compressed >(data);

                        if(compressedData != null && compressedData.fileName != string.Empty){

                            TextAsset file = Resources.Load<TextAsset>(compressedData.fileName);
                            int count = compressedData.endIndex - compressedData.startIndex;
                            
                            List<string> lines = File_Manager.Read_Text_Asset(file).Skip(compressedData.startIndex).Take(count + 1).ToList();

                            conversation = new Conversation(lines, compressedData.progress, compressedData.fileName, compressedData.startIndex, compressedData.endIndex);

                        }else{

                            Debug.LogError($"This convo format's all wring. Can't load {data}.");

                        }

                    }

                    if(conversation != null && conversation.Get_Lines().Count > 0){

                        if(i == 0){

                            Dialog_System.instance.conversationManager.Start_Conversation(conversation);

                        }else{

                            Dialog_System.instance.conversationManager.Enqueue(conversation);

                        }

                    }
                    
                }catch(System.Exception e){

                    Debug.LogError($"Fuck, {e} encountered when extracting convo data.");
                    continue;

                }

            }

        }

        private VN_Variable_Data[] Get_Variable_Data(){

            List<VN_Variable_Data> retData = new List<VN_Variable_Data>();

            foreach(var database in Variable_Store.databases.Values){

                foreach(var variable in database.variables){

                    VN_Variable_Data variableData = new VN_Variable_Data();
                    variableData.name = $"{database.name}.{variable.Key}";
                    string val = $"{variable.Value.Get()}";
                    variableData.value = val;
                    variableData.type = val == string.Empty ? "System.String" : variable.Value.Get().GetType().ToString();
                    retData.Add(variableData);

                }

            }

            return retData.ToArray();

        }

        private void Set_Variable_Data(){

            foreach(var variable in variables){

                string val = variable.value;

                switch(variable.type){

                    case "System.Boolean":
                        
                        if(bool.TryParse(val, out bool boolVal)){

                            Variable_Store.Try_Set_Value(variable.name, boolVal);
                            continue;

                        }

                        break;

                    case "System.Int32":
                        if(int.TryParse(val, out int intVal)){

                            Variable_Store.Try_Set_Value(variable.name, intVal);
                            continue;

                        }

                        break;

                    case "System.Single":
                    case "System.Double":

                        if(float.TryParse(val, out float floatVal)){

                            Variable_Store.Try_Set_Value(variable.name, floatVal);
                            continue;

                        }

                        break;

                    case "System.String":

                        Variable_Store.Try_Set_Value(variable.name, val);
                        continue;

                }

                Debug.LogError($"I have no idea what kind of variable {variable.name} is. It's {variable.type}, which is unknown.");

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
