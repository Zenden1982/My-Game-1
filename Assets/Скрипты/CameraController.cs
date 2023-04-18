using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float followSpeed = 10f;
    public float zoomSpeed = 2f;
    public Vector3 cameraOffset = new Vector3(0, 20, -10);
    public float jumpingOffset = 4f;
    public static float score = 0f;
    public GameObject win;

    public void Start()
    {
        Cursor.visible = false;
        win = GameObject.Find("Win");
        win.SetActive(false);
        
    }
    private void Update()
    {
        if (YouWin.winBool)
        {
            win.SetActive (true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void LateUpdate()
    {
        
        Vector3 targetPos = target.position + cameraOffset;
        if (Передвижение.isJumping)
        {
            Vector3 lerpPos = Vector3.Lerp(transform.position, targetPos, followSpeed/2 * Time.deltaTime);
            lerpPos.y = transform.position.y;
            transform.position =lerpPos;
        }
        else transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed/2 * Time.deltaTime);
        
    }
}
