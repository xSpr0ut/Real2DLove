<<<<<<< HEAD
using System.IO;
using History;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VN;

namespace Menu.Save_Load{

    public class Save_Load_Slot : MonoBehaviour{
        
        public GameObject root;
        public RawImage previewImage;
        public TextMeshProUGUI titleText;
        public Button deleteButton;
        public Button saveButton;
        public Button loadButton;

        [HideInInspector] public int fileNumber = 0;
        [HideInInspector] public string filePath = "";

        private UI_Confirmation_Menu uiChoiceMenu => UI_Confirmation_Menu.instance;

        public void Populate_Details(Save_And_Load_Menu.Menu_Function function){

            if(File.Exists(filePath)){

                VN_Save_Game file = VN_Save_Game.Load(filePath, popDetails: true);
                Populate_Details_From_File(function, file);

            }else{

                Populate_Details_From_File(function, null);

            }

        }

        private void Populate_Details_From_File(Save_And_Load_Menu.Menu_Function function, VN_Save_Game file){

            if(file == null){

                titleText.text = $"{fileNumber}. Empty File";
                deleteButton.gameObject.SetActive(false);
                loadButton.gameObject.SetActive(false);
                saveButton.gameObject.SetActive(function == Save_And_Load_Menu.Menu_Function.save);
                previewImage.texture = Save_And_Load_Menu.instance.emptyFileImage;

            }else{

                titleText.text = $"{fileNumber}. {file.title}\n{file.timestamp}";
                deleteButton.gameObject.SetActive(true);
                loadButton.gameObject.SetActive(function == Save_And_Load_Menu.Menu_Function.load);
                saveButton.gameObject.SetActive(function == Save_And_Load_Menu.Menu_Function.save);
                
                byte[] data = File.ReadAllBytes(file.screenshotPath);
                Texture2D screenshotPreview = new Texture2D(1, 1);
                ImageConversion.LoadImage(screenshotPreview, data);
                
                previewImage.texture = screenshotPreview;

            }

        }

        public void Click_Delete(){

            uiChoiceMenu.Show("Delete file permanently?\nSeriously?", 
            new UI_Confirmation_Menu.Confirmation_Button("Yes", () =>{

                uiChoiceMenu.Show(

                    "Are you sure? Really?",
                    new UI_Confirmation_Menu.Confirmation_Button("Yes, really", Delete),
                    new UI_Confirmation_Menu.Confirmation_Button("I guess not", null));

            }, autoCloseOnClick: false), 
            new UI_Confirmation_Menu.Confirmation_Button("Nevermind", null));

        }

        private void Delete(){

            File.Delete(filePath);
            Populate_Details(Save_And_Load_Menu.instance.menuFunction);

        }

        public void Load(){

            VN_Save_Game file = VN_Save_Game.Load(filePath, false);
            Save_And_Load_Menu.instance.Close(true);

            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main_Menu"){

                Main_Menu.instance.Load_Game(file);

            }else{

                file.Reapply();

            }

        }
        
        public void Save(){

            if(History_Manager.instance.isViewingHistory){

                UI_Confirmation_Menu.instance.Show("Ya can't save while viewing history.", new UI_Confirmation_Menu.Confirmation_Button("Fine", null));

                return;

            }

            if(File.Exists(filePath)){

                var activeSave = VN_Save_Game.Load(filePath);

                if(activeSave != null){

                    UI_Confirmation_Menu.instance.Show("Do you want to overwrite this file?", new UI_Confirmation_Menu.Confirmation_Button("Yes", Confirm_Save), new UI_Confirmation_Menu.Confirmation_Button("No", null));

                }
                
            }else{

                Confirm_Save();
                
            }

        }
        
        private void Confirm_Save(){
            
            var activeSave = VN_Save_Game.activeFile;
            activeSave.slotNumber = fileNumber;
            activeSave.Save();
            Populate_Details_From_File(Save_And_Load_Menu.instance.menuFunction, activeSave);
            
        }

    }

}
=======
using System.IO;
using History;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VN;

namespace Menu.Save_Load{

    public class Save_Load_Slot : MonoBehaviour{
        
        public GameObject root;
        public RawImage previewImage;
        public TextMeshProUGUI titleText;
        public Button deleteButton;
        public Button saveButton;
        public Button loadButton;

        [HideInInspector] public int fileNumber = 0;
        [HideInInspector] public string filePath = "";

        private UI_Confirmation_Menu uiChoiceMenu => UI_Confirmation_Menu.instance;

        public void Populate_Details(Save_And_Load_Menu.Menu_Function function){

            if(File.Exists(filePath)){

                VN_Save_Game file = VN_Save_Game.Load(filePath, popDetails: true);
                Populate_Details_From_File(function, file);

            }else{

                Populate_Details_From_File(function, null);

            }

        }

        private void Populate_Details_From_File(Save_And_Load_Menu.Menu_Function function, VN_Save_Game file){

            if(file == null){

                titleText.text = $"{fileNumber}. Empty File";
                deleteButton.gameObject.SetActive(false);
                loadButton.gameObject.SetActive(false);
                saveButton.gameObject.SetActive(function == Save_And_Load_Menu.Menu_Function.save);
                previewImage.texture = Save_And_Load_Menu.instance.emptyFileImage;

            }else{

                titleText.text = $"{fileNumber}. {file.title}\n{file.timestamp}";
                deleteButton.gameObject.SetActive(true);
                loadButton.gameObject.SetActive(function == Save_And_Load_Menu.Menu_Function.load);
                saveButton.gameObject.SetActive(function == Save_And_Load_Menu.Menu_Function.save);
                
                byte[] data = File.ReadAllBytes(file.screenshotPath);
                Texture2D screenshotPreview = new Texture2D(1, 1);
                ImageConversion.LoadImage(screenshotPreview, data);
                
                previewImage.texture = screenshotPreview;

            }

        }

        public void Click_Delete(){

            uiChoiceMenu.Show("Delete file permanently?\nSeriously?", 
            new UI_Confirmation_Menu.Confirmation_Button("Yes", () =>{

                uiChoiceMenu.Show(

                    "Are you sure? Really?",
                    new UI_Confirmation_Menu.Confirmation_Button("Yes, really", Delete),
                    new UI_Confirmation_Menu.Confirmation_Button("I guess not", null));

            }, autoCloseOnClick: false), 
            new UI_Confirmation_Menu.Confirmation_Button("Nevermind", null));

        }

        private void Delete(){

            File.Delete(filePath);
            Populate_Details(Save_And_Load_Menu.instance.menuFunction);

        }

        public void Load(){

            VN_Save_Game file = VN_Save_Game.Load(filePath, false);
            Save_And_Load_Menu.instance.Close(true);

            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main_Menu"){

                Main_Menu.instance.Load_Game(file);

            }else{

                file.Reapply();

            }

        }
        
        public void Save(){

            if(History_Manager.instance.isViewingHistory){

                UI_Confirmation_Menu.instance.Show("Ya can't save while viewing history.", new UI_Confirmation_Menu.Confirmation_Button("Fine", null));

                return;

            }

            if(File.Exists(filePath)){

                var activeSave = VN_Save_Game.Load(filePath);

                if(activeSave != null){

                    UI_Confirmation_Menu.instance.Show("Do you want to overwrite this file?", new UI_Confirmation_Menu.Confirmation_Button("Yes", Confirm_Save), new UI_Confirmation_Menu.Confirmation_Button("No", null));

                }
                
            }else{

                Confirm_Save();
                
            }

        }
        
        private void Confirm_Save(){
            
            var activeSave = VN_Save_Game.activeFile;
            activeSave.slotNumber = fileNumber;
            activeSave.Save();
            Populate_Details_From_File(Save_And_Load_Menu.instance.menuFunction, activeSave);
            
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
