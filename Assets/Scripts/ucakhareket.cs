﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ucakhareket : MonoBehaviour
{
    public float hareketHizi = 5f;
    public float yukseklikHizi = 5f;
    public float maxYukseklik = 4f;
    public float minYukseklik = 0f;
    public float hizArtisKademesi = 10f;
    public float geriDonusHizi = 2f;
    public float egimHizi = 2f;
    public float maxEgilmeAcisi = 20f;

    private float yPozisyonu;
    private float previousZPosition = 0f;
    private float mevcutEgilmeX = 0f;
    private float mevcutEgilmeZ = 0f;

    private float baslangicZ; // 🚀 Uçağın başlangıç Z konumunu tutacak
    public TextMeshProUGUI mesafeText; // 🏁 Mesafeyi gösterecek UI Text

    void Start()
    {
        yPozisyonu = transform.position.y;
        previousZPosition = transform.position.z;
        baslangicZ = transform.position.z; // Başlangıç Z pozisyonunu kaydet

        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            yPozisyonu += yukseklikHizi * Time.deltaTime;
            yPozisyonu = Mathf.Clamp(yPozisyonu, minYukseklik, maxYukseklik);
        }
        else
        {
            yPozisyonu = Mathf.Lerp(yPozisyonu, minYukseklik, geriDonusHizi * Time.deltaTime);
        }

        if (transform.position.z - previousZPosition >= 100f)
        {
            hareketHizi += hizArtisKademesi;
            previousZPosition = transform.position.z;
        }

        Vector3 hareket = new Vector3(horizontal, 0f, 1f);
        Vector3 yeniPozisyon = transform.position + hareket * hareketHizi * Time.deltaTime;
        yeniPozisyon.y = yPozisyonu;
        transform.position = yeniPozisyon;

        float hedefEgilmeX = horizontal * maxEgilmeAcisi;
        float hedefEgilmeZ = Input.GetKey(KeyCode.Space) ? -maxEgilmeAcisi : 0f;

        mevcutEgilmeX = Mathf.Lerp(mevcutEgilmeX, hedefEgilmeX, Time.deltaTime * egimHizi);
        mevcutEgilmeZ = Mathf.Lerp(mevcutEgilmeZ, hedefEgilmeZ, Time.deltaTime * egimHizi);

        Quaternion hedefRotasyon = Quaternion.Euler(mevcutEgilmeX, 90f, mevcutEgilmeZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, hedefRotasyon, Time.deltaTime * egimHizi);

        // 🚀 **Mesafeyi Güncelle ve UI'ye Yazdır**
        float katEdilenMesafe = transform.position.z - baslangicZ;
        mesafeText.text = katEdilenMesafe.ToString("F1") + " m";
    }
}
