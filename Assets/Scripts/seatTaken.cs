using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.SceneManagement;

public class seatTaken : MonoBehaviour {

	private string returnValue, filePath;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		filePath = Application.persistentDataPath + "/data.db";
		returnValue = checkSeatTaken ("URI=file://" + filePath);

		if ( returnValue != "") {
			gameObject.SetActive (true);
			transform.name = returnValue;
		} 
	}

	// Update is called once per frame
	void Update () {
		
	}

	private string checkSeatTaken(string connectionstring){
		string query = "SELECT Name FROM seats WHERE ID = '" + transform.name + "'";
		string returnText = "";
		//connecting to database
		using (IDbConnection db = new SqliteConnection (connectionstring)) {
			db.Open ();

			using (IDbCommand c = db.CreateCommand ()) {
				c.CommandText = query;

				using (IDataReader reader = c.ExecuteReader ()) {
					while (reader.Read ()) {
						returnText += reader.GetString (0);
					}
				}
			}
		}
		return returnText;
	}
}
