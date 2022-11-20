using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Kiraio.Utility
{
    [AddComponentMenu("Kiraio/Managers/Scene Manager")]
    public class SceneManager : MonoSingleton<SceneManager>
    {
        [SerializeField] SceneList _sceneList;
        public SceneList m_SceneListAsset => _sceneList;

        IEnumerator LoadSceneAsynchronously(string name, int index, LoadSceneMode loadSceneMode, float delay)
        {
            // Time.timeScale = 1;
            AsyncOperation loading = new AsyncOperation();

            if (name != null && index <= -1) // Load using name
                loading = UnitySceneManager.LoadSceneAsync(name, loadSceneMode);
            else if (name == null && index <= -1) // If name null, throw error
                throw new NullReferenceException("Invalid scene name!");
            else if (name == null && index > -1) // Load using scene index
                loading = UnitySceneManager.LoadSceneAsync(index, loadSceneMode);
            else if (name == null && index <= -1)
                throw new ArgumentOutOfRangeException($"index = {index}", "Index out of Range!");

            loading.allowSceneActivation = false;

            while (!loading.isDone)
            {
                if (loading.progress >= .9f)
                    loading.allowSceneActivation = true;

                yield return new WaitForSecondsRealtime(delay);
                // yield return null;
            }
        }

        public void Load(string name, LoadSceneMode loadSceneMode, float delay = 0)
        {
            StartCoroutine(LoadSceneAsynchronously(name, -1, loadSceneMode, delay));
        }

        public void Load(int index, LoadSceneMode loadSceneMode, float delay = 0)
        {
            StartCoroutine(LoadSceneAsynchronously(null, index, loadSceneMode, delay));
        }

        public void Restart(float delay = 0)
        {
            StartCoroutine(LoadSceneAsynchronously(null, UnitySceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single, delay));
        }
    }
}
