using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Condition
{
    public bool IsCollidingBelow { get; set; }
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingRight { get; set; }
    public bool IsCollidingLeft { get; set; }
    public bool IsFalling { get; set; }
    public bool IsWallClinging { get; set; }
    public bool IsJetpacking { get; set; }
    public bool IsJumping { get; set; }
    public bool IsDashing { get; set; }
    public bool WallPrevious { get; set; }
    public bool WallNow { get; set; }
    public bool IceLeft { get; set; }
    public bool IceRight { get; set; }
    public bool StopIce { get; set; }
    public bool TimeStop { get; set; }
    public bool Stand { get; set; }
    public bool isWater{ get; set; }


    public void Reset()
    {
        IsCollidingBelow = false;
        IsCollidingLeft = false;
        IsCollidingRight = false;
        IsCollidingAbove = false;
        TimeStop = false;

        IsFalling = false;
    }
}
