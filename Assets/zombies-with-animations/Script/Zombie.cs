using Unity.VisualScripting;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class Zombie : MonoBehaviour
{
    private NavMeshAgent agent;
    private IEstadosZombies _estadosZombies;

    [Header ("Sonidos")]
    [SerializeField] private AudioSource pasos;
    [SerializeField] private AudioSource sonidoZombie;
    [SerializeField] private AudioSource golpe;


    [Header ("Distancia de Vision")]
    [SerializeField] private float radioAtaque;
    [SerializeField] private float radioSeguidor;
    [SerializeField] private float distanciaVision;

    [Header ("Velocidad de Movimiento")]
    [SerializeField, Range(0.3f, 2)] private float speed;

    private Transform target;
    private Animator animator;
    private Ray ray;

    [Header ("Objetivo")]
    [SerializeField] private Player jugador;
    [SerializeField] private bool dentro;
    [SerializeField] private float distanciaTarget;





    private void Start()
    {

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(DetectarJugador());
        SeleccionarVelocidad();
        
        _estadosZombies = new Quieto(transform, animator);
    }

    private void Update()
    {
        ray = new Ray(transform.position + new Vector3(0, 1f, 0), (transform.forward * distanciaVision));
        Debug.DrawRay(ray.origin, ray.direction * distanciaVision, Color.green);
        _estadosZombies.Accion(agent);
    }




    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, radioAtaque);
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, Vector3.up, radioSeguidor);
    }

    private void SeleccionarVelocidad() 
    {
        int random = Random.Range(0, 2);
        Debug.Log(random);
        if (random == 0)
        {
            speed = 0.3f;
        }
        else 
        {
            speed = 2;
        }
    }

    private void Ataque() 
    {
        AudioSource audio = Instantiate(golpe, transform);
        audio.gameObject.transform.position = transform.position;
        audio.Play();
        Destroy(audio.gameObject, 1);
        jugador.vida -= 20;
    }

    private void SonidoPasos() 
    {
        AudioSource audio = Instantiate(pasos, transform);
        audio.Play();
        Destroy(audio.gameObject,0.5f);
        
    }

    private void SonidoZombie() 
    {
        AudioSource audio = Instantiate(sonidoZombie, transform);
        audio.gameObject.transform.position = transform.position; 
        audio.Play();
        Destroy(audio.gameObject, 8);
    } 

    IEnumerator DetectarJugador() 
    {
        while (true)
        {
            if (dentro) 
            {
                Vector3 distancia = transform.position - target.position;
                distanciaTarget = distancia.sqrMagnitude;

                float sqrRadioSeguidor = radioSeguidor * radioSeguidor;
                float sqrRadioAtaque   = radioAtaque * radioAtaque;

                if (distanciaTarget > sqrRadioSeguidor) 
                {
                    _estadosZombies = new Quieto(transform, animator);
                    dentro = false;
                }
                else if (distanciaTarget < sqrRadioAtaque)
                {
                    _estadosZombies = new Atacar(transform, speed, animator);
                    animator.SetBool("Atacar", true);
                }
                else { 
                    _estadosZombies = new Seguir(target, gameObject, speed, animator);
                    animator.SetBool("Atacar", false);
                }
            
            }
            else if (Physics.Raycast(ray, out RaycastHit hit, distanciaVision))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    jugador = hit.transform.GetComponent<Player>();
                    target = hit.transform;
                    _estadosZombies = new Seguir(target, gameObject, speed, animator);
                    dentro = true;
                    SonidoZombie();
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}