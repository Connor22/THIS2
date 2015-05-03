using UnityEngine;
using System.Collections;
public class PlayerMovement: MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float jumpHeight = 3f;
	public float extraSpeedOnJump = 5f;

	[HideInInspector]
	private float horizontalSpeed = 0;
	private float finalizedRunSpeed;
	
	private CharacterController2D charControl;
	private Animator anim;
	private AudioSource[] Sounds;
	private AudioSource jumpSound;
	private AudioSource dbljumpSound;

	private Vector3 curVelocity;
	private PlayerActions power_up;
	private bool hasDoubleJumped;
	private bool idleStarted;
	private float groundDamping = 20f; // how fast do we change direction? higher means faster
	private float inAirDamping = 5f;
	
	
	void Awake()
	{
		anim = GetComponent<Animator>();
		Sounds = GetComponents<AudioSource>();
		power_up = gameObject.GetComponent<PlayerActions>();
		charControl = GetComponent<CharacterController2D>();

		jumpSound = Sounds[0];
		dbljumpSound = Sounds[1];

		idleStarted = false;
	}
	
	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		// grab our current curVelocity to use as a base for all calculations
		curVelocity = charControl.velocity;
		
		if( charControl.isGrounded ){
			curVelocity.y = 0;
			hasDoubleJumped = false;
			finalizedRunSpeed = runSpeed;
		} else {
			finalizedRunSpeed = runSpeed + extraSpeedOnJump;
		}
		
		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			horizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( charControl.isGrounded )
				anim.Play( Animator.StringToHash( "Walk" ) );
				idleStarted = false;
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			horizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( charControl.isGrounded )
				anim.Play( Animator.StringToHash( "Walk" ) );
				idleStarted = false;
		}
		else
		{
			horizontalSpeed = 0;
			
			if( charControl.isGrounded && !idleStarted ){
				anim.Play( Animator.StringToHash( "Idle" ) );
				idleStarted = true;
			}
		}
		
		
		// we can only jump whilst grounded
		if( charControl.isGrounded && Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			jumpSound.Play();
			curVelocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			idleStarted = false;
			anim.Play( Animator.StringToHash( "Jump" ) );
		} else if ( Input.GetKeyDown( KeyCode.UpArrow ) && !hasDoubleJumped && power_up.state == PlayerActions.Its.Jump && (power_up.uses_left - power_up.jump_cost) > 0 )  {
			dbljumpSound.Play();
			hasDoubleJumped = true;
			curVelocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			power_up.SendMessage("JumpCounter");
		}

		if( charControl.isGrounded && !Input.GetKey( KeyCode.LeftArrow ) && !Input.GetKey( KeyCode.RightArrow ) && !Input.GetKey( KeyCode.UpArrow ) ) {
			Vector3 stopVec = new Vector3(0, 0, curVelocity.z);
			curVelocity = stopVec;
		}
		
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = charControl.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		curVelocity.x = Mathf.Lerp( curVelocity.x, horizontalSpeed * finalizedRunSpeed , Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		curVelocity.y += gravity * Time.deltaTime;
		
		charControl.move( curVelocity * Time.deltaTime );
	}
}