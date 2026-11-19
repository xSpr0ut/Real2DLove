<<<<<<< HEAD
using System.Collections;
using Characters;
using Dialog;
using Graphics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Testing{

    public class Graphic_Layer_Test : MonoBehaviour{
        
        void Start(){

            StartCoroutine(Run_Layers());
            
        }

        IEnumerator Run(){

            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel("Background");
            Graphic_Layer layer = panel.Get_Layer(0, true);

            yield return new WaitForSeconds(1);
            
            Texture blendTexture = Resources.Load<Texture>("Graphics/Transitions/blackHole");
            layer.Set_Texture("Graphics/Background_Images/BG Bath", blendingTexture: blendTexture);

            yield return new WaitForSeconds(1);

            layer.Set_Video("Graphics/Background_Videos/Nebula", transitionSpeed: 0.5f, useAudio: false, blendingTexture: blendTexture);

            yield return new WaitForSeconds(3);

            layer.currentGraphic.Fade_Out();

            yield return new WaitForSeconds(1);
            Debug.Log(layer.currentGraphic);

        }

        IEnumerator Run_Layers(){

            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel("Background");
            Graphic_Layer layer0 = panel.Get_Layer(0, true);
            Graphic_Layer layer1 = panel.Get_Layer(1, true);

            layer0.Set_Texture("Graphics/Background_Images/BG Bath");

            layer1.Set_Video("Graphics/Background_Videos/Nebula");
            
            yield return new WaitForSeconds(1);

            Graphic_Panel cinematic = Graphic_Panel_Manager.instance.Get_Panel("Cinematic");
            Graphic_Layer cinLayer0 = cinematic.Get_Layer(0, true);

            Character Oiloaso = Character_Manager.instance.Create_Character("Oiloaso", true);

            yield return Oiloaso.Say("Wow, a moving background.");

            cinLayer0.Set_Texture("Graphics/Gallery/Space1a");

            yield return Dialog_System.instance.Say("Me", "Now a space battle.");

            cinLayer0.Clear();

            yield return new WaitForSeconds(1);

            panel.Clear();

        }

    }

}
=======
using System.Collections;
using Characters;
using Dialog;
using Graphics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Testing{

    public class Graphic_Layer_Test : MonoBehaviour{
        
        void Start(){

            StartCoroutine(Run_Layers());
            
        }

        IEnumerator Run(){

            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel("Background");
            Graphic_Layer layer = panel.Get_Layer(0, true);

            yield return new WaitForSeconds(1);
            
            Texture blendTexture = Resources.Load<Texture>("Graphics/Transitions/blackHole");
            layer.Set_Texture("Graphics/Background_Images/BG Bath", blendingTexture: blendTexture);

            yield return new WaitForSeconds(1);

            layer.Set_Video("Graphics/Background_Videos/Nebula", transitionSpeed: 0.5f, useAudio: false, blendingTexture: blendTexture);

            yield return new WaitForSeconds(3);

            layer.currentGraphic.Fade_Out();

            yield return new WaitForSeconds(1);
            Debug.Log(layer.currentGraphic);

        }

        IEnumerator Run_Layers(){

            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel("Background");
            Graphic_Layer layer0 = panel.Get_Layer(0, true);
            Graphic_Layer layer1 = panel.Get_Layer(1, true);

            layer0.Set_Texture("Graphics/Background_Images/BG Bath");

            layer1.Set_Video("Graphics/Background_Videos/Nebula");
            
            yield return new WaitForSeconds(1);

            Graphic_Panel cinematic = Graphic_Panel_Manager.instance.Get_Panel("Cinematic");
            Graphic_Layer cinLayer0 = cinematic.Get_Layer(0, true);

            Character Oiloaso = Character_Manager.instance.Create_Character("Oiloaso", true);

            yield return Oiloaso.Say("Wow, a moving background.");

            cinLayer0.Set_Texture("Graphics/Gallery/Space1a");

            yield return Dialog_System.instance.Say("Me", "Now a space battle.");

            cinLayer0.Clear();

            yield return new WaitForSeconds(1);

            panel.Clear();

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
