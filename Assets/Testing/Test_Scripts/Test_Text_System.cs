<<<<<<< HEAD
using Unity.VisualScripting;
using UnityEngine;
using Dialog;

namespace Testing{
    public class Test_Text_System : MonoBehaviour{

        Dialog_System ds;
        Text_System textSystem;

        public Text_System.BuildMethod bm = Text_System.BuildMethod.instant;

        string[] testLines = new string[5]{

            "Mango is best.",
            "Nuke eats rocks.",
            "Hiroshima is too small.",
            "Fuzzy's spirit.",
            "Oi."

        };
    
        void Start(){

            ds = Dialog_System.instance;
            textSystem = new Text_System(ds.dialogContainer.dialogText);
            textSystem.buildMethod = Text_System.BuildMethod.fade;
        
        }

        void Update(){

            if(bm != textSystem.buildMethod){

                textSystem.buildMethod = bm;
                testLines.ToHashSetPooled();

            }

            if(Input.GetKeyDown(KeyCode.S)){

                textSystem.Stop();

            }

            string longLine = "Hola amigos, est-ce que tu connais Pierre Jacques? Il est un cuisiner celebre qui cuisine les repas des fruits de la mer. Non? C'est normale parce que j'ai juste cree Pierre Jacques, il n'existe pas.";
            if(Input.GetKeyDown(KeyCode.Space)){

                if(textSystem.isBuilding){

                    if(!textSystem.speedUp){

                        textSystem.speedUp = true;

                    }else{

                        textSystem.Force_Complete();

                    }

                }else{

                    textSystem.Build(longLine);
                    //textSystem.Build(testLines[Random.Range(0, testLines.Length)]);

                }

            }else if(Input.GetKeyDown(KeyCode.A)){

                textSystem.Append(longLine);
                //textSystem.Append(testLines[Random.Range(0, testLines.Length)]);

            }
        
        }
    }

}
=======
using Unity.VisualScripting;
using UnityEngine;
using Dialog;

namespace Testing{
    public class Test_Text_System : MonoBehaviour{

        Dialog_System ds;
        Text_System textSystem;

        public Text_System.BuildMethod bm = Text_System.BuildMethod.instant;

        string[] testLines = new string[5]{

            "Mango is best.",
            "Nuke eats rocks.",
            "Hiroshima is too small.",
            "Fuzzy's spirit.",
            "Oi."

        };
    
        void Start(){

            ds = Dialog_System.instance;
            textSystem = new Text_System(ds.dialogContainer.dialogText);
            textSystem.buildMethod = Text_System.BuildMethod.fade;
        
        }

        void Update(){

            if(bm != textSystem.buildMethod){

                textSystem.buildMethod = bm;
                testLines.ToHashSetPooled();

            }

            if(Input.GetKeyDown(KeyCode.S)){

                textSystem.Stop();

            }

            string longLine = "Hola amigos, est-ce que tu connais Pierre Jacques? Il est un cuisiner celebre qui cuisine les repas des fruits de la mer. Non? C'est normale parce que j'ai juste cree Pierre Jacques, il n'existe pas.";
            if(Input.GetKeyDown(KeyCode.Space)){

                if(textSystem.isBuilding){

                    if(!textSystem.speedUp){

                        textSystem.speedUp = true;

                    }else{

                        textSystem.Force_Complete();

                    }

                }else{

                    textSystem.Build(longLine);
                    //textSystem.Build(testLines[Random.Range(0, testLines.Length)]);

                }

            }else if(Input.GetKeyDown(KeyCode.A)){

                textSystem.Append(longLine);
                //textSystem.Append(testLines[Random.Range(0, testLines.Length)]);

            }
        
        }
    }

}
>>>>>>> c8d4f422 (Whole Template)
