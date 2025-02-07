using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] engelPrefablar; // Engellerin prefablarý (binalar)
    public Transform spawnNoktasi; // Engellerin spawnlanacaðý nokta
    public float spawnAraligi = 3f; // Kaç saniyede bir spawn olacak
    public float minXKayma = -10f, maxXKayma = 10f; // X ekseni için rastgele kayma
    public float zAraligi = 15f; // Engellerin Z ekseninde birbirine uzaklýðý
    private float sonrakiZ; // Bir sonraki engelin spawn olacaðý Z konumu

    void Start()
    {
        sonrakiZ = spawnNoktasi.position.z; // Ýlk spawn noktasý
        StartCoroutine(EngelleriSpawnla());
    }

    IEnumerator EngelleriSpawnla()
    {
        while (true)
        {
            yield return StartCoroutine(EngelSirasiSpawnla());
            yield return new WaitForSeconds(spawnAraligi);
        }
    }

    IEnumerator EngelSirasiSpawnla()
    {
        foreach (GameObject engel in engelPrefablar)
        {
            float rastgeleX = Random.Range(spawnNoktasi.position.x + minXKayma, spawnNoktasi.position.x + maxXKayma);
            float zOffset = Random.Range(-2f, 2f); // Engellerin Z ekseninde hafif rastgele kaymasý için

            Vector3 spawnKonumu = new Vector3(
                Mathf.Clamp(rastgeleX, spawnNoktasi.position.x - 10, spawnNoktasi.position.x + 10),
                spawnNoktasi.position.y,
                sonrakiZ + zOffset // Z ekseninde küçük rastgele kayma
            );

            Instantiate(engel, spawnKonumu, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0f, 1.5f)); // Her engelin farklý zamanlarda spawn olmasý için rastgele bekleme süresi
        }

        sonrakiZ += zAraligi; // Engellerin ileriye doðru gitmesi için Z'yi artýr
    }
}