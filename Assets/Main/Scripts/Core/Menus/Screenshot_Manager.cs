<<<<<<< HEAD
using UnityEngine;

public class Screenshot_Manager : MonoBehaviour{
    
    public static Texture2D Capture_Screenshot(int width, int height, float supersize = 1, string filePath = "") => Capture_Screenshot(Camera.main, width, height, supersize, filePath);

    public static Texture2D Capture_Screenshot(Camera camera, int width, int height, float supersize = 1, string filePath = ""){

        if(supersize != 1){

            width = Mathf.RoundToInt(width * supersize);
            height = Mathf.RoundToInt(height * supersize);

        }

        RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 32);
        camera.targetTexture = renderTexture;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);

        camera.Render();

        RenderTexture.active = renderTexture;

        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);

        if(filePath != ""){

            Save_Screenshot_To_File(screenshot, filePath);

        }

        return screenshot;

    }

    public enum Image_Type{PNG, JPG}

    public static void Save_Screenshot_To_File(Texture2D screenshot, string filePath, Image_Type fileType = Image_Type.PNG){

        byte[] bytes = new byte[0];
        string extension = "";

        switch(fileType){

            case Image_Type.PNG:

                bytes = screenshot.EncodeToPNG();
                extension = ".png";
                break;

            case Image_Type.JPG:

                bytes = screenshot.EncodeToJPG();
                extension = ".jpg";
                break;

        }

        if(!filePath.Contains('.')){

            filePath = filePath + extension;

        }

        File_Manager.Try_Create_Directory_From_Path(filePath);

        System.IO.File.WriteAllBytes(filePath, bytes);

    }

}
=======
using UnityEngine;

public class Screenshot_Manager : MonoBehaviour{
    
    public static Texture2D Capture_Screenshot(int width, int height, float supersize = 1, string filePath = "") => Capture_Screenshot(Camera.main, width, height, supersize, filePath);

    public static Texture2D Capture_Screenshot(Camera camera, int width, int height, float supersize = 1, string filePath = ""){

        if(supersize != 1){

            width = Mathf.RoundToInt(width * supersize);
            height = Mathf.RoundToInt(height * supersize);

        }

        RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 32);
        camera.targetTexture = renderTexture;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);

        camera.Render();

        RenderTexture.active = renderTexture;

        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);

        if(filePath != ""){

            Save_Screenshot_To_File(screenshot, filePath);

        }

        return screenshot;

    }

    public enum Image_Type{PNG, JPG}

    public static void Save_Screenshot_To_File(Texture2D screenshot, string filePath, Image_Type fileType = Image_Type.PNG){

        byte[] bytes = new byte[0];
        string extension = "";

        switch(fileType){

            case Image_Type.PNG:

                bytes = screenshot.EncodeToPNG();
                extension = ".png";
                break;

            case Image_Type.JPG:

                bytes = screenshot.EncodeToJPG();
                extension = ".jpg";
                break;

        }

        if(!filePath.Contains('.')){

            filePath = filePath + extension;

        }

        File_Manager.Try_Create_Directory_From_Path(filePath);

        System.IO.File.WriteAllBytes(filePath, bytes);

    }

}
>>>>>>> c8d4f422 (Whole Template)
