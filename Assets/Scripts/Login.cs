using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public GameObject Username, Password, Seat, DisplayText;
	private string username, password, seat, filePath;

	// Use this for initialization
	void Start () {
		filePath = Application.persistentDataPath + "/data.db";

		if (!File.Exists (filePath)) {
			var loadDB = new WWW ("jar:file://" + Application.dataPath + "!/assets/data.db");
			while (!loadDB.isDone) {}
			Debug.Log(filePath);
			Debug.Log ("jar:file://" + Application.dataPath + "/data.db");
			File.WriteAllBytes (filePath, loadDB.bytes);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// updating username, password and seat string every frame
		username = Username.GetComponent<InputField> ().text;
		password = Password.GetComponent<InputField> ().text;
		seat = Seat.GetComponent<Dropdown>().options[Seat.GetComponent<Dropdown> ().value].text;
	}

	public void login(){
		// database path
		// checking if user is valid
		if (checkUser ("URI=file://"+filePath, username, password, seat) == true) {
			// display successful login message
			DisplayText.GetComponent<Text>().text = "Login Successul";

			// Update Seat Information
			if (seat == "Seat 1") {
				if (checkSeat ("URI=file://" + filePath, "1") == false) {
					markSeat ("URI=file://" + filePath, "1");

					// load classroom
					SceneManager.LoadScene ("Classroom");
				} else {
					DisplayText.GetComponent<Text> ().text = "Seat Taken";
				}
			} else if (seat == "Seat 2") {
				if (checkSeat ("URI=file://" + filePath, "2") == false) {
					markSeat ("URI=file://" + filePath, "2");

					// load classroom
					SceneManager.LoadScene ("Classroom");
				} else {
					DisplayText.GetComponent<Text> ().text = "Seat Taken";
				}
			} else {
				// load classroom
				SceneManager.LoadScene ("Classroom");
			}

		} else {
			DisplayText.GetComponent<Text>().text = "Invalid Username or Password";
		}
	}

	private bool checkUser(string connectionstring, string name, string key, string seat){
		string query = "SELECT Identifier FROM Users WHERE Username = '"+name+"' AND Password = '"+key+"'";

		//connecting to database
		using (IDbConnection db = new SqliteConnection (connectionstring)) {
			db.Open ();

			using (IDbCommand c = db.CreateCommand ()) {
				c.CommandText = query;

				using (IDataReader reader = c.ExecuteReader ()) {
					while (reader.Read ()) {
						if (seat == "Prof") {
							if (reader.GetString (0) == "Teacher") {
								Debug.Log ("Welcome Professor");
								return true;
							} 
						} else {
							if (reader.GetString (0) == "Student") {
								Debug.Log ("Mornin Student");
								return true;
							} 
						}
					}
				}
			}
		}
		return false;
	}

	private bool checkSeat(string connectionstring, string id){
		string query = "SELECT Name FROM seats WHERE ID = '"+id+"'";

		//connecting to database
		using (IDbConnection db = new SqliteConnection (connectionstring)) {
			db.Open ();

			using (IDbCommand c = db.CreateCommand ()) {
				c.CommandText = query;

				using (IDataReader reader = c.ExecuteReader ()) {
					while (reader.Read ()) {
						if(reader.GetString(0) != "")
							return true;
					}
				}
			}
		}
		return false;
	}

	private bool markSeat(string connectionstring, string id){
		string query = "UPDATE seats SET Name = '"+username+"' WHERE ID = '"+id+"'";

		//connecting to database
		using (IDbConnection db = new SqliteConnection (connectionstring)) {
			db.Open ();

			using (IDbCommand c = db.CreateCommand ()) {
				c.CommandText = query;

				c.ExecuteScalar ();
				db.Close ();
			}
		}
		return false;
	}
}
