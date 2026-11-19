<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Characters;

namespace Dialog{
    
    public class Dialog_System : MonoBehaviour{

        private const string textboxNameFormat = "Dialog_Box: [{0}]";

        [SerializeField] private Dialog_System_Configurations_SO _configurations;
        public Dialog_System_Configurations_SO configurations => _configurations;
        public Dialog_Container dialogContainer = new Dialog_Container();
        public Conversation_Manager conversationManager { get; private set; }
        public Text_System textSystem = null;
        public Auto_Reader autoReader {get; private set;}

        public TextMeshProUGUI textBox = null;
        public GameObject dialogBoxObject = null;
        public bool makeNewDialogBox = false;
        public string boxName = "default";

        [SerializeField] private CanvasGroup mainCanvas;

        public static Dialog_System instance {get; private set;}
        public delegate void Dialog_System_Event();
        public event Dialog_System_Event next_On_Player_Input;
        public event Dialog_System_Event onClear;

        public bool isRunningCoversation => conversationManager.isRunning;

        public Dialog_Continue_Prompt prompt;
        private Canvas_Group_Controller cgController;

        private void Awake(){

            if(instance == null){

                instance = this;
                Initialize();

            }else{

                DestroyImmediate(gameObject);

            }
            
        }

        bool initialized = false;

        private void Initialize(){

            if(initialized){

                return;

            }

            textBox = dialogContainer.textDefault;
            textSystem = new Text_System(textBox);
            textSystem.buildMethod = Text_System.BuildMethod.gradual;
            conversationManager = new Conversation_Manager(textSystem);

            cgController = new Canvas_Group_Controller(this, mainCanvas);
            dialogContainer.Initialize();

            autoReader = GetComponent<Auto_Reader>();

            if(autoReader != null){

                autoReader.Initialize(conversationManager);

            }

        }

        public void Switch_Textbox(string name, bool keep = false){

            if(name == "default".ToLower()){

                if(!keep){

                    textBox.text = "";

                }
                
                textBox = dialogContainer.textDefault;
                dialogContainer.dialogText = textBox;
                textSystem = new Text_System(textBox);
                conversationManager.textSystem = textSystem;
                prompt.transform.SetParent(dialogContainer.textDefault.transform);
                boxName = "default";
                return;

            }

            if(name == null){

                return;

            }

            GameObject characterPrefab = Character_Manager.instance.Get_Prefab_For_Character(name);

            if(dialogBoxObject == null || makeNewDialogBox == true){

                dialogBoxObject = Create_Box_Gameobject(name, characterPrefab);

            }
            
            RectTransform root = dialogBoxObject.GetComponent<RectTransform>();

            root.transform.SetParent(dialogContainer.root.transform, worldPositionStays: true);
            
            Character_Configuration_Data configurationData = Character_Manager.instance.Get_Character_Configuration_Data(name);
            root.localPosition = configurationData.dialogBoxVector;
            dialogBoxObject.transform.localScale = configurationData.dialogBoxScale;

            TextMeshProUGUI characterTextClone = dialogBoxObject.GetComponent<TextMeshProUGUI>();
            Text_System textSystemCharacter = new Text_System(characterTextClone);
            dialogContainer.dialogText = characterTextClone;

            if(!keep){

                textBox.text = "";

            }
           
            textBox = characterTextClone;
            textSystem = textSystemCharacter;

            conversationManager.textSystem = textSystem;
            makeNewDialogBox = false;
            boxName = name;
            dialogContainer.nameContainer.nameText.text = name;

        }

        private GameObject Create_Box_Gameobject(string name, GameObject characterPrefab){

            TextMeshProUGUI characterText = characterPrefab.GetComponentInChildren<TextMeshProUGUI>();
            GameObject dialogBox = characterText.gameObject;

            GameObject dialogBoxObject = Instantiate(dialogBox);
            dialogBoxObject.name = string.Format(textboxNameFormat, name);
            return dialogBoxObject;

        }

        public void Next_On_Player_Input(){

            next_On_Player_Input?.Invoke();
           
            if(autoReader != null && autoReader.isOn){

                autoReader.Disable();

            }

        }

        public void Next_On_System_Input(){

            next_On_Player_Input?.Invoke();

        }

        public void On_System_Clear(){

            onClear?.Invoke();

        }

        public void On_Start_Viewing_History(){

            prompt.Hide();
            autoReader.allowToggle = false;
            conversationManager.allowInput = false;

            if(autoReader.isOn){

                autoReader.Disable();

            }

        }

        public void On_Stop_Viewing_History(){

            prompt.Show();
            autoReader.allowToggle = true;
            conversationManager.allowInput = true;

        }

        public void Apply_Speaker_Data_To_Dialog_Container(string speakerName){

            Character character = Character_Manager.instance.Get_Character(speakerName);
            Character_Configuration_Data configurationData = character != null ? character.configurationData : Character_Manager.instance.Get_Character_Configuration_Data(speakerName);
           
            Apply_Speaker_Data_To_Dialog_Container(configurationData);

        }

        public void Apply_Speaker_Data_To_Dialog_Container(Character_Configuration_Data configurationData){

            dialogContainer.Set_Dialog_Color(configurationData.dialogColor);
            dialogContainer.Set_Dialog_Font(configurationData.dialogFont);
            dialogContainer.Set_Dialog_Font_Size(configurationData.dialogFontSize * this.configurations.dialogFontScale);
            dialogContainer.nameContainer.Set_Name_Color(configurationData.nameColor);
            dialogContainer.nameContainer.Set_Name_Font(configurationData.nameFont);
            dialogContainer.nameContainer.Set_Name_Font_Size(configurationData.nameFontSize);

        }

        public void Show_Speaker_Name(string speakerName = ""){
            
            if(speakerName.ToLower() != "narrator"){
                
                dialogContainer.nameContainer.Show(speakerName);
            
            }else{

                Hide_Speaker_Name();
                dialogContainer.nameContainer.nameText.text = "";

            }

        }

        public void Hide_Speaker_Name() => dialogContainer.nameContainer.Hide();

        public Coroutine Say(string speaker, string dialog){

            List<string> conversation = new List<string>(){$"{speaker} \"{dialog}\""};
            return Say(conversation);

        }

        public Coroutine Say(List<string> lines, string filePath = ""){

            Conversation conversation = new Conversation(lines, file: filePath);
            return conversationManager.Start_Conversation(conversation);

        }

        public Coroutine Say(Conversation conversation){

            return conversationManager.Start_Conversation(conversation);

        }

        public bool isVisible => cgController.isVisible;
        public Coroutine Show(float speed = 1f, bool immediate = false) => cgController.Show(speed, immediate);

        public Coroutine Hide(float speed = 1f, bool immediate = false) => cgController.Hide(speed, immediate);

    }

=======
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Characters;

namespace Dialog{
    
    public class Dialog_System : MonoBehaviour{

        private const string textboxNameFormat = "Dialog_Box: [{0}]";

        [SerializeField] private Dialog_System_Configurations_SO _configurations;
        public Dialog_System_Configurations_SO configurations => _configurations;
        public Dialog_Container dialogContainer = new Dialog_Container();
        public Conversation_Manager conversationManager { get; private set; }
        public Text_System textSystem = null;
        public Auto_Reader autoReader {get; private set;}

        public TextMeshProUGUI textBox = null;
        public GameObject dialogBoxObject = null;
        public bool makeNewDialogBox = false;
        public string boxName = "default";

        [SerializeField] private CanvasGroup mainCanvas;

        public static Dialog_System instance {get; private set;}
        public delegate void Dialog_System_Event();
        public event Dialog_System_Event next_On_Player_Input;
        public event Dialog_System_Event onClear;

        public bool isRunningCoversation => conversationManager.isRunning;

        public Dialog_Continue_Prompt prompt;
        private Canvas_Group_Controller cgController;

        private void Awake(){

            if(instance == null){

                instance = this;
                Initialize();

            }else{

                DestroyImmediate(gameObject);

            }
            
        }

        bool initialized = false;

        private void Initialize(){

            if(initialized){

                return;

            }

            textBox = dialogContainer.textDefault;
            textSystem = new Text_System(textBox);
            textSystem.buildMethod = Text_System.BuildMethod.gradual;
            conversationManager = new Conversation_Manager(textSystem);

            cgController = new Canvas_Group_Controller(this, mainCanvas);
            dialogContainer.Initialize();

            autoReader = GetComponent<Auto_Reader>();

            if(autoReader != null){

                autoReader.Initialize(conversationManager);

            }

        }

        public void Switch_Textbox(string name, bool keep = false){

            if(name == "default".ToLower()){

                if(!keep){

                    textBox.text = "";

                }
                
                textBox = dialogContainer.textDefault;
                dialogContainer.dialogText = textBox;
                textSystem = new Text_System(textBox);
                conversationManager.textSystem = textSystem;
                prompt.transform.SetParent(dialogContainer.textDefault.transform);
                boxName = "default";
                return;

            }

            if(name == null){

                return;

            }

            GameObject characterPrefab = Character_Manager.instance.Get_Prefab_For_Character(name);

            if(dialogBoxObject == null || makeNewDialogBox == true){

                dialogBoxObject = Create_Box_Gameobject(name, characterPrefab);

            }
            
            RectTransform root = dialogBoxObject.GetComponent<RectTransform>();

            root.transform.SetParent(dialogContainer.root.transform, worldPositionStays: true);
            
            Character_Configuration_Data configurationData = Character_Manager.instance.Get_Character_Configuration_Data(name);
            root.localPosition = configurationData.dialogBoxVector;
            dialogBoxObject.transform.localScale = configurationData.dialogBoxScale;

            TextMeshProUGUI characterTextClone = dialogBoxObject.GetComponent<TextMeshProUGUI>();
            Text_System textSystemCharacter = new Text_System(characterTextClone);
            dialogContainer.dialogText = characterTextClone;

            if(!keep){

                textBox.text = "";

            }
           
            textBox = characterTextClone;
            textSystem = textSystemCharacter;

            conversationManager.textSystem = textSystem;
            makeNewDialogBox = false;
            boxName = name;
            dialogContainer.nameContainer.nameText.text = name;

        }

        private GameObject Create_Box_Gameobject(string name, GameObject characterPrefab){

            TextMeshProUGUI characterText = characterPrefab.GetComponentInChildren<TextMeshProUGUI>();
            GameObject dialogBox = characterText.gameObject;

            GameObject dialogBoxObject = Instantiate(dialogBox);
            dialogBoxObject.name = string.Format(textboxNameFormat, name);
            return dialogBoxObject;

        }

        public void Next_On_Player_Input(){

            next_On_Player_Input?.Invoke();
           
            if(autoReader != null && autoReader.isOn){

                autoReader.Disable();

            }

        }

        public void Next_On_System_Input(){

            next_On_Player_Input?.Invoke();

        }

        public void On_System_Clear(){

            onClear?.Invoke();

        }

        public void On_Start_Viewing_History(){

            prompt.Hide();
            autoReader.allowToggle = false;
            conversationManager.allowInput = false;

            if(autoReader.isOn){

                autoReader.Disable();

            }

        }

        public void On_Stop_Viewing_History(){

            prompt.Show();
            autoReader.allowToggle = true;
            conversationManager.allowInput = true;

        }

        public void Apply_Speaker_Data_To_Dialog_Container(string speakerName){

            Character character = Character_Manager.instance.Get_Character(speakerName);
            Character_Configuration_Data configurationData = character != null ? character.configurationData : Character_Manager.instance.Get_Character_Configuration_Data(speakerName);
           
            Apply_Speaker_Data_To_Dialog_Container(configurationData);

        }

        public void Apply_Speaker_Data_To_Dialog_Container(Character_Configuration_Data configurationData){

            dialogContainer.Set_Dialog_Color(configurationData.dialogColor);
            dialogContainer.Set_Dialog_Font(configurationData.dialogFont);
            dialogContainer.Set_Dialog_Font_Size(configurationData.dialogFontSize * this.configurations.dialogFontScale);
            dialogContainer.nameContainer.Set_Name_Color(configurationData.nameColor);
            dialogContainer.nameContainer.Set_Name_Font(configurationData.nameFont);
            dialogContainer.nameContainer.Set_Name_Font_Size(configurationData.nameFontSize);

        }

        public void Show_Speaker_Name(string speakerName = ""){
            
            if(speakerName.ToLower() != "narrator"){
                
                dialogContainer.nameContainer.Show(speakerName);
            
            }else{

                Hide_Speaker_Name();
                dialogContainer.nameContainer.nameText.text = "";

            }

        }

        public void Hide_Speaker_Name() => dialogContainer.nameContainer.Hide();

        public Coroutine Say(string speaker, string dialog){

            List<string> conversation = new List<string>(){$"{speaker} \"{dialog}\""};
            return Say(conversation);

        }

        public Coroutine Say(List<string> lines, string filePath = ""){

            Conversation conversation = new Conversation(lines, file: filePath);
            return conversationManager.Start_Conversation(conversation);

        }

        public Coroutine Say(Conversation conversation){

            return conversationManager.Start_Conversation(conversation);

        }

        public bool isVisible => cgController.isVisible;
        public Coroutine Show(float speed = 1f, bool immediate = false) => cgController.Show(speed, immediate);

        public Coroutine Hide(float speed = 1f, bool immediate = false) => cgController.Hide(speed, immediate);

    }

>>>>>>> c8d4f422 (Whole Template)
}