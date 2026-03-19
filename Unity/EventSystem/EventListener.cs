using System;
using UnityEngine.Events;

namespace SystemsGrimoire {
    /// <summary>
    /// Serializable listener that connects to an EventChannel via the Inspector.
    /// Handles registration/unregistration on MonoBehaviour lifecycle events.
    ///
    /// Usage (as a serialized field on a MonoBehaviour):
    ///     [SerializeField] private EventListener&lt;FloatEventChannel&gt; onHealthChanged;
    ///
    ///     void OnEnable()  => onHealthChanged.OnEnable();
    ///     void OnDisable() => onHealthChanged.OnDisable();
    ///     void OnDestroy() => onHealthChanged.OnDestroy();
    /// </summary>
    [Serializable]
    public abstract class EventListenerBase {
        public abstract void OnEnable();
        public abstract void OnDisable();
        public abstract void OnDestroy();
    }

    [Serializable]
    public sealed class EventListener<T> : EventListenerBase {
        public EventChannel<T> Channel;
        public UnityEvent<T> Response;
        public bool UnregisterOnDisable = true;

        public override void OnEnable() => Channel.RegisterListener(this);

        public override void OnDisable() {
            if (UnregisterOnDisable)
                Channel.UnregisterListener(this);
        }

        public override void OnDestroy() => Channel.UnregisterListener(this);
    }

    [Serializable]
    public sealed class EventListener<T, T1> : EventListenerBase {
        public EventChannel<T, T1> Channel;
        public UnityEvent<T, T1> Response;
        public bool UnregisterOnDisable = true;

        public override void OnEnable() => Channel.RegisterListener(this);

        public override void OnDisable() {
            if (UnregisterOnDisable)
                Channel.UnregisterListener(this);
        }

        public override void OnDestroy() => Channel.UnregisterListener(this);
    }

    [Serializable]
    public sealed class EventListener<T, T1, T2> : EventListenerBase {
        public EventChannel<T, T1, T2> Channel;
        public UnityEvent<T, T1, T2> Response;
        public bool UnregisterOnDisable = true;

        public override void OnEnable() => Channel.RegisterListener(this);

        public override void OnDisable() {
            if (UnregisterOnDisable)
                Channel.UnregisterListener(this);
        }

        public override void OnDestroy() => Channel.UnregisterListener(this);
    }

    [Serializable]
    public sealed class EventListener<T, T1, T2, T3> : EventListenerBase {
        public EventChannel<T, T1, T2, T3> Channel;
        public UnityEvent<T, T1, T2, T3> Response;
        public bool UnregisterOnDisable = true;

        public override void OnEnable() => Channel.RegisterListener(this);

        public override void OnDisable() {
            if (UnregisterOnDisable)
                Channel.UnregisterListener(this);
        }

        public override void OnDestroy() => Channel.UnregisterListener(this);
    }
}
