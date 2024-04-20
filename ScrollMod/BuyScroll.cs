using UnityEngine;
using Services;
using Home.Shop;
using Home.Services;
using Home.Shared.Enums;

namespace ScrollMod;

public class BuyScroll {
    public static void Buy(CommonItemData data, int quantity) {
        HomeShopService service = Service.Home.Shop;
        int productId = data.TPProductId;

        if (productId != 0) {
            service.PurchaseStoreProduct(productId, quantity);
        }
    }

    public static CommonItemData GetProduct(int scrollId) {
        HomeShopService service = Service.Home.Shop;
        CommonItemData data = service.GetCommonData(Service.Home.Scrolls.IsIncreaseScroll(scrollId) ? ItemTypeCategory.Scroll : ItemTypeCategory.CursedScroll, scrollId);
        return data;
    }

    public static bool IsScrollsPurchase(ProductPurchaseResult result) {
        return result.Items.Find((ProductItem x) => x.ItemType == ItemTypeCategory.Scroll || x.ItemType == ItemTypeCategory.CursedScroll).ItemType > ItemTypeCategory.Undefined;
    }
}
