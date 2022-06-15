using UnityEngine;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
public class Superliminal : MonoBehaviour
{

	//public InputAction pick;
	private InputAction pick = new InputAction("pick", binding: "<Mouse>/rightButton");

	[Header("Components")]
	public Transform target;            // The target object we picked up for scaling

	[Header("Parameters")]
	public LayerMask targetMask;        // The layer mask used to hit only potential targets with a raycast
	public LayerMask ignoreTargetMask;  // The layer mask used to ignore the player and target objects while raycasting
	public float offsetFactor;          // The offset amount for positioning the object so it doesn't clip into walls
	public bool scaleHardStop = false;
	public float _maxScale = 5.0f;
	public float _minScale = 0.15f;
	public bool lockRotation = false;

	float originalDistance;             // The original distance between the player camera and the target
	Vector3 originalScale;                // The original scale of the target objects prior to being resized
	Vector3 targetScale;                // The scale we want our object to be set to each frame


	private CharacterController _controller;
	private StarterAssetsInputs _input;
	private GameObject _mainCamera;


	private void Awake()
	{
		// get a reference to our main camera
		if (_mainCamera == null)
		{
			_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}

		//pick.performed += OnPick;
	}

	void Start()
	{
		pick.Enable();
		_controller = GetComponent<CharacterController>();
		_input = GetComponent<StarterAssetsInputs>();
	}

	void Update()
	{
		HandleInput();
		ResizeTarget();
	}

	// void OnPick(InputValue value)
	// {
	// 	if(value.isPressed){Debug.Log("if(value.isPressed)");}
	// 	Debug.Log("Acionado OnPick");
	// }

	void HandleInput()
	{
		
		// Se trigger de acao (Pick), botao direito do mouse e, acredito,  "E"
		if (pick.triggered)
		{
			Debug.Log("pick.triggered");
			// If we do not currently have a target
			if (target == null)
			{
				// Debug.Log("if (target == null)");
				RaycastHit hit;
				// Raycast e entao checa se bateu primeiro num gameObject que não os targets, entrando caso s
				
				if( Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, Mathf.Infinity) &&
					((1<<hit.transform.gameObject.layer) & ignoreTargetMask) == 0)
				{
					Debug.Log("Target object found!");
					target = hit.transform;
					// Desliga fisica do obejto
					target.GetComponent<Rigidbody>().isKinematic = true;
					target.GetComponent<Collider>().isTrigger = true;

					// Distancia entre camera principal e objeto
					originalDistance = Vector3.Distance(_mainCamera.transform.position, target.position);

					originalScale = target.localScale;

					targetScale = target.localScale;
				}
				else
				{
					// Debug.Log("hit nothing");
					// Debug.Log("hit layer:"+hit.transform.gameObject.layer);
				}
			}
			// If we DO have a target
			else
			{
				// Reactivate physics for the target object
				target.GetComponent<Rigidbody>().isKinematic = false;
				target.GetComponent<Collider>().isTrigger = false;
				// Set our target variable to null
				target = null;
			}
		}
	}

	

	void ResizeTarget()
	{
		// If our target is null
		if (target == null)
		{
			// Return from this method, nothing to do here
			return;
		}

		// Cast a ray forward from the camera position, ignore the layer that is used to acquire targets
		// so we don't hit the attached target with our ray
		RaycastHit hit;
		if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward,
				 out hit, Mathf.Infinity, ignoreTargetMask))
		{
			// Set the new position of the target by getting the hit point and moving it back a bit
			// depending on the scale and offset factor
			target.position = hit.point - _mainCamera.transform.forward * offsetFactor * targetScale.x;

			// Calculate the current distance between the camera and the target object
			float currentDistance = Vector3.Distance(_mainCamera.transform.position, target.position);

			// Calculate the ratio between the current distance and the original distance
			float s = currentDistance / originalDistance;

			// Set the scale Vector3 variable to be the ratio of the distances
			targetScale.x = targetScale.y = targetScale.z = s;

			//Escala final
			Vector3 finalScale = Vector3.Scale(targetScale, originalScale);
			if(scaleHardStop){
				if(finalScale.x > _maxScale){
					target.localScale = new Vector3(_maxScale, _maxScale, _maxScale); }
				else if(finalScale.x < _minScale) {
					target.localScale = new Vector3(_minScale, _minScale, _minScale); }
				else {target.localScale = finalScale; }
			}
			else{//Parte comentada forca o objeto a voltar pro tamanho original
				// if(finalScale.x > _maxScale){target.localScale = originalScale; }
				// else if(finalScale.x < _minScale) {target.localScale = originalScale; }
				// else {target.localScale = finalScale; }
				target.localScale = finalScale; //Livre para crescer
			}
			

			//Corrigindo a rotacao
			if(lockRotation) target.rotation = _mainCamera.transform.rotation;

		}
	}
}

}