using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.SendMessage("ReceiveDamage", SendMessageOptions.DontRequireReceiver);
        }
        Destroy(this.gameObject);
    }
}
