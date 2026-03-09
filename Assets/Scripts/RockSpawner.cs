using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Inställningar")]
    public GameObject rockPrefab; // Dra in din sten-prefab här!
    public float spawnInterval = 1.5f; // Hur ofta en sten ska falla (i sekunder)
    public float spawnWidth = 20f; // Hur brett område stenarna kan spridas på

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRock();
            timer = 0f;
        }
    }

    void SpawnRock()
    {
        // Slumpa fram en X-position baserat på hur brett du vill att de ska falla
        float randomX = Random.Range(-spawnWidth / 2f, spawnWidth / 2f);

        // Sätt startpositionen relativt till Spawnerns position
        Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, 0f);

        // Skapa stenen!
        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
    }

    // Detta ritar ut en röd linje i Editorn så du ser exakt var stenarna kan spawna
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - new Vector3(spawnWidth / 2f, 0, 0), transform.position + new Vector3(spawnWidth / 2f, 0, 0));
    }
}