using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	private bool _isDocket = false;

	public bool IsDocket {
		get { return _isDocket; }
		set { _isDocket = value; }
	}


}
