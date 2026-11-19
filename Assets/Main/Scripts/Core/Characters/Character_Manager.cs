<<<<<<< HEAD
using Dialog;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters{

    public class Character_Manager : MonoBehaviour{
        
        public static Character_Manager instance{get; private set;}
        public Character[] allCharacters => characters.Values.ToArray();
        private Dictionary<string, Character> characters = new Dictionary<string, Character>();
        private Character_Configuration_SO configurations => Dialog_System.instance.configurations.characterConfigurationAsset;

        //Create_Character(characterName as artCharacterName) to use the same 
        //sprites for different characters
        public const string Character_Cast_ID = " as ";
        private const string Character_Name_ID = "<charactername>";
        public string characterRootPathFormat => $"Characters/{Character_Name_ID}";
        public string characterPrefabNameFormat => $"Character_({Character_Name_ID})";
        public string characterPrefabPathFormat => $"{characterRootPathFormat}/{characterPrefabNameFormat}";
        [SerializeField] private RectTransform _characterPanel = null;
        public RectTransform characterPanel => _characterPanel;

        private void Awake(){

            instance = this;

        }

        public Character_Configuration_Data Get_Character_Configuration_Data(string characterName, bool getOriginal = false){

            if(!getOriginal){
                
                Character character = Get_Character(characterName);

                if(character != null){

                    return character.configurationData;

                }

            }
            
            return configurations.Get_Configuration(characterName);

        }

        public Character Get_Character(string characterName, bool createIfNonexistent = false){

            if(characters.ContainsKey(characterName.ToLower())){

                return characters[characterName.ToLower()];

            }else if(createIfNonexistent){

                return Create_Character(characterName);

            }

            return null;

        }

        public bool Has_Character(string characterName) => characters.ContainsKey(characterName.ToLower());

        public Character Create_Character(string characterName, bool reveal = false){

            if(characters.ContainsKey(characterName.ToLower())){

                Debug.LogWarning($"A character called '{characterName}' already exists. Character was not created.");
                return null;

            }

            Character_Info info = Get_Character_Info(characterName);
            Character character = Create_Character_With_Info(info);

            if(info.castingName != info.name){

                character.castingName = info.castingName;

            }

            characters.Add(info.name.ToLower(), character);

            if(reveal){

                character.Show();

            }

            return character;

        }

        private Character_Info Get_Character_Info(string characterName){

            Character_Info result = new Character_Info();
            string[] nameData = characterName.Split(Character_Cast_ID, System.StringSplitOptions.RemoveEmptyEntries);
            result.name = nameData[0];
            result.castingName = nameData.Length > 1 ? nameData[1] : result.name;
            result.configurations = configurations.Get_Configuration(result.castingName);
            result.prefab = Get_Prefab_For_Character(result.castingName);
            result.rootCharacterFolder = Format_Character_Path(characterRootPathFormat, result.castingName);

            return result;

        }

        public GameObject Get_Prefab_For_Character(string characterName){

            string prefabPath = Format_Character_Path(characterPrefabPathFormat, characterName);
            return Resources.Load<GameObject>(prefabPath);

        }

        public string Format_Character_Path(string path, string characterName) => path.Replace(Character_Name_ID, characterName);

        private Character Create_Character_With_Info(Character_Info info){

            Character_Configuration_Data configurationData = info.configurations;

            switch(info.configurations.characterType){

                case Character.Character_Type.Text:
                    return new Text_Character(info.name, configurationData);

                case Character.Character_Type.Sprite:
                case Character.Character_Type.SpriteSheet:
                    return new Sprite_Character(info.name, configurationData, info.prefab, info.rootCharacterFolder);

                default:
                    return null;

            }

        }

        public void Sort_Characters(){

            List<Character> activeCharacters =characters.Values.Where(c => c.root.gameObject.activeInHierarchy && c.isVisible).ToList();
            List<Character> inActiveCharacters =characters.Values.Except(activeCharacters).ToList();

            activeCharacters.Sort((a, b) => a.priority.CompareTo(b.priority));
            activeCharacters.Concat(inActiveCharacters);

            Sort_Characters(activeCharacters);

        }

        public void Sort_Characters(string[] characterNames){

            List<Character> sortedCharacters = new List<Character>();

            sortedCharacters = characterNames
                .Select(name => Get_Character(name))
                .Where(character => character != null)
                .ToList();

            List<Character> remainingCharacters = characters.Values
                .Except(sortedCharacters)
                .OrderBy(character => character.priority)
                .ToList();

            sortedCharacters.Reverse();
            int startingPriority = remainingCharacters.Count > 0 ? remainingCharacters.Max(c => c.priority) : 0;

            for(int i = 0; i < sortedCharacters.Count; i++){

                Character character = sortedCharacters[i];
                character.Set_Priority(startingPriority + i + 1, autoSortCharactersOnUI: false);

            }

            List <Character> allCharacters = remainingCharacters.Concat(sortedCharacters).ToList();
            Sort_Characters(allCharacters);

        }

        private void Sort_Characters(List<Character> charactersSortingOrder){

            int i = 0;

            foreach(Character character in charactersSortingOrder){

                //Debug.Log($"{character.name} priority is {character.priority}");
                character.root.SetSiblingIndex(i++);

            }

        }

        private class Character_Info{

            public string name = "";
            public string castingName = "";
            public string rootCharacterFolder = "";
            public Character_Configuration_Data configurations = null;
            public GameObject prefab = null;

        }

    }

}
=======
using Dialog;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters{

    public class Character_Manager : MonoBehaviour{
        
        public static Character_Manager instance{get; private set;}
        public Character[] allCharacters => characters.Values.ToArray();
        private Dictionary<string, Character> characters = new Dictionary<string, Character>();
        private Character_Configuration_SO configurations => Dialog_System.instance.configurations.characterConfigurationAsset;

        //Create_Character(characterName as artCharacterName) to use the same 
        //sprites for different characters
        public const string Character_Cast_ID = " as ";
        private const string Character_Name_ID = "<charactername>";
        public string characterRootPathFormat => $"Characters/{Character_Name_ID}";
        public string characterPrefabNameFormat => $"Character_({Character_Name_ID})";
        public string characterPrefabPathFormat => $"{characterRootPathFormat}/{characterPrefabNameFormat}";
        [SerializeField] private RectTransform _characterPanel = null;
        public RectTransform characterPanel => _characterPanel;

        private void Awake(){

            instance = this;

        }

        public Character_Configuration_Data Get_Character_Configuration_Data(string characterName, bool getOriginal = false){

            if(!getOriginal){
                
                Character character = Get_Character(characterName);

                if(character != null){

                    return character.configurationData;

                }

            }
            
            return configurations.Get_Configuration(characterName);

        }

        public Character Get_Character(string characterName, bool createIfNonexistent = false){

            if(characters.ContainsKey(characterName.ToLower())){

                return characters[characterName.ToLower()];

            }else if(createIfNonexistent){

                return Create_Character(characterName);

            }

            return null;

        }

        public bool Has_Character(string characterName) => characters.ContainsKey(characterName.ToLower());

        public Character Create_Character(string characterName, bool reveal = false){

            if(characters.ContainsKey(characterName.ToLower())){

                Debug.LogWarning($"A character called '{characterName}' already exists. Character was not created.");
                return null;

            }

            Character_Info info = Get_Character_Info(characterName);
            Character character = Create_Character_With_Info(info);

            if(info.castingName != info.name){

                character.castingName = info.castingName;

            }

            characters.Add(info.name.ToLower(), character);

            if(reveal){

                character.Show();

            }

            return character;

        }

        private Character_Info Get_Character_Info(string characterName){

            Character_Info result = new Character_Info();
            string[] nameData = characterName.Split(Character_Cast_ID, System.StringSplitOptions.RemoveEmptyEntries);
            result.name = nameData[0];
            result.castingName = nameData.Length > 1 ? nameData[1] : result.name;
            result.configurations = configurations.Get_Configuration(result.castingName);
            result.prefab = Get_Prefab_For_Character(result.castingName);
            result.rootCharacterFolder = Format_Character_Path(characterRootPathFormat, result.castingName);

            return result;

        }

        public GameObject Get_Prefab_For_Character(string characterName){

            string prefabPath = Format_Character_Path(characterPrefabPathFormat, characterName);
            return Resources.Load<GameObject>(prefabPath);

        }

        public string Format_Character_Path(string path, string characterName) => path.Replace(Character_Name_ID, characterName);

        private Character Create_Character_With_Info(Character_Info info){

            Character_Configuration_Data configurationData = info.configurations;

            switch(info.configurations.characterType){

                case Character.Character_Type.Text:
                    return new Text_Character(info.name, configurationData);

                case Character.Character_Type.Sprite:
                case Character.Character_Type.SpriteSheet:
                    return new Sprite_Character(info.name, configurationData, info.prefab, info.rootCharacterFolder);

                default:
                    return null;

            }

        }

        public void Sort_Characters(){

            List<Character> activeCharacters =characters.Values.Where(c => c.root.gameObject.activeInHierarchy && c.isVisible).ToList();
            List<Character> inActiveCharacters =characters.Values.Except(activeCharacters).ToList();

            activeCharacters.Sort((a, b) => a.priority.CompareTo(b.priority));
            activeCharacters.Concat(inActiveCharacters);

            Sort_Characters(activeCharacters);

        }

        public void Sort_Characters(string[] characterNames){

            List<Character> sortedCharacters = new List<Character>();

            sortedCharacters = characterNames
                .Select(name => Get_Character(name))
                .Where(character => character != null)
                .ToList();

            List<Character> remainingCharacters = characters.Values
                .Except(sortedCharacters)
                .OrderBy(character => character.priority)
                .ToList();

            sortedCharacters.Reverse();
            int startingPriority = remainingCharacters.Count > 0 ? remainingCharacters.Max(c => c.priority) : 0;

            for(int i = 0; i < sortedCharacters.Count; i++){

                Character character = sortedCharacters[i];
                character.Set_Priority(startingPriority + i + 1, autoSortCharactersOnUI: false);

            }

            List <Character> allCharacters = remainingCharacters.Concat(sortedCharacters).ToList();
            Sort_Characters(allCharacters);

        }

        private void Sort_Characters(List<Character> charactersSortingOrder){

            int i = 0;

            foreach(Character character in charactersSortingOrder){

                //Debug.Log($"{character.name} priority is {character.priority}");
                character.root.SetSiblingIndex(i++);

            }

        }

        private class Character_Info{

            public string name = "";
            public string castingName = "";
            public string rootCharacterFolder = "";
            public Character_Configuration_Data configurations = null;
            public GameObject prefab = null;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
