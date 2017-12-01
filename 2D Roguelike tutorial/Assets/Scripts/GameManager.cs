using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BourdManager bourdScript;

    private int level = 4;

    // Use this for initialization
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

    // Update is called once per frame
    void Update()
    {

    }
}
