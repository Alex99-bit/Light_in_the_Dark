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
        if (!isAttacking)
        {
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, player.position) < visionRange)
        {
            timeSinceLastSawPlayer = 0f;

            if (!isAttacking)
            {
                isPatrolling = false;
                isAttacking = true;
            }

            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                // Implementa aquí tu lógica de disparo
                Debug.Log("¡Disparando al jugador!");
            }
            else
            {
                // Si el jugador está dentro del rango de visión pero fuera del rango de ataque,
                // detener la persecución y comenzar a patrullar nuevamente
                isPatrolling = true;
                isAttacking = false;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }
        else if (!isPatrolling)
        {
            if (timeSinceLastSawPlayer > timeToLoseTarget)
            {
                isPatrolling = true;
                isAttacking = false;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }

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
