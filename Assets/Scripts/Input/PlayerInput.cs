using CodeBase.Core.Character.Enemy;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _spark;
    [SerializeField] private int _inputDamage = 1;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private float _soundDelay;

    private Vector3 _pointOffset;
    private float _elapsedTimeToDamage;
    private float _elapsedTimeToSound;
    private Ray _ray;

    private void Start()
    {
        _pointOffset = new Vector3(0, -2, 0);
    }

    private void Update()
    {
        _elapsedTimeToDamage += Time.deltaTime;
        _elapsedTimeToSound += Time.deltaTime;


        if (Input.GetMouseButton(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(_ray, 400f);

            _spark.SetActive(true);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent(out Button button))
                {
                    break;
                }

                if (hits[i].collider.TryGetComponent(out Enemy enemy))
                {
                    _spark.transform.position = hits[i].point;
                    if (_elapsedTimeToDamage > 0.1f)
                    {
                        enemy.Health.GetDamage(_inputDamage);
                        _elapsedTimeToDamage = 0;
                    }

                    if (_elapsedTimeToSound > _soundDelay)
                    {
                        _shotSound.Play();
                        _elapsedTimeToSound = 0;
                    }
                }

                if (hits[i].collider.TryGetComponent(out Plane plane))
                {
                    _spark.transform.position = hits[i].point;

                    if (_elapsedTimeToSound > _soundDelay)
                    {
                        _shotSound.Play();
                        _elapsedTimeToSound = 0;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _spark.SetActive(false);
        }
    }
}