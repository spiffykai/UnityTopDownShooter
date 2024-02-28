using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public GameObject gunPoint;
    [SerializeField] public GameObject gun;
    [SerializeField] public GameObject gunShootPoint;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject mainObject;
    [SerializeField] public TMP_Text coinText;

    public bool playerDead = false;
    public bool canShoot = true;
    public int coins = 0;

    [SerializeField] public Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Screen.width);
        
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontalMove, verticalMove, 0);
        move = move.normalized * moveSpeed;
        if (!playerDead)
        {
            transform.Translate(move * Time.deltaTime);
        }

        // Rotate gun
        if (!playerDead)
        {
            Vector2 mousePositionOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = AngleBetweenTwoPoints(gunPoint.transform.position, mousePositionOnScreen);
            gunPoint.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        
        
        // Shoot
        if (Input.GetAxis("Fire1") > 0 && canShoot && !playerDead) {
            GameObject bullet = Instantiate(bulletPrefab, gunShootPoint.transform.position, gunShootPoint.transform.rotation);
            canShoot = false;
            StartCoroutine(ShootCooldown());
        }
        
        // Clamp the player on the screen
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = pos;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

    IEnumerator ShootCooldown() {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") && !playerDead) {
            Destroy(collision.gameObject);
            playerDead = true;
            mainObject.GetComponent<MainScript>().PlayerDead(coins);
            coinText.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            coinText.text = "Coins: " + coins.ToString();
        }
    }
}
