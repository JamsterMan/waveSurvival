using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 arenaPos;
    public Vector3 roomPos;
    public Transform cameraPostion;
    public Transform playerPostion;
    public Transform playerArenaAdjust;
    public Transform playerRoomAdjust;
    public bool isBossRoom = false;
    public float cameraMoveSpeed = 5f;

    private bool _isInRoom = false;
    private Vector3 _targetPos;
    private bool _moveCam = false;

    private void Start()
    {
        _targetPos = arenaPos;
    }

    /*
     * moves the camera between the main arena and the room (boss room or shop room)
     */
    private void MoveCameraPosition()
    {
        if (_isInRoom)
        {
            _targetPos = arenaPos;
            playerPostion.position = playerArenaAdjust.position;
            _moveCam = true;
        }
        else
        {
            _targetPos = roomPos;
            playerPostion.position = playerRoomAdjust.position;
            _moveCam = true;
        }
        _isInRoom = !_isInRoom;
    }

    private void Update()
    {
        if (_moveCam)
        {
            cameraPostion.position = Vector3.Lerp(cameraPostion.position, _targetPos, cameraMoveSpeed * Time.deltaTime);
            if(cameraPostion.position == _targetPos)
            {
                _moveCam = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveCameraPosition();
        }
    }
}
