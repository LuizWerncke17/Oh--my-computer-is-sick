using UnityEngine;

public class Link : MonoBehaviour
{
    // Link que será aberto
    public string url = "https://www.youtube.com/watch?v=oHg5SJYRHA0";

    // Função que o botão vai chamar
    public void Abrir()
    {
        Application.OpenURL(url);
    }
}


