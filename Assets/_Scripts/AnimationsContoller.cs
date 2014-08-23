using UnityEngine;
using System.Collections;

public class AnimationsContoller : MonoBehaviour 
{
    public Animator MecanimController;
    public bool Dancing = false;
    public bool Wave = false;
    public AudioSource Footsteps;

    private float Speed = 0;
    private float Direction = 0;
    private bool Jump = false;
    private bool Dead = false;
    private bool Roll = false;
    private ExplosionController ExController;

    //
    private bool RestartDirectionSlowly = false;
	// Use this for initialization
	void Start () 
    {
        ExController = gameObject.GetComponent<ExplosionController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Speed != 0)
        {
            Footsteps.enabled = true;
        }
        else
        {
            Footsteps.enabled = false;
        }

        if (networkView.isMine && !ExController.IsImmobilized)
        {
            ListenForButtons();
        }

        PassDataToController();

        if (RestartDirectionSlowly)
        {
            RestartDirection();
        }
	}
    
    void ListenForButtons()
    {
        if (Input.GetButton("Forward"))
        {
            Speed = 1;
            Dancing = false;
            Wave = false;
        }
        if (Input.GetButton("Backwards"))
        {
            Speed = -1;
            Dancing = false;
            Wave = false;
        }
        if (Input.GetButtonUp("Forward"))
        {
            Speed = 0;
        }
        if (Input.GetButtonUp("Backwards"))
        {
            Speed = 0;
        }
        if (Input.GetButton("Left") || Input.GetButton("StrafeLeft"))
        {
            Direction = Mathf.Lerp(Direction, -1, 5 * Time.deltaTime);
        }
        if (Input.GetButton("Right") || Input.GetButton("StrafeRight"))
        {
            Direction = Mathf.Lerp(Direction, 1, 5 * Time.deltaTime);
        }
        if (Input.GetButtonUp("Left") || Input.GetButtonUp("StrafeLeft"))
        {
            RestartDirectionSlowly = true;
        }
        if (Input.GetButtonUp("Right") || Input.GetButtonUp("StrafeRight"))
        {
            RestartDirectionSlowly = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            SetJump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            SetJump();
        }
        if (Input.GetButtonDown("Roll"))
        {
            SetRoll();
        }
        if (Input.GetButtonUp("Roll"))
        {
            SetRoll();
        }
    }

    
    void PassDataToController()
    {
        MecanimController.SetFloat("Speed", Speed);
        MecanimController.SetFloat("Direction", Direction);
        MecanimController.SetBool("Jump", Jump);
        MecanimController.SetBool("Roll", Roll);
        MecanimController.SetBool("Wave", Wave);
        MecanimController.SetBool("Dead", Dead);
        MecanimController.SetBool("Dancing", Dancing);
    }

    void RestartDirection()
    {
        if(Direction < 0.1 && Direction > 0 || Direction > -0.1 && Direction < 0)
        {
            RestartDirectionSlowly = false;
            Direction = 0;
        }
        Direction = Mathf.Lerp(Direction, 0, 5 * Time.deltaTime);
    }

    void SetJump()
    {
        if (Jump == false)
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }
    }

    void SetRoll()
    {
        if (Roll == false)
        {
            Roll = true;
        }
        else
        {
            Roll = false;
        }
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        float speed = Speed;
        float direction = Direction;
        bool jump = Jump;
        bool dead = Dead;
        bool wave = Wave;
        bool roll = Roll;
        bool dancing = Dancing;
        if (stream.isWriting)
        {
            stream.Serialize(ref speed);
            stream.Serialize(ref direction);
            stream.Serialize(ref jump);
            stream.Serialize(ref wave);
            stream.Serialize(ref roll);
            stream.Serialize(ref dead);
            stream.Serialize(ref dancing);
        }
        else
        {
            stream.Serialize(ref speed);
            stream.Serialize(ref direction);
            stream.Serialize(ref jump);
            stream.Serialize(ref wave);
            stream.Serialize(ref roll);
            stream.Serialize(ref dead);
            stream.Serialize(ref dancing);
            Speed = speed;
            Direction = direction;
            Jump = jump;
            Dead = dead;
            Wave = wave;
            Roll = roll;
            Dancing = dancing;
        }
    }
}
