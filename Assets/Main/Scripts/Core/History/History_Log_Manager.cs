<<<<<<< HEAD
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace History{

    public class History_Log_Manager : MonoBehaviour{
        
        private const float logStartingHeight = 2f;
        private const float logHeightPerLine = 2f;
        private const float logDefaultHeight = 1f;
        private const float textDefaultScale = 1f;

        private const string nameTextName = "Name_Text";
        private const string dialogTextName = "Dialog_Text";

        private float logScaling = 1f;

        [SerializeField] private Animator animator;
        [SerializeField] private GameObject logPrefab;

        History_Manager manager => History_Manager.instance;
        private List<History_Log> logs = new List<History_Log>();

        public bool isOpen { get; private set; } = false;

        [SerializeField] private Slider logScaleSlider;

        private float textScaling => logScaling * 3f;

        public void Open(){

            if(isOpen){

                return;

            }

            animator.Play("Open");

            isOpen = true;
           
        }

        public void Close(){

            if(!isOpen){

                return;

            }

            animator.Play("Close");

            isOpen = false;
          
        }

        public void Add_Log(History_State state){

            if(logs.Count >= History_Manager.historyCacheLimit){

                DestroyImmediate(logs[0].container);
                logs.RemoveAt(0);

            }

            Create_Log(state);

        }

        public void Create_Log(History_State state){

            History_Log log = new History_Log();

            log.container = Instantiate(logPrefab, logPrefab.transform.parent);
            log.container.SetActive(true);

            log.nameText = log.container.transform.Find(nameTextName).GetComponent<TextMeshProUGUI>();
            log.dialogText = log.container.transform.Find(dialogTextName).GetComponent<TextMeshProUGUI>();

            if(state.dialog.currentSpeaker == string.Empty){

                log.nameText.text = string.Empty;

            }else{

                log.nameText.text = state.dialog.currentSpeaker;
                log.nameText.font = History_Cache.Load_Font(state.dialog.speakerFont);
                log.nameText.color = state.dialog.speakerNameColor;
                log.nameFontSize = textDefaultScale * state.dialog.speakerScale;
                log.nameText.fontSize = log.nameFontSize + textScaling;
                
            }

            log.dialogText.text = state.dialog.currentDialog;
            log.dialogText.font = History_Cache.Load_Font(state.dialog.dialogFont);
            log.dialogText.color = state.dialog.dialogColor;
            log.dialogFontSize = textDefaultScale * state.dialog.dialogScale;
            log.dialogText.fontSize = log.dialogFontSize + textScaling;

            Fit_Log_To_Text(log);

            logs.Add(log);

        }

        private void Fit_Log_To_Text(History_Log log){

            RectTransform rect = log.dialogText.GetComponent<RectTransform>();
            ContentSizeFitter textCSF = log.dialogText.GetComponent<ContentSizeFitter>();

            textCSF.SetLayoutVertical();

            LayoutElement logLayout = log.container.GetComponent<LayoutElement>();
            
            float height = rect.rect.height;
            float perc = height / logDefaultHeight;
            float extraScale = logHeightPerLine * perc - logHeightPerLine;
            float scale = logStartingHeight + extraScale;

            logLayout.preferredHeight = scale + textScaling;
            logLayout.preferredHeight += 2f * logScaling;

        }

        public void Set_Log_Scaling(){

            logScaling = logScaleSlider.value;

            foreach(History_Log log in logs){

                log.nameText.fontSize = log.nameFontSize + textScaling;
                log.dialogText.fontSize = log.dialogFontSize + textScaling;

                Fit_Log_To_Text(log);

            }

        }

        public void Clear(){

            for(int i = 0; i < logs.Count; i++){

                DestroyImmediate(logs[i].container);

            }

            logs.Clear();

        }

        public void Rebuild(){

            foreach(var state in manager.history){

                Create_Log(state);

            }

        }

    }

}
=======
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace History{

    public class History_Log_Manager : MonoBehaviour{
        
        private const float logStartingHeight = 2f;
        private const float logHeightPerLine = 2f;
        private const float logDefaultHeight = 1f;
        private const float textDefaultScale = 1f;

        private const string nameTextName = "Name_Text";
        private const string dialogTextName = "Dialog_Text";

        private float logScaling = 1f;

        [SerializeField] private Animator animator;
        [SerializeField] private GameObject logPrefab;

        History_Manager manager => History_Manager.instance;
        private List<History_Log> logs = new List<History_Log>();

        public bool isOpen { get; private set; } = false;

        [SerializeField] private Slider logScaleSlider;

        private float textScaling => logScaling * 3f;

        public void Open(){

            if(isOpen){

                return;

            }

            animator.Play("Open");

            isOpen = true;
           
        }

        public void Close(){

            if(!isOpen){

                return;

            }

            animator.Play("Close");

            isOpen = false;
          
        }

        public void Add_Log(History_State state){

            if(logs.Count >= History_Manager.historyCacheLimit){

                DestroyImmediate(logs[0].container);
                logs.RemoveAt(0);

            }

            Create_Log(state);

        }

        public void Create_Log(History_State state){

            History_Log log = new History_Log();

            log.container = Instantiate(logPrefab, logPrefab.transform.parent);
            log.container.SetActive(true);

            log.nameText = log.container.transform.Find(nameTextName).GetComponent<TextMeshProUGUI>();
            log.dialogText = log.container.transform.Find(dialogTextName).GetComponent<TextMeshProUGUI>();

            if(state.dialog.currentSpeaker == string.Empty){

                log.nameText.text = string.Empty;

            }else{

                log.nameText.text = state.dialog.currentSpeaker;
                log.nameText.font = History_Cache.Load_Font(state.dialog.speakerFont);
                log.nameText.color = state.dialog.speakerNameColor;
                log.nameFontSize = textDefaultScale * state.dialog.speakerScale;
                log.nameText.fontSize = log.nameFontSize + textScaling;
                
            }

            log.dialogText.text = state.dialog.currentDialog;
            log.dialogText.font = History_Cache.Load_Font(state.dialog.dialogFont);
            log.dialogText.color = state.dialog.dialogColor;
            log.dialogFontSize = textDefaultScale * state.dialog.dialogScale;
            log.dialogText.fontSize = log.dialogFontSize + textScaling;

            Fit_Log_To_Text(log);

            logs.Add(log);

        }

        private void Fit_Log_To_Text(History_Log log){

            RectTransform rect = log.dialogText.GetComponent<RectTransform>();
            ContentSizeFitter textCSF = log.dialogText.GetComponent<ContentSizeFitter>();

            textCSF.SetLayoutVertical();

            LayoutElement logLayout = log.container.GetComponent<LayoutElement>();
            
            float height = rect.rect.height;
            float perc = height / logDefaultHeight;
            float extraScale = logHeightPerLine * perc - logHeightPerLine;
            float scale = logStartingHeight + extraScale;

            logLayout.preferredHeight = scale + textScaling;
            logLayout.preferredHeight += 2f * logScaling;

        }

        public void Set_Log_Scaling(){

            logScaling = logScaleSlider.value;

            foreach(History_Log log in logs){

                log.nameText.fontSize = log.nameFontSize + textScaling;
                log.dialogText.fontSize = log.dialogFontSize + textScaling;

                Fit_Log_To_Text(log);

            }

        }

        public void Clear(){

            for(int i = 0; i < logs.Count; i++){

                DestroyImmediate(logs[i].container);

            }

            logs.Clear();

        }

        public void Rebuild(){

            foreach(var state in manager.history){

                Create_Log(state);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
