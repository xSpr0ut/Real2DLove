<<<<<<< HEAD
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu{

    public class UI_Confirmation_Menu : MonoBehaviour{

        public static UI_Confirmation_Menu instance {get; private set;}

        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private LayoutGroup choiceLayoutGroup;
        [SerializeField] private GameObject buttonPrefab;
        public bool isOpen = false;

        private GameObject[] activeOptions = new GameObject[0];

        private void Awake(){

            instance = this;
            
        }

        public void Show(string title, params Confirmation_Button[] options){

            if(options.Length == 0){

                Debug.LogError("Why ain't there any options?");
                return;

            }
            

            this.title.text = title;
            Create_Option_Buttons(options);

            animator.Play("Open");

            isOpen = true;

        }

        public void Hide(){

            animator.Play("Close");
            isOpen = false;

        }

        private void Create_Option_Buttons(Confirmation_Button[] options){

            foreach(GameObject g in activeOptions){

                DestroyImmediate(g);

            }

            activeOptions = new GameObject[options.Length];

            for(int i = 0; i < options.Length; i++){

                Confirmation_Button option = options[i];
                GameObject obj = Instantiate(buttonPrefab, choiceLayoutGroup.transform);
                obj.SetActive(true);

                Button button = obj.GetComponent<Button>();

                if(option.action != null){

                    button.onClick.AddListener(() => option.action.Invoke());

                }

                if(option.autoCloseOnQuit){

                    button.onClick.AddListener(() => Hide());

                }

                TextMeshProUGUI txt = obj.GetComponentInChildren<TextMeshProUGUI>();
                txt.text = option.title;

                activeOptions[i] = obj;

            }   

        }

        public struct Confirmation_Button{

            public string title;
            public Action action;
            public bool autoCloseOnQuit;

            public Confirmation_Button(string title, Action action, bool autoCloseOnClick = true){

                this.title = title;
                this.action = action;
                this.autoCloseOnQuit = autoCloseOnClick;

            }

        }

    }

}
=======
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu{

    public class UI_Confirmation_Menu : MonoBehaviour{

        public static UI_Confirmation_Menu instance {get; private set;}

        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private LayoutGroup choiceLayoutGroup;
        [SerializeField] private GameObject buttonPrefab;

        private GameObject[] activeOptions = new GameObject[0];

        private void Awake(){

            instance = this;
            
        }

        public void Show(string title, params Confirmation_Button[] options){

            if(options.Length == 0){

                Debug.LogError("Why ain't there any options?");
                return;

            }

            this.title.text = title;
            Create_Option_Buttons(options);

            animator.Play("Open");

        }

        public void Hide(){

            animator.Play("Close");

        }

        private void Create_Option_Buttons(Confirmation_Button[] options){

            foreach(GameObject g in activeOptions){

                DestroyImmediate(g);

            }

            activeOptions = new GameObject[options.Length];

            for(int i = 0; i < options.Length; i++){

                Confirmation_Button option = options[i];
                GameObject obj = Instantiate(buttonPrefab, choiceLayoutGroup.transform);
                obj.SetActive(true);

                Button button = obj.GetComponent<Button>();

                if(option.action != null){

                    button.onClick.AddListener(() => option.action.Invoke());

                }

                if(option.autoCloseOnQuit){

                    button.onClick.AddListener(() => Hide());

                }

                TextMeshProUGUI txt = obj.GetComponentInChildren<TextMeshProUGUI>();
                txt.text = option.title;

                activeOptions[i] = obj;

            }   

        }

        public struct Confirmation_Button{

            public string title;
            public Action action;
            public bool autoCloseOnQuit;

            public Confirmation_Button(string title, Action action, bool autoCloseOnClick = true){

                this.title = title;
                this.action = action;
                this.autoCloseOnQuit = autoCloseOnClick;

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
