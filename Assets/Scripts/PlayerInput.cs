using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private static Cat cat;
    private static MaskGenerator paint;
    private static CameraManager cam;
    private static Paint paintMode = new Paint();

    [SerializeField] private Canvas pause;

    private float attackCoolTime = 0.5f;
    private float preAttackTime = 0;

    void Start()
    {
        pause.gameObject.SetActive(false);
        cam = Camera.main.GetComponent<CameraManager>();
        SpawnCat();
        cat.OnStart();
        paint = FindObjectOfType<MaskGenerator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            paintMode.PaintMode();
            if (paintMode.paintmode)
            {
                paint.OnStart();
            }
        }
        if (paintMode.paintmode)
        {
            paint.OnUpdate();
        }
        if (!paintMode.paintmode && !pause.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.A)) Cat.cat.A = true;
            if (Input.GetKeyUp(KeyCode.A)) Cat.cat.A = false;

            if (Input.GetKeyDown(KeyCode.D)) Cat.cat.D = true;
            if (Input.GetKeyUp(KeyCode.D)) Cat.cat.D = false;

            if (Input.GetKeyDown(KeyCode.Space)) Cat.cat.Space = true;
            if (Input.GetKeyUp(KeyCode.Space)) Cat.cat.Space = false;

            if (Cat.cat.D) Cat.par.facingDir = Cat.attitude.DirSet(Cat.cat.tf, 1);
            if (Cat.cat.A) Cat.par.facingDir = Cat.attitude.DirSet(Cat.cat.tf, -1);
            Cat.cat.OnUpdate();

            if (Input.GetKey(KeyCode.LeftShift) && (Time.time - preAttackTime >= attackCoolTime || Time.time < attackCoolTime))
            {
                preAttackTime = Time.time;
                cat.Attack();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void SpawnCat()
    {
        float x = cam.gameObject.transform.position.x - 500;
        float y = cam.gameObject.transform.position.y;

        cat = Instantiate(Resources.Load("Animals/Cat"), new Vector3(x, y, 0), Quaternion.identity).GetComponent<Cat>();
        cam.target = cat.gameObject;
    }
}
