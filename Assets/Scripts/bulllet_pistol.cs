using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulllet_pistol : MonoBehaviour
{
    float speed = 10.0f; 
    int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            GameObject enemy = other.transform.parent.gameObject;
            EnemyController enemyScript = enemy.GetComponent<EnemyController>();
            enemyScript.TakeCharge(damage);
            
            Destroy(gameObject);
        }
    } 
}
