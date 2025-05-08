public class CatParamater : ParamaterBase
{
    public CatParamater()
    {
        hp = 10;
        moveSpeed = 400f;
        jumpHight = 2000f;
        jumpTime = 0.5f;
        fallSpeed = 1500f;
        atkReach = 220f;
        atkDmg = 1;
        facingDir = 1;
    }
}

public class WPParamater : ParamaterBase
{
    public WPParamater()
    {
        hp = 2;
        moveSpeed = 400f;
        jumpHight = 0f;
        jumpTime = 0f;
        fallSpeed = 0f;
        atkReach = 220f;
        atkDmg = 2;
        facingDir = 1;
    }
}
