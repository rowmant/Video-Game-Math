using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Kiraio.Utility
{
    /// <summary>
    /// Membuat sebuah daftar nama scene yang akan digunakan untuk meload scene.
    /// </summary>
    [CreateAssetMenu(fileName = "SceneList", menuName = "Kiraio/Utility/Generate Scene List")]
    public class SceneList : ScriptableSingleton<SceneList>
    {
        [Tooltip("Daftar nama scene yang ada di Build Settings")]
        [ReadOnly]
        public List<string> m_Scenes;

        void OnEnable()
        {
            SyncSceneList();
        }

        void OnInspectorUpdate()
        {
            SyncSceneList();
        }

        void SyncSceneList()
        {
            m_Scenes = GetScenesName();
        }

        int SceneCount()
        {
            return UnitySceneManager.sceneCountInBuildSettings;
        }

        List<string> GetScenesName()
        {
            List<string> scenes = new List<string>();

            for (int i = 0; i < SceneCount(); i++)
            {
                string sceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)).ToString();

                if (sceneName != string.Empty)
                    scenes.Add(sceneName);

                if (scenes[i] != sceneName)
                    scenes[i] = sceneName;
            }

            while (scenes.Count > SceneCount())
                scenes.RemoveAt(scenes.Count - 1);

            return scenes;
        }
    }
}
