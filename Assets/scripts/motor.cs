using UnityEngine;

public class Motor : MonoBehaviour
{
    // Velocidade de movimentação da nave
    public float velocidade = 10f;

    // Prefab do projétil
    public GameObject projetilPrefab;

    // Ponto de spawn do projétil
    public Transform pontoDisparo;

    // Velocidade do projétil
    public float velocidadeProjetil = 20f;

    // Duração do projétil em tela
    public float duracaoProjetil = 0.5f;

    // Update é chamado uma vez por frame
    void Update()
    {
        // Movimentação
        Vector3 movimento = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow)) movimento += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow)) movimento += Vector3.down;
        if (Input.GetKey(KeyCode.LeftArrow)) movimento += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) movimento += Vector3.right;
        transform.Translate(movimento * velocidade * Time.deltaTime);

        // Disparo de projétil
        if (Input.GetKeyDown(KeyCode.Space)) // Pressiona barra de espaço
        {
            Atirar();
        }
    }

    void Atirar()
    {
        // Instancia o projétil no ponto de disparo
        GameObject projetil = Instantiate(projetilPrefab, pontoDisparo.position, pontoDisparo.rotation);

        // Adiciona movimento ao projétil
        Rigidbody2D rb2D = projetil.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.linearVelocity = pontoDisparo.up * velocidadeProjetil;
        }

        // Para jogos 3D, use Rigidbody ao invés de Rigidbody2D
        Rigidbody rb = projetil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = pontoDisparo.forward * velocidadeProjetil;
        }

        // Destroi o projétil após 'duracaoProjetil' segundos
        Destroy(projetil, duracaoProjetil);
    }
}
