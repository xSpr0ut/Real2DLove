<<<<<<< HEAD
using System.Collections.Generic;
using Dialog;
using TMPro;
using UnityEngine;

namespace History{

    public class History_Navigation : MonoBehaviour{

        public int progress = 0;

        [SerializeField] private TextMeshProUGUI statusText;

        History_Manager manager => History_Manager.instance;
        List<History_State> history => manager.history;

        public bool isViewingHistory = false;

        History_State cachedState = null;
        private bool isOnCachedState = false;

        public bool canNavigate => !Dialog_System.instance.conversationManager.isOnLogicalLine;
        
        public void Go_Forward(){

            if(!isViewingHistory || !canNavigate){

                return;

            }

            History_State state = null;

            if(progress < history.Count - 1){

                progress++;
                state = history[progress];

            }else{

                isOnCachedState = true;
                state = cachedState;

            }

            state.Load();

            if(isOnCachedState){

                isViewingHistory = false;
                Dialog_System.instance.next_On_Player_Input -= Go_Forward;
                statusText.text = "";
                Dialog_System.instance.On_Stop_Viewing_History();

            }else{

                Update_Status_Text();

            }

        }

        public void Go_Backward(){

            if(history.Count == 0 || (progress == 0 && isViewingHistory) || !canNavigate){

                return;

            }

            progress = isViewingHistory ? progress - 1 : history.Count - 1;

            if(!isViewingHistory){

                isViewingHistory = true;
                isOnCachedState = false;
                cachedState = History_State.Capture();

                Dialog_System.instance.next_On_Player_Input += Go_Forward;
                Dialog_System.instance.On_Start_Viewing_History();

            }

            History_State state = history[progress];
            state.Load();
            Update_Status_Text();

        }

        private void Update_Status_Text(){

            statusText.text = $"{history.Count - progress}/{history.Count}";

        }

    }

}
=======
using System.Collections.Generic;
using Dialog;
using TMPro;
using UnityEngine;

namespace History{

    public class History_Navigation : MonoBehaviour{

        public int progress = 0;

        [SerializeField] private TextMeshProUGUI statusText;

        History_Manager manager => History_Manager.instance;
        List<History_State> history => manager.history;

        public bool isViewingHistory = false;

        History_State cachedState = null;
        private bool isOnCachedState = false;

        public bool canNavigate => !Dialog_System.instance.conversationManager.isOnLogicalLine;
        
        public void Go_Forward(){

            if(!isViewingHistory || !canNavigate){

                return;

            }

            History_State state = null;

            if(progress < history.Count - 1){

                progress++;
                state = history[progress];

            }else{

                isOnCachedState = true;
                state = cachedState;

            }

            state.Load();

            if(isOnCachedState){

                isViewingHistory = false;
                Dialog_System.instance.next_On_Player_Input -= Go_Forward;
                statusText.text = "";
                Dialog_System.instance.On_Stop_Viewing_History();

            }else{

                Update_Status_Text();

            }

        }

        public void Go_Backward(){

            if(history.Count == 0 || (progress == 0 && isViewingHistory) || !canNavigate){

                return;

            }

            progress = isViewingHistory ? progress - 1 : history.Count - 1;

            if(!isViewingHistory){

                isViewingHistory = true;
                isOnCachedState = false;
                cachedState = History_State.Capture();

                Dialog_System.instance.next_On_Player_Input += Go_Forward;
                Dialog_System.instance.On_Start_Viewing_History();

            }

            History_State state = history[progress];
            state.Load();
            Update_Status_Text();

        }

        private void Update_Status_Text(){

            statusText.text = $"{history.Count - progress}/{history.Count}";

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
