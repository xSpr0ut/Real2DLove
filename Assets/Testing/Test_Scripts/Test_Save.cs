<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Menu.Config;
using UnityEngine;
using VN;

namespace Testing{

    public class Test_Save : MonoBehaviour{

        public VN_Save_Game save;

        void Start(){

            VN_Save_Game.activeFile = new VN_Save_Game();
            
        }

        void Update(){

            if(Input.GetKeyDown(KeyCode.A)){

                VN_Configuration.activeConfig.Save();
                VN_Save_Game.activeFile.Save();

            }

            if(Input.GetKeyDown(KeyCode.S)){

                try{

                    VN_Save_Game save = VN_Save_Game.Load($"{File_Paths.gameSaves}1{VN_Save_Game.fileType}", true);

                }catch(System.Exception e){

                    Debug.LogError($"DATA CORRUPTED EFWDKSDGOSQ jk :) Got a {e} error.");

                }

            }
            
        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Menu.Config;
using UnityEngine;
using VN;

namespace Testing{

    public class Test_Save : MonoBehaviour{

        public VN_Save_Game save;

        void Start(){

            VN_Save_Game.activeFile = new VN_Save_Game();
            
        }

        void Update(){

            if(Input.GetKeyDown(KeyCode.A)){

                VN_Configuration.activeConfig.Save();
                VN_Save_Game.activeFile.Save();

            }

            if(Input.GetKeyDown(KeyCode.S)){

                try{

                    VN_Save_Game save = VN_Save_Game.Load($"{File_Paths.gameSaves}1{VN_Save_Game.fileType}", true);

                }catch(System.Exception e){

                    Debug.LogError($"DATA CORRUPTED EFWDKSDGOSQ jk :) Got a {e} error.");

                }

            }
            
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
