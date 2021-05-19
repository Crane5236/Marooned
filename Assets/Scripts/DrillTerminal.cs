using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillTerminal : MonoBehaviour
{
    private bool canUse;
    private long purchaseCost;
    private int maxDrills;
    private int currentNumDrills;
    private long tier1Cost;
    private long tier2Cost;
    private long tier3Cost;
    private long tier4Cost;
    private long storageCost;

    public Text buttonPrompt;
    public Text drillCount;
    public Text drillPrice;

    public Text drill1Level;
    public Text drill2Level;
    public Text drill3Level;
    public Text drill4Level;
    public Text drill5Level;

    public Text upgrade1Price;
    public Text upgrade2Price;
    public Text upgrade3Price;
    public Text upgrade4Price;
    public Text upgrade5Price;
    public Text storagePrice;
    public Text storageSize;

    public Player player;
    public Image drillMenu;
    public Button purchaseDrillButton;
    public Button upgrade1, upgrade2, upgrade3, upgrade4, upgrade5;
    public Button upgradeOutput;
    public Drill drill1, drill2, drill3, drill4, drill5;
    public OutputBox output;

    // Start is called before the first frame update
    void Start()
    {
        canUse = false;
        buttonPrompt.text = "E";
        buttonPrompt.gameObject.SetActive(false);
        drillMenu.gameObject.SetActive(false);
        purchaseCost = 100;
        maxDrills = 5;
        currentNumDrills = 0;

        tier1Cost = 500;
        tier2Cost = 5000;
        tier3Cost = 50000;
        tier4Cost = 250000;
        storageCost = 250;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && canUse)
        {
            drillMenu.gameObject.SetActive(true);
            player.canControl = false;
        }

        if (drillMenu.IsActive())
        {
            DisableButtons();
        }

        DisplayPrices();
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
        drillMenu.gameObject.SetActive(false);
        player.canControl = true;
    }

    private void DisableButtons()
    {
        if (player.ore < purchaseCost || currentNumDrills == maxDrills)
            purchaseDrillButton.interactable = false;
        else
            purchaseDrillButton.interactable = true;

        if (player.ore < GetPrice(drill1) || drill1.drillLevel >= 5)
            upgrade1.interactable = false;
        else
            upgrade1.interactable = true;

        if (player.ore < GetPrice(drill2) || drill2.drillLevel >= 5)
            upgrade2.interactable = false;
        else
            upgrade2.interactable = true;

        if (player.ore < GetPrice(drill3) || drill3.drillLevel >= 5)
            upgrade3.interactable = false;
        else
            upgrade3.interactable = true;

        if (player.ore < GetPrice(drill4) || drill4.drillLevel >= 5)
            upgrade4.interactable = false;
        else
            upgrade4.interactable = true;

        if (player.ore < GetPrice(drill5) || drill5.drillLevel >= 5)
            upgrade5.interactable = false;
        else
            upgrade5.interactable = true;

        if (player.ore < storageCost || output.level >= 5)
            upgradeOutput.interactable = false;
        else
            upgradeOutput.interactable = true;
    }

    public void PurcahseDrill()
    {
        player.ore -= purchaseCost;

        switch (currentNumDrills)
        {
            case 0:
                drill1.gameObject.SetActive(true);
                upgrade1.gameObject.SetActive(true);
                break;
            case 1:
                drill2.gameObject.SetActive(true);
                upgrade2.gameObject.SetActive(true);
                break;
            case 2:
                drill3.gameObject.SetActive(true);
                upgrade3.gameObject.SetActive(true);
                break;
            case 3:
                drill4.gameObject.SetActive(true);
                upgrade4.gameObject.SetActive(true);
                break;
            case 4:
                drill5.gameObject.SetActive(true);
                upgrade5.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        player.oreCount.text = "Ore: " + player.ore;
        currentNumDrills++;
        purchaseCost = (long)Mathf.Round(purchaseCost * 1.15f);
        drillCount.text = "Drills: " + currentNumDrills;
        if (currentNumDrills >= 5)
            drillPrice.text = "Max Drills";
        else
            drillPrice.text = "Cost: " + purchaseCost + " Ore";

        DisplayPrices();
    }

    public void UpgradeDrill(int drill)
    {
        switch (drill)
        {
            case 1:
                Upgrade(drill1, 1);
                break;
            case 2:
                Upgrade(drill2, 2);
                break;
            case 3:
                Upgrade(drill3, 3);
                break;
            case 4:
                Upgrade(drill4, 4);
                break;
            case 5:
                Upgrade(drill5, 5);
                break;
            default:
                break;
        }
    }

    private void Upgrade(Drill drill, int num)
    {
        switch (drill.drillLevel)
        {
            case 1:
                player.ore -= tier1Cost;
                tier1Cost = (long)Mathf.Round(tier1Cost * 1.15f);
                break;
            case 2:
                player.ore -= tier2Cost;
                tier2Cost = (long)Mathf.Round(tier2Cost * 1.15f);
                break;
            case 3:
                player.ore -= tier3Cost;
                tier3Cost = (long)Mathf.Round(tier3Cost * 1.15f);
                break;
            case 4:
                player.ore -= tier4Cost;
                tier4Cost = (long)Mathf.Round(tier4Cost * 1.15f);
                break;
            default:
                break;
        }

        drill.LevelUp();
        
        switch (num)
        {
            case 1:
                drill1Level.text = "Level: " + drill.drillLevel;
                break;
            case 2:
                drill2Level.text = "Level: " + drill.drillLevel;
                break;
            case 3:
                drill3Level.text = "Level: " + drill.drillLevel;
                break;
            case 4:
                drill4Level.text = "Level: " + drill.drillLevel;
                break;
            case 5:
                drill5Level.text = "Level: " + drill.drillLevel;
                break;
            default:
                break;
        }

        DisplayPrices();
    }

    private void DisplayPrices()
    {
        upgrade1Price.text = DisplayUpgradePrice(drill1);
        upgrade2Price.text = DisplayUpgradePrice(drill2);
        upgrade3Price.text = DisplayUpgradePrice(drill3);
        upgrade4Price.text = DisplayUpgradePrice(drill4);
        upgrade5Price.text = DisplayUpgradePrice(drill5);
    }

    private string DisplayUpgradePrice(Drill drill)
    {
        switch (drill.drillLevel)
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

    private long GetPrice(Drill drill)
    {
        switch (drill.drillLevel)
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
