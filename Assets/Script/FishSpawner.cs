using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs; 
    public bool spawnFromRight; 
    public Vector2 spawnAreaMin; 
    public Vector2 spawnAreaMax; 
    public float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFish), 0f, spawnInterval);
    }

    private void SpawnFish()
    {
        
        float spawnX = spawnFromRight ? spawnAreaMax.x : spawnAreaMin.x;
        float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        
        GameObject selectedFishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

       
        GameObject newFish = Instantiate(selectedFishPrefab, spawnPosition, Quaternion.identity);

       
        Vector3 scale = newFish.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (spawnFromRight ? -1 : 1); 
        newFish.transform.localScale = scale;

    
    }
}
