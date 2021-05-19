using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipMenu : MonoBehaviour
{
    private bool canUse;

    public Text buttonPrompt;
    public Text repairCost;
    public Player player;
    public Image shipMenu, winScreen;
    public Button repair, launch;

    // Start is called before the first frame update
    void Start()
    {
        canUse = false;
        buttonPrompt.text = "E";
        buttonPrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && canUse)
        {
            shipMenu.gameObject.SetActive(true);
            player.canControl = false;
        }

        if (shipMenu.IsActive())
        {
            DisableButtons();
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
        shipMenu.gameObject.SetActive(false);
        player.canControl = true;
    }

    private void DisableButtons()
    {
        if (player.smeltedOre < 5000000)
            repair.interactable = false;
        else
            repair.interactable = true;
    }

    public void Repair()
    {
        player.smeltedOre -= 5000000;
        repairCost.text = "Ship repaired!";
        launch.gameObject.SetActive(true);
    }

    public void Launch()
    {
        winScreen.gameObject.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
