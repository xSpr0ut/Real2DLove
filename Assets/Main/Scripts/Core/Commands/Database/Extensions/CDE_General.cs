<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using Dialog;
using Menu.Config;using UnityEngine;
using VN;

namespace Commands{
    public class CDE_General : Command_Database_Extension{

        private static string[] parameterImmediate = new string[] { "#i", "#immediate" };
        private static string[] parameterSpeed = new string[] { "#spd", "#speed" };
        private static string[] parameterBuildMethod = new string[] { "#bm", "#buildmethod" };
        private static string[] parameterFilepath = new string[] { "#f", "#file", "#filepath" };
        private static string[] parameterEnqueue = new string[] { "#e", "#enqueue" };
        private static string[] parameterEnqueuePriority = new string[] { "#ep", "#enqueuepriority" };
        
        private Text_System textSystem => Dialog_System.instance.conversationManager.textSystem;

        new public static void Extend(Command_Database database){

            database.Add_Command("wait", new Func<string, IEnumerator>(Wait));

            database.Add_Command("set_DCP_sprite", new Action<string>(Set_DCP_Sprite));
            database.Add_Command("show_dialog_box", new Func<string[], IEnumerator>(Show_Dialog_Box));
            database.Add_Command("hide_dialog_box", new Func<string[], IEnumerator>(Hide_Dialog_Box));

            database.Add_Command("show_dialog_system", new Func<string[], IEnumerator>(Show_Dialog_System));
            database.Add_Command("hide_dialog_system", new Func<string[], IEnumerator>(Hide_Dialog_System));

            database.Add_Command("destroy_box_gameobject", new Action<string>(Destroy_Box_Gameobject));
            //To scale for different res, on 4k dialog root must scale x & y to 2
            //Characters like Oiloaso also need to scale

            //Only works if keeps same textbox, no switching
            database.Add_Command("change_build_method", new Action<string[]>(Change_Build_Method));
            database.Add_Command("reset_text_speed", new Action(Reset_Text_Speed));

            database.Add_Command("load", new Action<string[]>(Load_New_Dialog_File));

            database.Add_Command("roll", new Action<string>(Pick_Random_Number));

        }

        private static IEnumerator Wait(string data){

            if(float.TryParse(data, out float time)){

                yield return new WaitForSeconds(time);

            }

        }

        private static void Set_DCP_Sprite(string data){

            Dialog_Continue_Prompt.instance.Set_Sprite(data);

        }

        private static IEnumerator Show_Dialog_Box(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.dialogContainer.Show(speed, immediate);

        }

        private static IEnumerator Hide_Dialog_Box(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.dialogContainer.Hide(speed, immediate);
            
        }

        private static IEnumerator Show_Dialog_System(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.Show(speed, immediate);

        }

        private static IEnumerator Hide_Dialog_System(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.Hide(speed, immediate);
            
        }

        private static void Destroy_Box_Gameobject(string data){

            UnityEngine.Object.Destroy(Dialog_System.instance.dialogBoxObject);
            
        }

        private static void Change_Build_Method(string[] data){

            string buildMethod;
            float speed;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterBuildMethod, out buildMethod, defaultValue: "gradual");
            
            if(buildMethod == "instant".ToLower()){
            
                Dialog_System.instance.textSystem.buildMethod = Text_System.BuildMethod.instant;
                return;

            }else if(buildMethod == "fade".ToLower()){
            
                Dialog_System.instance.textSystem.buildMethod = Text_System.BuildMethod.fade;
                
            }else{
            
                Dialog_System.instance.textSystem.buildMethod = Text_System.BuildMethod.gradual;

            }

            Dialog_System.instance.textSystem.speed = speed;

        }

        private static void Load_New_Dialog_File(string[] data){

            string fileName = string.Empty;
            bool enqueue = false;
            bool enqueuePriority = false;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterFilepath, out fileName);
            parameters.Try_Get_Value(parameterEnqueue, out enqueue, defaultValue: false);
            parameters.Try_Get_Value(parameterEnqueuePriority, out enqueuePriority, defaultValue: false);

            string filePath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesDialogFiles, fileName);
            TextAsset file = Resources.Load<TextAsset>(filePath);

            if(file == null){

                Debug.LogWarning($"{filePath} not found, what the fuck am I supposed to do with this? Is it really in {File_Paths.resourcesDialogFiles}?");
                return;

            }

            List<string> lines = File_Manager.Read_Text_Asset(file, includeBlankLines: true);
            Conversation newConversation = new Conversation(lines);
            
            if(enqueue){

                Dialog_System.instance.conversationManager.Enqueue(newConversation);

            }else if(enqueuePriority){
                
                Dialog_System.instance.conversationManager.Enqueue_Priority(newConversation);
                
            }else{

                Dialog_System.instance.conversationManager.Start_Conversation(newConversation);

            }

        }

        private static void Reset_Text_Speed(){

            float speed = VN_Configuration.activeConfig.dialogTextSpeed;

            if(Dialog_System.instance != null){

                Dialog_System.instance.conversationManager.textSystem.speed = speed;

            }

        }

        private static void Pick_Random_Number(string data){

            int max = 1;
            int num = 1;

            if(int.TryParse(data, out max)){

                num = UnityEngine.Random.Range(1, max + 1);

            }

            VN_Save_Game.activeFile.num = num;

        }

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using Dialog;
using Menu.Config;using UnityEngine;
using VN;

namespace Commands{
    public class CDE_General : Command_Database_Extension{

        private static string[] parameterImmediate = new string[] { "#i", "#immediate" };
        private static string[] parameterSpeed = new string[] { "#spd", "#speed" };
        private static string[] parameterBuildMethod = new string[] { "#bm", "#buildmethod" };
        private static string[] parameterFilepath = new string[] { "#f", "#file", "#filepath" };
        private static string[] parameterEnqueue = new string[] { "#e", "#enqueue" };
        private static string[] parameterEnqueuePriority = new string[] { "#ep", "#enqueuepriority" };
        
        private Text_System textSystem => Dialog_System.instance.conversationManager.textSystem;

        new public static void Extend(Command_Database database){

            database.Add_Command("wait", new Func<string, IEnumerator>(Wait));

            database.Add_Command("set_DCP_sprite", new Action<string>(Set_DCP_Sprite));
            database.Add_Command("show_dialog_box", new Func<string[], IEnumerator>(Show_Dialog_Box));
            database.Add_Command("hide_dialog_box", new Func<string[], IEnumerator>(Hide_Dialog_Box));

            database.Add_Command("show_dialog_system", new Func<string[], IEnumerator>(Show_Dialog_System));
            database.Add_Command("hide_dialog_system", new Func<string[], IEnumerator>(Hide_Dialog_System));

            database.Add_Command("destroy_box_gameobject", new Action<string>(Destroy_Box_Gameobject));
            //To scale for different res, on 4k dialog root must scale x & y to 2
            //Characters like Oiloaso also need to scale

            //Only works if keeps same textbox, no switching
            database.Add_Command("change_build_method", new Action<string[]>(Change_Build_Method));
            database.Add_Command("reset_text_speed", new Action(Reset_Text_Speed));

            database.Add_Command("load", new Action<string[]>(Load_New_Dialog_File));

            database.Add_Command("roll", new Action<string>(Pick_Random_Number));

        }

        private static IEnumerator Wait(string data){

            if(float.TryParse(data, out float time)){

                yield return new WaitForSeconds(time);

            }

        }

        private static void Set_DCP_Sprite(string data){

            Dialog_Continue_Prompt.instance.Set_Sprite(data);

        }

        private static IEnumerator Show_Dialog_Box(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.dialogContainer.Show(speed, immediate);

        }

        private static IEnumerator Hide_Dialog_Box(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.dialogContainer.Hide(speed, immediate);
            
        }

        private static IEnumerator Show_Dialog_System(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.Show(speed, immediate);

        }

        private static IEnumerator Hide_Dialog_System(string[] data){

            float speed;
            bool immediate;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            yield return Dialog_System.instance.Hide(speed, immediate);
            
        }

        private static void Destroy_Box_Gameobject(string data){

            UnityEngine.Object.Destroy(Dialog_System.instance.dialogBoxObject);
            
        }

        private static void Change_Build_Method(string[] data){

            string buildMethod;
            float speed;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterBuildMethod, out buildMethod, defaultValue: "gradual");
            
            if(buildMethod == "instant".ToLower()){
            
                Dialog_System.instance.textSystem.buildMethod = Text_System.BuildMethod.instant;
                return;

            }else if(buildMethod == "fade".ToLower()){
            
                Dialog_System.instance.textSystem.buildMethod = Text_System.BuildMethod.fade;
                
            }else{
            
                Dialog_System.instance.textSystem.buildMethod = Text_System.BuildMethod.gradual;

            }

            Dialog_System.instance.textSystem.speed = speed;

        }

        private static void Load_New_Dialog_File(string[] data){

            string fileName = string.Empty;
            bool enqueue = false;
            bool enqueuePriority = false;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterFilepath, out fileName);
            parameters.Try_Get_Value(parameterEnqueue, out enqueue, defaultValue: false);
            parameters.Try_Get_Value(parameterEnqueuePriority, out enqueuePriority, defaultValue: false);

            string filePath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesDialogFiles, fileName);
            TextAsset file = Resources.Load<TextAsset>(filePath);

            if(file == null){

                Debug.LogWarning($"{filePath} not found, what the fuck am I supposed to do with this? Is it really in {File_Paths.resourcesDialogFiles}?");
                return;

            }

            List<string> lines = File_Manager.Read_Text_Asset(file, includeBlankLines: true);
            Conversation newConversation = new Conversation(lines);
            
            if(enqueue){

                Dialog_System.instance.conversationManager.Enqueue(newConversation);

            }else if(enqueuePriority){
                
                Dialog_System.instance.conversationManager.Enqueue_Priority(newConversation);
                
            }else{

                Dialog_System.instance.conversationManager.Start_Conversation(newConversation);

            }

        }

        private static void Reset_Text_Speed(){

            float speed = VN_Configuration.activeConfig.dialogTextSpeed;

            if(Dialog_System.instance != null){

                Dialog_System.instance.conversationManager.textSystem.speed = speed;

            }

        }

        private static void Pick_Random_Number(string data){

            int max = 1;
            int num = 1;

            if(int.TryParse(data, out max)){

                num = UnityEngine.Random.Range(1, max + 1);

            }

            VN_Save_Game.activeFile.num = num;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
