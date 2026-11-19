<<<<<<< HEAD
using System.Collections;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using JetBrains.Annotations;
using System;

public class Text_System {
    
    private TextMeshProUGUI tmproUi;
    private TextMeshPro tmproWorld;
    //To put text in the appropiate spot
    public TMP_Text tmpro => tmproUi != null ? tmproUi : tmproWorld;

    public string currentText => tmpro.text;
    //The text to add
    public string targetText {get; private set;} = "";
    //To append onto already existing text, or preText
    public string preText {get; private set;} = "";
    private int preTextLength = 0;

    public string fullTargetText => preText + targetText;

    public enum BuildMethod {instant, gradual, fade}
    public BuildMethod buildMethod = BuildMethod.gradual;

    public Color textColor {get{return tmpro.color;} set{tmpro.color = value;}}

    //Text building speed
    private float baseSpeed = 1f;
    private float speedMultiplier = 1f;
    public float speed{get{return baseSpeed * speedMultiplier;} set{speedMultiplier = value;}}

    //For speeding up characters if the player clicks 
    private int characterMultiplier = 1;
    public int charactersPerCycle{get{return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3;}}

    public bool speedUp = false;

    public Text_System(TextMeshProUGUI tmproUi){

        this.tmproUi = tmproUi;

    }

    public Text_System(TextMeshPro tmproWorld){

        this.tmproWorld = tmproWorld;

    }

    public Coroutine Build(string text){

        preText = "";
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;

    }

    public Coroutine Append(string text){

        preText = tmpro.text;
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;

    }

    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;

    public void Stop(){

        if(!isBuilding)

            return;

        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;

    }

    IEnumerator Building(){

        Prepare();

        switch(buildMethod){

            case BuildMethod.gradual:

                yield return Build_Gradual();
                break;

            case BuildMethod.fade:

                yield return Build_Fade();
                break;

        }

        On_Complete();

    }

    private void On_Complete(){

        buildProcess = null;
        speedUp = false;

    }

    public void Force_Complete(){

        switch(buildMethod){

            case BuildMethod.gradual:

            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
            break;

        case BuildMethod.fade:

            tmpro.ForceMeshUpdate();
            break;

        }
        
        Stop();
        On_Complete();

    }

    private void Prepare(){

        switch(buildMethod){

            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.gradual:
                Prepare_Gradual();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;

        }

    }

    private void Prepare_Instant(){

        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;

    }

    private void Prepare_Gradual(){

        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if(preText != ""){

            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;

        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();

    }

    private void Prepare_Fade(){

        tmpro.text = preText;

        if(preText != ""){

            tmpro.ForceMeshUpdate();
            preTextLength = tmpro.textInfo.characterCount;

        }else{

            preTextLength = 0;

        }

        tmpro.text += targetText;
        tmpro.maxVisibleCharacters = int.MaxValue;
        tmpro.ForceMeshUpdate();

        TMP_TextInfo textInfo = tmpro.textInfo;

        Color visibleColor = new Color(textColor.r, textColor.g, textColor.b, 1);
        Color hiddenColor = new Color(textColor.r, textColor.g, textColor.b, 0);

        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

        for(int i = 0; i < textInfo.characterCount; i++){

            TMP_CharacterInfo characterInfo = textInfo.characterInfo[i];

            if(!characterInfo.isVisible){

                continue;

            }

            if(i < preTextLength){

                for(int v = 0; v < 4; v++){

                    vertexColors[characterInfo.vertexIndex + v] = visibleColor;

                }

            }else{

                for(int v = 0; v < 4; v++){

                    vertexColors[characterInfo.vertexIndex + v] = hiddenColor;

                }

            }

        }

        tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

    }

    private IEnumerator Build_Gradual(){

        while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount){

            tmpro.maxVisibleCharacters += speedUp ? charactersPerCycle * 5 : charactersPerCycle;
            yield return new WaitForSeconds(0.015f / speed);

        }

    }

    private IEnumerator Build_Fade(){

        int minRange = preTextLength;
        int maxRange = minRange +1;

        byte alphaThreshold = 15;
        TMP_TextInfo textInfo = tmpro.textInfo;
        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;
        float[] alphas = new float[textInfo.characterCount];

        while(true){

            float fadeSpeed = ((speedUp ? charactersPerCycle * 5 : charactersPerCycle) * speed) * 2.5f;

            for(int i = minRange; i < maxRange; i++){

                TMP_CharacterInfo characterInfo = textInfo.characterInfo[i];

                if(!characterInfo.isVisible){

                    continue;

                }

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                alphas[i] = Mathf.MoveTowards(alphas[i], 255, fadeSpeed);

                for(int v = 0; v < 4; v++){

                    vertexColors[characterInfo.vertexIndex + v].a = (byte)alphas[i];

                }

                if(alphas[i] >= 255){

                    minRange++;

                }

            }

            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            bool lastCharacterIsInvisible = !textInfo.characterInfo[maxRange -1].isVisible;

            if(alphas[maxRange -1] > alphaThreshold || lastCharacterIsInvisible){

                if(maxRange < textInfo.characterCount){

                    maxRange++;

                }else if(alphas[maxRange -1] >= 255 || lastCharacterIsInvisible){

                    break;

                }

            }

            yield return new WaitForEndOfFrame();

        }

    }

    public void Set_Text(string text){

        preText = "";
        targetText = text;

        Stop();

        tmpro.text = targetText;
        Force_Complete();
        
    }
}
=======
using System.Collections;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using JetBrains.Annotations;
using System;

public class Text_System {
    
    private TextMeshProUGUI tmproUi;
    private TextMeshPro tmproWorld;
    //To put text in the appropiate spot
    public TMP_Text tmpro => tmproUi != null ? tmproUi : tmproWorld;

    public string currentText => tmpro.text;
    //The text to add
    public string targetText {get; private set;} = "";
    //To append onto already existing text, or preText
    public string preText {get; private set;} = "";
    private int preTextLength = 0;

    public string fullTargetText => preText + targetText;

    public enum BuildMethod {instant, gradual, fade}
    public BuildMethod buildMethod = BuildMethod.gradual;

    public Color textColor {get{return tmpro.color;} set{tmpro.color = value;}}

    //Text building speed
    private float baseSpeed = 1f;
    private float speedMultiplier = 1f;
    public float speed{get{return baseSpeed * speedMultiplier;} set{speedMultiplier = value;}}

    //For speeding up characters if the player clicks 
    private int characterMultiplier = 1;
    public int charactersPerCycle{get{return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3;}}

    public bool speedUp = false;

    public Text_System(TextMeshProUGUI tmproUi){

        this.tmproUi = tmproUi;

    }

    public Text_System(TextMeshPro tmproWorld){

        this.tmproWorld = tmproWorld;

    }

    public Coroutine Build(string text){

        preText = "";
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;

    }

    public Coroutine Append(string text){

        preText = tmpro.text;
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;

    }

    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;

    public void Stop(){

        if(!isBuilding)

            return;

        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;

    }

    IEnumerator Building(){

        Prepare();

        switch(buildMethod){

            case BuildMethod.gradual:

                yield return Build_Gradual();
                break;

            case BuildMethod.fade:

                yield return Build_Fade();
                break;

        }

        On_Complete();

    }

    private void On_Complete(){

        buildProcess = null;
        speedUp = false;

    }

    public void Force_Complete(){

        switch(buildMethod){

            case BuildMethod.gradual:

            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
            break;

        case BuildMethod.fade:

            tmpro.ForceMeshUpdate();
            break;

        }
        
        Stop();
        On_Complete();

    }

    private void Prepare(){

        switch(buildMethod){

            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.gradual:
                Prepare_Gradual();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;

        }

    }

    private void Prepare_Instant(){

        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;

    }

    private void Prepare_Gradual(){

        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if(preText != ""){

            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;

        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();

    }

    private void Prepare_Fade(){

        tmpro.text = preText;

        if(preText != ""){

            tmpro.ForceMeshUpdate();
            preTextLength = tmpro.textInfo.characterCount;

        }else{

            preTextLength = 0;

        }

        tmpro.text += targetText;
        tmpro.maxVisibleCharacters = int.MaxValue;
        tmpro.ForceMeshUpdate();

        TMP_TextInfo textInfo = tmpro.textInfo;

        Color visibleColor = new Color(textColor.r, textColor.g, textColor.b, 1);
        Color hiddenColor = new Color(textColor.r, textColor.g, textColor.b, 0);

        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

        for(int i = 0; i < textInfo.characterCount; i++){

            TMP_CharacterInfo characterInfo = textInfo.characterInfo[i];

            if(!characterInfo.isVisible){

                continue;

            }

            if(i < preTextLength){

                for(int v = 0; v < 4; v++){

                    vertexColors[characterInfo.vertexIndex + v] = visibleColor;

                }

            }else{

                for(int v = 0; v < 4; v++){

                    vertexColors[characterInfo.vertexIndex + v] = hiddenColor;

                }

            }

        }

        tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

    }

    private IEnumerator Build_Gradual(){

        while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount){

            tmpro.maxVisibleCharacters += speedUp ? charactersPerCycle * 5 : charactersPerCycle;
            yield return new WaitForSeconds(0.015f / speed);

        }

    }

    private IEnumerator Build_Fade(){

        int minRange = preTextLength;
        int maxRange = minRange +1;

        byte alphaThreshold = 15;
        TMP_TextInfo textInfo = tmpro.textInfo;
        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;
        float[] alphas = new float[textInfo.characterCount];

        while(true){

            float fadeSpeed = ((speedUp ? charactersPerCycle * 5 : charactersPerCycle) * speed) * 2.5f;

            for(int i = minRange; i < maxRange; i++){

                TMP_CharacterInfo characterInfo = textInfo.characterInfo[i];

                if(!characterInfo.isVisible){

                    continue;

                }

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                alphas[i] = Mathf.MoveTowards(alphas[i], 255, fadeSpeed);

                for(int v = 0; v < 4; v++){

                    vertexColors[characterInfo.vertexIndex + v].a = (byte)alphas[i];

                }

                if(alphas[i] >= 255){

                    minRange++;

                }

            }

            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            bool lastCharacterIsInvisible = !textInfo.characterInfo[maxRange -1].isVisible;

            if(alphas[maxRange -1] > alphaThreshold || lastCharacterIsInvisible){

                if(maxRange < textInfo.characterCount){

                    maxRange++;

                }else if(alphas[maxRange -1] >= 255 || lastCharacterIsInvisible){

                    break;

                }

            }

            yield return new WaitForEndOfFrame();

        }

    }

    public void Set_Text(string text){

        preText = "";
        targetText = text;

        Stop();

        tmpro.text = targetText;
        Force_Complete();
        
    }
}
>>>>>>> c8d4f422 (Whole Template)
