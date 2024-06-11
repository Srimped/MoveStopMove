using UnityEngine;

public class FolowPlayerCam : MonoBehaviour
{
    public Player player;
    public Transform playerbody;
    public Vector3 offSet;

    void Start()
    {
        offSet = new Vector3(0, 35, -36);

        player = playerbody.gameObject.GetComponent<Player>();
        if (player != null)
            playerbody = player.transform;
        else return;
    }

    void Update()
    {
        if (player == null || playerbody == null) return;

        Vector3 newOffSet = new Vector3(offSet.x, offSet.y + player.cameraChange, offSet.z - player.cameraChange);
        transform.position = playerbody.position + newOffSet;
    }
}
