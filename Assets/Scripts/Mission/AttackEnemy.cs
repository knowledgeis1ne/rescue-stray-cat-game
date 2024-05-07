using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    public static AttackEnemy instance;

    public GameObject p_enemy;
    public List<Transform> enemyList;
    public int enemyCount;
    public int attackCount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (Transform trans in p_enemy.transform)
        {
            enemyList.Add(trans);
            enemyCount++;
        }
    }
}
