using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kiraio.Utility
{
    [AddComponentMenu("Kiraio/Utility/Load Scene")]
    public class LoadScene : MonoBehaviour
    {
        [Tooltip("Mode load scene")]
        [SerializeField] LoadSceneMode m_LoadMode;
        // [SerializeField] LoadMode m_LoadMode = LoadMode.Single;
        // public enum LoadMode { Additive, Single }
        [Tooltip("Perlakukan Game Object ini sebagai apa?")]
        [SerializeField, Range(0, 5f)] float m_LoadDelay;

        SceneManager m_SceneManager;
        [HideInInspector] public string m_SelectedScene;
        public List<string> m_Scenes { get; private set; }

        [Header("EVENTS")]
        [SerializeField] UnityEvent m_ClickEvents;
        [SerializeField] UnityEvent m_SceneLoaded;

        #region Editor
        void OnValidate()
        {
            Init();
        }

        void OnInspectorUpdate()
        {
            Init();
        }
        #endregion

        #region Initialization
        void Init()
        {
            m_SceneManager = FindObjectOfType<SceneManager>();
            if (m_SceneManager == null)
            {
                m_SceneManager = SceneManager.Instance; // Create one if missing
                Debug.LogWarning("Scene Manager doesn\'t exist! Create one...");
            }

            if (m_SceneManager.m_SceneListAsset != null)
                m_Scenes = m_SceneManager.m_SceneListAsset.m_Scenes;
            else
                Debug.LogError("Scene List asset haven\'t been assigned in Scene Manager, please assign it. \n If you haven't made one yet, create one by Right Click in Project Tab > Create > Kiraio > Generate Scene List.");
        }
        #endregion

        #region Main
        void Start()
        {
            if (TryGetComponent(out Button btn))
                btn.onClick.AddListener(() =>
                {
                    m_ClickEvents?.Invoke();
                    btn.onClick.RemoveAllListeners();
                });
        }

        public void Load()
        {
            m_SceneManager.Load(m_SelectedScene, m_LoadMode);
        }
        #endregion
    }
}
