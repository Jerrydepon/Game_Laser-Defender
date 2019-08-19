using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {
	
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public int health = 150;
	public float shotsPerSeconds = 0.5f;
	public float shootingFreq = 2f;
	public int scoreValue = 150;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability * shootingFreq){
			Fire ();
		}
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0, -0.5f, 0);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Die();
			}
		}
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
}
