using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Define.CameraMode _cameraMode = Define.CameraMode.QuarterView;

    [SerializeField] private Vector3 _delta = new(0.0f, 0.4f, -0.4f);

    [SerializeField] private GameObject _player = null;

    readonly int _mask = 1 << (int)Define.Layer.Block;

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    void Start()
    {

    }

    void LateUpdate()
    {
        if (!_player.IsValid())
            return;

        if (_cameraMode == Define.CameraMode.QuarterView)
        {
            RaycastHit[] hits = Physics.RaycastAll(_player.transform.position, _delta, _delta.magnitude, _mask);
            if (hits.Length != 0)
            {
                RaycastHit hit = hits[hits.Length - 1];
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            // RaycastHit hit;
            // if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, _mask))
            // {
            //     float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
            //     transform.position = _player.transform.position + _delta.normalized * dist;
            // }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
    }

    void SetQuarterView(Vector3 delta)
    {
        _cameraMode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
