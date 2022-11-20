#if UNITY_EDITOR
using UnityEditor;

namespace Kiraio.Utility
{
    [CustomEditor(typeof(SceneList))]
    public class SceneListEditor : Editor
    {
        static readonly string[] m_ExcludeProperty = new string[] { "m_Script" };
        SceneList m_SceneList;

        void OnEnable()
        {
            m_SceneList = (SceneList)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.HelpBox("Jika daftar nama tidak ter-update, silahkan hapus file ini dan buat lagi dengan cara Klik Kanan di Project Tab > Create > Kiraio > Utility > Generate Scene List", MessageType.Info);

            EditorGUILayout.Space(10);
            // DrawPropertiesExcluding(serializedObject, m_ExcludeProperty);
            EditorGUILayout.LabelField("SCENES");

            for (int i = 0; i < m_SceneList.m_Scenes.Count; i++)
                EditorGUILayout.LabelField($"◘ {m_SceneList.m_Scenes[i]}");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
