using Masco.Display.ILSValidator.Client.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public class ValidationMethods
    {
        private PsdFileSectionVM _psdFileSectionVM;
        private PsdFileVM _psdFileVM;
        private string _ILSType;

        public ValidationMethods()
        {
            DSPsdLayerVMList = new BindingList<PsdLayerVM>();
            DsValidCodeList = new BindingList<ValidateVM>();
            DsErrorMsgList = new BindingList<ErrorMsgVM>();
        }

        public BindingList<PsdLayerVM> DSPsdLayerVMList { get; set; }
        public BindingList<ErrorMsgVM> DsErrorMsgList { get; set; }
        public BindingList<ValidateVM> DsValidCodeList { get; set; }

        #region ErrorMsg

        private void AddErrorMsg(ValidateVM vm, string msg)
        {
            AddErrorMsg(vm, 0, ErrorMsgLevel.Level4, msg);
        }

        private void AddErrorMsg(ValidateVM vm, ErrorMsgLevel level, string msg)
        {
            AddErrorMsg(vm, 0, level, msg);
        }

        private void AddErrorMsg(ValidateVM vm, int layerIDX, string msg)
        {
            AddErrorMsg(vm, layerIDX, ErrorMsgLevel.Level4, msg);
        }

        private void AddErrorMsg(ValidateVM vm, int layerIDX, ErrorMsgLevel level, string msg)
        {
            var item = new ErrorMsgVM();
            item.Index = vm.INDEX;
            item.Code = vm.CODE;
            item.Level = level;
            item.LayerIDX = layerIDX;
            item.ErrorMsg = msg;
            DsErrorMsgList.Add(item);
        }

        #endregion ErrorMsg

        public void Reset()
        {
            DsErrorMsgList.Clear();
            foreach (var x in DsValidCodeList)
            {
                x.ResultState = ResultType.Notset;
            }
        }

        public void Run(string ilsType, PsdFileVM psdFile, PsdFileSectionVM psdFileSection)
        {
            this._ILSType = ilsType;
            this._psdFileVM = psdFile;
            this._psdFileSectionVM = psdFileSection;
            var isFirst = DSPsdLayerVMList.FirstOrDefault();

            foreach (var item in DsValidCodeList)
            {
                item.ResultState = ResultType.Fail;
                try
                {
                    switch (item.CODE)
                    {
                        #region 이미지 CE
                        case ValidationCodeType.M_CE_WD_MC_0001: M_CE_WD_MC_0001(item); break;// -- CE : 도시고속입구 확대도 파일명 글자갯수 8자
                        case ValidationCodeType.M_CE_D_AC_0001: M_CE_D_AC_0001(item); break;// -- CE : 파일명은 90으로 시작한다.
                        case ValidationCodeType.M_CE_D_AC_0002: M_CE_D_AC_0002(item); break;// -- CE : 'D0'로 시작하는 레어어는 1개 뿐이다.

                        case ValidationCodeType.M_CE_WD_AC_0004: M_CE_WD_AC_0004(item); break;// -- CE : 도시고속입구 확대도  화살표 그룹내 레이어 글자갯수 11자
                        case ValidationCodeType.M_CE_WD_LS_0001: M_CE_WD_LS_0001(item); break;// -- CE : 레이어구조 고정 B Type
                        case ValidationCodeType.M_CE_WD_LS_0002: M_CE_WD_LS_0002(item); break;// -- CE : 레이어구조 가변 B Type
                        case ValidationCodeType.M_CE_WD_LS_0009: M_CE_WD_LS_0009(item); break;// -- CE : 레이어/레이어셋 총 개수 확인
                        case ValidationCodeType.M_CE_WD_LS_0010: M_CE_WD_LS_0010(item); break;// -- CE : 최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)
                        case ValidationCodeType.M_CE_WD_LS_0011: M_CE_WD_LS_0011(item); break;// -- CE : 가장 마지막 레이어셋은 Arrow
                        #endregion

                        #region 이미지 CM
                        case ValidationCodeType.M_CM_WD_FM_0006: M_CM_WD_FM_0006(item); break;// -- CM : PSD Mode (이미지 > Image Size > Width 2048 / Height 2048)
                        #endregion

                        #region 이미지 ET
                        case ValidationCodeType.M_ET_WD_AC_0001: M_ET_WD_AC_0001(item); break;// -- ETC : 파일명 KREI 는 그룹명 'ETC_'로 시작해야한다. (예:ETC_1)
                        case ValidationCodeType.M_ET_WD_AC_0002: M_ET_WD_AC_0002(item); break;// -- ETC : ETC_'로 시작하는 그룹은 0개 이상이다.
                        case ValidationCodeType.M_ET_WD_AC_0004: M_ET_WD_AC_0004(item); break;// -- ETC : ETC ETC그룹내 그룹명 글자갯수 $5
                        case ValidationCodeType.M_ET_WD_FM_0006: M_ET_WD_FM_0006(item); break;// -- ETC : PSD Mode (이미지 > Image Size > Width 2048 / Height 2048 )
                        case ValidationCodeType.M_ET_WD_LS_0009: M_ET_WD_LS_0009(item); break;// -- ETC : 레이어/레이어셋 총 개수 확인
                        case ValidationCodeType.M_ET_WD_LS_0010: M_ET_WD_LS_0010(item); break;// -- ETC :최상위 레이어셋 구조
                        case ValidationCodeType.M_ET_WD_LS_0011: M_ET_WD_LS_0011(item); break;//  -- ETC : 가장 마지막 레이어셋은 ETC
                        #endregion

                        #region 이미지 JC
                        case ValidationCodeType.M_JC_WD_AC_0001: M_JC_WD_AC_0001(item); break;// -- JC : 파일명 KRJM 는 그룹명 'Arrow_KRJA'로 시작해야한다. (예:Arrow_KRJA16090693D084F00101)
                        case ValidationCodeType.M_JC_WD_AC_0002: M_JC_WD_AC_0002(item); break;// -- JC : 그룹명 'Arrow_KRJA'로 시작하는 그룹이 1개 이상이다
                        case ValidationCodeType.M_JC_WD_AC_0003: M_JC_WD_AC_0003(item); break;// -- JC : 그룹명 'Arrow_KRJA'로 시작하는 그룹의 모든 포맷 확인
                        case ValidationCodeType.M_JC_WD_AC_0004: M_JC_WD_AC_0004(item); break;// -- JC Arrow 그룹내 그룹명 글자갯수 $28
                        case ValidationCodeType.M_JC_WD_LS_0006: M_JC_WD_LS_0006(item); break;// -- JC Arrow 그룹내 그룹명 글자갯수 $28
                        case ValidationCodeType.M_JC_WD_LS_0007: M_JC_WD_LS_0007(item); break;// -- JC Arrow 그룹내 그룹명 글자갯수 $28

                        case ValidationCodeType.M_JC_WD_LS_0001: M_JC_WD_LS_0001(item); break;// -- JC :  레이어구조 A Type : 고정부분
                        case ValidationCodeType.M_JC_WD_LS_0002: M_JC_WD_LS_0002(item); break;// -- JC : 레이어구조 A Type : 가변부분
                        case ValidationCodeType.M_JC_WD_LS_0008: M_JC_WD_LS_0008(item); break;// -- JC : Road Background_Color 0,2048,02048 or 800,600 (꽉 채워져야함)
                        case ValidationCodeType.M_JC_WD_LS_0009: M_JC_WD_LS_0009(item); break;// -- JC : Transparency_Color 0,2048,02048 or 800,600 (꽉 채워져야함)

                        case ValidationCodeType.M_JC_WD_LS_0010: M_JC_WD_LS_0010(item); break;// -- JC : 레이어 개수 확인
                        case ValidationCodeType.M_JC_WD_LS_0011: M_JC_WD_LS_0011(item); break;// -- JC : 최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)
                        case ValidationCodeType.M_JC_WD_LS_0012: M_JC_WD_LS_0012(item); break;// -- JC : 가장 마지막 레이어셋은 Arrow
                        case ValidationCodeType.M_JC_WD_MC_0001: M_JC_WD_MC_0001(item); break;// -- JC : 파일명 글자갯수 22자


                        #endregion

                        #region 이미지 NC
                        case ValidationCodeType.M_NC_WD_AC_0001: M_NC_WD_AC_0001(item); break; // -- 파일명 KRCM 는 그룹명 'Arrow_KRCA'로 시작해야한다. (예:Arrow_KRCA16090693D084F00101)
                        case ValidationCodeType.M_NC_WD_AC_0002: M_NC_WD_AC_0002(item); break; // -- 그룹명 'Arrow_KRCA'로 시작하는 그룹이' 1개' 이상이다
                        case ValidationCodeType.M_NC_WD_AC_0003: M_NC_WD_AC_0003(item); break; // -- 그룹명 'Arrow_KRCA'로 시작하는 그룹의 모든 포맷 확인
                        case ValidationCodeType.M_NC_WD_LS_0001: M_NC_WD_LS_0001(item); break;  // -- 레이어구조 A Type : 고정부분
                        case ValidationCodeType.M_NC_WD_LS_0002: M_NC_WD_LS_0002(item); break;  // -- 레이어구조 A Type : 가변부분 (Arrow 폴더 하위 구조)
                        case ValidationCodeType.M_NC_WD_LS_0003: M_NC_WD_LS_0003(item); break;  // -- Layer Transparency_Color 는 ARGB 값이 255/255/0/255 이여야 한다
                        case ValidationCodeType.M_NC_WD_LS_0004: M_NC_WD_LS_0004(item); break;  // -- Layer Road Background_Color 는  ARGB 값이 255/64/69/76이여야 한다
                        case ValidationCodeType.M_NC_WD_LS_0005: M_NC_WD_LS_0005(item); break;  // -- Road Background_Colo/Transparency_Color - Arrow 그룹내 위치
                        //case ValidationCodeType.M_NC_WD_LS_0006: M_NC_WD_LS_0006(item); break;  // -- Arrow->Direction - Opacity 100% => 20171212 변경 Transparency - Opacity 50% 을 제외하고 모두 100;
                        case ValidationCodeType.M_NC_WD_LS_0007: M_NC_WD_LS_0007(item); break;  // -- Arrow->Transparency - Opacity 50%
                        case ValidationCodeType.M_NC_WD_LS_0008: M_NC_WD_LS_0008(item); break;  // -- Road Background_Color 0,2048,02048(꽉 채워져야함) -> >c+마우스 해당 클릭 ->c+t
                        case ValidationCodeType.M_NC_WD_LS_0009: M_NC_WD_LS_0009(item); break;  // -- Transparency_Color 0,2048,02048(꽉 채워져야함) -> >c+마우스 해당 클릭 ->c+t
                        case ValidationCodeType.M_NC_WD_LS_0010: M_NC_WD_LS_0010(item); break;  // -- 레이어/레이어셋 총 개수 확인
                        case ValidationCodeType.M_NC_WD_LS_0011: M_NC_WD_LS_0011(item); break;  // -- 최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)
                        case ValidationCodeType.M_NC_WD_LS_0012: M_NC_WD_LS_0012(item); break;  // -- 가장 마지막 레이어셋은 Arrow
                        #endregion

                        #region 이미지/ 패턴 CE
                        case ValidationCodeType.MP_CE_WD_LS_0003: MP_CE_WD_LS_0003(item); break; //  -- 레이어별 show/hide
                        case ValidationCodeType.MP_CE_WD_LS_0007: MP_CE_WD_LS_0007(item); break; //  -- CE : 레이어별 공백포함 확인

                        #endregion

                        #region 이미지/ 패턴 CM
                        case ValidationCodeType.MP_CM_WD_FM_0001: MP_CM_WD_FM_0001(item); break; // -- CM : 채널 - Alpha/Red/Green/Blue 4개임
                        case ValidationCodeType.MP_CM_WD_FM_0002: MP_CM_WD_FM_0002(item); break; // -- CM : 채널 - Alpha/Red/Green/Blue 이외 없어야 함
                        //case ValidationCodeType.MP_CM_WD_FM_0003: MP_CM_WD_FM_0003(item); break; // -- CM : PSD ColorMode (이미지 > 모드 > RGB Color) ////////////////////////////////////////////// < -- 전체검사
                        case ValidationCodeType.MP_CM_WD_FM_0004: MP_CM_WD_FM_0004(item); break; // -- CM : PSD Depth - rgd 8bit (이미지 > 모드 > 8bit)
                        case ValidationCodeType.MP_CM_WD_FM_0005: MP_CM_WD_FM_0005(item); break; // -- CM : PSD 해상도 72pixel (C+A+I)
                        case ValidationCodeType.MP_CM_WD_LS_0001: MP_CM_WD_LS_0001(item); break; // -- CM : 레이어 그룹의 명칭들이 명칭 중복되면 안된다
                        case ValidationCodeType.MP_CM_WD_LS_0002: MP_CM_WD_LS_0002(item); break; // -- CM : Clipping 기본 체크 해제
                        case ValidationCodeType.MP_CM_WD_LS_0003: MP_CM_WD_LS_0003(item); break; // -- CM : Layer Night_Filtter_B는 RGB 값이 0/0/0/0 이여야 한다
                        case ValidationCodeType.MP_CM_WD_LS_0004: MP_CM_WD_LS_0004(item); break; // -- CM : Layer 이미지 Road_B는 ARGB 값이 0/0/0/0 이여야 한다
                        case ValidationCodeType.MP_CM_WD_LS_0005: MP_CM_WD_LS_0005(item); break; // -- CM : Layer 이미지 Arrow는 ARGB 값이 0/255/0/0 이여야 한다
                        case ValidationCodeType.MP_CM_WD_LS_0006: MP_CM_WD_LS_0006(item); break; // -- CM : 각 레이어 별로 Lock이 걸려있는지 확인 (이미지 레이어만 해당됨)
                        case ValidationCodeType.MP_CM_WD_LS_0007: MP_CM_WD_LS_0007(item); break; // -- CM : 그룹명/레이어명 공란 무조건 없어야 한다. 
                        case ValidationCodeType.MP_CM_WD_LS_0008: MP_CM_WD_LS_0008(item); break;  // -- CM : Arrow->Direction - Opacity 100% => 20171212 변경 Transparency - Opacity 50% 을 제외하고 모두 100;
                        case ValidationCodeType.MP_CM_WD_LS_0009: MP_CM_WD_LS_0009(item); break;// -- CM : Layer Road Background_Color 는  ARGB 값이 255/64/69/76이여야 한다

                        case ValidationCodeType.MP_CM_WD_MC_0001: MP_CM_WD_MC_0001(item); break; // -- CM : 파일확장자 확인 $.psd
                        case ValidationCodeType.MP_CM_WD_MC_0002: MP_CM_WD_MC_0002(item); break; // -- CM : 메인코드의 길이는 22자????????????????????????????? <---------------------확인필요(리스트에 없음)
                        #endregion

                        #region 이미지/ 패턴 ET
                        case ValidationCodeType.MP_ET_WD_LS_0001: MP_ET_WD_LS_0001(item); break;// -- ETC : 레이어구조 고정 C Type
                        case ValidationCodeType.MP_ET_WD_LS_0002: MP_ET_WD_LS_0002(item); break;// -- ETC : 레이어구조 가변 C Type
                        case ValidationCodeType.MP_ET_WD_LS_0003: MP_ET_WD_LS_0003(item); break;// --  ETC :레이어별 show/hide
                        case ValidationCodeType.MP_ET_WD_MC_0001: MP_ET_WD_MC_0001(item); break;// -- ETC 파일명 글자 개수 $21
                        case ValidationCodeType.MP_ET_WD_LS_0007: MP_ET_WD_LS_0007(item); break;// -- ETC 파일명 글자 개수 $21

                        #endregion

                        #region 이미지/ 패턴 JC
                        case ValidationCodeType.MP_JC_WD_LS_0003: MP_JC_WD_LS_0003(item); break;//  -- 레이어별 show/hide
                        #endregion

                        #region 이미지/ 패턴 NC
                        case ValidationCodeType.MP_NC_WD_AC_0001: MP_NC_WD_AC_0001(item); break;//  -- 이미지+일반교차로 : 일반교차로 Arrow 레이어셋 하위 그룹명 Arrow_KRCA*** 길이는 28
                        case ValidationCodeType.MP_NC_WD_LS_0008: MP_NC_WD_LS_0008(item); break;//  -- 레이어별 show/hide
                        #endregion

                        #region 이미지 모식도(MimeticDiagram) 
                        case ValidationCodeType.M_MD_WD_MC_0001: M_MD_WD_MC_0001(item); break; // -- MimeticDiagram : 모식도 파일명은 8로 시작한다.
                        case ValidationCodeType.M_MD_WD_MC_0002: M_MD_WD_MC_0002(item); break; // -- MimeticDiagram : 모식도 파일명 글자갯수 8자
                        case ValidationCodeType.M_MD_WD_AC_0001: M_MD_WD_AC_0001(item); break; // -- MimeticDiagram : 파일명 Arrow 는 그룹명 'Arrow_'로 시작해야한다. (예:Arrow_16090693)
                        case ValidationCodeType.M_MD_WD_AC_0002: M_MD_WD_AC_0002(item); break; // -- MimeticDiagram : 그룹명 'Arrow_'로 시작하는 그룹이' 1개' 이상이다
                        case ValidationCodeType.M_MD_WD_AC_0003: M_MD_WD_AC_0003(item); break; // -- MimeticDiagram : 그룹명 'Arrow_'로 시작하는 모든 그룹확인
                        case ValidationCodeType.M_MD_WD_AC_0004: M_MD_WD_AC_0004(item); break; // -- MimeticDiagram : Arrow 그룹내 그룹명 글자갯수 14
                        #endregion

                        #region 이미지 3D교차점(CrossRoadPoint3D) 
                        case ValidationCodeType.M_CR3D_WD_MC_0001: M_CR3D_WD_MC_0001(item); break; // -- CrossRoadPoint3D : 모식도 파일명은 8로 시작한다.
                        case ValidationCodeType.M_CR3D_WD_MC_0002: M_CR3D_WD_MC_0002(item); break; // -- CrossRoadPoint3D : 모식도 파일명 글자갯수 8자
                        case ValidationCodeType.M_CR3D_WD_AC_0001: M_CR3D_WD_AC_0001(item); break; // -- CrossRoadPoint3D : Arrow 그룹내 그룹명 글자갯수 14
                        case ValidationCodeType.M_CR3D_WD_AC_0002: M_CR3D_WD_AC_0002(item); break; // -- CrossRoadPoint3D : 그룹명 '_AI'로 시작하는 그룹이 2개이다
                        #endregion

                        #region 휴게소 요약맵(RestAreaSummaryMap_Mapy) 
                        case ValidationCodeType.M_RASMM_WD_MC_0001: M_RASMM_WD_MC_0001(item); break; // -- RestAreaSummaryMap_Mapy : 첫 폴더는 Title이여야한다
                        case ValidationCodeType.M_RASMM_WD_MC_0002: M_RASMM_WD_MC_0002(item); break; // -- RestAreaSummaryMap_Mapy : 구조확인
                        #endregion

                        #region 휴게소 요약맵(RestAreaSummaryMap_Gini) 
                        case ValidationCodeType.M_RASMG_WD_MC_0001: M_RASMG_WD_MC_0001(item); break; // -- RestAreaSummaryMap_Gini : 첫 폴더는 Title_set이여야한다
                        case ValidationCodeType.M_RASMG_WD_MC_0002: M_RASMG_WD_MC_0002(item); break; // -- RestAreaSummaryMap_Gini : 구조확인
                        #endregion
                    }

                    if (DsErrorMsgList.Any(x => x.Index == item.INDEX) == false)
                        item.ResultState = ResultType.Success;
                }
                catch (Exception ex)
                {
                    AddErrorMsg(item, string.Format("검증코드.Index : {0}, {1}", item.INDEX, ex.ToString()));
                }
            }
        }

        #region 모식도
        private void M_MD_WD_AC_0001(ValidateVM item)
        {
            #region (모식도) MimeticDiagram : 파일명 Arrow 는 그룹명 'Arrow_'로 시작해야한다.
            var layerIndex = LayerIndex.MimeticDiagram.L21_Arrow;
            var arrowLayerName = LayerIndex.GetName<LayerIndex.MimeticDiagram>(layerIndex);

            var arrowLayerList = DSPsdLayerVMList.Where(o_ => o_.Name.StartsWith(arrowLayerName) && o_.ParentIndex == 0).ToList();
            var arrowLayer = arrowLayerList.FirstOrDefault();
            var arrowMimeticDiagram = DSPsdLayerVMList.Where(o_ => o_.ParentIndex == arrowLayer.Index && o_.HasImage == false).ToList();
            if (arrowMimeticDiagram.Count() >= 1)
            {
                foreach (var x in arrowMimeticDiagram)
                {
                    var pattern = "Arrow_";
                    var match = x.Name.StartsWith(pattern);
                    if (match == false)
                    {
                        AddErrorMsg(item, x.Index, string.Format("레이어명 오류 :  명칭 {1}", pattern, x.Name));
                    }
                }
            }
            #endregion
        }

        private void M_MD_WD_AC_0002(ValidateVM item)
        {
            #region Arrow 레이어셋 1개 이상인가?
            var layerIndex = LayerIndex.MimeticDiagram.Arrow_;
            var layerNameStartWidth = LayerIndex.GetName<LayerIndex.NC>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWidth)).ToList();
            if (arrowLayerList.Count() < 1)
            {
                AddErrorMsg(item, string.Format("{0}로 시작하는 폴더명은 최소 1개 이상입니다.", layerNameStartWidth));
            }
            #endregion Arrow 레이어셋 1개 이상인가?
        }

        private void M_MD_WD_AC_0003(ValidateVM item)
        {
            #region 멀티개의 Arrow 레이어셋 명칭이 Arrow_KRMC***로 시작하는가? (공백, 특수문자 확인), 포맷 확인
            var layerIndex = LayerIndex.MimeticDiagram.Arrow_;
            var layerNameStartWidth = LayerIndex.GetName<LayerIndex.MimeticDiagram>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWidth)).ToList();
            if (arrowLayerList.Count() >= 1)
            {
                var list = LayerNameManager.Instance.NCList;
                var pattern = @"^Arrow_";
                var regEx = new Regex(pattern);

                foreach (var x in arrowLayerList)
                {
                    var match = regEx.Match(x.Name);
                    if (match.Success == false)
                    {
                        AddErrorMsg(item, x.Index, string.Format("레이어 오류 :  명칭 {1}", pattern, x.Name));
                    }
                }
            }

            #endregion 멀티개의 Arrow 레이어셋 명칭이 Arrow_KRMC***로 시작하는가? (공백, 특수문자 확인)
        }

        private void M_MD_WD_AC_0004(ValidateVM item)
        {
            #region MimeticDiagram : Arrow 그룹내 그룹명 글자갯수 14
            var layerIndex = LayerIndex.MimeticDiagram.Arrow_;
            var layerNameStartWith = LayerIndex.GetName<LayerIndex.MimeticDiagram>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWith)).ToList();
            if (arrowLayerList.Any() == false)
            {
                AddErrorMsg(item, string.Format("레이어 못 찾음 :  {0}, {1}", layerIndex, layerNameStartWith));
            }
            else
            {
                var validLength = LayerNameManager.Instance.MD_ArrowCode_Length;
                foreach (var x in arrowLayerList)
                {

                    if (x.Name.Length != validLength)
                    {
                        AddErrorMsg(item, x.Index, string.Format("Arrow 코드 길이({0}) 오류 : {1} ", validLength, x.Name));
                    }
                }
            }
            #endregion MimeticDiagram : Arrow 그룹내 그룹명 글자갯수 14
        }
        #endregion

        #region 휴게소 요약도 맵피
        private void M_RASMM_WD_MC_0001(ValidateVM item)
        {
            var folderList = DSPsdLayerVMList.Where(x => x.HasImage == false && x.LayerDepth == 1).ToList();
            var lastFolder = folderList.LastOrDefault();
            var firstFolder = folderList.FirstOrDefault();
            if (firstFolder.Name != "Title")
            {
                AddErrorMsg(item, string.Format("Title이 첫번째가 아닙니다."));
            }
        }

        private void M_RASMM_WD_MC_0002(ValidateVM item)
        {
            var topList = DSPsdLayerVMList.Where(x => x.ParentIndex == 0).ToList();

            #region RestAreaSummaryMap_Mapy : 구조확인 고정Type
            var fixedLayerIndex = LayerIndex.RestAreaSummaryMap_Mapy.GetFixedLayerIndex();
            var layerNameList = LayerNameManager.Instance.RASMMList.Where(x => x.Index <= fixedLayerIndex).Reverse();

            foreach (var layer in layerNameList)
            {
                var resultLayerList = topList.Where(x => x.LayerSeq == layer.Index);
                if (resultLayerList.Count() != 1)
                {
                    AddErrorMsg(item, string.Format("레이어 인덱스 오류 :  {0}", layer.Index));
                    return;
                }
                var resultLayer = resultLayerList.FirstOrDefault();
                if (layer.Name != resultLayer.Name)
                {
                    AddErrorMsg(item, string.Format("레이어 순서 오류 :  {0}, {1}", layer.Index, resultLayer.Name));
                    return;
                }
            }
            #endregion
        }
        #endregion

        #region 휴게소 요약도 지니
        private void M_RASMG_WD_MC_0001(ValidateVM item)
        {
            var folderList = DSPsdLayerVMList.Where(x => x.HasImage == false && x.LayerDepth == 1).ToList();
            var lastFolder = folderList.LastOrDefault();
            var firstFolder = folderList.FirstOrDefault();
            if (firstFolder.Name != "Title_set")
            {
                AddErrorMsg(item, string.Format("Title_set이 첫번째가 아닙니다."));
            }
        }

        private void M_RASMG_WD_MC_0002(ValidateVM item)
        {
            var topList = DSPsdLayerVMList.Where(x => x.ParentIndex == 0).ToList();

            #region RestAreaSummaryMap_Mapy : 구조확인 고정Type
            var fixedLayerIndex = LayerIndex.RestAreaSummaryMap_Gini.GetFixedLayerIndex();
            var layerNameList = LayerNameManager.Instance.RASMGList.Where(x => x.Index <= fixedLayerIndex).Reverse();

            foreach (var layer in layerNameList)
            {
                var resultLayerList = topList.Where(x => x.LayerSeq == layer.Index);
                if (resultLayerList.Count() != 1)
                {
                    AddErrorMsg(item, string.Format("레이어 인덱스 오류 :  {0}", layer.Index));
                    return;
                }
                var resultLayer = resultLayerList.FirstOrDefault();
                if (layer.Name != resultLayer.Name)
                {
                    AddErrorMsg(item, string.Format("레이어 순서 오류 :  {0}, {1}", layer.Index, resultLayer.Name));
                    return;
                }
            }
            #endregion
        }
        #endregion

        #region 3D 교차점
        private void M_CR3D_WD_AC_0001(ValidateVM item)
        {
            #region CrossRoadPoint3D : Arrow 그룹내 그룹명 글자갯수 14
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.EndsWith("_AI")).ToList();
            if (arrowLayerList.Any() == false)
            {
                AddErrorMsg(item, string.Format("_AI 레이어 못 찾음}"));
            }
            else
            {
                var validLength = LayerNameManager.Instance.CR3D_ArrowCode_Length;
                foreach (var x in arrowLayerList)
                {

                    if (x.Name.Length != validLength)
                    {
                        AddErrorMsg(item, x.Index, string.Format("Arrow 코드 길이({0}) 오류 : {1} ", validLength, x.Name));
                    }
                }
            }
            #endregion
        }

        private void M_CR3D_WD_AC_0002(ValidateVM item)
        {
            #region CrossRoadPoint3D : _AI 레이어셋 2개인가?
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.EndsWith("_AI")).ToList();
            if (arrowLayerList.Count() != 2)
            {
                AddErrorMsg(item, string.Format("_AI로 끝나는 레이어명이 2개가 아닙니다."));
            }
            #endregion
        }

        private void M_CR3D_WD_MC_0001(ValidateVM item)
        {
            #region CrossRoadPoint3D : 3D 교차점 파일명은 8로 시작한다.
            var startWith8 = _psdFileVM.Name.StartsWith("8");
            if (startWith8 == false)
            {
                AddErrorMsg(item, string.Format("파일 명 오류 (Start With 8) : {0}", _psdFileVM.Name));
            }
            #endregion
        }

        private void M_CR3D_WD_MC_0002(ValidateVM item)
        {
            #region CrossRoadPoint3D : 3D 교차점 확대도 파일명 글자갯수 8자
            var fileNameLen = _psdFileVM.Name.Length;
            var validMainCodeLen = LayerNameManager.Instance.MD_MainCode_Length;
            if (fileNameLen != validMainCodeLen)
            {
                AddErrorMsg(item, string.Format("파일명 길이({0}) 오류 : {1} {2}", validMainCodeLen, fileNameLen, _psdFileVM.Name));
            }
            #endregion
        }
        #endregion

        private void M_MD_WD_MC_0001(ValidateVM item)
        {
            #region MimeticDiagram : 모식도 파일명은 8로 시작한다.
            var startWith8 = _psdFileVM.Name.StartsWith("8");
            if (startWith8 == false)
            {
                AddErrorMsg(item, string.Format("파일 명 오류 (Start With 8) : {0}", _psdFileVM.Name));
            }
            #endregion
        }

        private void M_MD_WD_MC_0002(ValidateVM item)
        {
            #region MimeticDiagram : 모식도 파일명 글자갯수 8자
            var fileNameLen = _psdFileVM.Name.Length;
            var validMainCodeLen = LayerNameManager.Instance.MD_MainCode_Length;
            if (fileNameLen != validMainCodeLen)
            {
                AddErrorMsg(item, string.Format("파일명 길이({0}) 오류 : {1} {2}", validMainCodeLen, fileNameLen, _psdFileVM.Name));
            }
            #endregion
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        private IList<PsdLayerVM> GetChildLayerListByDefaultParentIndex<T>(int defaultLayerIndex, ref string parentLayerName, ref int parentLayerIndex)
        {
            var plName = LayerIndex.GetName<T>(defaultLayerIndex);
            parentLayerName = plName;
            var parentLayer = DSPsdLayerVMList.SingleOrDefault(x => x.Name == plName && x.HasImage == false);

            if (parentLayer == null)
            {
                return null;
            }

            var plIndex = parentLayer.Index;
            parentLayerIndex = plIndex;
            var childLayerList = DSPsdLayerVMList.Where(x => x.ParentIndex == plIndex).ToList();
            return childLayerList;
        }

        //private string GetName<T>

        private void M_CE_WD_MC_0001(ValidateVM item)
        {
            #region CE : 도시고속입구 확대도 파일명 글자갯수 8자

            var fileNameLen = _psdFileVM.Name.Length;
            var validMainCodeLen = LayerNameManager.Instance.CE_MainCode_Length;
            if (fileNameLen != LayerNameManager.Instance.CE_MainCode_Length)
            {
                AddErrorMsg(item, string.Format("메인코드 길이({0}) 오류 : {1} {2}", validMainCodeLen, fileNameLen, _psdFileVM.Name));
            }
            #endregion
        }

        private void M_CE_D_AC_0001(ValidateVM item)
        {
            #region CE : 파일명은 90으로 시작한다.

            var fileName = _psdFileVM.Name;

            var pattern = "^90([a-zA-Z0-9]){1,}$";
            var regEx = new Regex(pattern);
            var match = regEx.Match(fileName);

            if (match.Success == false)
            {
                AddErrorMsg(item, string.Format("파일명 오류 :  {0}", fileName));
            }
            #endregion
        }

        private void M_CE_D_AC_0002(ValidateVM item)
        {
            #region 'CE : 'D0'로 시작하는 레어어는 1개 뿐이다.
            var arrwoLayerIndex = 0;
            var d0LayerName = string.Empty;
            var d0LayerIndex = 0;
            {
                var layerIndex = LayerIndex.CE.L17_Arrow;
                var layerName = LayerIndex.GetName<LayerIndex.CE>(layerIndex);
                var arrowLayer = DSPsdLayerVMList.SingleOrDefault(x => x.Name == layerName && x.HasImage == false);
                if (arrowLayer == null)
                {
                    AddErrorMsg(item, string.Format("{0} 레이어가 없습니다.", layerName));
                    return;
                }
                if (arrowLayer.LayerDepth != 1)
                {
                    AddErrorMsg(item, string.Format("{0} 레이어의 Depth(1)가 틀림.", layerName));
                    return;

                }
                arrwoLayerIndex = arrowLayer.Index;
            }

            {
                var layerIndex = LayerIndex.CE.L14_D0;
                var layerNameStartWith = LayerIndex.GetName<LayerIndex.CE>(layerIndex);
                var arrowLayerList = DSPsdLayerVMList.Where(x => LayerIndex.CE.L14_DO_StringStartWith(x.Name, layerNameStartWith) && x.HasImage == true && x.ParentIndex == arrwoLayerIndex).ToList();
                var arrowLayerCount = arrowLayerList.Count();
                if (arrowLayerList.Count() != 1)
                {
                    AddErrorMsg(item, string.Format("{0}로 시작하는 레이어의 개수 (1) 불일치 : {1})", layerNameStartWith, arrowLayerCount));
                    return;
                }
                var arrowLayer = arrowLayerList.First();
                if (arrowLayer.LayerDepth != 2)
                {
                    AddErrorMsg(item, string.Format("{0} 레이어의 Depth(2)가 틀림. {1}", layerNameStartWith, arrowLayer.LayerDepth));
                    return;

                }
                d0LayerName = arrowLayer.Name;
                d0LayerIndex = arrowLayer.Index;
            }


            var pattern = "^(d|D)0([a-zA-Z0-9]){1,}_AI$";
            var regEx = new Regex(pattern);
            var match = regEx.Match(d0LayerName);

            if (match.Success == false)
            {
                AddErrorMsg(item, d0LayerIndex, string.Format("D0 레이어명 오류: 레이어명 {1}", pattern, d0LayerName));
            }
            #endregion
        }

        private void M_CE_WD_AC_0004(ValidateVM item)
        {
            #region CE : 도시고속입구 확대도  화살표 그룹내 레이어 글자갯수 11자

            var layerIndex = LayerIndex.CE.L17_Arrow;
            string outlayerName = string.Empty;
            int outLayerIndex = 0;
            var childList = GetChildLayerListByDefaultParentIndex<LayerIndex.CE>(layerIndex, ref outlayerName, ref outLayerIndex);
            if (childList == null)
            {
                AddErrorMsg(item, string.Format("{0} 레이어 못찾음  INDEX : {1}", outlayerName, outLayerIndex));
                return;
            }
            if (childList.Any() == false)
            {
                AddErrorMsg(item, string.Format("{0} 레이어 없음 0EA, INDEX : {1}", outlayerName, outLayerIndex));
                return;
            }

            var d0Name = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L14_D0);
            var d0LayerList = childList.Where(x => LayerIndex.CE.L14_DO_StringStartWith(x.Name, d0Name));
            var count = d0LayerList.Count();
            var defaultLength = LayerNameManager.Instance.CE_ArrowCode_Length;
            foreach (var x in d0LayerList)
            {
                var layerNameLength = x.Name.Length;
                if (layerNameLength != defaultLength)
                    AddErrorMsg(item, x.Index, string.Format("{0} 레이어 길이 ({1}) 오류 :  {2} ", outlayerName, defaultLength, layerNameLength));
            }
            #endregion
        }

        private void M_CE_WD_LS_0001(ValidateVM item)
        {
            #region CE : 레이어구조 고정 B Type
            var fixedLayerIndex = LayerIndex.CE.GetFixedLayerIndex();
            var layerNameList = LayerNameManager.Instance.CEList.Where(x => x.Index <= fixedLayerIndex);

            foreach (var x in layerNameList)
            {
                if (CheckLayer(item, DSPsdLayerVMList, x) == false) { }
            }
            #endregion
        }

        private void M_CE_WD_LS_0002(ValidateVM item)
        {
            #region CE : 레이어구조 가변 B Type
            var parentLayerName = string.Empty;
            var parentLayerIndex = 0;
            var childList = GetChildLayerListByDefaultParentIndex<LayerIndex.CE>(LayerIndex.CE.L11_Direction_Sub, ref parentLayerName, ref parentLayerIndex);
            if (childList == null)
            {

            }
            else
            {
                if (childList.Count != 1)
                {
                    AddErrorMsg(item, parentLayerIndex, string.Format("[{0}]레이어  Child 개수가 1개가 아님", parentLayerName));
                }
                else
                {
                    var name1 = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L10_Direction);
                    var name2 = childList.First().Name;

                    if (name1 != name2)
                    {
                        AddErrorMsg(item, childList.First().Index, string.Format("[{0}] 레이어명 불일치 : [{1}]", name1, name2));
                    }
                }
            }

            childList = GetChildLayerListByDefaultParentIndex<LayerIndex.CE>(LayerIndex.CE.L17_Arrow, ref parentLayerName, ref parentLayerIndex);
            var d0Name = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L14_D0);
            if (childList == null)
            {

            }
            else
            {
                var idx = 0;
                if (childList.Count > 4)
                {
                    idx = 2;
                }

                var name1 = childList[idx].Name;
                if (LayerIndex.CE.L14_DO_StringStartWith(name1, d0Name) == false)
                {
                    AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 레이어명 없음 (상위레이어 첫번째레이어에 존재해야 됨)", name1));
                    // error
                }
                name1 = childList[++idx].Name;
                if (name1 != LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L13_Road_Background_Color))
                {
                    AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 레이어명 없음 (상위레이어 첫번째레이어에 존재해야 됨)", name1));
                    // error
                }
                name1 = childList[++idx].Name;
                if (name1 != LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L12_Transparency_Color))
                {
                    AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 레이어명 없음 (상위레이어 첫번째레이어에 존재해야 됨)", name1));
                    // error
                }

                if (childList.Count == 5)
                {
                    name1 = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L16_Direction);
                    var dicLayer = childList.FirstOrDefault(x => x.Index == parentLayerIndex - 1);
                    if (dicLayer == null)
                    {
                        AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 레이어명 없음 (상위레이어 첫번째레이어에 존재해야 됨)", name1));
                    }
                    else
                    {
                        var name2 = dicLayer.Name;
                        if (name1 != name2)
                        {
                            AddErrorMsg(item, dicLayer.Index, string.Format("[{0}] 레이어명 불일치 : [{1}]", name1, name2));
                        }
                    }
                    name1 = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L15_Transperency);
                    var transLayer = childList.FirstOrDefault(x => x.Index == parentLayerIndex - 2);
                    if (transLayer == null)
                    {
                        AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 레이어명 없음 (상위레이어 첫번째레이어에 존재해야 됨)", name1));
                    }
                    else
                    {
                        var name2 = transLayer.Name;
                        if (name1 != name2)
                        {
                            AddErrorMsg(item, transLayer.Index, string.Format("[{0}] 레이어명 불일치 : [{1}]", name1, name2));
                        }
                    }
                }
            }

            //var layerIndex = LayerIndex.CE.;//<------★
            //var layerName = LayerIndex.GetName<LayerIndex.JC>(layerIndex);//<------★
            //var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerName)).ToList();
            //if (arrowLayerList.Count() >= 1)
            //{
            //    var list = LayerNameManager.Instance.JCList;//<------★
            //    var name = string.Empty;
            //    var maxLayerSeq = list.Count(x => x.IsVariable && x.LayerSeq > 0);
            //    foreach (var x in arrowLayerList)
            //    {
            //        for (var layerSeq = 1; layerSeq <= maxLayerSeq; layerSeq++)
            //        {
            //            PsdLayerVM temp = DSPsdLayerVMList.Where(y => y.ParentIndex == x.Index && y.LayerSeq == layerSeq).FirstOrDefault();
            //            if (temp == null)
            //            {
            //                AddErrorMsg(item, string.Format("레이어명 없음 : 부모순번 {0}, 순서 {1}", x.Index, layerSeq));
            //            }
            //            else if (LayerNameManager.Instance.GetNameBySeq(list, layerSeq, out name) == false)
            //            {
            //                AddErrorMsg(item, string.Format("비교할 기준 레이어 없음 : 부모순번 {0}, 순서 {1}", x.Index, layerSeq));
            //            }
            //            else if (temp.Name != name)
            //            {
            //                AddErrorMsg(item, string.Format("레이어 불 일치 : 부모순번 {0}, 순서 {1}, {2} != {3}", x.Index, layerSeq, temp.Name, name));
            //            }
            //        }
            //    }
            //}
            #endregion
        }


        private void MP_CE_WD_LS_0003(ValidateVM item)
        {
            #region 레이어의 Show/Hide 확인(JC)

            var layerList = DSPsdLayerVMList.ToList();
            var list = LayerNameManager.Instance.CEList;// <-----★
            var name = string.Empty;

            var transparencyColor = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L12_Transparency_Color);
            var roadBacgroundColor = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L13_Road_Background_Color);
            foreach (var x in layerList)
            {
                if (x.Index <= LayerIndex.CE.GetFixedLayerIndex())// <-----★
                {
                    var z = list.SingleOrDefault(y => y.Index == x.Index);
                    if (z == null)
                    {
                        //AddErrorMsg(item, x.Index, string.Format("[{0}] 기준 레이어 검색 실패", x.Name));
                    }
                    else if (x.IsVisible != z.IsShow)
                    {
                        AddErrorMsg(item, x.Index, string.Format("[{0}] Show ({1}) 설정 오류 : {2}", x.Name, z.IsShow, x.IsVisible));
                    }
                }
                else
                {
                    if (x.Name == transparencyColor || x.Name == roadBacgroundColor)
                    {
                        if (x.IsVisible == true)
                            AddErrorMsg(item, x.Index, string.Format("[{0}] Show (false) 설정 오류 : {1}", x.Name, x.IsVisible));
                    }
                    else
                    {
                        if (x.IsVisible == false)
                            AddErrorMsg(item, x.Index, string.Format("[{0}] Show (true) 설정 오류 : {1}", x.Name, x.IsVisible));
                    }
                }
            }

            #endregion 레이어의 Show/Hide 확인
        }


        private void MP_CE_WD_LS_0007(ValidateVM item)
        {
            #region 그룹명/레이어명 공란 무조건 없어야 한다.
            //1단계
            var nightSky = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L01_Night_Sky);
            var daySky = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L03_DaySky);
            var roadBackgroundColor = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L13_Road_Background_Color);
            var isContainEmpty = DSPsdLayerVMList.Where(_o => _o.Name != roadBackgroundColor && _o.Name != daySky
            && _o.Name != nightSky && _o.Name.Contains(" ")).ToList();
            if (isContainEmpty.Count > 0)
            {
                foreach (var containEmptylayer in isContainEmpty)
                {
                    AddErrorMsg(item, containEmptylayer.Index, string.Format("[{0}] 공백이 포함되어있습니다.", containEmptylayer.Name));
                }
            }
            #endregion 그룹명/레이어명 공란 무조건 없어야 한다.
        }


        private void M_CE_WD_LS_0009(ValidateVM item)
        {
            #region CE : 레이어/레이어셋 총 개수 확인
            var fixedLayerIndex = LayerIndex.CE.GetFixedLayerIndex();//<------★
            var layerName = LayerIndex.GetName<LayerIndex.CE>(fixedLayerIndex);//<------★
            var currentLayerCount = DSPsdLayerVMList.Count;

            var arrowKRCACount = DSPsdLayerVMList.Where(x => x.Name.StartsWith(layerName) && x.LayerDepth == 2 && x.HasImage == false).Count();
            var directionSubName = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L11_Direction_Sub);
            var isDIrectionSub = DSPsdLayerVMList.Where(x => x.Index > fixedLayerIndex && x.HasImage == false && x.LayerDepth == 1 && x.Name == directionSubName).Any();
            var totalLayerCount = fixedLayerIndex;
            if (isDIrectionSub)
                totalLayerCount += 2;
            var arrowName = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L17_Arrow);
            var arrowLayer = DSPsdLayerVMList.Where(x => x.Index > fixedLayerIndex && x.HasImage == false && x.LayerDepth == 1 && x.Name == arrowName).FirstOrDefault();
            if (arrowLayer == null)
            {
                AddErrorMsg(item, string.Format("[{0}] 레이어가 없음", arrowName));
                return;
            }
            var arrowIndex = arrowLayer.Index;
            if (arrowLayer.ChildCount != 4 && arrowLayer.ChildCount != 5)
            {
                AddErrorMsg(item, string.Format("[{0}] 하위레이어의 개수 오류 (4 or 5)", arrowName));
                return;
            }

            var childLayer = DSPsdLayerVMList.SingleOrDefault(x => x.Index == arrowIndex - 1);
            if (childLayer == null)
            {
                AddErrorMsg(item, string.Format("레이어인덱스 오류 : {0}", arrowIndex - 1));
                return;
            }
            var directionName = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L16_Direction);
            if (childLayer.Name == directionName)
            {
                totalLayerCount += 6;
            }
            else
            {
                totalLayerCount += 4;
            }

            if (currentLayerCount != totalLayerCount)
            {
                AddErrorMsg(item, string.Format("레이어 총 개수 불일치 {0} != {1}", currentLayerCount, totalLayerCount));
            }
            #endregion
        }

        private void M_CE_WD_LS_0010(ValidateVM item)
        {
            #region 최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)

            var layerNameVMList = LayerNameManager.Instance.CEList.Where(x => x.ParentIndex == 0).ToList();//<----★
            var fixedIndex = LayerIndex.CE.GetFixedLayerIndex();
            var arrowName = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L17_Arrow);
            var directionSubName = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L11_Direction_Sub);
            foreach (var x in layerNameVMList)
            {
                if (x.Index <= fixedIndex)
                {
                    var psdLayer = DSPsdLayerVMList.SingleOrDefault(y => y.Index == x.Index);
                    if (psdLayer == null)
                    {
                        AddErrorMsg(item, x.Index, string.Format("[{0}] 레이어 못 찾음", x.Name));
                    }
                    else if (x.Name != psdLayer.Name)
                    {
                        AddErrorMsg(item, psdLayer.Index, string.Format("[{0}] 레이어 이름 불일치 : {1}", x.Name, psdLayer.Name));
                    }
                }
                else
                {
                    if (x.Name != arrowName && x.Name != directionSubName)
                    {
                        AddErrorMsg(item, x.Index, string.Format("[{0}] 레이어 이름 불일치", x.Name));
                    }
                }
            }
            #endregion
        }

        private void M_CE_WD_LS_0011(ValidateVM item)
        {
            #region CE : 가장 마지막 레이어셋은 Arrow

            //1단계
            var layerArrowName = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L17_Arrow);

            var layerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 1 && x.Name == layerArrowName && x.HasImage == false).ToList();
            if (layerList.Count() != 1)
            {
                AddErrorMsg(item, "최상위 레이어셋 중 Arrow 폴더는 1개만 존재해야 합니다.");
                return;
            }

            var layerCount = DSPsdLayerVMList.Count();
            var lastLayer = layerList.First();


            if (layerCount != lastLayer.Index)
            {
                AddErrorMsg(item, string.Format("최상위 레이어셋 Arrow폴더의 순번(Index)는 레이어 개수와 일치해야 합니다. Index {0} != Count {1} ",
                    lastLayer.Index, layerCount));
                return;
            }

            var name1 = lastLayer.Name;
            if (name1 != layerArrowName)
            {
                AddErrorMsg(item, string.Format("최상위 레이어셋이름[{0}] 불일치 : {1}", layerArrowName, name1));
                return;
            }
            #endregion
        }

        private void P_ET_WD_FM_0006(ValidateVM item)
        {
            #region ET : PSD Mode (패턴 > Image Size > Width 800 / Height 600 )
            //var width = _psdFileSectionVM.Width; // txtFileSectionWidth.Text;
            //var height = _psdFileSectionVM.Height;
            //if (width != 800 || height != 600)
            //{
            //    AddErrorMsg(item, string.Format("Width/Height (800x600) : {0}x{1}", width, height));
            //}
            #endregion
        }
        private void M_ET_WD_FM_0006(ValidateVM item)
        {
            #region ET : PSD Mode (이미지 > Image Size > Width 2048 / Height 2048 )
            var width = _psdFileSectionVM.Width; // txtFileSectionWidth.Text;
            var height = _psdFileSectionVM.Height;
            if ((width == 2048 && height == 2048) || (width == 800 && height == 600))
            { }
            else
                AddErrorMsg(item, string.Format("사이즈 오류 : {0}x{1}", width, height));
            #endregion
        }

        private void MP_ET_WD_MC_0001(ValidateVM item)
        {
            #region ETC : 파일명 글자 개수 21자

            var fileNameLen = _psdFileVM.Name.Length;
            var validMainCodeLen = LayerNameManager.Instance.ET_MainCode_Length;
            if (fileNameLen != LayerNameManager.Instance.ET_MainCode_Length)
            {
                AddErrorMsg(item, string.Format("메인코드 길이({0}) 오류 : {1} {2}", validMainCodeLen, fileNameLen, _psdFileVM.Name));
            }
            #endregion
        }

        private void MP_ET_WD_LS_0007(ValidateVM item)
        {
            #region 그룹명/레이어명 공란 무조건 없어야 한다.
            //1단계
            var nightSky = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L01_Night_Sky);
            var daySky = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L03_DaySky);
            var hipassRedRoad = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L11_Hipass_Red_Road);
            var hipassArrow = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L12_Hipass_Arrow);
            var signPost = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L18_Sign_post);
            var isContainEmpty = DSPsdLayerVMList.Where(_o => _o.Name != hipassRedRoad && _o.Name != hipassArrow && _o.Name != signPost && _o.Name != daySky
            && _o.Name != nightSky && _o.Name.Contains(" ")).ToList();
            if (isContainEmpty.Count > 0)
            {
                foreach (var containEmptylayer in isContainEmpty)
                {
                    AddErrorMsg(item, containEmptylayer.Index, string.Format("[{0}] 공백이 포함되어있습니다.", containEmptylayer.Name));
                }
            }
            #endregion 그룹명/레이어명 공란 무조건 없어야 한다.
        }

        private void M_ET_WD_AC_0001(ValidateVM item)
        {
            #region ET : 파일명 KREI 는 그룹명 'ETC_'로 시작해야한다. (예:ETC_1)

            var fileName = _psdFileVM.Name;

            var pattern = "^KREI([a-zA-Z0-9]){1,}[a-zA-Z0-9]$";
            var regEx = new Regex(pattern);
            var match = regEx.Match(fileName);

            if (match.Success == false)
            {
                AddErrorMsg(item, string.Format("메인코드명 오류 : 파일명(메인코드) {0}", fileName));
            }
            else
            {
                var layerIndex = LayerIndex.ET.L20_ETC;
                var layerNameStartWidth = LayerIndex.GetName<LayerIndex.ET>(layerIndex);
                var etcLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 1 && x.Name == (layerNameStartWidth)).ToList();

                if (etcLayerList.Count != 1)
                {

                }
                else
                {
                    var etcLayer = etcLayerList.FirstOrDefault();
                    string outlayerName = string.Empty;
                    int outLayerIndex = 0;
                    var childList = GetChildLayerListByDefaultParentIndex<LayerIndex.ET>(layerIndex, ref outlayerName, ref outLayerIndex);

                    layerIndex = LayerIndex.ET.L19_ETC_;
                    layerNameStartWidth = LayerIndex.GetName<LayerIndex.ET>(layerIndex);

                    foreach (var etc_ in childList.Where(x => x.HasImage == false))
                    {
                        if (etc_.Name.StartsWith(layerNameStartWidth) == false)
                        {
                            AddErrorMsg(item, string.Format("KREI 그룹명이 [{0}]로 시작하지 않습니다.", etc_.Name));
                        }
                    }
                }

                //var isETC = DSPsdLayerVMList.FirstOrDefault();
                //if (isETC.Name != LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L20_ETC))
                //{

                //}
            }
            #endregion
        }

        private void M_ET_WD_AC_0002(ValidateVM item)
        {
            #region 'ETC_'로 시작하는 그룹은 0개 이상이다.

            var layerIndex = LayerIndex.ET.L19_ETC_;
            var layerNameStartWidth = LayerIndex.GetName<LayerIndex.ET>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWidth)).ToList();
            //if (arrowLayerList.Count() < 1)
            //{
            //    AddErrorMsg(item, string.Format("{0}로 시작하는 폴더명은 최소 0개 이상입니다.", layerNameStartWidth));
            //}

            #endregion
        }


        private void M_ET_WD_AC_0004(ValidateVM item)
        {
            #region ETC그룹내 그룹명 글자갯수 $5
            var layerIndex = LayerIndex.ET.L19_ETC_;
            var layerNameStartWith = LayerIndex.GetName<LayerIndex.ET>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWith)).ToList();
            if (arrowLayerList.Any() == false)
            {
                //AddErrorMsg(item, string.Format("레이어 못 찾음 :  {0}, {1}", layerIndex, layerNameStartWith));
            }
            else
            {
                var validLength = LayerNameManager.Instance.ET_ArrowCode_Length;
                foreach (var x in arrowLayerList)
                {
                    if (x.Name.Length != validLength)
                    {
                        AddErrorMsg(item, x.Index, string.Format("ETC 코드 길이({0}) 오류 : {1} ", validLength, x.Name));
                    }
                }
            }
            #endregion
        }


        private void MP_ET_WD_LS_0001(ValidateVM item)
        {
            #region CE : 레이어구조 고정 B Type
            var fixedLayerIndex = LayerIndex.ET.GetFixedLayerIndex();// <-----★
            var layerNameList = LayerNameManager.Instance.ETList.Where(x => x.Index <= fixedLayerIndex);

            foreach (var x in layerNameList)
            {
                if (CheckLayer(item, DSPsdLayerVMList, x) == false) { }
            }

            #endregion
        }

        private void MP_ET_WD_LS_0002(ValidateVM item)
        {
            #region CE : 레이어구조 가변 B Type
            var parentLayerName = string.Empty;
            var parentLayerIndex = 0;
            var childList = GetChildLayerListByDefaultParentIndex<LayerIndex.ET>(LayerIndex.ET.L20_ETC, ref parentLayerName, ref parentLayerIndex);
            if (childList == null)
            {
                //ETC 비어있음
            }
            else
            {
                foreach (var etc_ in childList)
                {
                    if (etc_.ChildCount != 8)
                    {
                        AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 하위 레이어 갯수 오류", etc_.Name));
                        continue;
                    }
                    childList = DSPsdLayerVMList.Where(x => x.ParentIndex == etc_.Index).ToList();
                    if (childList == null)
                    {
                        //ETC_ 비어있음
                        AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 하위 레이어 오류", etc_.Name));
                        continue;
                    }
                    var name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L18_Sign_post);
                    var name2 = childList[0].Name;
                    var index = childList[0].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                    name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L17_Hipass);
                    name2 = childList[1].Name;
                    index = childList[1].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                    name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L16_Hipass1);
                    name2 = childList[2].Name;
                    index = childList[2].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                    name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L15_Column);
                    name2 = childList[3].Name;
                    index = childList[3].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                    name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L14_Direction_Road);
                    name2 = childList[4].Name;
                    index = childList[4].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                    name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L13_Direction_Main);
                    name2 = childList[5].Name;
                    index = childList[5].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                    name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L12_Hipass_Arrow);
                    name2 = childList[6].Name;
                    index = childList[6].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                    name1 = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L11_Hipass_Red_Road);
                    name2 = childList[7].Name;
                    index = childList[7].Index;
                    if (name2 != name1)
                    {
                        AddErrorMsg(item, index, string.Format("[{0}] 레이어명 불일치 {1}", name1, name2));
                    }
                }
            }
            #endregion
        }

        private void MP_ET_WD_LS_0003(ValidateVM item)
        {
            #region 레이어의 Show/Hide 확인(JC)
            var layerList = DSPsdLayerVMList.ToList();
            var list = LayerNameManager.Instance.ETList;// <-----★
            var name = string.Empty;

            var transparencyColor = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L12_Transparency_Color);
            var roadBacgroundColor = LayerIndex.GetName<LayerIndex.CE>(LayerIndex.CE.L13_Road_Background_Color);


            foreach (var x in layerList)
            {
                if (x.Index <= LayerIndex.ET.GetFixedLayerIndex())// <-----★
                {
                    var z = list.SingleOrDefault(y => y.Index == x.Index);
                    if (z == null)
                    {
                        AddErrorMsg(item, x.Index, string.Format("[{0}] 기준 레이어 검색 실패", x.Name));
                    }
                    else if (x.IsVisible != z.IsShow)
                    {
                        AddErrorMsg(item, x.Index, string.Format("[{0}] Show ({1}) 설정 오류 : {2}", x.Name, z.IsShow, x.IsVisible));
                    }
                }
                else
                {
                    if (x.IsVisible == false)
                        AddErrorMsg(item, x.Index, string.Format("[{0}] Show (true) 설정 오류 : {1}", x.Name, x.IsVisible));
                }
            }
            #endregion 레이어의 Show/Hide 확인
        }
        private void M_ET_WD_LS_0009(ValidateVM item)
        {
            #region 레이어 총개수

            var etcLayerName = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L19_ETC_);
            var layerCount = DSPsdLayerVMList.Count;

            var etc_Count = DSPsdLayerVMList.Where(x => x.Name.StartsWith(etcLayerName) && x.LayerDepth == 2 && x.HasImage == false).Count();
            var totalLayerCount = LayerIndex.ET.GetFixedLayerIndex() + (9 * etc_Count) + 1;
            if (layerCount != totalLayerCount)
            {
                AddErrorMsg(item, string.Format("레이어 총 개수 불일치 {0} != {1}", layerCount, totalLayerCount));
            }

            #endregion 레이어 총개수
        }

        private void M_ET_WD_LS_0010(ValidateVM item)
        {
            #region 최상위 레이어셋 구조 : 6개 (ETC, …, Night_on_Sky)

            var layerNameVMList = LayerNameManager.Instance.ETList.Where(x => x.ParentIndex == 0).ToList();//<----★
            var fixedIndex = LayerIndex.ET.GetFixedLayerIndex();
            var etcName = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L20_ETC);
            foreach (var x in layerNameVMList)
            {
                if (x.Index <= fixedIndex)
                {
                    var psdLayer = DSPsdLayerVMList.SingleOrDefault(y => y.Index == x.Index);
                    if (psdLayer == null)
                    {
                        AddErrorMsg(item, x.Index, string.Format("[{0}] 레이어 못 찾음", x.Name));
                    }
                    else if (x.Name != psdLayer.Name)
                    {
                        AddErrorMsg(item, psdLayer.Index, string.Format("[{0}] 레이어 이름 불일치 : {1}", x.Name, psdLayer.Name));
                    }
                }
                else
                {
                    var etcLayer = DSPsdLayerVMList.Where(y => y.Index > fixedIndex && y.ParentIndex == 0 && y.Name == etcName).FirstOrDefault();
                    if (etcLayer == null)
                    {
                        AddErrorMsg(item, x.Index, string.Format("[{0}] 레이어 이름 불일치", x.Name));
                    }
                }
            }
            #endregion
        }
        private void M_ET_WD_LS_0011(ValidateVM item)
        {
            #region CE : 가장 마지막 레이어셋은 Arrow
            //1단계
            var layerArrowName = LayerIndex.GetName<LayerIndex.ET>(LayerIndex.ET.L20_ETC);

            var layerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 1 && x.Name == layerArrowName && x.HasImage == false).ToList();
            if (layerList.Count() != 1)
            {
                AddErrorMsg(item, "최상위 레이어셋 중 ETC 폴더는 1개만 존재해야 합니다.");
                return;
            }

            var layerCount = DSPsdLayerVMList.Count();
            var lastLayer = layerList.First();


            if (layerCount != lastLayer.Index)
            {
                AddErrorMsg(item, string.Format("최상위 레이어셋 ETC폴더의 순번(Index)는 레이어 개수와 일치해야 합니다. Index {0} != Count {1} ",
                    lastLayer.Index, layerCount));
                return;
            }

            var name1 = lastLayer.Name;
            if (name1 != layerArrowName)
            {
                AddErrorMsg(item, string.Format("최상위 레이어셋이름[{0}] 불일치 : {1}", layerArrowName, name1));
                return;
            }
            #endregion
        }

        private void M_JC_WD_MC_0001(ValidateVM item)
        {
            #region JC : 파일명 글자갯수 22자
            //var fileName = _psdFileVM.Name;
            var fileNameLen = _psdFileVM.Name.Length;
            var validMainCodeLen = LayerNameManager.Instance.JC_MainCode_Length;
            if (fileNameLen != LayerNameManager.Instance.JC_MainCode_Length)
            {
                AddErrorMsg(item, string.Format("메인코드 길이({0}) 오류 : {1} {2}", validMainCodeLen, fileNameLen, _psdFileVM.Name));
            }
            #endregion
        }

        private void M_JC_WD_AC_0001(ValidateVM item)
        {
            #region 메인코드 포맷 (JC)
            var fileName = _psdFileVM.Name;

            var pattern = "^KRJM([a-zA-Z0-9]){0,}[a-zA-Z0-9]$";
            var regEx = new Regex(pattern);
            var match = regEx.Match(fileName);

            if (match.Success == false)
            {
                AddErrorMsg(item, string.Format("메인코드명 불일치 : 파일명(메인코드) {1}", pattern, fileName));
                return;
            }

            var layerIndex = LayerIndex.NC.Arrow_KRCA;
            var layerNameStartWidth = LayerIndex.GetName<LayerIndex.NC>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWidth)).ToList();
            if (arrowLayerList.Count() >= 1)
            {
                var list = LayerNameManager.Instance.NCList;
                pattern = @"^Arrow_KRJA([a-zA-Z0-9]){1,}[a-zA-Z0-9]$";
                regEx = new Regex(pattern);

                foreach (var x in arrowLayerList)
                {
                    match = regEx.Match(x.Name);
                    if (match.Success == false)
                    {
                        AddErrorMsg(item, x.Index, string.Format("레이어명 오류 :  명칭 {1}", x.Name));
                    }
                }
            }
            #endregion 메인코드 포맷 (JC)
        }

        private void M_JC_WD_AC_0002(ValidateVM item)
        {
            #region Arrow 레이어셋 1개 이상인가?
            var layerIndex = LayerIndex.JC.Arrow_KRJA;
            var layerNameStartWith = LayerIndex.GetName<LayerIndex.JC>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWith)).ToList();
            if (arrowLayerList.Count() < 1)
            {
                AddErrorMsg(item, string.Format("{0} 폴더는 최소 1개 이상입니다.", layerNameStartWith));
            }

            #endregion Arrow 레이어셋 1개 이상인가?
        }

        private void M_JC_WD_AC_0003(ValidateVM item)
        {
            #region 멀티개의 Arrow 레이어셋 명칭이 Arrow_KRJA***로 시작하는가? (공백, 특수문자 확인)
            var layerIndex = LayerIndex.JC.Arrow_KRJA;
            var layerNameStartWith = LayerIndex.GetName<LayerIndex.JC>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWith)).ToList();
            if (arrowLayerList.Count() >= 1)
            {
                var list = LayerNameManager.Instance.JCList;
                var pattern = @"^Arrow_KRJA([a-zA-Z0-9]){1,}[a-zA-Z0-9]$";
                var regEx = new Regex(pattern);

                foreach (var x in arrowLayerList)
                {
                    var match = regEx.Match(x.Name);
                    if (match.Success == false)
                    {
                        AddErrorMsg(item, x.Index, string.Format("레이어명 오류 :  명칭 {1}", pattern, x.Name));
                    }
                }
            }

            #endregion 멀티개의 Arrow 레이어셋 명칭이 Arrow_KRMC***로 시작하는가? (공백, 특수문자 확인)
        }

        private void M_JC_WD_AC_0004(ValidateVM item)
        {
            #region JC Arrow 그룹내 그룹명 글자갯수 $28
            var layerIndex = LayerIndex.JC.Arrow_KRJA;
            var layerNameStartWith = LayerIndex.GetName<LayerIndex.JC>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWith)).ToList();
            if (arrowLayerList.Any() == false)
            {
                AddErrorMsg(item, string.Format("레이어 못 찾음 :  {0}, {1}", layerIndex, layerNameStartWith));
            }
            else
            {
                var validLength = LayerNameManager.Instance.JC_ArrowCode_Length;
                foreach (var x in arrowLayerList)
                {

                    if (x.Name.Length != validLength)
                    {
                        AddErrorMsg(item, x.Index, string.Format("Arrow 코드 길이({0}) 오류 : {1} ", validLength, x.Name));
                    }
                }
            }

            #endregion 멀티개의 Arrow 레이어셋 명칭이 Arrow_KRMC***로 시작하는가? (공백, 특수문자 확인)
        }

        private void M_JC_WD_LS_0006(ValidateVM item)
        {
            #region Arrow->Direction - Opacity 100%
            var dftArrowIndex = LayerIndex.JC.Arrow;
            string parentLayerName = string.Empty;
            int parentLayerIndex = 0;
            var arrowChildList = GetChildLayerListByDefaultParentIndex<LayerIndex.JC>(dftArrowIndex, ref parentLayerName, ref parentLayerIndex);
            foreach (var x in arrowChildList.Where(a => a.HasImage == false))
            {
                var childList = DSPsdLayerVMList.Where(y => y.ParentIndex == x.Index).ToList();
                var childCount = childList.Count();
                if (childCount != 3)
                {
                    AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 하위레이어개수(3) 불일치 : {1}", parentLayerName, childCount));
                    continue;
                }

                var idx = 0;
                var layer = childList[idx];
                var name1 = layer.Name;
                var name2 = LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Arrow_KRCA_Direction);
                if (name1 != name2)
                {
                    //AddErrorMsg(item, layer.Index, string.Format("[{0}] 레이어 명칭 불일치 : {1}", name1, name2));
                }
                else
                {
                    if (layer.Opacity != 1)
                    {
                        AddErrorMsg(item, layer.Index, string.Format("[{0}] Opacity 1.0(100%) 오류 : {1}", name1, layer.Opacity));
                    }
                }

                ++idx;
                layer = childList[idx];
                name1 = layer.Name;
                name2 = LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Arrow_KRCA_Transparency);
                if (name1 != name2)
                {
                    //AddErrorMsg(item, layer.Index, string.Format("[{0}] 레이어 명칭 불일치 : {1}", name1, name2));
                }
                else
                {
                    //if (layer.Opacity < 0.5 || layer.Opacity >= 0.505)
                    //    AddErrorMsg(item, layer.Index, string.Format("[{0}] Opacity 1.0 오류 : {1}", name1, layer.Opacity));
                }
                ++idx;
                layer = childList[idx];
                name1 = layer.Name;
                name2 = LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Arrow_KRCA_Arrow);
                if (name1 != name2)
                {
                    //AddErrorMsg(item, layer.Index, string.Format("[{0}] 레이어 명칭 불일치 : {1}", name1, name2));
                }
            }
            #endregion Arrow->Direction - Opacity 100%
        }

        private void M_JC_WD_LS_0007(ValidateVM item)
        {
            #region 일반교차로 : Arrow->Transparency - Opacity 50%

            var dftArrowIndex = LayerIndex.JC.Arrow;
            string parentLayerName = string.Empty;
            int parentLayerIndex = 0;
            var arrowChildList = GetChildLayerListByDefaultParentIndex<LayerIndex.JC>(dftArrowIndex, ref parentLayerName, ref parentLayerIndex);
            foreach (var x in arrowChildList.Where(a => a.HasImage == false))
            {
                var childList = DSPsdLayerVMList.Where(y => y.ParentIndex == x.Index).ToList();
                var childCount = childList.Count();
                if (childCount != 3)
                {
                    AddErrorMsg(item, parentLayerIndex, string.Format("[{0}] 하위레이어개수(3) 불일치 : {1}", parentLayerName, childCount));
                    continue;
                }

                var idx = 0;
                var layer = childList[idx];
                var name1 = layer.Name;
                var name2 = LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Arrow_KRCA_Direction);
                if (name1 != name2)
                {
                    //AddErrorMsg(item, layer.Index, string.Format("[{0}] 레이어 명칭 불일치 : {1}", name1, name2));
                }
                else
                {
                    //if (layer.Opacity != 1)
                    //{
                    //    //AddErrorMsg(item, layer.Index, string.Format("[{0}] Opacity 1.0(100%) 오류 : {1}", name1, layer.Opacity));
                    //}
                }

                ++idx;
                layer = childList[idx];
                name1 = layer.Name;
                name2 = LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Arrow_KRCA_Transparency);
                if (name1 != name2)
                {
                    //AddErrorMsg(item, layer.Index, string.Format("[{0}] 레이어 명칭 불일치 : {1}", name1, name2));
                }
                else
                {
                    if (layer.Opacity < 0.5 || layer.Opacity >= 0.505)
                        AddErrorMsg(item, layer.Index, string.Format("[{0}] Opacity 1.0 오류 : {1}", name1, layer.Opacity));
                }
                ++idx;
                layer = childList[idx];
                name1 = layer.Name;
                name2 = LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Arrow_KRCA_Arrow);
                if (name1 != name2)
                {
                    //AddErrorMsg(item, layer.Index, string.Format("[{0}] 레이어 명칭 불일치 : {1}", name1, name2));
                }
            }
            #endregion 일반교차로 : Arrow->Transparency - Opacity 50%
        }

        private void M_JC_WD_LS_0001(ValidateVM item)
        {
            #region 레이어구조 A Type : 고정부분 (JC)
            var layerIndex = LayerIndex.JC.Road_Background_Color;
            var layerNameList = LayerNameManager.Instance.JCList.Where(x => x.Index <= layerIndex);


            foreach (var x in layerNameList)
            {
                if (CheckLayer(item, DSPsdLayerVMList, x) == false) { }
            }

            #endregion Arrow 레이어셋 1개 이상인 경우 구조 확인 (레이어명, 순서)
        }

        private void M_JC_WD_LS_0002(ValidateVM item)
        {
            #region Arrow 레이어셋 1개 이상인 경우 구조 확인 (레이어명, 순서)
            var layerIndex = LayerIndex.JC.Arrow_KRJA;//<------★
            var layerName = LayerIndex.GetName<LayerIndex.JC>(layerIndex);//<------★
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerName)).ToList();
            if (arrowLayerList.Count() >= 1)
            {
                var list = LayerNameManager.Instance.JCList;//<------★
                var name = string.Empty;
                var maxLayerSeq = list.Count(x => x.IsVariable && x.LayerSeq > 0);
                foreach (var x in arrowLayerList)
                {
                    for (var layerSeq = 1; layerSeq <= maxLayerSeq; layerSeq++)
                    {
                        PsdLayerVM temp = DSPsdLayerVMList.Where(y => y.ParentIndex == x.Index && y.LayerSeq == layerSeq).FirstOrDefault();
                        if (temp == null)
                        {
                            AddErrorMsg(item, string.Format("레이어명 없음 : 부모순번 {0}, 순서 {1}", x.Index, layerSeq));
                        }
                        else if (LayerNameManager.Instance.GetNameBySeq(list, layerSeq, out name) == false)
                        {
                            AddErrorMsg(item, string.Format("비교할 기준 레이어 없음 : 부모순번 {0}, 순서 {1}", x.Index, layerSeq));
                        }
                        else if (temp.Name != name)
                        {
                            AddErrorMsg(item, string.Format("레이어 불 일치 : 부모순번 {0}, 순서 {1}, {2} != {3}", x.Index, layerSeq, temp.Name, name));
                        }
                    }
                }
            }
            #endregion Arrow 레이어셋 1개 이상인 경우 구조 확인 (레이어명, 순서)
        }

        private void M_JC_WD_LS_0008(ValidateVM item)
        {
            #region JC : Road Background_Color 0,2048,0,2048(꽉 채워져야함)

            var layerIndex = LayerIndex.JC.Road_Background_Color;
            var layerName = LayerIndex.GetName<LayerIndex.CM>(layerIndex);

            var layerList = DSPsdLayerVMList.Where(x => x.Name == layerName).ToList();
            if (layerList.Count != 1)
            {
                AddErrorMsg(item, layerIndex, string.Format("[{0}] 레이어와 동일한 레이어가 없거나 1개 이상입니다.", layerName));
                return;
            }
            var layer = layerList.FirstOrDefault();

            CheckLayerLocation(layer, item);

            #endregion 일반교차로 : Road Background_Color 0,2048,0,2048(꽉 채워져야함)
        }

        private void M_JC_WD_LS_0009(ValidateVM item)
        {
            #region JC : Road Background_Color 0,2048,0,2048(꽉 채워져야함)

            var layerIndex = LayerIndex.JC.Road_Background_Color;
            var layerName = LayerIndex.GetName<LayerIndex.CM>(layerIndex);

            var layerList = DSPsdLayerVMList.Where(x => x.Name == layerName).ToList();
            if (layerList.Count != 1)
            {
                AddErrorMsg(item, layerIndex, string.Format("[{0}] 레이어와 동일한 레이어가 없거나 1개 이상입니다.", layerName));
                return;
            }
            var layer = layerList.FirstOrDefault();

            CheckLayerLocation(layer, item);

            #endregion 일반교차로 : Road Background_Color 0,2048,0,2048(꽉 채워져야함)
        }

        private void M_JC_WD_LS_0010(ValidateVM item)
        {
            #region JC : 레이어/레이어셋 총 개수 확인
            var layerIndex = LayerIndex.JC.Arrow_KRJA;//<------★
            var layerName = LayerIndex.GetName<LayerIndex.JC>(layerIndex);//<------★
            var layerCount = DSPsdLayerVMList.Count;
            var arrowKRCACount = DSPsdLayerVMList.Where(x => x.Name.StartsWith(layerName) && x.LayerDepth == 2 && x.HasImage == false).Count();
            var totalLayerCount = LayerIndex.JC.Road_Background_Color + 4 * arrowKRCACount + 1;
            if (layerCount != totalLayerCount)
            {
                AddErrorMsg(item, string.Format("레이어 총 개수 불일치 {0} != {1}", layerCount, totalLayerCount));
            }
            #endregion 레이어/레이어셋 총 개수 확인
        }


        private void M_JC_WD_LS_0011(ValidateVM item)
        {
            #region 최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)

            var layerNameVMList = LayerNameManager.Instance.JCList.Where(x => x.ParentIndex == 0).ToList();

            foreach (var x in layerNameVMList)
            {
                var result = DSPsdLayerVMList.SingleOrDefault(y => y.Index == x.Index);
                if (result == null)
                {
                    AddErrorMsg(item, string.Format("레이어 못 찾음 순번 {0} {1}", x.Index, x.Name));
                }
                else if (x.Name != result.Name)
                {
                    AddErrorMsg(item, result.Index, string.Format("레이어 이름 불일치 {0} != {1}", x.Name, result.Name));
                }
            }
            #endregion
        }

        private void M_JC_WD_LS_0012(ValidateVM item)
        {
            #region 최상위 레이어셋 중 Arrow 가 있는가?

            //1단계
            var layerArrowName = LayerIndex.GetName<LayerIndex.JC>(LayerIndex.JC.Arrow);

            var layerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 1 && x.Name == layerArrowName).ToList();
            if (layerList.Count() != 1)
            {
                AddErrorMsg(item, "최상위 레이어셋 중 Arrow 폴더는 1개만 존재해야 합니다.");
            }
            else
            {
                var layerCount = DSPsdLayerVMList.Count();
                var layerIndex = layerList.First().Index;
                if (layerCount != layerIndex)
                {
                    AddErrorMsg(item, string.Format("최상위 레이어셋 Arrow폴더의 순번(Index)는 레이어 개수와 일치해야 합니다. Index {0} != Count {1} ",
                        layerIndex, layerCount));
                }
            }

            #endregion 최상위 레이어셋 중 Arrow 가 있는가?
        }

        private void MP_CM_WD_LS_0009(ValidateVM item)
        {
            #region 'Road Background_Color'레이어의 RGB 값 64/69/76

            var layer = DSPsdLayerVMList.Where(x => x.Index == LayerIndex.NC.Road_Background_Color).SingleOrDefault();
            var colorString = "/64/69/76";
            if (layer == null)
            {
                AddErrorMsg(item, "Road Background_Color 레이어 순번 14를 찾을 수 없습니다.");
            }
            else if (layer.ChannelARGB.Contains(colorString) == false)
            {
                AddErrorMsg(item, string.Format("Road Background_Color 칼라값 불일치 {0} != {1}", layer.ChannelARGB, colorString));
            }

            #endregion 'Road Background_Color'레이어의 RGB 값 64/69/76
        }

        private void M_CM_WD_FM_0006(ValidateVM item)
        {
            #region 공통 : PSD Mode (이미지 > Image Size > Width 2048 / Height 2048)

            var width = _psdFileSectionVM.Width; // txtFileSectionWidth.Text;
            var height = _psdFileSectionVM.Height;
            if ((width == 2048 && height == 2048) || (width == 800 && height == 600))
            { }
            else
            {
                AddErrorMsg(item, string.Format("Width/Height  : {0}x{1}", width, height));
            }

            #endregion
        }


        private void M_NC_WD_AC_0001(ValidateVM item)
        {
            #region 메인코드 포맷 (일반교차로)
            var fileName = _psdFileVM.Name;

            var pattern = "^KRCM([a-zA-Z0-9]){0,}[a-zA-Z0-9]$";
            var regEx = new Regex(pattern);
            var match = regEx.Match(fileName);

            if (match.Success == false)
            {
                AddErrorMsg(item, string.Format("메인코드명 오류 : 파일명(메인코드) {1}", pattern, fileName));
                return;
            }

            var layerIndex = LayerIndex.NC.Arrow;
            var arrowLayerName = LayerIndex.GetName<LayerIndex.NC>(layerIndex);

            var arrowLayerList = DSPsdLayerVMList.Where(o_ => o_.Name == arrowLayerName && o_.ParentIndex == 0).ToList();
            if (arrowLayerList.Count != 1)
            {

                return;
            }
            var arrowLayer = arrowLayerList.FirstOrDefault();
            var arrowKrcm = DSPsdLayerVMList.Where(o_ => o_.ParentIndex == arrowLayer.Index && o_.HasImage == false).ToList();
            if (arrowKrcm.Count() >= 1)
            {
                var list = LayerNameManager.Instance.NCList;
                pattern = @"^Arrow_KRCA([a-zA-Z0-9]){1,}[a-zA-Z0-9]$";
                regEx = new Regex(pattern);

                foreach (var x in arrowKrcm)
                {
                    match = regEx.Match(x.Name);
                    if (match.Success == false)
                    {
                        AddErrorMsg(item, x.Index, string.Format("레이어명 오류 :  명칭 {1}", pattern, x.Name));
                    }
                }
            }
            #endregion 메인코드 포맷 (일반교차로)
        }

        private void M_NC_WD_AC_0002(ValidateVM item)
        {
            #region Arrow 레이어셋 1개 이상인가?

            var layerIndex = LayerIndex.NC.Arrow_KRCA;
            var layerNameStartWidth = LayerIndex.GetName<LayerIndex.NC>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWidth)).ToList();
            if (arrowLayerList.Count() < 1)
            {
                AddErrorMsg(item, string.Format("{0}로 시작하는 폴더명은 최소 1개 이상입니다.", layerNameStartWidth));
            }
            #endregion Arrow 레이어셋 1개 이상인가?
        }

        private void M_NC_WD_AC_0003(ValidateVM item)
        {
            #region 멀티개의 Arrow 레이어셋 명칭이 Arrow_KRMC***로 시작하는가? (공백, 특수문자 확인), 포맷 확인
            var layerIndex = LayerIndex.NC.Arrow_KRCA;
            var layerNameStartWidth = LayerIndex.GetName<LayerIndex.NC>(layerIndex);
            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith(layerNameStartWidth)).ToList();
            if (arrowLayerList.Count() >= 1)
            {
                var list = LayerNameManager.Instance.NCList;
                var pattern = @"^Arrow_KRCA([a-zA-Z0-9]){1,}[a-zA-Z0-9]$";
                var regEx = new Regex(pattern);

                foreach (var x in arrowLayerList)
                {
                    var match = regEx.Match(x.Name);
                    if (match.Success == false)
                    {
                        AddErrorMsg(item, x.Index, string.Format("레이어 오류 :  명칭 {1}", pattern, x.Name));
                    }
                }
            }

            #endregion 멀티개의 Arrow 레이어셋 명칭이 Arrow_KRMC***로 시작하는가? (공백, 특수문자 확인)
        }


        private bool CheckLayer(ValidateVM item, BindingList<PsdLayerVM> list, LayerNameVM vm)
        {
            var padLayer = list.SingleOrDefault(x => x.Index == vm.Index);
            if (padLayer == null)
            {
                AddErrorMsg(item, string.Format("레이어 못찾음 [{0}] {1}", vm.Index, vm.Name));
                return false;
            }

            if (padLayer.Name != vm.Name)
            {
                AddErrorMsg(item, vm.Index, string.Format("레이어명 불일치 [{0}] {1}", padLayer.Name, vm.Name));
                return false;
            }

            return true;
        }

        private void M_NC_WD_LS_0001(ValidateVM item)
        {
            #region 레이어구조 A Type : 고정부분
            var layerIndex = LayerIndex.NC.Road_Background_Color;
            var layerNameList = LayerNameManager.Instance.NCList.Where(x => x.Index <= layerIndex);


            foreach (var x in layerNameList)
            {
                if (CheckLayer(item, DSPsdLayerVMList, x) == false) { }
            }

            #endregion Arrow 레이어셋 1개 이상인 경우 구조 확인 (레이어명, 순서)
        }

        private void M_NC_WD_LS_0002(ValidateVM item)
        {
            #region Arrow 레이어셋 1개 이상인 경우 구조 확인 (레이어명, 순서)

            var arrowLayerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith("Arrow_")).ToList();
            if (arrowLayerList.Count() >= 1)
            {
                var list = LayerNameManager.Instance.NCList;
                var name = string.Empty;
                var maxLayerSeq = list.Count(x => x.IsVariable && x.LayerSeq > 0);
                foreach (var x in arrowLayerList)
                {
                    for (var layerSeq = 1; layerSeq <= maxLayerSeq; layerSeq++)
                    {
                        PsdLayerVM temp = DSPsdLayerVMList.Where(y => y.ParentIndex == x.Index && y.LayerSeq == layerSeq).FirstOrDefault();
                        if (temp == null)
                        {
                            AddErrorMsg(item, string.Format("레이어명 없음 : 부모순번 {0}, 순서 {1}", x.Index, layerSeq));
                        }
                        else if (LayerNameManager.Instance.GetNameBySeq(list, layerSeq, out name) == false)
                        {
                            AddErrorMsg(item, string.Format("비교할 기준 레이어 없음 : 부모순번 {0}, 순서 {1}", x.Index, layerSeq));
                        }
                        else if (temp.Name != name)
                        {
                            AddErrorMsg(item, string.Format("레이어 불 일치 : 부모순번 {0}, 순서 {1}, {2} != {3}", x.Index, layerSeq, temp.Name, name));
                        }
                    }
                }
            }

            #endregion Arrow 레이어셋 1개 이상인 경우 구조 확인 (레이어명, 순서)
        }

        private void M_NC_WD_LS_0003(ValidateVM item)
        {
            #region 'Transparency_Color' 레이어의 ARGB 값 255/255/0/255

            var layer = DSPsdLayerVMList.Where(x => x.Index == LayerIndex.NC.Transparency_Color).SingleOrDefault();
            var colorString = "/255/0/255";
            if (layer == null)
            {
                AddErrorMsg(item, "Transparency_Color 레이어 순번 13를 찾을 수 없습니다.");
            }
            else if (layer.ChannelARGB.Contains(colorString) == false)
            {
                AddErrorMsg(item, string.Format("Transparency_Color 칼라값 불일치 {0} != {1}", layer.ChannelARGB, colorString));
            }

            #endregion 'Transparency_Color' 레이어의 ARGB 값 255/255/0/255
        }

        private void M_NC_WD_LS_0004(ValidateVM item)
        {
            #region 'Road Background_Color'레이어의 RGB 값 64/69/76

            var layer = DSPsdLayerVMList.Where(x => x.Index == LayerIndex.NC.Road_Background_Color).SingleOrDefault();
            var colorString = "/64/69/76";
            if (layer == null)
            {
                AddErrorMsg(item, "Road Background_Color 레이어 순번 14를 찾을 수 없습니다.");
            }
            else if (layer.ChannelARGB.Contains(colorString) == false)
            {
                AddErrorMsg(item, string.Format("Road Background_Color 칼라값 불일치 {0} != {1}", layer.ChannelARGB, colorString));
            }

            #endregion 'Road Background_Color'레이어의 RGB 값 64/69/76
        }

        private void M_NC_WD_LS_0005(ValidateVM item)
        {
            #region Road Background_Color/Transparency_Color - Arrow 그룹내 위치

            var parentLayerName = string.Empty;
            var parentLayerIndex = 0;
            var childList = GetChildLayerListByDefaultParentIndex<LayerIndex.NC>(LayerIndex.NC.Arrow, ref parentLayerName, ref parentLayerIndex);
            var childImages = childList.Where(x => x.HasImage == true).ToList();
            var roadBackgroundColorName = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Road_Background_Color);
            var transparencyColorName = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Transparency_Color);

            var isroadBackgroundColorName = childImages.Where(x => x.Name != roadBackgroundColorName).FirstOrDefault();
            if (isroadBackgroundColorName == null)
            {
                AddErrorMsg(item, string.Format("[{0}] 레이어 위치 오류", roadBackgroundColorName));
            }
            var istransparencyColorName = childImages.Where(x => x.Name != transparencyColorName).FirstOrDefault();
            if (istransparencyColorName == null)
            {
                AddErrorMsg(item, string.Format("[{0}] 레이어 위치 오류", transparencyColorName));
            }

            //var arrowLayerList = DSPsdLayerVMList.Where(_o => _o.ParentIndex == 0 && _o.HasImage == false
            //    && _o.Name == arrawName).OrderBy(x => x.Index).ToList();
            //if (arrowLayerList.Count != 1)
            //{
            //    AddErrorMsg(item, string.Format("[{0}] 레이어 그룹 오류 (개수 : {1})", arrawName, arrowLayerList.Count));
            //    return;
            //}
            //var arrowLayer = arrowLayerList.FirstOrDefault();
            //var isRightList = DSPsdLayerVMList.Where(_o => (_o.ParentIndex == arrowLayer.Index && _o.HasImage == true)).ToList();
            //var roadBackgroundColorName = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Road_Background_Color);
            //var transparencyColorName = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Transparency_Color);
            //var roadBackgroundColor = isRightList.Where(_o => _o.Name == roadBackgroundColorName).FirstOrDefault();
            //if (roadBackgroundColor == null)
            //{
            //    AddErrorMsg(item, string.Format("[{0}] 레이어 그룹 위치 오류", roadBackgroundColorName));
            //}
            //var transparencyColor = isRightList.Where(_o => _o.Name == transparencyColorName).FirstOrDefault();
            //if (transparencyColor == null)
            //{
            //    AddErrorMsg(item, string.Format("[{0}] 레이어 그룹 위치 오류", transparencyColorName));
            //}

            //var rbcLayer = DSPsdLayerVMList.SingleOrDefault(x => x.Index == LayerIndex.NC.Road_Background_Color);
            //var tcLayer = DSPsdLayerVMList.SingleOrDefault(x => x.Index == LayerIndex.NC.Transparency_Color);


            //else if (rbcLayer == null)
            //{
            //    AddErrorMsg(item, string.Format("{0} 폴더 못찾음",
            //        LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Road_Background_Color)));
            //}
            //else if (tcLayer == null)
            //{
            //    AddErrorMsg(item, string.Format("{0} 폴더 못찾음",
            //        LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Transparency_Color)));
            //}
            //else
            //{
            //    if (arrowLayer.Index != rbcLayer.ParentIndex)
            //    {
            //        AddErrorMsg(item, string.Format("Arrow폴더 불포함 : 순번 {0}, {1}, {2}", rbcLayer.Index, rbcLayer.ParentIndex, rbcLayer.Name));
            //    }
            //    if (arrowLayer.Index != tcLayer.ParentIndex)
            //    {
            //        AddErrorMsg(item, string.Format("Arrow폴더 불포함 : 순번 {0}, {1}, {2}", rbcLayer.Index, rbcLayer.ParentIndex, rbcLayer.Name));
            //    }
            //}

            //foreach(var x in layerList
            //var parentIndex = layer.ParentIndex;
            //if (layer == null)
            //{
            //    AddErrorMsg(item, "Transparency_Color 레이어 순번 13를 찾을 수 없습니다.");
            //}
            //else if (layer.ChannelData != colorString)
            //{
            //    AddErrorMsg(item, string.Format("Transparency_Color 칼라값 불일치 {0} != {1}", layer.ChannelData, colorString));
            //}

            #endregion Road Background_Color/Transparency_Color - Arrow 그룹내 위치
        }

        private void M_NC_WD_LS_0006(ValidateVM item)
        {
            #region Arrow->Direction - Opacity 100%
            //var layerIndex = LayerIndex.NC.Arrow_KRCA_Transparency;
            //var layerName = LayerIndex.GetName<LayerIndex.NC>(layerIndex);

            //var layerList = DSPsdLayerVMList.Where(x => x.Index != layerIndex);
            //foreach (var layer in layerList)
            //{
            //    if (layer.Opacity != 1)
            //    {
            //        AddErrorMsg(item, layer.Index, string.Format("[{0}] Opacity 1.0(100%) 오류 : {1}", layer.Name, layer.Opacity));
            //    }
            //}
            #endregion Arrow->Direction - Opacity 100%
        }

        private void M_NC_WD_LS_0007(ValidateVM item)
        {
            #region 일반교차로 : Arrow->Transparency - Opacity 50%

            var layerIndex = LayerIndex.NC.Arrow_KRCA_Transparency;
            var layerName = LayerIndex.GetName<LayerIndex.NC>(layerIndex);

            var layer = DSPsdLayerVMList.Where(x => x.Index == layerIndex).SingleOrDefault();
            if (layer == null)
            {
                AddErrorMsg(item,
                    string.Format("레이어 못찾음 : {0} 순번{1}", layerName, layerIndex));
            }
            else if (layer.Name != layerName)
            {
                AddErrorMsg(item, layer.Index,
                    string.Format("레이어명 불일치 : {0} != {1}", layer.Name, layerName));
            }
            else if (layer.Opacity < 0.50 || layer.Opacity >= 0.51)
            {
                AddErrorMsg(item, layer.Index,
                    string.Format("Opacity 0.50 <= x < 0.51 (50%) 오류 : {0}", layer.Opacity));
            }

            #endregion 일반교차로 : Arrow->Transparency - Opacity 50%
        }

        private void M_NC_WD_LS_0008(ValidateVM item)
        {
            #region 일반교차로 : Road Background_Color 0,2048,0,2048(꽉 채워져야함)

            var layerIndex = LayerIndex.NC.Road_Background_Color;
            var layerName = LayerIndex.GetName<LayerIndex.CM>(layerIndex);

            //var layer = DSPsdLayerVMList.Where(x => x.Name == layerName).SingleOrDefault();
            var layerList = DSPsdLayerVMList.Where(x => x.Name == layerName).ToList();
            if (layerList.Count != 1)
            {
                AddErrorMsg(item, layerIndex, string.Format("[{0}] 레이어와 동일한 레이어가 없거나 1개 이상입니다.", layerName));
                return;
            }
            var layer = layerList.FirstOrDefault();
            //if (layer == null)
            //{
            //    //AddErrorMsg(item,
            //    //    string.Format("레이어 못찾음 : {0} 순번{1}", layerName, layerIndex));
            //}
            //else
            //if (layer.Name != layerName)
            //{
            //    //AddErrorMsg(item, layer.Index,
            //    //    string.Format("레이어명 불일치 : {0} != {1}", layer.Name, layerName));
            //}
            //else
            CheckLayerLocation(layer, item);
            #endregion 일반교차로 : Road Background_Color 0,2048,0,2048(꽉 채워져야함)
        }


        private void M_NC_WD_LS_0009(ValidateVM item)
        {
            #region 일반교차로 : Transparency_Color 0,2048,0,2048(꽉 채워져야함)

            var layerIndex = LayerIndex.NC.Transparency_Color;
            var layerName = LayerIndex.NC.GetName(layerIndex);

            //var layer = DSPsdLayerVMList.Where(x => x.Name == layerName).SingleOrDefault();
            var layerList = DSPsdLayerVMList.Where(x => x.Name == layerName).ToList();
            if (layerList.Count != 1)
            {
                AddErrorMsg(item, layerIndex, string.Format("[{0}] 레이어와 동일한 레이어가 없거나 1개 이상입니다.", layerName));
                return;
            }
            var layer = layerList.FirstOrDefault();
            //if (layer == null)
            //{
            //    //AddErrorMsg(item,
            //    //    string.Format("레이어 못찾음 : {0} 순번{1}", layerName, layerIndex));
            //}
            //else
            //if (layer.Name != layerName)
            //{
            //    //AddErrorMsg(item, layer.Index,
            //    //    string.Format("레이어명 불일치 : {0} != {1}", layer.Name, layerName));
            //}
            //else
            //if ((layer.Left != 0 || layer.Top != 0 || layer.Right != 2048 || layer.Bottom != 2048) && (layer.Left != 0 || layer.Top != 0 || layer.Right != 800 || layer.Bottom != 600))
            CheckLayerLocation(layer, item);
            #endregion 일반교차로 : Transparency_Color 0,2048,0,2048(꽉 채워져야함)
        }

        private void MP_CM_WD_FM_0001(ValidateVM item)
        {
            #region 일반교차로 : 채널개수 = 4EA


            var layerList = DSPsdLayerVMList.ToList();
            foreach (var x in layerList)
            {
                if (x.ChannelCount != 4)
                {
                    AddErrorMsg(item, string.Format("채널개수 : {0}", x.ChannelCount));
                }
            }

            #endregion 일반교차로 : 채널개수 = 4EA
        }

        private void MP_CM_WD_FM_0002(ValidateVM item)
        {
            #region 공통 : 채널타입 - RGB/Red/Green/Blue 이외 없어야 함

            var layerList = DSPsdLayerVMList.ToList();
            foreach (var x in layerList)
            {
                if (x.ChannelTypes != "Alpha/Red/Green/Blue")
                {
                    AddErrorMsg(item, string.Format("채널타입 : {0}", x.ChannelTypes));
                }
            }

            #endregion 공통 : 채널타입 - RGB/Red/Green/Blue 이외 없어야 함
        }

        //private void MP_CM_WD_FM_0003(ValidateVM item)
        //{
        //    #region 공통 : ColorMode는 RGB 값을 갖는다.

        //    //var fileSectionColorMode = _psdFileSectionVM.ColorMode; //txtFileSectionColorMode.Text;
        //    //if (fileSectionColorMode != "RGB")
        //    //{
        //    //    AddErrorMsg(item, string.Format("FileSection.ColorMode(RGB) : {0}", fileSectionColorMode));
        //    //}

        //    #endregion 공통 : ColorMode는 RGB 값을 갖는다.
        //}

        private void MP_CM_WD_FM_0004(ValidateVM item)
        {
            #region 공통 : 파일 Depth 는 8을 갖는다..

            var fileSectionDepth = _psdFileSectionVM.Depth; // txtFileSectionDepth.Text;
            if (fileSectionDepth != 8)
            {
                AddErrorMsg(item, string.Format("FileSection.Depth(8) : {0}", fileSectionDepth));
            }

            #endregion 공통 : 파일 Depth 는 8을 갖는다..
        }

        private void MP_CM_WD_FM_0005(ValidateVM item)
        {
            #region 공통 : PSD 해상도 72pixel
            var first = DSPsdLayerVMList.FirstOrDefault();
            var last = DSPsdLayerVMList.LastOrDefault();

            if (first != null && last != null)
            {
                if (first.HorizontalRes != "72" | first.VerticalRes != "72" || last.HorizontalRes != "72" || last.VerticalRes != "72")
                {
                    AddErrorMsg(item, string.Format("픽셀값 오류 72x72 : {0}x{1}", first.HorizontalRes, last.VerticalRes));
                }
            }
            //var layerList = DSPsdLayerVMList.ToList();
            //foreach (var x in layerList)
            //{
            //    if (x.HorizontalRes != "72" || x.VerticalRes != "72")
            //    {
            //        AddErrorMsg(item, string.Format("픽셀값 오류 72x72 : {0}x{1}", x.HorizontalRes, x.VerticalRes));
            //    }
            //}
            #endregion 공통 : PSD 해상도 72pixel
        }


        private void MP_CM_WD_LS_0001(ValidateVM item)
        {
            #region 공통 : 레이어그룹(레이어셋)의 명칭들이 중복되면 안된다

            var grouping = from x in DSPsdLayerVMList
                           where x.ChildCount != 0
                           group x by x.Name into g
                           where g.Count() > 1
                           select new { NAME = g.Key, COUNT = g.Count() };

            foreach (var g in grouping)
            {
                AddErrorMsg(item, string.Format("레이어셋 이름 중복 : {0} : {1}EA", g.NAME, g.COUNT));
            }

            #endregion 공통 : 레이어그룹의 명칭들이 중복되면 안된다
        }

        private void MP_CM_WD_LS_0002(ValidateVM item)
        {
            #region 공통 : Clipping 기본 체크 해제

            var layerList = DSPsdLayerVMList.ToList();
            foreach (var x in layerList)
            {
                if (x.IsClippinig)
                {
                    AddErrorMsg(item, x.Index, string.Format("Clipping 오류 : On "));
                }
            }

            #endregion 공통 : Clipping 기본 체크 해제
        }

        private void MP_CM_WD_LS_0003(ValidateVM item)
        {
            #region 공통 : Layer Night_Filtter_B는 ARGB 값이 0/0/0/0 이여야 한다

            var layerIndex = LayerIndex.CM.Night_Filter_B;
            var layerName = LayerIndex.GetName<LayerIndex.CM>(layerIndex);
            var layer = DSPsdLayerVMList.Where(x => x.Index == layerIndex).SingleOrDefault();
            if (layer == null)
            {
                AddErrorMsg(item, string.Format("레이어 못찾음 : [{1}]{0}", layerName, layerIndex));
            }
            else if (layer.Name != layerName)
            {
                AddErrorMsg(item, string.Format("레이어명이 다름 : [{1}]{0}  = {2}", layerName, layerIndex, layer.Name));
            }
            else
            {
                var validColor = "/0/0/0";
                if (layer.ChannelARGB.Contains(validColor) == false)
                    AddErrorMsg(item, layer.Index, string.Format("칼라값({2}) 오류 : {0}, ChannelARGB  {1}", layer.Name, layer.ChannelARGB, validColor));
            }

            #endregion 공통 : Layer Night_Filtter_B는 ARGB 값이 0/0/0/0 이여야 한다
        }

        private void MP_CM_WD_LS_0004(ValidateVM item)
        {
            #region Layer Road_B는 ARGB 값이 0/0/0/0 이여야 한다
            var layerIndex = LayerIndex.CM.Road_B;
            var layer = DSPsdLayerVMList.Where(x => x.Index == layerIndex).SingleOrDefault();
            var layerName = LayerIndex.GetName<LayerIndex.CM>(layerIndex);

            if (layer == null)
            {
                AddErrorMsg(item, string.Format("레이어 못찾음 : {0}", layerName));
            }
            else if (layer.Name != layerName)
            {
                AddErrorMsg(item, string.Format("레이어명이 다름 : [{1}]{0}  = {2}", layerName, layerIndex, layer.Name));
            }
            else
            {
                var validColor = "/0/0/0";
                if (layer.ChannelARGB.Contains(validColor) == false)
                    AddErrorMsg(item, layer.Index, string.Format("칼라값({2}) 오류 : {0},  ChannelARGB {1}", layer.Name, layer.ChannelARGB, validColor));
            }

            #endregion Layer Road_B는 ARGB 값이 0/0/0/0 이여야 한다
        }

        private void MP_CM_WD_LS_0005(ValidateVM item)
        {
            #region Layer 이미지 Arrow는 ARGB 값이 0/255/0/0 이여야 한다
            int layerIndex = 0;
            string layerName = string.Empty;
            int layerDepth = 3;
            if (_ILSType == ILSType.Code1_NC)
            {
                layerIndex = LayerIndex.NC.Arrow_KRCA_Arrow;
                layerName = LayerIndex.GetName<LayerIndex.NC>(layerIndex);
            }
            else if (_ILSType == ILSType.Code2_JC)
            {
                layerIndex = LayerIndex.JC.Arrow_KRJA_Arrow;
                layerName = LayerIndex.GetName<LayerIndex.JC>(layerIndex);
            }
            else if (_ILSType == ILSType.Code3_CE)
            {
                //layerIndex = LayerIndex.CE.Arrow_KRCA_Arrow;
                //layerName = LayerIndex.GetName<LayerIndex.CE>(layerIndex);
            }
            else if (_ILSType == ILSType.Code4_ET)
            {
                //layerIndex = LayerIndex.ET.Arrow_KRCA_Arrow;
                //layerName = LayerIndex.GetName<LayerIndex.ET>(layerIndex);
            }
            else
            {
                AddErrorMsg(item, string.Format("검증코드 ILSType  오류"));
                return;
            }

            var layerList = DSPsdLayerVMList.Where(x => x.LayerDepth == layerDepth && x.HasImage && x.Name == layerName).ToList();
            if (layerList.Any() == false)
            {
                AddErrorMsg(item, string.Format("레이어 못찾음 : {0}", layerName));
            }
            else
            {
                var validColor = "/255/0/0";
                foreach (var layer in layerList)
                {

                    if (layer.ChannelARGB.Contains(validColor) == false)
                        AddErrorMsg(item, layer.Index, string.Format("칼라값({2}) 오류 : {0} = {1}", layer.Name, layer.ChannelARGB, validColor));
                }
            }

            #endregion Layer 이미지 Arrow는 ARGB 값이 0/255/0/0 이여야 한다
        }

        private void MP_CM_WD_LS_0006(ValidateVM item)
        {
            #region 공통 : 각 레이어 별로 Lock이 걸려있는지 확인 (이미지 레이어만 해당됨)

            var layerList = DSPsdLayerVMList.ToList();
            if (layerList.Any() == false)
            {
                AddErrorMsg(item, string.Format("레이어가 없습니다."));
            }
            else
            {
                foreach (var layer in layerList)
                {
                    if (layer.IsLock)
                        AddErrorMsg(item, layer.Index, string.Format("IsLock On : {0}", layer.Name));
                }
            }

            #endregion 공통 : 각 레이어 별로 Lock이 걸려있는지 확인 (이미지 레이어만 해당됨)
        }

        private void MP_CM_WD_LS_0007(ValidateVM item)
        {
            #region 그룹명/레이어명 공란 무조건 없어야 한다.
            //1단계
            var nightSky = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Night_Sky);
            var daySky = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.DaySky);
            var roadBackgroundColor = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Road_Background_Color);
            var isContainEmpty = DSPsdLayerVMList.Where(_o => _o.Name != roadBackgroundColor && _o.Name != daySky
            && _o.Name != nightSky && _o.Name.Contains(" ")).ToList();
            if (isContainEmpty.Count > 0)
            {
                foreach (var containEmptylayer in isContainEmpty)
                {
                    AddErrorMsg(item, containEmptylayer.Index, string.Format("[{0}] 공백이 포함되어있습니다.", containEmptylayer.Name));
                }
            }
            #endregion 그룹명/레이어명 공란 무조건 없어야 한다.
        }

        private void MP_CM_WD_LS_0008(ValidateVM item)
        {
            #region 공통 : Transparency를 제외한 모든 Opacity 값은 100이여야 한다
            var layerIndex = LayerIndex.NC.Arrow_KRCA_Transparency;
            var layerName = LayerIndex.GetName<LayerIndex.NC>(layerIndex);

            var layerList = DSPsdLayerVMList.Where(x => x.Name != layerName);
            foreach (var layer in layerList)
            {
                if (layer.Opacity != 1)
                {
                    AddErrorMsg(item, layer.Index, string.Format("[{0}] Opacity 1.0(100%) 오류 : {1}", layer.Name, layer.Opacity));
                }
            }
            #endregion 공통 : Transparency를 제외한 모든 Opacity 값은 100이여야 한다
        }

        //private void MP_CM_WD_LS_0007(ValidateVM item)
        //{
        //    #region 그룹명/레이어명 공란 무조건 없어야 한다.
        //    //1단계
        //    var nightSky = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Night_Sky);
        //    var daySky = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.DaySky);
        //    var roadBackgroundColor = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Road_Background_Color);
        //    var isContainEmpty = DSPsdLayerVMList.Where(_o => _o.Name != roadBackgroundColor && _o.Name != daySky
        //    && _o.Name != nightSky && _o.Name.Contains(" ")).ToList();
        //    if (isContainEmpty.Count > 0)
        //    {
        //        foreach (var containEmptylayer in isContainEmpty)
        //        {
        //            AddErrorMsg(item, containEmptylayer.Index, string.Format("[{0}] 공백이 포함되어있습니다.", containEmptylayer.Name));
        //        }
        //    }
        //    #endregion 그룹명/레이어명 공란 무조건 없어야 한다.
        //}

        //private void MP_CM_WD_LS_0007(ValidateVM item)
        //{
        //    #region 그룹명/레이어명 공란 무조건 없어야 한다.
        //    //1단계
        //    var nightSky = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Night_Sky);
        //    var daySky = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.DaySky);
        //    var roadBackgroundColor = LayerIndex.GetName<LayerIndex.CM>(LayerIndex.CM.Road_Background_Color);
        //    var isContainEmpty = DSPsdLayerVMList.Where(_o => _o.Name != roadBackgroundColor && _o.Name != daySky
        //    && _o.Name != nightSky && _o.Name.Contains(" ")).ToList();
        //    if (isContainEmpty.Count > 0)
        //    {
        //        foreach (var containEmptylayer in isContainEmpty)
        //        {
        //            AddErrorMsg(item, containEmptylayer.Index, string.Format("[{0}] 공백이 포함되어있습니다.", containEmptylayer.Name));
        //        }
        //    }
        //    #endregion 그룹명/레이어명 공란 무조건 없어야 한다.
        //}

        private void MP_NC_WD_LS_0008(ValidateVM item)
        {
            #region 레이어의 Show/Hide 확인

            var layerList = DSPsdLayerVMList.ToList();
            var list = LayerNameManager.Instance.NCList;
            var name = string.Empty;
            foreach (var x in layerList)
            {
                if (x.Index <= LayerIndex.NC.Road_Background_Color)
                {
                    var z = list.SingleOrDefault(y => y.Index == x.Index);
                    if (z == null)
                    {
                        AddErrorMsg(item, string.Format("기준 레이어(NormalCross) 검색 실패: 순번 {0}, 이름 {1}, {2} != {3}", x.Index, x.Name));
                    }
                    else if (x.IsVisible != z.IsShow)
                    {
                        AddErrorMsg(item, string.Format("Show 설정 오류 :  순번 {0}, 이름 {1}", x.Index, x.Name));
                    }
                }
                else
                {
                    if (x.IsVisible == false)
                    {
                        AddErrorMsg(item, string.Format("Show 설정 오류 :  순번 {0}, 이름 {1}", x.Index, x.Name));
                    }
                }
            }

            #endregion 레이어의 Show/Hide 확인
        }

        private void MP_CM_WD_MC_0001(ValidateVM item)
        {
            #region 공통 : 파일 확장자명은 반드시 *.PSD

            var fileExtension = _psdFileVM.Extension;
            if (string.IsNullOrWhiteSpace(fileExtension)
                || fileExtension.Equals("PSD", StringComparison.CurrentCultureIgnoreCase) == false)
            {
                AddErrorMsg(item, string.Format("파일 확장자 불일치 : {0}", fileExtension));
            }

            #endregion 공통 : 파일 확장자명은 반드시 *.PSD
        }

        private void MP_CM_WD_MC_0002(ValidateVM item)
        {
            #region 공통 : 파일명(메인코드) 길이 = 22EA

            var fileNameLen = _psdFileVM.Name.Length;
            var mainCodeLen = LayerNameManager.Instance.NormalCross_MainCode_Length;
            if (fileNameLen != LayerNameManager.Instance.NormalCross_MainCode_Length)
            {
                AddErrorMsg(item, string.Format("메인코드 길이오류 : {0}, {1}", fileNameLen, mainCodeLen));
            }
            #endregion 공통 : 파일명(메인코드) 길이 = 22EA
        }

        private void MP_NC_WD_AC_0001(ValidateVM item)
        {
            #region 이미지+일반교차로 : 일반교차로 Arrow 레이어셋 하위 그룹명 Arrow_KRCA*** 길이는 28

            var layerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 2 && x.Name.StartsWith("Arrow_KRCA")).ToList();
            foreach (var x in layerList)
            {
                if (x.Name.Length != LayerNameManager.Instance.NormalCross_ArrowCode_Length)
                {
                    AddErrorMsg(item, string.Format("Arrow Code 길이 : {0}, {1}", x.Name.Length, x.Name));
                }
            }

            #endregion 이미지+일반교차로 : 일반교차로 Arrow 레이어셋 하위 그룹명 Arrow_KRCA*** 길이는 28
        }

        private void M_NC_WD_LS_0010(ValidateVM item)
        {
            #region 레이어 총개수

            var layerCount = DSPsdLayerVMList.Count;
            var arrowKRCACount = DSPsdLayerVMList.Where(x => x.Name.StartsWith("Arrow_KRCA") && x.LayerDepth == 2 && x.HasImage == false).Count();
            var totalLayerCount = LayerIndex.NC.Road_Background_Color + 4 * arrowKRCACount + 1;
            if (layerCount != totalLayerCount)
            {
                AddErrorMsg(item, string.Format("레이어 총 개수 불일치 {0} != {1}", layerCount, totalLayerCount));
            }

            #endregion 레이어 총개수
        }

        private void M_NC_WD_LS_0011(ValidateVM item)
        {
            #region 최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)

            var layerNameVMList = LayerNameManager.Instance.NCList.Where(x => x.ParentIndex == 0).ToList();

            foreach (var x in layerNameVMList)
            {
                var result = DSPsdLayerVMList.SingleOrDefault(y => y.Index == x.Index);
                if (result == null)
                {
                    AddErrorMsg(item, string.Format("레이어 못 찾음 순번 {0} {1}", x.Index, x.Name));
                }
                else if (x.Name != result.Name)
                {
                    AddErrorMsg(item, result.Index, string.Format("레이어 이름 불일치 {0} != {1}", x.Name, result.Name));
                }
            }
            #endregion
        }

        private void M_NC_WD_LS_0012(ValidateVM item)
        {
            #region 최상위 레이어셋 중 Arrow 가 있는가?

            //1단계
            var layerArrowName = LayerIndex.GetName<LayerIndex.NC>(LayerIndex.NC.Arrow);

            var layerList = DSPsdLayerVMList.Where(x => x.LayerDepth == 1 && x.Name == layerArrowName).ToList();
            if (layerList.Count() != 1)
            {
                AddErrorMsg(item, "최상위 레이어셋 중 Arrow 폴더는 1개만 존재해야 합니다.");
            }
            else
            {
                var layerCount = DSPsdLayerVMList.Count();
                var layerIndex = layerList.First().Index;
                if (layerCount != layerIndex)
                {
                    AddErrorMsg(item, string.Format("최상위 레이어셋 Arrow폴더의 순번(Index)는 레이어 개수와 일치해야 합니다. Index {0} != Count {1} ",
                        layerIndex, layerCount));
                }
            }

            #endregion 최상위 레이어셋 중 Arrow 가 있는가?
        }


        private void MP_JC_WD_LS_0003(ValidateVM item)
        {
            #region 레이어의 Show/Hide 확인(JC)

            var layerList = DSPsdLayerVMList.ToList();
            var list = LayerNameManager.Instance.JCList;// <-----★
            var name = string.Empty;
            foreach (var x in layerList)
            {
                if (x.Index <= LayerIndex.JC.Road_Background_Color)// <-----★
                {
                    var z = list.SingleOrDefault(y => y.Index == x.Index);
                    if (z == null)
                    {
                        AddErrorMsg(item, string.Format("기준 레이어(NormalCross) 검색 실패: 순번 {0}, 이름 {1}, {2} != {3}", x.Index, x.Name));
                    }
                    else if (x.IsVisible != z.IsShow)
                    {
                        AddErrorMsg(item, string.Format("Show 설정 오류 :  순번 {0}, 이름 {1}", x.Index, x.Name));
                    }
                }
                else
                {
                    if (x.IsVisible == false)
                    {
                        AddErrorMsg(item, string.Format("Show 설정 오류 :  순번 {0}, 이름 {1}", x.Index, x.Name));
                    }
                }
            }

            #endregion 레이어의 Show/Hide 확인
        }

        private void CheckLayerLocation(PsdLayerVM layer, ValidateVM item)
        {
            var w = this._psdFileSectionVM.Width;
            var h = this._psdFileSectionVM.Height;

            if (layer.Left > 0 || layer.Top > 0 || layer.Bottom < h || layer.Right < w)
            {
                AddErrorMsg(item, layer.Index, string.Format("오류 : bottom : [{0}], right :[{1}] , left 0 : [{2}], Top 0 : [{3}] ", layer.Bottom, layer.Right, layer.Left, layer.Top));
            }
        }

    }
}