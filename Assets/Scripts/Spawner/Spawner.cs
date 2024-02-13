using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class Spawner : MonoBehaviour
{
    public static Action<GameObject> onEnemyDied;
    [SerializeField] private Enemy[] enemyPrefabs;
    private SpawnPoint[] spawnPoints;
    [HideInInspector] public List<GameObject> enemies;
    private bool tankAlive=true;
    private void OnEnable()
    {
        onEnemyDied += DeleteEnemyFromList;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }
    private void OnDisable()
    {
        onEnemyDied -= DeleteEnemyFromList;
    }
    private void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(UnityEngine.Random.Range(1, 4))).TakeWhile(x => tankAlive).TakeUntilDisable(this).Subscribe(x =>
           {
               GenerateEnemy(GetRandomSpawnPoint(spawnPoints), GetRandomEnemy(enemyPrefabs));
           });
    }
    private void GenerateEnemy(Vector3 spawnTransform,GameObject spawnObject)
    {
        enemies.Add(Instantiate(spawnObject, spawnTransform, Quaternion.identity, this.transform));
    }
    private void DeleteEnemyFromList(GameObject diedEnemy)
    {
        Destroy(diedEnemy);
        enemies.Remove(diedEnemy);
        Debug.Log("Ostalos' enemies-" + enemies.Count);
        GameManager.instance.IncreaseScore();
    }
    private GameObject GetRandomEnemy(Enemy[] enemiesMass)
    {
        return enemiesMass[UnityEngine.Random.Range(0, enemiesMass.Length)].gameObject;
    }
    private Vector3 GetRandomSpawnPoint(SpawnPoint[] pointsMass)
    {
        return pointsMass[UnityEngine.Random.Range(0, pointsMass.Length)].gameObject.transform.position;
    }
    public void DestroyAllEnemies()
    {
        foreach(GameObject obj in enemies)
        {
            Destroy(obj);
        }
        enemies.Clear();
    }
}
