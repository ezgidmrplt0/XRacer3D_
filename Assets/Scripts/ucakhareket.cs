using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucakhareket : MonoBehaviour
{
    public float hareketHizi = 5f;  // Hareket hýzý
    public float yukseklikHizi = 2f;  // Daha yumuþak yükselme/alçalma
    public float maxYukseklik = 4f;  // Maksimum yukarý çýkma limiti
    public float minYukseklik = 0f;  // Minimum yukarý inme limiti
    public float hizArtisKademesi = 0.5f;  // Hýzýn artma oraný
    public float donusHassasiyeti = 2f; // Dönüþlerin yumuþaklýðý
    public float maxYatisAcisi = 20f; // Maksimum yatýþ açýsý (kanat eðimi)
    public float maxBurunAcisi = 15f; // Maksimum burun açýsý (uçak burnu)
    public float rotasyonGecikme = 3f; // Rotasyon yumuþatma süresi

    private float yPozisyonu;  // Uçaðýn y eksenindeki pozisyonu
    private float previousZPosition = 0f; // Önceki Z pozisyonu
    private float hedefYatisAcisi = 0f; // Yumuþak geçiþ için hedef yatýþ açýsý
    private float hedefBurunAcisi = 0f; // Yumuþak geçiþ için hedef burun açýsý

    private void Start()
    {
        yPozisyonu = transform.position.y;
        previousZPosition = transform.position.z;

        // Uçaðýn baþlangýç yönünü ayarla (Y ekseni 90 derece)
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    private void Update()
    {
        float yatayHareket = 0f; // A ve D için sað-sol hareket
        float dikeyHareket = 0f; // W ve S için yukarý-aþaðý hareket

        // W ve S ile yukarý ve aþaðý hareket (burun açýsý)
        if (Input.GetKey(KeyCode.W))
        {
            dikeyHareket = 1f; // Yukarý çýk
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dikeyHareket = -1f; // Aþaðý in
        }

        // A ve D ile sola ve saða hareket (kanat yatýþý)
        if (Input.GetKey(KeyCode.A))
        {
            yatayHareket = -1f; // Sola git
        }
        else if (Input.GetKey(KeyCode.D))
        {
            yatayHareket = 1f; // Saða git
        }

        // Yüksekliði yumuþak þekilde deðiþtir
        yPozisyonu = Mathf.Lerp(yPozisyonu, yPozisyonu + (dikeyHareket * yukseklikHizi), Time.deltaTime * 2f);
        yPozisyonu = Mathf.Clamp(yPozisyonu, minYukseklik, maxYukseklik);

        // Hýzý arttýrma mekaniði
        float distanceTravelledZ = transform.position.z - previousZPosition;
        if (Mathf.Abs(distanceTravelledZ) >= 10f)
        {
            hareketHizi += hizArtisKademesi;
            previousZPosition = transform.position.z;
        }

        // Hareket vektörünü oluþtur
        Vector3 hareket = new Vector3(yatayHareket, 0f, 1f); // Z ekseninde hep ileri gidecek

        // Yeni pozisyonu hesapla
        Vector3 yeniPozisyon = transform.position + hareket * hareketHizi * Time.deltaTime;
        yeniPozisyon.y = yPozisyonu; // Yükseklik ayarý

        // Uçaðýn pozisyonunu güncelle
        transform.position = yeniPozisyon;

        // Daha yumuþak dönüþler için hedef açýlarý belirle
        hedefYatisAcisi = Mathf.Lerp(hedefYatisAcisi, -yatayHareket * maxYatisAcisi, Time.deltaTime * rotasyonGecikme);
        hedefBurunAcisi = Mathf.Lerp(hedefBurunAcisi, dikeyHareket * maxBurunAcisi, Time.deltaTime * rotasyonGecikme);

        // Uçaðýn rotasyonunu yumuþakça güncelle
        Quaternion hedefRotasyon = Quaternion.Euler(hedefBurunAcisi, 90f, hedefYatisAcisi);
        transform.rotation = Quaternion.Lerp(transform.rotation, hedefRotasyon, Time.deltaTime * donusHassasiyeti);
    }
}
