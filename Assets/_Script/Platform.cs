using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public LayerMask layer;

	public LayerMask GetLayerMask(){
		return layer;
	}
}
