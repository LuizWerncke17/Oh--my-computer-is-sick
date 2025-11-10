using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPrincipal : MonoBehaviour {
    [Header("Transição de Cena")]
    public GameObject Transicao;       // O painel com a imagem e o Animator
    public Animator AnimationTransicao;       // O Animator do painel
    public float duracaoTransicao = 1.5f;    // Mesmo tempo da animação

    // Chamado pelo botão "Jogar"
    public void Jogar() {
        Debug.Log("[MenuPrincipal] Botão Jogar clicado!");
        StartCoroutine(CarregarCenaComAnimacao("SampleScene"));
    }

    private IEnumerator CarregarCenaComAnimacao(string nomeCena) {
        // Garante que o painel está ativo (para o Animator poder tocar)
        if (Transicao != null && !Transicao.activeSelf) {
            Transicao.SetActive(true);
            Debug.Log("[MenuPrincipal] Painel de transição ativado.");
        }

        // Garante que temos um Animator configurado
        if (AnimationTransicao == null) {
            Debug.LogError("[MenuPrincipal] Nenhum Animator atribuído ao painel de transição!");
            yield break;
        }

        // Dispara a animação de FadeOut
        Debug.Log("[MenuPrincipal] Disparando animação FadeOut...");
        AnimationTransicao.ResetTrigger("FadeOut"); // limpa antes de usar, só por segurança
        AnimationTransicao.SetTrigger("FadeOut");

        // Espera o tempo da animação
        yield return new WaitForSeconds(duracaoTransicao);

        // Carrega a cena
        Debug.Log("[MenuPrincipal] Carregando cena: " + nomeCena);
        SceneManager.LoadScene(nomeCena);
    }
}

