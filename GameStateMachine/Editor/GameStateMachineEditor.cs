using System;
using System.Linq;
using UnityEditor;

namespace D2D.Core
{
    [CustomEditor(typeof(GameStateMachine))]
    public class GameStateMachineEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // ...
            
            base.OnInspectorGUI();
            
            GameStateMachine gsm = null;
            
            try
            {
                gsm = FindObjectOfType<GameStateMachine>();
            }
            catch (Exception e)
            {
                return;
            }
            
            if (gsm == null)
                return;

            if (gsm.PushedStatesNames.Count > 0)
            {
                EditorGUILayout.LabelField("Last state: ");
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.LabelField(gsm.PushedStatesNames.Last());
                EditorGUI.EndDisabledGroup();
                
                EditorGUILayout.Space();
                
                EditorGUILayout.LabelField("States stack: ");
            
                EditorGUI.BeginDisabledGroup(true);
                var lines = gsm.PushedStatesNames;
                int i = 1;
                foreach (var line in lines)
                {
                    EditorGUILayout.LabelField($"{i}. {line}");
                    i++;
                }
                EditorGUI.EndDisabledGroup();
            }
            // else
            // {
            //     if (!Application.isPlaying)
            //         return;
            //     
            //     EditorGUI.BeginDisabledGroup(true);
            //     EditorGUILayout.LabelField("For now it is empty...");
            //     EditorGUI.EndDisabledGroup();
            // }
        }
    }
}