<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Dialog;
using Dialog.Logical_Lines;
using Dialog.Logical_Lines.Choices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Testing{

    public class Test_Choices : MonoBehaviour{
        
        Choice_Panel panel;
        public int intv = 0;

        void Start(){

            panel = Choice_Panel.instance;

            StartCoroutine(Run_Variables());
            
        }

        IEnumerator Run(){

            string[]choices = new string[]{

                "Nuke.",
                "Hiroshima.",
                "Truffle",
                "Marinade",
                "Flamethrower",
                "The fuck do I care about some seafood?"

            };

            panel.Show("What's your favorite fish?", choices);

            while(panel.isWaitingOnPlayerChoice){

                yield return null;

            }

            var decision = panel.lastDecision;

            Debug.Log($"Your favorite fish is #{decision.answerIndex}, {decision.choices[decision.answerIndex]}?");

        }

        IEnumerator Run_Conversation_Queue(){

            List<string> lines = new List<string>(){

                "Line 1 of original conversation",
                "Line 2 of original conversation",
                "Line 3 of original conversation"

            };

            yield return Dialog_System.instance.Say(lines);

            Dialog_System.instance.Hide();

        }

        void Update(){

            List<string> lines = new    ();
            Conversation conversation = null;

            if(Input.GetKeyDown(KeyCode.Q)){

                lines = new List<string>(){

                    "An enqueued conversation.",
                    "Bam!"

                };
                
                conversation = new Conversation(lines);
                Dialog_System.instance.conversationManager.Enqueue(conversation);

            }

            if(Input.GetKeyDown(KeyCode.W)){

                lines = new List<string>(){

                    "New convo",
                    "Die <mainChar>!"

                };

                conversation = new Conversation(lines);
                Dialog_System.instance.conversationManager.Enqueue_Priority(conversation);

            }

            if(Input.GetKeyDown(KeyCode.Space)){

                Variable_Store.Print_All_Variables();

            }

            if(Input.GetKeyDown(KeyCode.Z)){

                string variable = "Num1";
                Variable_Store.Try_Get_Value(variable, out object v);
                Variable_Store.Try_Set_Value(variable, (int)v + 1);
                
            }

            if(Input.GetKeyDown(KeyCode.X)){

                string variable = "Nuke.Color";
                Variable_Store.Try_Get_Value(variable, out object str_calico);
                Variable_Store.Try_Set_Value(variable, (string)str_calico + "Milk Cow");
                
            }

            if(Input.GetKeyDown(KeyCode.D)){

                Variable_Store.Try_Get_Value("Links.intv", out object linked);
                Variable_Store.Try_Set_Value("Links.intv", (int)linked + 2);
                
            }

            if(Input.GetKeyDown(KeyCode.F)){

                Variable_Store.Remove_Variable("Links.intv");
                Variable_Store.Remove_Variable("Nuke.Color");
                
            }

            if(Input.GetKeyDown(KeyCode.G)){

                Variable_Store.Remove_All_Variables();
                
            }
            
        }

        IEnumerator Run_Variables(){

            Variable_Store.Create_Database("Links");
            Variable_Store.Create_Variable("Links.intv", intv, () => intv, value => intv = value);

            Variable_Store.Create_Database("Nuke");
            Variable_Store.Create_Database("Hiroshima");
            Variable_Store.Create_Database("Truffle");

            Variable_Store.Create_Variable("Num1", 1);
            Variable_Store.Create_Variable("Nuke.Size", 4.5f);
            Variable_Store.Create_Variable("Nuke.Color", "Calico");
            Variable_Store.Create_Variable("Hiroshima.IsCute", true);

            Variable_Store.Print_All_Databases();
            Variable_Store.Print_All_Variables();

            yield return null;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Dialog;
using Dialog.Logical_Lines;
using Dialog.Logical_Lines.Choices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Testing{

    public class Test_Choices : MonoBehaviour{
        
        Choice_Panel panel;
        public int intv = 0;

        void Start(){

            panel = Choice_Panel.instance;

            StartCoroutine(Run_Variables());
            
        }

        IEnumerator Run(){

            string[]choices = new string[]{

                "Nuke.",
                "Hiroshima.",
                "Truffle",
                "Marinade",
                "Flamethrower",
                "The fuck do I care about some seafood?"

            };

            panel.Show("What's your favorite fish?", choices);

            while(panel.isWaitingOnPlayerChoice){

                yield return null;

            }

            var decision = panel.lastDecision;

            Debug.Log($"Your favorite fish is #{decision.answerIndex}, {decision.choices[decision.answerIndex]}?");

        }

        IEnumerator Run_Conversation_Queue(){

            List<string> lines = new List<string>(){

                "Line 1 of original conversation",
                "Line 2 of original conversation",
                "Line 3 of original conversation"

            };

            yield return Dialog_System.instance.Say(lines);

            Dialog_System.instance.Hide();

        }

        void Update(){

            List<string> lines = new    ();
            Conversation conversation = null;

            if(Input.GetKeyDown(KeyCode.Q)){

                lines = new List<string>(){

                    "An enqueued conversation.",
                    "Bam!"

                };
                
                conversation = new Conversation(lines);
                Dialog_System.instance.conversationManager.Enqueue(conversation);

            }

            if(Input.GetKeyDown(KeyCode.W)){

                lines = new List<string>(){

                    "New convo",
                    "Die <mainChar>!"

                };

                conversation = new Conversation(lines);
                Dialog_System.instance.conversationManager.Enqueue_Priority(conversation);

            }

            if(Input.GetKeyDown(KeyCode.Space)){

                Variable_Store.Print_All_Variables();

            }

            if(Input.GetKeyDown(KeyCode.Z)){

                string variable = "Num1";
                Variable_Store.Try_Get_Value(variable, out object v);
                Variable_Store.Try_Set_Value(variable, (int)v + 1);
                
            }

            if(Input.GetKeyDown(KeyCode.X)){

                string variable = "Nuke.Color";
                Variable_Store.Try_Get_Value(variable, out object str_calico);
                Variable_Store.Try_Set_Value(variable, (string)str_calico + "Milk Cow");
                
            }

            if(Input.GetKeyDown(KeyCode.D)){

                Variable_Store.Try_Get_Value("Links.intv", out object linked);
                Variable_Store.Try_Set_Value("Links.intv", (int)linked + 2);
                
            }

            if(Input.GetKeyDown(KeyCode.F)){

                Variable_Store.Remove_Variable("Links.intv");
                Variable_Store.Remove_Variable("Nuke.Color");
                
            }

            if(Input.GetKeyDown(KeyCode.G)){

                Variable_Store.Remove_All_Variables();
                
            }
            
        }

        IEnumerator Run_Variables(){

            Variable_Store.Create_Database("Links");
            Variable_Store.Create_Variable("Links.intv", intv, () => intv, value => intv = value);

            Variable_Store.Create_Database("Nuke");
            Variable_Store.Create_Database("Hiroshima");
            Variable_Store.Create_Database("Truffle");

            Variable_Store.Create_Variable("Num1", 1);
            Variable_Store.Create_Variable("Nuke.Size", 4.5f);
            Variable_Store.Create_Variable("Nuke.Color", "Calico");
            Variable_Store.Create_Variable("Hiroshima.IsCute", true);

            Variable_Store.Print_All_Databases();
            Variable_Store.Print_All_Variables();

            yield return null;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
