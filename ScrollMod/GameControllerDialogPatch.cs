using HarmonyLib;
using Home.Shop;
using Game;
using UnityEngine;

namespace ScrollMod;

[HarmonyPatch(typeof(GameSceneController), "HandleOnProductPurchaseResult")]
public class GameControllerDialogPatch
{
	public static bool Prefix(ProductPurchaseResult result) {
		return !BuyScroll.IsScrollsPurchase(result);
	}

	public static void Postfix(GameSceneController __instance, ProductPurchaseResult result, bool __runOriginal) {
		if (!__runOriginal) {
			BetaShopPurchaseResultController purchaseController = __instance.GetComponent<BetaShopPurchaseResultController>();
			if (!purchaseController) purchaseController = __instance.gameObject.AddComponent(typeof(BetaShopPurchaseResultController)) as BetaShopPurchaseResultController;
			purchaseController.ShowPurchaseResult(result, TransactionController.transactionItemData, TransactionController.transactionQuantity);
		}
	}
}
