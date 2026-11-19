<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Testing{
    public class Test_Characters : MonoBehaviour{

        private Character Create_Character(string name) => Character_Manager.instance.Create_Character(name);
        
        void Start(){

            //Character Omniscient = Character_Manager.instance.Create_Character("Omniscient");
            //Character Oiloaso = Character_Manager.instance.Create_Character("Oiloaso");
            // Character Execute2 = Character_Manager.instance.Create_Character("Execute");
            // Character Nuke = Character_Manager.instance.Create_Character("Nuke");

            StartCoroutine(Test_Animations());

        }

        IEnumerator Test_Text_Characters(){

            Character Nuke = Character_Manager.instance.Create_Character("Nuke");
            Character Omniscient = Character_Manager.instance.Create_Character("Omniscient");

            List<string> lines = new List<string>(){

                "You guys have the worst names.",
                "Shut it.",
                "Not my problem, {wa 1}whatever your name is."

            };

            yield return Nuke.Say(lines);

            Nuke.Set_Name_Color(Color.blue);
            Nuke.Set_Dialog_Color(Color.magenta);

            yield return Nuke.Say(lines);

            Nuke.Reset_Configuration_Data();

            yield return Nuke.Say(lines);

            yield return Omniscient.Say("Bam.{a} Nuke.");

            Debug.Log("Done.");
            
        }

        IEnumerator Test_Sprite_Characters(){

            //Character Omniscient = Character_Manager.instance.Create_Character("Omniscient");
            Character Oiloaso1 = Create_Character("Oi as Oiloaso");
            Character Oiloaso2 = Create_Character("Oil as Oiloaso");
            Character Oiloaso3 = Create_Character("Oilo as Oiloaso");

            Sprite_Character OmniscientLayerTest = Create_Character("Omniscient") as Sprite_Character;

            Oiloaso1.Set_Position(Vector2.zero);
            Oiloaso2.Set_Position(new Vector2(0.5f, 0.5f));
            Oiloaso3.Set_Position(Vector2.one);

            Sprite mainSprite = OmniscientLayerTest.Get_Sprite("TV_On_Light_On");
            Sprite lightSprite = OmniscientLayerTest.Get_Sprite("Light_White");

            OmniscientLayerTest.Set_Sprite(mainSprite, 0);
            OmniscientLayerTest.Set_Sprite(lightSprite, 1);
            
            // yield return Omniscient.Hide();
            // yield return Omniscient.Show();
            // Omniscient.Say("Nuke.");

            yield return Oiloaso1.Show();
            yield return Oiloaso2.Show();
            yield return Oiloaso3.Show();

            yield return Oiloaso1.Move_To_Position(Vector2.one, smooth: true);
            yield return Oiloaso1.Move_To_Position(Vector2.zero, smooth: true);

        }

        IEnumerator Test_Layers(){

            //For spritesheet characters
            //Sprite name = Sprite_Character.Get_Sprite("Characters_ArtName);
            //Sprite_Character.Set_Sprite(name);

            Sprite_Character Omniscient = Create_Character("Omniscient") as Sprite_Character;
            Sprite_Character Oiloaso = Create_Character("Oiloaso") as Sprite_Character;
            Oiloaso.isVisible = true;
            Oiloaso.Set_Position(new Vector2(0.5f, 0));

            Sprite mainSprite = Omniscient.Get_Sprite("TV_On_Light_On");
            Sprite lightSprite = Omniscient.Get_Sprite("Light_White");

            Omniscient.Set_Sprite(mainSprite, 0);
            Omniscient.Set_Sprite(lightSprite, 1);

            yield return new WaitForSeconds(1);

            mainSprite = Omniscient.Get_Sprite("TV_Red_Light_On");
            lightSprite = Omniscient.Get_Sprite("Light_Red");

            Omniscient.Transition_Sprite(lightSprite, 1);
            Omniscient.Transition_Sprite(mainSprite);

            yield return new WaitForSeconds(1);

            mainSprite = Omniscient.Get_Sprite("TV_Off_Light_On");
            lightSprite = Omniscient.Get_Sprite("Light_White");

            Omniscient.Transition_Sprite(mainSprite);
            Omniscient.Transition_Sprite(lightSprite, layer: 1, 0.1f);

            yield return Oiloaso.Move_To_Position(new Vector2(1, 0));

            yield return Oiloaso.Hide();
            
            yield return null;

        }

        IEnumerator Test_Colors(){

            Sprite_Character Omniscient = Create_Character("Omniscient") as Sprite_Character;
            
            Omniscient.Set_Color(Color.red);

            yield return new WaitForSeconds(1);

            Omniscient.layers[0].Set_Color(Color.blue);

            yield return new WaitForSeconds(1);

            yield return Omniscient.Un_Highlight();

            yield return new WaitForSeconds(2);

            yield return Omniscient.Transition_Color(Color.cyan);
            yield return Omniscient.Transition_Color(Color.yellow, speed: 1f);
            yield return Omniscient.Transition_Color(Color.black, 0.5f);

            yield return new WaitForSeconds(2);

            yield return Omniscient.Transition_Color(Color.red);

            yield return new WaitForSeconds(2);

            yield return Omniscient.Highlight();
            
            Omniscient.Say("Nuke.");

            yield return null;

        }

        IEnumerator Test_Animations(){

            Sprite_Character OiloasoTop = Create_Character("OiloasoTop as Oiloaso") as Sprite_Character;
            Sprite_Character Oiloaso = Create_Character("Oiloaso") as Sprite_Character;

            Oiloaso.Set_Position(new Vector2(0.5f, 0));
            OiloasoTop.Set_Position(new Vector2(0.5f, 0));
            OiloasoTop.Set_Color(Color.red, 0.5f);
            Oiloaso.Show();

            yield return new WaitForSeconds(1);

            yield return Oiloaso.Flip();

            yield return Oiloaso.Face_Left();

            yield return Oiloaso.Face_Right();

            yield return new WaitForSeconds(1);

            yield return Oiloaso.Flip(0.2f);

            yield return Oiloaso.Face_Right(immediate: true);

            yield return Oiloaso.Face_Left(0.5f);

            OiloasoTop.Set_Priority(1000);
            OiloasoTop.Flip(immediate: true);
            yield return OiloasoTop.Show();

            yield return new WaitForSeconds(1);

            Oiloaso.Set_Priority(1001);

            yield return new WaitForSeconds(1);

            //Sets OiloasoTop above Oiloaso and both above any other characters
            //regardless of their prior priority setting
            Character_Manager.instance.Sort_Characters(new string[]{"OiloasoTop", "Oiloaso"});

            yield return new WaitForSeconds(1);

            //Should do nothing because it just sorts the characters again
            Character_Manager.instance.Sort_Characters();

            yield return new WaitForSeconds(1);

            Oiloaso.Animate("Hop");
            OiloasoTop.Animate("Shake", true);

            yield return new WaitForSeconds(1);

            OiloasoTop.Animate("Shake", false);

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Testing{
    public class Test_Characters : MonoBehaviour{

        private Character Create_Character(string name) => Character_Manager.instance.Create_Character(name);
        
        void Start(){

            //Character Omniscient = Character_Manager.instance.Create_Character("Omniscient");
            //Character Oiloaso = Character_Manager.instance.Create_Character("Oiloaso");
            // Character Execute2 = Character_Manager.instance.Create_Character("Execute");
            // Character Nuke = Character_Manager.instance.Create_Character("Nuke");

            StartCoroutine(Test_Animations());

        }

        IEnumerator Test_Text_Characters(){

            Character Nuke = Character_Manager.instance.Create_Character("Nuke");
            Character Omniscient = Character_Manager.instance.Create_Character("Omniscient");

            List<string> lines = new List<string>(){

                "You guys have the worst names.",
                "Shut it.",
                "Not my problem, {wa 1}whatever your name is."

            };

            yield return Nuke.Say(lines);

            Nuke.Set_Name_Color(Color.blue);
            Nuke.Set_Dialog_Color(Color.magenta);

            yield return Nuke.Say(lines);

            Nuke.Reset_Configuration_Data();

            yield return Nuke.Say(lines);

            yield return Omniscient.Say("Bam.{a} Nuke.");

            Debug.Log("Done.");
            
        }

        IEnumerator Test_Sprite_Characters(){

            //Character Omniscient = Character_Manager.instance.Create_Character("Omniscient");
            Character Oiloaso1 = Create_Character("Oi as Oiloaso");
            Character Oiloaso2 = Create_Character("Oil as Oiloaso");
            Character Oiloaso3 = Create_Character("Oilo as Oiloaso");

            Sprite_Character OmniscientLayerTest = Create_Character("Omniscient") as Sprite_Character;

            Oiloaso1.Set_Position(Vector2.zero);
            Oiloaso2.Set_Position(new Vector2(0.5f, 0.5f));
            Oiloaso3.Set_Position(Vector2.one);

            Sprite mainSprite = OmniscientLayerTest.Get_Sprite("TV_On_Light_On");
            Sprite lightSprite = OmniscientLayerTest.Get_Sprite("Light_White");

            OmniscientLayerTest.Set_Sprite(mainSprite, 0);
            OmniscientLayerTest.Set_Sprite(lightSprite, 1);
            
            // yield return Omniscient.Hide();
            // yield return Omniscient.Show();
            // Omniscient.Say("Nuke.");

            yield return Oiloaso1.Show();
            yield return Oiloaso2.Show();
            yield return Oiloaso3.Show();

            yield return Oiloaso1.Move_To_Position(Vector2.one, smooth: true);
            yield return Oiloaso1.Move_To_Position(Vector2.zero, smooth: true);

        }

        IEnumerator Test_Layers(){

            //For spritesheet characters
            //Sprite name = Sprite_Character.Get_Sprite("Characters_ArtName);
            //Sprite_Character.Set_Sprite(name);

            Sprite_Character Omniscient = Create_Character("Omniscient") as Sprite_Character;
            Sprite_Character Oiloaso = Create_Character("Oiloaso") as Sprite_Character;
            Oiloaso.isVisible = true;
            Oiloaso.Set_Position(new Vector2(0.5f, 0));

            Sprite mainSprite = Omniscient.Get_Sprite("TV_On_Light_On");
            Sprite lightSprite = Omniscient.Get_Sprite("Light_White");

            Omniscient.Set_Sprite(mainSprite, 0);
            Omniscient.Set_Sprite(lightSprite, 1);

            yield return new WaitForSeconds(1);

            mainSprite = Omniscient.Get_Sprite("TV_Red_Light_On");
            lightSprite = Omniscient.Get_Sprite("Light_Red");

            Omniscient.Transition_Sprite(lightSprite, 1);
            Omniscient.Transition_Sprite(mainSprite);

            yield return new WaitForSeconds(1);

            mainSprite = Omniscient.Get_Sprite("TV_Off_Light_On");
            lightSprite = Omniscient.Get_Sprite("Light_White");

            Omniscient.Transition_Sprite(mainSprite);
            Omniscient.Transition_Sprite(lightSprite, layer: 1, 0.1f);

            yield return Oiloaso.Move_To_Position(new Vector2(1, 0));

            yield return Oiloaso.Hide();
            
            yield return null;

        }

        IEnumerator Test_Colors(){

            Sprite_Character Omniscient = Create_Character("Omniscient") as Sprite_Character;
            
            Omniscient.Set_Color(Color.red);

            yield return new WaitForSeconds(1);

            Omniscient.layers[0].Set_Color(Color.blue);

            yield return new WaitForSeconds(1);

            yield return Omniscient.Un_Highlight();

            yield return new WaitForSeconds(2);

            yield return Omniscient.Transition_Color(Color.cyan);
            yield return Omniscient.Transition_Color(Color.yellow, speed: 1f);
            yield return Omniscient.Transition_Color(Color.black, 0.5f);

            yield return new WaitForSeconds(2);

            yield return Omniscient.Transition_Color(Color.red);

            yield return new WaitForSeconds(2);

            yield return Omniscient.Highlight();
            
            Omniscient.Say("Nuke.");

            yield return null;

        }

        IEnumerator Test_Animations(){

            Sprite_Character OiloasoTop = Create_Character("OiloasoTop as Oiloaso") as Sprite_Character;
            Sprite_Character Oiloaso = Create_Character("Oiloaso") as Sprite_Character;

            Oiloaso.Set_Position(new Vector2(0.5f, 0));
            OiloasoTop.Set_Position(new Vector2(0.5f, 0));
            OiloasoTop.Set_Color(Color.red, 0.5f);
            Oiloaso.Show();

            yield return new WaitForSeconds(1);

            yield return Oiloaso.Flip();

            yield return Oiloaso.Face_Left();

            yield return Oiloaso.Face_Right();

            yield return new WaitForSeconds(1);

            yield return Oiloaso.Flip(0.2f);

            yield return Oiloaso.Face_Right(immediate: true);

            yield return Oiloaso.Face_Left(0.5f);

            OiloasoTop.Set_Priority(1000);
            OiloasoTop.Flip(immediate: true);
            yield return OiloasoTop.Show();

            yield return new WaitForSeconds(1);

            Oiloaso.Set_Priority(1001);

            yield return new WaitForSeconds(1);

            //Sets OiloasoTop above Oiloaso and both above any other characters
            //regardless of their prior priority setting
            Character_Manager.instance.Sort_Characters(new string[]{"OiloasoTop", "Oiloaso"});

            yield return new WaitForSeconds(1);

            //Should do nothing because it just sorts the characters again
            Character_Manager.instance.Sort_Characters();

            yield return new WaitForSeconds(1);

            Oiloaso.Animate("Hop");
            OiloasoTop.Animate("Shake", true);

            yield return new WaitForSeconds(1);

            OiloasoTop.Animate("Shake", false);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
