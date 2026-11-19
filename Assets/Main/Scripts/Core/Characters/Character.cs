<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Dialog;
using TMPro;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Characters{

    public abstract class Character{

        public const bool enableOnStart = false;
        private const float unHightlightColorDarkenStrength = 0.65f;
        public const bool defaultOrientationFacingLeft = true;
        public const string animationRefreshTrigger = "Refresh";
        public string name = "";
        public string displayName = "";
        public string castingName = "";
        public RectTransform root = null;
        public Character_Configuration_Data configurationData;
        public Animator animator;
        public Color color{get; protected set;} = Color.white;
        protected Color displayColor => highlighted ? highlightColor : unHightlightColor;
        protected Color highlightColor => color;
        protected Color unHightlightColor => new Color(color.r * unHightlightColorDarkenStrength, color.g * unHightlightColorDarkenStrength, color.b * unHightlightColorDarkenStrength, color.a);
        public bool highlighted{get; protected set;} = true;
        protected bool facingLeft = defaultOrientationFacingLeft;
        public int priority{get; protected set;}
        public Vector2 targetPosition { get; private set; }
        
        public Dialog_System dialog_System => Dialog_System.instance;
        protected Character_Manager characterManager => Character_Manager.instance;

        protected Coroutine Co_Revealing, Co_Hiding;
        protected Coroutine Co_Moving;    
        protected Coroutine Co_Changing_Color;   
        protected Coroutine Co_Highlighting; 
        protected Coroutine Co_Flipping;
        public bool isRevealing => Co_Revealing != null;
        public bool isHiding => Co_Hiding != null;
        public bool isMoving => Co_Moving != null;
        public bool isChangingColor => Co_Changing_Color != null;
        public bool isHighlighting => (highlighted && Co_Highlighting != null);
        public bool isUnHighlighting => (!highlighted && Co_Highlighting != null);
        public virtual bool isVisible {get; set;}
        public bool isFacingLeft => facingLeft;
        public bool isFacingRight => !facingLeft;
        public bool isFlipping => Co_Flipping != null;

        //Character has string name & display name, configurations, and a prefab
        
        public Character(string name, Character_Configuration_Data configurationData, GameObject prefab){

            this.name = name;
            displayName = this.name;
            this.configurationData = configurationData;

            if(prefab != null){

                GameObject obj = Object.Instantiate(prefab,characterManager.characterPanel);
                obj.name = characterManager.Format_Character_Path(characterManager.characterPrefabNameFormat, name);
                obj.SetActive(true);
                root = obj.GetComponent<RectTransform>();
                animator = root.GetComponentInChildren<Animator>();

            }

        }

        //Configurations

        public void Set_Name_Color(Color color) => configurationData.nameColor = color;
        public void Set_Name_Font(TMP_FontAsset font) => configurationData.nameFont = font;
        public void Set_Dialog_Color(Color color) => configurationData.dialogColor = color;
        public void Set_Dialog_Font(TMP_FontAsset font) => configurationData.dialogFont = font;
        public void Set_Name_Size(int size) => configurationData.nameFontSize = size;
        public void Set_Dialog_Size(int size) => configurationData.dialogFontSize = size;
        
        public void Reset_Configuration_Data() => configurationData = Character_Manager.instance.Get_Character_Configuration_Data(name, getOriginal: true);
        public void Update_Text_Customizations_Onscreen() => dialog_System.Apply_Speaker_Data_To_Dialog_Container(configurationData);

        //Saying things, can be used outside a visual novel format since
        //Dialog can be called with the character class directly instead
        //Of dialog files

        public Coroutine Say(string dialog) => Say(new List<string> {dialog});
        public Coroutine Say(List<string> dialog){

            dialog_System.Show_Speaker_Name(displayName);
            dialog_System.Apply_Speaker_Data_To_Dialog_Container(configurationData);
            return dialog_System.Say(dialog);

        }

        //Showing/Hiding
        
        public virtual Coroutine Show(float speed = 5){

            if(isRevealing){

                characterManager.StopCoroutine(Co_Revealing);

            }

            if(isHiding){

                characterManager.StopCoroutine(Co_Hiding);

            }

            Co_Revealing = characterManager.StartCoroutine(Showing_Or_Hiding(true, speed));
            return Co_Revealing;

        }

        public virtual Coroutine Hide(float speed = 5){

            if(isHiding){

                characterManager.StopCoroutine(Co_Hiding);

            }

            if(isRevealing){

                characterManager.StopCoroutine(Co_Revealing);

            }

            Co_Hiding = characterManager.StartCoroutine(Showing_Or_Hiding(false, speed));
            return Co_Hiding;
            
        }

        public virtual IEnumerator Showing_Or_Hiding(bool show, float speed){

            Debug.Log("Show/Hide can't be called from a base character type.");
            yield return null;

        }

        //Moving

        public virtual void Set_Position(Vector2 position){

            if(root == null){

                return;

            }

            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = Convert_Target_Position_To_Relative_Anchor_Targets(position);

            root.anchorMin = minAnchorTarget;
            root.anchorMax = maxAnchorTarget;

            targetPosition = position;

        }

        public virtual Coroutine Move_To_Position(Vector2 position, float speed = 2f, bool smooth = false){

            if(root == null){

                return null;

            }
            
            if(isMoving){

                characterManager.StopCoroutine(Co_Moving);

            }

            Co_Moving = characterManager.StartCoroutine(Moving_To_Position(position, speed, smooth));

            targetPosition = position;

            return Co_Moving;

        }

        private IEnumerator Moving_To_Position(Vector2 position, float speed, bool smooth){

            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = Convert_Target_Position_To_Relative_Anchor_Targets(position);
            Vector2 offset = root.anchorMax - root.anchorMin;

            while(root.anchorMin != minAnchorTarget || root.anchorMax != maxAnchorTarget){

                root.anchorMin = smooth ?
                    Vector2.Lerp(root.anchorMin, minAnchorTarget, speed * Time.deltaTime)
                    : Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed * Time.deltaTime * 0.35f);

                root.anchorMax =root.anchorMin + offset;

                if(smooth && Vector2.Distance(root.anchorMin, minAnchorTarget) <= 0.001f){

                    root.anchorMin = minAnchorTarget;
                    root.anchorMax = maxAnchorTarget;
                    break;

                }

                yield return null;

            }

            Co_Moving = null;

        }

        protected (Vector2, Vector2) Convert_Target_Position_To_Relative_Anchor_Targets(Vector2 position){

            Vector2 offset = root.anchorMax - root.anchorMin;
            float maxX = 1f - offset.x;
            float maxY = 1f - offset.y;
            Vector2 minAnchorTarget = new Vector2(maxX * position.x, maxY * position.y);
            Vector2 maxAnchorTarget = minAnchorTarget + offset;

            return (minAnchorTarget, maxAnchorTarget);

        }

        public virtual void Set_Color(Color color, float alpha = 1){
            
            this.color = color;
            color = color.Set_Alpha(alpha);

        }

        public Coroutine Transition_Color(Color color, float speed = 1f){

            this.color = color;

            if(isChangingColor){

                characterManager.StopCoroutine(Co_Changing_Color);

            }

            Co_Changing_Color = characterManager.StartCoroutine(Changing_Color(displayColor, speed));

            return Co_Changing_Color;

        }

        public virtual IEnumerator Changing_Color(Color color, float speed){

            Debug.Log("Color changing not for this character type :p");
            yield return null;

        }

        public Coroutine Highlight(float speed = 1f, bool immediate = false){

            if(isHighlighting || isUnHighlighting){

                characterManager.StopCoroutine(Co_Highlighting);

            }

            highlighted = true;
            Co_Highlighting = characterManager.StartCoroutine(Highlighting(speed, immediate));

            return Co_Highlighting;

        }

         public Coroutine Un_Highlight(float speed = 1f, bool immediate = false){

            if(isUnHighlighting || isHighlighting){

                characterManager.StopCoroutine(Co_Highlighting);

            }

            highlighted = false;
            Co_Highlighting = characterManager.StartCoroutine(Highlighting(speed, immediate));

            return Co_Highlighting;
            
        }

        public virtual IEnumerator Highlighting(float speedMultiplier, bool immediate = false){

            Debug.Log("Highlighting is not available for this character type :p");
            yield return null;

        }

        public Coroutine Flip(float speed = 1, bool immediate = false){

            if(isFacingLeft){

                return Face_Right(speed, immediate);

            }else{

                return Face_Left(speed, immediate);

            }

        }

        public Coroutine Face_Left(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            facingLeft = true;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(facingLeft, speed, immediate));

            return Co_Flipping;

        }

        public Coroutine Face_Right(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            facingLeft = false;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(facingLeft, speed, immediate));

            return Co_Flipping;
            
        }

        public virtual IEnumerator Face_Direction(bool faceLeft, float speedMultiplier, bool immediate){

            Debug.Log("Cannot flip this type fo character.");
            yield return null;

        }

        public void Set_Priority(int priority, bool autoSortCharactersOnUI = true){

            this.priority = priority;

            if(autoSortCharactersOnUI){

                characterManager.Sort_Characters();

            }

        }

        public void Animate(string animation){

            animator.SetTrigger(animation);

        }

        public void Animate(string animation, bool state){

            animator.SetBool(animation, state);
            animator.SetTrigger(animationRefreshTrigger);

        }

        public virtual void On_Recieve_Casting_Expression(int layer, string expression){

            return;

        }

        //Character Types
        public enum Character_Type{

            Text,
            Sprite,
            SpriteSheet,

        }
        
    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Dialog;
using TMPro;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Characters{

    public abstract class Character{

        public const bool enableOnStart = false;
        private const float unHightlightColorDarkenStrength = 0.65f;
        public const bool defaultOrientationFacingLeft = true;
        public const string animationRefreshTrigger = "Refresh";
        public string name = "";
        public string displayName = "";
        public string castingName = "";
        public RectTransform root = null;
        public Character_Configuration_Data configurationData;
        public Animator animator;
        public Color color{get; protected set;} = Color.white;
        protected Color displayColor => highlighted ? highlightColor : unHightlightColor;
        protected Color highlightColor => color;
        protected Color unHightlightColor => new Color(color.r * unHightlightColorDarkenStrength, color.g * unHightlightColorDarkenStrength, color.b * unHightlightColorDarkenStrength, color.a);
        public bool highlighted{get; protected set;} = true;
        protected bool facingLeft = defaultOrientationFacingLeft;
        public int priority{get; protected set;}
        public Vector2 targetPosition { get; private set; }
        
        public Dialog_System dialog_System => Dialog_System.instance;
        protected Character_Manager characterManager => Character_Manager.instance;

        protected Coroutine Co_Revealing, Co_Hiding;
        protected Coroutine Co_Moving;    
        protected Coroutine Co_Changing_Color;   
        protected Coroutine Co_Highlighting; 
        protected Coroutine Co_Flipping;
        public bool isRevealing => Co_Revealing != null;
        public bool isHiding => Co_Hiding != null;
        public bool isMoving => Co_Moving != null;
        public bool isChangingColor => Co_Changing_Color != null;
        public bool isHighlighting => (highlighted && Co_Highlighting != null);
        public bool isUnHighlighting => (!highlighted && Co_Highlighting != null);
        public virtual bool isVisible {get; set;}
        public bool isFacingLeft => facingLeft;
        public bool isFacingRight => !facingLeft;
        public bool isFlipping => Co_Flipping != null;

        //Character has string name & display name, configurations, and a prefab
        
        public Character(string name, Character_Configuration_Data configurationData, GameObject prefab){

            this.name = name;
            displayName = this.name;
            this.configurationData = configurationData;

            if(prefab != null){

                GameObject obj = Object.Instantiate(prefab,characterManager.characterPanel);
                obj.name = characterManager.Format_Character_Path(characterManager.characterPrefabNameFormat, name);
                obj.SetActive(true);
                root = obj.GetComponent<RectTransform>();
                animator = root.GetComponentInChildren<Animator>();

            }

        }

        //Configurations

        public void Set_Name_Color(Color color) => configurationData.nameColor = color;
        public void Set_Name_Font(TMP_FontAsset font) => configurationData.nameFont = font;
        public void Set_Dialog_Color(Color color) => configurationData.dialogColor = color;
        public void Set_Dialog_Font(TMP_FontAsset font) => configurationData.dialogFont = font;
        public void Set_Name_Size(int size) => configurationData.nameFontSize = size;
        public void Set_Dialog_Size(int size) => configurationData.dialogFontSize = size;
        
        public void Reset_Configuration_Data() => configurationData = Character_Manager.instance.Get_Character_Configuration_Data(name, getOriginal: true);
        public void Update_Text_Customizations_Onscreen() => dialog_System.Apply_Speaker_Data_To_Dialog_Container(configurationData);

        //Saying things, can be used outside a visual novel format since
        //Dialog can be called with the character class directly instead
        //Of dialog files

        public Coroutine Say(string dialog) => Say(new List<string> {dialog});
        public Coroutine Say(List<string> dialog){

            dialog_System.Show_Speaker_Name(displayName);
            dialog_System.Apply_Speaker_Data_To_Dialog_Container(configurationData);
            return dialog_System.Say(dialog);

        }

        //Showing/Hiding
        
        public virtual Coroutine Show(float speed = 5){

            if(isRevealing){

                characterManager.StopCoroutine(Co_Revealing);

            }

            if(isHiding){

                characterManager.StopCoroutine(Co_Hiding);

            }

            Co_Revealing = characterManager.StartCoroutine(Showing_Or_Hiding(true, speed));
            return Co_Revealing;

        }

        public virtual Coroutine Hide(float speed = 5){

            if(isHiding){

                characterManager.StopCoroutine(Co_Hiding);

            }

            if(isRevealing){

                characterManager.StopCoroutine(Co_Revealing);

            }

            Co_Hiding = characterManager.StartCoroutine(Showing_Or_Hiding(false, speed));
            return Co_Hiding;
            
        }

        public virtual IEnumerator Showing_Or_Hiding(bool show, float speed){

            Debug.Log("Show/Hide can't be called from a base character type.");
            yield return null;

        }

        //Moving

        public virtual void Set_Position(Vector2 position){

            if(root == null){

                return;

            }

            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = Convert_Target_Position_To_Relative_Anchor_Targets(position);

            root.anchorMin = minAnchorTarget;
            root.anchorMax = maxAnchorTarget;

            targetPosition = position;

        }

        public virtual Coroutine Move_To_Position(Vector2 position, float speed = 2f, bool smooth = false){

            if(root == null){

                return null;

            }
            
            if(isMoving){

                characterManager.StopCoroutine(Co_Moving);

            }

            Co_Moving = characterManager.StartCoroutine(Moving_To_Position(position, speed, smooth));

            targetPosition = position;

            return Co_Moving;

        }

        private IEnumerator Moving_To_Position(Vector2 position, float speed, bool smooth){

            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = Convert_Target_Position_To_Relative_Anchor_Targets(position);
            Vector2 offset = root.anchorMax - root.anchorMin;

            while(root.anchorMin != minAnchorTarget || root.anchorMax != maxAnchorTarget){

                root.anchorMin = smooth ?
                    Vector2.Lerp(root.anchorMin, minAnchorTarget, speed * Time.deltaTime)
                    : Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed * Time.deltaTime * 0.35f);

                root.anchorMax =root.anchorMin + offset;

                if(smooth && Vector2.Distance(root.anchorMin, minAnchorTarget) <= 0.001f){

                    root.anchorMin = minAnchorTarget;
                    root.anchorMax = maxAnchorTarget;
                    break;

                }

                yield return null;

            }

            Co_Moving = null;

        }

        protected (Vector2, Vector2) Convert_Target_Position_To_Relative_Anchor_Targets(Vector2 position){

            Vector2 offset = root.anchorMax - root.anchorMin;
            float maxX = 1f - offset.x;
            float maxY = 1f - offset.y;
            Vector2 minAnchorTarget = new Vector2(maxX * position.x, maxY * position.y);
            Vector2 maxAnchorTarget = minAnchorTarget + offset;

            return (minAnchorTarget, maxAnchorTarget);

        }

        public virtual void Set_Color(Color color, float alpha = 1){
            
            this.color = color;
            color = color.Set_Alpha(alpha);

        }

        public Coroutine Transition_Color(Color color, float speed = 1f){

            this.color = color;

            if(isChangingColor){

                characterManager.StopCoroutine(Co_Changing_Color);

            }

            Co_Changing_Color = characterManager.StartCoroutine(Changing_Color(displayColor, speed));

            return Co_Changing_Color;

        }

        public virtual IEnumerator Changing_Color(Color color, float speed){

            Debug.Log("Color changing not for this character type :p");
            yield return null;

        }

        public Coroutine Highlight(float speed = 1f, bool immediate = false){

            if(isHighlighting || isUnHighlighting){

                characterManager.StopCoroutine(Co_Highlighting);

            }

            highlighted = true;
            Co_Highlighting = characterManager.StartCoroutine(Highlighting(speed, immediate));

            return Co_Highlighting;

        }

         public Coroutine Un_Highlight(float speed = 1f, bool immediate = false){

            if(isUnHighlighting || isHighlighting){

                characterManager.StopCoroutine(Co_Highlighting);

            }

            highlighted = false;
            Co_Highlighting = characterManager.StartCoroutine(Highlighting(speed, immediate));

            return Co_Highlighting;
            
        }

        public virtual IEnumerator Highlighting(float speedMultiplier, bool immediate = false){

            Debug.Log("Highlighting is not available for this character type :p");
            yield return null;

        }

        public Coroutine Flip(float speed = 1, bool immediate = false){

            if(isFacingLeft){

                return Face_Right(speed, immediate);

            }else{

                return Face_Left(speed, immediate);

            }

        }

        public Coroutine Face_Left(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            facingLeft = true;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(facingLeft, speed, immediate));

            return Co_Flipping;

        }

        public Coroutine Face_Right(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            facingLeft = false;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(facingLeft, speed, immediate));

            return Co_Flipping;
            
        }

        public virtual IEnumerator Face_Direction(bool faceLeft, float speedMultiplier, bool immediate){

            Debug.Log("Cannot flip this type fo character.");
            yield return null;

        }

        public void Set_Priority(int priority, bool autoSortCharactersOnUI = true){

            this.priority = priority;

            if(autoSortCharactersOnUI){

                characterManager.Sort_Characters();

            }

        }

        public void Animate(string animation){

            animator.SetTrigger(animation);

        }

        public void Animate(string animation, bool state){

            animator.SetBool(animation, state);
            animator.SetTrigger(animationRefreshTrigger);

        }

        public virtual void On_Recieve_Casting_Expression(int layer, string expression){

            return;

        }

        //Character Types
        public enum Character_Type{

            Text,
            Sprite,
            SpriteSheet,

        }
        
    }

}
>>>>>>> c8d4f422 (Whole Template)
