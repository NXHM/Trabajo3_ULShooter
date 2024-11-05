using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] PlayerHealth player;

    void Start()
    {
        UpdateSprite();
    }

    int GetHealthStatus()
    {
        float remaining = (float)player.health/(float)player.maxHealth;
        Debug.Log(remaining);

        if (remaining >= .75) return 0;
        if (remaining >= .5) return 1;
        if (remaining >= .25) return 2;
        return 3;
    }

    public void UpdateSprite()
    {
        image.sprite = sprites[GetHealthStatus()];
    }
}