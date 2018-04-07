using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecontroller : MonoBehaviour {
	public Camera gamecamera;
	public GameObject bulletprefab;
	public GameObject enemyprefab;

	public float enemyspawningcooldown = 1f;
	public float enemyspawntimer = 0;
	public float enemyspawningdistance=7f;

	public float shootingcooldown = 0.75f;
	private float shootingTimer = 0;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "enemy") {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		shootingTimer -= Time.deltaTime;
		enemyspawntimer -= Time.deltaTime;
		if (enemyspawntimer <= 0f) {
			enemyspawntimer = enemyspawningcooldown;
			GameObject enemyObject = Instantiate (enemyprefab);
			float randomangle = Random.Range (0,Mathf.PI*2);
			enemyObject.transform.position = new Vector3 (gamecamera.transform.position.x+Mathf.Cos(randomangle)*enemyspawningdistance,
			0,
			gamecamera.transform.position.z+Mathf.Sin(randomangle)*enemyspawningdistance);
			enemy enemy1 = enemyObject.GetComponent<enemy> ();
			enemy1.direction = (gamecamera.transform.position - enemy1.transform.position).normalized;
			enemy1.transform.LookAt (Vector3.zero);
		}
		if(Physics.Raycast(gamecamera.transform.position,gamecamera.transform.forward,out hit)){
			Debug.Log ("1");
			if (hit.transform.tag=="enemy" && shootingTimer<=0f) {
				GameObject bulletobject=Instantiate (bulletprefab);
				bulletobject.transform.position = gamecamera.transform.position;
				shootingTimer = shootingcooldown;
				bullet bullet1 = bulletobject.GetComponent<bullet> ();
				bullet1.direction = gamecamera.transform.forward;
				bullet1.transform.LookAt (Vector3.zero);
			}
		}
	}
}
