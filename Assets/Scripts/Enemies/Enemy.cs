using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Enemy : MonoBehaviour
{
    private GameObject deathTrack;
    private float speed;
    private int hp;
    private int damage;
    private void FixedUpdate()
    {
        transform.Translate(-transform.forward*speed*Time.fixedDeltaTime);
    }
    public void ReceiveDamage()
    {
        hp--;
        if(hp<1)
        {
            Spawner.onEnemyDied?.Invoke(this.gameObject);
            Instantiate(deathTrack, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    public void SetCharacteristics(float newSpeed,int newHP,int newDamage,GameObject deathEffect)
    {
        speed = newSpeed;
        hp = newHP;
        damage = newDamage;
        deathTrack = deathEffect;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerZone"))
        {
            int dealedDamage=0;
            while (dealedDamage < damage)
                {
                    dealedDamage++;
                    other.SendMessage("ReceiveDamage", SendMessageOptions.DontRequireReceiver);
                };
            Observable.Timer(System.TimeSpan.FromSeconds(3)).TakeUntilDestroy(this.gameObject).Subscribe(x => 
            {
                Spawner.onEnemyDied?.Invoke(this.gameObject);
            }
            );
        }
    }
    private void OnDestroy()
    {
        
    }
}
