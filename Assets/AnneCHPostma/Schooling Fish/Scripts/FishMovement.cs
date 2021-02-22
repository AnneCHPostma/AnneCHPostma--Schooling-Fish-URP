using UnityEngine;

namespace AnneCHPostma.SchoolingFish
{
    [RequireComponent(typeof(GameObject))]
    [RequireComponent(typeof(BoxCollider))]
    public class FishMovement : MonoBehaviour
    {
        [Tooltip("Add a GameObject providing the boundaries of the swimming area")]
        [SerializeField]
        private GameObject _swimArea = null;

        [Tooltip("The swim speed for the fish (0 = random speed)")]
        [SerializeField]
        [Range(0.0f, 5.0f)]
        private float _swimSpeed = 0.0f;

        private readonly float minSwimSpeed = 0.1f;
        private readonly float maxSwimSpeed = 5.0f;

        private bool turning = false;

        private BoxCollider boxCollider = null;
        private BoxCollider areaCollider = null;

        private void Awake()
        {
            if (_swimArea == null)
            {
                Debug.LogWarning("No _swimArea GameObject found on the FishMovement script. Terminating the Play mode.");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        private void Start()
        {
            boxCollider = gameObject.GetComponent<BoxCollider>();
            areaCollider = _swimArea.GetComponent<BoxCollider>();

            if (_swimSpeed == 0.0f) _swimSpeed = Random.Range(minSwimSpeed, maxSwimSpeed);
        }

        private void Update()
        {
            turning = !(boxCollider.bounds.Intersects(areaCollider.bounds));

            if (turning)
            {
                var direction = Vector3.zero - transform.position;
                var rotationSpeed = Random.Range(0.1f, 45.0f);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }

            transform.Translate(0.0f, 0.0f, Time.deltaTime * _swimSpeed);
        }
    }
}
