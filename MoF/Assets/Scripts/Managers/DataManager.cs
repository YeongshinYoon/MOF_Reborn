using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
            }

            return instance;
        }
    }

    [Serializable]
    public class SaveData
    {
        public int level;

        public float cur_hp;

        public float max_hp;

        public float cur_mp;

        public float max_mp;

        public float cur_exp;

        public float max_exp;

        public int bagCount;

        public int[,] items;

        public int itemCount;
    }
    

    

    void Start()
    {
        
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Save/savedata.dat");

        SaveData data = new SaveData();

        //Data Allocation Start
        data.level = Player.MyInstance.MyLevel;
        data.cur_hp = Player.MyInstance.MyHealth.MyCurrentValue;
        data.max_hp = Player.MyInstance.MyHealth.MyMaxValue;
        data.cur_mp = Player.MyInstance.MyMana.MyCurrentValue;
        data.max_mp = Player.MyInstance.MyMana.MyMaxValue;
        data.cur_exp = Player.MyInstance.MyExp.MyCurrentValue;
        data.max_exp = Player.MyInstance.MyExp.MyMaxValue;
        //Data Allocation End

        formatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Save/savedata.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            SaveData data = (SaveData)formatter.Deserialize(file);


        }
    }
}
