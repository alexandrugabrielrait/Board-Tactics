using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {

    public float scrollZone = 30;
    public float scrollSpeed = 5;
    public float xMax = 4;
    public float xMin = 0;
    public float yMax = 4;
    public float yMin = 0;

    public KeyCode arrowUp;
    public KeyCode arrowDown;
    public KeyCode arrowRight;
    public KeyCode arrowLeft;

    private Vector3 desiredPosition;

    private void Start()
    {
        desiredPosition = transform.position;
    }

    // Update is called once per frame
    private void Update () {
        float x = 0, y = 0, z = 0;
        float speed = scrollSpeed * Time.deltaTime;
        if (Input.mousePosition.x < scrollZone||Input.GetKey(arrowLeft))
            x -= speed;
        if (Input.mousePosition.x > Screen.width - scrollZone || Input.GetKey(arrowRight))
            x += speed;
        if (Input.mousePosition.y < scrollZone || Input.GetKey(arrowDown))
            y -= speed;
        if (Input.mousePosition.y > Screen.height - scrollZone || Input.GetKey(arrowUp))
            y+= speed;

        Vector3 move = new Vector3(x, y, z) + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.y = Mathf.Clamp(move.y, yMin, yMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);
    }
}
