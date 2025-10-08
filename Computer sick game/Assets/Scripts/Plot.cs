using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plot : MonoBehaviour {

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if (tower != null) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.money)
        {
            Debug.Log("Voc� n�o tem dinheiro para comprar isso");
            return;
        }

        LevelManager.main.SpendMoney(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

    }
}
