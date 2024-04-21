using TMPro;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private float _interactDistance = 1f; 
    [SerializeField] private LayerMask _interactableLayer; 
    [SerializeField] private GameObject _interactionTextPrefab; 
    [SerializeField] private GameObject _interactionImage; 
    private Camera _mainCamera;

    private GameObject _currentInteractableObject; 
    private GameObject _interactionTextInstance; 
    private GameObject _player;

    private void Start()
    {
        _mainCamera = Camera.main;
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactDistance, _interactableLayer))
        {
            GameObject interactableObject = hit.collider.gameObject;
            if (_interactionTextInstance != null)
            {
                _interactionTextInstance.transform.rotation = _player.transform.rotation;
            }

            if (interactableObject != _currentInteractableObject)
            {
                _currentInteractableObject = interactableObject;
                //ShowInteractionText(interactableObject);
                _interactionImage.SetActive(true);
            }

            if (Input.GetKey(KeyCode.E))
            {
                // Действие
            }
        }
        else
        {
            //HideInteractionText();
            _interactionImage.SetActive(false);
            _currentInteractableObject = null;
        }
    }
    
    private void ShowInteractionText(GameObject interactableObject)
    {
        if (_interactionTextInstance == null)
        {
            _interactionTextInstance = Instantiate(_interactionTextPrefab, transform);
        }

        _interactionTextInstance.transform.position = interactableObject.transform.position + Vector3.down * 0.3f + Vector3.forward * 0.1f;
        
        string interactionKey = "E"; 
        _interactionTextInstance.GetComponent<TMP_Text>().text = interactionKey;
    }

    private void HideInteractionText()
    {
        if (_interactionTextInstance != null)
        {
            Destroy(_interactionTextInstance);
        }
    }
}
