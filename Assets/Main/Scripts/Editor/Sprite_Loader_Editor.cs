<<<<<<< HEAD
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Characters;
using System.Linq;

public class SpriteLoaderEditor : EditorWindow{
    private string rootFolderPath = "Assets/";
    private Character_Configuration_SO characterConfigurationFile;
    private int selectedCharacterIndex = 0;
    private Dictionary<string, Sprite> sprites;

    [MenuItem("Tools/Sprite Loader")]
    public static void ShowWindow(){

        GetWindow<SpriteLoaderEditor>("Sprite Loader");

    }

    private void OnGUI(){

        EditorGUILayout.LabelField("Sprite Loader", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        rootFolderPath = EditorGUILayout.TextField("Root Folder Path:", rootFolderPath);
        
        if (GUILayout.Button("Browse", GUILayout.Width(80))){

            string selectedFolder = EditorUtility.OpenFolderPanel("Select Folder", rootFolderPath, "");
            
            if (!string.IsNullOrEmpty(selectedFolder) && selectedFolder.Contains(Application.dataPath)){

                rootFolderPath = "Assets" + selectedFolder.Substring(Application.dataPath.Length);
            
            }else if (!string.IsNullOrEmpty(selectedFolder)){

                Debug.LogError("The selected folder must be inside the Assets directory!");
            
            }
        
        }
        
        EditorGUILayout.EndHorizontal();

        characterConfigurationFile = (Character_Configuration_SO)EditorGUILayout.ObjectField("Character Config:", characterConfigurationFile, typeof(Character_Configuration_SO), false);

        if (characterConfigurationFile != null && characterConfigurationFile.characters != null && characterConfigurationFile.characters.Length > 0){

            string[] characterNames = GetCharacterNames(characterConfigurationFile.characters);
            selectedCharacterIndex = EditorGUILayout.Popup("Character Name:", selectedCharacterIndex, characterNames);
        
        }

        if (GUILayout.Button("Load Sprites")){

            LoadSpritesFromFolder(rootFolderPath);
            
            if (sprites != null){

                Debug.Log($"{sprites.Count} sprites loaded!");
            
            }else{

                Debug.Log("No sprites loaded!");

            }

        }

        if (sprites != null && sprites.Count > 0){

            EditorGUILayout.LabelField($"Loaded {sprites.Count} Sprites:");
            
            foreach (var pair in sprites){

                EditorGUILayout.LabelField($"Key: {pair.Key}, Sprite: {pair.Value.name}");
            
            }
        
        }

    }

    private string[] GetCharacterNames(Character_Configuration_Data[] characters){
        
        List<string> names = new List<string>();
        
        foreach (var character in characters){
            
            names.Add(character.name);
        
        }
        
        return names.ToArray();
    
    }

    private void LoadSpritesFromFolder(string folderPath){
        
        if (characterConfigurationFile != null && characterConfigurationFile.characters != null && selectedCharacterIndex < characterConfigurationFile.characters.Length){
            
            string characterName = characterConfigurationFile.characters[selectedCharacterIndex].name;
            var config = characterConfigurationFile.Get_Configuration(characterName, safe: false);

            if (config.characterType == Character.Character_Type.SpriteSheet){
                
                string[] texturePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

                foreach (string path in texturePaths){
                    
                    Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                    
                    if (texture != null){
                        
                        string subFolderPath = Path.GetDirectoryName(path).Replace(rootFolderPath, "");
                        Sprite[] spritesFromTexture = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();

                        foreach (Sprite spriteFromSheet in spritesFromTexture){
                            
                            string keyName = spriteFromSheet.name;
                            config.sprites[keyName] = spriteFromSheet;
                        
                        }
                   
                    }
                
                }
            
            }else{

                //Character type is sprite
                string[] spritePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

                foreach (string path in spritePaths){
                    
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                    
                    if (sprite != null){
                        
                        string keyName = path.Replace(rootFolderPath, "").Replace(".png", "").ToLower();
                        string fullKeyName = keyName.Substring(1); // Removing the starting "/"
                        config.sprites[fullKeyName] = sprite;
                    
                    }
                
                }
           
            }

            sprites = config.sprites;
       
        }else{
          
            Debug.LogWarning("Invalid CharacterConfigSO or character selection.");
        
        }
    
    }

}
=======
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Characters;
using System.Linq;

public class SpriteLoaderEditor : EditorWindow{
    private string rootFolderPath = "Assets/";
    private Character_Configuration_SO characterConfigurationFile;
    private int selectedCharacterIndex = 0;
    private Dictionary<string, Sprite> sprites;

    [MenuItem("Tools/Sprite Loader")]
    public static void ShowWindow(){

        GetWindow<SpriteLoaderEditor>("Sprite Loader");

    }

    private void OnGUI(){

        EditorGUILayout.LabelField("Sprite Loader", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        rootFolderPath = EditorGUILayout.TextField("Root Folder Path:", rootFolderPath);
        
        if (GUILayout.Button("Browse", GUILayout.Width(80))){

            string selectedFolder = EditorUtility.OpenFolderPanel("Select Folder", rootFolderPath, "");
            
            if (!string.IsNullOrEmpty(selectedFolder) && selectedFolder.Contains(Application.dataPath)){

                rootFolderPath = "Assets" + selectedFolder.Substring(Application.dataPath.Length);
            
            }else if (!string.IsNullOrEmpty(selectedFolder)){

                Debug.LogError("The selected folder must be inside the Assets directory!");
            
            }
        
        }
        
        EditorGUILayout.EndHorizontal();

        characterConfigurationFile = (Character_Configuration_SO)EditorGUILayout.ObjectField("Character Config:", characterConfigurationFile, typeof(Character_Configuration_SO), false);

        if (characterConfigurationFile != null && characterConfigurationFile.characters != null && characterConfigurationFile.characters.Length > 0){

            string[] characterNames = GetCharacterNames(characterConfigurationFile.characters);
            selectedCharacterIndex = EditorGUILayout.Popup("Character Name:", selectedCharacterIndex, characterNames);
        
        }

        if (GUILayout.Button("Load Sprites")){

            LoadSpritesFromFolder(rootFolderPath);
            
            if (sprites != null){

                Debug.Log($"{sprites.Count} sprites loaded!");
            
            }else{

                Debug.Log("No sprites loaded!");

            }

        }

        if (sprites != null && sprites.Count > 0){

            EditorGUILayout.LabelField($"Loaded {sprites.Count} Sprites:");
            
            foreach (var pair in sprites){

                EditorGUILayout.LabelField($"Key: {pair.Key}, Sprite: {pair.Value.name}");
            
            }
        
        }

    }

    private string[] GetCharacterNames(Character_Configuration_Data[] characters){
        
        List<string> names = new List<string>();
        
        foreach (var character in characters){
            
            names.Add(character.name);
        
        }
        
        return names.ToArray();
    
    }

    private void LoadSpritesFromFolder(string folderPath){
        
        if (characterConfigurationFile != null && characterConfigurationFile.characters != null && selectedCharacterIndex < characterConfigurationFile.characters.Length){
            
            string characterName = characterConfigurationFile.characters[selectedCharacterIndex].name;
            var config = characterConfigurationFile.Get_Configuration(characterName, safe: false);

            if (config.characterType == Character.Character_Type.SpriteSheet){
                
                string[] texturePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

                foreach (string path in texturePaths){
                    
                    Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                    
                    if (texture != null){
                        
                        string subFolderPath = Path.GetDirectoryName(path).Replace(rootFolderPath, "");
                        Sprite[] spritesFromTexture = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();

                        foreach (Sprite spriteFromSheet in spritesFromTexture){
                            
                            string keyName = spriteFromSheet.name;
                            config.sprites[keyName] = spriteFromSheet;
                        
                        }
                   
                    }
                
                }
            
            }else{

                //Character type is sprite
                string[] spritePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

                foreach (string path in spritePaths){
                    
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                    
                    if (sprite != null){
                        
                        string keyName = path.Replace(rootFolderPath, "").Replace(".png", "").ToLower();
                        string fullKeyName = keyName.Substring(1); // Removing the starting "/"
                        config.sprites[fullKeyName] = sprite;
                    
                    }
                
                }
           
            }

            sprites = config.sprites;
       
        }else{
          
            Debug.LogWarning("Invalid CharacterConfigSO or character selection.");
        
        }
    
    }

}
>>>>>>> c8d4f422 (Whole Template)
