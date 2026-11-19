<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.Logical_Lines.Choices{

    public class Choice_Panel : MonoBehaviour{

        public static Choice_Panel instance { get; private set; }

        private const float buttonMinWidth = 50;
        private const float buttonMaxWidth = 1000;
        private const float buttonSizePadding = 25;

        private const float buttonHeightPerLine = 50;
        private const float buttonHeightPadding = 20;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private VerticalLayoutGroup buttonLayout;

        private Canvas_Group_Controller cg = null;
        private List<Choice_Button> buttons = new List<Choice_Button>();
        public Choice_Panel_Decision lastDecision { get; private set; } = null;

        public bool isWaitingOnPlayerChoice { get; private set; } = false; 

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            cg = new Canvas_Group_Controller(this, canvasGroup);

            cg.alpha = 0;
            cg.Set_Interactable_State(false);
            
        }

        public void Show(string question, string[] choices){

            lastDecision = new Choice_Panel_Decision(question, choices);

            isWaitingOnPlayerChoice = true;

            cg.Show();
            cg.Set_Interactable_State(active: true);

            titleText.text = question;
            StartCoroutine(Generate_Choices(choices));
        }

        private IEnumerator Generate_Choices(string[] choices){

            float maxWidth = 0;

            for(int i = 0; i < choices.Length; i++){

                Choice_Button choiceButton;
                
                if(i < buttons.Count){

                    choiceButton = buttons[i];

                }else{

                    GameObject newButtonObject = Instantiate(choiceButtonPrefab, buttonLayout.transform);
                    newButtonObject.SetActive(true);

                    Button newButton = newButtonObject.GetComponent<Button>();
                    TextMeshProUGUI newTitle = newButton.GetComponentInChildren<TextMeshProUGUI>();
                    LayoutElement newLayout = newButton.GetComponent<LayoutElement>();

                    choiceButton = new Choice_Button { button = newButton, layout = newLayout, title = newTitle };

                    buttons.Add(choiceButton);

                }

                choiceButton.button.onClick.RemoveAllListeners();
                int buttonIndex = i;
                choiceButton.button.onClick.AddListener(() => Accept_Answer(buttonIndex));
                choiceButton.title.text = choices[i];

                float buttonWidth = Mathf.Clamp(buttonSizePadding + choiceButton.title.preferredWidth, buttonMinWidth, buttonMaxWidth);
                maxWidth = Mathf.Max(maxWidth, buttonWidth);

            }

            foreach(var button in buttons){

                button.layout.preferredWidth = maxWidth;

            }

            for(int i = 0; i < buttons.Count; i++){

                bool show = i < choices.Length;
                buttons[i].button.gameObject.SetActive(show);

            }

            yield return new WaitForEndOfFrame();

            foreach(var button in buttons){

                int lines = button.title.textInfo.lineCount;
                button.layout.preferredHeight = buttonHeightPadding + (buttonHeightPerLine * lines);

            }

        }

        public void Hide(){

            cg.Hide();
            cg.Set_Interactable_State(false);

        }

        private void Accept_Answer(int index){

            if(index < 0 || index > lastDecision.choices.Length - 1){

                return;

            }

            lastDecision.answerIndex = index;
            isWaitingOnPlayerChoice = false;
            Hide();

        }

        public class Choice_Panel_Decision{

            public string question = string.Empty;
            public int answerIndex = -1;
            public string[] choices = new string[0];

            public Choice_Panel_Decision(string question, string[] choices){

                this.question = question;
                this.choices = choices;
                answerIndex = -1;

            }

        }

        private struct Choice_Button{

            public Button button;
            public TextMeshProUGUI title;
            public LayoutElement layout;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.Logical_Lines.Choices{

    public class Choice_Panel : MonoBehaviour{

        public static Choice_Panel instance { get; private set; }

        private const float buttonMinWidth = 50;
        private const float buttonMaxWidth = 1000;
        private const float buttonSizePadding = 25;

        private const float buttonHeightPerLine = 50;
        private const float buttonHeightPadding = 20;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private VerticalLayoutGroup buttonLayout;

        private Canvas_Group_Controller cg = null;
        private List<Choice_Button> buttons = new List<Choice_Button>();
        public Choice_Panel_Decision lastDecision { get; private set; } = null;

        public bool isWaitingOnPlayerChoice { get; private set; } = false; 

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            cg = new Canvas_Group_Controller(this, canvasGroup);

            cg.alpha = 0;
            cg.Set_Interactable_State(false);
            
        }

        public void Show(string question, string[] choices){

            lastDecision = new Choice_Panel_Decision(question, choices);

            isWaitingOnPlayerChoice = true;

            cg.Show();
            cg.Set_Interactable_State(active: true);

            titleText.text = question;
            StartCoroutine(Generate_Choices(choices));
        }

        private IEnumerator Generate_Choices(string[] choices){

            float maxWidth = 0;

            for(int i = 0; i < choices.Length; i++){

                Choice_Button choiceButton;
                
                if(i < buttons.Count){

                    choiceButton = buttons[i];

                }else{

                    GameObject newButtonObject = Instantiate(choiceButtonPrefab, buttonLayout.transform);
                    newButtonObject.SetActive(true);

                    Button newButton = newButtonObject.GetComponent<Button>();
                    TextMeshProUGUI newTitle = newButton.GetComponentInChildren<TextMeshProUGUI>();
                    LayoutElement newLayout = newButton.GetComponent<LayoutElement>();

                    choiceButton = new Choice_Button { button = newButton, layout = newLayout, title = newTitle };

                    buttons.Add(choiceButton);

                }

                choiceButton.button.onClick.RemoveAllListeners();
                int buttonIndex = i;
                choiceButton.button.onClick.AddListener(() => Accept_Answer(buttonIndex));
                choiceButton.title.text = choices[i];

                float buttonWidth = Mathf.Clamp(buttonSizePadding + choiceButton.title.preferredWidth, buttonMinWidth, buttonMaxWidth);
                maxWidth = Mathf.Max(maxWidth, buttonWidth);

            }

            foreach(var button in buttons){

                button.layout.preferredWidth = maxWidth;

            }

            for(int i = 0; i < buttons.Count; i++){

                bool show = i < choices.Length;
                buttons[i].button.gameObject.SetActive(show);

            }

            yield return new WaitForEndOfFrame();

            foreach(var button in buttons){

                int lines = button.title.textInfo.lineCount;
                button.layout.preferredHeight = buttonHeightPadding + (buttonHeightPerLine * lines);

            }

        }

        public void Hide(){

            cg.Hide();
            cg.Set_Interactable_State(false);

        }

        private void Accept_Answer(int index){

            if(index < 0 || index > lastDecision.choices.Length - 1){

                return;

            }

            lastDecision.answerIndex = index;
            isWaitingOnPlayerChoice = false;
            Hide();

        }

        public class Choice_Panel_Decision{

            public string question = string.Empty;
            public int answerIndex = -1;
            public string[] choices = new string[0];

            public Choice_Panel_Decision(string question, string[] choices){

                this.question = question;
                this.choices = choices;
                answerIndex = -1;

            }

        }

        private struct Choice_Button{

            public Button button;
            public TextMeshProUGUI title;
            public LayoutElement layout;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
