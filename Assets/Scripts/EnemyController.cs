using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    Transform Player;
    public GameObject pointsTextbox;
    public GameObject stabilityTextbox;
    public GameObject enemyCollider;
    public float speedRot = 5.0f;
    int points;
    float speed;
    int stability;
    float shakeSpeed; 
    float shakeAmount; 
    public float MaxShakeSpeed = 50.0f; 
    public float MaxShakeAmount = 0.05f; 
    public Color fullStability;
    public Color freeStability; 
    public int damageRangeMin = 5;
    public int damageRangeMax = 10;
    SpriteRenderer spriteBorder;

    // Start is called before the first frame update
    void Start(){
        spriteBorder = enemyCollider.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        speed = Random.Range(0.2f, 1.0f);
        stability = Random.Range(1, 100);
        
        if(stability > 70) // 71 - 100
            points = Random.Range(1, 2);
        else if(stability > 50) // 51 - 70
            points = Random.Range(3, 4);
        else if(stability > 25) // 26 - 50
            points = Random.Range(5, 6);
        else  // 1 - 25
            points = Random.Range(6, 7);

        Player = GameObject.FindWithTag("Player").transform;    
    }


    // Update is called once per frame
    void Update(){
        FollowPlayer();
        CheckStatus();
        Stylize();
    }

    void CheckStatus(){
        if(stability < 1){
            Destroy(gameObject);
            GameStatus.actualGameEnemiesKilled++;
        }
    }

    public void TakeCharge(int addpoints){
        points += addpoints;
        stability -= Random.Range(damageRangeMin, damageRangeMax);
    }

    public int GetPoints(){
        return points;
    }

    public int GetStability(){
        return stability;
    }

    void FollowPlayer(){
        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        
        transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        enemyCollider.transform.rotation = Quaternion.Slerp(enemyCollider.transform.rotation, rotation, speedRot * Time.deltaTime);
    }

    void Stylize(){
        //show status
        pointsTextbox.GetComponent<TMP_Text>().text = (points).ToString();
        stabilityTextbox.GetComponent<TMP_Text>().text = (stability).ToString() + "%";
        //calc color
        spriteBorder.color =  Color.Lerp(freeStability, fullStability, stability/100.0f);
        //calc shake
        shakeSpeed = (100.0f - (float)stability) * MaxShakeSpeed / 100.0f;
        shakeAmount = (100.0f - (float)stability) * MaxShakeAmount / 100.0f;
        spriteBorder.transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, transform.position.y + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, 0);
    }
}
