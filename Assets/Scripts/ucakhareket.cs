using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucakhareket : MonoBehaviour
{
    public float hareketHizi = 5f;  // Hareket h�z�
    public float yukseklikHizi = 2f;  // Daha yumu�ak y�kselme/al�alma
    public float maxYukseklik = 4f;  // Maksimum yukar� ��kma limiti
    public float minYukseklik = 0f;  // Minimum yukar� inme limiti
    public float hizArtisKademesi = 0.5f;  // H�z�n artma oran�
    public float donusHassasiyeti = 2f; // D�n��lerin yumu�akl���
    public float maxYatisAcisi = 20f; // Maksimum yat�� a��s� (kanat e�imi)
    public float maxBurunAcisi = 15f; // Maksimum burun a��s� (u�ak burnu)
    public float rotasyonGecikme = 3f; // Rotasyon yumu�atma s�resi

    private float yPozisyonu;  // U�a��n y eksenindeki pozisyonu
    private float previousZPosition = 0f; // �nceki Z pozisyonu
    private float hedefYatisAcisi = 0f; // Yumu�ak ge�i� i�in hedef yat�� a��s�
    private float hedefBurunAcisi = 0f; // Yumu�ak ge�i� i�in hedef burun a��s�

    private void Start()
    {
        yPozisyonu = transform.position.y;
        previousZPosition = transform.position.z;

        // U�a��n ba�lang�� y�n�n� ayarla (Y ekseni 90 derece)
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    private void Update()
    {
        float yatayHareket = 0f; // A ve D i�in sa�-sol hareket
        float dikeyHareket = 0f; // W ve S i�in yukar�-a�a�� hareket

        // W ve S ile yukar� ve a�a�� hareket (burun a��s�)
        if (Input.GetKey(KeyCode.W))
        {
            dikeyHareket = 1f; // Yukar� ��k
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dikeyHareket = -1f; // A�a�� in
        }

        // A ve D ile sola ve sa�a hareket (kanat yat���)
        if (Input.GetKey(KeyCode.A))
        {
            yatayHareket = -1f; // Sola git
        }
        else if (Input.GetKey(KeyCode.D))
        {
            yatayHareket = 1f; // Sa�a git
        }

        // Y�ksekli�i yumu�ak �ekilde de�i�tir
        yPozisyonu = Mathf.Lerp(yPozisyonu, yPozisyonu + (dikeyHareket * yukseklikHizi), Time.deltaTime * 2f);
        yPozisyonu = Mathf.Clamp(yPozisyonu, minYukseklik, maxYukseklik);

        // H�z� artt�rma mekani�i
        float distanceTravelledZ = transform.position.z - previousZPosition;
        if (Mathf.Abs(distanceTravelledZ) >= 10f)
        {
            hareketHizi += hizArtisKademesi;
            previousZPosition = transform.position.z;
        }

        // Hareket vekt�r�n� olu�tur
        Vector3 hareket = new Vector3(yatayHareket, 0f, 1f); // Z ekseninde hep ileri gidecek

        // Yeni pozisyonu hesapla
        Vector3 yeniPozisyon = transform.position + hareket * hareketHizi * Time.deltaTime;
        yeniPozisyon.y = yPozisyonu; // Y�kseklik ayar�

        // U�a��n pozisyonunu g�ncelle
        transform.position = yeniPozisyon;

        // Daha yumu�ak d�n��ler i�in hedef a��lar� belirle
        hedefYatisAcisi = Mathf.Lerp(hedefYatisAcisi, -yatayHareket * maxYatisAcisi, Time.deltaTime * rotasyonGecikme);
        hedefBurunAcisi = Mathf.Lerp(hedefBurunAcisi, dikeyHareket * maxBurunAcisi, Time.deltaTime * rotasyonGecikme);

        // U�a��n rotasyonunu yumu�ak�a g�ncelle
        Quaternion hedefRotasyon = Quaternion.Euler(hedefBurunAcisi, 90f, hedefYatisAcisi);
        transform.rotation = Quaternion.Lerp(transform.rotation, hedefRotasyon, Time.deltaTime * donusHassasiyeti);
    }
}
