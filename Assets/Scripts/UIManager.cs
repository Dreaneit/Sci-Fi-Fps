using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text pickupCoinText;
    [SerializeField]
    private Image inventoryCoin;
    [SerializeField]
    private Text genericText;

    public void UpdateAmmo(int ammoCount)
    {
        ammoText.text = "Ammo: " + ammoCount;
    }

    public void GeneratedTextVisibility(bool visibility, string text = "")
    {
        if (!string.IsNullOrEmpty(text))
        {
            genericText.text = text;
        }

        genericText.gameObject.SetActive(visibility);
    }

    public void PickupCoinTextVisibility(bool visibility)
    {
        pickupCoinText.gameObject.SetActive(visibility);
    }

    public void InventoryCoinVisibility(bool visibility)
    {
        inventoryCoin.gameObject.SetActive(visibility);
    }
}
