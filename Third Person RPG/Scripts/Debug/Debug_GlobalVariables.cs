using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Debug_EventFloat))]
public class Debug_GlobalVariables : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Debug_EventFloat gf = (Debug_EventFloat) target;
        if (GUILayout.Button("Apply Float"))
        {
            gf.SetValue();
        }
    }

}
