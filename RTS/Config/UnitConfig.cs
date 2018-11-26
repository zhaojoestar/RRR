using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitConfig
{
    public int ID;
    public string Resource;
    public int[] Attribute;
    public int Skill;
    public int SkillCD;
    private static Dictionary<int, UnitConfig> _dic = null;
    public static Dictionary<int, UnitConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, UnitConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("UnitConfig");
                while (reader.Read())
                {
                    var e = new UnitConfig();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Resource = reader.GetString(reader.GetOrdinal("Resource"));
                    e.Attribute = Array.ConvertAll<string, int>(reader.GetString(reader.GetOrdinal("Attribute")).Split(';'), (string s) => { return int.Parse(s); });
                    e.Skill = reader.GetInt16(reader.GetOrdinal("Skill"));
                    e.SkillCD = reader.GetInt16(reader.GetOrdinal("SkillCD"));
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static UnitConfig Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("UnitConfig cannot find " + id);
            return null;
        }
    }
}
