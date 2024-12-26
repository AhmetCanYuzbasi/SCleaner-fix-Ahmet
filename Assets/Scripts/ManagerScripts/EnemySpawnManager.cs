using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance{get; private set;}
    private GameObject[] _enemyArray;
    [SerializeField] Collider2D spawnAreaCollider;
    [SerializeField] GameObject enemyPrefab;
    
    [SerializeField] LayerMask _invalidSpawnLayers;

    [SerializeField] int enemyCount = 10;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Awake(){
        if (instance != null && instance != this){
            Destroy(this);
        } else {
            instance = this;
        }
        _enemyArray = new GameObject[enemyCount];
        CalculateBoundMinMax();
    }

    void Start(){
        //StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies(){

        for (int i = 0; i < _enemyArray.Length; ++i){
            Vector2 spawnPos = CalculateRandomSpawnLocation();
            _enemyArray[i] = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(.5f);
        }
    }

    private Vector2 CalculateRandomSpawnLocation(){

        Vector2 spawnPos = Vector2.zero;
        bool isValidSpawnPos = false;
        float maxAttemptCount = 100;
        float currentAttemptCount = 0;
        int WallLayer = LayerMask.NameToLayer("Wall");

        while (!isValidSpawnPos && currentAttemptCount < maxAttemptCount){

            spawnPos = CalculateRandomPositionInCollider();
            Collider2D[] collidersInArea = Physics2D.OverlapCircleAll(spawnPos, 2f);
            bool isInvalidSpot = false;

            foreach (Collider2D coll in collidersInArea){
                if(((1 << coll.gameObject.layer) & _invalidSpawnLayers) != 0){
                    Debug.Log("Invalid spot!");
                    isInvalidSpot = true;
                    break;
                }
            }

            if(!isInvalidSpot){
                isValidSpawnPos = true;
            }

            currentAttemptCount++;
        }
        //Debug.Log(spawnPos);
        return spawnPos;
    }

    private Vector2 CalculateRandomPositionInCollider(){

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        return new Vector2(randomX, randomY);
    }

    private void CalculateBoundMinMax(float offset = 5f){
        Bounds collBounds = spawnAreaCollider.bounds;
        minBounds = new Vector2(collBounds.min.x + offset, collBounds.min.y + offset);
        maxBounds = new Vector2(collBounds.max.x - offset, collBounds.max.y - offset);
    }

    

}
