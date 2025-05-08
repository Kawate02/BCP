using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;            

    void Update()
    {
        Vector3 cameraPos = target.transform.position; 

        if (target.transform.position.x < -500)
        {
            cameraPos.x = 0; 
        }

        if (target.transform.position.x >= -500)
        {
            cameraPos.x = target.transform.position.x + 500;
        }

        cameraPos.y = 0;
        cameraPos.z = -10;
        Camera.main.gameObject.transform.position = cameraPos;

    }
}