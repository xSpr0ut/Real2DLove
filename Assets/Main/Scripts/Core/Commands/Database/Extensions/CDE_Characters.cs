<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

namespace Commands{

    public class CDE_Characters : Command_Database_Extension{

        private static string[] parameterImmediate => new string[] { "#i", "#immediate" };
        private static string[] parameterOnly => new string[] { "#o", "#only" };
        private static string[] parameterState => new string[] { "#st", "#state" };
        private static string[] parameterEnable => new string[] { "#e", "#enable" };
        private static string[] parameterSpeed => new string[] { "#spd", "#speed" };
        private static string[] parameterSmooth => new string[] { "#sm", "#smooth" };
        private static string[] parameterColor => new string[] { "#c", "#color" };
        private static string[] parameterAlpha => new string[] { "#a", "#alpha" };
        private static string[] parameterLayer => new string[] { "#l", "#layer" };
        private static string[] parameterSprite => new string[] { "#s", "#sprite" };
        private static string[] parameterAnimation => new string[] { "#ani", "#animation" };
        private static string[] parameterSize => new string[] { "#s", "#size" };
        private static string parameterXPosition => "#x";
        private static string parameterYPosition => "#y";

        new public static void Extend(Command_Database database){

            database.Add_Command("create_character", new Action<string[]>(Create_Character));
            database.Add_Command("move_character", new Func<string[], IEnumerator>(Move_Character));
            database.Add_Command("show", new Func<string[], IEnumerator>(Show_All));
            database.Add_Command("hide", new Func<string[], IEnumerator>(Hide_All));
            database.Add_Command("sort", new Action<string[]>(Sort));
            database.Add_Command("highlight", new Func<string[], IEnumerator>(Highlight_All));
            database.Add_Command("un_highlight", new Func<string[], IEnumerator>(Un_Highlight_All));

            //Adding commands to all characters
            Command_Database baseCommandDatabase = Command_Manager.instance.Create_Sub_Database(Command_Manager.databaseCharactersBase);
            baseCommandDatabase.Add_Command("move", new Func<string[], IEnumerator>(Move_Character));
            baseCommandDatabase.Add_Command("show", new Func<string[], IEnumerator>(Show));
            baseCommandDatabase.Add_Command("hide", new Func<string[], IEnumerator>(Hide));
            baseCommandDatabase.Add_Command("set_position", new Action<string[]>(Set_Position));
            baseCommandDatabase.Add_Command("set_priority", new Action<string[]>(Set_Priority));
            baseCommandDatabase.Add_Command("set_color", new Func<string[], IEnumerator>(Set_Color));
            baseCommandDatabase.Add_Command("highlight", new Func<string[], IEnumerator>(Highlight));
            baseCommandDatabase.Add_Command("un_highlight", new Func<string[], IEnumerator>(Un_Highlight));
            baseCommandDatabase.Add_Command("flip", new Func<string[], IEnumerator>(Flip));

            //Textbox character commands
            baseCommandDatabase.Add_Command("set_dialog_color", new Action<string[]>(Set_Dialog_Color));
            baseCommandDatabase.Add_Command("set_name_color", new Action<string[]>(Set_Name_Color));
            baseCommandDatabase.Add_Command("set_dialog_size", new Action<string[]>(Set_Dialog_Size));
            baseCommandDatabase.Add_Command("set_name_size", new Action<string[]>(Set_Name_Size));
            baseCommandDatabase.Add_Command("reset_box", new Action<string>(Reset_Configuration_Data));
            //Also can add change font type which also can come inline(rich text)

            //Adding commands to sprite characters
            Command_Database spriteCommandDatabase = Command_Manager.instance.Create_Sub_Database(Command_Manager.databaseCharactersSprite);
            spriteCommandDatabase.Add_Command("set_sprite", new Func<string[], IEnumerator>(Set_Sprite));
            spriteCommandDatabase.Add_Command("animate", new Func<string[], IEnumerator>(Animate));

        }

        #region Global Commands

        private static void Create_Character(string[] data){

            string characterName = data[0];
            bool enable = false;
            bool immediate = false;
            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterEnable, out enable, defaultValue: false);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            Character character = Character_Manager.instance.Create_Character(characterName);

            if(!enable){

                return;

            }

            if(immediate){

                character.isVisible = true;

            }else{

                character.Show();

            }

        }

        private static IEnumerator Move_Character(string[] data){

            string characterName = data[0];
            Character character = Character_Manager.instance.Get_Character(characterName);

            if(character == null){

                yield break;

            }

            float x = 0, y = 0;
            float speed = 1;
            bool smooth = false;
            bool immediate = false;

            var parameters = Convert_Data_To_Parameters(data);

            //Getting x-axis position
            parameters.Try_Get_Value(parameterXPosition, out x);

            //Getting y-axis position
            parameters.Try_Get_Value(parameterYPosition, out y);

            //Getting speed
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1);

            //Getting smoothing
            parameters.Try_Get_Value(parameterSmooth, out smooth, defaultValue: false);

            //Getting immediate
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);

            Vector2 position = new Vector2(x, y);

            if(immediate){

                character.Set_Position(position);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Set_Position(position); });
                yield return character.Move_To_Position(position, speed, smooth);

            }

        }

        private static IEnumerator Show_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 5;

            foreach(string s in data){

                Character character = Character_Manager.instance.Get_Character(s, createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            //Converting data array to a parameter container
            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            //Calling logic for all characters
            foreach(Character character in characters){

                if(immediate){

                    character.isVisible = true;

                }else{

                    character.Show(speed);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(Character character in characters){

                        character.isVisible = true;

                    }

                });

                while(characters.Any(c => c.isRevealing)){

                    yield return null;

                }

            }

        }

        private static IEnumerator Hide_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 5;

            foreach(string s in data){

                Character character = Character_Manager.instance.Get_Character(s, createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            //Converting data array to a parameter container
            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            //Calling logic for all characters
            foreach(Character character in characters){

                if(immediate){

                    character.isVisible = false;

                }else{

                    character.Hide(speed);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(Character character in characters){

                        character.isVisible = false;

                    }

                });

                while(characters.Any(c => c.isHiding)){

                    yield return null;

                }

            }

        }

        private static void Sort(string[] data){

            Character_Manager.instance.Sort_Characters(data);

        }

        public static IEnumerator Highlight_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            bool handleUnspecifiedCharacters = true;
            float speed = 1f;
            List<Character> unspecifiedCharacters = new List<Character>();

            //Adding specified characters to be highlighted
            for(int i = 0; i < data.Length; i++){

                Character character = Character_Manager.instance.Get_Character(data[i], createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterOnly, out handleUnspecifiedCharacters, defaultValue: true);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            foreach(Character character in characters){

                character.Highlight(speed, immediate);

            }

            if(handleUnspecifiedCharacters){

                foreach(Character character in Character_Manager.instance.allCharacters){

                    if(characters.Contains(character)){

                        continue;

                    }

                    unspecifiedCharacters.Add(character);
                    character.Un_Highlight(speed, immediate);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(var character in characters){

                        character.Highlight(immediate: true);

                    }

                    if(!handleUnspecifiedCharacters) return;

                    foreach(var character in unspecifiedCharacters){

                        character.Un_Highlight(immediate: true);

                    }

                });

                while(characters.Any(c => c.isHighlighting) || (handleUnspecifiedCharacters && unspecifiedCharacters.Any(uc => uc.isUnHighlighting))){

                    yield return null;

                }

            }

        }

        public static IEnumerator Un_Highlight_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            bool handleUnspecifiedCharacters = true;
            float speed = 1f;
            List<Character> unspecifiedCharacters = new List<Character>();

            //Adding specified characters to be highlighted
            for(int i = 0; i < data.Length; i++){

                Character character = Character_Manager.instance.Get_Character(data[i], createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterOnly, out handleUnspecifiedCharacters, defaultValue: true);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            foreach(Character character in characters){

                character.Un_Highlight(speed, immediate);

            }

            if(handleUnspecifiedCharacters){

                foreach(Character character in Character_Manager.instance.allCharacters){

                    if(characters.Contains(character)){

                        continue;

                    }

                    unspecifiedCharacters.Add(character);
                    character.Highlight(speed, immediate);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(var character in characters){

                        character.Un_Highlight(immediate: true);

                    }

                    if(!handleUnspecifiedCharacters) return;

                    foreach(var character in unspecifiedCharacters){

                        character.Highlight(immediate: true);

                    }

                });

                while(characters.Any(c => c.isUnHighlighting) || (handleUnspecifiedCharacters && unspecifiedCharacters.Any(uc => uc.isHighlighting))){

                    yield return null;

                }

            }

        }

        #endregion
        
        #region Base Character Commands
        
        private static void Set_Dialog_Color(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            string colorName;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterColor, out colorName);

            Color color = Color.white;
            color = color.Get_Color_From_Name(colorName);

            character.Set_Dialog_Color(color);

        }

        private static void Set_Name_Color(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            string colorName;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterColor, out colorName);

            Color color = Color.white;
            color = color.Get_Color_From_Name(colorName);

            character.Set_Name_Color(color);

        }

        private static void Set_Dialog_Size(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            int size;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterSize, out size);

            character.Set_Dialog_Size(size);

        }

        private static void Set_Name_Size(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            int size;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterSize, out size);

            character.Set_Name_Size(size);

        }

        private static void Reset_Configuration_Data (string data){

            Character character = Character_Manager.instance.Get_Character(data, createIfNonexistent: false);
            character.Reset_Configuration_Data();

        }

        private static IEnumerator Show(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool immediate = false;
            float speed = 5;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            if(immediate){

                character.isVisible = true;

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { if(character != null) character.isVisible = true; });
                yield return character.Show(speed);

            }

        }

        private static IEnumerator Hide(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool immediate = false;
            float speed = 5;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            if(immediate){

                character.isVisible = false;

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { if(character != null) character.isVisible = false; });
                yield return character.Hide(speed);

            }
            
        }

        private static void Set_Position(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            float x = 0, y =0;

            if(character == null || data.Length < 2){

                return;

            }

            var parameters = Convert_Data_To_Parameters(data, 1);

            parameters.Try_Get_Value(parameterXPosition, out x, defaultValue: 0);
            parameters.Try_Get_Value(parameterYPosition, out y, defaultValue: 0);

            character.Set_Position(new Vector2(x, y));

        }

        private static void Set_Priority(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            int priority;

            if(character == null || data.Length < 2){

                return;

            }

            if(!int.TryParse(data[1], out priority)){

                priority = 0;

            }

            character.Set_Priority(priority);

        }

        private static IEnumerator Set_Color(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            string colorName;
            float speed;
            bool immediate;

            if(character == null || data.Length < 2){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterColor, out colorName);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: true);
            
            Color color = Color.white;
            color = color.Get_Color_From_Name(colorName);

            if(immediate){

                character.Set_Color(color);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Set_Color(color); });
                yield return character.Transition_Color(color, speed);

            }

            yield break;

        }

        private static IEnumerator Highlight(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false) as Character;

            if(character == null){

                yield break;

            }

            float speed = 1f;
            bool immediate = false;
            var parameters = Convert_Data_To_Parameters(data, startingIndex:1);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);

            if(immediate){

                character.Highlight(immediate: true);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Highlight(speed, immediate: true); });
                yield return character.Highlight(speed);

            }

        }

        private static IEnumerator Un_Highlight(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false) as Character;

            if(character == null){

                yield break;

            }

            float speed = 1f;
            bool immediate = false;
            var parameters = Convert_Data_To_Parameters(data, startingIndex:1);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);

            if(immediate){

                character.Un_Highlight(immediate: true);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Un_Highlight(speed, immediate: true); });
                yield return character.Un_Highlight(speed);

            }

        }

        private static IEnumerator Flip(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool immediate = false;
            float speed = 1f;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1);

            if(immediate){

                character.Flip(immediate: true);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { if (character.isFacingLeft) character?.Face_Left(immediate: true); if (character.isFacingRight) character?.Face_Right(immediate: true);});
                yield return character.Flip(speed);

            }

        }

        #endregion

        #region Sprite Character Commands

        private static IEnumerator Set_Sprite(string[] data){

            Sprite_Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false) as Sprite_Character;
            string spriteName;
            bool immediate;
            int layer;
            float speed;

            if(character == null || data.Length < 2){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);
;
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSprite, out spriteName);
            parameters.Try_Get_Value(parameterLayer, out layer, defaultValue: 0);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1);

            Sprite sprite = character.Get_Sprite(spriteName);

            if(sprite == null){

                yield break;

            }

            if(immediate){

                character.Set_Sprite(sprite, layer);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Set_Sprite(sprite, layer); });
                yield return character.Transition_Sprite(sprite, layer, speed);

            }

        }

        private static IEnumerator Animate(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool state;
            string animation;
            bool enable;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterAnimation, out animation);
            parameters.Try_Get_Value(parameterState, out state, defaultValue: false);
            parameters.Try_Get_Value(parameterEnable, out enable, defaultValue: false);
            
            if(enable){

                //Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Animate(animation, state: false); });
                character.Animate(animation, state);

            }else{

                character.Animate(animation);

            }
            
        }

        #endregion

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

namespace Commands{

    public class CDE_Characters : Command_Database_Extension{

        private static string[] parameterImmediate => new string[] { "#i", "#immediate" };
        private static string[] parameterOnly => new string[] { "#o", "#only" };
        private static string[] parameterState => new string[] { "#st", "#state" };
        private static string[] parameterEnable => new string[] { "#e", "#enable" };
        private static string[] parameterSpeed => new string[] { "#spd", "#speed" };
        private static string[] parameterSmooth => new string[] { "#sm", "#smooth" };
        private static string[] parameterColor => new string[] { "#c", "#color" };
        private static string[] parameterAlpha => new string[] { "#a", "#alpha" };
        private static string[] parameterLayer => new string[] { "#l", "#layer" };
        private static string[] parameterSprite => new string[] { "#s", "#sprite" };
        private static string[] parameterAnimation => new string[] { "#ani", "#animation" };
        private static string[] parameterSize => new string[] { "#s", "#size" };
        private static string parameterXPosition => "#x";
        private static string parameterYPosition => "#y";

        new public static void Extend(Command_Database database){

            database.Add_Command("create_character", new Action<string[]>(Create_Character));
            database.Add_Command("move_character", new Func<string[], IEnumerator>(Move_Character));
            database.Add_Command("show", new Func<string[], IEnumerator>(Show_All));
            database.Add_Command("hide", new Func<string[], IEnumerator>(Hide_All));
            database.Add_Command("sort", new Action<string[]>(Sort));
            database.Add_Command("highlight", new Func<string[], IEnumerator>(Highlight_All));
            database.Add_Command("un_highlight", new Func<string[], IEnumerator>(Un_Highlight_All));

            //Adding commands to all characters
            Command_Database baseCommandDatabase = Command_Manager.instance.Create_Sub_Database(Command_Manager.databaseCharactersBase);
            baseCommandDatabase.Add_Command("move", new Func<string[], IEnumerator>(Move_Character));
            baseCommandDatabase.Add_Command("show", new Func<string[], IEnumerator>(Show));
            baseCommandDatabase.Add_Command("hide", new Func<string[], IEnumerator>(Hide));
            baseCommandDatabase.Add_Command("set_position", new Action<string[]>(Set_Position));
            baseCommandDatabase.Add_Command("set_priority", new Action<string[]>(Set_Priority));
            baseCommandDatabase.Add_Command("set_color", new Func<string[], IEnumerator>(Set_Color));
            baseCommandDatabase.Add_Command("highlight", new Func<string[], IEnumerator>(Highlight));
            baseCommandDatabase.Add_Command("un_highlight", new Func<string[], IEnumerator>(Un_Highlight));
            baseCommandDatabase.Add_Command("flip", new Func<string[], IEnumerator>(Flip));

            //Textbox character commands
            baseCommandDatabase.Add_Command("set_dialog_color", new Action<string[]>(Set_Dialog_Color));
            baseCommandDatabase.Add_Command("set_name_color", new Action<string[]>(Set_Name_Color));
            baseCommandDatabase.Add_Command("set_dialog_size", new Action<string[]>(Set_Dialog_Size));
            baseCommandDatabase.Add_Command("set_name_size", new Action<string[]>(Set_Name_Size));
            baseCommandDatabase.Add_Command("reset_box", new Action<string>(Reset_Configuration_Data));
            //Also can add change font type which also can come inline(rich text)

            //Adding commands to sprite characters
            Command_Database spriteCommandDatabase = Command_Manager.instance.Create_Sub_Database(Command_Manager.databaseCharactersSprite);
            spriteCommandDatabase.Add_Command("set_sprite", new Func<string[], IEnumerator>(Set_Sprite));
            spriteCommandDatabase.Add_Command("animate", new Func<string[], IEnumerator>(Animate));

        }

        #region Global Commands

        private static void Create_Character(string[] data){

            string characterName = data[0];
            bool enable = false;
            bool immediate = false;
            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterEnable, out enable, defaultValue: false);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            Character character = Character_Manager.instance.Create_Character(characterName);

            if(!enable){

                return;

            }

            if(immediate){

                character.isVisible = true;

            }else{

                character.Show();

            }

        }

        private static IEnumerator Move_Character(string[] data){

            string characterName = data[0];
            Character character = Character_Manager.instance.Get_Character(characterName);

            if(character == null){

                yield break;

            }

            float x = 0, y = 0;
            float speed = 1;
            bool smooth = false;
            bool immediate = false;

            var parameters = Convert_Data_To_Parameters(data);

            //Getting x-axis position
            parameters.Try_Get_Value(parameterXPosition, out x);

            //Getting y-axis position
            parameters.Try_Get_Value(parameterYPosition, out y);

            //Getting speed
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1);

            //Getting smoothing
            parameters.Try_Get_Value(parameterSmooth, out smooth, defaultValue: false);

            //Getting immediate
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);

            Vector2 position = new Vector2(x, y);

            if(immediate){

                character.Set_Position(position);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Set_Position(position); });
                yield return character.Move_To_Position(position, speed, smooth);

            }

        }

        private static IEnumerator Show_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 5;

            foreach(string s in data){

                Character character = Character_Manager.instance.Get_Character(s, createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            //Converting data array to a parameter container
            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            //Calling logic for all characters
            foreach(Character character in characters){

                if(immediate){

                    character.isVisible = true;

                }else{

                    character.Show(speed);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(Character character in characters){

                        character.isVisible = true;

                    }

                });

                while(characters.Any(c => c.isRevealing)){

                    yield return null;

                }

            }

        }

        private static IEnumerator Hide_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 5;

            foreach(string s in data){

                Character character = Character_Manager.instance.Get_Character(s, createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            //Converting data array to a parameter container
            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            //Calling logic for all characters
            foreach(Character character in characters){

                if(immediate){

                    character.isVisible = false;

                }else{

                    character.Hide(speed);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(Character character in characters){

                        character.isVisible = false;

                    }

                });

                while(characters.Any(c => c.isHiding)){

                    yield return null;

                }

            }

        }

        private static void Sort(string[] data){

            Character_Manager.instance.Sort_Characters(data);

        }

        public static IEnumerator Highlight_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            bool handleUnspecifiedCharacters = true;
            float speed = 1f;
            List<Character> unspecifiedCharacters = new List<Character>();

            //Adding specified characters to be highlighted
            for(int i = 0; i < data.Length; i++){

                Character character = Character_Manager.instance.Get_Character(data[i], createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterOnly, out handleUnspecifiedCharacters, defaultValue: true);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            foreach(Character character in characters){

                character.Highlight(speed, immediate);

            }

            if(handleUnspecifiedCharacters){

                foreach(Character character in Character_Manager.instance.allCharacters){

                    if(characters.Contains(character)){

                        continue;

                    }

                    unspecifiedCharacters.Add(character);
                    character.Un_Highlight(speed, immediate);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(var character in characters){

                        character.Highlight(immediate: true);

                    }

                    if(!handleUnspecifiedCharacters) return;

                    foreach(var character in unspecifiedCharacters){

                        character.Un_Highlight(immediate: true);

                    }

                });

                while(characters.Any(c => c.isHighlighting) || (handleUnspecifiedCharacters && unspecifiedCharacters.Any(uc => uc.isUnHighlighting))){

                    yield return null;

                }

            }

        }

        public static IEnumerator Un_Highlight_All(string[] data){

            List<Character> characters = new List<Character>();
            bool immediate = false;
            bool handleUnspecifiedCharacters = true;
            float speed = 1f;
            List<Character> unspecifiedCharacters = new List<Character>();

            //Adding specified characters to be highlighted
            for(int i = 0; i < data.Length; i++){

                Character character = Character_Manager.instance.Get_Character(data[i], createIfNonexistent: false);

                if(character != null){

                    characters.Add(character);

                }

            }

            if(characters.Count == 0){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterOnly, out handleUnspecifiedCharacters, defaultValue: true);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);

            foreach(Character character in characters){

                character.Un_Highlight(speed, immediate);

            }

            if(handleUnspecifiedCharacters){

                foreach(Character character in Character_Manager.instance.allCharacters){

                    if(characters.Contains(character)){

                        continue;

                    }

                    unspecifiedCharacters.Add(character);
                    character.Highlight(speed, immediate);

                }

            }

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() =>{

                    foreach(var character in characters){

                        character.Un_Highlight(immediate: true);

                    }

                    if(!handleUnspecifiedCharacters) return;

                    foreach(var character in unspecifiedCharacters){

                        character.Highlight(immediate: true);

                    }

                });

                while(characters.Any(c => c.isUnHighlighting) || (handleUnspecifiedCharacters && unspecifiedCharacters.Any(uc => uc.isHighlighting))){

                    yield return null;

                }

            }

        }

        #endregion
        
        #region Base Character Commands
        
        private static void Set_Dialog_Color(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            string colorName;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterColor, out colorName);

            Color color = Color.white;
            color = color.Get_Color_From_Name(colorName);

            character.Set_Dialog_Color(color);

        }

        private static void Set_Name_Color(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            string colorName;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterColor, out colorName);

            Color color = Color.white;
            color = color.Get_Color_From_Name(colorName);

            character.Set_Name_Color(color);

        }

        private static void Set_Dialog_Size(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            int size;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterSize, out size);

            character.Set_Dialog_Size(size);

        }

        private static void Set_Name_Size(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            int size;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterSize, out size);

            character.Set_Name_Size(size);

        }

        private static void Reset_Configuration_Data (string data){

            Character character = Character_Manager.instance.Get_Character(data, createIfNonexistent: false);
            character.Reset_Configuration_Data();

        }

        private static IEnumerator Show(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool immediate = false;
            float speed = 5;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            if(immediate){

                character.isVisible = true;

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { if(character != null) character.isVisible = true; });
                yield return character.Show(speed);

            }

        }

        private static IEnumerator Hide(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool immediate = false;
            float speed = 5;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 5);

            if(immediate){

                character.isVisible = false;

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { if(character != null) character.isVisible = false; });
                yield return character.Hide(speed);

            }
            
        }

        private static void Set_Position(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            float x = 0, y =0;

            if(character == null || data.Length < 2){

                return;

            }

            var parameters = Convert_Data_To_Parameters(data, 1);

            parameters.Try_Get_Value(parameterXPosition, out x, defaultValue: 0);
            parameters.Try_Get_Value(parameterYPosition, out y, defaultValue: 0);

            character.Set_Position(new Vector2(x, y));

        }

        private static void Set_Priority(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            int priority;

            if(character == null || data.Length < 2){

                return;

            }

            if(!int.TryParse(data[1], out priority)){

                priority = 0;

            }

            character.Set_Priority(priority);

        }

        private static IEnumerator Set_Color(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false);
            string colorName;
            float speed;
            bool immediate;

            if(character == null || data.Length < 2){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterColor, out colorName);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: true);
            
            Color color = Color.white;
            color = color.Get_Color_From_Name(colorName);

            if(immediate){

                character.Set_Color(color);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Set_Color(color); });
                yield return character.Transition_Color(color, speed);

            }

            yield break;

        }

        private static IEnumerator Highlight(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false) as Character;

            if(character == null){

                yield break;

            }

            float speed = 1f;
            bool immediate = false;
            var parameters = Convert_Data_To_Parameters(data, startingIndex:1);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);

            if(immediate){

                character.Highlight(immediate: true);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Highlight(speed, immediate: true); });
                yield return character.Highlight(speed);

            }

        }

        private static IEnumerator Un_Highlight(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false) as Character;

            if(character == null){

                yield break;

            }

            float speed = 1f;
            bool immediate = false;
            var parameters = Convert_Data_To_Parameters(data, startingIndex:1);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1f);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);

            if(immediate){

                character.Un_Highlight(immediate: true);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Un_Highlight(speed, immediate: true); });
                yield return character.Un_Highlight(speed);

            }

        }

        private static IEnumerator Flip(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool immediate = false;
            float speed = 1f;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1);

            if(immediate){

                character.Flip(immediate: true);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { if (character.isFacingLeft) character?.Face_Left(immediate: true); if (character.isFacingRight) character?.Face_Right(immediate: true);});
                yield return character.Flip(speed);

            }

        }

        #endregion

        #region Sprite Character Commands

        private static IEnumerator Set_Sprite(string[] data){

            Sprite_Character character = Character_Manager.instance.Get_Character(data[0], createIfNonexistent: false) as Sprite_Character;
            string spriteName;
            bool immediate;
            int layer;
            float speed;

            if(character == null || data.Length < 2){

                yield break;

            }

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);
;
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSprite, out spriteName);
            parameters.Try_Get_Value(parameterLayer, out layer, defaultValue: 0);
            parameters.Try_Get_Value(parameterSpeed, out speed, defaultValue: 1);

            Sprite sprite = character.Get_Sprite(spriteName);

            if(sprite == null){

                yield break;

            }

            if(immediate){

                character.Set_Sprite(sprite, layer);

            }else{

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Set_Sprite(sprite, layer); });
                yield return character.Transition_Sprite(sprite, layer, speed);

            }

        }

        private static IEnumerator Animate(string[] data){

            Character character = Character_Manager.instance.Get_Character(data[0]);

            if(character == null){

                yield break;

            }

            bool state;
            string animation;
            bool enable;

            var parameters = Convert_Data_To_Parameters(data, startingIndex: 1);

            parameters.Try_Get_Value(parameterAnimation, out animation);
            parameters.Try_Get_Value(parameterState, out state, defaultValue: false);
            parameters.Try_Get_Value(parameterEnable, out enable, defaultValue: false);
            
            if(enable){

                //Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => { character?.Animate(animation, state: false); });
                character.Animate(animation, state);

            }else{

                character.Animate(animation);

            }
            
        }

        #endregion

    }

}
>>>>>>> c8d4f422 (Whole Template)
