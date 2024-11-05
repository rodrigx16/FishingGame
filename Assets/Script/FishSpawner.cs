using System.Collections;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public float spawnInterval = 2f;
    public Vector2 spawnYRange = new Vector2(-3f, -1f); // Altura no oceano para o spawn
    public bool spawnFromRight;

    private void Start()
    {
        StartCoroutine(SpawnFish());
    }

    private IEnumerator SpawnFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            // Escolhe um peixe aleatoriamente
            GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

            // Define a posição de spawn
            float spawnY = Random.Range(spawnYRange.x, spawnYRange.y);
            Vector3 spawnPosition = new Vector3(
                spawnFromRight ? 10f : -10f, // Ajuste a posição X conforme necessário
                spawnY,
                0f
            );

            // Instancia o peixe na posição de spawn
            GameObject fish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

            // Define a direção do peixe
            if (spawnFromRight)
            {
                fish.transform.localScale = new Vector3(-1, 1, 1); // Espelha o peixe se vier da direita
            }

            // Adiciona movimento ao peixe
            Rigidbody2D rb = fish.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(spawnFromRight ? -2f : 2f, 0f);
            }
        }
    }
}
