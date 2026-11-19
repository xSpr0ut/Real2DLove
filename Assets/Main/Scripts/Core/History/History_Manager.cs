<<<<<<< HEAD
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace History{

    [RequireComponent(typeof(History_Navigation))]
    [RequireComponent(typeof(History_Log_Manager))]

    public class History_Manager : MonoBehaviour{

        public const int historyCacheLimit = 100;

        public static History_Manager instance { get; private set; }
        public List<History_State> history = new List<History_State>();
        private History_Navigation navigation;
        public bool isViewingHistory => navigation.isViewingHistory;
        public History_Log_Manager logManager {get; private set; }

        private void Awake(){

            instance = this;
            navigation = GetComponent<History_Navigation>();
            logManager = GetComponent<History_Log_Manager>();
            
        }
        
        void Start(){

            Dialog_System.instance.onClear += Log_Current_State;
            
        }

        public void Log_Current_State(){

            History_State state = History_State.Capture();
            history.Add(state);
            logManager.Add_Log(state);

            if(history.Count > historyCacheLimit){

                history.RemoveAt(0);

            }

        }

        public void Load_State(History_State state){

            state.Load();

        }

        public void Go_Forward() => navigation.Go_Forward();
        public void Go_Backward() => navigation.Go_Backward();

    }

}
=======
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace History{

    [RequireComponent(typeof(History_Navigation))]
    [RequireComponent(typeof(History_Log_Manager))]

    public class History_Manager : MonoBehaviour{

        public const int historyCacheLimit = 100;

        public static History_Manager instance { get; private set; }
        public List<History_State> history = new List<History_State>();
        private History_Navigation navigation;
        public bool isViewingHistory => navigation.isViewingHistory;
        public History_Log_Manager logManager {get; private set; }

        private void Awake(){

            instance = this;
            navigation = GetComponent<History_Navigation>();
            logManager = GetComponent<History_Log_Manager>();
            
        }
        
        void Start(){

            Dialog_System.instance.onClear += Log_Current_State;
            
        }

        public void Log_Current_State(){

            History_State state = History_State.Capture();
            history.Add(state);
            logManager.Add_Log(state);

            if(history.Count > historyCacheLimit){

                history.RemoveAt(0);

            }

        }

        public void Load_State(History_State state){

            state.Load();

        }

        public void Go_Forward() => navigation.Go_Forward();
        public void Go_Backward() => navigation.Go_Backward();

    }

}
>>>>>>> c8d4f422 (Whole Template)
