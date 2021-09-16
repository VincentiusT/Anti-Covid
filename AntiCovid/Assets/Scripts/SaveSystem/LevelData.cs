using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    int day, money;
    float awareness;

    #region data info bar
    private int totalCitizen;
    private int transmissionRate;
    private int sickPeoples;
    private int healthyPeoples;
    private int hospitalizedPeoples;
    private int vaksinedPeoples;
    private int vaksinedPeoples2;
    private int unvaccinatedPeoples;
    private int unvaccinatedPeoples2;
    private int deadPeoples;
    private int transmissionIncreaseRate;
    private int vaccineStock;

    public int TotalCitizen { get => totalCitizen; set => totalCitizen = value; }
    public int TransmissionRate { get => transmissionRate; set => transmissionRate = value; }
    public int SickPeoples { get => sickPeoples; set => sickPeoples = value; }
    public int HealthyPeoples { get => healthyPeoples; set => healthyPeoples = value; }
    public int HospitalizedPeoples { get => hospitalizedPeoples; set => hospitalizedPeoples = value; }
    public int VaksinedPeoples { get => vaksinedPeoples; set => vaksinedPeoples = value; }
    public int VaksinedPeoples2 { get => vaksinedPeoples2; set => vaksinedPeoples2 = value; }
    public int UnvaccinatedPeoples { get => unvaccinatedPeoples; set => unvaccinatedPeoples = value; }
    public int UnvaccinatedPeoples2 { get => unvaccinatedPeoples2; set => unvaccinatedPeoples2 = value; }
    public int DeadPeoples { get => deadPeoples; set => deadPeoples = value; }
    public int TransmissionIncreaseRate { get => transmissionIncreaseRate; set => transmissionIncreaseRate = value; }
    public int Day { get => day; set => day = value; }
    public int Money { get => money; set => money = value; }
    public float Awareness { get => awareness; set => awareness = value; }
    public int VaccineStock { get => vaccineStock; set => vaccineStock = value; }
    #endregion

    #region bangunan
    //private Hospital[] hospitals;
    //private Pharmacy[] pharmacies;
    //private Officer officer;
    //private VaksinPlace[] vaksinPlaces;
    public List<AmbulanceData> ambulanceDatas = new List<AmbulanceData>();
    public List<HospitalData> hospitalDatas = new List<HospitalData>();
    public OfficerData officerData;
    public List<PharmacyData> pharmacyDatas = new List<PharmacyData>();
    public List<VaccinePlaceData> vaccinePlaceDatas = new List<VaccinePlaceData>();

    //public Hospital[] Hospitals { get => hospitals; set => hospitals = value; }
    //public Pharmacy[] Pharmacies { get => pharmacies; set => pharmacies = value; }
    //public Officer Officer { get => officer; set => officer = value; }
    //public VaksinPlace[] VaksinPlaces { get => vaksinPlaces; set => vaksinPlaces = value; }
    #endregion
}
