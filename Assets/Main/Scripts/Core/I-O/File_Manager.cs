<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class File_Manager{

    private const string key ="EncryptionKey";
    
    //Reading dialog files
    public static List<string> Read_Text_File(string filePath, bool includeBlankLines = true){

        if (!filePath.StartsWith('/')){

            filePath = File_Paths.root + filePath;

        }

        List<string> lines = new List<string>();

        try{

            using (StreamReader sr = new StreamReader(filePath)){

                while(!sr.EndOfStream){

                    string line = sr.ReadLine();

                    if(includeBlankLines || !string.IsNullOrWhiteSpace(line)){

                        lines.Add(line);

                    }

                }

            }

        }catch(FileNotFoundException ex){

            Debug.LogError($"What file? '{ex.FileName}' doesn't exist.");

        }catch(DirectoryNotFoundException){

            Debug.LogError($"What file?");

        }

        return lines;

    }

    public static List<String> Read_Text_Asset(string filePath, bool includeBlankLines = true){

        TextAsset asset = Resources.Load<TextAsset>(filePath);

        if(asset == null){

            Debug.LogError($"What asset? '{filePath}' doesn't exist.");
            return null;

        }

        return Read_Text_Asset(asset, includeBlankLines);

    }

    public static List<string> Read_Text_Asset(TextAsset asset, bool includeBlankLines = true){

        List<string> lines = new List<string>();

        using(StringReader sr = new StringReader(asset.text)){

            while(sr.Peek() > -1){

                string line = sr.ReadLine();

                if(includeBlankLines || !string.IsNullOrWhiteSpace(line)){

                    lines.Add(line);

                }

            }

        }

        return lines;

    }

    public static bool Try_Create_Directory_From_Path(string path){

        if(Directory.Exists(path) || File.Exists(path)){

            return true;

        }

        if(path.Contains(".")){

            path = Path.GetDirectoryName(path);

            if(Directory.Exists(path)){

                return true;

            }

        }

        if(path == string.Empty){

            return false;

        }

        try{

            Directory.CreateDirectory(path);
            return true;

        }catch(System.Exception e){

            Debug.LogError($"AHHHH Can't make directory! {e}!");
            return false;

        }

    }

    public static void Save(string filePath, string JSONData, bool encrypt = false){

        if(!Try_Create_Directory_From_Path(filePath)){

            Debug.LogError($"AHHHHHH CAN'T SAVE {filePath} I'M SORRY DON'T KILL ME.");
            return;

        }

        if(encrypt){

            byte[] dataBytes = Encoding.UTF8.GetBytes(JSONData);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encryptedBytes = XOR(dataBytes, keyBytes);

            File.WriteAllBytes(filePath, encryptedBytes);

        }else{

            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(JSONData);
            streamWriter.Close();

        }

    }   

    public static T Load<T>(string filePath, bool encrypt = false){

        if(File.Exists(filePath)){

            if(encrypt){

                byte[] encryptedBytes = File.ReadAllBytes(filePath);
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] decryptedData = XOR(encryptedBytes, keyBytes);

                string decryptedString = Encoding.UTF8.GetString(decryptedData);
                return JsonUtility.FromJson<T>(decryptedString);

            }else{

                string JSONData = File.ReadAllLines(filePath)[0];
                return JsonUtility.FromJson<T>(JSONData);

            }

        }else{

            Debug.LogError($"{filePath} does not exist :p");
            return default(T);

        }

    }

    private static byte[] XOR(byte[] input, byte[] key){

        byte[] output = new byte[input.Length];

        for(int i = 0; i < input.Length; i++){

            output[i] = (byte)(input[i] ^ key[i % key.Length]);

        }

        return output;

    }

}
=======
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class File_Manager{

    private const string key ="EncryptionKey";
    
    //Reading dialog files
    public static List<string> Read_Text_File(string filePath, bool includeBlankLines = true){

        if (!filePath.StartsWith('/')){

            filePath = File_Paths.root + filePath;

        }

        List<string> lines = new List<string>();

        try{

            using (StreamReader sr = new StreamReader(filePath)){

                while(!sr.EndOfStream){

                    string line = sr.ReadLine();

                    if(includeBlankLines || !string.IsNullOrWhiteSpace(line)){

                        lines.Add(line);

                    }

                }

            }

        }catch(FileNotFoundException ex){

            Debug.LogError($"What file? '{ex.FileName}' doesn't exist.");

        }catch(DirectoryNotFoundException){

            Debug.LogError($"What file?");

        }

        return lines;

    }

    public static List<String> Read_Text_Asset(string filePath, bool includeBlankLines = true){

        TextAsset asset = Resources.Load<TextAsset>(filePath);

        if(asset == null){

            Debug.LogError($"What asset? '{filePath}' doesn't exist.");
            return null;

        }

        return Read_Text_Asset(asset, includeBlankLines);

    }

    public static List<string> Read_Text_Asset(TextAsset asset, bool includeBlankLines = true){

        List<string> lines = new List<string>();

        using(StringReader sr = new StringReader(asset.text)){

            while(sr.Peek() > -1){

                string line = sr.ReadLine();

                if(includeBlankLines || !string.IsNullOrWhiteSpace(line)){

                    lines.Add(line);

                }

            }

        }

        return lines;

    }

    public static bool Try_Create_Directory_From_Path(string path){

        if(Directory.Exists(path) || File.Exists(path)){

            return true;

        }

        if(path.Contains(".")){

            path = Path.GetDirectoryName(path);

            if(Directory.Exists(path)){

                return true;

            }

        }

        if(path == string.Empty){

            return false;

        }

        try{

            Directory.CreateDirectory(path);
            return true;

        }catch(System.Exception e){

            Debug.LogError($"AHHHH Can't make directory! {e}!");
            return false;

        }

    }

    public static void Save(string filePath, string JSONData, bool encrypt = false){

        if(!Try_Create_Directory_From_Path(filePath)){

            Debug.LogError($"AHHHHHH CAN'T SAVE {filePath} I'M SORRY DON'T KILL ME.");
            return;

        }

        if(encrypt){

            byte[] dataBytes = Encoding.UTF8.GetBytes(JSONData);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encryptedBytes = XOR(dataBytes, keyBytes);

            File.WriteAllBytes(filePath, encryptedBytes);

        }else{

            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(JSONData);
            streamWriter.Close();

        }

    }   

    public static T Load<T>(string filePath, bool encrypt = false){

        if(File.Exists(filePath)){

            if(encrypt){

                byte[] encryptedBytes = File.ReadAllBytes(filePath);
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] decryptedData = XOR(encryptedBytes, keyBytes);

                string decryptedString = Encoding.UTF8.GetString(decryptedData);
                return JsonUtility.FromJson<T>(decryptedString);

            }else{

                string JSONData = File.ReadAllLines(filePath)[0];
                return JsonUtility.FromJson<T>(JSONData);

            }

        }else{

            Debug.LogError($"{filePath} does not exist :p");
            return default(T);

        }

    }

    private static byte[] XOR(byte[] input, byte[] key){

        byte[] output = new byte[input.Length];

        for(int i = 0; i < input.Length; i++){

            output[i] = (byte)(input[i] ^ key[i % key.Length]);

        }

        return output;

    }

}
>>>>>>> c8d4f422 (Whole Template)
