using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILS_TEST_V1.Model
{
    public enum ValidationCodeType
    {
        [Description("CE : 도시고속입구 확대도 파일명 글자갯수 8자")]
        M_CE_WD_MC_0001,

        [Description("CE : 파일명은 90으로 시작한다. ")]
        M_CE_D_AC_0001,

        [Description("CE : 'D0'로 시작하는 레어어는 1개 뿐이다.")]
        M_CE_D_AC_0002,

        [Description("CE : 도시고속입구 확대도  화살표 그룹내 레이어 글자갯수 11자")]
        M_CE_WD_AC_0004,

        [Description("CE : 레이어구조 고정 B Type")]
        M_CE_WD_LS_0001,

        [Description("CE : 레이어구조 가변 B Type")]
        M_CE_WD_LS_0002,

        [Description("CE : 레이어별 show/hide")]
        MP_CE_WD_LS_0003,

        [Description("CE : 레이어별 공백포함 확인")]
        MP_CE_WD_LS_0007,


        [Description("CE : 레이어/레이어셋 총 개수 확인")]
        M_CE_WD_LS_0009,


        [Description("CE : 최상위 레이어셋 갯수 : 5 or 6")]
        M_CE_WD_LS_0010,

        [Description("CE : 가장 마지막 레이어셋은 Arrow")]
        M_CE_WD_LS_0011,


        [Description("ET : ETC 파일명 글자 개수 21자")]
        MP_ET_WD_MC_0001,

        [Description("ET : ETC 파일명 글자 개수 21자")]
        MP_ET_WD_LS_0007,

        [Description("ET : 파일명 KREI 는 그룹명 'ETC_'로 시작해야한다. (예:ETC_1)")]
        M_ET_WD_AC_0001,

        [Description("ET : 'ETC_'로 시작하는 그룹은 0개 이상이다.")]
        M_ET_WD_AC_0002,

        [Description("ET :ETC그룹내 그룹명 글자갯수 $5")]
        M_ET_WD_AC_0004,

        [Description("ET : PSD Mode (이미지 > Image Size > Width 2048 / Height 2048 )")]
        M_ET_WD_FM_0006,

        //[Description("ET : PSD Mode (이미지 > Image Size > Width 800 / Height 600 )")]
        //P_ET_WD_FM_0006,


        [Description("ET : 레이어구조 고정 C Type")]
        MP_ET_WD_LS_0001,

        [Description("ET : 레이어구조 가변 C Type")]
        MP_ET_WD_LS_0002,

        [Description("ET : 레이어별 show/hide")]
        MP_ET_WD_LS_0003,

        [Description("ET : ET : 레이어/레이어셋 총 개수 확인")]
        M_ET_WD_LS_0009,

        [Description("ET : 최상위 레이어셋 구조")]
        M_ET_WD_LS_0010,

        [Description("ET : 가장 마지막 레이어셋은 ETC")]
        M_ET_WD_LS_0011,

        [Description("JC :  파일명 글자갯수 22자")]
        M_JC_WD_MC_0001,

        [Description("JC :  파일명 KRJM 는 그룹명 'Arrow_KRJA'로 시작해야한다. ")]
        M_JC_WD_AC_0001,

        [Description("JC :  그룹명 'Arrow_KRJA'로 시작하는 그룹이 1개 이상이다 ")]
        M_JC_WD_AC_0002,

        [Description("JC :  그룹명 'Arrow_KRJA'로 시작하는 그룹의 모든 포맷 확인")]
        M_JC_WD_AC_0003,

        [Description("JC :  JC Arrow 그룹내 그룹명 글자갯수 $28")]
        M_JC_WD_AC_0004,

        [Description("JC : Arrow->Direction - Opacity 100%")]
        M_JC_WD_LS_0006,

        [Description("JC : Arrow->Transparency - Opacity 50%")]
        M_JC_WD_LS_0007,

        [Description("JC :  레이어구조 A Type : 고정부분")]
        M_JC_WD_LS_0001,

        [Description("JC : 레이어구조 A Type : 가변부분")]
        M_JC_WD_LS_0002,

        [Description("JC : Road Background_Color 0,2048,02048 or 800,600 (꽉 채워져야함)")]
        M_JC_WD_LS_0008,

        [Description("JC : Transparency_Color 0,2048,02048 or 800,600 (꽉 채워져야함)")]
        M_JC_WD_LS_0009,

        [Description("JC : 레이어/레이어셋 총 개수 확인")]
        M_JC_WD_LS_0010,


        //파일명 KRCM 는 그룹명 'Arrow_KRCA'로 시작해야한다. (예:Arrow_KRCA16090693D084F00101)
        [Description("일반교차로 : 메인코드 포맷 확인 KRCM{알파벳,숫자}")]
        M_NC_WD_AC_0001,

        [Description("그룹명 'Arrow_KRCA'로 시작하는 그룹이' 1개' 이상이다")]
        M_NC_WD_AC_0002,



        [Description("공통 : 채널개수 4개 (Alpha/Red/Green/Blue) ")]
        MP_CM_WD_FM_0001,

        [Description("공통 : 채널타입은 (Alpha/Red/Green/Blue)만 있어야 함.")]
        MP_CM_WD_FM_0002,

        [Description("공통 : PSD Mode (이미지 > 모드 > RGB Color) ")]
        MP_CM_WD_FM_0003,

        [Description("공통 : PSD Depth - RGB 8bit (이미지 > 모드 > 8bit) ")]
        MP_CM_WD_FM_0004,

        [Description("공통 : PSD 해상도 72pixel  ")]
        MP_CM_WD_FM_0005,

        [Description("공통 :PSD Mode (이미지 > Image Size > Width 2048 / Height 2048)")]
        M_CM_WD_FM_0006,




        [Description("일반교차료 : 레이어그룹의 명칭들이 중복되면 안된다")]
        MP_CM_WD_LS_0001,

        [Description("공통 : Clipping 기본 체크 해제")]
        MP_CM_WD_LS_0002,

        [Description("공통 : Layer Night_Filtter_B는 ARGB 값이 0/0/0/0 이여야 한다")]
        MP_CM_WD_LS_0003,

        [Description("공통 : Layer Road_B는 ARGB 값이 0/0/0/0 이여야 한다")]
        MP_CM_WD_LS_0004,

        [Description("공통 : Layer 이미지 Arrow는 ARGB 값이 0/255/0/0 이여야 한다")]
        MP_CM_WD_LS_0005,

        [Description("공통 : 각 레이어 별로 Lock이 걸려있는지 확인 (이미지 레이어만 해당됨)")]
        MP_CM_WD_LS_0006,

        [Description("공통 : 최상위 레이어셋 구조(코드 포맷, 공백 확인)")]
        MP_CM_WD_LS_0007,


        [Description("공통 : 파일 확장자명은 반드시 *.PSD")]
        MP_CM_WD_MC_0001,

        [Description("공통 : 일반교차로 파일명 글자갯수 22EA,")]
        MP_CM_WD_MC_0002,


        [Description("이미지+일반교차로 : 일반교차로 Arrow 레이어셋 하위 그룹명 Arrow_KRCA*** 길이는 28")]
        MP_NC_WD_AC_0001,

        [Description("레이어/레이어셋 총 개수 확인, 19, 24, 28, 4개씩 증가")]
        M_NC_WD_LS_0010,

        [Description("최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)")]
        M_NC_WD_LS_0011,

        [Description("최상위 레이어셋 중 Arrow가 있는가?")]
        M_NC_WD_LS_0012,

        [Description("레이어구조 A Type : 고정부분")]
        M_NC_WD_LS_0001,

        [Description("멀티개의 Arrow 레이어셋의 구조(Direction, Transparency, Arrow 명칭, 순서)는 올바른가?")]
        M_NC_WD_LS_0002,

        [Description("레이어별 Visible On/Off 옵션 확인")]
        MP_NC_WD_LS_0008,

        [Description("멀티개의 Arrow 레이어셋 명칭 포맷확인 Arrow_KRMC{알파벳,숫자}")]
        M_NC_WD_AC_0003,

        [Description("JC : 최상위 레이어셋 구조 : 6개 (Arrow, …, Night_on_Sky)")]
        M_JC_WD_LS_0011,

        [Description("JC : 가장 마지막 레이어셋은 Arrow")]
        M_JC_WD_LS_0012,

        [Description("'Road Background_Color'레이어의 ARGB 값은 255/64/69/76")]
        M_NC_WD_LS_0004,

        [Description("화살표코드 레이어셋은 1개 이상이다")]
        M_NC_WD_LS_0003,

        [Description("Road Background_Color/Transparency_Color - Arrow 그룹내 위치")]
        M_NC_WD_LS_0005,

        [Description("일반교차로 : Arrow->Direction - Opacity 100%")]
        M_NC_WD_LS_0006,

        [Description("일반교차로 : Arrow->Transparency - Opacity 50%")]
        M_NC_WD_LS_0007,

        [Description("일반교차로 : Road Background_Color 0,2048,02048(꽉 채워져야함)")]
        M_NC_WD_LS_0008,

        [Description("일반교차로 : Transparency_Color 0,2048,02048(꽉 채워져야함)")]
        M_NC_WD_LS_0009,

        [Description("레이어별 show/hide 옵션 확인")]
        MP_JC_WD_LS_0003,

        [Description("CM : Layer Road Background_Color 는  ARGB 값이 255/64/69/76이여야 한다")]
        MP_CM_WD_LS_0009,

        [Description("Transparency - Opacity 50% 을 제외하고 모두 100%여야 한다")]
        MP_CM_WD_LS_0008,

        [Description("MimeticDiagram : 모식도 파일명은 8로 시작한다")]
        M_MD_WD_MC_0001,

        [Description("MimeticDiagram : 모식도 파일명 글자갯수 8자")]
        M_MD_WD_MC_0002,

        [Description("파일명 Arrow 는 그룹명 'Arrow_'로 시작해야한다. (예:Arrow_16090693)")]
        M_MD_WD_AC_0001,

        [Description("그룹명 'Arrow_'로 시작하는 그룹이' 1개' 이상이다")]
        M_MD_WD_AC_0002,

        [Description("그룹명 'Arrow_'로 시작하는 모든 그룹확인")]
        M_MD_WD_AC_0003,

        [Description("MimeticDiagram : Arrow 그룹내 그룹명 글자갯수 14")]
        M_MD_WD_AC_0004,

        [Description("CrossRoadPoint3D : 모식도 파일명은 8로 시작한다.")]
        M_CR3D_WD_MC_0001,

        [Description("CrossRoadPoint3D : 모식도 파일명 글자갯수 8자")]
        M_CR3D_WD_MC_0002,

        [Description("CrossRoadPoint3D : Arrow 그룹내 그룹명 글자갯수 14")]
        M_CR3D_WD_AC_0001,

        [Description("CrossRoadPoint3D : 그룹명 '_AI'로 끝나는 그룹이 2개이다")]
        M_CR3D_WD_AC_0002,

        [Description("RestAreaSummaryMap_Mapy : 첫 폴더는 Title이여야한다")]
        M_RASMM_WD_MC_0001,

        [Description("RestAreaSummaryMap_Gini : 첫 폴더는 Title_set이여야한다")]
        M_RASMG_WD_MC_0001,

        [Description("RestAreaSummaryMap_Mapy : 구조확인")]
        M_RASMM_WD_MC_0002,

        [Description("RestAreaSummaryMap_Gini : 구조확인")]
        M_RASMG_WD_MC_0002,


    }
}
