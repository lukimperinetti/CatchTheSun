using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;


public class SpaceCraftControler : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 35f; // how fost you move RLUD
    [SerializeField] private float smoothTime = .2f;
    [SerializeField] private float borderReboundSmoothTime = 1f;
    [SerializeField] private float horizontalLimit = 17f;
    [SerializeField] private float verticalLimit = 7f;

    [SerializeField] private Transform bodyTransform;

    [SerializeField] private float speed = 30f;
    [SerializeField] private float currentDistanceTraveled = 0f;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 currentDirection = Vector3.zero;
    private float totalDistanceTraveled = 0;
    public UnityEvent<Vector3> onSetVelocity;
    public UnityEvent<float> onSetDistance;

    private string dataFilePath;

    void Start()
    {
        dataFilePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ApplyRoll();
    }

    private void Move()
    {

        var velocity = Vector3.back * speed * Time.deltaTime;
        TravelDistance(velocity.magnitude);
        onSetVelocity.Invoke(velocity);//invoque l'event

        currentDirection = Vector3.SmoothDamp(currentDirection, GetInputDirection(), ref currentVelocity, smoothTime); //static methode
        transform.Translate(currentDirection * moveSpeed * Time.deltaTime); // apply the direction
    }

    public void TravelDistance(float dist)
    {
        currentDistanceTraveled += dist;
        totalDistanceTraveled += dist;
        onSetDistance.Invoke(currentDistanceTraveled);

        // Check if total distance traveled is a multiple of 1000
        if (totalDistanceTraveled >= 1000)
        {
            // Increase speed by 10
            speed += 10;
            // Reset total distance traveled
            totalDistanceTraveled = 0;
            GameController.instance.LevelUp();
        }
    }

    /// <summary>
    /// Here is the function that handle all my game's input (like the Car gameplay, character etc...)
    /// </summary>
    private Vector3 GetInputDirection()
    {
        Vector3 direction = Vector3.zero;
        bool isAtBorder = false;

        // Handle spacecraft direction
        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;
        if (Input.GetKey(KeyCode.W))
            direction += Vector3.up;
        if (Input.GetKey(KeyCode.S))
            direction += Vector3.down;

        if (transform.position.x > horizontalLimit)
        {
            direction = Vector3.left;
            isAtBorder = true;
        }
        if (transform.position.x < -horizontalLimit)
        {
            direction = Vector3.right;
            isAtBorder = true;
        }
        if (transform.position.y > verticalLimit)
        {
            direction = Vector3.down;
            isAtBorder = true;
        }
        if (transform.position.y < -verticalLimit)
        {
            direction = Vector3.up;
            isAtBorder = true;
        }

        float currentSmoothTime = isAtBorder ? borderReboundSmoothTime : smoothTime;
        currentDirection = Vector3.SmoothDamp(currentDirection, direction, ref currentVelocity, currentSmoothTime);

        return direction;
    }

    /// <summary>
    /// Appliquer un roulis au vaisseau (sur son Z axis)
    /// </summary>
    private void ApplyRoll()
    {
        float rollAmp = -currentDirection.x * 50f;
        float rollVertAmp = -currentDirection.y * 50f;
        Quaternion targetRotation = Quaternion.Euler(rollVertAmp, 0, rollAmp);
        bodyTransform.rotation = Quaternion.Lerp(bodyTransform.rotation, targetRotation, Time.deltaTime * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("asteroid"))
        {
            SaveDataAndLoadGameOver();
        }
    }

    private void SaveDataAndLoadGameOver()
    {
        GameData data = new GameData
        {
            distance = currentDistanceTraveled,
            level = GameController.instance.currentLevel // ou autre source pour le niveau
        };

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(dataFilePath, jsonData);

        SceneManager.LoadScene("GameOver");
    }


}
