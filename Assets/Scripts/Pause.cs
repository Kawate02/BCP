using UnityEngine;

public class Paint
{
    public bool paintmode = false;

    public void PaintMode()
    {
        if(!paintmode)
        {
            paintmode = true;
            Time.timeScale = 0;
        }
            else if (paintmode)
        {
            paintmode = false;
            Time.timeScale = 1f;
        }
    }
}
