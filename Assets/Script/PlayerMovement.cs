using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    public FloatingJoystick joystick;
    public float moveHorizontal, moveVertical;
    public Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = joystick.Horizontal * player.movementSpeed;
        moveVertical = joystick.Vertical * player.movementSpeed;

        movement = new Vector3(moveHorizontal, 0, moveVertical);
        movement.Normalize();
        transform.Translate(movement * player.movementSpeed * Time.deltaTime, Space.World);

        if(movement != Vector3.zero)
        {
            Quaternion turn = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, turn, player.rotateSpeed * Time.deltaTime);
        }
    }
}
