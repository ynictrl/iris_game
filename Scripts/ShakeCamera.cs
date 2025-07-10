using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

	public float shakeDuration = 0.5f;
	public float shakeAmplitude = 0.1f;
	public float shakeFrequency = 0.1f;

	// public float currentShakeDuration = 0f;
	// private Vector3 originalPosition;

	private void Start()
	{
		// originalPosition = transform.localPosition;
	}

	private void Update()
	{
		// if (currentShakeDuration > 0)
		// {
		// 	Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
		// 	transform.localPosition = originalPosition + randomOffset;

		// 	currentShakeDuration -= Time.deltaTime;
		// }
		// else
		// {
		// 	currentShakeDuration = 0f;
		// 	transform.localPosition = originalPosition;
		// }


	}

	public void Shake(float shakeAmp, float shakeFre)
	{
		// GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain =-Time.deltaTime;
		// currentShakeDuration = shakeDuration;
		GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = shakeAmp;
		GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = shakeFre;
	}

	public IEnumerator Shaking(float shakeAmp, float shakeFre)
    {
		GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = shakeAmp;
		GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = shakeFre;
		// Shake();

		// GetComponent<CinemachineBasicMultiChannelPerlin>().enabled = true;
        yield return new WaitForSeconds(shakeDuration);

		GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = 0;
		GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = 0;
    }





}//End




/*
	 snippets: 
	DEB, SS, CC, TEX, PLAY, STOP, SET, FF, TMP, AUD, ANIM, GAME, FIX, VOID, EE, RR, ON, TT
	
	#if UNITY_EDITOR 
	UnityEditor.EditorGUIUtility.PingObject(gameObject); 
	#endif

 	Time.timeScale = 0f;
   
	GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

	GetComponent<Rigidbody>().isKinematic = true false;

	GetComponent<Rigidbody2D>().gravityScale = 0;

	GetComponent<Collider2D>().isTrigger = true false;
 	
	//TAGS
	this.transform.gameObject.tag = "Untagged";
	
	//LAYER
	gameObject.layer = 2;// to change gameObject layer numbers
	
	//IGNORE COLLISION
	Physics2D.IgnoreLayerCollision(10, 11);//PHYSICS 2D
	Physics.IgnoreLayerCollision(10, 11);//PHYSICS 3D
	
	//FIND OBJECTS
 	GameObjectToFin = GameObject.Find("GameObject");
	GameObjectToFin = GameObject.FindGameObjectWithTag("tag");
	GameObjectToFin = GameObject.FindObjectOfType<GameObjectInHierachy>(); 
   
	if (score > PlayerPrefs.GetInt ("Record" ))
	PlayerPrefs.SetInt ("Record", score); 

	PlayerPrefs.DeleteAll();
    PlayerPrefs.DeleteKey("mScore");
  
	// CALL VOID IN THE EDITOR 
	[ContextMenu("nameMethod")]
 
	//SCALE
	transform.localScale = new Vector3(1f, 1f, 0f); 
  
	// UI CANVAS 
	GetComponent<Image>().color = new Color32(255, 0, 0, 255);
	GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);// 2D OBJECTS SpriteRenderer
	
	//GOES FOR BOTH
	public Sprite m_sprite;
	gameObject.GetComponent<Image>().sprite = m_sprite;//UI
	gameObject.GetComponent<SpriteRenderer>().sprite = m_sprite;//RENDERER 2D

	GetComponent<Renderer>().material = Resources.Load<Material>("new");//requires folder named "Resources"

	//AUDIO
	public AudioClip sound;// 
	AudioSource.PlayClipAtPoint(sound,FindObjectOfType<AudioListener>().transform.position,1f);//1f=volume

	AudioListener.volume=0f//global unity audio
	
	// IF FOR SCENES
	if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("scenename")) //then do sth

    //the one below moves the direction it is facing also it works with impulse 2d
	transform.Translate(Vector3.forward * Time.deltaTime * speed);//Vector3.forward/right/left/up/down
	 
	//the one below moves the direction it is facing
	transform.Translate(Vector3.forward * Time.deltaTime * speed);//Vector3.forward/right/left/up/down
	rb.AddForce(transform.right * speed * Time.deltaTime);//right/up/forward
	rb.velocity = transform.right * speed * Time.deltaTime;
	rb.velocity = transform.forward * walkSpeed * Time.deltaTime;//Vector3.forward/right/up *works better
	
	//LOOK AT TARGET POSITION 3D
	Vector3 lookAtPosition = Player.position; //the target 
	lookAtPosition.y = transform.position.y;//wont look at Y
	transform.LookAt(lookAtPosition);
	transform.LookAt(Target.transform);//normal look at target	 
	
	//unity profiler 
	using UnityEngine.Profiling;
	void Update(){//can be used in any method
	Profiler.BeginSample("Search_this in the unity profiler");
	//your line codes go here
	Profiler.EndSample();
	} 
	
	//RANDOM TEXT COLOR
	text.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
	 
	  
	//bullet in a canvas and disattached of its original object
	GameObject Bullet = Instantiate(BulletPref, ShotPos.position, Quaternion.identity) as GameObject;
    Bullet.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
	
	//INTERVAL IN UPDATE
	private float interval = 2f;
	if (Time.frameCount % interval == 0)
	{
	
	}
  
	  
	GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		
	transform.parent = transform;
	transform.parent = null;
	gameObject.transform.SetParent(null);//for new unity version

	gameObject.transform.GetChild(0);
	
	if(gameObject.activeInHierarchy){ }
	
	 Screen.SetResolution(640, 480, true);
	 
	LeanTween.reset();
	public LeanTweenType easeType;
	setEase(easeType).setOnComplete(voidname).delay = 1f;
	LeanTween.moveLocalZ(move, 0, speed).setEaseInOutCubic().setLoopPingPong();

#if UNITY_ANRDOID
     // Android code goes here
#elif UNITY_IOS
     // IOS Code goes here
#endif

		"<size=175%>FIX</size>"

  PhotonNetwork.UseRpcMonoBehaviourCache = true;
  
 [TextArea(4, 10)]

*/