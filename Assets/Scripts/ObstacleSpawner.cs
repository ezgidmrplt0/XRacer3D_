using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] engelPrefablar; // Engellerin prefablar� (binalar)
    public Transform spawnNoktasi; // Engellerin spawnlanaca�� nokta
    public float spawnAraligi = 3f; // Ka� saniyede bir spawn olacak
    public float minXKayma = -10f, maxXKayma = 10f; // X ekseni i�in rastgele kayma
    public float zAraligi = 15f; // Engellerin Z ekseninde birbirine uzakl���
    private float sonrakiZ; // Bir sonraki engelin spawn olaca�� Z konumu

    void Start()
    {
        sonrakiZ = spawnNoktasi.position.z; // �lk spawn noktas�
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
            float zOffset = Random.Range(-2f, 2f); // Engellerin Z ekseninde hafif rastgele kaymas� i�in

            Vector3 spawnKonumu = new Vector3(
                Mathf.Clamp(rastgeleX, spawnNoktasi.position.x - 10, spawnNoktasi.position.x + 10),
                spawnNoktasi.position.y,
                sonrakiZ + zOffset // Z ekseninde k���k rastgele kayma
            );

            Instantiate(engel, spawnKonumu, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0f, 1.5f)); // Her engelin farkl� zamanlarda spawn olmas� i�in rastgele bekleme s�resi
        }

        sonrakiZ += zAraligi; // Engellerin ileriye do�ru gitmesi i�in Z'yi art�r
    }
}