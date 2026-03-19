using System;
using System.Collections.Generic;

namespace SystemsGrimoire {
    /// <summary>
    /// Reactive property wrapper that raises events when its value changes.
    /// Provides three levels of change notification:
    /// - OnValueChanged: no parameters, simple notification
    /// - OnValueChangedTo: passes the new value
    /// - OnValueChangedFromTo: passes both old and new values
    ///
    /// Usage:
    ///     public Observable&lt;float&gt; Health;
    ///
    ///     // Subscribe
    ///     character.Health.OnValueChangedTo += newHealth => healthBar.SetFill(newHealth);
    ///
    ///     // Trigger
    ///     character.Health.Value = 50f; // fires all subscribed events
    /// </summary>
    public struct Observable<T> {
        public Action OnValueChanged;
        public Action<T> OnValueChangedTo;
        public Action<T, T> OnValueChangedFromTo;

        private T m_Value;

        public T Value {
            get => m_Value;
            set {
                if (EqualityComparer<T>.Default.Equals(value, m_Value)) return;

                var previousValue = m_Value;
                m_Value = value;
                OnValueChanged?.Invoke();
                OnValueChangedTo?.Invoke(m_Value);
                OnValueChangedFromTo?.Invoke(previousValue, m_Value);
            }
        }
    }
}
