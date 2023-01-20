using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;


public class DBDemo : MonoBehaviour
{
    private string dbIzena = "URI=file:Db/jolasaDB.db"; //jolasaren Db karpetan

    public static DBDemo instance;

    public DBDemo(){}
    public DBDemo(String name, float puntuazioa)
    {
        AddNewPartida(name, puntuazioa); //Partida berria(erabiltzailea, puntuazioa)
    }
    
    void Start()
    {
        instance = this;
        //DisplayLangileak();
    }

    public void DisplayPartidak(){
        using (var connection = new SqliteConnection(dbIzena))
        {
            connection.Open();

            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT * FROM Partida;";

                using(IDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Debug.Log("Partida: " + reader["id"] + "," + reader["langilea"] + "," + reader["puntuazioa"] + "," + reader["data"] );  //consolan bistaratuko da
                    }
                    reader.Close();
                }
            }   
            connection.Close();
        }
        
    }

    public void AddNewPartida(string erabiltzailea, float puntuazioa){
        
        using (var connection = new SqliteConnection(dbIzena))
        {
            connection.Open();

            using (IDbCommand dbcmd = connection.CreateCommand()){
                IDbDataParameter param = dbcmd.CreateParameter();
                DateTime localDate = DateTime.Now;

                string sql = "INSERT INTO Partida (langilea, puntuazioa, data) VALUES (@param1,@param2,@param3);";
                dbcmd.CommandText = sql;
                dbcmd.Parameters.Add(new SqliteParameter("@param1", erabiltzailea));
                dbcmd.Parameters.Add(new SqliteParameter("@param2", puntuazioa));
                dbcmd.Parameters.Add(new SqliteParameter("@param3", localDate));
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
            }
            connection.Close();
        }   
    }

    public void DisplayLangileak(){
        
        using (var connection = new SqliteConnection(dbIzena))
        {
            connection.Open();

            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT * FROM Langilea;";

                using(IDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Debug.Log("Langilea: " + reader["erabiltzailea"] + "," + reader["emaila"] + "," + reader["izena"] + "," + reader["jaiotzeData"] + "," + reader["taldea"]); //consolan bistaratuko da
                    }
                    reader.Close();
                }
            }   
            connection.Close();
        }
    }

    public bool VerifyUser(String user){
        bool result = false;
        using (var connection = new SqliteConnection(dbIzena))
        {
            connection.Open();

            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT EXISTS(SELECT erabiltzailea from Langilea WHERE erabiltzailea = @param1) as 'result';";
                command.Parameters.Add(new SqliteParameter("@param1", user));

                using(IDataReader reader = command.ExecuteReader()){
                    int i = Convert.ToInt32(reader.GetValue(0));
                    if(i == 1){
                        result = true;
                    }
                    reader.Close();
                }
            }   
            connection.Close();
        }
        return result;
    }
}
