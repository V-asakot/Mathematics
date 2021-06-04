using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GameMain))]
public class MainGameEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameMain main = (GameMain)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("StartGame"))
        {
            main.StartGame(main.gameId);
        }

        if (GUILayout.Button("EndGame"))
        {
            main.EndGame();
        }
        if (GUILayout.Button("Round"))
        {
            main.Answer(true);
        }

        GUILayout.EndHorizontal();
    }
}
