using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{
	public Text bulletText;
	public GameObject currentWeapon, knife, killedPlayer, restartPanel, curRoom;
	public float movingSpeed, soundRadius = 10;
	public static float bulletSpeedBuff = 1;
	public bool canThrow = true, isImmortal = false;
	public AudioSource steps;
	public int hearts = 1, damage = 16, soundResultsNumb;
	List<Collider2D> soundResults = new List<Collider2D>();
	[SerializeField] private ContactFilter2D enemyContactFilter;

	float hor, vert, angle;
	Rigidbody2D rb;
	bool attacked = true;
	
	private void Start () {
		Time.timeScale = 1;
		bulletSpeedBuff = 1;
		currentWeapon = knife;
		rb = GetComponent <Rigidbody2D> ();
	}

	void FixedUpdate () {
		/*		soundResults.Clear();*/
		soundRadius = currentWeapon.GetComponent<Weapon>().soundRadius;
        if (currentWeapon != knife)
        {
			knife.SetActive(false);
        }
        else { knife.SetActive(true); }
/*		if (currentWeapon.TryGetComponent(out FireWeapon FW))
		{
			bulletText.text = FW.bulletsInHolder.ToString() + "/" + FW.bulletsNormalInHolder.ToString();


		}*/
		if (currentWeapon.GetComponent<FireWeapon>() == null)
        {
			bulletText.gameObject.SetActive(false);
		}
        else
        {
			bulletText.gameObject.SetActive(true);
			bulletText.text = currentWeapon.GetComponent<FireWeapon>().bulletsInHolder.ToString() + "/" + currentWeapon.GetComponent<FireWeapon>().bulletsNormalInHolder.ToString();
		}
        if (Input.GetKey(KeyCode.G) && currentWeapon != knife)
        {
			canThrow = true;
			currentWeapon.GetComponent<Weapon>().Throw();
			currentWeapon = null;
		}
		if (currentWeapon == null) {
			currentWeapon = knife;
			GetComponent <Animator> ().SetBool ("withKnife", true);
			knife.SetActive (true);
		}
		if (Input.GetMouseButton (0)) {
			attacked = currentWeapon.GetComponent <Weapon> ().Attack();
            if (attacked)
            {
				soundResultsNumb = Physics2D.OverlapCircle(transform.position, soundRadius, enemyContactFilter, soundResults);
                foreach (Collider2D nearEnemy in soundResults)
                {
					nearEnemy.gameObject.GetComponent<EnemyMovement>().agred = true;

				}
				
			}
			bulletText.gameObject.SetActive(true);
		
			/*			if (currentWeapon != knife)
						{
							currentWeapon.GetComponent<SpriteRenderer>().enabled = true;
						}*/
			/*		GetComponent<Animator>().SetBool("Attack", true);*/
			if (!attacked) {
				currentWeapon = null;
			}
		}

        if (!Input.GetMouseButton(0))
        {
/*            GetComponent<Animator>().SetBool("Attack", false);*/
            if (currentWeapon.TryGetComponent(out FireWeapon fw))
            {
				currentWeapon.GetComponent<Animator>().SetBool("Attack", false);
               

            }

            // currentWeapon.SetActive (false);
        }

        // 		else if (!Input.GetMouseButton (0)) {
        // /*            GetComponent<Animator>().SetBool("Attack", false);*/
        //       if (currentWeapon != knife) {
        // 				currentWeapon.SetActive (false);
        // /*				currentWeapon.GetComponent<Animator>().SetBool("Attack", false);*/
        // }
        // }


        vert = Input.GetAxis ("Vertical");
		hor = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2(hor, vert) * movingSpeed;
		if (rb.velocity == Vector2.zero) {
			currentWeapon.SetActive(true);
			GetComponent <Animator> ().SetBool("walk", false);
			steps.Stop();
		}
		else  {
			if (currentWeapon.GetComponent<meleeWeapon>() != null && !currentWeapon.GetComponent<meleeWeapon>().attack)
            {
				currentWeapon.SetActive(false);

			}
			GetComponent <Animator> ().SetBool("walk", true);
			if (!steps.isPlaying) {
				steps.pitch = Random.Range (0.9f, 1.1f);
				steps.Play();
			}
		}

		var mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition); //положение мыши из экранных в мировые координаты
		angle = Vector2.Angle (Vector2.up, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
/*        if (Vector2.Distance(mousePosition, transform.position) > 0.6f)
		{*/
		transform.eulerAngles = new Vector3 (0f, 0f, transform.position.x < mousePosition.x ? -angle : angle);//немного магии на последок
/*        }*/

	}

	private void OnTriggerStay2D (Collider2D collision) {
		if (collision.tag == "Weapon" && Input.GetMouseButtonDown (1) && canThrow) {
			if (currentWeapon != knife) {
				currentWeapon.GetComponent <Weapon> ().Throw();
			} 
			GetComponent <Animator> ().SetBool ("withKnife", false);
			currentWeapon = collision.gameObject;
			currentWeapon.GetComponent <Weapon> ().Take (transform);
            if (currentWeapon.GetComponent<FireWeapon>() != null)
            {
				currentWeapon.GetComponent<FireWeapon>().colInPlayersHands.enabled = true;
			}
			// currentWeapon.SetActive (false);
		}

	}
	
	void OnTriggerEnter2D (Collider2D collision) {
		
/*		if (collision.tag == "Room") {
			
			curRoom = collision.gameObject;
			curRoom.GetComponent <Manager> ().enemysAgr();
*/
		/*}*/ /*else*/ if (collision.tag == "EnemyAttack") {
			Destroy(collision.gameObject);
			KillPlayer ();
		}
	}
/*
	void OnTriggerExit2D (Collider2D collision) {
		
		if (collision.tag == "Room") {

			curRoom.GetComponent <Manager> ().enemysAgr();
			curRoom = null;

		}
	}*/

	public void KillPlayer () {
		if (!isImmortal) {
			hearts--;
            if (hearts == 0)
            {
				Instantiate(killedPlayer, transform.position, transform.rotation);
				restartPanel.SetActive(true);
				// if (curRoom != null) curRoom.GetComponent <Manager> ().enemysDisAgr();
				Destroy(gameObject);
			}
		}
	}

}
