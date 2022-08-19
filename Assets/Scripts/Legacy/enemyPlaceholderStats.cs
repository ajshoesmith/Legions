using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPlaceholderStats : MonoBehaviour
{
    [SerializeField]
    public int health = 900;
    public int damage = 10;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Debug.Log(health);
    }

    // Update is called once per frame
    void Update()
    {
        rb.mass = health ;
        //Debug.Log(rb.mass);
    }
}
