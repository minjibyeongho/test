﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.17929
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.masco.co.kr/ILSValidator", ConfigurationName="ServiceReferenceFileTransfer.IFileTransfer")]
    public interface IFileTransfer {
        
        // CODEGEN: RemoteFileInfo 메시지의 래퍼 이름(RemoteFileInfo)이 기본값(UploadCaptureImage)과 일치하지 않으므로 메시지 계약을 생성합니다.
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.masco.co.kr/ILSValidator/IFileTransfer/UploadCaptureImage")]
        void UploadCaptureImage(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo request);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://www.masco.co.kr/ILSValidator/IFileTransfer/UploadCaptureImage")]
        System.IAsyncResult BeginUploadCaptureImage(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo request, System.AsyncCallback callback, object asyncState);
        
        void EndUploadCaptureImage(System.IAsyncResult result);
        
        // CODEGEN: DownloadStreamRequest 메시지의 래퍼 이름(DownloadStreamRequest)이 기본값(DownloadCaptureImageBy)과 일치하지 않으므로 메시지 계약을 생성합니다.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.masco.co.kr/ILSValidator/IFileTransfer/DownloadCaptureImageBy", ReplyAction="http://www.masco.co.kr/ILSValidator/IFileTransfer/DownloadCaptureImageByResponse")]
        Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamInfo DownloadCaptureImageBy(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.masco.co.kr/ILSValidator/IFileTransfer/DownloadCaptureImageBy", ReplyAction="http://www.masco.co.kr/ILSValidator/IFileTransfer/DownloadCaptureImageByResponse")]
        System.IAsyncResult BeginDownloadCaptureImageBy(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest request, System.AsyncCallback callback, object asyncState);
        
        Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamInfo EndDownloadCaptureImageBy(System.IAsyncResult result);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RemoteFileInfo", WrapperNamespace="http://www.masco.co.kr/ILSValidator", IsWrapped=true)]
    public partial class RemoteFileInfo {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public int CheckListPk;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public bool IsBasemap;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public long Length;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.masco.co.kr/ILSValidator", Order=0)]
        public System.IO.Stream FileByteStream;
        
        public RemoteFileInfo() {
        }
        
        public RemoteFileInfo(int CheckListPk, bool IsBasemap, long Length, System.IO.Stream FileByteStream) {
            this.CheckListPk = CheckListPk;
            this.IsBasemap = IsBasemap;
            this.Length = Length;
            this.FileByteStream = FileByteStream;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadStreamRequest", WrapperNamespace="http://www.masco.co.kr/ILSValidator", IsWrapped=true)]
    public partial class DownloadStreamRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public int captureImagePk;
        
        public DownloadStreamRequest() {
        }
        
        public DownloadStreamRequest(int captureImagePk) {
            this.captureImagePk = captureImagePk;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadStreamInfo", WrapperNamespace="http://www.masco.co.kr/ILSValidator", IsWrapped=true)]
    public partial class DownloadStreamInfo {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public byte[] Bytes;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public string ChildPath;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public string FileName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.masco.co.kr/ILSValidator")]
        public long Length;
        
        public DownloadStreamInfo() {
        }
        
        public DownloadStreamInfo(byte[] Bytes, string ChildPath, string FileName, long Length) {
            this.Bytes = Bytes;
            this.ChildPath = ChildPath;
            this.FileName = FileName;
            this.Length = Length;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileTransferChannel : Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DownloadCaptureImageByCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public DownloadCaptureImageByCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string ChildPath {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
        
        public string FileName {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        public long Length {
            get {
                base.RaiseExceptionIfNecessary();
                return ((long)(this.results[2]));
            }
        }
        
        public byte[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[3]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileTransferClient : System.ServiceModel.ClientBase<Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer>, Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer {
        
        private BeginOperationDelegate onBeginUploadCaptureImageDelegate;
        
        private EndOperationDelegate onEndUploadCaptureImageDelegate;
        
        private System.Threading.SendOrPostCallback onUploadCaptureImageCompletedDelegate;
        
        private BeginOperationDelegate onBeginDownloadCaptureImageByDelegate;
        
        private EndOperationDelegate onEndDownloadCaptureImageByDelegate;
        
        private System.Threading.SendOrPostCallback onDownloadCaptureImageByCompletedDelegate;
        
        public FileTransferClient() {
        }
        
        public FileTransferClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileTransferClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileTransferClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileTransferClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> UploadCaptureImageCompleted;
        
        public event System.EventHandler<DownloadCaptureImageByCompletedEventArgs> DownloadCaptureImageByCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer.UploadCaptureImage(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo request) {
            base.Channel.UploadCaptureImage(request);
        }
        
        public void UploadCaptureImage(int CheckListPk, bool IsBasemap, long Length, System.IO.Stream FileByteStream) {
            Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo inValue = new Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo();
            inValue.CheckListPk = CheckListPk;
            inValue.IsBasemap = IsBasemap;
            inValue.Length = Length;
            inValue.FileByteStream = FileByteStream;
            ((Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer)(this)).UploadCaptureImage(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer.BeginUploadCaptureImage(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginUploadCaptureImage(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginUploadCaptureImage(int CheckListPk, bool IsBasemap, long Length, System.IO.Stream FileByteStream, System.AsyncCallback callback, object asyncState) {
            Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo inValue = new Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.RemoteFileInfo();
            inValue.CheckListPk = CheckListPk;
            inValue.IsBasemap = IsBasemap;
            inValue.Length = Length;
            inValue.FileByteStream = FileByteStream;
            return ((Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer)(this)).BeginUploadCaptureImage(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndUploadCaptureImage(System.IAsyncResult result) {
            base.Channel.EndUploadCaptureImage(result);
        }
        
        private System.IAsyncResult OnBeginUploadCaptureImage(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int CheckListPk = ((int)(inValues[0]));
            bool IsBasemap = ((bool)(inValues[1]));
            long Length = ((long)(inValues[2]));
            System.IO.Stream FileByteStream = ((System.IO.Stream)(inValues[3]));
            return this.BeginUploadCaptureImage(CheckListPk, IsBasemap, Length, FileByteStream, callback, asyncState);
        }
        
        private object[] OnEndUploadCaptureImage(System.IAsyncResult result) {
            this.EndUploadCaptureImage(result);
            return null;
        }
        
        private void OnUploadCaptureImageCompleted(object state) {
            if ((this.UploadCaptureImageCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.UploadCaptureImageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void UploadCaptureImageAsync(int CheckListPk, bool IsBasemap, long Length, System.IO.Stream FileByteStream) {
            this.UploadCaptureImageAsync(CheckListPk, IsBasemap, Length, FileByteStream, null);
        }
        
        public void UploadCaptureImageAsync(int CheckListPk, bool IsBasemap, long Length, System.IO.Stream FileByteStream, object userState) {
            if ((this.onBeginUploadCaptureImageDelegate == null)) {
                this.onBeginUploadCaptureImageDelegate = new BeginOperationDelegate(this.OnBeginUploadCaptureImage);
            }
            if ((this.onEndUploadCaptureImageDelegate == null)) {
                this.onEndUploadCaptureImageDelegate = new EndOperationDelegate(this.OnEndUploadCaptureImage);
            }
            if ((this.onUploadCaptureImageCompletedDelegate == null)) {
                this.onUploadCaptureImageCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnUploadCaptureImageCompleted);
            }
            base.InvokeAsync(this.onBeginUploadCaptureImageDelegate, new object[] {
                        CheckListPk,
                        IsBasemap,
                        Length,
                        FileByteStream}, this.onEndUploadCaptureImageDelegate, this.onUploadCaptureImageCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamInfo Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer.DownloadCaptureImageBy(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest request) {
            return base.Channel.DownloadCaptureImageBy(request);
        }
        
        public byte[] DownloadCaptureImageBy(int captureImagePk, out string ChildPath, out string FileName, out long Length) {
            Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest inValue = new Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest();
            inValue.captureImagePk = captureImagePk;
            Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamInfo retVal = ((Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer)(this)).DownloadCaptureImageBy(inValue);
            ChildPath = retVal.ChildPath;
            FileName = retVal.FileName;
            Length = retVal.Length;
            return retVal.Bytes;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer.BeginDownloadCaptureImageBy(Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDownloadCaptureImageBy(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginDownloadCaptureImageBy(int captureImagePk, System.AsyncCallback callback, object asyncState) {
            Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest inValue = new Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamRequest();
            inValue.captureImagePk = captureImagePk;
            return ((Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer)(this)).BeginDownloadCaptureImageBy(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamInfo Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer.EndDownloadCaptureImageBy(System.IAsyncResult result) {
            return base.Channel.EndDownloadCaptureImageBy(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public byte[] EndDownloadCaptureImageBy(System.IAsyncResult result, out string ChildPath, out string FileName, out long Length) {
            Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.DownloadStreamInfo retVal = ((Masco.Display.ILSValidator.Client.Common.ServiceReferenceFileTransfer.IFileTransfer)(this)).EndDownloadCaptureImageBy(result);
            ChildPath = retVal.ChildPath;
            FileName = retVal.FileName;
            Length = retVal.Length;
            return retVal.Bytes;
        }
        
        private System.IAsyncResult OnBeginDownloadCaptureImageBy(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int captureImagePk = ((int)(inValues[0]));
            return this.BeginDownloadCaptureImageBy(captureImagePk, callback, asyncState);
        }
        
        private object[] OnEndDownloadCaptureImageBy(System.IAsyncResult result) {
            string ChildPath = this.GetDefaultValueForInitialization<string>();
            string FileName = this.GetDefaultValueForInitialization<string>();
            long Length = this.GetDefaultValueForInitialization<long>();
            byte[] retVal = this.EndDownloadCaptureImageBy(result, out ChildPath, out FileName, out Length);
            return new object[] {
                    ChildPath,
                    FileName,
                    Length,
                    retVal};
        }
        
        private void OnDownloadCaptureImageByCompleted(object state) {
            if ((this.DownloadCaptureImageByCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DownloadCaptureImageByCompleted(this, new DownloadCaptureImageByCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DownloadCaptureImageByAsync(int captureImagePk) {
            this.DownloadCaptureImageByAsync(captureImagePk, null);
        }
        
        public void DownloadCaptureImageByAsync(int captureImagePk, object userState) {
            if ((this.onBeginDownloadCaptureImageByDelegate == null)) {
                this.onBeginDownloadCaptureImageByDelegate = new BeginOperationDelegate(this.OnBeginDownloadCaptureImageBy);
            }
            if ((this.onEndDownloadCaptureImageByDelegate == null)) {
                this.onEndDownloadCaptureImageByDelegate = new EndOperationDelegate(this.OnEndDownloadCaptureImageBy);
            }
            if ((this.onDownloadCaptureImageByCompletedDelegate == null)) {
                this.onDownloadCaptureImageByCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDownloadCaptureImageByCompleted);
            }
            base.InvokeAsync(this.onBeginDownloadCaptureImageByDelegate, new object[] {
                        captureImagePk}, this.onEndDownloadCaptureImageByDelegate, this.onDownloadCaptureImageByCompletedDelegate, userState);
        }
    }
}
