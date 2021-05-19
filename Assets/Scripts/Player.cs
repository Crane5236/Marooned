using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Text oreCount;
    public Text smeltedOreCount;
    public Text healthCount;
    public Image gameOverScreen;
    public Text pause;

    public float bulletForce = 20f;
    public float fireRate = .5f;
    public float speed = 5f;
    public int currentHealth = 10;
    public float invincibilityTime = 1f;
    public long ore = 0;
    public long smeltedOre = 0;
    public long totalOre = 0;
    public bool canControl = false;

    Vector2 movement;
    Vector2 mousePosition;

    private float nextFire;
    private float nextDamage = 0;
    private bool canMine = false;
    private bool displayOre = true;
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        DisplayResources();

        if (canControl)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && canControl)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetButtonDown("Jump") && canMine && canControl)
        {
            ore++;
            totalOre++;
        }

        if (Input.GetButtonDown("Switch"))
        {
            if (displayOre)
            {
                displayOre = false;
                SwapCounter();
            }
            else
            {
                displayOre = true;
                SwapCounter();
            }
        }

        if (Input.GetButtonDown("Pause") && canControl) 
        {
            if (isPaused)
            {
                ResumeGame();
                pause.gameObject.SetActive(false);
                isPaused = false;
            }
            else
            {
                PauseGame();
                pause.gameObject.SetActive(true);
                isPaused = true;
            }
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePosition - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        rb2d.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        if (Time.time > nextDamage)
        {
            currentHealth -= damage;
            nextDamage = Time.time + invincibilityTime;
        }

        healthCount.text = "Health: " + currentHealth;

        if (currentHealth <= 0)
            gameOverScreen.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ore"))
        {
            canMine = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ore"))
        {
            canMine = false;
        }
    }

    private void DisplayResources()
    {
        if (displayOre)
            oreCount.text = "Ore: " + ore.ToString("n0");
        else
            smeltedOreCount.text = "Smelted Ore: " + smeltedOre.ToString("n0");
    }
    
    private void SwapCounter()
    {
        if (displayOre)
        {
            smeltedOreCount.gameObject.SetActive(false);
            oreCount.gameObject.SetActive(true);
        }
        else
        {
            oreCount.gameObject.SetActive(false);
            smeltedOreCount.gameObject.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}