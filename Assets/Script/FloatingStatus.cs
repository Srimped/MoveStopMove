using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;

public class FloatingStatus : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI nameTag, score;
    public Transform playerBody;
    public Camera mainCam;
    public Vector3 offSet;

    void Start()
    {
        mainCam = Camera.main;
        player = playerBody.gameObject.GetComponent<Player>();
    }

    void Update()
    {
        if (player == null || playerBody == null) return;

        Vector3 newOffSet = new Vector3(offSet.x, offSet.y + player.statusPosition, offSet.z);
        transform.rotation = mainCam.transform.rotation;
        transform.position = playerBody.position + newOffSet;
    }

    public void GetPlayerNameUI(string playerName)
    {
        nameTag.text = playerName;
    }

    public void GetPlayerScoreUI(string playerScore)
    {
        score.text = playerScore;
    }
}
