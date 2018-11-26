using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffConfig
{
    public int ID;
    public string Resource;
    public string Root;
    public int Duration;
    public int Delay;
    public int Period;
    public int EffectType;
    public int ValueType;
    public int EffectValue;

    private static Dictionary<int, BuffConfig> _dic = null;
    public static Dictionary<int, BuffConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, BuffConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("BuffConfig");
                while (reader.Read())
                {
                    var e = new BuffConfig();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Resource = reader.GetString(reader.GetOrdinal("Resource"));
                    e.Root = reader.GetString(reader.GetOrdinal("Root"));
                    e.Duration = reader.GetInt16(reader.GetOrdinal("Duration"));
                    e.Delay = reader.GetInt16(reader.GetOrdinal("Delay"));
                    e.Period = reader.GetInt16(reader.GetOrdinal("Period"));
                    e.EffectType = reader.GetInt16(reader.GetOrdinal("EffectType"));
                    e.ValueType = reader.GetInt16(reader.GetOrdinal("ValueType"));
                    e.EffectValue = reader.GetInt16(reader.GetOrdinal("EffectValue"));
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static BuffConfig Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("BuffConfig cannot find " + id);
            return null;
        }
    }
}
