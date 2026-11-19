<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace Dialog{

    public class Auto_Reader : MonoBehaviour{
        
        private const int defaultCharactersReadPerSecond = 18;
        private const float readTimePadding = 0.5f;
        private const float maxReadTime = 99f;
        private const float minReadTime = 1f;
        private const string statusTextAuto = "Auto";
        private const string statusTextSkip = "Skipping";

        private Conversation_Manager conversationManager;
        private Text_System textSystem => conversationManager.textSystem;

        public bool skip { get; set; } = false;
        public float speed { get; set; } = 1f;
        private Coroutine Co_Running = null;
        public bool isOn => Co_Running != null;

        [SerializeField] private TextMeshProUGUI statusText;
        [HideInInspector] public bool allowToggle = true;

        public void Initialize(Conversation_Manager conversationManager){
          
          this.conversationManager = conversationManager;

          statusText.text = string.Empty;

        }

        public void Enable(){

            if(isOn){

                return;

            }

            Co_Running = StartCoroutine(Auto_Read());

        }

        public void Disable(){

            if(!isOn){

                return;

            }

            StopCoroutine(Co_Running);
            skip = false;
            Co_Running = null;
            statusText.text = string.Empty;
            
        }

        private IEnumerator Auto_Read(){

            if(!conversationManager.isRunning){

                Disable();
                yield break;

            }

            if(!textSystem.isBuilding && textSystem.currentText != string.Empty){

                Dialog_System.instance.Next_On_System_Input();

            }

            while(conversationManager.isRunning){

                //Reading and waiting
                if(!skip){

                    while(!textSystem.isBuilding && textSystem.buildMethod != Text_System.BuildMethod.instant && !conversationManager.isWaitingOnAutoTimer){

                        yield return null;

                    }

                    float timeStarted = Time.time;

                    while(textSystem.isBuilding || conversationManager.isWaitingOnAutoTimer){

                        yield return null;

                    }

                    float timeToRead = Mathf.Clamp((float)textSystem.tmpro.textInfo.characterCount / defaultCharactersReadPerSecond, minReadTime, maxReadTime);
                    timeToRead = Mathf.Clamp(timeToRead - (Time.time - timeStarted), minReadTime, maxReadTime);
                    timeToRead = (timeToRead / speed) + readTimePadding;

                    yield return new WaitForSeconds(timeToRead);

                }else{

                    textSystem.Force_Complete();
                    yield return new WaitForSeconds(0.05f);

                }

                Dialog_System.instance.Next_On_System_Input();

            }

            Disable();

        }

        public void Toggle_Auto(){

            if(!allowToggle){

                return;

            }

            skip = false;

            if(skip){

                Enable();

            }else{

                if(!isOn){

                    Enable();

                }else{

                    Disable();

                }

            }

            if(isOn){

                statusText.text = statusTextAuto;

            }

            skip = false;

        }

        public void Toggle_Skip(){

            if(!allowToggle){

                return;

            }

            skip = true;

            if(!skip){

                Enable();

            }else{

                if(!isOn){

                    Enable();

                }else{

                    Disable();

                }

            }

            if(isOn){

                statusText.text = statusTextSkip;

            }

            skip = true;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace Dialog{

    public class Auto_Reader : MonoBehaviour{
        
        private const int defaultCharactersReadPerSecond = 18;
        private const float readTimePadding = 0.5f;
        private const float maxReadTime = 99f;
        private const float minReadTime = 1f;
        private const string statusTextAuto = "Auto";
        private const string statusTextSkip = "Skipping";

        private Conversation_Manager conversationManager;
        private Text_System textSystem => conversationManager.textSystem;

        public bool skip { get; set; } = false;
        public float speed { get; set; } = 1f;
        private Coroutine Co_Running = null;
        public bool isOn => Co_Running != null;

        [SerializeField] private TextMeshProUGUI statusText;
        [HideInInspector] public bool allowToggle = true;

        public void Initialize(Conversation_Manager conversationManager){
          
          this.conversationManager = conversationManager;

          statusText.text = string.Empty;

        }

        public void Enable(){

            if(isOn){

                return;

            }

            Co_Running = StartCoroutine(Auto_Read());

        }

        public void Disable(){

            if(!isOn){

                return;

            }

            StopCoroutine(Co_Running);
            skip = false;
            Co_Running = null;
            statusText.text = string.Empty;
            
        }

        private IEnumerator Auto_Read(){

            if(!conversationManager.isRunning){

                Disable();
                yield break;

            }

            if(!textSystem.isBuilding && textSystem.currentText != string.Empty){

                Dialog_System.instance.Next_On_System_Input();

            }

            while(conversationManager.isRunning){

                //Reading and waiting
                if(!skip){

                    while(!textSystem.isBuilding && textSystem.buildMethod != Text_System.BuildMethod.instant && !conversationManager.isWaitingOnAutoTimer){

                        yield return null;

                    }

                    float timeStarted = Time.time;

                    while(textSystem.isBuilding || conversationManager.isWaitingOnAutoTimer){

                        yield return null;

                    }

                    float timeToRead = Mathf.Clamp((float)textSystem.tmpro.textInfo.characterCount / defaultCharactersReadPerSecond, minReadTime, maxReadTime);
                    timeToRead = Mathf.Clamp(timeToRead - (Time.time - timeStarted), minReadTime, maxReadTime);
                    timeToRead = (timeToRead / speed) + readTimePadding;

                    yield return new WaitForSeconds(timeToRead);

                }else{

                    textSystem.Force_Complete();
                    yield return new WaitForSeconds(0.05f);

                }

                Dialog_System.instance.Next_On_System_Input();

            }

            Disable();

        }

        public void Toggle_Auto(){

            if(!allowToggle){

                return;

            }

            skip = false;

            if(skip){

                Enable();

            }else{

                if(!isOn){

                    Enable();

                }else{

                    Disable();

                }

            }

            if(isOn){

                statusText.text = statusTextAuto;

            }

            skip = false;

        }

        public void Toggle_Skip(){

            if(!allowToggle){

                return;

            }

            skip = true;

            if(!skip){

                Enable();

            }else{

                if(!isOn){

                    Enable();

                }else{

                    Disable();

                }

            }

            if(isOn){

                statusText.text = statusTextSkip;

            }

            skip = true;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
