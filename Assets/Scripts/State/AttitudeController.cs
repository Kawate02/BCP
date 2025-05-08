using UnityEngine;

public class AttitudeController
{
    public int DirSet(Transform tf, int facingDir)
    {
        Vector2 lscale = tf.localScale;
            
        lscale.x = facingDir;
        tf.localScale = lscale;

        return facingDir;
    }
}
