using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GenerateEnum : EditorWindow
{
    private string enumName = "EnumName";
    private static EmoteDatabase emoteDatabase;

    [MenuItem("LudumDare_2020/GenerateEnum")]
    private static void ShowWindow()
    {
        var window = GetWindow<GenerateEnum>();
        window.titleContent = new GUIContent("GenerateEnum");
        window.Show();
    }

    private void OnGUI()
    {
        enumName = EditorGUILayout.TextField("New Enum Name", enumName);
        emoteDatabase = (EmoteDatabase)EditorGUILayout.ObjectField(emoteDatabase, typeof(EmoteDatabase), true);

        if(GUILayout.Button("Generate From Directory"))
        {
            string selectedFolder = EditorUtility.OpenFolderPanel("Select enum files root", Application.dataPath, null);
            if(Directory.Exists(selectedFolder))
            {
                Generate(Directory.GetFiles(selectedFolder));
            }
        }

        if(GUILayout.Button("Assign sprites From Directory"))
        {
            string selectedFolder = EditorUtility.OpenFolderPanel("Select sprites folder", Application.dataPath, null);
            if(Directory.Exists(selectedFolder))
            {
                AssignSprites(selectedFolder);
            }
        }
    }

    private void Generate(string[] enumEntries)
    {
        string filePathAndName = "Assets/Scripts/Editor/Enums/" + enumName + ".cs"; //The folder Scripts/Enums/ is expected to exist
        using ( StreamWriter streamWriter = new StreamWriter( filePathAndName ) )
        {
            int index = 0;
            streamWriter.WriteLine( "public enum " + enumName );
            streamWriter.WriteLine( "{" );
            for( int i = 0; i < enumEntries.Length; i++ )
            {
                if(!enumEntries[i].Contains("meta"))
                {
                    streamWriter.WriteLine( $"\t{Path.GetFileNameWithoutExtension(enumEntries[i])} = {index}," );
                    index ++;
                }
            }
            streamWriter.WriteLine( "}" );
        }
        AssetDatabase.Refresh();
    }

    public static void AssignSprites(string folderName)
    {
        string[] imgFiles = Directory.GetFiles(folderName);
        List<Sprite> spriteList = new List<Sprite>();
        for (int i = 0; i < imgFiles.Length; i++)
        {
            if(imgFiles[i].EndsWith(".png"))
            {
                Sprite s = AssetDatabase.LoadAssetAtPath(imgFiles[i], typeof(Sprite)) as Sprite;
                spriteList.Add(s);
            }
        }

        emoteDatabase.SetSprites(spriteList);
    }
}
