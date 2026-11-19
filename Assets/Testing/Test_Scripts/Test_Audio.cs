<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Audio;
using Characters;
using UnityEngine;

namespace Testing{

    public class Test_Audio : MonoBehaviour{

        void Start(){

            StartCoroutine(Run());
            
        }

        Character Create_Character(string name) => Character_Manager.instance.Create_Character(name);

        IEnumerator Run(){

            Sprite_Character Oiloaso = Create_Character("Oiloaso") as Sprite_Character;
            Oiloaso.Show();

            yield return new WaitForSeconds(1);

            Audio_Manager.instance.Play_Sound_Effect("Audio/SFX/RadioStatic", loop: true);

            yield return Oiloaso.Say("Off with the radio.");

            Audio_Manager.instance.Stop_Sound_Effect("RadioStatic");
            Audio_Manager.instance.Play_Voice("Audio/Voices/exclamation");

            yield return Oiloaso.Say("Yey.");

            Audio_Manager.instance.Play_Track("Audio/Music/Calm", volumeCap: 0.5f, channel: 0);

            yield return Oiloaso.Say("Boring music.");

            Audio_Manager.instance.Play_Track("Audio/Ambience/RainyMood", volumeCap: 0.8f, channel: 1, pitch: 0.7f);

            yield return Oiloaso.Say("No more music.");

            Audio_Manager.instance.Stop_Track(0);

        }

        IEnumerator Run_Channels(){

            Audio_Channel channel = new Audio_Channel(1);
            yield return null;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Audio;
using Characters;
using UnityEngine;

namespace Testing{

    public class Test_Audio : MonoBehaviour{

        void Start(){

            StartCoroutine(Run());
            
        }

        Character Create_Character(string name) => Character_Manager.instance.Create_Character(name);

        IEnumerator Run(){

            Sprite_Character Oiloaso = Create_Character("Oiloaso") as Sprite_Character;
            Oiloaso.Show();

            yield return new WaitForSeconds(1);

            Audio_Manager.instance.Play_Sound_Effect("Audio/SFX/RadioStatic", loop: true);

            yield return Oiloaso.Say("Off with the radio.");

            Audio_Manager.instance.Stop_Sound_Effect("RadioStatic");
            Audio_Manager.instance.Play_Voice("Audio/Voices/exclamation");

            yield return Oiloaso.Say("Yey.");

            Audio_Manager.instance.Play_Track("Audio/Music/Calm", volumeCap: 0.5f, channel: 0);

            yield return Oiloaso.Say("Boring music.");

            Audio_Manager.instance.Play_Track("Audio/Ambience/RainyMood", volumeCap: 0.8f, channel: 1, pitch: 0.7f);

            yield return Oiloaso.Say("No more music.");

            Audio_Manager.instance.Stop_Track(0);

        }

        IEnumerator Run_Channels(){

            Audio_Channel channel = new Audio_Channel(1);
            yield return null;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
