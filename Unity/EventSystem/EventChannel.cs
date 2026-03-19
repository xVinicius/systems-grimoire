using System.Collections.Generic;
using UnityEngine;

namespace SystemsGrimoire {
    /// <summary>
    /// ScriptableObject-based event channel for decoupled pub/sub communication.
    /// Create concrete channels as ScriptableObject assets and wire them in the Inspector.
    ///
    /// To create a concrete channel:
    ///     [CreateAssetMenu(menuName = "Events/Float Event")]
    ///     public class FloatEventChannel : EventChannel&lt;FloatEventChannel&gt; { }
    ///
    /// To raise:
    ///     myChannel.Raise(42f);
    ///
    /// Supports 1-4 parameter variants.
    /// </summary>
    public abstract class EventChannel<T> : ScriptableObject {
        [SerializeField] private List<EventListener<T>> m_Listeners = new();

        public void Raise(T arg) {
            m_Listeners.RemoveAll(x => x == null);

            for (int i = m_Listeners.Count - 1; i >= 0; i--) {
                m_Listeners[i].Response?.Invoke(arg);
            }
        }

        public void RegisterListener(EventListener<T> listener) {
            if (!m_Listeners.Contains(listener))
                m_Listeners.Add(listener);
        }

        public void UnregisterListener(EventListener<T> listener) {
            m_Listeners.Remove(listener);
        }
    }

    public abstract class EventChannel<T, T1> : ScriptableObject {
        [SerializeField] private List<EventListener<T, T1>> m_Listeners = new();

        public void Raise(T arg0, T1 arg1) {
            m_Listeners.RemoveAll(x => x == null);

            for (int i = m_Listeners.Count - 1; i >= 0; i--) {
                m_Listeners[i].Response?.Invoke(arg0, arg1);
            }
        }

        public void RegisterListener(EventListener<T, T1> listener) {
            if (!m_Listeners.Contains(listener))
                m_Listeners.Add(listener);
        }

        public void UnregisterListener(EventListener<T, T1> listener) {
            m_Listeners.Remove(listener);
        }
    }

    public abstract class EventChannel<T, T1, T2> : ScriptableObject {
        [SerializeField] private List<EventListener<T, T1, T2>> m_Listeners = new();

        public void Raise(T arg0, T1 arg1, T2 arg2) {
            m_Listeners.RemoveAll(x => x == null);

            for (int i = m_Listeners.Count - 1; i >= 0; i--) {
                m_Listeners[i].Response?.Invoke(arg0, arg1, arg2);
            }
        }

        public void RegisterListener(EventListener<T, T1, T2> listener) {
            if (!m_Listeners.Contains(listener))
                m_Listeners.Add(listener);
        }

        public void UnregisterListener(EventListener<T, T1, T2> listener) {
            m_Listeners.Remove(listener);
        }
    }

    public abstract class EventChannel<T, T1, T2, T3> : ScriptableObject {
        [SerializeField] private List<EventListener<T, T1, T2, T3>> m_Listeners = new();

        public void Raise(T arg0, T1 arg1, T2 arg2, T3 arg3) {
            m_Listeners.RemoveAll(x => x == null);

            for (int i = m_Listeners.Count - 1; i >= 0; i--) {
                m_Listeners[i].Response?.Invoke(arg0, arg1, arg2, arg3);
            }
        }

        public void RegisterListener(EventListener<T, T1, T2, T3> listener) {
            if (!m_Listeners.Contains(listener))
                m_Listeners.Add(listener);
        }

        public void UnregisterListener(EventListener<T, T1, T2, T3> listener) {
            m_Listeners.Remove(listener);
        }
    }
}
