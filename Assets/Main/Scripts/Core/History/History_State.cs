<<<<<<< HEAD
using System.Collections.Generic;

namespace History{

    [System.Serializable]

    public class History_State{
        
        public Dialog_Data dialog;
        public List<Character_Data> characters;
        public List<Audio_Data> audio;
        public List<Audio_SFX_Data> sfx;
        public List<Graphic_Data> graphics;

        public static History_State Capture(){

            History_State state = new History_State();
            state.dialog = Dialog_Data.Capture();
            state.characters = Character_Data.Capture();
            state.audio = Audio_Data.Capture();
            state.sfx = Audio_SFX_Data.Capture();
            state.graphics = Graphic_Data.Capture();

            return state;

        }

        public void Load(){

            Dialog_Data.Apply(dialog);
            Character_Data.Apply(characters);
            Audio_Data.Apply(audio);
            Audio_SFX_Data.Apply(sfx);
            Graphic_Data.Apply(graphics);

        }

    }

}
=======
using System.Collections.Generic;

namespace History{

    [System.Serializable]

    public class History_State{
        
        public Dialog_Data dialog;
        public List<Character_Data> characters;
        public List<Audio_Data> audio;
        public List<Audio_SFX_Data> sfx;
        public List<Graphic_Data> graphics;

        public static History_State Capture(){

            History_State state = new History_State();
            state.dialog = Dialog_Data.Capture();
            state.characters = Character_Data.Capture();
            state.audio = Audio_Data.Capture();
            state.sfx = Audio_SFX_Data.Capture();
            state.graphics = Graphic_Data.Capture();

            return state;

        }

        public void Load(){

            Dialog_Data.Apply(dialog);
            Character_Data.Apply(characters);
            Audio_Data.Apply(audio);
            Audio_SFX_Data.Apply(sfx);
            Graphic_Data.Apply(graphics);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
