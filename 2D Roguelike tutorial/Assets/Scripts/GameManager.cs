using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private Text levelText;
    private GameObject levelImage;
    private BourdManager bourdScript;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesAreMoving;
    private bool doingSetUp;

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

        this.enemies = new List<Enemy>();

        this.bourdScript = this.GetComponent<BourdManager>();

        this.InitGame();
    }

    private void InitGame()
    {
        this.doingSetUp = true;

        this.levelImage = GameObject.Find("LevelImage");
        this.levelText = GameObject.Find("LevelText").GetComponent<Text>();
        this.levelText.text = "Day " + this.level; 
        this.levelImage.SetActive(true);
        this.Invoke("HideImage", this.levelStartDelay);

        this.enemies.Clear();

        this.bourdScript.SetupScene(this.level);
    }

    private void HideImage()
    {
        this.levelImage.SetActive(false);

        this.doingSetUp = false;
    }

    void OnLevelWasLoaded(int index)
    {
        this.level++;

        this.InitGame();
    }

    public void GameOver()
    {
        this.levelText.text = "After " + this.level + " days you starved!";

        this.levelImage.SetActive(true);

        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playersTurn || this.enemiesAreMoving || this.doingSetUp)
        {
            return;
        }

        this.StartCoroutine(this.MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        this.enemiesAreMoving = true;
        yield return new WaitForSeconds(this.turnDelay);

        if (this.enemies.Count == 0)
        {
            yield return new WaitForSeconds(this.turnDelay);
        }

        for (int i = 0; i < this.enemies.Count; i++)
        {
            this.enemies[i].MoveEnemy();

            yield return new WaitForSeconds(this.enemies[i].moveTime);
        }

        this.playersTurn = true;
        this.enemiesAreMoving = false;
    }

    public void AddEnemyToList(Enemy script)
    {
        this.enemies.Add(script);
    }
}
