using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    private enum Anim { Idle, run, jump, fall };//利用枚举控制动画状态机,这里是定义Anime的枚举数据类型,类似于在UE的内容下创建枚举资产
    private Anim E_State;//创建类型为Anim枚举的变量 E_State
    private Animator Anime;//引入动画状态机
    private PlayerMove PlayerMove;//获取PlayerMove脚本,这里是定义变量容器
    private PlayerJump PlayerJump;//获取PlayerJump脚本,这里是定义变量容器
    void Start()
    {
        Anime = GetComponent<Animator>();//利用GetComponent< >把当前挂载对象的对应脚本存入变量
        PlayerMove = GetComponent<PlayerMove>();
        PlayerJump = GetComponent<PlayerJump>();
    } 
    void Update()
    {
        if (PlayerMove.MoveController != 0)//Run与Idle状态切换
        {
            E_State = Anim.run;
        }
        else
        {
            E_State = Anim.Idle;
        }

        if(PlayerJump.Rb.velocity.y > 0.3f)//跳跃状态切换,通过检测Y轴速度完成
        {
            E_State = Anim.jump;
        }
        if(PlayerJump.Rb.velocity.y < -0.3f)
        {  
            E_State = Anim.fall; 
        }
        Anime.SetInteger("States", (int)E_State);//将脚本变量通过SetInteger传给动画状态机.由于动画状态机类的变量类型为int,需要在枚举前进行一次强制类型转换.
    }
}
