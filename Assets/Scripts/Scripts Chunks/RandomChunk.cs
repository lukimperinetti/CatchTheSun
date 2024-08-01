using UnityEngine;

public class RandomChunk : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs; // Liste des prefabs d'ast�ro�des
    public int asteroidCount = 10; // Nombre d'ast�ro�des � g�n�rer
    public Vector3 chunkSize = new Vector3(50, 50, 22); // Taille du chunk

    public GameObject[] AsteroidPrefabs { get => asteroidPrefabs; set => asteroidPrefabs = value; }

    void Start()
    {
        GenerateRandomAsteroids();
    }

    void GenerateRandomAsteroids()
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            // S�lectionner un prefab d'ast�ro�de al�atoire dans la liste
            GameObject asteroidPrefab = AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)];

            // G�n�rer une position al�atoire � l'int�rieur du chunk
            Vector3 randomPosition = new Vector3(
                Random.Range(-chunkSize.x / 2, chunkSize.x / 2),
                Random.Range(-chunkSize.y / 2, chunkSize.y / 2),
                Random.Range(-chunkSize.z / 2, chunkSize.z / 2)
            );

            // Instancier l'ast�ro�de � la position al�atoire
            Instantiate(asteroidPrefab, transform.position + randomPosition, Quaternion.identity, transform);
        }
    }
}
