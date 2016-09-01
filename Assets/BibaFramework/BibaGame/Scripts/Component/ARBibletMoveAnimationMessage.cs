using UnityEngine;
using System.Collections;

public class ARBibletMoveAnimationMessage : MonoBehaviour {

	public ARBibletMove move;

	public void Chase() {
		move.Reset () ;
		move.ChaseTarget ();
	}
}
