using HarmonyLib;
using BMG.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScrollMod;

[HarmonyPatch(typeof(BMG_Button), "OnPointerClick")]
public class BMG_ButtonPatch
{
	public static void Postfix(BMG_Button __instance, PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
		    int scrollId = GetScroll.TryGetScrollIdFromButton(__instance.gameObject);
			if (scrollId != -1) {
				if (TransactionController.HandleBuyClick(scrollId, __instance)) {
					AudioPlayer.PlaySound("Audio/UI/ClickSound.wav");
					ScrollFocusEffect.FocusScrollButton(__instance);
					ScrollFocusEffect.UpdateTransactionQuantityCounter(__instance);
				}
			}
		}
	}
}
