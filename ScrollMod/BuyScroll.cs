using UnityEngine;
using Services;
using Home.Shop;
using Home.Services;
using Home.Shared.Enums;

namespace ScrollMod;

public class BuyScroll {
    public static void Buy(int scrollId, int quantity) {
        HomeShopService service = Service.Home.Shop;
        CommonItemData data = service.GetCommonData(Service.Home.Scrolls.IsIncreaseScroll(scrollId) ? ItemTypeCategory.Scroll : ItemTypeCategory.CursedScroll, scrollId);
        int productId = data.TPProductId;
        
        if (productId != 0) {
            service.PurchaseStoreProduct(productId, quantity);
        }
    }
}