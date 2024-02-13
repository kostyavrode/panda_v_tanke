using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    private void Start()
    {
        GetComponent<Enemy>().SetCharacteristics(speed, hp, damage,bloodEffect);
    }
}
