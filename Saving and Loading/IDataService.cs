using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataService 
{
    bool SaveData<T>(string RelativePath, T saveData);
    T LoadData<T>(string RelativePath);
}
