using UnityEngine;

public class Nave2TankControl : MonoBehaviour
{
    public float velocidadeMovimento = 10f;  // Velocidade de movimento
    public float velocidadeRotacao = 100f;   // Velocidade de rotação
    public GameObject prefabProjetil;        // Prefab do projétil
    public Transform pontoDeDisparo;         // Ponto de origem do tiro

    private Camera cameraPrincipal;          // Câmera principal da cena

    void Start()
    {
        // Pega a câmera principal para calcular as bordas da tela
        cameraPrincipal = Camera.main;
    }

    void Update()
    {
        // Movimento da Nave 2
        float movimento = 0f;
        if (Input.GetKey(KeyCode.I)) movimento = 1f;
        if (Input.GetKey(KeyCode.K)) movimento = -1f;

        float rotacao = 0f;
        if (Input.GetKey(KeyCode.J)) rotacao = 1f;
        if (Input.GetKey(KeyCode.L)) rotacao = -1f;

        // Move a nave para frente e rotaciona de acordo com as entradas
        transform.Translate(Vector3.up * movimento * velocidadeMovimento * Time.deltaTime);
        transform.Rotate(Vector3.forward, rotacao * velocidadeRotacao * Time.deltaTime);

        // Verifica as bordas e teletransporta a nave
        VerificarBordaTela();

        // Atirar ao pressionar a tecla Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Atirar();
        }
    }

    void Atirar()
    {
        // Instancia o projétil no ponto de disparo
        if (prefabProjetil != null && pontoDeDisparo != null)
        {
            Instantiate(prefabProjetil, pontoDeDisparo.position, transform.rotation);
        }
    }

    void VerificarBordaTela()
    {
        // Pega a posição atual da nave
        Vector3 posicaoNave = transform.position;

        // Obtém os limites da tela
        float larguraTela = cameraPrincipal.orthographicSize * cameraPrincipal.aspect;
        float alturaTela = cameraPrincipal.orthographicSize;

        // Verifica se a nave ultrapassou as bordas da tela e reposiciona ela
        if (posicaoNave.x > larguraTela) // Nave ultrapassou a borda direita
        {
            posicaoNave.x = -larguraTela; // Coloca ela na borda esquerda
        }
        else if (posicaoNave.x < -larguraTela) // Nave ultrapassou a borda esquerda
        {
            posicaoNave.x = larguraTela; // Coloca ela na borda direita
        }

        if (posicaoNave.y > alturaTela) // Nave ultrapassou a borda superior
        {
            posicaoNave.y = -alturaTela; // Coloca ela na borda inferior
        }
        else if (posicaoNave.y < -alturaTela) // Nave ultrapassou a borda inferior
        {
            posicaoNave.y = alturaTela; // Coloca ela na borda superior
        }

        // Atualiza a posição da nave para a nova posição
        transform.position = posicaoNave;
    }
}
