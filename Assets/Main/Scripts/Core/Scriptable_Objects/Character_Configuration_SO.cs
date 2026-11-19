<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters{

    [CreateAssetMenu(fileName = "Character Configuration Asset", menuName = "Dialog System/Character Configuration Asset")]

    public class Character_Configuration_SO : ScriptableObject{

        public Character_Configuration_Data[] characters;

        public Character_Configuration_Data Get_Configuration(string characterName, bool safe = true){

            characterName = characterName.ToLower();

            for(int i = 0; i < characters.Length; i++){

                Character_Configuration_Data data = characters[i];

                if(string.Equals(characterName, data.name.ToLower()) || string.Equals(characterName, data.alias.ToLower())){

                    return safe ? data.Copy() : data;

                }

            }

            return Character_Configuration_Data.Default_Data;

        }
        
    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters{

    [CreateAssetMenu(fileName = "Character Configuration Asset", menuName = "Dialog System/Character Configuration Asset")]

    public class Character_Configuration_SO : ScriptableObject{

        public Character_Configuration_Data[] characters;

        public Character_Configuration_Data Get_Configuration(string characterName, bool safe = true){

            characterName = characterName.ToLower();

            for(int i = 0; i < characters.Length; i++){

                Character_Configuration_Data data = characters[i];

                if(string.Equals(characterName, data.name.ToLower()) || string.Equals(characterName, data.alias.ToLower())){

                    return safe ? data.Copy() : data;

                }

            }

            return Character_Configuration_Data.Default_Data;

        }
        
    }

}
>>>>>>> c8d4f422 (Whole Template)
