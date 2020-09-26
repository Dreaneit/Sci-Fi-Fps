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

    public void UpdateAmmo(int ammoCount)
    {
        ammoText.text = "Ammo: " + ammoCount;
    }

    public void PickupCoinTextVisibility(bool visibility)
    {
        pickupCoinText.gameObject.SetActive(visibility);
    }
}
