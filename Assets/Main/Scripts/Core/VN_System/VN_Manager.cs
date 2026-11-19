<<<<<<< HEAD
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace VN{

    public class VN_Manager : MonoBehaviour{

        public static VN_Manager instance { get; private set; }
        [SerializeField] private VN_SO configuration;
        public Camera mainCamera;

        private void Awake(){

            instance = this;
            
            VN_Database_Link_Setup linkSetup = GetComponent<VN_Database_Link_Setup>();
            linkSetup.Setup_External_Links();

            if(VN_Save_Game.activeFile == null){

                VN_Save_Game.activeFile = new VN_Save_Game();

            }
            
        }

        private void Start(){

            Load_Game();
            
        }

        private void Load_Game(){

            if(VN_Save_Game.activeFile.newGame){

                List<string> lines = File_Manager.Read_Text_Asset(configuration.startingFile);
                Conversation start = new Conversation(lines);
                Dialog_System.instance.Say(start);

            }else{

                VN_Save_Game.activeFile.Reapply();

            }

        }

    }

}
=======
using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace VN{

    public class VN_Manager : MonoBehaviour{

        public static VN_Manager instance { get; private set; }
        [SerializeField] private VN_SO configuration;
        public Camera mainCamera;

        private void Awake(){

            instance = this;
            
            VN_Database_Link_Setup linkSetup = GetComponent<VN_Database_Link_Setup>();
            linkSetup.Setup_External_Links();

            if(VN_Save_Game.activeFile == null){

                VN_Save_Game.activeFile = new VN_Save_Game();

            }
            
        }

        private void Start(){

            Load_Game();
            
        }

        private void Load_Game(){

            if(VN_Save_Game.activeFile.newGame){

                List<string> lines = File_Manager.Read_Text_Asset(configuration.startingFile);
                Conversation start = new Conversation(lines);
                Dialog_System.instance.Say(start);

            }else{

                VN_Save_Game.activeFile.Reapply();

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
