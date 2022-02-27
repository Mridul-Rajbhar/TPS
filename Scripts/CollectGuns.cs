using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGuns : MonoBehaviour
{
    public GameObject gameManager;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        gameManager.SetActive(true);
        Destroy(this.gameObject);
    }
}
