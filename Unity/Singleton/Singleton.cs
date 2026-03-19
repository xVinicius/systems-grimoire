using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SystemsGrimoire {
    /// <summary>
    /// Thread-safe generic Singleton for MonoBehaviours.
    /// Features:
    /// - Lazy initialization with double-checked locking
    /// - Auto-creates instance if not found in scene
    /// - Survives scene loads via DontDestroyOnLoad
    /// - Async initialization support via GetInstanceAsync()
    /// - Built-in colored logging (editor only)
    ///
    /// Usage:
    ///     public class AudioManager : Singleton&lt;AudioManager&gt; {
    ///         protected override void Initialize() {
    ///             base.Initialize();
    ///             // setup audio system...
    ///         }
    ///
    ///         public void PlaySFX(AudioClip clip) { ... }
    ///     }
    ///
    ///     // Access from anywhere:
    ///     AudioManager.Instance.PlaySFX(clip);
    ///
    ///     // Or await initialization:
    ///     var manager = await AudioManager.GetInstanceAsync();
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T s_Instance;
        private static readonly object s_Lock = new();
        private static bool s_ApplicationIsQuitting;
        private static TaskCompletionSource<bool> s_InitializationTask;
        private bool m_Initialized;

        [field: SerializeField] public bool ShowLogs { get; private set; }
        public Color LogColor => m_Color;
        [SerializeField] private Color m_Color = Color.white;
        protected string m_HexColor = "FFFFFF";

        public static T Instance {
            get {
                if (s_ApplicationIsQuitting)
                    return s_Instance;

                lock (s_Lock) {
                    if (s_Instance != null) return s_Instance;

                    s_Instance = (T)FindObjectOfType(typeof(T), true);

                    if (s_Instance != null) return s_Instance;

                    var singleton = new GameObject();
                    s_Instance = singleton.AddComponent<T>();
                    singleton.name = $"(singleton) {typeof(T)}";
                    return s_Instance;
                }
            }
        }

        public static async Task<T> GetInstanceAsync() {
            var instance = Instance;

            if (instance is not Singleton<T> singleton || singleton.m_Initialized) return instance;
            s_InitializationTask ??= new TaskCompletionSource<bool>();
            await s_InitializationTask.Task;

            return instance;
        }

        public virtual void Awake() {
            if (s_Instance != null && s_Instance != this) {
                DestroyImmediate(gameObject);
            }
            else {
                s_Instance = this as T;
                if (s_Instance != null && Application.isPlaying) {
                    transform.SetParent(null);
                    DontDestroyOnLoad(gameObject);

                    var managersObject = GameObject.Find("Managers");
                    if (managersObject == null) {
                        managersObject = new GameObject("Managers");
                        DontDestroyOnLoad(managersObject);
                    }

                    transform.SetParent(managersObject.transform);
                }

                Initialize();
            }
        }

        protected virtual void Initialize() {
            m_Initialized = true;
            s_InitializationTask?.TrySetResult(true);
            SetColor(m_Color);
        }

        protected virtual void OnApplicationQuit() {
            s_ApplicationIsQuitting = true;
        }

        protected virtual void OnDestroy() {
            if (s_Instance != this) return;
            s_ApplicationIsQuitting = false;
            s_Instance = null;
            m_Initialized = false;
            s_InitializationTask = null;
        }

        public bool IsReady() => m_Initialized;

        protected void SetColor(Color c) {
            m_HexColor = ColorUtility.ToHtmlStringRGB(c);
        }

        protected void SetColor(string logColor) {
            if (ColorUtility.TryParseHtmlString(logColor, out var hc)) SetColor(hc);
        }

        protected static void Log(object message) {
            var instance = Instance as Singleton<T>;
            if (instance == null || !instance.ShowLogs) return;

#if UNITY_EDITOR
            Debug.LogFormat(LogType.Log, LogOption.None, Instance,
                "<color=#{0}>({1})</color> {2}", instance.m_HexColor, typeof(T).Name, message);
#else
            Debug.Log(message);
#endif
        }

        protected static void LogWarning(object message) {
            var instance = Instance as Singleton<T>;
            if (instance == null || !instance.ShowLogs) return;

#if UNITY_EDITOR
            Debug.LogFormat(LogType.Warning, LogOption.None, Instance,
                "<color=#{0}>({1})</color> {2}", instance.m_HexColor, typeof(T).Name, message);
#else
            Debug.LogWarning(message);
#endif
        }

        protected static void LogError(object message) {
            var instance = Instance as Singleton<T>;
            if (instance == null) return;

#if UNITY_EDITOR
            Debug.LogFormat(LogType.Error, LogOption.None, Instance,
                "<color=#{0}>({1})</color> {2}", instance.m_HexColor, typeof(T).Name, message);
#else
            Debug.LogError(message);
#endif
        }

        protected static void LogException(Exception e) {
            var instance = Instance as Singleton<T>;
            if (instance == null) return;
            Debug.LogException(e, Instance);
        }
    }
}
