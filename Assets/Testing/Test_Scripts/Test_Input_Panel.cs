<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Characters;
using Dialog.Logical_Lines.Input;
using UnityEngine;

namespace Testing{

    public class Test_Input_Panel : MonoBehaviour{

        public Input_Panel inputPanel;
        
        void Start(){

            StartCoroutine(Run());
            
        }

        IEnumerator Run(){

            Character Oiloaso = Character_Manager.instance.Create_Character("Olioaso");
            Oiloaso.Show();

            yield return Oiloaso.Say("I'm a ghost named what?");

            inputPanel.Show("Write your name.");

            while(inputPanel.isWaitingOnPlayerInput){

                yield return null;

            }

            string characterName = inputPanel.lastInput;

            yield return Oiloaso.Say($"I'm {characterName}.");

        }
        
    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Characters;
using Dialog.Logical_Lines.Input;
using UnityEngine;

namespace Testing{

    public class Test_Input_Panel : MonoBehaviour{

        public Input_Panel inputPanel;
        
        void Start(){

            StartCoroutine(Run());
            
        }

        IEnumerator Run(){

            Character Oiloaso = Character_Manager.instance.Create_Character("Olioaso");
            Oiloaso.Show();

            yield return Oiloaso.Say("I'm a ghost named what?");

            inputPanel.Show("Write your name.");

            while(inputPanel.isWaitingOnPlayerInput){

                yield return null;

            }

            string characterName = inputPanel.lastInput;

            yield return Oiloaso.Say($"I'm {characterName}.");

        }
        
    }

}
>>>>>>> c8d4f422 (Whole Template)
