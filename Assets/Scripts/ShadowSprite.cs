using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    //自己的sprite和player的sprite
    private SpriteRenderer thisSprite, playerSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度控制")]
    private float alpha;
    public float alphaSet;//初试值
    public float alphaMulitplier;//差值

    void OnEnable()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSprite = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;
        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;

        activeStart = Time.time;//记录enable开始的时间点
    }

    void Update()
    {
        alpha *= alphaMulitplier;//数值越来越小

        color = new Color(0.5f, 0.5f, 1, alpha);//RGBA 1,1,1,1 white
        // color.a = alpha;//默认是黑色
        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            //返回预制池
            ShadowPrefabPool.instance.ReturnPool(this.gameObject);
        }
    }

}
