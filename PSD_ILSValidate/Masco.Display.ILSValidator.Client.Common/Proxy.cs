using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class Proxy
    {
        #region Singleton
        private static Proxy _instance = null;
        public static Proxy Instance
        {
            get { return _instance ?? (_instance = new Proxy()); }
        }
        //private string _baseAddress = null;

        private Proxy()
        {
            //if (_baseAddress != null) return;

            //var settings = ConfigurationManager.GetSection("appSettings") as NameValueCollection;

            //if (settings != null)
            //{
            //    if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "DEBUG"))
            //    {
            //        _baseAddress = settings["BaseAddress_DEBUG"];
            //    }
            //    else
            //    {
            //        _baseAddress = settings["BaseAddress"];
            //    }
            //}
        }

        //public TInterface GetChannel<TInterface>(string changeName = null)
        //{
        //    var type = typeof(TInterface);
        //    var uri = new Uri(string.Format("{0}/{1}", _baseAddress, type.Name));
        //    var endpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(TInterface)), new BasicHttpBinding(), new EndpointAddress(uri));

        //    if (changeName != null)
        //    {
        //        BasicHttpBinding binding = new BasicHttpBinding();
        //        // Use double the default value
        //        binding.ReaderQuotas.MaxArrayLength = 2147483647;
        //        binding.MaxReceivedMessageSize = 2147483647;
        //        endpoint.Binding = binding;
        //    }

        //    var factory = new ChannelFactory<TInterface>(endpoint);
        //    var proxy = factory.CreateChannel();
        //    return proxy;
        //}



        #endregion

        #region Cache (List)
        //IList<RegularPlanVM> _regularPlanVMList = null;
        #endregion


        private ServiceReferenceCheckList.CheckListClient GetClient()
        {
           //var client = new ServiceReferenceCheckList.CheckListClient();
            var client = new ServiceReferenceCheckList.CheckListClient();
           return client;
        }


        public bool Validate()
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var result = proxy.Validate();
            proxy.Close();
            return result;
        }

        public int AddCheckItem(ILSValidatorVM vm)
        {
            //var item = new 
            return 0;
        }

        public IList<ILSValidatorVM> GetCheckListBy(Dictionary<string, string> filter)
        {
            //var proxy = new ServiceReferenceCheckList.CheckListClient();
            //var items = proxy.GetCheckList();
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var items = proxy.GetCheckListBy(filter);
            proxy.Close();

            var result = new List<ILSValidatorVM>();
            foreach (var x in items)
            {
                var item = ILSValidatorVM.Create(x);
                result.Add(item);
            }
            return result;
        }

        public int InsertCheckList(ILSValidatorVM vm)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var entity = ILSValidatorVM.CreateEntity(vm);
            var result = proxy.InsertCheckList(entity);
            proxy.Close();
            return result;
        }

        public int UpdateItem(ILSValidatorVM vm)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var entity = ILSValidatorVM.CreateEntity(vm);
            var result = proxy.UpdateCheckList(entity);
            return result;
        }

        //public string GetCheckListDescriptionBy(int descPk)
        //{
        //    var proxy = new ServiceReferenceCheckList.CheckListClient();
        //    var result = proxy.GetCheckListDescriptionBy(descPk);
        //    proxy.Close();
        //    if (result == null)
        //        return string.Empty;
        //    return result.DESCRIPTION;
        //}

        public int UploadCaptureFile(int captureImagePk, bool isBasemap, System.IO.Stream stream, out string errorMsg)
        {
            var proxy = new ServiceReferenceFileTransfer.FileTransferClient();
            errorMsg = string.Empty;
            proxy.UploadCaptureImage(captureImagePk, isBasemap, stream.Length, stream);
            return 0;
        }


        public Byte[] DownloadCaptureImageBy(int captureImagePk)
        {
            var proxy = new ServiceReferenceFileTransfer.FileTransferClient();

            string fileName = string.Empty;
            string childPath = string.Empty;
            long length = 0;

            var bytes = proxy.DownloadCaptureImageBy(captureImagePk, out childPath, out fileName, out length);
            return bytes;
        }

        public void DownloadCaptureImageByAsync(int captureImagePk, Action<Byte[]> OnCompleted)
        {
            var proxy = new ServiceReferenceFileTransfer.FileTransferClient();
            proxy.DownloadCaptureImageByAsync(captureImagePk);

            //object sender, ServiceReferenceFileTransfer.DownloadCaptureImageByCompletedEventArgs e
            proxy.DownloadCaptureImageByCompleted += (s, e) =>
            {
                OnCompleted.Invoke(e.Result);
            };
        }

        //public int AddCheckListDescription(int checkListPk, string desc)
        //{
        //    var proxy = new ServiceReferenceCheckList.CheckListClient();
        //    string errorMsg;
        //    bool isCompleted;
        //    int resultCheckListDescPk;
        //    var count = proxy.InsertCheckListDescription(0, checkListPk, desc, out errorMsg, out isCompleted, out resultCheckListDescPk);
        //    if (resultCheckListDescPk <= 0)
        //        return 0;

        //    return resultCheckListDescPk;
        //}

        //public int UpdateCheckListDescription(int checkListPk, int checkListDescPk, string desc)
        //{
        //    var proxy = new ServiceReferenceCheckList.CheckListClient();
        //    string errorMsg;
        //    bool isCompleted;
        //    int resultCheckListDescPk;
        //    var count = proxy.UpdateCheckListDescription(checkListDescPk, checkListPk, desc, out errorMsg, out isCompleted, out resultCheckListDescPk);
        //    if (resultCheckListDescPk <= 0)
        //        return 0;

        //    return resultCheckListDescPk;
        //}

        public IList<RequestVM> GetRequestList(int checkListPk, string reqType)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var items = proxy.GetRequestListBy(checkListPk, reqType);
            var result = new List<RequestVM>();
            foreach (var x in items)
            {
                var item = new RequestVM();
                Helper.CopyProperties(x, item);
                result.Add(item);
            }
            return result;
        }

        public IList<LaneInfoVM> GetLaneInfoListBy(int checkListPk)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var items = proxy.GetLaneInfoListBy(checkListPk);
            var result = new List<LaneInfoVM>();
            foreach (var x in items)
            {
                var item = new LaneInfoVM();
                Helper.CopyProperties(x, item);
                result.Add(item);
            }
            return result;
        }

        public int InsertLaneInfo(LaneInfoVM _vm)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var entity = LaneInfoVM.CreateEntity(_vm);
            var result = proxy.InsertLaneInfo(entity);
            proxy.Close();
            return result;
        }

        public int UpdateLaneInfo(LaneInfoVM _vm)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var entity = new ServiceReferenceCheckList.LANEINFO();
            Common.Helper.CopyProperties(_vm, entity);
            var result = proxy.UpdateLaneInfo(entity);
            proxy.Close();
            return result;
        }
        public int DeleteLaneInfoBy(int landInfoPk)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            // landInfoPk
            var result = proxy.DeleteLaneInfoBy(landInfoPk);
            proxy.Close();

            return result;
        }

        public int InsertRequest(RequestVM _vm)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var entity = RequestVM.CreateEntity(_vm);
            var result = proxy.InsertRequest(entity);
            proxy.Close();
            return result;
        }
        public int UpdateRequest(RequestVM _vm)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var entity = new ServiceReferenceCheckList.REQUEST();
            Common.Helper.CopyProperties(_vm, entity);
            var result = proxy.UpdateRequest(entity);
            proxy.Close();
            return result;
        }

        public RequestVM GetRequestBy(int checklistPk, int idx)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var item = proxy.GetRequestBy(checklistPk, idx);
            proxy.Close();

            if (item == null)
                return null;

            var result = new RequestVM();
            Common.Helper.CopyProperties(item, result);
            
            return result;
        }

        public int DeleteRequestBy(int checkListPk, int idx)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var resultIdx = proxy.DeleteRequestBy(checkListPk, idx);
            proxy.Close();

            return resultIdx ;
        }

        public int GetImagePkBy(int checkListPk, bool isBasemap)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var imagPk = proxy.GetImagePkBy(checkListPk, isBasemap);
            proxy.Close();

            return imagPk;
        }

        public int DeleteImageIsUseBy(int imagePk)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var imagPk = proxy.UpdateImageIsUse(imagePk, false);
            proxy.Close();

            return imagPk;
        }

        public int InsertMemo(int checkListPk, string remarks)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var result = proxy.InsertMemo(checkListPk, remarks);
            proxy.Close();

            return result;
        }

        public string GetMemoBy(int checkListPk)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var remarks = proxy.GetMemoBy(checkListPk);
            proxy.Close();

            return remarks;
        }


        public bool UserCheck(UserVm userVm)
        {
            var proxy = new ServiceReferenceCheckList.CheckListClient();
            var entity = new ServiceReferenceCheckList.USER();
            Common.Helper.CopyProperties(userVm, entity);
            var remarks = proxy.UserCheck(entity);
            proxy.Close();

            return remarks;
        }
    }
}
