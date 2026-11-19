<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphics{

    public class Graphic_Panel_Manager : MonoBehaviour{
        
        public static Graphic_Panel_Manager instance { get; private set; }
        [field:SerializeField] public Graphic_Panel[] allPanels { get; private set; }

        private void Awake(){

            instance = this;
            
        }

        public Graphic_Panel Get_Panel(string name){

            name = name.ToLower();

            foreach(var panel in allPanels){

                if(panel.panelName.ToLower() == name){

                    return panel;

                }

            }

            return null;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphics{

    public class Graphic_Panel_Manager : MonoBehaviour{
        
        public static Graphic_Panel_Manager instance { get; private set; }
        [field:SerializeField] public Graphic_Panel[] allPanels { get; private set; }

        private void Awake(){

            instance = this;
            
        }

        public Graphic_Panel Get_Panel(string name){

            name = name.ToLower();

            foreach(var panel in allPanels){

                if(panel.panelName.ToLower() == name){

                    return panel;

                }

            }

            return null;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
