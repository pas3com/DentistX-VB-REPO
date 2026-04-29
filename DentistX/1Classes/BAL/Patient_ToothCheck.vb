Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_ToothCheck


    Public Function Clone() As Patient_ToothCheck
        Return DirectCast(Me.MemberwiseClone(), Patient_ToothCheck)
    End Function

    Private m_ToothNum As Byte
    Property ToothNum As Byte
        Get
            Return m_ToothNum
        End Get
        Set(ByVal value As Byte)
            m_ToothNum = value
        End Set
    End Property

    Private m_PatientID As Integer
    Property PatientID As Integer
        Get
            Return m_PatientID
        End Get
        Set(ByVal value As Integer)
            m_PatientID = value
        End Set
    End Property

    Private m_ToothName As String
    Property ToothName As String
        Get
            Return m_ToothName
        End Get
        Set(ByVal value As String)
            m_ToothName = value
        End Set
    End Property

    Private m_ISEXIST As Byte
    Property ISEXIST As Byte
        Get
            Return m_ISEXIST
        End Get
        Set(ByVal value As Byte)
            m_ISEXIST = value
        End Set
    End Property

    Private m_ABCESS_DRAINAGE As Byte
    Property ABCESS_DRAINAGE As Byte
        Get
            Return m_ABCESS_DRAINAGE
        End Get
        Set(ByVal value As Byte)
            m_ABCESS_DRAINAGE = value
        End Set
    End Property

    Private m_APICECTOMY_PIC As Byte
    Property APICECTOMY_PIC As Byte
        Get
            Return m_APICECTOMY_PIC
        End Get
        Set(ByVal value As Byte)
            m_APICECTOMY_PIC = value
        End Set
    End Property

    Private m_APEXIFICATION As Byte
    Property APEXIFICATION As Byte
        Get
            Return m_APEXIFICATION
        End Get
        Set(ByVal value As Byte)
            m_APEXIFICATION = value
        End Set
    End Property

    Private m_BUILD_UP_COM As Byte
    Property BUILD_UP_COM As Byte
        Get
            Return m_BUILD_UP_COM
        End Get
        Set(ByVal value As Byte)
            m_BUILD_UP_COM = value
        End Set
    End Property

    Private m_BUILD_UP_ACR As Byte
    Property BUILD_UP_ACR As Byte
        Get
            Return m_BUILD_UP_ACR
        End Get
        Set(ByVal value As Byte)
            m_BUILD_UP_ACR = value
        End Set
    End Property

    Private m_BUILD_UP_GI As Byte
    Property BUILD_UP_GI As Byte
        Get
            Return m_BUILD_UP_GI
        End Get
        Set(ByVal value As Byte)
            m_BUILD_UP_GI = value
        End Set
    End Property

    Private m_CLASS_1_AMALGAM As Byte
    Property CLASS_1_AMALGAM As Byte
        Get
            Return m_CLASS_1_AMALGAM
        End Get
        Set(ByVal value As Byte)
            m_CLASS_1_AMALGAM = value
        End Set
    End Property

    Private m_CLASS_1_COMPOSITE As Byte
    Property CLASS_1_COMPOSITE As Byte
        Get
            Return m_CLASS_1_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_1_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_1_GI As Byte
    Property CLASS_1_GI As Byte
        Get
            Return m_CLASS_1_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_1_GI = value
        End Set
    End Property

    Private m_CLASS_1_TF As Byte
    Property CLASS_1_TF As Byte
        Get
            Return m_CLASS_1_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_1_TF = value
        End Set
    End Property

    Private m_CLASS_2_D_AMALGAM As Byte
    Property CLASS_2_D_AMALGAM As Byte
        Get
            Return m_CLASS_2_D_AMALGAM
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_D_AMALGAM = value
        End Set
    End Property

    Private m_CLASS_2_D_COMPOSITE As Byte
    Property CLASS_2_D_COMPOSITE As Byte
        Get
            Return m_CLASS_2_D_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_D_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_2_D_GI As Byte
    Property CLASS_2_D_GI As Byte
        Get
            Return m_CLASS_2_D_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_D_GI = value
        End Set
    End Property

    Private m_CLASS_2_D_TF As Byte
    Property CLASS_2_D_TF As Byte
        Get
            Return m_CLASS_2_D_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_D_TF = value
        End Set
    End Property

    Private m_CLASS_2_M_AMALGAM As Byte
    Property CLASS_2_M_AMALGAM As Byte
        Get
            Return m_CLASS_2_M_AMALGAM
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_M_AMALGAM = value
        End Set
    End Property

    Private m_CLASS_2_M_COMPOSITE As Byte
    Property CLASS_2_M_COMPOSITE As Byte
        Get
            Return m_CLASS_2_M_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_M_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_2_M_GI As Byte
    Property CLASS_2_M_GI As Byte
        Get
            Return m_CLASS_2_M_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_M_GI = value
        End Set
    End Property

    Private m_CLASS_2_M_TF As Byte
    Property CLASS_2_M_TF As Byte
        Get
            Return m_CLASS_2_M_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_M_TF = value
        End Set
    End Property

    Private m_CLASS_2_MOD_AMALGAM As Byte
    Property CLASS_2_MOD_AMALGAM As Byte
        Get
            Return m_CLASS_2_MOD_AMALGAM
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_MOD_AMALGAM = value
        End Set
    End Property

    Private m_CLASS_2_MOD_COMPOSITE As Byte
    Property CLASS_2_MOD_COMPOSITE As Byte
        Get
            Return m_CLASS_2_MOD_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_MOD_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_2_MOD_GI As Byte
    Property CLASS_2_MOD_GI As Byte
        Get
            Return m_CLASS_2_MOD_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_MOD_GI = value
        End Set
    End Property

    Private m_CLASS_2_MOD_TF As Byte
    Property CLASS_2_MOD_TF As Byte
        Get
            Return m_CLASS_2_MOD_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_2_MOD_TF = value
        End Set
    End Property

    Private m_CLASS_3_D_COMPOSITE As Byte
    Property CLASS_3_D_COMPOSITE As Byte
        Get
            Return m_CLASS_3_D_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_3_D_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_3_D_GI As Byte
    Property CLASS_3_D_GI As Byte
        Get
            Return m_CLASS_3_D_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_3_D_GI = value
        End Set
    End Property

    Private m_CLASS_3_D_TF As Byte
    Property CLASS_3_D_TF As Byte
        Get
            Return m_CLASS_3_D_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_3_D_TF = value
        End Set
    End Property

    Private m_CLASS_3_M_COMPOSITE As Byte
    Property CLASS_3_M_COMPOSITE As Byte
        Get
            Return m_CLASS_3_M_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_3_M_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_3_M_GI As Byte
    Property CLASS_3_M_GI As Byte
        Get
            Return m_CLASS_3_M_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_3_M_GI = value
        End Set
    End Property

    Private m_CLASS_3_M_TF As Byte
    Property CLASS_3_M_TF As Byte
        Get
            Return m_CLASS_3_M_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_3_M_TF = value
        End Set
    End Property

    Private m_CLASS_4_D_COMPOSITE As Byte
    Property CLASS_4_D_COMPOSITE As Byte
        Get
            Return m_CLASS_4_D_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_4_D_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_4_D_GI As Byte
    Property CLASS_4_D_GI As Byte
        Get
            Return m_CLASS_4_D_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_4_D_GI = value
        End Set
    End Property

    Private m_CLASS_4_D_TF As Byte
    Property CLASS_4_D_TF As Byte
        Get
            Return m_CLASS_4_D_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_4_D_TF = value
        End Set
    End Property

    Private m_CLASS_4_M_COMPOSITE As Byte
    Property CLASS_4_M_COMPOSITE As Byte
        Get
            Return m_CLASS_4_M_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_4_M_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_4_M_GI As Byte
    Property CLASS_4_M_GI As Byte
        Get
            Return m_CLASS_4_M_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_4_M_GI = value
        End Set
    End Property

    Private m_CLASS_4_M_TF As Byte
    Property CLASS_4_M_TF As Byte
        Get
            Return m_CLASS_4_M_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_4_M_TF = value
        End Set
    End Property

    Private m_CLASS_4_INCISAL As Byte
    Property CLASS_4_INCISAL As Byte
        Get
            Return m_CLASS_4_INCISAL
        End Get
        Set(ByVal value As Byte)
            m_CLASS_4_INCISAL = value
        End Set
    End Property

    Private m_CLASS_5_AMALGAM As Byte
    Property CLASS_5_AMALGAM As Byte
        Get
            Return m_CLASS_5_AMALGAM
        End Get
        Set(ByVal value As Byte)
            m_CLASS_5_AMALGAM = value
        End Set
    End Property

    Private m_CLASS_5_COMPOSITE As Byte
    Property CLASS_5_COMPOSITE As Byte
        Get
            Return m_CLASS_5_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_CLASS_5_COMPOSITE = value
        End Set
    End Property

    Private m_CLASS_5_GI As Byte
    Property CLASS_5_GI As Byte
        Get
            Return m_CLASS_5_GI
        End Get
        Set(ByVal value As Byte)
            m_CLASS_5_GI = value
        End Set
    End Property

    Private m_CLASS_5_TF As Byte
    Property CLASS_5_TF As Byte
        Get
            Return m_CLASS_5_TF
        End Get
        Set(ByVal value As Byte)
            m_CLASS_5_TF = value
        End Set
    End Property

    Private m_CRACK As Byte
    Property CRACK As Byte
        Get
            Return m_CRACK
        End Get
        Set(ByVal value As Byte)
            m_CRACK = value
        End Set
    End Property

    Private m_CROWN_LENGTHENING As Byte
    Property CROWN_LENGTHENING As Byte
        Get
            Return m_CROWN_LENGTHENING
        End Get
        Set(ByVal value As Byte)
            m_CROWN_LENGTHENING = value
        End Set
    End Property

    Private m_DIRECT_PULP_CAPPING As Byte
    Property DIRECT_PULP_CAPPING As Byte
        Get
            Return m_DIRECT_PULP_CAPPING
        End Get
        Set(ByVal value As Byte)
            m_DIRECT_PULP_CAPPING = value
        End Set
    End Property

    Private m_EXTRACTION As Byte
    Property EXTRACTION As Byte
        Get
            Return m_EXTRACTION
        End Get
        Set(ByVal value As Byte)
            m_EXTRACTION = value
        End Set
    End Property

    Private m_FACING_DIRECT_VENEERS As Byte
    Property FACING_DIRECT_VENEERS As Byte
        Get
            Return m_FACING_DIRECT_VENEERS
        End Get
        Set(ByVal value As Byte)
            m_FACING_DIRECT_VENEERS = value
        End Set
    End Property

    Private m_FIBER_POST As Byte
    Property FIBER_POST As Byte
        Get
            Return m_FIBER_POST
        End Get
        Set(ByVal value As Byte)
            m_FIBER_POST = value
        End Set
    End Property

    Private m_FISSURE_SEALENT As Byte
    Property FISSURE_SEALENT As Byte
        Get
            Return m_FISSURE_SEALENT
        End Get
        Set(ByVal value As Byte)
            m_FISSURE_SEALENT = value
        End Set
    End Property

    Private m_HEMISECTION As Byte
    Property HEMISECTION As Byte
        Get
            Return m_HEMISECTION
        End Get
        Set(ByVal value As Byte)
            m_HEMISECTION = value
        End Set
    End Property

    Private m_IMPLANT As Byte
    Property IMPLANT As Byte
        Get
            Return m_IMPLANT
        End Get
        Set(ByVal value As Byte)
            m_IMPLANT = value
        End Set
    End Property

    Private m_INDIRECT_PULP_CAPPING As Byte
    Property INDIRECT_PULP_CAPPING As Byte
        Get
            Return m_INDIRECT_PULP_CAPPING
        End Get
        Set(ByVal value As Byte)
            m_INDIRECT_PULP_CAPPING = value
        End Set
    End Property

    Private m_INDIRECT_VENEERS As Byte
    Property INDIRECT_VENEERS As Byte
        Get
            Return m_INDIRECT_VENEERS
        End Get
        Set(ByVal value As Byte)
            m_INDIRECT_VENEERS = value
        End Set
    End Property

    Private m_METAL_POST As Byte
    Property METAL_POST As Byte
        Get
            Return m_METAL_POST
        End Get
        Set(ByVal value As Byte)
            m_METAL_POST = value
        End Set
    End Property

    Private m_MTA_BULK_FLOW As Byte
    Property MTA_BULK_FLOW As Byte
        Get
            Return m_MTA_BULK_FLOW
        End Get
        Set(ByVal value As Byte)
            m_MTA_BULK_FLOW = value
        End Set
    End Property

    Private m_PARTIAL_PULPOTOMY As Byte
    Property PARTIAL_PULPOTOMY As Byte
        Get
            Return m_PARTIAL_PULPOTOMY
        End Get
        Set(ByVal value As Byte)
            m_PARTIAL_PULPOTOMY = value
        End Set
    End Property

    Private m_PERIAPICAL_LESION As Byte
    Property PERIAPICAL_LESION As Byte
        Get
            Return m_PERIAPICAL_LESION
        End Get
        Set(ByVal value As Byte)
            m_PERIAPICAL_LESION = value
        End Set
    End Property

    Private m_PULPOTOMY As Byte
    Property PULPOTOMY As Byte
        Get
            Return m_PULPOTOMY
        End Get
        Set(ByVal value As Byte)
            m_PULPOTOMY = value
        End Set
    End Property

    Private m_RCC_TF As Byte
    Property RCC_TF As Byte
        Get
            Return m_RCC_TF
        End Get
        Set(ByVal value As Byte)
            m_RCC_TF = value
        End Set
    End Property

    Private m_RCO_TF As Byte
    Property RCO_TF As Byte
        Get
            Return m_RCO_TF
        End Get
        Set(ByVal value As Byte)
            m_RCO_TF = value
        End Set
    End Property

    Private m_RCF_AMALGAM As Byte
    Property RCF_AMALGAM As Byte
        Get
            Return m_RCF_AMALGAM
        End Get
        Set(ByVal value As Byte)
            m_RCF_AMALGAM = value
        End Set
    End Property

    Private m_RCF_COMPOSITE As Byte
    Property RCF_COMPOSITE As Byte
        Get
            Return m_RCF_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_RCF_COMPOSITE = value
        End Set
    End Property

    Private m_RCF_GI As Byte
    Property RCF_GI As Byte
        Get
            Return m_RCF_GI
        End Get
        Set(ByVal value As Byte)
            m_RCF_GI = value
        End Set
    End Property

    Private m_REDO_AMALGAM As Byte
    Property REDO_AMALGAM As Byte
        Get
            Return m_REDO_AMALGAM
        End Get
        Set(ByVal value As Byte)
            m_REDO_AMALGAM = value
        End Set
    End Property

    Private m_REDO_COMPOSITE As Byte
    Property REDO_COMPOSITE As Byte
        Get
            Return m_REDO_COMPOSITE
        End Get
        Set(ByVal value As Byte)
            m_REDO_COMPOSITE = value
        End Set
    End Property

    Private m_REDO_GI As Byte
    Property REDO_GI As Byte
        Get
            Return m_REDO_GI
        End Get
        Set(ByVal value As Byte)
            m_REDO_GI = value
        End Set
    End Property

    Private m_REDO_RCT As Byte
    Property REDO_RCT As Byte
        Get
            Return m_REDO_RCT
        End Get
        Set(ByVal value As Byte)
            m_REDO_RCT = value
        End Set
    End Property

    Private m_RC_MEDICAMENT_TF As Byte
    Property RC_MED_TF As Byte
        Get
            Return m_RC_MEDICAMENT_TF
        End Get
        Set(ByVal value As Byte)
            m_RC_MEDICAMENT_TF = value
        End Set
    End Property

    Private m_ROOT_CARIES As Byte
    Property ROOT_CARIES As Byte
        Get
            Return m_ROOT_CARIES
        End Get
        Set(ByVal value As Byte)
            m_ROOT_CARIES = value
        End Set
    End Property

    Private m_STAINLESS_STEEL_CROWN As Byte
    Property STAINLESS_STEEL_CROWN As Byte
        Get
            Return m_STAINLESS_STEEL_CROWN
        End Get
        Set(ByVal value As Byte)
            m_STAINLESS_STEEL_CROWN = value
        End Set
    End Property

    Private m_TEMPORARY_CROWN As Byte
    Property TEMPORARY_CROWN As Byte
        Get
            Return m_TEMPORARY_CROWN
        End Get
        Set(ByVal value As Byte)
            m_TEMPORARY_CROWN = value
        End Set
    End Property

    Private m_TEMPORARY_FILLING As Byte
    Property TEMPORARY_FILLING As Byte
        Get
            Return m_TEMPORARY_FILLING
        End Get
        Set(ByVal value As Byte)
            m_TEMPORARY_FILLING = value
        End Set
    End Property

    Private m_CheckDate As Nullable(Of DateTime)
    Property CheckDate As Nullable(Of DateTime)
        Get
            Return m_CheckDate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_CheckDate = value
        End Set
    End Property

    Private m_CheckNotes As String
    Property CheckNotes As String
        Get
            Return m_CheckNotes
        End Get
        Set(ByVal value As String)
            m_CheckNotes = value
        End Set
    End Property



End Class
