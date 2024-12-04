using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HT
{
    /*[CustomEditor(typeof(Response))]
    public class DialogStatementEditor : Editor
    {
        SerializedProperty responseList;
        SerializedProperty condition;

        public void OnEnable()
        {
            responseList = serializedObject.FindProperty("responses");
            condition = serializedObject.FindProperty("hasResponses");
        }

        public override void OnInspectorGUI()
        {
           
            DialogStatement code = (DialogStatement)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(condition);

            if (condition.boolValue)
            {
                EditorGUILayout.PropertyField(responseList);
            }

            serializedObject.ApplyModifiedProperties();

             base.OnInspectorGUI();
        }
    }*/
}
