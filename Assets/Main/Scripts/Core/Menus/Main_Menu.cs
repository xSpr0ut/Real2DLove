<<<<<<< HEAD
using System.Collections;
using Audio;
using Dialog;
using Menu.Config;
using UnityEngine;
using VN;

namespace Menu{

    public class Main_Menu : MonoBehaviour{

        private const string sceneName = "Test_Scene";
        public static Main_Menu instance {get; private set;}

        public AudioClip menuMusic;
        public CanvasGroup mainPanel;
        private Canvas_Group_Controller mainCG;

        private UI_Confirmation_Menu uiChoiceMenu => UI_Confirmation_Menu.instance;

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            mainCG = new Canvas_Group_Controller(this, mainPanel);

            Audio_Manager.instance.Stop_All_Sound_Effects();
            Audio_Manager.instance.Stop_All_Tracks();
            Audio_Manager.instance.Play_Track(menuMusic, channel: 0, startingVolume: 1);
            
        }

        public void Click_Start_New_Game(){

            uiChoiceMenu.Show("Start a new game?", new UI_Confirmation_Menu.Confirmation_Button("Yup", Start_New_Game), new UI_Confirmation_Menu.Confirmation_Button("Nah", null));

        }

        public void Load_Game(VN_Save_Game file){

            VN_Save_Game.activeFile = file;
            StartCoroutine(Starting_Game());

        }

        private void Start_New_Game(){

            VN_Save_Game.activeFile = new VN_Save_Game();
            StartCoroutine(Starting_Game());

        }

        private IEnumerator Starting_Game(){

            mainCG.Hide(speed: 0.3f);
            Audio_Manager.instance.Stop_Track(0);

            while(mainCG.isVisible){

                yield return null;

            }

            VN_Configuration.activeConfig.Save();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        }

    }

}
=======
using System.Collections;
using Audio;
using Dialog;
using Menu.Config;
using UnityEngine;
using VN;

namespace Menu{

    public class Main_Menu : MonoBehaviour{

        private const string sceneName = "Test_Scene";
        public static Main_Menu instance {get; private set;}

        public AudioClip menuMusic;
        public CanvasGroup mainPanel;
        private Canvas_Group_Controller mainCG;

        private UI_Confirmation_Menu uiChoiceMenu => UI_Confirmation_Menu.instance;

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            mainCG = new Canvas_Group_Controller(this, mainPanel);

            Audio_Manager.instance.Stop_All_Sound_Effects();
            Audio_Manager.instance.Stop_All_Tracks();
            Audio_Manager.instance.Play_Track(menuMusic, channel: 0, startingVolume: 1);
            
        }

        public void Click_Start_New_Game(){

            uiChoiceMenu.Show("Start a new game?", new UI_Confirmation_Menu.Confirmation_Button("Yup", Start_New_Game), new UI_Confirmation_Menu.Confirmation_Button("Nah", null));

        }

        public void Load_Game(VN_Save_Game file){

            VN_Save_Game.activeFile = file;
            StartCoroutine(Starting_Game());

        }

        private void Start_New_Game(){

            VN_Save_Game.activeFile = new VN_Save_Game();
            StartCoroutine(Starting_Game());

        }

        private IEnumerator Starting_Game(){

            mainCG.Hide(speed: 0.3f);
            Audio_Manager.instance.Stop_Track(0);

            while(mainCG.isVisible){

                yield return null;

            }

            VN_Configuration.activeConfig.Save();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
