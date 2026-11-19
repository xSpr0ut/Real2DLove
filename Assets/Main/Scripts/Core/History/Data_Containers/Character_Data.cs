<<<<<<< HEAD
using System.Collections.Generic;
using Characters;
using UnityEngine;
using static History.Character_Data.Animation_Data;

namespace History{

    [System.Serializable]

    public class Character_Data{
        
        public string characterName;
        public string castingName;
        public string displayName;
        public bool enabled;
        public Color color;
        public int priority;
        public bool isHighlighted;
        public bool isFacingLeft;
        public Vector2 position;
        public Character_Configuration_Cache characterConfiguration;

        public string animationJSON;
        public string dataJSON;

        [System.Serializable]

        public class Character_Configuration_Cache{

            public string name;
            public string alias;
            public Character.Character_Type characterType;
            public Color nameColor;
            public Color dialogColor;
            public string nameFont;
            public string dialogFont;
            public float nameFontScale = 1f;
            public float dialogFontScale = 1f;

            public Vector2 dialogBoxVector;
            public Vector2 dialogBoxScale;

            public Character_Configuration_Cache(Character_Configuration_Data reference){

                name = reference.name;
                alias = reference.alias;
                characterType = reference.characterType;

                nameColor = reference.nameColor;
                dialogColor = reference.dialogColor;
                
                nameFont = File_Paths.resourcesFont + reference.nameFont.name;
                dialogFont = File_Paths.resourcesFont + reference.dialogFont.name;

                nameFontScale = reference.nameFontSize;
                dialogFontScale = reference.dialogFontSize;

                dialogBoxVector = reference.dialogBoxVector;
                dialogBoxScale = reference.dialogBoxScale;

            }

        }

        public static List<Character_Data> Capture(){

            List<Character_Data> characters = new List<Character_Data>();

            foreach(var character in Character_Manager.instance.allCharacters){

                if(!character.isVisible){

                    continue;

                }

                Character_Data entry = new Character_Data();
                entry.characterName = character.name;
                entry.castingName = character.castingName;
                entry.displayName = character.displayName;
                entry.enabled = character.isVisible;
                entry.color = character.color;
                entry.priority = character.priority;
                entry.isHighlighted = character.highlighted;
                entry.isFacingLeft = character.isFacingLeft;
                entry.position = character.targetPosition;
                entry.characterConfiguration = new Character_Configuration_Cache(character.configurationData);
                entry.animationJSON = Get_Animation_Data(character);

                Sprite_Data spriteData = new Sprite_Data();
                spriteData.layers = new List<Sprite_Data.Layer_Data>();
                Sprite_Character spriteCharacter = character as Sprite_Character;

                foreach(var layer in spriteCharacter.layers){

                    var layerData = new Sprite_Data.Layer_Data();
                    layerData.color = layer.renderer.color;
                    layerData.spriteName = layer.renderer.sprite.name;
                    spriteData.layers.Add(layerData);

                }

                entry.dataJSON = JsonUtility.ToJson(spriteData);

                characters.Add(entry);

            }

            return characters;

        }

        public static void Apply(List<Character_Data> data){

            List<string> cache = new List<string>();

            foreach(Character_Data characterData in data){

                Character character = null;

                if(characterData.castingName == string.Empty){

                    character = Character_Manager.instance.Get_Character(characterData.characterName, createIfNonexistent: true);

                }else{

                    character = Character_Manager.instance.Get_Character(characterData.characterName, createIfNonexistent: false);

                    if(character == null){

                        string castingName = $"{characterData.characterName}{Character_Manager.Character_Cast_ID}{characterData.castingName}";
                        character = Character_Manager.instance.Create_Character(castingName);

                    }

                }

                character.displayName = characterData.displayName;
                character.Set_Color(characterData.color);

                if(characterData.isHighlighted){

                    character.Highlight(immediate: true);

                }else{

                    character.Un_Highlight(immediate: true);

                }

                character.Set_Priority(characterData.priority);

                if(characterData.isFacingLeft){

                    character.Face_Left(immediate: true);

                }else{

                    character.Face_Right(immediate: true);

                }

                character.Set_Position(characterData.position);
                character.isVisible = characterData.enabled;

                Animation_Data animationData = JsonUtility.FromJson<Animation_Data>(characterData.animationJSON);
                Apply_Animation_Data(character, animationData);

                Sprite_Data spriteData = JsonUtility.FromJson<Sprite_Data>(characterData.dataJSON);
                Sprite_Character spriteCharacter = character as Sprite_Character;

                for(int i = 0; i < spriteData.layers.Count; i++){

                    var layer = spriteData.layers[i];

                    if(spriteCharacter.layers[i].renderer.sprite != null && spriteCharacter.layers[i].renderer.sprite.name != layer.spriteName){

                        Sprite sprite = spriteCharacter.Get_Sprite(layer.spriteName);

                        if(sprite != null){

                            spriteCharacter.Set_Sprite(sprite, i);

                        }else{

                            Debug.LogWarning($"I can't load sprite {layer.spriteName} from history states.");

                        }

                    }

                }

                cache.Add(character.name);

            }

            foreach(Character character in Character_Manager.instance.allCharacters){

                if(!cache.Contains(character.name)){

                    character.isVisible = false;

                }

            }

        }

        public static string Get_Animation_Data(Character character){

            Animator animator = character.animator;
            Animation_Data data = new Animation_Data();

            foreach(var parameter in animator.parameters){

                if(parameter.type == AnimatorControllerParameterType.Trigger){

                    continue;

                }

                Animation_Parameter pData = new Animation_Parameter { name = parameter.name };

                switch(parameter.type){

                    case AnimatorControllerParameterType.Bool:
                        pData.type = "Bool";
                        pData.value = animator.GetBool(parameter.name).ToString();
                        break;
                    case AnimatorControllerParameterType.Float:
                        pData.type = "Float";
                        pData.value = animator.GetFloat(parameter.name).ToString();
                        break;
                    case AnimatorControllerParameterType.Int:
                        pData.type = "Int";
                        pData.value = animator.GetInteger(parameter.name).ToString();
                        break;

                }

                data.parameters.Add(pData);

            }

            return JsonUtility.ToJson(data);

        }

        private static void Apply_Animation_Data(Character character, Animation_Data data){

            Animator animator = character.animator;

            foreach(var parameter in data.parameters){

                switch(parameter.type){

                    case "Bool":

                        animator.SetBool(parameter.name, bool.Parse(parameter.value));
                        break;

                    case "Float":

                        animator.SetFloat(parameter.name, float.Parse(parameter.value));
                        break;

                    case "Int":

                        animator.SetInteger(parameter.name, int.Parse(parameter.value));
                        break;

                }

            }

            animator.SetTrigger(Character.animationRefreshTrigger);

        }

        [System.Serializable]

        public class Animation_Data{

            public List<Animation_Parameter> parameters = new List<Animation_Parameter>();

            [System.Serializable]

            public class Animation_Parameter{

                public string name;
                public string type;
                public string value;

            }

        }

        [System.Serializable]

        public class Sprite_Data{

            public List<Layer_Data> layers;

            [System.Serializable]

            public class Layer_Data{

                public string spriteName;
                public Color color;

            }

        }

    }

}
=======
using System.Collections.Generic;
using Characters;
using UnityEngine;
using static History.Character_Data.Animation_Data;

namespace History{

    [System.Serializable]

    public class Character_Data{
        
        public string characterName;
        public string castingName;
        public string displayName;
        public bool enabled;
        public Color color;
        public int priority;
        public bool isHighlighted;
        public bool isFacingLeft;
        public Vector2 position;
        public Character_Configuration_Cache characterConfiguration;

        public string animationJSON;
        public string dataJSON;

        [System.Serializable]

        public class Character_Configuration_Cache{

            public string name;
            public string alias;
            public Character.Character_Type characterType;
            public Color nameColor;
            public Color dialogColor;
            public string nameFont;
            public string dialogFont;
            public float nameFontScale = 1f;
            public float dialogFontScale = 1f;

            public Vector2 dialogBoxVector;
            public Vector2 dialogBoxScale;

            public Character_Configuration_Cache(Character_Configuration_Data reference){

                name = reference.name;
                alias = reference.alias;
                characterType = reference.characterType;

                nameColor = reference.nameColor;
                dialogColor = reference.dialogColor;
                
                nameFont = File_Paths.resourcesFont + reference.nameFont.name;
                dialogFont = File_Paths.resourcesFont + reference.dialogFont.name;

                nameFontScale = reference.nameFontSize;
                dialogFontScale = reference.dialogFontSize;

                dialogBoxVector = reference.dialogBoxVector;
                dialogBoxScale = reference.dialogBoxScale;

            }

        }

        public static List<Character_Data> Capture(){

            List<Character_Data> characters = new List<Character_Data>();

            foreach(var character in Character_Manager.instance.allCharacters){

                if(!character.isVisible){

                    continue;

                }

                Character_Data entry = new Character_Data();
                entry.characterName = character.name;
                entry.castingName = character.castingName;
                entry.displayName = character.displayName;
                entry.enabled = character.isVisible;
                entry.color = character.color;
                entry.priority = character.priority;
                entry.isHighlighted = character.highlighted;
                entry.isFacingLeft = character.isFacingLeft;
                entry.position = character.targetPosition;
                entry.characterConfiguration = new Character_Configuration_Cache(character.configurationData);
                entry.animationJSON = Get_Animation_Data(character);

                Sprite_Data spriteData = new Sprite_Data();
                spriteData.layers = new List<Sprite_Data.Layer_Data>();
                Sprite_Character spriteCharacter = character as Sprite_Character;

                foreach(var layer in spriteCharacter.layers){

                    var layerData = new Sprite_Data.Layer_Data();
                    layerData.color = layer.renderer.color;
                    layerData.spriteName = layer.renderer.sprite.name;
                    spriteData.layers.Add(layerData);

                }

                entry.dataJSON = JsonUtility.ToJson(spriteData);

                characters.Add(entry);

            }

            return characters;

        }

        public static void Apply(List<Character_Data> data){

            List<string> cache = new List<string>();

            foreach(Character_Data characterData in data){

                Character character = null;

                if(characterData.castingName == string.Empty){

                    character = Character_Manager.instance.Get_Character(characterData.characterName, createIfNonexistent: true);

                }else{

                    character = Character_Manager.instance.Get_Character(characterData.characterName, createIfNonexistent: false);

                    if(character == null){

                        string castingName = $"{characterData.characterName}{Character_Manager.Character_Cast_ID}{characterData.castingName}";
                        character = Character_Manager.instance.Create_Character(castingName);

                    }

                }

                character.displayName = characterData.displayName;
                character.Set_Color(characterData.color);

                if(characterData.isHighlighted){

                    character.Highlight(immediate: true);

                }else{

                    character.Un_Highlight(immediate: true);

                }

                character.Set_Priority(characterData.priority);

                if(characterData.isFacingLeft){

                    character.Face_Left(immediate: true);

                }else{

                    character.Face_Right(immediate: true);

                }

                character.Set_Position(characterData.position);
                character.isVisible = characterData.enabled;

                Animation_Data animationData = JsonUtility.FromJson<Animation_Data>(characterData.animationJSON);
                Apply_Animation_Data(character, animationData);

                Sprite_Data spriteData = JsonUtility.FromJson<Sprite_Data>(characterData.dataJSON);
                Sprite_Character spriteCharacter = character as Sprite_Character;

                for(int i = 0; i < spriteData.layers.Count; i++){

                    var layer = spriteData.layers[i];

                    if(spriteCharacter.layers[i].renderer.sprite != null && spriteCharacter.layers[i].renderer.sprite.name != layer.spriteName){

                        Sprite sprite = spriteCharacter.Get_Sprite(layer.spriteName);

                        if(sprite != null){

                            spriteCharacter.Set_Sprite(sprite, i);

                        }else{

                            Debug.LogWarning($"I can't load sprite {layer.spriteName} from history states.");

                        }

                    }

                }

                cache.Add(character.name);

            }

            foreach(Character character in Character_Manager.instance.allCharacters){

                if(!cache.Contains(character.name)){

                    character.isVisible = false;

                }

            }

        }

        public static string Get_Animation_Data(Character character){

            Animator animator = character.animator;
            Animation_Data data = new Animation_Data();

            foreach(var parameter in animator.parameters){

                if(parameter.type == AnimatorControllerParameterType.Trigger){

                    continue;

                }

                Animation_Parameter pData = new Animation_Parameter { name = parameter.name };

                switch(parameter.type){

                    case AnimatorControllerParameterType.Bool:
                        pData.type = "Bool";
                        pData.value = animator.GetBool(parameter.name).ToString();
                        break;
                    case AnimatorControllerParameterType.Float:
                        pData.type = "Float";
                        pData.value = animator.GetFloat(parameter.name).ToString();
                        break;
                    case AnimatorControllerParameterType.Int:
                        pData.type = "Int";
                        pData.value = animator.GetInteger(parameter.name).ToString();
                        break;

                }

                data.parameters.Add(pData);

            }

            return JsonUtility.ToJson(data);

        }

        private static void Apply_Animation_Data(Character character, Animation_Data data){

            Animator animator = character.animator;

            foreach(var parameter in data.parameters){

                switch(parameter.type){

                    case "Bool":

                        animator.SetBool(parameter.name, bool.Parse(parameter.value));
                        break;

                    case "Float":

                        animator.SetFloat(parameter.name, float.Parse(parameter.value));
                        break;

                    case "Int":

                        animator.SetInteger(parameter.name, int.Parse(parameter.value));
                        break;

                }

            }

            animator.SetTrigger(Character.animationRefreshTrigger);

        }

        [System.Serializable]

        public class Animation_Data{

            public List<Animation_Parameter> parameters = new List<Animation_Parameter>();

            [System.Serializable]

            public class Animation_Parameter{

                public string name;
                public string type;
                public string value;

            }

        }

        [System.Serializable]

        public class Sprite_Data{

            public List<Layer_Data> layers;

            [System.Serializable]

            public class Layer_Data{

                public string spriteName;
                public Color color;

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
