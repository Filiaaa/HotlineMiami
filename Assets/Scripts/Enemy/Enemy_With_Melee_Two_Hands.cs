using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_With_Melee_Two_Hands : MonoBehaviour
{
	public GameObject moving_weapon_part_of_anim;
	public float agringDistanse;
	public Transform[] wayPoints;
	public Sprite deathBody;
	public GameObject playerObj, curWeapon, killedEnemy;
	public float minDistance = 4f, movingSpeed = 0.1f, deltaDictance = 0.3f, disagringDistance = 15f;
	public bool agred = false, walk = true;
	public AudioSource stepsSound;
	public Transform[] enters;
	public int heals = 1, enemyIndex;
	[SerializeField] private LayerMask obstacleLayerMask;

	private int wayPointNumber;
	private Transform player, enter, enemy;
	private bool isReturning = false;
	RaycastHit2D[] results;
	private SpriteRenderer sr;
	private Animator cur_weapon_animator, animator;
	private meleeWeapon cur_weapon_weapon;
	private PlayerMover plM;
	private NavMeshAgent agent;
	bool canDie = true;
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		enemy = GetComponent<Transform>();
		plM = playerObj.GetComponent<PlayerMover>();
		player = playerObj.GetComponent<Transform>();
		animator = GetComponent<Animator>();
		cur_weapon_weapon = curWeapon.GetComponent<meleeWeapon>();
		cur_weapon_animator = curWeapon.GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

	private float angle;


	void FixedUpdate()
	{
		if (movingSpeed == 0)
		{
			walk = false;
		}
		if (!cur_weapon_weapon.attack)
		{
            curWeapon.SetActive(false);
			sr.enabled = true;

			moving_weapon_part_of_anim.SetActive(true);

		}
		else
		{
			moving_weapon_part_of_anim.SetActive(false);
		}


		if (player != null && !Physics2D.Linecast(transform.position, player.position, obstacleLayerMask) && Vector2.Angle(transform.up, player.transform.position - transform.position) < 30 && Vector2.Distance(enemy.position, player.position) <= agringDistanse/* && player.GetComponent <PlayerMover> ().curRoom == transform.parent.gameObject*/)
		{
			if(movingSpeed != 0)
            {
				movingSpeed = 0.3f;
			}
			agred = true;
		}
		else if (player != null && (Physics2D.Linecast(transform.position, player.position, obstacleLayerMask) || Vector2.Distance(enemy.position, player.position) > disagringDistance))
		{
			if (movingSpeed != 0)
			{
				movingSpeed = 0.1f;
			}

			agred = false;
		}

		//////////////////////////////////////////////////////////////////////////////////
		if (!walk)
		{
			animator.SetBool("walk", false);
/*			stepsSound.Stop();*/

		}
		else if (walk)
		{
/*			stepsSound.pitch = Random.Range(0.9f, 1.1f);*/
			animator.SetBool("walk", true);
/*			stepsSound.Play();*/
		}


/*			if (!isReturning)
			{*/
				if (!agred)
				{
					agent.speed = 6;
					agent.SetDestination(wayPoints[wayPointNumber].position);
				/*					enemy.Translate(Vector2.up * movingSpeed);*/

					cur_weapon_weapon.attack = false;

					/*angle = Vector2.Angle(Vector2.up, wayPoints[wayPointNumber].position - enemy.position);
					enemy.eulerAngles = new Vector3(0, 0, enemy.position.x < wayPoints[wayPointNumber].position.x ? -angle : angle);*/
					if(movingSpeed != 0){walk = true;}
				}
				else if (player != null)
				{
					agent.speed = 18;
					if (Vector2.Distance(enemy.position, player.position) - deltaDictance > minDistance) //оружие начинает бить
					{
						/*enemy.Translate(Vector2.up * movingSpeed);*/
						if(movingSpeed != 0){walk = true;}
						cur_weapon_weapon.gameObject.SetActive(false);
						sr.enabled = true;
						moving_weapon_part_of_anim.SetActive(true);
					}
					else
					{
/*						enemy.Translate(Vector2.up * movingSpeed);*/
						if(movingSpeed != 0){walk = true;}
						sr.enabled = false;
						cur_weapon_weapon.gameObject.SetActive(true);
						cur_weapon_animator.SetBool("Enemy", true);
						cur_weapon_animator.SetInteger("EnemyInd", enemyIndex);
						cur_weapon_weapon.Attack();

					}

			/*				else if (Vector2.Distance(enemy.position, player.position) + deltaDictance < minDistance)
							{
								enemy.Translate(Vector2.down * movingSpeed);



							}*/
					agent.SetDestination(player.position);



				}




				angle = Vector2.Angle(Vector2.up, agent.steeringTarget - enemy.position);
				enemy.eulerAngles = new Vector3(0, 0, enemy.position.x < agent.steeringTarget.x ? -angle : angle);



        /*			}*/
        /*			else
					{
						angle = Vector2.Angle(Vector2.up, enter.GetComponent<Transform>().position - enemy.position);
						enemy.eulerAngles = new Vector3(0, 0, enemy.position.x < enter.GetComponent<Transform>().position.x ? -angle : angle);

						enemy.Translate(Vector2.up * movingSpeed);

					}*/

    }


	public void ArriveToPoint()
	{
		wayPointNumber++;
		wayPointNumber %= wayPoints.Length;
	}

 /*   Transform FindEnter()
    {
        int minNumber = 0;
        for (int i = 1; i < enters.Length; i++)
        {
            if (Vector2.Distance(enters[minNumber].position, enemy.position) > Vector2.Distance(enters[i].position, enemy.position))
                minNumber = i;
        }
        return enters[minNumber];
    }*/

/*    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            if (collision.gameObject == transform.parent.gameObject)
            {
                isReturning = false;
            }
            else if(!agred)
            {
                isReturning = true;
                enter = FindEnter();
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "PlayerAttack" && !Physics2D.Linecast(transform.position, player.position, obstacleLayerMask))
		{
			KillEnemy();
		}
		if (collision.tag == "WayPoint" && collision.transform == wayPoints[wayPointNumber])
		{
			ArriveToPoint();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack" && canDie)
		{
			canDie = false;
			StartCoroutine(ImmortalFrames());
			if (collision.gameObject.GetComponent<Bullet>() != null) { Destroy(collision.gameObject); }
			KillEnemy();
		}
	}

	void KillEnemy()
	{

		heals -= playerObj.GetComponent<PlayerMover>().damage;

		if (heals <= 0)
		{
			

			cur_weapon_weapon.Throw();
			cur_weapon_weapon.attack = false;
			cur_weapon_animator.SetBool("Enemy", false);

			GameObject deathBody_ = Instantiate(killedEnemy, transform.position, transform.rotation);
			deathBody_.GetComponent<SpriteRenderer>().sprite = deathBody;
			Destroy(gameObject);
		}
	}

	IEnumerator ImmortalFrames()
    {
		yield return new WaitForSeconds(0.33f);
		canDie = true;


	}

}
