using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;

    public Transform[] path

    private void Awake()
    {
        main = this;
    }
}
