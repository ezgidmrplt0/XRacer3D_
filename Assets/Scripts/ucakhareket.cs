using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucakhareket : MonoBehaviour
{
    public float hareketHizi = 5f;  // Baþlangýç hareket hýzý
    public float yukseklikHizi = 5f;  // Yukarý-aþaðý hareket hýzý
    public float maxYukseklik = 4f;  // Maksimum yukarý çýkma limiti
    public float minYukseklik = 0f;  // Minimum yukarý inme limiti
    public float hizArtisKademesi = 1f;  // Hýzýn artma oraný
    public float geriDonusHizi = 2f;  // Normal pozisyona dönüþ hýzý

    private float yPozisyonu;  // Uçaðýn y eksenindeki pozisyonu
    private float previousZPosition = 0f; // Önceki Z pozisyonu

    private void Start()
    {
        yPozisyonu = transform.position.y;
        previousZPosition = transform.position.z; // Baþlangýçta Z pozisyonunu kaydet
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        // Space tuþuna basýldýðýnda yukarý çýk
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
            // Space býrakýldýðýnda yavaþça minYukseklik seviyesine in
            yPozisyonu = Mathf.Lerp(yPozisyonu, minYukseklik, geriDonusHizi * Time.deltaTime);
        }

        // Z eksenindeki hareketi kontrol et (hýzý arttýrma mekaniði)
        float distanceTravelledZ = transform.position.z - previousZPosition;
        if (Mathf.Abs(distanceTravelledZ) >= 10f)
        {
            hareketHizi += hizArtisKademesi;
            previousZPosition = transform.position.z;
        }

        // Yatay ve ileri-geri hareket (artýk ileri-geri kontrol yok, hep ileri gidiyor)
        Vector3 hareket = new Vector3(horizontal, 0f, 1f); // Z ekseninde hep ileri gitmesi için '1f' sabit

        // Yeni pozisyonu oluþtur
        Vector3 yeniPozisyon = transform.position + hareket * hareketHizi * Time.deltaTime;

        // Y pozisyonunu güncelle
        yeniPozisyon.y = yPozisyonu;

        // Uçaðýn pozisyonunu güncelle
        transform.position = yeniPozisyon;
    }
}
