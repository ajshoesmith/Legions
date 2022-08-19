using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxA : MonoBehaviour
{
    /*/ Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            this.transform.parent.GetComponent<MovePositionDirect>().inCombat = true;
            //Debug.Log("Hitbox");
            this.transform.parent.GetComponent<MovePositionDirect>().CombatRotation(collider);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            this.transform.parent.GetComponent<MovePositionDirect>().inCombat = false;
        }
    }
    */
}
