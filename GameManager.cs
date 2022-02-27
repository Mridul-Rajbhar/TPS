using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UIGameScreen uIGameScreen;

    [SerializeField]
    int totalEnemy;

    //int currentEnemy;

    [SerializeField]
    GameObject[] enemy;

    [SerializeField]
    Transform[] instantiateEnemy;

    [SerializeField]
    CharacterMovement characterMovement;
    int instantiatePoints;

    public List<GameObject> blastedCylinder;

    public float timer;
    FunctionTimer functionTimer;
    // Start is called before the first frame update
    void Start()
    {
        //currentEnemy = totalEnemy;
        instantiatePoints = instantiateEnemy.Length;
        functionTimer = FunctionTimer.Create(GameEnded, timer);
        InstantiateEnemies();
    }

    void GameEnded()
    {
        StartCoroutine(characterMovement.GameOver());
    }

    void InstantiateEnemies()
    {
        for (int i = 0; i < totalEnemy; i++)
        {
            Vector3 position = instantiateEnemy[Random.Range(1, instantiatePoints)].position + new Vector3(Random.Range(0, 4), 0, Random.Range(0, 4));
            Instantiate(enemy[Random.Range(0, 2)], position, Quaternion.identity);
        }
        timer += 10;
    }

    void remakeCylinders()
    {
        for (int i = 0; i < blastedCylinder.Count; i++)
        {
            BlastCylinder cylinder = blastedCylinder[i].GetComponent<BlastCylinder>();
            cylinder.blasted = false;
            cylinder.currentHealth = cylinder.health;
            cylinder.broken_cylinder.SetActive(false);
            cylinder.cylinder.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        uIGameScreen.timer_text.text = (int)functionTimer.timer + "";
        int count = GameObject.FindGameObjectsWithTag("enemy").Length;
        if (count == 0)
        {
            totalEnemy += 2;
            InstantiateEnemies();
            characterMovement.Health += 25;
            characterMovement.score += (int)functionTimer.timer;
            functionTimer.RestartTimer(timer);
            remakeCylinders();
        }
    }
}
