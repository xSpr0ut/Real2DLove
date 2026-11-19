<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using History;
using UnityEngine;

namespace Testing{

    public class Test_History : MonoBehaviour{
        
        public History_State state = new History_State();

        void Update(){

            if(Input.GetKeyDown(KeyCode.H)){

                state = History_State.Capture();

            }

            if(Input.GetKeyDown(KeyCode.J)){

                state.Load();

            }
            
        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using History;
using UnityEngine;

namespace Testing{

    public class Test_History : MonoBehaviour{
        
        public History_State state = new History_State();

        void Update(){

            if(Input.GetKeyDown(KeyCode.H)){

                state = History_State.Capture();

            }

            if(Input.GetKeyDown(KeyCode.J)){

                state.Load();

            }
            
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
