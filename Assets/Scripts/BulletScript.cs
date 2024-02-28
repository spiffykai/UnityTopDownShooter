using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] public float bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float angle = AngleBetweenTwoPoints(transform.position, mouseOnScreen);
        //transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));

        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * Vector3.left);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
