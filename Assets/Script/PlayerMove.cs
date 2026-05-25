using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour //如果希望修改挂载到UnityEngine里面的脚本组件对象名,需要在这里修改Class类名
{   //[SerializeFied]：序列化字段,可以把字段在Unity中显示出来.一般用于private,用于增强安全性
    [SerializeField] private float MoveSpeed;//使用一个Vector2来存储XY轴上的移动速度
    private Rigidbody2D Rb;//移动通过刚体组件完成 新建一个刚体组件类型的变量rb存储它的引用
    private SpriteRenderer Sprite;
    public float MoveController;
    public AudioSource RunSound;
    private PlayerJump PlayerJump;
    void Start()
    {
        Initialization();
    }
    void Update()
    {
        XMove();  
    }
    private void Initialization()
    {
        Rb = GetComponent<Rigidbody2D>();//利用GetComponent把类的Rigidbody2D初始化
        Sprite = GetComponent<SpriteRenderer>();//初始化精灵渲染器
        PlayerJump = GetComponent<PlayerJump>();
    }
    private void flipUpdate()
    {
        if (Rb.velocity.x > 0)//同步玩家前进方向
        {
            //transform.localScale = new Vector2(1, 1); //根据玩家前进的x速度,设置玩家变换的本地缩放x数值,以实现翻转。
            Sprite.flipX = false;//利用精灵渲染器的flip进行翻转
        }
        if (Rb.velocity.x < 0)
        {
            //transform.localScale = new Vector2(-1, 1);
            Sprite.flipX = true;
        }
    }

    private void XMove()
    {
        if (MoveController != 0 && PlayerJump.IsGround)
        {
            if (!RunSound.isPlaying)
                RunSound.Play();
        }
        else
        {
            RunSound.Stop();
        }
        MoveController = Input.GetAxisRaw("Horizontal");//获取水平移动输入
        Rb.velocity = new Vector2(MoveSpeed * MoveController, Rb.velocity.y);//MoveSpeed*MoveController 当没有输入后,MoveController立即为0,相乘后的结果也为0.可以实现立即停止.
        flipUpdate();
    }

}
