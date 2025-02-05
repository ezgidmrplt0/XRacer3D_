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
    public float egimHizi = 2f;  // Eðim deðiþim hýzý (daha yumuþak olmasý için düþürdüm)
    public float maxEgilmeAcisi = 20f; // Maksimum eðim açýsý

    private float yPozisyonu;  // Uçaðýn y eksenindeki pozisyonu
    private float previousZPosition = 0f; // Önceki Z pozisyonu
    private float mevcutEgilmeX = 0f; // Yatay eðim (X ekseni)
    private float mevcutEgilmeZ = 0f; // Dikey eðim (Z ekseni)

    private void Start()
    {
        yPozisyonu = transform.position.y;
        previousZPosition = transform.position.z;

        // Uçaðýn baþlangýç rotasyonunu Y ekseninde 90 derece yap
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
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

        // Yatay ve ileri-geri hareket
        Vector3 hareket = new Vector3(horizontal, 0f, 1f); // Z ekseninde hep ileri gitmesi için '1f' sabit

        // Yeni pozisyonu oluþtur
        Vector3 yeniPozisyon = transform.position + hareket * hareketHizi * Time.deltaTime;

        // Y pozisyonunu güncelle
        yeniPozisyon.y = yPozisyonu;

        // Uçaðýn pozisyonunu güncelle
        transform.position = yeniPozisyon;

        // --- Daha Smooth Eðilme Mekanizmasý ---
        float hedefEgilmeX = horizontal * maxEgilmeAcisi; // Saða/sola eðim (X ekseni)
        float hedefEgilmeZ = Input.GetKey(KeyCode.Space) ? -maxEgilmeAcisi : 0f; // Space tuþu basýlýnca aþaðý eðilme (Z ekseni)

        // Önceki deðerleri Lerp ile güncelleyerek geçiþi yumuþat
        mevcutEgilmeX = Mathf.Lerp(mevcutEgilmeX, hedefEgilmeX, Time.deltaTime * egimHizi);
        mevcutEgilmeZ = Mathf.Lerp(mevcutEgilmeZ, hedefEgilmeZ, Time.deltaTime * egimHizi);

        // Hedef rotasyonu oluþtur (Y ekseni 90 derece sabit kalýyor!)
        Quaternion hedefRotasyon = Quaternion.Euler(mevcutEgilmeX, 90f, mevcutEgilmeZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, hedefRotasyon, Time.deltaTime * egimHizi);
    }
}
