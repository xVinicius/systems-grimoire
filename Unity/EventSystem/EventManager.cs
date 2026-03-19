using System.Collections.Generic;
using UnityEngine;

namespace SystemsGrimoire {
    /// <summary>
    /// MonoBehaviour base that manages a collection of EventListeners.
    /// Automatically handles registration/unregistration on enable/disable/destroy.
    ///
    /// Usage:
    ///     public class EnemyEventManager : EventManager&lt;EnemyEventChannel&gt;
    ///     {
    ///         // Add listeners via RegisterListener() or serialize them
    ///     }
    /// </summary>
    public abstract class EventManager<T> : MonoBehaviour {
        protected List<EventListener<T>> m_EventListeners = new();

        public void RegisterListener(EventListener<T> listener) {
            if (!m_EventListeners.Contains(listener)) {
                m_EventListeners.Add(listener);
                if (gameObject.activeInHierarchy)
                    listener.OnEnable();
            }
        }

        public void UnregisterListener(EventListener<T> listener) {
            if (m_EventListeners.Contains(listener)) {
                listener.OnDisable();
                m_EventListeners.Remove(listener);
            }
        }

        private void OnEnable() {
            foreach (var listener in m_EventListeners) listener.OnEnable();
        }

        private void OnDisable() {
            foreach (var listener in m_EventListeners) listener.OnDisable();
        }

        private void OnDestroy() {
            foreach (var listener in m_EventListeners) listener.OnDestroy();
        }
    }

    public abstract class EventManager<T, T1> : MonoBehaviour {
        protected List<EventListener<T, T1>> m_EventListeners = new();

        private void OnEnable() {
            foreach (var listener in m_EventListeners) listener.OnEnable();
        }

        private void OnDisable() {
            foreach (var listener in m_EventListeners) listener.OnDisable();
        }

        private void OnDestroy() {
            foreach (var listener in m_EventListeners) listener.OnDestroy();
        }
    }

    public abstract class EventManager<T, T1, T2> : MonoBehaviour {
        protected List<EventListener<T, T1, T2>> m_EventListeners = new();

        private void OnEnable() {
            foreach (var listener in m_EventListeners) listener.OnEnable();
        }

        private void OnDisable() {
            foreach (var listener in m_EventListeners) listener.OnDisable();
        }

        private void OnDestroy() {
            foreach (var listener in m_EventListeners) listener.OnDestroy();
        }
    }

    public abstract class EventManager<T, T1, T2, T3> : MonoBehaviour {
        protected List<EventListener<T, T1, T2, T3>> m_EventListeners = new();

        private void OnEnable() {
            foreach (var listener in m_EventListeners) listener.OnEnable();
        }

        private void OnDisable() {
            foreach (var listener in m_EventListeners) listener.OnDisable();
        }

        private void OnDestroy() {
            foreach (var listener in m_EventListeners) listener.OnDestroy();
        }
    }
}
