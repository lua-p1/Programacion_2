using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
            _currentObject = hit.collider.GetComponent<IInteractiveObject>();
        }
        else
        {
            _currentObject = null;
        }
    }
}
