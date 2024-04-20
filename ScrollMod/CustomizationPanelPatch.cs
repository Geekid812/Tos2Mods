using HarmonyLib;
using Game.Interface.Customization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Home.Shop;
using Home.Shared.Enums;

namespace ScrollMod;

[HarmonyPatch(typeof(CustomizationPanel), "PopulateScrolls")]
public class CustomizationPanelPatch
{
	public static bool flagFakeScollInventory = false;

	public static void Prefix(CustomizationPanel __instance) {
		flagFakeScollInventory = true;
	}

	public static void Postfix(List<CustomizationScrollItem> ___increaseScrollInstances, List<CustomizationScrollItem> ___decreaseScrollInstances)
	{
		flagFakeScollInventory = false;

		foreach (CustomizationScrollItem increaseScroll in ___increaseScrollInstances) {
			if (UserInventoryPatch.notActuallyOwnedIncreaseScrolls.Contains(increaseScroll.scrollId)) {
				increaseScroll.SetQuantity(0);
				increaseScroll.DisableSelection();
			}
		}

		foreach (CustomizationScrollItem decreaseScroll in ___decreaseScrollInstances) {
			if (UserInventoryPatch.notActuallyOwnedDecreaseScrolls.Contains(decreaseScroll.scrollId)) {
				decreaseScroll.SetQuantity(0);
				decreaseScroll.DisableSelection();
			}
		}
	}

	[HarmonyPostfix]
	[HarmonyPatch(typeof(CustomizationPanel), "HandleOnProductPurchaseResult")]
	public static void OnPurchasePostfix(CustomizationPanel __instance, ProductPurchaseResult result) {
		if (BuyScroll.IsScrollsPurchase(result))
		{
            TransactionController.isTransactionRunningPurchase = false;
			TransactionController.isTransactionStarted = false;
			__instance.StartCoroutine(PanelRefresh(__instance));
		}
	}

    [HarmonyReversePatch]
    [HarmonyPatch(typeof(CustomizationPanel), "ClearScrolls")]
    public static void ClearScrolls(object instance) => throw new NotImplementedException("stub");

	private static IEnumerator PanelRefresh(CustomizationPanel instance) {
		yield return new WaitForSeconds(0.1f);
		if (instance is null) yield break;
		ClearScrolls(instance);

		// weird reflection magic to get the patched version of the method
		MethodInfo dynMethod = instance.GetType().GetMethod("PopulateScrolls", BindingFlags.NonPublic | BindingFlags.Instance);
		dynMethod.Invoke(instance, new object[] {});
	}
}
