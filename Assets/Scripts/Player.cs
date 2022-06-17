using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueTalk _dialogueTalk;
    [SerializeField] private StarterAssetsInputs _inputs;
    [SerializeField] private int _maxDistanceRay;
    [SerializeField] private bool _isInteract;

    private IInteraction _interaction;
    private int _countPush = 0;
   

    public bool IsInteract => _isInteract;
    public StarterAssetsInputs Input => _inputs;


    private void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }


    private void Update()
    {
        Interaction();
    }


    private void LateUpdate()
    {
        
    }


    private void OpenQuestLogPanel()
    {

    }


    private void Interaction()
    {
        if (_inputs.interaction && _countPush == 0)
        {
            _countPush = 1;
            _isInteract = true;
            _inputs.interaction = false;
        }

        else if (_inputs.interaction && _countPush == 1)
        {
            _inputs.interaction = false;
            _isInteract = false;
            _countPush = 0;
        }

        else
        {
            _isInteract = false;
        }

    }
   

   /* private void FireRay()
    {
        ray = new Ray(Camera.main.transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, _maxDistanceRay))
        {
            DefineObject();
        }
        else
        {
            if (_interaction != null)
            {
                _interaction.UnInteraction();
                
                _interaction = null;
            }
        }

    }


    private void DrawRay()
    {
        if (hit.collider != null)
        {
            Debug.DrawRay(Camera.main.transform.position, transform.forward * _maxDistanceRay, Color.blue);
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, transform.forward * _maxDistanceRay, Color.red);
        }
    }


    private void DefineObject()
    {
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Character>() != null)
            {
                Character selectableInterection = hit.collider.GetComponent<Character>();

                if(_inputs.interaction)
                {
                    selectableInterection.GetComponentInChildren<DialogueTalk>().StartDialogue();
                    _inputs.interaction = false;
                }
                else
                {
                    Debug.Log("Dot't push button");
                }
            }
        }
    }*/

}
