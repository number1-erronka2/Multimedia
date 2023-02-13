using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;


public class DBDemo : MonoBehaviour
{
    private string dbIzena = "URI=file:Db/jolasaDB.db"; //jolasaren Db karpetan
    private bool sqlExecuted = false;
    public static DBDemo instance;

    public DBDemo(){}
    public DBDemo(String name, float puntuazioa)
    {
        AddNewPartida(name, puntuazioa); //Partida berria(erabiltzailea, puntuazioa)
    }
    
    void Start()
    {
        instance = this;
        //if file not exits
        if(!File.Exists("Db/jolasaDB.db")){
            File.Create("Db/jolasaDB.db");
            Debug.Log("DBa sortuta");
        }
    }

    public void sortuDB(){
        
        if(!sqlExecuted){
            using(var connection = new SqliteConnection(dbIzena))
            {
                connection.Open();

                using (IDbCommand dbcmd = connection.CreateCommand()){
                    string sql = "CREATE TABLE IF NOT EXISTS Langilea ( emaila TEXT NOT NULL UNIQUE, izena TEXT, erabiltzailea TEXT UNIQUE, jaiotzeData DATE, taldea INTEGER NOT NULL, PRIMARY KEY(erabiltzailea));";
                    dbcmd.CommandText = sql;
                    dbcmd.ExecuteNonQuery();
                    sql = "CREATE TABLE IF NOT EXISTS Partida (id INTEGER NOT NULL UNIQUE, langilea Langilea NOT NULL, puntuazioa FLOAT NOT NULL, data INTEGER NOT NULL, FOREIGN KEY(langilea) REFERENCES Langilea(erabiltzailea), PRIMARY KEY(id AUTOINCREMENT));" ;
                    dbcmd.CommandText = sql;
                    dbcmd.ExecuteNonQuery();

                    try {
                        sql = "INSERT INTO Langilea Values ('erlantz@uni.eus', 'Erlantz','ErlantzG', '2003-07-08', 4 ); INSERT INTO Langilea Values ('markel@uni.eus', 'Markel','MarkelUNI', '2000-10-23', 4 );" +
                            "INSERT INTO Langilea Values ('peruM@gmail.com', 'Peru', 'PeruM', '2002-07-31', 4 ); INSERT INTO Langilea Values ('raul@uni.eus', 'Raul','RaulUNI', '2001-07-29', 4 );"  +
                            "INSERT INTO Langilea Values ('alain@uni.eus', 'Alain', 'alain', '2000-09-19', 2 ); INSERT INTO Langilea Values ('otero.haritz@uni.eus', 'Haritz','haritz', '2003-12-08', 2 );"  +
                            "INSERT INTO Langilea Values ('arginzoniz.joseba@uni.eus', 'Joseba', 'joseba', '1971-11-06', 2 ); INSERT INTO Langilea Values ('arceredillo.adrian@uni.eus', 'Adrian','adrian', '1998-07-29', 2 );" + 
                            "INSERT INTO Langilea Values ('aitzol@uni.eus', 'Aitzol', 'aitzol', '2000-11-25', 1 ); INSERT INTO Langilea Values ('hodei@uni.eus', 'Hodei','hodei', '2003-10-18', 1 );" + 
                            "INSERT INTO Langilea Values ('markelR@uni.eus', 'Markel', 'MarkelR', '2003-07-31', 1 ); INSERT INTO Langilea Values ('gallastegui.iker@uni.eus', 'Iker','iker', '2000-09-10', 1 );" +
                            "INSERT INTO Langilea Values ('iker@uni.eus', 'Iker', 'IkerJa', '2000-09-10', 3 ); INSERT INTO Langilea Values ('karmele@uni.eus', 'Karmele','Kar21', '2001-07-01', 3 );" +
                            "INSERT INTO Langilea Values ('peru@uni.eus', 'Peru', 'peruUNI', '2001-02-21', 3 ); INSERT INTO Langilea Values ('arkaitz@uni.eus', 'Arkaitz','Arkaitz', '2003-03-23', 3 );" ;
                        dbcmd.CommandText = sql;
                        dbcmd.ExecuteNonQuery();
                    } catch (Exception e) {
                        Debug.Log("Insert error: Baloreak sartuta dagoz dagoeneko. " + e);
                    }

                    dbcmd.Dispose();
                }
                connection.Close();
                sqlExecuted = true;
            }
        }
    }



    public List<HighScore> GetPartidak(){
        List<HighScore> highScores = new List<HighScore>();
        try{
            using (var connection = new SqliteConnection(dbIzena))
            {
                connection.Open();

                using (var command = connection.CreateCommand()){
                    command.CommandText = "SELECT * FROM Partida ORDER BY puntuazioa DESC;";

                    using(IDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            highScores.Add(new HighScore(Convert.ToInt32(reader["id"]), reader["langilea"].ToString(), Convert.ToInt32(reader["puntuazioa"]), Convert.ToInt32(reader["data"])));
                        }
                        reader.Close();
                    }
                }   
                connection.Close();
            }
        }catch(Exception e){
            Debug.Log(e);
        }
        return highScores;
        
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
