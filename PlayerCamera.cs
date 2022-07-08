using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //setup this script on gameobject,that will rotate
    //around player,and make camera its child object
    [SerializeField]
    private float _speedX;
    [SerializeField]
    private float _speedY;

    [SerializeField]
    private Transform _camera;

    private float _rotationX;
    private float _rotationY;

    private Vector3 _camBasePos;

    private void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        //get base position of camera,for return to
        _camBasePos = _camera.localPosition;
    }

    private void Update()
    {
        RotateHandler();
        MoveCamera();
    }

    private void RotateHandler()
    {
        //get inputs and add them for saved rotations
        _rotationX -= Input.GetAxis("Mouse Y") * Time.deltaTime * _speedX;
        _rotationY += Input.GetAxis("Mouse X") * Time.deltaTime * _speedY;

        _rotationX = Mathf.Clamp(_rotationX, -55, 55);
        //apply rotation
        transform.eulerAngles = new Vector3(_rotationX, _rotationY, 0);
    }

    private void MoveCamera()
    {
        //check for colls
        var colls = Physics.OverlapSphere(_camera.position, 0.2f);
        //if your character don't use charactercontroller then change it with script/component that only
        //placed at your player or use tags
        if ((colls.Length == 0 || colls[0].GetComponent<CharacterController>()))
        {
            //go back to base position
            if(Physics.OverlapSphere(_camera.position, 0.4f).Length == 0)
                _camera.localPosition = Vector3.MoveTowards(_camera.localPosition, _camBasePos, Time.deltaTime * 4);
            return;
        }
        //move towards center of parent object
        _camera.localPosition = Vector3.MoveTowards(_camera.localPosition, transform.position, Time.deltaTime * 4);
    }
}
