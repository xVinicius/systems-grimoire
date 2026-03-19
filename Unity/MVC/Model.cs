using System.Collections.Generic;

namespace SystemsGrimoire.MVC {
    /// <summary>
    /// Generic base Model using ObservableList for reactive data binding.
    /// The Model holds data and notifies observers when it changes — it knows nothing about Views or Controllers.
    ///
    /// Usage:
    ///     public class InventoryModel : Model&lt;InventoryItem&gt; { }
    ///
    ///     var model = new InventoryModel();
    ///     model.Items.OnSpecificValueChanged += (item, index) => Debug.Log($"Slot {index} changed");
    ///     model.Add(new InventoryItem("Sword"));
    /// </summary>
    public class Model<T> {
        public ObservableList<T> Items = new(new List<T>());

        public void Add(T item) => Items.Add(item);

        public void Clear() => Items.Clear();

        public void RemoveAt(int index) => Items.RemoveAt(index);

        public int Count => Items.Count;
    }
}
