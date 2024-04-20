using UnityEngine;
using Game.Interface.Customization;
using Services;

namespace ScrollMod;

public class GetScroll {
    public static int TryGetScrollIdFromButton(GameObject buttonObject) {
        CustomizationScrollItem scrollItem = buttonObject.GetComponent<CustomizationScrollItem>();
        
        // Not a scroll button
        if (scrollItem is null) return -1;

        // Scroll decoration button (Tome of Fate scroll select)
        if (scrollItem.roleId != 0) return -1;

        // Bail if it is a legendary scroll
		if (Service.Home.Scrolls.IsScrollLegendaryScroll(scrollItem.scrollId)) return -1;

        return scrollItem.scrollId;
    }
}
