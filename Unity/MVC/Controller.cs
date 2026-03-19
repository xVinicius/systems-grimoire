namespace SystemsGrimoire.MVC {
    /// <summary>
    /// Generic base Controller that wires a Model to a View.
    /// The Controller mediates between them: it subscribes to View events (user input)
    /// and Model events (data changes), routing updates in both directions.
    ///
    /// Subclasses must implement ConnectModel() and ConnectView() to define the bindings.
    ///
    /// Usage:
    ///     public class ShopController : Controller&lt;ShopModel, ShopView&gt; {
    ///         public ShopController(ShopModel model, ShopView view) : base(model, view) { }
    ///
    ///         protected override void ConnectModel() {
    ///             m_Model.Items.OnSpecificValueChanged += (item, i) => m_View.UpdateSlot(item, i);
    ///         }
    ///
    ///         protected override void ConnectView() {
    ///             m_View.UpdateAllSlots(m_Model.Items);
    ///             m_View.OnItemClicked += index => HandlePurchase(index);
    ///         }
    ///     }
    /// </summary>
    public abstract class Controller<TModel, TView> {
        protected readonly TModel m_Model;
        protected readonly TView m_View;

        protected Controller(TModel model, TView view) {
            m_Model = model;
            m_View = view;

            ConnectModel();
            ConnectView();
        }

        /// <summary>
        /// Subscribe to Model changes and push updates to the View.
        /// </summary>
        protected abstract void ConnectModel();

        /// <summary>
        /// Initialize the View with current Model state and subscribe to View events.
        /// </summary>
        protected abstract void ConnectView();

        /// <summary>
        /// Unsubscribe from all Model and View events. Call when the UI is destroyed.
        /// </summary>
        public virtual void Destroy() { }
    }
}
