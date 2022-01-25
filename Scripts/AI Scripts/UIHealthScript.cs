using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthScript : MonoBehaviour
{
    public Transform target;
    public Image foregroundImage;
    public Image backgroundImage;
    public Vector3 offset; 
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0f;

        foregroundImage.enabled = !isBehind;
        backgroundImage.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset); 
        //Debug.Log(transform.position);
    }

    public void SetHealthBar(float percentage)
    {
        float oldWidth = GetComponent<RectTransform>().rect.width;
        float newWidth = oldWidth*percentage;
        //Debug.Log(newWidth);

        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }
}
