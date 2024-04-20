using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using Game.Decorations;
using Services;

namespace ScrollMod;

[HarmonyPatch(typeof(UserInventory))]
public class UserInventoryPatch
{
	public static List<int> notActuallyOwnedIncreaseScrolls = new List<int>();
	public static List<int> notActuallyOwnedDecreaseScrolls = new List<int>();

	[HarmonyPostfix]
	[HarmonyPatch("GetItems")]
	public static void PatchGetItems(ref Dictionary<int, int> __result)
	{
		if (!CustomizationPanelPatch.flagFakeScollInventory) return;
		notActuallyOwnedIncreaseScrolls.Clear();
		notActuallyOwnedDecreaseScrolls.Clear();
		
		// make a copy
		__result = new Dictionary<int, int>(__result);

		foreach (ScrollDecoration increaseScroll in Service.Home.Scrolls.GetIncreaseScrolls()) {
			int scrollId = increaseScroll.decorationID;

			if (Service.Home.Scrolls.IsScrollLegendaryScroll(scrollId)) continue;

			if (!__result.ContainsKey(scrollId)) {
				__result.Add(scrollId, 1);
				notActuallyOwnedIncreaseScrolls.Add(scrollId);
			}
		}

		foreach (ScrollDecoration decreaseScroll in Service.Home.Scrolls.GetDecreaseScrolls()) {
			int scrollId = decreaseScroll.decorationID;
			if (!__result.ContainsKey(scrollId)) {
				__result.Add(scrollId, 1);
				notActuallyOwnedDecreaseScrolls.Add(scrollId);
			}
		}
	}

	[HarmonyPostfix]
	[HarmonyPatch("GetOwnedCountOfItem")]
	public static void PatchCountItem(int id, ref int __result) {
		if (!CustomizationPanelPatch.flagFakeScollInventory) return;
		if (notActuallyOwnedIncreaseScrolls.Contains(id) || notActuallyOwnedDecreaseScrolls.Contains(id)) {
			__result = 1;
		}
	}
}
