using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {	

	public static GameManager gm;

	[SerializeField]
	private bool[] logicGroup = new bool[8];
	[SerializeField]
	private Color[] logicColors = new Color[8]{Color.white,Color.blue,Color.cyan,Color.gray,Color.green,Color.magenta,Color.red,Color.yellow};
	private List<LogicSetter> logicSetterList= new List<LogicSetter>();
	private List<LogicReciever> logicRecieverList= new List<LogicReciever>();


	public Tile[] tiles;

	private float countActiveAt;
	private int logEveryXsec = 3;
	// Use this for initialization
	void Awake () {
		GameManager.gm = this;
		countActiveAt = Time.time + logEveryXsec;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateSetters ();
		UpdateGetters ();


		if (Time.time >= countActiveAt) {
			countActiveAt += logEveryXsec;
			//GetActive ();
		}


	}

	private void GetActive (){
		int active = 0;

		foreach (Tile tile in tiles) {
		
			if (tile.isTileActive ())
				active++;
		
		}

		Debug.Log (active + "     " + (float)active/(float)tiles.Length);
	}
	private void UpdateSetters(){
		logicGroup = new bool[8];
		foreach (LogicSetter ls in logicSetterList) {
			if (ls.GetStatus() == true) {
				logicGroup [ls.GetGroup()] = true;
			}
		}
	}

	private void UpdateGetters (){
		foreach (LogicReciever lr in logicRecieverList) {
			lr.setLogicStatus (logicGroup [lr.GetGroup ()]);
		}
	}

	public void RegisterLogicSetter(LogicSetter ls){
		logicSetterList.Add (ls);
	}

	public void RegisterLogicReciever(LogicReciever lr){
		logicRecieverList.Add (lr);
	}





	public bool IsGroupActive(int logicGroup){
		if (logicGroup >= 0 && logicGroup < this.logicGroup.Length) {
			return this.logicGroup [logicGroup];
		}
		return false;
	}

	public Color GetColorForLGroup(int logicGroup){
		if (logicGroup >= 0 && logicGroup < this.logicColors.Length) {
			return this.logicColors [logicGroup];
		}
		return Color.black;
	}

}
