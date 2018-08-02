using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public float panSpeed = 30f;
    public float panBorderThickness = 20f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    // Update is called once per frame
    void Update () {

        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) && gameObject.transform.position.z < 65)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) && gameObject.transform.position.z > - 10)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) && gameObject.transform.position.x < 25)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) && gameObject.transform.position.x > -70)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        Vector3 position = transform.position;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        position.y -= scroll * 500 * scrollSpeed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;
    }
}
