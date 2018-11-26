using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MiracleConfig
{
    public int ID;
    public string Resource;
    public int Effect;
    private static Dictionary<int, MiracleConfig> _dic = null;
    public static Dictionary<int, MiracleConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, MiracleConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("MiracleConfig");
                while (reader.Read())
                {
                    var e = new MiracleConfig();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Resource = reader.GetString(reader.GetOrdinal("Resource"));
                    e.Effect = reader.GetInt16(reader.GetOrdinal("Effect"));
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static MiracleConfig Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("MiracleConfig cannot find " + id);
            return null;
        }
    }
}
