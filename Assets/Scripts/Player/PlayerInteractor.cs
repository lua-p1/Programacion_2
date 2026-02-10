using UnityEngine;
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]private float _interactionDistance = 3f;
    [SerializeField]private LayerMask _interactionLayer;
    [SerializeField]private Transform _rayOrigin;
    private IInteractiveObject _currentObject;
    private void Update()
    {
        CheckForInteractable();
        if (_currentObject != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("detected");
            _currentObject.InteractAction();
        }
    }
    private void CheckForInteractable()
    {
        Ray ray = new Ray(_rayOrigin.position, _rayOrigin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactionLayer))
        {
            Debug.DrawRay(_rayOrigin.position, _rayOrigin.forward * _interactionDistance, Color.green);
            _currentObject = hit.collider.GetComponent<IInteractiveObject>();
        }
        else
        {
             Debug.DrawRay(_rayOrigin.position, _rayOrigin.forward * _interactionDistance, Color.red);
            _currentObject = null;
        }
    }
}
