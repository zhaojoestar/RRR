using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardConfig
{
    public int ID;
    public string Texture;
    public int Type;
    public int Value;
    private static Dictionary<int, CardConfig> _dic = null;
    public static Dictionary<int, CardConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, CardConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("CardConfig");
                while (reader.Read())
                {
                    var e = new CardConfig();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Texture = reader.GetString(reader.GetOrdinal("Texture"));
                    e.Type = reader.GetInt16(reader.GetOrdinal("Type"));
                    e.Value = reader.GetInt16(reader.GetOrdinal("Value"));
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static CardConfig Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("CardConfig cannot find " + id);
            return null;
        }
    }
}
