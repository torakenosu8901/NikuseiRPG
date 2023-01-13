using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour
{
    private void Start()
    {
        DeleGateAction deleGateAction = GetComponent<DeleGateAction>();
        CreateShopItem createShopItem = GetComponent<CreateShopItem>();

        deleGateAction.Init(createShopItem.Init());
    }
}
