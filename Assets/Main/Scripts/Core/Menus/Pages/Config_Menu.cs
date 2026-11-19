<<<<<<< HEAD
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Audio;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Config{

    public class Config_Menu : Menu_Page{

        //To make more menus, duplicate this and change the ui items
        public static Config_Menu instance {get; private set;}
        [SerializeField] private GameObject[] panels; 
        public UI_Items ui;
        private GameObject activePanel;

        public VN_Configuration config => VN_Configuration.activeConfig;

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            for( int i = 0; i < panels.Length; i++){

                panels[i].SetActive(i == 0);

            }

            activePanel = panels[0];

            Set_Available_Resolutions();
            Load_Config();
            
        }

        private void Load_Config(){

            if(File.Exists(VN_Configuration.filePath)){

                VN_Configuration.activeConfig = File_Manager.Load<VN_Configuration>(VN_Configuration.filePath, VN_Configuration.encrypt);

            }else{

                VN_Configuration.activeConfig = new VN_Configuration();

            }

            VN_Configuration.activeConfig.Load();

        }

        //If you want ot make menus whose values are tied to save files, move this
        //bit into a function that is called upon save
        private void OnAppicationQuit(){

            VN_Configuration.activeConfig.Save();
            VN_Configuration.activeConfig = null;
            
        }

        public void Open_Panel(string panelName){

            GameObject panel = panels.First(p => p.name.ToLower() == panelName.ToLower());

            if(panel == null){

                Debug.LogError($"there ain't no panel called {panelName} in the config menu.");
                return;

            }

            if(activePanel != null && activePanel != panel){

                activePanel.SetActive(false);

            }

            panel.SetActive(true);
            activePanel = panel;

        }

        private void Set_Available_Resolutions(){

            Resolution[] resolutions = Screen.resolutions;
            List<string> options = new List<string>();

            for(int i = resolutions.Length -1; i >= 0; i--){

                options.Add($"{resolutions[i].width}x{resolutions[i].height}");

            }

            ui.resolutions.ClearOptions();
            ui.resolutions.AddOptions(options);

        }

        [System.Serializable]
        //Add eveerything on the panels here

        public class UI_Items{

            private static Color buttonSelectedColor = new Color(1, 0.35f, 0, 1);
            private static Color buttonUnselectedColor = new Color(1f, 1f, 1, 1);
            private static Color textSelectedColor = new Color(1f, 1f, 0, 1);
            private static Color textUnselectedColor = new Color(0.25f, 0.25f, 0.25f, 1);
            public static Color musicOnColor = new Color(1, 0.65f, 0, 1);
            public static Color musicOffColor = new Color(0.5f, 0.5f, 0.5f, 1);

            [Header("General")]
            public Button fullScreen;
            public Button  windowed;
            public TMP_Dropdown resolutions;
            public Button skippingContinue, skippingStop;
            public Slider textSpeed, autoReaderSpeed;

            [Header("Audio")]
            public Slider musicVolume;
            public Image musicFill;
            public Slider sfxVolume;
            public Image sfxFill;
            public Slider voicesVolume;
            public Image voicesFill;
            public Sprite mutedSymbol;
            public Sprite unmutedSymbol;
            public Image musicMute;
            public Image sfxMute;
            public Image voicesMute;

            public void Set_Button_Colors(Button A, Button B, bool selectedA){

                A.GetComponent<Image>().color = selectedA ? buttonSelectedColor : buttonUnselectedColor;
                B.GetComponent<Image>().color = !selectedA ? buttonSelectedColor : buttonUnselectedColor;
                
                A.GetComponentInChildren<TextMeshProUGUI>().color = selectedA ? textSelectedColor : textUnselectedColor;
                B.GetComponentInChildren<TextMeshProUGUI>().color = !selectedA ? textSelectedColor : textUnselectedColor;

            }
            
        }

        public void Set_Display_To_Fullscreen(bool fullscreen){

            Screen.fullScreen = fullscreen;
            ui.Set_Button_Colors(ui.fullScreen, ui.windowed, fullscreen);

        }

        public void Set_Display_Resolution(){

            string resolution = instance.ui.resolutions.captionText.text;
            string[] values = resolution.Split('x');

            if(int.TryParse(values[0], out int width) && int.TryParse(values[1], out int height)){

                Screen.SetResolution(width, height, Screen.fullScreen);
                config.displayResolution = resolution;

            }else{

                Debug.LogError($"What kinda resolution is {resolution}?!");

            }

        }

        public void Set_Continue_Skipping_After_Choice(bool continueSkipping){

            config.continueSkippingAfterChoice = continueSkipping;
            ui.Set_Button_Colors(ui.skippingContinue, ui.skippingStop, continueSkipping);

        }

        public void Set_Text_Speed(){

            config.dialogTextSpeed = ui.textSpeed.value;

            if(Dialog_System.instance != null){

                Dialog_System.instance.conversationManager.textSystem.speed = config.dialogTextSpeed;

            }
            
        }

        public void Set_Auto_Speed(){

            config.dialogAutoSpeed = ui.autoReaderSpeed.value;

            if(Dialog_System.instance != null){

                Auto_Reader autoReader = Dialog_System.instance.autoReader;

                if(autoReader != null){

                    autoReader.speed = config.dialogAutoSpeed;

                }

            }
        
        }

        //Audio Settings
        public void Set_Music_Volume(){

            config.musicVolume = ui.musicVolume.value;
            Audio_Manager.instance.Set_Music_Volume(config.musicVolume, config.musicMute);
            ui.musicFill.color = config.musicMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            
        }

        public void Set_SFX_Volume(){

            config.sfxVolume = ui.sfxVolume.value;
            Audio_Manager.instance.Set_SFX_Volume(config.sfxVolume, config.sfxMute);
            ui.sfxFill.color = config.sfxMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            
        }

        public void Set_Voices_Volume(){

            config.voicesVolume = ui.voicesVolume.value;

            Audio_Manager.instance.Set_Voices_Volume(config.voicesVolume, config.voicesMute);
            ui.voicesFill.color = config.voicesMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            
        }

        public void Set_Music_Mute(){

            config.musicMute = !config.musicMute;
            ui.musicVolume.fillRect.GetComponent<Image>().color = config.musicMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            ui.musicMute.sprite = config.musicMute ? ui.mutedSymbol : ui.unmutedSymbol;

            Audio_Manager.instance.Set_Music_Volume(config.musicVolume, config.musicMute);
            
        }

        public void Set_SFX_Mute(){

            config.sfxMute = !config.sfxMute;
            ui.sfxVolume.fillRect.GetComponent<Image>().color = config.sfxMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            ui.sfxMute.sprite = config.sfxMute ? ui.mutedSymbol : ui.unmutedSymbol;

            Audio_Manager.instance.Set_SFX_Volume(config.sfxVolume, config.sfxMute);
            
        }

        public void Set_Voices_Mute(){

            config.voicesMute = !config.voicesMute;
            ui.voicesVolume.fillRect.GetComponent<Image>().color = config.voicesMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            ui.voicesMute.sprite = config.voicesMute ? ui.mutedSymbol : ui.unmutedSymbol;

            Audio_Manager.instance.Set_Voices_Volume(config.voicesVolume, config.voicesMute);
            
        }

    }

}
=======
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Audio;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Config{

    public class Config_Menu : Menu_Page{

        //To make more menus, duplicate this and change the ui items
        public static Config_Menu instance {get; private set;}
        [SerializeField] private GameObject[] panels; 
        public UI_Items ui;
        private GameObject activePanel;

        public VN_Configuration config => VN_Configuration.activeConfig;

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            for( int i = 0; i < panels.Length; i++){

                panels[i].SetActive(i == 0);

            }

            activePanel = panels[0];

            Set_Available_Resolutions();
            Load_Config();
            
        }

        private void Load_Config(){

            if(File.Exists(VN_Configuration.filePath)){

                VN_Configuration.activeConfig = File_Manager.Load<VN_Configuration>(VN_Configuration.filePath, VN_Configuration.encrypt);

            }else{

                VN_Configuration.activeConfig = new VN_Configuration();

            }

            VN_Configuration.activeConfig.Load();

        }

        //If you want ot make menus whose values are tied to save files, move this
        //bit into a function that is called upon save
        private void OnAppicationQuit(){

            VN_Configuration.activeConfig.Save();
            VN_Configuration.activeConfig = null;
            
        }

        public void Open_Panel(string panelName){

            GameObject panel = panels.First(p => p.name.ToLower() == panelName.ToLower());

            if(panel == null){

                Debug.LogError($"there ain't no panel called {panelName} in the config menu.");
                return;

            }

            if(activePanel != null && activePanel != panel){

                activePanel.SetActive(false);

            }

            panel.SetActive(true);
            activePanel = panel;

        }

        private void Set_Available_Resolutions(){

            Resolution[] resolutions = Screen.resolutions;
            List<string> options = new List<string>();

            for(int i = resolutions.Length -1; i >= 0; i--){

                options.Add($"{resolutions[i].width}x{resolutions[i].height}");

            }

            ui.resolutions.ClearOptions();
            ui.resolutions.AddOptions(options);

        }

        [System.Serializable]
        //Add eveerything on the panels here

        public class UI_Items{

            private static Color buttonSelectedColor = new Color(1, 0.35f, 0, 1);
            private static Color buttonUnselectedColor = new Color(1f, 1f, 1, 1);
            private static Color textSelectedColor = new Color(1f, 1f, 0, 1);
            private static Color textUnselectedColor = new Color(0.25f, 0.25f, 0.25f, 1);
            public static Color musicOnColor = new Color(1, 0.65f, 0, 1);
            public static Color musicOffColor = new Color(0.5f, 0.5f, 0.5f, 1);

            [Header("General")]
            public Button fullScreen;
            public Button  windowed;
            public TMP_Dropdown resolutions;
            public Button skippingContinue, skippingStop;
            public Slider textSpeed, autoReaderSpeed;

            [Header("Audio")]
            public Slider musicVolume;
            public Image musicFill;
            public Slider sfxVolume;
            public Image sfxFill;
            public Slider voicesVolume;
            public Image voicesFill;
            public Sprite mutedSymbol;
            public Sprite unmutedSymbol;
            public Image musicMute;
            public Image sfxMute;
            public Image voicesMute;

            public void Set_Button_Colors(Button A, Button B, bool selectedA){

                A.GetComponent<Image>().color = selectedA ? buttonSelectedColor : buttonUnselectedColor;
                B.GetComponent<Image>().color = !selectedA ? buttonSelectedColor : buttonUnselectedColor;
                
                A.GetComponentInChildren<TextMeshProUGUI>().color = selectedA ? textSelectedColor : textUnselectedColor;
                B.GetComponentInChildren<TextMeshProUGUI>().color = !selectedA ? textSelectedColor : textUnselectedColor;

            }
            
        }

        public void Set_Display_To_Fullscreen(bool fullscreen){

            Screen.fullScreen = fullscreen;
            ui.Set_Button_Colors(ui.fullScreen, ui.windowed, fullscreen);

        }

        public void Set_Display_Resolution(){

            string resolution = instance.ui.resolutions.captionText.text;
            string[] values = resolution.Split('x');

            if(int.TryParse(values[0], out int width) && int.TryParse(values[1], out int height)){

                Screen.SetResolution(width, height, Screen.fullScreen);
                config.displayResolution = resolution;

            }else{

                Debug.LogError($"What kinda resolution is {resolution}?!");

            }

        }

        public void Set_Continue_Skipping_After_Choice(bool continueSkipping){

            config.continueSkippingAfterChoice = continueSkipping;
            ui.Set_Button_Colors(ui.skippingContinue, ui.skippingStop, continueSkipping);

        }

        public void Set_Text_Speed(){

            config.dialogTextSpeed = ui.textSpeed.value;

            if(Dialog_System.instance != null){

                Dialog_System.instance.conversationManager.textSystem.speed = config.dialogTextSpeed;

            }
            
        }

        public void Set_Auto_Speed(){

            config.dialogAutoSpeed = ui.autoReaderSpeed.value;

            if(Dialog_System.instance != null){

                Auto_Reader autoReader = Dialog_System.instance.autoReader;

                if(autoReader != null){

                    autoReader.speed = config.dialogAutoSpeed;

                }

            }
        
        }

        //Audio Settings
        public void Set_Music_Volume(){

            config.musicVolume = ui.musicVolume.value;
            Audio_Manager.instance.Set_Music_Volume(config.musicVolume, config.musicMute);
            ui.musicFill.color = config.musicMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            
        }

        public void Set_SFX_Volume(){

            config.sfxVolume = ui.sfxVolume.value;
            Audio_Manager.instance.Set_SFX_Volume(config.sfxVolume, config.sfxMute);
            ui.sfxFill.color = config.sfxMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            
        }

        public void Set_Voices_Volume(){

            config.voicesVolume = ui.voicesVolume.value;

            Audio_Manager.instance.Set_Voices_Volume(config.voicesVolume, config.voicesMute);
            ui.voicesFill.color = config.voicesMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            
        }

        public void Set_Music_Mute(){

            config.musicMute = !config.musicMute;
            ui.musicVolume.fillRect.GetComponent<Image>().color = config.musicMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            ui.musicMute.sprite = config.musicMute ? ui.mutedSymbol : ui.unmutedSymbol;

            Audio_Manager.instance.Set_Music_Volume(config.musicVolume, config.musicMute);
            
        }

        public void Set_SFX_Mute(){

            config.sfxMute = !config.sfxMute;
            ui.sfxVolume.fillRect.GetComponent<Image>().color = config.sfxMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            ui.sfxMute.sprite = config.sfxMute ? ui.mutedSymbol : ui.unmutedSymbol;

            Audio_Manager.instance.Set_SFX_Volume(config.sfxVolume, config.sfxMute);
            
        }

        public void Set_Voices_Mute(){

            config.voicesMute = !config.voicesMute;
            ui.voicesVolume.fillRect.GetComponent<Image>().color = config.voicesMute ? UI_Items.musicOffColor : UI_Items.musicOnColor;
            ui.voicesMute.sprite = config.voicesMute ? ui.mutedSymbol : ui.unmutedSymbol;

            Audio_Manager.instance.Set_Voices_Volume(config.voicesVolume, config.voicesMute);
            
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
