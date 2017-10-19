using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(PrefabSet))]
public class PrefabSetPropertyDrawer : PropertyDrawer {

    private int _index = -1;

    private List<PrefabSet> _prefabSets;
    private string[] _options;

    public PrefabSetPropertyDrawer()
    {
        _prefabSets = new List<PrefabSet>(GameObject.FindObjectsOfType<PrefabSet>());
        _options = new string[_prefabSets.Count + 1];
        _options[0] = "<Not Set>";
        for (int i = 0; i < _prefabSets.Count; i++)
        {
            _options[i + 1] = _prefabSets[i].Name;
        }

    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        //draw the label
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        EditorGUI.indentLevel = 0;

        //Not sure why I need to do this EVERY time, originally I was only calling InitializeIndex if the index was -1
        //but the field was sometimes not propertly updating, so now we just do it every time I guess.
        InitializeIndex(property);
        
        //now draw the popup with the options
        EditorGUI.BeginChangeCheck();
        _index = EditorGUI.Popup(contentPosition, _index, _options);

        //Set the underlying value when we're finally done
        if (EditorGUI.EndChangeCheck())
        {
            if (_index == 0)
            {
                property.objectReferenceValue = null;
            }
            else
            {
                property.objectReferenceValue = _prefabSets[_index - 1];
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        EditorGUI.EndProperty();

        
    }

    private void InitializeIndex(SerializedProperty property)
    {
        PrefabSet currentSet = property.objectReferenceValue as PrefabSet;
        if (currentSet == null)
        {
            _index = 0;
        }
        else
        {
            _index = 0;
            for (int i = 0; i < _options.Length; i++)
            {
                if (_options[i] == currentSet.Name)
                {
                    _index = i;
                    break;
                }
            }
        }
    }
}
