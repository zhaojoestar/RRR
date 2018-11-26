using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicConfig
{
    public int ID;
    public string Resource;
    public int SideType;
    public int RangeType;
    public int Connect;
    public int Frequnce;
    public int Duration;
    public int Delay;
    public int Period;
    public int EffectType;
    public int EffectValue;
    private static Dictionary<int, MagicConfig> _dic = null;
    public static Dictionary<int, MagicConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, MagicConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("MagicConfig");
                while (reader.Read())
                {
                    var e = new MagicConfig();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Resource = reader.GetString(reader.GetOrdinal("Resource"));
                    e.SideType = reader.GetInt16(reader.GetOrdinal("SideType"));
                    e.RangeType = reader.GetInt16(reader.GetOrdinal("RangeType"));
                    e.Connect = reader.GetInt16(reader.GetOrdinal("Connect"));
                    e.Frequnce = reader.GetInt16(reader.GetOrdinal("Frequnce"));
                    e.Duration = reader.GetInt16(reader.GetOrdinal("Duration"));
                    e.Delay = reader.GetInt16(reader.GetOrdinal("Delay"));
                    e.Period = reader.GetInt16(reader.GetOrdinal("Period"));
                    e.EffectType = reader.GetInt16(reader.GetOrdinal("EffectType"));
                    e.EffectValue = reader.GetInt16(reader.GetOrdinal("EffectValue"));
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static MagicConfig Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("MagicConfig cannot find " + id);
            return null;
        }
    }
}
