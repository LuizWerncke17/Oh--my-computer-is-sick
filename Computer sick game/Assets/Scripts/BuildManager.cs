using UnityEngine;

public class BuildManager : MonoBehaviour {
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] towerPrefabs;

    private int selectdTower = 0;

    private void Awake() {
        main = this;
    }

    public GameObject GetSelectdTower() {
        return towerPrefabs[selectdTower];
    }
}
