<<<<<<< HEAD
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.Logical_Lines.Input{

    public class Input_Panel : MonoBehaviour{

        public static Input_Panel instance { get; private set; } = null;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private TMP_InputField inputField;
        private Canvas_Group_Controller cg;
        public string lastInput { get; private set; } = string.Empty;
        public bool isWaitingOnPlayerInput { get; private set; }

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            cg = new Canvas_Group_Controller(this, canvasGroup);
            cg.alpha = 0;
            cg.Set_Interactable_State(active: false);
            acceptButton.gameObject.SetActive(false);

            inputField.onValueChanged.AddListener(On_Input_Changed);
            acceptButton.onClick.AddListener(On_Accept_Input);
            
        }

        public void Show(string title){

            titleText.text = title;
            inputField.text = string.Empty;
            cg.Show();
            cg.Set_Interactable_State(active: true);
            isWaitingOnPlayerInput = true;

        }

        public void Hide(){

            cg.Hide();
            cg.Set_Interactable_State(active: false);
            isWaitingOnPlayerInput = false;
            
        }

        public void On_Accept_Input(){

            if(inputField.text == string.Empty){

                return;

            }   

            lastInput = inputField.text;
            Hide();

        }

        public void On_Input_Changed(string value){

            acceptButton.gameObject.SetActive(Has_Valid_Text());

        }

        private bool Has_Valid_Text(){

            return inputField.text != string.Empty;

        }

    }

}
=======
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.Logical_Lines.Input{

    public class Input_Panel : MonoBehaviour{

        public static Input_Panel instance { get; private set; } = null;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private TMP_InputField inputField;
        private Canvas_Group_Controller cg;
        public string lastInput { get; private set; } = string.Empty;
        public bool isWaitingOnPlayerInput { get; private set; }

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            cg = new Canvas_Group_Controller(this, canvasGroup);
            cg.alpha = 0;
            cg.Set_Interactable_State(active: false);
            acceptButton.gameObject.SetActive(false);

            inputField.onValueChanged.AddListener(On_Input_Changed);
            acceptButton.onClick.AddListener(On_Accept_Input);
            
        }

        public void Show(string title){

            titleText.text = title;
            inputField.text = string.Empty;
            cg.Show();
            cg.Set_Interactable_State(active: true);
            isWaitingOnPlayerInput = true;

        }

        public void Hide(){

            cg.Hide();
            cg.Set_Interactable_State(active: false);
            isWaitingOnPlayerInput = false;
            
        }

        public void On_Accept_Input(){

            if(inputField.text == string.Empty){

                return;

            }   

            lastInput = inputField.text;
            Hide();

        }

        public void On_Input_Changed(string value){

            acceptButton.gameObject.SetActive(Has_Valid_Text());

        }

        private bool Has_Valid_Text(){

            return inputField.text != string.Empty;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
