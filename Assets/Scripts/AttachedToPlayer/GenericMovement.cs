using UnityEngine;
using System.Collections;
public class GenericMovement: MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float jumpHeight = 3f;

	public AudioClip jumpClip;

	[HideInInspector]
	private float horizontalSpeed = 0;
	
	private CharacterController2D charControl;
	private Animator anim;
	private AudioSource jumpSound;

	private Vector3 curVelocity;
	private bool hasDoubleJumped;
	private float groundDamping = 20f; // how fast do we change direction? higher means faster
	private float inAirDamping = 5f;
	
	
	void Awake()
	{
		//anim = GetComponent<Animator>();
		charControl = GetComponent<CharacterController2D>();
		jumpSound = GetComponent<AudioSource>();
	}
	
	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		// grab our current curVelocity to use as a base for all calculations
		curVelocity = charControl.velocity;
		
		if( charControl.isGrounded )
			curVelocity.y = 0;
		
		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			horizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			//if( charControl.isGrounded )
				// anim.Play( Animator.StringToHash( "Run" ) );
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			horizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			//if( charControl.isGrounded )
				// anim.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			horizontalSpeed = 0;
			
			//if( charControl.isGrounded )
				// anim.Play( Animator.StringToHash( "Idle" ) );
		}
		
		
		// we can only jump whilst grounded
		if( charControl.isGrounded && Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			jumpSound.PlayOneShot(jumpClip, 1F);
			hasDoubleJumped = false;
			curVelocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			// anim.Play( Animator.StringToHash( "Jump" ) );
		} else if ( Input.GetKeyDown( KeyCode.UpArrow ) && !hasDoubleJumped ) {
			hasDoubleJumped = true;
			curVelocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
		}

		if( charControl.isGrounded && !Input.GetKey( KeyCode.LeftArrow ) && !Input.GetKey( KeyCode.RightArrow ) ) {
			Vector3 stopVec = new Vector3(0, 0, curVelocity.z);
			curVelocity = stopVec;
		}
		
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = charControl.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		curVelocity.x = Mathf.Lerp( curVelocity.x, horizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		curVelocity.y += gravity * Time.deltaTime;
		
		charControl.move( curVelocity * Time.deltaTime );
	}
}