namespace SystemsGrimoire.MVC.Example {
    /// <summary>
    /// Wrapper around ShopItemData. The Model holds a list of these.
    /// </summary>
    public class ShopItem {
        public readonly ShopItemData Data;

        public ShopItem(ShopItemData data) {
            Data = data;
        }
    }
}
