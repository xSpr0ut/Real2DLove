<<<<<<< HEAD
using UnityEngine;

public static class Color_Extension{
    
    public static Color Set_Alpha(this Color original, float alpha){

        return new Color(original.r, original.b, original.g, alpha);

    }

    public static Color Get_Color_From_Name(this Color original, string colorName){

        switch(colorName.ToLower()){

            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "blue":
                return Color.blue;
            case "yellow":
                return Color.yellow;
            case "white":
                return Color.white;
            case "black":
                return Color.black;
            case "gray":
                return Color.gray;
            case "cyan":
                return Color.cyan;
            case "magenta":
                return Color.magenta;
            case "orange":
                return new Color(1f, 0.5f, 0f);
            default:
                return Color.clear;

        }

    }

}
=======
using UnityEngine;

public static class Color_Extension{
    
    public static Color Set_Alpha(this Color original, float alpha){

        return new Color(original.r, original.b, original.g, alpha);

    }

    public static Color Get_Color_From_Name(this Color original, string colorName){

        switch(colorName.ToLower()){

            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "blue":
                return Color.blue;
            case "yellow":
                return Color.yellow;
            case "white":
                return Color.white;
            case "black":
                return Color.black;
            case "gray":
                return Color.gray;
            case "cyan":
                return Color.cyan;
            case "magenta":
                return Color.magenta;
            case "orange":
                return new Color(1f, 0.5f, 0f);
            default:
                return Color.clear;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
