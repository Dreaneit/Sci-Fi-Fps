using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinPickupSound;
    private UIManager uiManager;

    private bool canPickupCoin = false;

    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickupCoin = true;
            uiManager.PickupCoinTextVisibility(canPickupCoin);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (canPickupCoin && Input.GetKeyDown(KeyCode.E))
            {
                player.CollectCoin();
                AudioSource.PlayClipAtPoint(coinPickupSound, transform.position);
                Destroy(this.gameObject);
                DisablePickup();
            }
        }
    }

    private void OnTriggerExit()
    {
        DisablePickup();
    }

    private void DisablePickup()
    {
        canPickupCoin = false;
        uiManager.PickupCoinTextVisibility(canPickupCoin);
    }
}
