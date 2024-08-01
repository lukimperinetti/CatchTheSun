using UnityEngine;

public class RandomChunk : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs; // Liste des prefabs d'astéroïdes
    public int asteroidCount = 10; // Nombre d'astéroïdes à générer
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
            // Sélectionner un prefab d'astéroïde aléatoire dans la liste
            GameObject asteroidPrefab = AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)];

            // Générer une position aléatoire à l'intérieur du chunk
            Vector3 randomPosition = new Vector3(
                Random.Range(-chunkSize.x / 2, chunkSize.x / 2),
                Random.Range(-chunkSize.y / 2, chunkSize.y / 2),
                Random.Range(-chunkSize.z / 2, chunkSize.z / 2)
            );

            // Instancier l'astéroïde à la position aléatoire
            Instantiate(asteroidPrefab, transform.position + randomPosition, Quaternion.identity, transform);
        }
    }
}
