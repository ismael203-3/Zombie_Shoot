using Unity.VisualScripting;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    private Renderer arbol;
    public AudioSource SonidoTalar;
    private float time;
    public bool damage;
    private float vida = 100f;

    private void Start()
    {
        arbol = GetComponent<Renderer>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Axe") 
        {
            SonidoTalar.Play();
            vida -= 20f;
            damage = true;
            arbol.material.color = Color.red;
            if (vida <= 0) Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (damage) 
        {
            time += Time.deltaTime;
            if (time > 0.5)
            {
                arbol.material.color = Color.white;
                time = 0f;
                damage = false;
            }
        }
        
    }
}
