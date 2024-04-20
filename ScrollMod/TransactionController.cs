using UnityEngine;
using Home.Shop;
using Home.Services;
using Home.Shared.Enums;
using System.Collections;

namespace ScrollMod;

public class TransactionController {
    const float DELAY_TRANSACTION = 0.75f;

    public static Coroutine activeTransactionCoroutine = null;
    public static bool isTransactionRunningPurchase = false;
    public static bool isTransactionStarted = false;
    public static CommonItemData transactionItemData = null;
    public static int transactionQuantity = 0;



    public static bool HandleBuyClick(int scrollId, MonoBehaviour coroutineController) {
        if (isTransactionRunningPurchase) return false;
        if (isTransactionStarted && transactionItemData.id != scrollId) return false;        

        if (!isTransactionStarted) {
            isTransactionStarted = true;
            transactionQuantity = 0;
            transactionItemData = BuyScroll.GetProduct(scrollId);
        }
        if (activeTransactionCoroutine != null) coroutineController.StopCoroutine(activeTransactionCoroutine);

        transactionQuantity += 1;
        activeTransactionCoroutine = coroutineController.StartCoroutine(BuyDelayed());
        return true;
    }

    public static IEnumerator BuyDelayed() {
        yield return new WaitForSeconds(DELAY_TRANSACTION);
        isTransactionRunningPurchase = true;
        BuyScroll.Buy(transactionItemData, transactionQuantity);
    }
}
