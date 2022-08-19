using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontHitbox : MonoBehaviour
{
    float elapsed = 0f;
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitbox)
    {
        if (hitbox.gameObject.tag == "Enemy")
        {

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                Attack(collision.gameObject);
            }
        }
    }



    public void Attack(GameObject enemy)
    {
        enemy.GetComponent<enemyPlaceholderStats>().health -= 25;

    }
}
