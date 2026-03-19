using System;
using System.Collections;
using System.Collections.Generic;

namespace SystemsGrimoire {
    /// <summary>
    /// Reactive list wrapper that raises events when the collection is modified.
    /// Implements IList&lt;T&gt; so it can be used as a drop-in replacement for List&lt;T&gt;.
    ///
    /// Events:
    /// - OnAnyValueChanged: fired on any mutation (add, remove, clear, set)
    /// - OnAnyValueChangedTo: passes the current list state
    /// - OnAnyValueChangedFromTo: passes previous and current list state
    /// - OnSpecificValueChanged: passes the changed item and its index
    ///
    /// Usage:
    ///     public ObservableList&lt;Item&gt; Inventory = new(new List&lt;Item&gt;());
    ///
    ///     Inventory.OnSpecificValueChanged += (item, index) => UpdateSlot(item, index);
    ///     Inventory.Add(newItem); // fires events
    /// </summary>
    public struct ObservableList<T> : IList<T> {
        public Action OnAnyValueChanged;
        public Action<IList<T>> OnAnyValueChangedTo;
        public Action<IList<T>, IList<T>> OnAnyValueChangedFromTo;
        public Action<T, int> OnSpecificValueChanged;

        private IList<T> m_List;

        public ObservableList(IList<T> list = null) {
            m_List = list ?? new List<T>();
            OnAnyValueChangedTo = default;
            OnAnyValueChanged = default;
            OnAnyValueChangedFromTo = default;
            OnSpecificValueChanged = default;
        }

        public T this[int index] {
            get => m_List[index];
            set {
                var previousValue = m_List;
                m_List[index] = value;
                Invoke(previousValue, m_List, value, index);
            }
        }

        public void Invoke(IList<T> previousValue, IList<T> newValue, T newItemValue = default, int itemIndex = 0) {
            OnAnyValueChanged?.Invoke();
            OnAnyValueChangedTo?.Invoke(m_List);
            OnAnyValueChangedFromTo?.Invoke(previousValue, newValue);
            if (newItemValue != null)
                OnSpecificValueChanged?.Invoke(newItemValue, itemIndex);
        }

        public IEnumerator<T> GetEnumerator() => m_List.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item) {
            var previousValue = m_List;
            m_List.Add(item);
            Invoke(previousValue, m_List);
        }

        public void Clear() {
            var previousValue = m_List;
            m_List.Clear();
            Invoke(previousValue, m_List);
        }

        public bool Contains(T item) => m_List.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => m_List.CopyTo(array, arrayIndex);

        public bool Remove(T item) {
            var previousValue = m_List;
            var result = m_List.Remove(item);
            if (result)
                Invoke(previousValue, m_List);
            return result;
        }

        public int Count => m_List.Count;
        public bool IsReadOnly => m_List.IsReadOnly;

        public int IndexOf(T item) => m_List.IndexOf(item);

        public void Insert(int index, T item) {
            var previousValue = m_List;
            m_List.Insert(index, item);
            Invoke(previousValue, m_List);
        }

        public void RemoveAt(int index) {
            var previousValue = m_List;
            m_List.RemoveAt(index);
            Invoke(previousValue, m_List);
        }
    }
}
