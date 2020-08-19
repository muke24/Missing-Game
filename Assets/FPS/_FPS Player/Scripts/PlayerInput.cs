using PeterThompson;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerInput : MonoBehaviour
{
	private Inputs inputs;
	private Vector2 i;
	private Vector2 iRaw;
	private Vector2 previous;
	private Vector2 _down;
	private int jumping = 0;
	private int jumpTimer;
	private bool jump = false;
	private bool sprinting = false;
	private bool _crouch = false;
	private bool _crouching = false;
	private bool interacting = false;

	public Vector2 input
	{
		get
		{
			inputs.Player.WASD.performed += ctx =>
			{
				Vector2 value = ctx.ReadValue<Vector2>();

				i = value;
				i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
			};


			inputs.Player.WASD.canceled += ctx =>
			{
				i = ctx.ReadValue<Vector2>();
			};

			return i;
		}
	}

	public Vector2 down
	{
		get { return _down; }
	}

	public Vector2 raw
	{
		get
		{
			inputs.Player.WASD.performed += ctx =>
			{
				var value = ctx.ReadValue<Vector2>();   // Read value from control.
				var control = ctx.control;
				var button = control as ButtonControl;

				if (value.x > 0)
					iRaw.x = 1;

				if (value.x < 0)
					iRaw.x = -1;

				if (value.y > 0)
					iRaw.y = 1;

				if (value.y < 0)
					iRaw.y = -1;

				iRaw *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
			};

			inputs.Player.WASD.canceled += ctx =>
			{
				iRaw = ctx.ReadValue<Vector2>();
			};

			return iRaw;
		}
	}

	public float elevate
	{
		get
		{
			inputs.Player.Jump.performed += ctx =>
			{
				jumping = 1;
			};
			inputs.Player.Jump.canceled += ctx =>
			{
				jumping = 0;
			};
			return jumping;
		}
	}

	public bool run
	{
		get
		{
			inputs.Player.Sprint.started += ctx =>
			{
				sprinting = true;
			};
			inputs.Player.Sprint.canceled += ctx =>
			{
				sprinting = false;
			};
			return sprinting;
		}
	}

	public bool crouch
	{
		get
		{
			if (inputs.Player.Crouch.triggered)
			{
				_crouch = true;
			}
			else
			{
				_crouch = false;
			}
			return _crouch;
		}
	}

	public bool crouching
	{
		get
		{
			inputs.Player.Crouch.started += ctx =>
			{
				_crouching = true;
			};
			inputs.Player.Crouch.canceled += ctx =>
			{
				_crouching = false;
			};

			return _crouching;
		}
	}

	public bool interactKey
	{
		get
		{
			inputs.Player.Interact.started += ctx =>
			{
				interacting = true;
				Debug.Log(interacting);
			};
			inputs.Player.Interact.canceled += ctx =>
			{
				interacting = false;
				Debug.Log(interacting);
			};
			
			return interacting;
		}
	}

	public bool interact
	{
		get
		{
			return interactKey;
		}
	}

	public bool reload
	{
		get { return Input.GetKeyDown(KeyCode.R); }
	}

	public bool aim
	{
		get { return Input.GetMouseButtonDown(1); }
	}

	public bool aiming
	{
		get { return Input.GetMouseButton(1); }
	}

	public bool shooting
	{
		get { return Input.GetMouseButton(0); }
	}

	public float mouseScroll
	{
		get { return Input.GetAxisRaw("Mouse ScrollWheel"); }
	}

	public void OnEnable()
	{
		inputs.Enable();
	}

	public void OnDisable()
	{
		inputs.Disable();
	}

	private void Awake()
	{
		inputs = new Inputs();
	}

	void Start()
	{
		jumpTimer = -1;
	}

	void Update()
	{
		_down = Vector2.zero;
		if (raw.x != previous.x)
		{
			previous.x = raw.x;
			if (previous.x != 0)
				_down.x = previous.x;
		}
		if (raw.y != previous.y)
		{
			previous.y = raw.y;
			if (previous.y != 0)
				_down.y = previous.y;
		}
	}

	public void FixedUpdate()
	{
		//if (!Input.GetKey(KeyCode.Space))
		//{
		//	jump = false;
		//	jumpTimer++;
		//}
		//else if (jumpTimer > 0)
		//	jump = true;
		if (!jump)
		{
			jumpTimer++;
		}

	}

	public bool Jump()
	{
		inputs.Player.Jump.performed += ctx =>
		{
			if (jumpTimer > 0)
			{
				jump = true;
			}
		};
		inputs.Player.Jump.canceled += ctx =>
		{
			jump = false;
		};

		return jump;
	}

	public void ResetJump()
	{
		jumpTimer = -1;
	}
}
