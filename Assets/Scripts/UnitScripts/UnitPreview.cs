using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPreview : MonoBehaviour
{
    public GameObject previewGameObject;
    private SpriteRenderer spriteR;

    void Start()
    {
        spriteR = this.transform.GetChild(1).GetComponent<SpriteRenderer>();
        spriteR.sprite = GetComponent<BaseUnitPrefab>().unitObject.sprite;
        previewGameObject = transform.Find("Preview").gameObject;
        SetPreviewVisible(false);
    }

    public void SetPreviewVisible(bool visible)
    {
        previewGameObject.SetActive(visible);
    }
}
