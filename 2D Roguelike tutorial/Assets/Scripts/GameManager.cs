using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BourdManager bourdScript;
    public int playerFoodPoints = 100;

    [HideInInspector] public bool playersTurn = true;


    private int level = 4;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        this.bourdScript = this.GetComponent<BourdManager>();
        this.InitGame();
    }

    private void InitGame()
    {
        this.bourdScript.SetupScene(this.level);
    }

    public void GameOver()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
