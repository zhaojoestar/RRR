using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemAI
{
    public int ID;
    public string Name;
    public int Core;
    public List<int> Deck;
    private static Dictionary<int, SystemAI> _dic = null;
    public static Dictionary<int, SystemAI> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, SystemAI>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("SystemAI");
                while (reader.Read())
                {
                    var e = new SystemAI();
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

    public static SystemAI Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("SystemAI cannot find " + id);
            return null;
        }
    }
}
