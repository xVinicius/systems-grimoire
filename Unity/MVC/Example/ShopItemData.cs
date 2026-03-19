namespace SystemsGrimoire.MVC.Example {
    /// <summary>
    /// Example data class for a shop item.
    /// Replace with your own domain data.
    /// </summary>
    public class ShopItemData {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public bool IsLocked { get; set; }
    }
}
