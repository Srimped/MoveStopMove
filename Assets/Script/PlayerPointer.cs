using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    public GameObject arrow;
    public Vector3 enemyPosition;
    public RectTransform pointerTransform;

    void Start()
    {
        enemyPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        pointerTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 toDestination = enemyPosition;
        Vector3 fromDestination = Camera.main.transform.position;
        fromDestination.z = 0f;
        float anglePointer = Mathf.Atan2(toDestination.x - fromDestination.x, toDestination.z - fromDestination.z);
        float radPointer = anglePointer * Mathf.Rad2Deg;
        if (pointerTransform != null)
        {
            pointerTransform.localEulerAngles = new Vector3(0, 0, -radPointer);
        }
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(enemyPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        
        if (isOffScreen)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
}
