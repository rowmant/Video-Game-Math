#if UNITY_EDITOR
using UnityEditor;

namespace Kiraio.Utility
{
    [CustomEditor(typeof(LoadScene))]
    public class LoadSceneEditor : Editor
    {
        static readonly string[] m_ExcludeProperty = new string[] { "m_Script" };
        SerializedProperty m_ScenesProperty;
        string[] m_Choices;
        int m_ChoiceIndex = 0;
        LoadScene m_LoadScene;

        void OnEnable()
        {
            // Setup the SerializedProperties.
            m_ScenesProperty = serializedObject.FindProperty("m_SelectedScene");
            SyncSceneValue();
        }

        void OnInspectorUpdate()
        {
            SyncSceneValue();
        }

        void SyncSceneValue()
        {
            m_LoadScene = (LoadScene)target;
            m_Choices = m_LoadScene.m_Scenes.ToArray();
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            m_ChoiceIndex = EditorGUILayout.Popup("Target Scene", m_ChoiceIndex, m_Choices);
            if (m_ChoiceIndex < 0) m_ChoiceIndex = 0;
            m_ScenesProperty.stringValue = m_Choices[m_ChoiceIndex];

            DrawPropertiesExcluding(serializedObject, m_ExcludeProperty);

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
