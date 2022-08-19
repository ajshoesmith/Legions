using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPreview : MonoBehaviour
{
    public GameObject previewGameObject;

    void Awake()
    {
        previewGameObject = transform.Find("Preview").gameObject;
        SetPreviewVisible(false);
    }

    public void SetPreviewVisible(bool visible)
    {
        previewGameObject.SetActive(visible);
    }
}
