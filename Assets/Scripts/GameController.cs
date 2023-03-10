using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    Vector3 screenDimensions;
    int[] negativePositive = {-1,1};

    int numberOfEnemies = 2;
    // Start is called before the first frame update
    void Start(){
        screenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
    }

    // Update is called once per frame
    void Update(){
        if(!player.GetComponent<PlayerController>().CheckStatus()){
            GameStatus.UpdateScore();
            GameStatus.isGameOver = true;
            Destroy(gameObject);
        }
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            for(int i = 0 ; i < numberOfEnemies ; i++)
                StartCoroutine(GenerateEnemyCoroutine());
            numberOfEnemies++;
        }

    }

    IEnumerator GenerateEnemyCoroutine(){
        GenerateEnemy(screenDimensions.x * negativePositive[Random.Range(0, negativePositive.Length)], screenDimensions.y * negativePositive[Random.Range(0, negativePositive.Length)]);
        yield return new WaitForSeconds(Random.Range(1, 4));
    }

    void GenerateEnemy(float x, float y){
        Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
    }
}
