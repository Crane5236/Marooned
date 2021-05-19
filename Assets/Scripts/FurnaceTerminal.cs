using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceTerminal : MonoBehaviour
{
    private bool canUse;
    private long purchaseCost;
    private int maxFurnaces;
    private int currentNumFurnaces;
    private long tier1Cost;
    private long tier2Cost;
    private long tier3Cost;
    private long tier4Cost;
    private long storageCost;

    public Text buttonPrompt;
    public Text furnaceCount;
    public Text furnacePrice;

    public Text furnace1Level;
    public Text furnace2Level;
    public Text furnace3Level;
    public Text furnace4Level;
    public Text furnace5Level;

    public Text upgrade1Price;
    public Text upgrade2Price;
    public Text upgrade3Price;
    public Text upgrade4Price;
    public Text upgrade5Price;
    public Text storagePrice;
    public Text storageSize;

    public Player player;
    public Image furnaceMenu;
    public Button purchaseFurnaceButton;
    public Button upgrade1, upgrade2, upgrade3, upgrade4, upgrade5;
    public Button upgradeOutput;
    public Furnace furnace1, furnace2, furnace3, furnace4, furnace5;
    public OutputBox output;

    // Start is called before the first frame update
    void Start()
    {
        canUse = false;
        buttonPrompt.text = "E";
        buttonPrompt.gameObject.SetActive(false);
        furnaceMenu.gameObject.SetActive(false);
        purchaseCost = 100;
        maxFurnaces = 5;
        currentNumFurnaces = 0;

        tier1Cost = 250;
        tier2Cost = 1000;
        tier3Cost = 10000;
        tier4Cost = 100000;
        storageCost = 250;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && canUse)
        {
            furnaceMenu.gameObject.SetActive(true);
            player.canControl = false;
        }

        if (furnaceMenu.IsActive())
        {
            DisableButtons();
        }

        DisplayPrices();
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
        furnaceMenu.gameObject.SetActive(false);
        player.canControl = true;
    }

    private void DisableButtons()
    {
        if (player.ore < purchaseCost || currentNumFurnaces == maxFurnaces)
            purchaseFurnaceButton.interactable = false;
        else
            purchaseFurnaceButton.interactable = true;

        if (player.ore < GetPrice(furnace1) || furnace1.furnaceLevel >= 5)
            upgrade1.interactable = false;
        else
            upgrade1.interactable = true;

        if (player.ore < GetPrice(furnace2) || furnace2.furnaceLevel >= 5)
            upgrade2.interactable = false;
        else
            upgrade2.interactable = true;

        if (player.ore < GetPrice(furnace3) || furnace3.furnaceLevel >= 5)
            upgrade3.interactable = false;
        else
            upgrade3.interactable = true;

        if (player.ore < GetPrice(furnace4) || furnace4.furnaceLevel >= 5)
            upgrade4.interactable = false;
        else
            upgrade4.interactable = true;

        if (player.ore < GetPrice(furnace5) || furnace5.furnaceLevel >= 5)
            upgrade5.interactable = false;
        else
            upgrade5.interactable = true;

        if (player.ore < storageCost || output.level >= 5)
            upgradeOutput.interactable = false;
        else
            upgradeOutput.interactable = true;
    }

    public void PurcahseFurnace()
    {
        player.ore -= purchaseCost;

        switch (currentNumFurnaces)
        {
            case 0:
                furnace1.gameObject.SetActive(true);
                upgrade1.gameObject.SetActive(true);
                break;
            case 1:
                furnace2.gameObject.SetActive(true);
                upgrade2.gameObject.SetActive(true);
                break;
            case 2:
                furnace3.gameObject.SetActive(true);
                upgrade3.gameObject.SetActive(true);
                break;
            case 3:
                furnace4.gameObject.SetActive(true);
                upgrade4.gameObject.SetActive(true);
                break;
            case 4:
                furnace5.gameObject.SetActive(true);
                upgrade5.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        player.oreCount.text = "Ore: " + player.ore;
        currentNumFurnaces++;
        purchaseCost = (long)Mathf.Round(purchaseCost * 1.15f);
        furnaceCount.text = "Furnaces: " + currentNumFurnaces;
        if (currentNumFurnaces >= 5)
            furnacePrice.text = "Max Furnaces";
        else
            furnacePrice.text = "Cost: " + purchaseCost + " Ore";

        DisplayPrices();
    }

    public void UpgradeFurnace(int furnace)
    {
        switch (furnace)
        {
            case 1:
                Upgrade(furnace1, 1);
                break;
            case 2:
                Upgrade(furnace2, 2);
                break;
            case 3:
                Upgrade(furnace3, 3);
                break;
            case 4:
                Upgrade(furnace4, 4);
                break;
            case 5:
                Upgrade(furnace5, 5);
                break;
            default:
                break;
        }
    }

    private void Upgrade(Furnace furnace, int num)
    {
        switch (furnace.furnaceLevel)
        {
            case 1:
                player.ore -= tier1Cost;
                break;
            case 2:
                player.ore -= tier2Cost;
                break;
            case 3:
                player.ore -= tier3Cost;
                break;
            case 4:
                player.ore -= tier4Cost;
                break;
            default:
                break;
        }

        furnace.LevelUp();

        switch (num)
        {
            case 1:
                furnace1Level.text = "Level: " + furnace.furnaceLevel;
                break;
            case 2:
                furnace2Level.text = "Level: " + furnace.furnaceLevel;
                break;
            case 3:
                furnace3Level.text = "Level: " + furnace.furnaceLevel;
                break;
            case 4:
                furnace4Level.text = "Level: " + furnace.furnaceLevel;
                break;
            case 5:
                furnace5Level.text = "Level: " + furnace.furnaceLevel;
                break;
            default:
                break;
        }

        DisplayPrices();
    }

    private void DisplayPrices()
    {
        upgrade1Price.text = DisplayUpgradePrice(furnace1);
        upgrade2Price.text = DisplayUpgradePrice(furnace2);
        upgrade3Price.text = DisplayUpgradePrice(furnace3);
        upgrade4Price.text = DisplayUpgradePrice(furnace4);
        upgrade5Price.text = DisplayUpgradePrice(furnace5);
    }

    private string DisplayUpgradePrice(Furnace furnace)
    {
        switch (furnace.furnaceLevel)
        {
            case 1:
                return "Cost: " + tier1Cost.ToString("n0") + " Ore";
            case 2:
                return "Cost: " + tier2Cost.ToString("n0") + " Ore";
            case 3:
                return "Cost: " + tier3Cost.ToString("n0") + " Ore";
            case 4:
                return "Cost: " + tier4Cost.ToString("n0") + " Ore";
            case 5:
                return "Fully Upgraded";
            default:
                return "Error";
        }
    }

    private long GetPrice(Furnace furnace)
    {
        switch (furnace.furnaceLevel)
        {
            case 1:
                return tier1Cost;
            case 2:
                return tier2Cost;
            case 3:
                return tier3Cost;
            case 4:
                return tier4Cost;
            default:
                return 0;
        }
    }

    public void UpgradeStorage()
    {
        switch (output.level)
        {
            case 1:
                output.maxSize = 1000;
                storageCost = 2500;
                storagePrice.text = "Cost: 2,500 Ore";
                storageSize.text = "Current Size: 1K";
                player.ore -= 250;
                break;
            case 2:
                output.maxSize = 10000;
                storageCost = 25000;
                storagePrice.text = "Cost: 25,000 Ore";
                storageSize.text = "Current Size: 10K";
                player.ore -= 2500;
                break;
            case 3:
                output.maxSize = 100000;
                storageCost = 200000;
                storagePrice.text = "Cost: 200,000 Ore";
                storageSize.text = "Current Size: 100K";
                player.ore -= 25000;
                break;
            case 4:
                output.maxSize = 1000000;
                storageCost = 0;
                storagePrice.text = "Max Size";
                storageSize.text = "Current Size: 1M";
                player.ore -= 200000;
                break;
            default:
                break;
        }

        output.level++;
        output.capacityText.text = output.oreInBox + " /\n" + output.maxSize;
    }
}
