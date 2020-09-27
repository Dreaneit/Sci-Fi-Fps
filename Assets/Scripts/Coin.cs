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
            HandlePickupCoin(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canPickupCoin && Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.CollectCoin();
                AudioSource.PlayClipAtPoint(coinPickupSound, transform.position);
                Destroy(this.gameObject);
                HandlePickupCoin(false);
            }
        }
    }

    private void OnTriggerExit()
    {
        HandlePickupCoin(false);
    }

    private void HandlePickupCoin(bool canPickup)
    {
        canPickupCoin = canPickup;
        uiManager.PickupCoinTextVisibility(canPickupCoin);
    }
}
