using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	Vector3[] path;
	int targetIndex;
	//-------------------
	Vector3 target;
	bool selected = false;
	VehicleStatus vehicleStatus; 
	LineRenderer lineRenderer; 
	Coroutine myCoroutine;
	float timePatrol;
	//-------------------
	public Dispatch dispatch;
	[System.NonSerialized] public UnitType unitType;
	[System.NonSerialized] public VehicleType vehicleType;
	public GameObject icon;
	public GameObject status;
	float speed;
	int timeSolve;
	//-------------------

	
	void Start() 
	{
		lineRenderer = GetComponent<LineRenderer>();
		vehicleStatus = VehicleStatus.OnPatrol;
		myCoroutine = StartCoroutine(Patrol());

		unitType = dispatch.unitType;
		vehicleType = dispatch.vehicleType;
		icon.GetComponent<SpriteRenderer>().sprite = Status.GetVehicleTypeSprite(vehicleType);
		speed = Status.GetVehicleSpeed(vehicleType);
		timeSolve = Status.GetTimeResolve(vehicleType);
		Debug.Log($"unit: {unitType}, vehicle: {vehicleType}, speed: {speed}, time solve: {timeSolve}.");
	}

	void OnMouseDown()
	{
		if (vehicleStatus == VehicleStatus.OnPatrol)
		{
			StopCoroutine(myCoroutine);
			vehicleStatus = VehicleStatus.WaitOrder;
			selected = true;
		}
		else if (vehicleStatus == VehicleStatus.WaitOrder)
		{
			vehicleStatus = VehicleStatus.FindNewPatrol;
			selected = true;
		}
		else if (vehicleStatus == VehicleStatus.FindNewPatrol)
		{
			vehicleStatus = VehicleStatus.OnPatrol;
			selected = false;
			myCoroutine = StartCoroutine(Patrol());
		}

		Debug.Log($"Status: {vehicleStatus}, Selected: {selected}");
	}

//---------------------------------------------------------------
	// ---------------OnPatrol--
	IEnumerator Patrol()
	{
		Vector3 startPatrol = transform.position;
		while(true)
		{
			timePatrol = 5f;
			Debug.Log("thời gian patrol: " + timePatrol);
			if(vehicleStatus != VehicleStatus.OnPatrol){break;} 

			yield return new WaitForSeconds(timePatrol);
			// Debug.Log("b1");
			if(vehicleStatus != VehicleStatus.OnPatrol){break;} 

			Vector3 patrol = GetRandomWalkablePosition(startPatrol, 2f);
			if(vehicleStatus != VehicleStatus.OnPatrol){break;} 

			PathRequestManager.RequestPath(transform.position,patrol,OnPathFound);
			if(vehicleStatus != VehicleStatus.OnPatrol){break;} 
			
			yield return null;
			if(vehicleStatus != VehicleStatus.OnPatrol){break;} 
		}
	}

	public static Vector3 GetRandomWalkablePosition(Vector3 center, float radius)
	{
		for (int i = 0; i < 10; i++)
    	{
			Vector3 randomPoint = center + (Vector3)(Random.insideUnitCircle * radius);
			Debug.Log(randomPoint);
			Node node = GridPath.instance.NodeFromWorldPoint(randomPoint);
			if (node != null && node.walkable)
			{
				return randomPoint;
			}
		}
		return center;
	}

	// ---------------FindNewPatrol--
	void Update()
	{
		if (Input.GetMouseButtonDown(1) && selected && vehicleStatus == VehicleStatus.FindNewPatrol)
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log(transform.position);

			PathRequestManager.RequestPath(transform.position,target,OnPathFound);
			vehicleStatus = VehicleStatus.OnRoute;
			selected = false;
			Debug.Log($"Status: {vehicleStatus}, Selected: {selected}");
		}
		
		status.GetComponent<SpriteRenderer>().sprite = Status.GetVehicleStatusSprite(vehicleStatus);
	}

	public void OnRoute(Vector3 accident)
	{
		if(Input.GetMouseButtonDown(1) && selected && vehicleStatus == VehicleStatus.WaitOrder)
		{
			Debug.Log("vị trí patrol mới: " + accident);
			PathRequestManager.RequestPath(transform.position,accident,OnPathFound);
			vehicleStatus = VehicleStatus.OnRoute;
			selected = false;
			Debug.Log($"Status: {vehicleStatus}, Selected: {selected}");
		}
	}

	// ---------------OnScene--
	public void OnScene()
	{
		StopCoroutine(myCoroutine);
		myCoroutine = StartCoroutine(Work());
	}

	IEnumerator Work()
	{
		Debug.Log("đang làm việc");
		vehicleStatus = VehicleStatus.OnScene;
		selected = false;
		Debug.Log($"Status: {vehicleStatus}, Selected: {selected}");
		yield return new WaitForSeconds(timeSolve);
		Debug.Log("đã hoàn thành");
		vehicleStatus = VehicleStatus.OnPatrol;
		selected = false;
		Debug.Log($"Status: {vehicleStatus}, Selected: {selected}");
		yield return null;
		myCoroutine = StartCoroutine(Patrol());
	}

//---------------------------------------------------------------

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) 
	{
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;

			DrawPath();

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
	void DrawPath()
	{
    	lineRenderer.positionCount = path.Length + 1;

    	lineRenderer.SetPosition(0, transform.position);

    	for (int i = 0; i < path.Length; i++)
    	{
    		lineRenderer.SetPosition(i + 1, path[i]);
    	}
	}
//---------------------------------------------------------------

	IEnumerator FollowPath()
	{
		if(path == null || path.Length == 0)
		{
			yield break;
		}
		Vector3 currentWaypoint = path[0];
		while(true)
		{
			if(Vector3.Distance(transform.position, currentWaypoint) < 0.1f)
			{
				targetIndex++;
				if(targetIndex >= path.Length)
				{
					lineRenderer.positionCount = 0;
					path = null;
					if (vehicleStatus == VehicleStatus.OnRoute)
                    {
						vehicleStatus = VehicleStatus.OnPatrol;
						selected = false;
						myCoroutine = StartCoroutine(Patrol());
						Debug.Log($"Status: {vehicleStatus}, Selected: {selected}");
						Debug.Log("ok");
                    }
					yield break;
				}
				currentWaypoint = path[targetIndex];
				UpdateLine();
			}
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			lineRenderer.SetPosition(0, transform.position);
			yield return null;
		}
	}

//---------------------------------------------------------------

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}

	void UpdateLine()
	{
		if (path == null) return;
		int remain = path.Length - targetIndex;
		lineRenderer.positionCount = remain + 1;
		lineRenderer.SetPosition(0, transform.position);
		for (int i = 0; i < remain; i++)
		{
			lineRenderer.SetPosition(i + 1, path[targetIndex + i]);
		}
	}
}



// using UnityEngine;
// using System.Collections;

// public class Unit : MonoBehaviour {

// 	const float minPathUpdateTime = .2f;
// 	const float pathUpdateMoveThreshold = .5f;

// 	public Transform target;
// 	public float speed = 5;
// 	public float turnSpeed = 3;
// 	public float turnDst = 5;
// 	public float stoppingDst = 10;

// 	Path path;

// 	void Start() {
// 		StartCoroutine (UpdatePath ());
// 	}

// //---------------------------------------------------------------

// 	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
// 		if (pathSuccessful) {
// 			path = new Path(waypoints, transform.position, turnDst, stoppingDst);

// 			StopCoroutine("FollowPath");
// 			StartCoroutine("FollowPath");
// 		}
// 	}

// 	IEnumerator UpdatePath() {

// 		if (Time.timeSinceLevelLoad < .3f) {
// 			yield return new WaitForSeconds (.3f);
// 		}
// 		PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));

// 		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
// 		Vector3 targetPosOld = target.position;

// 		while (true) {
// 			yield return new WaitForSeconds (minPathUpdateTime);
// 			print (((target.position - targetPosOld).sqrMagnitude) + "    " + sqrMoveThreshold);
// 			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
// 				PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));
// 				targetPosOld = target.position;
// 			}
// 		}
// 	}

// 	IEnumerator FollowPath() {

// 		bool followingPath = true;
// 		int pathIndex = 0;
// 		transform.LookAt (path.lookPoints [0]);

// 		float speedPercent = 1;

// 		while (followingPath) {
// 			Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
// 			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
// 				if (pathIndex == path.finishLineIndex) {
// 					followingPath = false;
// 					break;
// 				} else {
// 					pathIndex++;
// 				}
// 			}

// 			if (followingPath) {

// 				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
// 					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
// 					if (speedPercent < 0.01f) {
// 						followingPath = false;
// 					}
// 				}

// 				Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
// 				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
// 				transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
// 			}

// 			yield return null;

// 		}
// 	}

// 	public void OnDrawGizmos() {
// 		if (path != null) {
// 			path.DrawWithGizmos ();
// 		}
// 	}
// }
