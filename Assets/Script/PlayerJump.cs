using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float GroundCheak;
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float JumpAddTime;
    [SerializeField] private float JumpAddPower;
    [SerializeField] private float FallAddPower;
    public Rigidbody2D Rb;//移动通过刚体组件完成 新建一个刚体组件类型的变量rb存储它的引用
    public bool IsGround;
    public AudioSource JumpSound;
    private bool IsJumping;//用于跳跃手感优化,判定是否在进行跳跃,如果为true给Rb一个下坠值.
    private float JumpAddController;
    private bool IsCanAirJump;
    private PlayerAnime PlayerAnimeScript;
    
    void Start()
    {
        Initialization();
    }
    void Update()
    {
        Jump();
        
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGround)
        {
            JumpSound.Play();
            Rb.velocity = new Vector2(Rb.velocity.x, JumpSpeed);//跳跃.
            IsJumping = true;//跳跃手感优化.
            JumpAddController = 0;//长按更高跳跃功能使用
            IsCanAirJump = true;//跳跃后允许空中跳跃.
        }
        IsGround = Physics2D.Raycast(transform.position, Vector2.down, GroundCheak, GroundLayer);//地面检测——利用射线检测,让玩家在离开地面后无法再次跳跃.
        
        if(Input.GetButtonUp("Jump"))
        {
            IsJumping = false;
        }

        if (!IsJumping)//坠落效果
        {
            Rb.velocity -= new Vector2(0, -Physics2D.gravity.y/*获取引擎物理设置内的重力数值*/ * Time.deltaTime * FallAddPower);
        }//用于跳跃手感优化,当玩家没有按下跳跃按键的时候,在Y轴添加一个向下的力.

        if (IsJumping )//长按更高跳跃功能
        {   
            if(JumpAddController < JumpAddTime)
                 Rb.velocity += new Vector2(0, -Physics2D.gravity.y * Time.deltaTime * JumpAddPower);//JumpAddPower控制增强跳跃高度
            else
                 IsJumping = false ;

            JumpAddController += Time.deltaTime;//为JumpAddController增加数值
        }
        //空中跳跃
        if(IsCanAirJump && !IsGround && Input.GetButtonDown("Jump")) //当IsCanAirJump为true,且玩家不在地面,允许玩家进行空中跳跃.
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpSpeed);//二段跳,给Rb组件添加一个Y轴JumpSpeed速度.
            IsJumping = true;
            JumpAddController = 0;
            IsCanAirJump = false;
            PlayerAnimeScript.AirJumpAnimeUpdate();//二段跳动画播放
        }

    }

    private void Initialization()
    {
        Rb = GetComponent<Rigidbody2D>();//利用GetComponent把类的Rigidbody2D初始化
        PlayerAnimeScript = GetComponent<PlayerAnime>();//初始化,获取玩家动画同步脚本的引用
    }
    private void OnDrawGizmos()//Debug用,可以绘制某些数值
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - GroundCheak));
    }

}
