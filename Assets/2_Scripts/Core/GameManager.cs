using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [HideInInspector]
    public int _score = 0;
    [HideInInspector]
    public bool gameOver = false;


    [SerializeField, Range(1, 20)]
    private int asteroidsAmount;
    [SerializeField]
    private float spawnLenght = 15f;
    [SerializeField]
    private float spawnHeight = 10f;
    [SerializeField]
    private float spawnDistToPlayer = 2f;
    [SerializeField]
    private float spawnDelay = 2f;

    private bool isInitiated = false;
    private Transform player;
    private Camera cam; 

    private void Start()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;
        SpawnWithDelay();
    }
    private void Update()
    {
        if (isInitiated)
        CheckForAsteroidsLeft();
    }

    private void CheckForAsteroidsLeft()
    {
        int i = 0;
        foreach (Transform t in ObjectsPooling.Instance.asteroidsParent)
        {
            if (t.gameObject.activeSelf)
            {
                i++;
            }
        }
        if (i == 0)
        {
            SpawnWithDelay();
            isInitiated = false;
        }
    }

    private void SpawnWithDelay() => Invoke(nameof(SpawnAsteroids), spawnDelay);

    private void SpawnAsteroids()
    {
        int i = 0;
        while(i < asteroidsAmount)
        {
            Vector3 position = GetRandomPosition();

            if (CheckFreePosition(position))
            {
                var asteroid = ObjectsPooling.Instance.BigAsteroidPool.Get();
                asteroid.transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                asteroid.transform.parent = ObjectsPooling.Instance.asteroidsParent;
                i++;
            }
        }
        isInitiated = true;
    }

    ///Generate a random vector3
    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-spawnLenght, spawnLenght), Random.Range(-spawnHeight, spawnHeight), 0f);
    }

    //Checks if the generated vector3 is inside the viewport and at certain distance from the player
    private bool CheckFreePosition(Vector3 pos)
    {
        Vector3 viewportPosition = cam.WorldToViewportPoint(pos);
        if(viewportPosition.x < 1 && viewportPosition.x > 0 && viewportPosition.y < 1 && viewportPosition.y > 0)
        {
            if(Vector3.Distance(pos, player.position) > spawnDistToPlayer)
            {
                return true;
            }
        }
        return false;
    }

    public void GameOver()
    {
        gameOver = true;
        player.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}