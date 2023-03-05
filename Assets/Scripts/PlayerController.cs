using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float SpeedMov = 1.0f;
    public float SpeedRot = 5.0f;
    public GameObject PlayerCollider;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject pointsTextbox;
    public int points = 1;

    // Start is called before the first frame update
    void Start(){
        points = 1;
    }

    // Update is called once per frame
    void Update(){
        Movement();
        Shot();

        pointsTextbox.GetComponent<TMP_Text>().text = (points).ToString();
    }

    void Movement(){

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - PlayerCollider.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        PlayerCollider.transform.rotation = Quaternion. Slerp(PlayerCollider.transform.rotation, rotation, SpeedRot * Time.deltaTime);

        if (Input.GetKey(KeyCode.D)){
			transform.position += Vector3.right * SpeedMov * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)){
			transform.position += Vector3.left* SpeedMov * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W)){
			transform.position += Vector3.up * SpeedMov * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)){
			transform.position += Vector3.down* SpeedMov * Time.deltaTime;
		}
    }

    void Shot(){
        if(GameStatus.focus == 0 && points > 0 && Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bullet, firePoint.position, PlayerCollider.transform.rotation);
            points--;
        }
    }

    public bool CheckStatus(){
        return points > -1;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            GameObject enemy = other.transform.parent.gameObject;
            EnemyController enemyScript = enemy.GetComponent<EnemyController>();
            if(Random.Range(1, 100) > enemyScript.GetStability()){
                //destroy player
                points = -1;
            }
            else {
                points += enemyScript.GetPoints();
                Destroy(enemy);
                GameStatus.actualGameEnemiesKilled++;
            }
            
        }
    } 
}
