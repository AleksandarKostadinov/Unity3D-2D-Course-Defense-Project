using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    public Sprite dmgSprite;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        this.spriteRenderer.sprite = this.dmgSprite;

        this.hp -= loss;
        if (this.hp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
