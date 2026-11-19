<<<<<<< HEAD
using Dialog;
using TMPro;
using UnityEngine;

namespace History{

    [System.Serializable]

    public class Dialog_Data{
        
        public string currentDialog = "";
        public string currentSpeaker = "";

        public string dialogFont;
        public Color dialogColor;
        public float dialogScale;

        public string speakerFont;
        public Color speakerNameColor;
        public float speakerScale;
        
        public string boxName;
        public Text_System.BuildMethod buildMethod;
        public string dcp;
        public float textSpeed;

        public float nameBoxSize;

        public static Dialog_Data Capture(){

            Dialog_Data data = new Dialog_Data();

            var dialogSystem = Dialog_System.instance;
            var dialogText = dialogSystem.dialogContainer.dialogText;
            var nameText = dialogSystem.dialogContainer.nameContainer.nameText;

            data.currentDialog = dialogText.text;
            data.dialogFont = File_Paths.resourcesFont + dialogText.font.name;
            data.dialogColor = dialogText.color;
            data.dialogScale = dialogText.fontSize;

            data.currentSpeaker = nameText.text;
            data.speakerFont = File_Paths.resourcesFont + nameText.font.name;
            data.speakerNameColor = nameText.color;
            data.speakerScale = nameText.fontSize;

            data.boxName = Dialog_System.instance.boxName;
            data.buildMethod = Dialog_System.instance.textSystem.buildMethod;
            data.dcp = Dialog_Continue_Prompt.instance.spriteName;
            data.textSpeed = Dialog_System.instance.textSystem.speed;

            data.nameBoxSize = Dialog_System.instance.dialogContainer.nameContainer.nameBoxSize.value;

            return data;

        }

        public static void Apply(Dialog_Data data){

            var dialogSystem = Dialog_System.instance;

            dialogSystem.dialogContainer.nameContainer.nameBoxSize.value = data.nameBoxSize;

            if(data.boxName == "default"){

                if(dialogSystem.dialogBoxObject != dialogSystem.dialogContainer.textDefault.gameObject){

                    dialogSystem.Switch_Textbox("default");
                    Object.Destroy(Dialog_System.instance.dialogBoxObject);

                }

            }else{

                dialogSystem.Switch_Textbox(data.boxName);

            }

            Dialog_Continue_Prompt.instance.Set_Sprite(data.dcp);
            dialogSystem.textSystem.buildMethod = data.buildMethod;
            dialogSystem.textSystem.speed = data.textSpeed;

            var dialogText = dialogSystem.dialogContainer.dialogText;
            var nameText = dialogSystem.dialogContainer.nameContainer.nameText;

            dialogSystem.conversationManager.textSystem.Set_Text(data.currentDialog);
            dialogText.color = data.dialogColor;
            dialogText.fontSize = data.dialogScale;

            nameText.text = data.currentSpeaker;

            if(nameText.text != string.Empty && dialogSystem.boxName == "default"){

                dialogSystem.dialogContainer.nameContainer.Show();

            }else{

                dialogSystem.dialogContainer.nameContainer.Hide();

            }

            nameText.color = data.speakerNameColor;
            nameText.fontSize = data.speakerScale;

            if(data.dialogFont != dialogText.font.name){

                TMP_FontAsset fontAsset = History_Cache.Load_Font(data.dialogFont);

                if(fontAsset != null){

                    dialogText.font = fontAsset;

                }

            }

            if(data.speakerFont != nameText.font.name){

                TMP_FontAsset fontAsset = History_Cache.Load_Font(data.speakerFont);

                if(fontAsset != null){

                    nameText.font = fontAsset;

                }

            }
        
        }

    }

}
=======
using Dialog;
using TMPro;
using UnityEngine;

namespace History{

    [System.Serializable]

    public class Dialog_Data{
        
        public string currentDialog = "";
        public string currentSpeaker = "";

        public string dialogFont;
        public Color dialogColor;
        public float dialogScale;

        public string speakerFont;
        public Color speakerNameColor;
        public float speakerScale;
        
        public string boxName;
        public Text_System.BuildMethod buildMethod;
        public string dcp;
        public float textSpeed;

        public float nameBoxSize;

        public static Dialog_Data Capture(){

            Dialog_Data data = new Dialog_Data();

            var dialogSystem = Dialog_System.instance;
            var dialogText = dialogSystem.dialogContainer.dialogText;
            var nameText = dialogSystem.dialogContainer.nameContainer.nameText;

            data.currentDialog = dialogText.text;
            data.dialogFont = File_Paths.resourcesFont + dialogText.font.name;
            data.dialogColor = dialogText.color;
            data.dialogScale = dialogText.fontSize;

            data.currentSpeaker = nameText.text;
            data.speakerFont = File_Paths.resourcesFont + nameText.font.name;
            data.speakerNameColor = nameText.color;
            data.speakerScale = nameText.fontSize;

            data.boxName = Dialog_System.instance.boxName;
            data.buildMethod = Dialog_System.instance.textSystem.buildMethod;
            data.dcp = Dialog_Continue_Prompt.instance.spriteName;
            data.textSpeed = Dialog_System.instance.textSystem.speed;

            data.nameBoxSize = Dialog_System.instance.dialogContainer.nameContainer.nameBoxSize.value;

            return data;

        }

        public static void Apply(Dialog_Data data){

            var dialogSystem = Dialog_System.instance;

            dialogSystem.dialogContainer.nameContainer.nameBoxSize.value = data.nameBoxSize;

            if(data.boxName == "default"){

                if(dialogSystem.dialogBoxObject != dialogSystem.dialogContainer.textDefault.gameObject){

                    dialogSystem.Switch_Textbox("default");
                    Object.Destroy(Dialog_System.instance.dialogBoxObject);

                }

            }else{

                dialogSystem.Switch_Textbox(data.boxName);

            }

            Dialog_Continue_Prompt.instance.Set_Sprite(data.dcp);
            dialogSystem.textSystem.buildMethod = data.buildMethod;
            dialogSystem.textSystem.speed = data.textSpeed;

            var dialogText = dialogSystem.dialogContainer.dialogText;
            var nameText = dialogSystem.dialogContainer.nameContainer.nameText;

            dialogSystem.conversationManager.textSystem.Set_Text(data.currentDialog);
            dialogText.color = data.dialogColor;
            dialogText.fontSize = data.dialogScale;

            nameText.text = data.currentSpeaker;

            if(nameText.text != string.Empty && dialogSystem.boxName == "default"){

                dialogSystem.dialogContainer.nameContainer.Show();

            }else{

                dialogSystem.dialogContainer.nameContainer.Hide();

            }

            nameText.color = data.speakerNameColor;
            nameText.fontSize = data.speakerScale;

            if(data.dialogFont != dialogText.font.name){

                TMP_FontAsset fontAsset = History_Cache.Load_Font(data.dialogFont);

                if(fontAsset != null){

                    dialogText.font = fontAsset;

                }

            }

            if(data.speakerFont != nameText.font.name){

                TMP_FontAsset fontAsset = History_Cache.Load_Font(data.speakerFont);

                if(fontAsset != null){

                    nameText.font = fontAsset;

                }

            }
        
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
