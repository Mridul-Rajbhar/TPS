using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int totalEnemy;

    int currentEnemy;

    [SerializeField]
    GameObject[] enemy;

    [SerializeField]
    GameObject ParentStartPoint;
    [SerializeField]
    Transform[] instantiateEnemy;
    int instantiatePoints;

    // Start is called before the first frame update
    void Start()
    {
        instantiateEnemy = ParentStartPoint.GetComponentsInChildren<Transform>();
        currentEnemy = totalEnemy;
        instantiatePoints = instantiateEnemy.Length;
        InstantiateEnemies();
    }
    void InstantiateEnemies()
    {
        for (int i = 0; i < totalEnemy; i++)
        {
            Instantiate(enemy[Random.Range(0, 2)], instantiateEnemy[Random.Range(1, instantiatePoints)].position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
