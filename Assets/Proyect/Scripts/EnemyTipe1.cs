using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTipe1 : MonoBehaviour
{
    #region "Light stuff"
        // Referencia al renderer del objeto
        public Renderer meshRenderer;

        // Colores originales y de da�o
        private Color colorOriginal;
        public Color nuevoColor;
    #endregion;

    #region "Vida"
        public int vidaEnemy, vidaMax;
    #endregion;

    #region "Cosas para la IA"
        public Transform[] patrolPoints;
        public float visionRange = 10f;
        public float attackRange = 5f;
        public float timeToLoseTarget = 30f;

        [SerializeField]
        private Transform player;
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private int currentPatrolIndex = 0;
        [SerializeField]
        private bool isPatrolling = true;
        [SerializeField]
        private bool isAttacking = false;
        [SerializeField]
        private float timeSinceLastSawPlayer = 0f;
    #endregion;

    // Duraci�n del cambio de color
    public float duracionCambioColor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar la vida
        vidaEnemy = vidaMax;

        // Obtener el color original
        colorOriginal = meshRenderer.material.color;

        #region "Cosas para la IA"
        agent = this.GetComponent<NavMeshAgent>();

        // Iniciar patrulla
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
        #endregion;
    }

    // Update is called once per frame
    void Update()
    { 
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (vidaEnemy <= 0)
        {
            // En su lugar ira una animacion de muerte y despues de un tiempo desaparecera el cadaver
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate() {
        ThreadAI();
    }

    // M�todo para cambiar el color del objeto
    public void CambiarColor(Color color)
    {
        // Asignar el nuevo color al material del objeto
        meshRenderer.material.color = color;
    }

    private void ThreadAI(){
        // Actualizar el tiempo desde que se vio al jugador por última vez
        if (!isAttacking)
        {
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        // Si el jugador está dentro del rango de visión
        if (Vector3.Distance(transform.position, player.position) < visionRange)
        {
            // Actualizar la última vez que se vio al jugador
            timeSinceLastSawPlayer = 0f;

            // Cambiar al estado de ataque
            isPatrolling = false;
            isAttacking = true;

            // Moverse hacia el jugador
            agent.SetDestination(player.position);

            // Si el jugador está dentro del rango de ataque
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                // Disparar al jugador (aquí debes implementar tu lógica de disparo)
                Debug.Log("¡Disparando al jugador!");
            }
        }
        else
        {
            // Si el enemigo no ve al jugador
            if (!isPatrolling)
            {
                // Si el enemigo ha perdido de vista al jugador durante más de 30 segundos
                if (timeSinceLastSawPlayer > timeToLoseTarget)
                {
                    // Regresar al estado de patrullaje
                    isPatrolling = true;
                    isAttacking = false;

                    // Reiniciar patrulla
                    agent.SetDestination(patrolPoints[currentPatrolIndex].position);
                }
            }
        }

        // Si el enemigo está patrullando y llega al punto de patrulla actual, avanzar al siguiente punto
        if (isPatrolling && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet Light")){
            Destroy(other.gameObject);
            // Aplicar da�o
            vidaEnemy -= 25;

            // Cambiar color al recibir da�o
            CambiarColor(nuevoColor);
        }
    }
}
