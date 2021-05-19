using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    private bool canUse;
    private long healPrice;

    public Text buttonPrompt;
    public Text healCost;
    public Player player;
    public Image playerMenu;
    public Button heal;

    // Start is called before the first frame update
    void Start()
    {
        canUse = false;
        buttonPrompt.text = "E";
        buttonPrompt.gameObject.SetActive(false);
        healPrice = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && canUse)
        {
            playerMenu.gameObject.SetActive(true);
            player.canControl = false;
        }

        if (playerMenu.IsActive())
        {
            DisableButtons();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonPrompt.gameObject.SetActive(true);
            canUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonPrompt.gameObject.SetActive(false);
        canUse = false;
    }

    public void ExitMenu()
    {
        playerMenu.gameObject.SetActive(false);
        player.canControl = true;
    }

    private void DisableButtons()
    {
        if (player.smeltedOre < healPrice || player.currentHealth >= 10)
            heal.interactable = false;
        else
            heal.interactable = true;
    }

    public void HealPlayer()
    {
        player.currentHealth++;
        player.smeltedOre -= healPrice;
        
        healPrice = (long)Mathf.Round(healPrice * 1.25f);
        healCost.text = "Cost: " + healPrice.ToString("n0") + " Smelted Ore";
        player.healthCount.text = "Health: " + player.currentHealth;
    }
}
