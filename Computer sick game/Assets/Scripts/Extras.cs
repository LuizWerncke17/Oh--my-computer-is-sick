using UnityEngine;
using UnityEngine.SceneManagement;

public class Extras : MonoBehaviour {
    public void Voltar() {
        SceneManager.LoadScene("MainMenu");
    }
}