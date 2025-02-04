using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucakhareket : MonoBehaviour
{
    public float hareketHizi = 5f;  // Ba�lang�� hareket h�z�
    public float yukseklikHizi = 5f;  // Yukar�-a�a�� hareket h�z�
    public float maxYukseklik = 4f;  // Maksimum yukar� ��kma limiti
    public float minYukseklik = 0f;  // Minimum yukar� inme limiti
    public float hizArtisKademesi = 1f;  // H�z�n artma oran�
    public float geriDonusHizi = 2f;  // Normal pozisyona d�n�� h�z�

    private float yPozisyonu;  // U�a��n y eksenindeki pozisyonu
    private float previousZPosition = 0f; // �nceki Z pozisyonu

    private void Start()
    {
        yPozisyonu = transform.position.y;
        previousZPosition = transform.position.z; // Ba�lang��ta Z pozisyonunu kaydet
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        // Space tu�una bas�ld���nda yukar� ��k
        if (Input.GetKey(KeyCode.Space))
        {
            yPozisyonu += yukseklikHizi * Time.deltaTime;
            if (yPozisyonu > maxYukseklik)
            {
                yPozisyonu = maxYukseklik;
            }
        }
        else
        {
            // Space b�rak�ld���nda yava��a minYukseklik seviyesine in
            yPozisyonu = Mathf.Lerp(yPozisyonu, minYukseklik, geriDonusHizi * Time.deltaTime);
        }

        // Z eksenindeki hareketi kontrol et (h�z� artt�rma mekani�i)
        float distanceTravelledZ = transform.position.z - previousZPosition;
        if (Mathf.Abs(distanceTravelledZ) >= 10f)
        {
            hareketHizi += hizArtisKademesi;
            previousZPosition = transform.position.z;
        }

        // Yatay ve ileri-geri hareket (art�k ileri-geri kontrol yok, hep ileri gidiyor)
        Vector3 hareket = new Vector3(horizontal, 0f, 1f); // Z ekseninde hep ileri gitmesi i�in '1f' sabit

        // Yeni pozisyonu olu�tur
        Vector3 yeniPozisyon = transform.position + hareket * hareketHizi * Time.deltaTime;

        // Y pozisyonunu g�ncelle
        yeniPozisyon.y = yPozisyonu;

        // U�a��n pozisyonunu g�ncelle
        transform.position = yeniPozisyon;
    }
}
