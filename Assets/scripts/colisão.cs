using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocidade = 10f;  // Velocidade do projétil
    public float tempoDeVida = 10f; // Tempo antes de destruir o projétil

    void Start()
    {
        // Destroi o projétil após um tempo para evitar acumulação
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        // Move o projétil para frente (eixo Y local)
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        // Verifica se o objeto colidido tem a tag "Nave" ou "Nave2"
        if (outro.CompareTag("Nave") || outro.CompareTag("Nave2"))
        {
            Debug.Log($"Projétil colidiu com {outro.gameObject.name}");

            // Reagendar o respawn da nave
            RespawnManager.Instance.RespawnNave(outro.tag);

            // Destroi a nave atingida
            Destroy(outro.gameObject);

            // Destroi o projétil
            Destroy(gameObject);
        }
    }
}
