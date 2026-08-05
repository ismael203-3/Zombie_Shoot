using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int vida = 100;
    [SerializeField] private Image[] Sangre;
    [SerializeField] public float a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = 0;
        Colores();
    }

    // Update is called once per frame
    void Update()
    {
        a = Mathf.InverseLerp(100, 0, vida);

        if (vida <= 0) 
        {
            a = 1f;
            Debug.Log("Jugador a Muerto");
            
        }
        Colores();
        //a = Mathf.Clamp(a, 0f, 1f);
    }

    private void Colores() 
    {
        foreach (Image item in Sangre) 
        {
            Color c = new Color(item.color.r, item.color.g, item.color.b, a);
            item.color = c;
        }

    }
}
