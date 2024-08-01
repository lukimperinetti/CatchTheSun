using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern
    /// </summary>
    public static GameController instance { get; private set; }
    public float distance { get; private set; } = 0f;
    public int currentLevel { get; private set; } = 1;


    /// <summary>
    /// this = va pointer le script (le composant)
    /// this.gameGameObject = va pointer le gameobject
    /// awake = s'active a l'aparition de l'objet dans la scene
    /// start = activation / d�sactivation de l'objet
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this) // si instance existe
        {
            Destroy(this.gameObject);//je d�truis (1 seule instance)
            return;
        }
        instance = this;//sinon je la cr�er
        DontDestroyOnLoad(gameObject);
    }

    public void setDistance(float value) => distance = value;
    public void LevelUp() => currentLevel++;

    public void Reset()
    {
        distance = 0;
        currentLevel = 1;
    }


}
