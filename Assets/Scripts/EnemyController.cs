using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float baseSpeed = 3f;
    [SerializeField] public GameObject coinPrefab;
    private float _moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = Random.Range(baseSpeed - 2f, baseSpeed + 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Follow player
        GameObject player = GameObject.Find("Player");
        Vector3 move = player.transform.position - transform.position;
        move = move.normalized * _moveSpeed;
        transform.Translate(move * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject newCoin = Instantiate(coinPrefab, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
