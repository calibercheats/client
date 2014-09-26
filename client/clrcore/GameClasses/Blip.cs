using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenFX.Core
{
    public sealed class Blip : HandleObject
    {
        public float Alpha
        {
            get
            {
                return Function.Call<float>(Natives.GET_BLIP_ALPHA, m_handle);
            }
            set
            {
                Function.Call(Natives.CHANGE_BLIP_ALPHA, m_handle, value);
            }
        }

        public BlipColor Color
        {
            get
            {
                Pointer pColor = typeof(int);

                Function.Call(Natives.GET_BLIP_COLOUR, m_handle, pColor);

                return (BlipColor)(int)pColor;
            }
            set
            {
                Function.Call(Natives.CHANGE_BLIP_COLOUR, m_handle, (int)value);
            }
        }

        public BlipIcon Icon
        {
            get
            {
                return (BlipIcon)Function.Call<uint>(Natives.GET_BLIP_SPRITE, m_handle);
            }
            set
            {
                Function.Call(Natives.CHANGE_BLIP_SPRITE, m_handle, (int)value);
            }
        }

        public string Name
        {
            get
            {
                //odd, Natives doesn't have that function
                return null;//Function.Call<string>(Natives.GET_BLIP_NAME);
            }
            set
            {
                Function.Call(Natives.CHANGE_BLIP_NAME_FROM_ASCII, this.Handle, value);
            }
        }

        public BlipType Type
        {
            get
            {
                return (BlipType)Function.Call<uint>(Natives.GET_BLIP_INFO_ID_TYPE, m_handle);
            }
        }

        public bool ShowOnlyWhenNear
        {
            get
            {
                return Function.Call<bool>(Natives.IS_BLIP_SHORT_RANGE, m_handle);
            }
            set
            {
                Function.Call(Natives.SET_BLIP_AS_SHORT_RANGE, m_handle, value);
            }
        }

        public BlipDisplay Display
        {
            get
            {
                return (BlipDisplay)Function.Call<uint>(Natives.GET_BLIP_INFO_ID_DISPLAY, m_handle);
            }
            set
            {
                Function.Call(Natives.CHANGE_BLIP_DISPLAY, m_handle, (int)Display);
            }
        }

        /*public static Blip AddBlip(Pickup pickup)
        {

        }*/

        public bool Friendly
        {
            set
            {
                Function.Call(Natives.SET_BLIP_AS_FRIENDLY, m_handle, value);
            }
        }

        public float Scale
        {
            set
            {
                Function.Call(Natives.CHANGE_BLIP_SCALE, m_handle, value);
            }
        }

        public int Priority
        {
            set
            {
                Function.Call(Natives.CHANGE_BLIP_PRIORITY, m_handle, value);
            }
        }

        public bool RouteActive
        {
            set
            {
                Function.Call(Natives.SET_ROUTE, m_handle, value);
            }
        }

        internal override void SetHandle(int handle)
        {
            if (m_handle != -1)
                ObjectCache<Blip>.Remove(this);

            m_handle = handle;

            ObjectCache<Blip>.Add(this);
        }

        public void Remove()
        {
            Display = BlipDisplay.Hidden;
            Function.Call(Natives.REMOVE_BLIP, m_handle);
        }

        public bool InternalCheckExists()
        {
            if (m_handle == 0)
                return false;

            return Function.Call<bool>(Natives.DOES_BLIP_EXIST, m_handle);
        }

        public Entity GetAttachedItem()
        {
            int id;
            switch (Type)
            {
                case BlipType.Vehicle:
                    id = Function.Call<int>(Natives.GET_BLIP_INFO_ID_CAR_INDEX, m_handle);
                    if (!Function.Call<bool>(Natives.DOES_VEHICLE_EXIST, id)) 
                        return null;

                    return ObjectCache<Vehicle>.Get(id);

                case BlipType.Ped:
                    id = Function.Call<int>(Natives.GET_BLIP_INFO_ID_PED_INDEX, m_handle);
                    if (!Function.Call<bool>(Natives.DOES_CHAR_EXIST, id))
                        return null;

                    return ObjectCache<Ped>.Get(id);

                /*case BlipType.Pickup:
                    id = Function.Call<uint>(Natives.GET_BLIP_INFO_ID_PICKUP_INDEX, m_handle);
                    if (!Function.Call<bool>(Natives.DOES_PICKUP_EXIST, id))
                        return ObjectCache<Pickup>.Get(id);*/
            }

            return null;
        }

        /*public void SetColorRGB(Color color)
        {

        }*/

        public static Blip AddBlip(Ped ped)
        {
            Pointer blipPtr = typeof(int);

            Function.Call(Natives.ADD_BLIP_FOR_CHAR, ped.Handle, blipPtr);

            return ObjectCache<Blip>.Get((int)blipPtr);
        }

        public static Blip AddBlip(Vector3 origin)
        {
            Pointer blipPtr = typeof(int);

            Function.Call(Natives.ADD_BLIP_FOR_COORD, origin.X, origin.Y, origin.Z, blipPtr);

            return ObjectCache<Blip>.Get((int)blipPtr);
        }

        public static Blip AddBlip(Vehicle vehicle)
        {
            Pointer blipPtr = typeof(int);

            Function.Call(Natives.ADD_BLIP_FOR_CAR, vehicle.Handle, blipPtr);

            return ObjectCache<Blip>.Get((int)blipPtr);
        }
    }

    public enum BlipColor
    {
        White = 0,
        DarkRed = 1,
        DarkGreen = 2,
        Cyan = 3,
        Grey = 4,
        Yellow = 5,
        Orange = 6,
        Purple = 7,
        Green = 8,
        Red = 9,
        LightRed = 10,
        LightOrange = 11,
        DarkTurquoise = 12,
        Turquoise = 13,
        LightYellow = 14
    }

    public enum BlipDisplay
    {
        Hidden = 0,
        ArrowOnly = 1,
        MapOnly = 2,
        ArrowAndMap = 4,
    }

    public enum BlipIcon
    {
        Misc_Destination,
        Misc_Destination1,
        Misc_Destination2,
        Misc_Objective,
        Misc_Objective4,
        Misc_Objective5,
        Misc_Player,
        Misc_North,
        Misc_Waypoint,
        Weapon_Pistol,
        Weapon_Shotgun,
        Weapon_SMG,
        Weapon_Rifle,
        Weapon_Rocket,
        Weapon_Grenade,
        Weapon_Molotov,
        Weapon_Sniper,
        Weapon_BaseballBat,
        Weapon_Knife,
        Pickup_Health,
        Pickup_Armor,
        Building_BurgerShot,
        Building_CluckinBell,
        Person_Vlad,
        Building_Internet,
        Person_Manny,
        Person_LittleJacob,
        Person_Roman,
        Person_Faustin,
        Building_Safehouse,
        Misc_TaxiRank,
        Person_Bernie,
        Person_Brucie,
        Person_Unknown,
        Person_Dwayne,
        Person_Elizabeta,
        Person_Gambetti,
        Person_JimmyPegorino,
        Person_Derrick,
        Person_Francis,
        Person_Gerry,
        Person_Katie,
        Person_Packie,
        Person_PhilBell,
        Person_PlayboyX,
        Person_RayBoccino,
        Misc_8BALL,
        Activity_Bar,
        Activity_BoatTour,
        Activity_Bowling,
        Building_ClothShop,
        Activity_Club,
        Activity_Darts,
        Person_Dwayne_Red,
        Activity_Date,
        Person_PlayboyX_Red,
        Activity_HeliTour,
        Activity_Restaurant,
        Building_TrainStation,
        Building_WeaponShop,
        Building_PoliceStation,
        Building_FireStation,
        Building_Hospital,
        Person_Male,
        Person_Female,
        Misc_FinishLine,
        Activity_StripClub,
        Misc_ConsoleGame,
        Misc_CopCar,
        Person_Dimitri,
        Activity_ComedyClub,
        Activity_CabaretClub,
        Misc_Ransom,
        Misc_CopHeli,
        Person_Michelle,
        Building_PayNSpray,
        Person_Assassin,
        Misc_Revenge,
        Misc_Deal,
        Building_Garage,
        Person_Lawyer,
        Misc_Trophy,
        Misc_MultiplayerTutorial,
        Building_TrainStation3,
        Building_TrainStation8,
        Building_TrainStationA,
        Building_TrainStationB,
        Building_TrainStationC,
        Building_TrainStationE,
        Building_TrainStationJ,
        Building_TrainStationK,
        Building_CarWash,
        Person_UnitedLibertyPaper,
        Misc_Boss,
        Misc_Base
    }

    public enum BlipType
    {
        Vehicle = 1,
        Ped = 2,
        Object = 3,
        Coordinate = 4,
        Contact = 5,
        Pickup = 6,
        Unknown = 7,
        Pickup2 = 8,
    }
}
