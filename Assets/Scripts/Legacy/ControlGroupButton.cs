using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGroupButton : MonoBehaviour
{
    [SerializeField] public int groupNumber;

    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.SetActive(false);
    }
}
