using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance; // Singleton para acesso global

    public GameObject cleber; // Prefab da Nave 1
    public GameObject triangle; // Prefab da Nave 2

    public Camera cameraPrincipal; // Câmera principal da cena

    void Awake()
    {
        // Configura o Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Se não houver uma câmera atribuída, tenta pegar a câmera principal
        if (cameraPrincipal == null)
        {
            cameraPrincipal = Camera.main;
        }
    }

    public void RespawnNave(string tag)
    {
        // Determina qual prefab usar com base na tag
        GameObject prefab = null;

        if (tag == "Nave")
        {
            prefab = cleber; // Nave Cleber
        }
        else if (tag == "Nave2")
        {
            prefab = triangle; // Nave Triangle
        }

        if (prefab != null)
        {
            Vector3 posicaoAleatoria = GerarPosicaoAleatoria();
            float rotaçãoAleatoria = Random.Range(0f, 360f); // Gera um valor aleatório de rotação
            StartCoroutine(RespawnCoroutine(prefab, posicaoAleatoria, rotaçãoAleatoria));
        }
    }

    private Vector3 GerarPosicaoAleatoria()
    {
        // Obtém as bordas da tela (mundo 2D)
        float larguraTela = cameraPrincipal.orthographicSize * cameraPrincipal.aspect;
        float alturaTela = cameraPrincipal.orthographicSize;

        // Gera uma posição aleatória dentro da tela
        float xAleatorio = Random.Range(-larguraTela, larguraTela);
        float yAleatorio = Random.Range(-alturaTela, alturaTela);

        return new Vector3(xAleatorio, yAleatorio, 0f); // Retorna a posição no plano X, Y
    }

    private System.Collections.IEnumerator RespawnCoroutine(GameObject prefab, Vector3 posicaoInicial, float rotaçãoAleatoria)
    {
        // Espera 2 segundos antes de criar a nave
        yield return new WaitForSeconds(2f);

        // Instancia a nova nave na posição inicial aleatória com a rotação aleatória
        Instantiate(prefab, posicaoInicial, Quaternion.Euler(0, 0, rotaçãoAleatoria));
    }
}
