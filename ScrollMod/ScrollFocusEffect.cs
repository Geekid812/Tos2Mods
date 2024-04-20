using UnityEngine;
using BMG.UI;
using Game.Interface.Customization;
using TMPro;

namespace ScrollMod;

public class ScrollFocusEffect {
    public static void FocusScrollButton(BMG_Button button) {
        Transform parent = button.gameObject.transform.parent;

        foreach (Transform child in parent) {
            CustomizationScrollItem scrollComponent = child.gameObject.GetComponent<CustomizationScrollItem>();
            if (scrollComponent) {
                if (child.gameObject == button.gameObject) {
                    button.onClick.RemoveAllListeners();
                    scrollComponent.EnableSelection();
                } else {
                    scrollComponent.DisableSelection();
                }
            }
        }
    }

    public static void UpdateTransactionQuantityCounter(BMG_Button button) {
        TMP_Text qtyText = button.gameObject.transform.Find("QuantityText").GetComponent<TMP_Text>();
        if (!qtyText) {
            Debug.LogWarning("couldn't find quantity text counter...");
            return;
        }

        int quantity = TransactionController.transactionItemData.Multiplier * TransactionController.transactionQuantity;
        qtyText.color = new Color(0.1f, 0.7f, 0.1f, 1f);
        qtyText.text = "+" + quantity;
    }
}
