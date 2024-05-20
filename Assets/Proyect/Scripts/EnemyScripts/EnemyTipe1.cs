using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LD_GameManager;

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

    #region "Cosas para el disparo"
        // Prefab para el disparo
        public GameObject darkLight;
        public float fireRate,bulletForce,cronoRate; // Tasa de disparo en segundos
        public Transform firePoint, thisEnemy;
        public float rotationSpeed = 1.5f;
        private Dictionary<GameObject, Coroutine> deactivateRoutines = new Dictionary<GameObject, Coroutine>();
    #endregion;

    // Duraci�n del cambio de color
    public float duracionCambioColor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar la vida
        vidaEnemy = vidaMax;

        cronoRate = 0;

        // Obtener el color original
        colorOriginal = meshRenderer.material.color;

        #region "Cosas para la IA"
        agent = this.GetComponent<NavMeshAgent>();

        GetPatrolPoints();
        #endregion;
    }

    // Update is called once per frame
    void Update()
    { 
        if(GameManager.instance.currentGameState == GameState.InGame){
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                if(player != null && GameManager.instance.currentGameState == GameState.InGame){
                    ThreadAI();
                }
            }
            catch (System.NullReferenceException e)
            {
                Debug.LogWarning("El objeto Player no se ha encontrado: " + e.Message);
            }

            if (vidaEnemy <= 0)
            {
                // En su lugar ira una animacion de muerte y despues de un tiempo desaparecera el cadaver
                Destroy(this.gameObject);
            }
        }else{
            // Para que deje de moverse si esta en pausa
            agent.ResetPath();
        }
    }

    // M�todo para cambiar el color del objeto
    public void CambiarColor(Color color)
    {
        // Asignar el nuevo color al material del objeto
        meshRenderer.material.color = color;
    }

    private void ThreadAI()
    {
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
                agent.ResetPath();
                
                // Hacer que el enemigo mire al jugador
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                
                cronoRate += Time.deltaTime;
                if(cronoRate >= fireRate){
                    cronoRate = 0;
                    ShootDarkSoul();
                    Debug.Log("¡Disparando al jugador!");
                }
            }
        }
        else
        {
            // Si el enemigo no ve al jugador
            timeSinceLastSawPlayer += Time.deltaTime;
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

        // Si el enemigo está patrullando y llega al punto de patrulla actual, avanzar al siguiente punto
        if (isPatrolling && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void ShootDarkSoul()
    {
        // Obtener la dirección de disparo basada en la dirección hacia el jugador
        Vector3 shootDirection = (player.position - transform.position).normalized;

        // Obtener un proyectil del pool en lugar de instanciar uno nuevo
        GameObject bullet = GameManager.instance.GetEnemyBullet();

        // Configurar la posición y la rotación del proyectil
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(shootDirection);

        // Activar el proyectil
        bullet.SetActive(true);

        // Si la bala ya tiene una corrutina de desactivación, cancelarla
        if (deactivateRoutines.ContainsKey(bullet)) {
            StopCoroutine(deactivateRoutines[bullet]);
            deactivateRoutines.Remove(bullet);
        }

        // Obtener el componente Rigidbody del proyectil
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Resetear la velocidad del proyectil
        bulletRb.velocity = Vector3.zero;
        bulletRb.angularVelocity = Vector3.zero;

        // Aplicar una fuerza hacia adelante al proyectil
        bulletRb.AddForce(shootDirection * bulletForce, ForceMode.Impulse);

        // En lugar de destruir el proyectil, lo desactivamos después de un tiempo
        Coroutine deactivateRoutine = StartCoroutine(DeactivateBullet(bullet, 5f));
        deactivateRoutines.Add(bullet, deactivateRoutine);
    }

    IEnumerator DeactivateBullet(GameObject bullet, float delay) {
        yield return new WaitForSeconds(delay);
        
        // Solo desactivar la bala si todavía está activa
        if (bullet.activeInHierarchy) {
            bullet.SetActive(false);
        }

        // Eliminar la referencia a la corrutina
        if (deactivateRoutines.ContainsKey(bullet)) {
            deactivateRoutines.Remove(bullet);
        }
    }

    void GetPatrolPoints(){
        // Buscar puntos de patrullaje con el tag "Puntos Patrullaje"
        GameObject[] puntosPatrullaje = GameObject.FindGameObjectsWithTag("Patrol Points");
        patrolPoints = new Transform[puntosPatrullaje.Length];
        for (int i = 0; i < puntosPatrullaje.Length; i++)
        {
            patrolPoints[i] = puntosPatrullaje[i].transform;
        }

        // Iniciar patrulla si hay puntos de patrullaje disponibles
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet Light")){
            // Aplicar da�o
            vidaEnemy -= 25;

            // Cambiar color al recibir da�o
            CambiarColor(nuevoColor);
        }
    }
}
