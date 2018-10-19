using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour 
{
	public float speed;
	public float sideBoundary;
	public float rotateAngle;
	Vector3 position;
	Quaternion rotation;
	float MaxSpeed;
	float vert;
	float horiz;
	Animator carTurn;
    public GameController gameController;
    public Game_Manager game_Manager;

    float random_offset = 0;

    // Use this for initialization
    void Start () 
	{
		MaxSpeed = 88.0f;
		position = transform.position;
		rotation = transform.rotation;
		carTurn = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
//	void Update()
//	{
//
//		speed += 0.02f;
//
//	}

	void Update () 
	{
		

		vert = Input.GetAxis ("Vertical");
		horiz = Input.GetAxis ("Horizontal");

		//only rotates on z axis
		//transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z);

        //transform.gameObject.GetComponent<Rigidbody2D> ().AddForce (gameObject.transform.up * speed * vert);

        position.x += (horiz + Mathf.Lerp(0,random_offset,Time.deltaTime*10f)* speed * Time.deltaTime);
        transform.position = position;
        //transform.Translate(transform.right*( horiz * speed * Time.deltaTime));

        Mathf.Clamp (transform.position.x, -sideBoundary, sideBoundary);
		//rotation.z = Mathf.Clamp (rotation.z, -30, 30);


		if(Input.GetButtonDown("Left"))// && horiz >= 0)
		{
			carTurn.SetBool ("Left", true);
			//if (horiz > 0 && transform.rotation.z < 30 && transform.rotation.z > 0) {
			Debug.Log ("ButtonDownR");

			transform.Rotate(0, 0, horiz * rotateAngle * Time.deltaTime);
		}
		else if (Input.GetButtonDown("Right"))// && horiz <= 0) 
		{

			carTurn.SetBool ("Right", true);
			//Debug.Log ("ButtonDownL");

			transform.Rotate(0, 0, rotateAngle  * Time.deltaTime );
		}

		if(Input.GetButtonUp("Horizontal"))
		{
			carTurn.SetBool ("Left", false);
			carTurn.SetBool ("Right", false);
			//transform.rotation = new Quaternion (0, 0, 0,0);

			horiz = 0.0f;
			//Debug.Log ("Button Up");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("EnemyCar"))
		{
            Debug.Log ("collide");
            KillPlayer();
        }
		if(other.CompareTag("Oil"))
		{
            //Debug.Log ("collideOil");
            gameController.speed -= 0.5f;
            random_offset = Random.Range(-8, 8);
		}
	

	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EnemyCar"))
        {
            //Debug.Log ("collide");
            //gameController.speed -= 0.5f;
        }
        if (other.CompareTag("Oil"))
        {
            //Debug.Log ("collideOil");
            //gameController.speed -= 0.5f;
            random_offset = 0f;
        }


    }

    void KillPlayer()
    {
        game_Manager.Lose();
        // Stop speed

        // Play explode sound


    }

}