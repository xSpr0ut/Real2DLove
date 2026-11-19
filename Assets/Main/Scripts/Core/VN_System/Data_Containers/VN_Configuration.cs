<<<<<<< HEAD
using UnityEngine;

namespace Menu.Config{

    [System.Serializable]

    public class VN_Configuration{
        
        //For settings to be saved across all save files
        public static VN_Configuration activeConfig;

        public static string filePath => $"{File_Paths.root}vnconfig.cfg";
        public const bool encrypt = false;

        public bool displayFullscreen = true;
        public string displayResolution = "1920x1080";
        public bool continueSkippingAfterChoice = false;
        public float dialogTextSpeed = 1f;
        public float dialogAutoSpeed = 1f;

        public float musicVolume = 1f;
        public float sfxVolume = 1f;
        public float voicesVolume = 1f;
        public bool musicMute = false;
        public bool sfxMute = false;
        public bool voicesMute = false;

        public float historyLogScale = 1f;

        public void Load(){

            var ui = Config_Menu.instance.ui;

            Config_Menu.instance.Set_Display_To_Fullscreen(displayFullscreen);
            ui.Set_Button_Colors(ui.fullScreen, ui.windowed, displayFullscreen);

            int resolutionIndex = 0;

            for(int i = 0; i < ui.resolutions.options.Count; i++){

                string resolution = ui.resolutions.options[i].text;

                if(resolution == displayResolution){

                    resolutionIndex = i;
                    break;

                }

            }

            ui.resolutions.value = resolutionIndex;

            ui.Set_Button_Colors(ui.skippingContinue, ui.skippingStop, continueSkippingAfterChoice);

            ui.textSpeed.value = dialogTextSpeed;
            ui.autoReaderSpeed.value = dialogAutoSpeed;

            //Audio
            ui.musicVolume.value = musicVolume;
            ui.sfxVolume.value = sfxVolume;
            ui.voicesVolume.value = voicesVolume;
            ui.musicMute.sprite = musicMute ? ui.mutedSymbol : ui.unmutedSymbol;
            ui.sfxMute.sprite = sfxMute ? ui.mutedSymbol : ui.unmutedSymbol;
            ui.voicesMute.sprite = voicesMute ? ui.mutedSymbol : ui.unmutedSymbol;

        }

        public void Save(){

            File_Manager.Save(filePath, JsonUtility.ToJson(this), encrypt);

        }
    }

}
=======
using UnityEngine;

namespace Menu.Config{

    [System.Serializable]

    public class VN_Configuration{
        
        //For settings to be saved across all save files
        public static VN_Configuration activeConfig;

        public static string filePath => $"{File_Paths.root}vnconfig.cfg";
        public const bool encrypt = false;

        public bool displayFullscreen = true;
        public string displayResolution = "1920x1080";
        public bool continueSkippingAfterChoice = false;
        public float dialogTextSpeed = 1f;
        public float dialogAutoSpeed = 1f;

        public float musicVolume = 1f;
        public float sfxVolume = 1f;
        public float voicesVolume = 1f;
        public bool musicMute = false;
        public bool sfxMute = false;
        public bool voicesMute = false;

        public float historyLogScale = 1f;

        public void Load(){

            var ui = Config_Menu.instance.ui;

            Config_Menu.instance.Set_Display_To_Fullscreen(displayFullscreen);
            ui.Set_Button_Colors(ui.fullScreen, ui.windowed, displayFullscreen);

            int resolutionIndex = 0;

            for(int i = 0; i < ui.resolutions.options.Count; i++){

                string resolution = ui.resolutions.options[i].text;

                if(resolution == displayResolution){

                    resolutionIndex = i;
                    break;

                }

            }

            ui.resolutions.value = resolutionIndex;

            ui.Set_Button_Colors(ui.skippingContinue, ui.skippingStop, continueSkippingAfterChoice);

            ui.textSpeed.value = dialogTextSpeed;
            ui.autoReaderSpeed.value = dialogAutoSpeed;

            //Audio
            ui.musicVolume.value = musicVolume;
            ui.sfxVolume.value = sfxVolume;
            ui.voicesVolume.value = voicesVolume;
            ui.musicMute.sprite = musicMute ? ui.mutedSymbol : ui.unmutedSymbol;
            ui.sfxMute.sprite = sfxMute ? ui.mutedSymbol : ui.unmutedSymbol;
            ui.voicesMute.sprite = voicesMute ? ui.mutedSymbol : ui.unmutedSymbol;

        }

        public void Save(){

            File_Manager.Save(filePath, JsonUtility.ToJson(this), encrypt);

        }
    }

}
>>>>>>> c8d4f422 (Whole Template)
