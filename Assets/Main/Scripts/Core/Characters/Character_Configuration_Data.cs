<<<<<<< HEAD
using UnityEngine;
using TMPro;
using Dialog;
using AYellowpaper.SerializedCollections;

namespace Characters{

    [System.Serializable]

    public class Character_Configuration_Data{
        
        public string name;
        public string alias;
        public Color nameColor;
        public Color dialogColor;
        public TMP_FontAsset nameFont;
        public TMP_FontAsset dialogFont;
        public float nameFontSize;
        public float dialogFontSize;
        public Vector3 dialogBoxVector;
        public Vector3 dialogBoxScale;
        public Character.Character_Type characterType;

        [SerializedDictionary("Path / ID", "Sprite")]
        public SerializedDictionary<string, Sprite> sprites = new SerializedDictionary<string, Sprite>();

        public Character_Configuration_Data Copy(){

            Character_Configuration_Data result = new Character_Configuration_Data();

            result.name = name;
            result.alias = alias;
            result.nameFont = nameFont;
            result.dialogFont = dialogFont;
            result.characterType = characterType;

            result.nameColor = new Color(nameColor.r, nameColor.g, nameColor.b, nameColor.a);
            result.dialogColor = new Color(dialogColor.r, dialogColor.g, dialogColor.b, dialogColor.a);

            result.dialogFontSize = dialogFontSize;
            result.nameFontSize = nameFontSize;

            result.dialogBoxVector = dialogBoxVector;
            result.dialogBoxScale = dialogBoxScale;

            return result;

        }

        private static Color defaultColor => Dialog_System.instance.configurations.defaultTextColor;
        private static TMP_FontAsset defaultFont => Dialog_System.instance.configurations.defaultTextFont;

        public static Character_Configuration_Data Default_Data{

            get{

            Character_Configuration_Data result = new Character_Configuration_Data();

            result.name = "";
            result.alias = "";
            result.nameFont = defaultFont;
            result.dialogFont = defaultFont;
            result.characterType = Character.Character_Type.Text;

            result.nameColor = new Color(defaultColor.r, defaultColor.b, defaultColor.g, defaultColor.a);
            result.dialogColor = new Color(defaultColor.r, defaultColor.b, defaultColor.g, defaultColor.a);

            result.dialogFontSize = Dialog_System.instance.configurations.defaultDialogFontSize;
            result.nameFontSize = Dialog_System.instance.configurations.defaultNameFontSize;
            
            result.dialogBoxVector = new Vector3(0, 0, 0);
            result.dialogBoxScale = new Vector3(0, 0, 0);

            return result;

            }

        }
        
    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
=======
using UnityEngine;
using TMPro;
using Dialog;
using AYellowpaper.SerializedCollections;

namespace Characters{

    [System.Serializable]

    public class Character_Configuration_Data{
        
        public string name;
        public string alias;
        public Color nameColor;
        public Color dialogColor;
        public TMP_FontAsset nameFont;
        public TMP_FontAsset dialogFont;
        public float nameFontSize;
        public float dialogFontSize;
        public Vector3 dialogBoxVector;
        public Vector3 dialogBoxScale;
        public Character.Character_Type characterType;

        [SerializedDictionary("Path / ID", "Sprite")]
        public SerializedDictionary<string, Sprite> sprites = new SerializedDictionary<string, Sprite>();

        public Character_Configuration_Data Copy(){

            Character_Configuration_Data result = new Character_Configuration_Data();

            result.name = name;
            result.alias = alias;
            result.nameFont = nameFont;
            result.dialogFont = dialogFont;
            result.characterType = characterType;

            result.nameColor = new Color(nameColor.r, nameColor.g, nameColor.b, nameColor.a);
            result.dialogColor = new Color(dialogColor.r, dialogColor.g, dialogColor.b, dialogColor.a);

            result.dialogFontSize = dialogFontSize;
            result.nameFontSize = nameFontSize;

            result.dialogBoxVector = dialogBoxVector;
            result.dialogBoxScale = dialogBoxScale;

            return result;

        }

        private static Color defaultColor => Dialog_System.instance.configurations.defaultTextColor;
        private static TMP_FontAsset defaultFont => Dialog_System.instance.configurations.defaultTextFont;

        public static Character_Configuration_Data Default_Data{

            get{

            Character_Configuration_Data result = new Character_Configuration_Data();

            result.name = "";
            result.alias = "";
            result.nameFont = defaultFont;
            result.dialogFont = defaultFont;
            result.characterType = Character.Character_Type.Text;

            result.nameColor = new Color(defaultColor.r, defaultColor.b, defaultColor.g, defaultColor.a);
            result.dialogColor = new Color(defaultColor.r, defaultColor.b, defaultColor.g, defaultColor.a);

            result.dialogFontSize = Dialog_System.instance.configurations.defaultDialogFontSize;
            result.nameFontSize = Dialog_System.instance.configurations.defaultNameFontSize;
            
            result.dialogBoxVector = new Vector3(0, 0, 0);
            result.dialogBoxScale = new Vector3(0, 0, 0);

            return result;

            }

        }
        
    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
>>>>>>> c8d4f422 (Whole Template)
