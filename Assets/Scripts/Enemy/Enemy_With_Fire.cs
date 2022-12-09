using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_With_Fire : MonoBehaviour
{
	public float agringDistanse;
	public Transform[] wayPoints;
	public Sprite deathBody;
	public GameObject playerObj, curWeapon, killedEnemy;
	public float minDistance = 4f, movingSpeed = 0.1f, deltaDictance = 0.3f, disagringDistance = 15f;
	public bool agred = false, walk = true;
	public AudioSource stepsSound;
/*	public Transform[] enters;*/
	public int heals = 1;
	[SerializeField] private LayerMask obstacleLayerMask;

	private int wayPointNumber;
	private Transform player,/* enter,*/ enemy;
/*	private bool isReturning = false;*/
	private Animator cur_weapon_animator, animator;
	private FireWeapon cur_weapon_weapon;
	private NavMeshAgent agent;
	bool canDie = true;
	void Start()
	{
		enemy = GetComponent<Transform>();
		player = playerObj.GetComponent<Transform>();
		animator = GetComponent<Animator>();
		cur_weapon_weapon = curWeapon.GetComponent<FireWeapon>();
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
		if (player != null && !Physics2D.Linecast(transform.position, player.position, obstacleLayerMask) && Vector2.Angle(transform.up, player.transform.position - transform.position) < 30 && Vector2.Distance(enemy.position, player.position) <= agringDistanse/* && player.GetComponent <PlayerMover> ().curRoom == transform.parent.gameObject*/)
		{
			if (movingSpeed != 0)
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


		/*
					if (!isReturning)
					{
						*/
				if (!agred)
				{
					agent.SetDestination(wayPoints[wayPointNumber].position);
/*					enemy.Translate(Vector2.up * movingSpeed);*/
					cur_weapon_animator.SetBool("Attack", false);
/*					angle = Vector2.Angle(Vector2.up, wayPoints[wayPointNumber].position - enemy.position);
					enemy.eulerAngles = new Vector3(0, 0, enemy.position.x < wayPoints[wayPointNumber].position.x ? -angle : angle);*/
					walk = true;
				}
				else if (player != null)
				{
					if (Vector2.Distance(enemy.position, player.position) - deltaDictance > minDistance)
					{
						
/*						enemy.Translate(Vector2.up * movingSpeed);*/
						walk = true;


					}
					else
					{
/*						enemy.Translate(Vector2.up * movingSpeed);*/
						walk = true;
						cur_weapon_animator.SetBool("Attack", true);
						cur_weapon_weapon.Attack();

					}
				/*				else if (Vector2.Distance(enemy.position, player.position) + deltaDictance < minDistance)
								{
									enemy.Translate(Vector2.down * movingSpeed);



								}*/

/*					angle = Vector2.Angle(Vector2.up, player.position - enemy.position);
					enemy.eulerAngles = new Vector3(0, 0, enemy.position.x < player.position.x ? -angle : angle);*/
					agent.SetDestination(player.position);

				}
				angle = Vector2.Angle(Vector2.up, agent.steeringTarget - enemy.position);
				enemy.eulerAngles = new Vector3(0, 0, enemy.position.x < agent.steeringTarget.x ? -angle : angle);
		/*			}*/
		/*			else
					{
						angle = Vector2.Angle(Vector2.up, agent.steeringTarget - enemy.position);
						enemy.eulerAngles = new Vector3(0, 0, enemy.position.x < agent.steeringTarget.x ? -angle : angle);
						FindEnter();
		*//*				enemy.Translate(Vector2.up * movingSpeed);*//*
					}*/
		/*else
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

/*    void FindEnter()
    {
        int minNumber = 0;
        for (int i = 0; i < enters.Length; i++)
        {
			print("a");	
			float distToEnter;
			agent.SetDestination(enters[minNumber].position);
			distToEnter = agent.remainingDistance;
			agent.SetDestination(enters[i].position);
			print(distToEnter);
			print(agent.remainingDistance);
			*//*			if (Vector2.Distance(enters[minNumber].position, enemy.position) > Vector2.Distance(enters[i].position, enemy.position))
							minNumber = i;*//*

			if (distToEnter > agent.remainingDistance)
            {
				minNumber = i;
				print(enters[minNumber].name);
				agent.SetDestination(enters[minNumber].position);
			}
        }
    }
*/
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

*//*				FindEnter();*//*
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "PlayerAttack" && !Physics2D.Linecast(transform.position, player.position, obstacleLayerMask))
		{
			canDie = false;
			StartCoroutine(ImmortalFrames(playerObj.GetComponent<PlayerMover>().currentWeapon.GetComponent<meleeWeapon>().attackTime));
			KillEnemy();
		}
		if (collision.tag == "WayPoint" && collision.transform == wayPoints[wayPointNumber])
		{
			ArriveToPoint();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			if (collision.gameObject.GetComponent<Bullet>()) { Destroy(collision.gameObject); }
			KillEnemy();
		}
	}

	void KillEnemy()
	{
/*		print("damage"); */
		heals -= playerObj.GetComponent<PlayerMover>().damage;

		if (heals <= 0)
		{

			curWeapon.GetComponent<FireWeapon>().bulletsInHolder = curWeapon.GetComponent<FireWeapon>().bulletsNormalInHolder;
			cur_weapon_weapon.Throw();
		

			GameObject deathBody_ = Instantiate(killedEnemy, transform.position, transform.rotation);
			deathBody_.GetComponent<SpriteRenderer>().sprite = deathBody;
			Destroy(gameObject);
		}
	}
	IEnumerator ImmortalFrames(float immortalTime)
	{
		yield return new WaitForSeconds(immortalTime);
		canDie = true;


	}
}
