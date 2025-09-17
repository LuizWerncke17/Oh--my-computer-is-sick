using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager main; //Aqui é criado a classe do levelManager, e diz que ele é o principal

    public Transform startPoint;
    public Transform[] path; //Aqui são os campos que podem receber informações dentro do jogo, isso é uma lista e a de cima é algo fixo, nessa lista dentro do jogo tu coloca todos os pontos que vão ser o caminho dos bixos no jogo.
    //Depois disso entendi que o comando "target = LevelManager.main.path[pathIndex];" diz que onde o inimigo tem que ir é o index em sequência dos pontos onde o inimigo tem que seguir que são colocados dentro do jogo, fazendo assim o seu path como alvo.

    public int money;

    private void Awake() {
        main = this;
    }

    private void Start() {
        money = 100;
    }

    public void IncreaseMoney(int amount) {
        money += amount;
    }

    public bool SpendMoney(int amount) {
        if (amount <= money) {
            money -= amount;
            return true;
        } else {
            Debug.Log("Você não tem dinheiro o suficiente para comprar esse item");
            return false;
        }
    }
}
