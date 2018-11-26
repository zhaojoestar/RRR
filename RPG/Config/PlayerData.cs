using System;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class PlayerData : MonoBehaviour
{
    [HideInInspector]
    public int ID;
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public int Core;
    [HideInInspector]
    public List<int> Deck;
    private static Dictionary<int, PlayerData> _dic = null;
    public static Dictionary<int, PlayerData> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, PlayerData>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("PlayerData");
                while (reader.Read())
                {
                    PlayerData e = new PlayerData();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Name = reader.GetString(reader.GetOrdinal("Name"));
                    e.Core = reader.GetInt16(reader.GetOrdinal("CoreLevel"));
                    int[] array = Array.ConvertAll<string, int>(reader.GetString(reader.GetOrdinal("Deck")).Split(';'), (string s) => { return int.Parse(s); });
                    e.Deck = new List<int>(array);
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static PlayerData Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("PlayerData cannot find " + id);
            return null;
        }
    }
}
