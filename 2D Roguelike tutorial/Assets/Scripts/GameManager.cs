using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private BourdManager bourdScript;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesAreMoving;

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
        this.enemies.Clear();

        this.bourdScript.SetupScene(this.level);
    }

    void OnLevelWasLoaded(int index)
    {
        this.level++;

        this.InitGame();
    }

    public void GameOver()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playersTurn || this.enemiesAreMoving)
        {
            return;
        }

        StartCoroutine(this.MoveEnemies());
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
