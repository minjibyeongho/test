//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;

//namespace Masco.Display.ILSValidator.Client.Common
//{
//    //https://blog.tonysneed.com/2011/01/25/type-safe-two-way-data-binding-with-inotifypropertychanged-3/
//    //ModelBase<ILSValidatorVM>
//    public class ILSValidatorVM : 
//    {
//        public int CHECKLIST_PK {get;set;}

//        private DateTime _CreateDT;
//        public DateTime CreateDT { get { return _CreateDT; } set { _CreateDT = value; NotifyPropertyChanged(m => m.CreateDT); } }

//        private DateTime _UpdateDT;
//        public DateTime UpdateDT { get { return _UpdateDT; } set { _UpdateDT = value; NotifyPropertyChanged(m => m.UpdateDT); } }

//        private string _ReqUserName;
//        public string ReqUserName { get { return _ReqUserName; } set { _ReqUserName = value; NotifyPropertyChanged(m => m.ReqUserName); } }

//        private string _UpdateUserName ;
//        public string UpdateUserName { get{ return _UpdateUserName ; } set { _UpdateUserName  = value; NotifyPropertyChanged(m => m.UpdateUserName); } }

//        public string ReqType { get; set; }

//        public string _ILSType;
//        public string ILSType { get { return _ILSType; } set { _ILSType = value; NotifyPropertyChanged(m => m.ILSType); } }

//        public string _MainCode;
//        public string MainCode { get { return _MainCode; } set { _MainCode = value; NotifyPropertyChanged(m => m.MainCode); } }

//        public string _Name;
//        public string Name { get { return _Name; } set { _Name = value; NotifyPropertyChanged(m => m.Name); } }

//        public bool _IsName;
//        public bool IsName { get { return _IsName; } set { _IsName = value; NotifyPropertyChanged(m => m.IsName); } }

//        public int _MapId;
//        public int MapId { get { return _MapId; } set { _MapId = value; NotifyPropertyChanged(m => m.MapId); } }

//        public int _NodeId;
//        public int NodeId { get { return _NodeId; } set { _NodeId = value; NotifyPropertyChanged(m => m.NodeId); } }

//        public string _MMSLonLat;
//        public string MMSLonLat { get { return _MMSLonLat; } set { _MMSLonLat = value; NotifyPropertyChanged(m => m.MMSLonLat); } }

//        public Int16 _LaneCount;
//        public Int16 LaneCount { get { return _LaneCount; } set { _LaneCount = value; NotifyPropertyChanged(m => m.LaneCount); } }

//        public string _LaneName;
//        public string LaneName { get { return _LaneName; } set { _LaneName = value; NotifyPropertyChanged(m => m.LaneName); } }

//        public Int16 _ImageCount;
//        public Int16 ImageCount { get { return _ImageCount; } set { _ImageCount = value; NotifyPropertyChanged(m => m.ImageCount); } }

//        public string _ImageName;
//        public string ImageName { get { return _ImageName; } set { _ImageName = value; NotifyPropertyChanged(m => m.ImageName); } }

//        public string _BottomName;
//        public string BottomName { get { return _BottomName; } set { _BottomName = value; NotifyPropertyChanged(m => m.BottomName); } }

//        public string _ImageRequest;
//        public string ImageRequest { get { return _ImageRequest; } set { _ImageRequest = value; NotifyPropertyChanged(m => m.ImageRequest); } }

//        public int _CI_PK_BASEMAP;
//        public int CI_PK_BASEMAP { get { return _CI_PK_BASEMAP; } set { _CI_PK_BASEMAP = value; NotifyPropertyChanged(m => m.CI_PK_BASEMAP); } }

//        public int _CI_PK_REFERENCE;
//        public int CI_PK_REFERENCE { get { return _CI_PK_REFERENCE; } set { _CI_PK_REFERENCE = value; NotifyPropertyChanged(m => m.CI_PK_REFERENCE); } }

//        public string _ApplyAMTeam1;
//        public string ApplyAMTeam1 { get { return _ApplyAMTeam1; } set { _ApplyAMTeam1 = value; NotifyPropertyChanged(m => m.ApplyAMTeam1); } }

//        public string _ApplyAMTeam2;
//        public string ApplyAMTeam2 { get { return _ApplyAMTeam2; } set { _ApplyAMTeam2 = value; NotifyPropertyChanged(m => m.ApplyAMTeam2); } }

//        public string _ApplyAMTeam3;
//        public string ApplyAMTeam3 { get { return _ApplyAMTeam3; } set { _ApplyAMTeam3 = value; NotifyPropertyChanged(m => m.ApplyAMTeam3); } }

//        public string _ApplyOEMTeam1;
//        public string ApplyOEMTeam1 { get { return _ApplyOEMTeam1; } set { _ApplyOEMTeam1 = value; NotifyPropertyChanged(m => m.ApplyOEMTeam1); } }

//        public string _ApplyOEMTeam2;
//        public string ApplyOEMTeam2 { get { return _ApplyOEMTeam2; } set { _ApplyOEMTeam2 = value; NotifyPropertyChanged(m => m.ApplyOEMTeam2); } }

//        public string _ApplyOEMTeam3;
//        public string ApplyOEMTeam3 { get { return _ApplyOEMTeam3; } set { _ApplyOEMTeam3 = value; NotifyPropertyChanged(m => m.ApplyOEMTeam3); } }

//        public string _ApplyOBNTeam1;
//        public string ApplyOBNTeam1 { get { return _ApplyOBNTeam1; } set { _ApplyOBNTeam1 = value; NotifyPropertyChanged(m => m.ApplyOBNTeam1); } }

//        public string _ApplyOBNTeam2;
//        public string ApplyOBNTeam2 { get { return _ApplyOBNTeam2; } set { _ApplyOBNTeam2 = value; NotifyPropertyChanged(m => m.ApplyOBNTeam2); } }

//        public string _ApplyOBNTeam3;
//        public string ApplyOBNTeam3 { get { return _ApplyOBNTeam3; } set { _ApplyOBNTeam3 = value; NotifyPropertyChanged(m => m.ApplyOBNTeam3); } }

//        public int _DESC_PK;
//        public int DESC_PK { get { return _DESC_PK; } set { _DESC_PK = value; NotifyPropertyChanged(m => m.DESC_PK); } }

//        public string _Description;
//        public string Description { get { return _Description; } set { _Description = value; NotifyPropertyChanged(m => m.Description); } }


//        internal static ILSValidatorVM Create(ServiceReferenceCheckList.CHECKLIST x)
//        {
//            var item = new ILSValidatorVM();
//            Helper.CopyProperties(x, item);
//            return item;
//        }

//        internal static ServiceReferenceCheckList.CHECKLIST CreateEntity(ILSValidatorVM x)
//        {
//            var item = new ServiceReferenceCheckList.CHECKLIST();
//            Helper.CopyProperties(x, item);
//            return item;
//        }
//    }
//}
