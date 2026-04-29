Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper
Imports System.Windows.Forms

Public Class Patient_ToothCheckDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString




    Public Function SelectAll() As IEnumerable(Of Patient_ToothCheck)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_ToothCheck)("SELECT * FROM Patient_ToothCheck")
        End Using
    End Function


    Public Function Select_Record(ByVal clsPatient_ToothCheck As Patient_ToothCheck) As Patient_ToothCheck
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_ToothCheck WHERE ToothNum = @ToothNum And PatientID = @PatientID"
            Return conn.QuerySingleOrDefault(Of Patient_ToothCheck)(sql, New With {.ToothNum = clsPatient_ToothCheck.ToothNum, .PatientID = clsPatient_ToothCheck.PatientID})
        End Using
    End Function

    Public Function Add(ByVal clsPatient_ToothCheck As Patient_ToothCheck) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_ToothCheck (ToothNum,
			PatientID,
			ToothName,
			ISEXIST,
			ABCESS_DRAINAGE,
			APICECTOMY_PIC,
			APEXIFICATION,
			BUILD_UP_COM,
			BUILD_UP_ACR,
			BUILD_UP_GI,
			CLASS_1_AMALGAM,
			CLASS_1_COMPOSITE,
			CLASS_1_GI,
			CLASS_1_TF,
			CLASS_2_D_AMALGAM,
			CLASS_2_D_COMPOSITE,
			CLASS_2_D_GI,
			CLASS_2_D_TF,
			CLASS_2_M_AMALGAM,
			CLASS_2_M_COMPOSITE,
			CLASS_2_M_GI,
			CLASS_2_M_TF,
			CLASS_2_MOD_AMALGAM,
			CLASS_2_MOD_COMPOSITE,
			CLASS_2_MOD_GI,
			CLASS_2_MOD_TF,
			CLASS_3_D_COMPOSITE,
			CLASS_3_D_GI,
			CLASS_3_D_TF,
			CLASS_3_M_COMPOSITE,
			CLASS_3_M_GI,
			CLASS_3_M_TF,
			CLASS_4_D_COMPOSITE,
			CLASS_4_D_GI,
			CLASS_4_D_TF,
			CLASS_4_M_COMPOSITE,
			CLASS_4_M_GI,
			CLASS_4_M_TF,
			CLASS_4_INCISAL,
			CLASS_5_AMALGAM,
			CLASS_5_COMPOSITE,
			CLASS_5_GI,
			CLASS_5_TF,
			CRACK,
			CROWN_LENGTHENING,
			DIRECT_PULP_CAPPING,
			EXTRACTION,
			FACING_DIRECT_VENEERS,
			FIBER_POST,
			FISSURE_SEALENT,
			HEMISECTION,
			IMPLANT,
			INDIRECT_PULP_CAPPING,
			INDIRECT_VENEERS,
			METAL_POST,
			MTA_BULK_FLOW,
			PARTIAL_PULPOTOMY,
			PERIAPICAL_LESION,
			PULPOTOMY,
			RCC_TF,
			RCO_TF,
			RCF_AMALGAM,
			RCF_COMPOSITE,
			RCF_GI,
			REDO_AMALGAM,
			REDO_COMPOSITE,
			REDO_GI,
			REDO_RCT,
			RC_MED_TF,
			ROOT_CARIES,
			STAINLESS_STEEL_CROWN,
			TEMPORARY_CROWN,
			TEMPORARY_FILLING,
			CheckDate,
			CheckNotes) VALUES (@ToothNum,
			@PatientID,
			@ToothName,
			@ISEXIST,
			@ABCESS_DRAINAGE,
			@APICECTOMY_PIC,
			@APEXIFICATION,
			@BUILD_UP_COM,
			@BUILD_UP_ACR,
			@BUILD_UP_GI,
			@CLASS_1_AMALGAM,
			@CLASS_1_COMPOSITE,
			@CLASS_1_GI,
			@CLASS_1_TF,
			@CLASS_2_D_AMALGAM,
			@CLASS_2_D_COMPOSITE,
			@CLASS_2_D_GI,
			@CLASS_2_D_TF,
			@CLASS_2_M_AMALGAM,
			@CLASS_2_M_COMPOSITE,
			@CLASS_2_M_GI,
			@CLASS_2_M_TF,
			@CLASS_2_MOD_AMALGAM,
			@CLASS_2_MOD_COMPOSITE,
			@CLASS_2_MOD_GI,
			@CLASS_2_MOD_TF,
			@CLASS_3_D_COMPOSITE,
			@CLASS_3_D_GI,
			@CLASS_3_D_TF,
			@CLASS_3_M_COMPOSITE,
			@CLASS_3_M_GI,
			@CLASS_3_M_TF,
			@CLASS_4_D_COMPOSITE,
			@CLASS_4_D_GI,
			@CLASS_4_D_TF,
			@CLASS_4_M_COMPOSITE,
			@CLASS_4_M_GI,
			@CLASS_4_M_TF,
			@CLASS_4_INCISAL,
			@CLASS_5_AMALGAM,
			@CLASS_5_COMPOSITE,
			@CLASS_5_GI,
			@CLASS_5_TF,
			@CRACK,
			@CROWN_LENGTHENING,
			@DIRECT_PULP_CAPPING,
			@EXTRACTION,
			@FACING_DIRECT_VENEERS,
			@FIBER_POST,
			@FISSURE_SEALENT,
			@HEMISECTION,
			@IMPLANT,
			@INDIRECT_PULP_CAPPING,
			@INDIRECT_VENEERS,
			@METAL_POST,
			@MTA_BULK_FLOW,
			@PARTIAL_PULPOTOMY,
			@PERIAPICAL_LESION,
			@PULPOTOMY,
			@RCC_TF,
			@RCO_TF,
			@RCF_AMALGAM,
			@RCF_COMPOSITE,
			@RCF_GI,
			@REDO_AMALGAM,
			@REDO_COMPOSITE,
			@REDO_GI,
			@REDO_RCT,
			@RC_MED_TF,
			@ROOT_CARIES,
			@STAINLESS_STEEL_CROWN,
			@TEMPORARY_CROWN,
			@TEMPORARY_FILLING,
			@CheckDate,
			@CheckNotes)"
            RowsAffected = conn.Execute(sql, New With {.ToothNum = clsPatient_ToothCheck.ToothNum,
                    .PatientID = clsPatient_ToothCheck.PatientID,
                    .ToothName = clsPatient_ToothCheck.ToothName,
                    .ISEXIST = clsPatient_ToothCheck.ISEXIST,
                    .ABCESS_DRAINAGE = clsPatient_ToothCheck.ABCESS_DRAINAGE,
                    .APICECTOMY_PIC = clsPatient_ToothCheck.APICECTOMY_PIC,
                    .APEXIFICATION = clsPatient_ToothCheck.APEXIFICATION,
                    .BUILD_UP_COM = clsPatient_ToothCheck.BUILD_UP_COM,
                    .BUILD_UP_ACR = clsPatient_ToothCheck.BUILD_UP_ACR,
                    .BUILD_UP_GI = clsPatient_ToothCheck.BUILD_UP_GI,
                    .CLASS_1_AMALGAM = clsPatient_ToothCheck.CLASS_1_AMALGAM,
                    .CLASS_1_COMPOSITE = clsPatient_ToothCheck.CLASS_1_COMPOSITE,
                    .CLASS_1_GI = clsPatient_ToothCheck.CLASS_1_GI,
                    .CLASS_1_TF = clsPatient_ToothCheck.CLASS_1_TF,
                    .CLASS_2_D_AMALGAM = clsPatient_ToothCheck.CLASS_2_D_AMALGAM,
                    .CLASS_2_D_COMPOSITE = clsPatient_ToothCheck.CLASS_2_D_COMPOSITE,
                    .CLASS_2_D_GI = clsPatient_ToothCheck.CLASS_2_D_GI,
                    .CLASS_2_D_TF = clsPatient_ToothCheck.CLASS_2_D_TF,
                    .CLASS_2_M_AMALGAM = clsPatient_ToothCheck.CLASS_2_M_AMALGAM,
                    .CLASS_2_M_COMPOSITE = clsPatient_ToothCheck.CLASS_2_M_COMPOSITE,
                    .CLASS_2_M_GI = clsPatient_ToothCheck.CLASS_2_M_GI,
                    .CLASS_2_M_TF = clsPatient_ToothCheck.CLASS_2_M_TF,
                    .CLASS_2_MOD_AMALGAM = clsPatient_ToothCheck.CLASS_2_MOD_AMALGAM,
                    .CLASS_2_MOD_COMPOSITE = clsPatient_ToothCheck.CLASS_2_MOD_COMPOSITE,
                    .CLASS_2_MOD_GI = clsPatient_ToothCheck.CLASS_2_MOD_GI,
                    .CLASS_2_MOD_TF = clsPatient_ToothCheck.CLASS_2_MOD_TF,
                    .CLASS_3_D_COMPOSITE = clsPatient_ToothCheck.CLASS_3_D_COMPOSITE,
                    .CLASS_3_D_GI = clsPatient_ToothCheck.CLASS_3_D_GI,
                    .CLASS_3_D_TF = clsPatient_ToothCheck.CLASS_3_D_TF,
                    .CLASS_3_M_COMPOSITE = clsPatient_ToothCheck.CLASS_3_M_COMPOSITE,
                    .CLASS_3_M_GI = clsPatient_ToothCheck.CLASS_3_M_GI,
                    .CLASS_3_M_TF = clsPatient_ToothCheck.CLASS_3_M_TF,
                    .CLASS_4_D_COMPOSITE = clsPatient_ToothCheck.CLASS_4_D_COMPOSITE,
                    .CLASS_4_D_GI = clsPatient_ToothCheck.CLASS_4_D_GI,
                    .CLASS_4_D_TF = clsPatient_ToothCheck.CLASS_4_D_TF,
                    .CLASS_4_M_COMPOSITE = clsPatient_ToothCheck.CLASS_4_M_COMPOSITE,
                    .CLASS_4_M_GI = clsPatient_ToothCheck.CLASS_4_M_GI,
                    .CLASS_4_M_TF = clsPatient_ToothCheck.CLASS_4_M_TF,
                    .CLASS_4_INCISAL = clsPatient_ToothCheck.CLASS_4_INCISAL,
                    .CLASS_5_AMALGAM = clsPatient_ToothCheck.CLASS_5_AMALGAM,
                    .CLASS_5_COMPOSITE = clsPatient_ToothCheck.CLASS_5_COMPOSITE,
                    .CLASS_5_GI = clsPatient_ToothCheck.CLASS_5_GI,
                    .CLASS_5_TF = clsPatient_ToothCheck.CLASS_5_TF,
                    .CRACK = clsPatient_ToothCheck.CRACK,
                    .CROWN_LENGTHENING = clsPatient_ToothCheck.CROWN_LENGTHENING,
                    .DIRECT_PULP_CAPPING = clsPatient_ToothCheck.DIRECT_PULP_CAPPING,
                    .EXTRACTION = clsPatient_ToothCheck.EXTRACTION,
                    .FACING_DIRECT_VENEERS = clsPatient_ToothCheck.FACING_DIRECT_VENEERS,
                    .FIBER_POST = clsPatient_ToothCheck.FIBER_POST,
                    .FISSURE_SEALENT = clsPatient_ToothCheck.FISSURE_SEALENT,
                    .HEMISECTION = clsPatient_ToothCheck.HEMISECTION,
                    .IMPLANT = clsPatient_ToothCheck.IMPLANT,
                    .INDIRECT_PULP_CAPPING = clsPatient_ToothCheck.INDIRECT_PULP_CAPPING,
                    .INDIRECT_VENEERS = clsPatient_ToothCheck.INDIRECT_VENEERS,
                    .METAL_POST = clsPatient_ToothCheck.METAL_POST,
                    .MTA_BULK_FLOW = clsPatient_ToothCheck.MTA_BULK_FLOW,
                    .PARTIAL_PULPOTOMY = clsPatient_ToothCheck.PARTIAL_PULPOTOMY,
                    .PERIAPICAL_LESION = clsPatient_ToothCheck.PERIAPICAL_LESION,
                    .PULPOTOMY = clsPatient_ToothCheck.PULPOTOMY,
                    .RCC_TF = clsPatient_ToothCheck.RCC_TF,
                    .RCO_TF = clsPatient_ToothCheck.RCO_TF,
                    .RCF_AMALGAM = clsPatient_ToothCheck.RCF_AMALGAM,
                    .RCF_COMPOSITE = clsPatient_ToothCheck.RCF_COMPOSITE,
                    .RCF_GI = clsPatient_ToothCheck.RCF_GI,
                    .REDO_AMALGAM = clsPatient_ToothCheck.REDO_AMALGAM,
                    .REDO_COMPOSITE = clsPatient_ToothCheck.REDO_COMPOSITE,
                    .REDO_GI = clsPatient_ToothCheck.REDO_GI,
                    .REDO_RCT = clsPatient_ToothCheck.REDO_RCT,
                    .RC_MEDICAMENT_TF = clsPatient_ToothCheck.RC_MED_TF,
                    .ROOT_CARIES = clsPatient_ToothCheck.ROOT_CARIES,
                    .STAINLESS_STEEL_CROWN = clsPatient_ToothCheck.STAINLESS_STEEL_CROWN,
                    .TEMPORARY_CROWN = clsPatient_ToothCheck.TEMPORARY_CROWN,
                    .TEMPORARY_FILLING = clsPatient_ToothCheck.TEMPORARY_FILLING,
                    .CheckDate = clsPatient_ToothCheck.CheckDate,
                    .CheckNotes = clsPatient_ToothCheck.CheckNotes})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldPatient_ToothCheck As Patient_ToothCheck, newPatient_ToothCheck As Patient_ToothCheck) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewToothNum = newPatient_ToothCheck.ToothNum, .OldToothNum = oldPatient_ToothCheck.ToothNum,
                    .NewPatientID = newPatient_ToothCheck.PatientID, .OldPatientID = oldPatient_ToothCheck.PatientID,
                    .NewToothName = newPatient_ToothCheck.ToothName, .OldToothName = oldPatient_ToothCheck.ToothName,
                    .NewISEXIST = newPatient_ToothCheck.ISEXIST, .OldISEXIST = oldPatient_ToothCheck.ISEXIST,
                    .NewABCESS_DRAINAGE = newPatient_ToothCheck.ABCESS_DRAINAGE, .OldABCESS_DRAINAGE = oldPatient_ToothCheck.ABCESS_DRAINAGE,
                    .NewAPICECTOMY_PIC = newPatient_ToothCheck.APICECTOMY_PIC, .OldAPICECTOMY_PIC = oldPatient_ToothCheck.APICECTOMY_PIC,
                    .NewAPEXIFICATION = newPatient_ToothCheck.APEXIFICATION, .OldAPEXIFICATION = oldPatient_ToothCheck.APEXIFICATION,
                    .NewBUILD_UP_COM = newPatient_ToothCheck.BUILD_UP_COM, .OldBUILD_UP_COM = oldPatient_ToothCheck.BUILD_UP_COM,
                    .NewBUILD_UP_ACR = newPatient_ToothCheck.BUILD_UP_ACR, .OldBUILD_UP_ACR = oldPatient_ToothCheck.BUILD_UP_ACR,
                    .NewBUILD_UP_GI = newPatient_ToothCheck.BUILD_UP_GI, .OldBUILD_UP_GI = oldPatient_ToothCheck.BUILD_UP_GI,
                    .NewCLASS_1_AMALGAM = newPatient_ToothCheck.CLASS_1_AMALGAM, .OldCLASS_1_AMALGAM = oldPatient_ToothCheck.CLASS_1_AMALGAM,
                    .NewCLASS_1_COMPOSITE = newPatient_ToothCheck.CLASS_1_COMPOSITE, .OldCLASS_1_COMPOSITE = oldPatient_ToothCheck.CLASS_1_COMPOSITE,
                    .NewCLASS_1_GI = newPatient_ToothCheck.CLASS_1_GI, .OldCLASS_1_GI = oldPatient_ToothCheck.CLASS_1_GI,
                    .NewCLASS_1_TF = newPatient_ToothCheck.CLASS_1_TF, .OldCLASS_1_TF = oldPatient_ToothCheck.CLASS_1_TF,
                    .NewCLASS_2_D_AMALGAM = newPatient_ToothCheck.CLASS_2_D_AMALGAM, .OldCLASS_2_D_AMALGAM = oldPatient_ToothCheck.CLASS_2_D_AMALGAM,
                    .NewCLASS_2_D_COMPOSITE = newPatient_ToothCheck.CLASS_2_D_COMPOSITE, .OldCLASS_2_D_COMPOSITE = oldPatient_ToothCheck.CLASS_2_D_COMPOSITE,
                    .NewCLASS_2_D_GI = newPatient_ToothCheck.CLASS_2_D_GI, .OldCLASS_2_D_GI = oldPatient_ToothCheck.CLASS_2_D_GI,
                    .NewCLASS_2_D_TF = newPatient_ToothCheck.CLASS_2_D_TF, .OldCLASS_2_D_TF = oldPatient_ToothCheck.CLASS_2_D_TF,
                    .NewCLASS_2_M_AMALGAM = newPatient_ToothCheck.CLASS_2_M_AMALGAM, .OldCLASS_2_M_AMALGAM = oldPatient_ToothCheck.CLASS_2_M_AMALGAM,
                    .NewCLASS_2_M_COMPOSITE = newPatient_ToothCheck.CLASS_2_M_COMPOSITE, .OldCLASS_2_M_COMPOSITE = oldPatient_ToothCheck.CLASS_2_M_COMPOSITE,
                    .NewCLASS_2_M_GI = newPatient_ToothCheck.CLASS_2_M_GI, .OldCLASS_2_M_GI = oldPatient_ToothCheck.CLASS_2_M_GI,
                    .NewCLASS_2_M_TF = newPatient_ToothCheck.CLASS_2_M_TF, .OldCLASS_2_M_TF = oldPatient_ToothCheck.CLASS_2_M_TF,
                    .NewCLASS_2_MOD_AMALGAM = newPatient_ToothCheck.CLASS_2_MOD_AMALGAM, .OldCLASS_2_MOD_AMALGAM = oldPatient_ToothCheck.CLASS_2_MOD_AMALGAM,
                    .NewCLASS_2_MOD_COMPOSITE = newPatient_ToothCheck.CLASS_2_MOD_COMPOSITE, .OldCLASS_2_MOD_COMPOSITE = oldPatient_ToothCheck.CLASS_2_MOD_COMPOSITE,
                    .NewCLASS_2_MOD_GI = newPatient_ToothCheck.CLASS_2_MOD_GI, .OldCLASS_2_MOD_GI = oldPatient_ToothCheck.CLASS_2_MOD_GI,
                    .NewCLASS_2_MOD_TF = newPatient_ToothCheck.CLASS_2_MOD_TF, .OldCLASS_2_MOD_TF = oldPatient_ToothCheck.CLASS_2_MOD_TF,
                    .NewCLASS_3_D_COMPOSITE = newPatient_ToothCheck.CLASS_3_D_COMPOSITE, .OldCLASS_3_D_COMPOSITE = oldPatient_ToothCheck.CLASS_3_D_COMPOSITE,
                    .NewCLASS_3_D_GI = newPatient_ToothCheck.CLASS_3_D_GI, .OldCLASS_3_D_GI = oldPatient_ToothCheck.CLASS_3_D_GI,
                    .NewCLASS_3_D_TF = newPatient_ToothCheck.CLASS_3_D_TF, .OldCLASS_3_D_TF = oldPatient_ToothCheck.CLASS_3_D_TF,
                    .NewCLASS_3_M_COMPOSITE = newPatient_ToothCheck.CLASS_3_M_COMPOSITE, .OldCLASS_3_M_COMPOSITE = oldPatient_ToothCheck.CLASS_3_M_COMPOSITE,
                    .NewCLASS_3_M_GI = newPatient_ToothCheck.CLASS_3_M_GI, .OldCLASS_3_M_GI = oldPatient_ToothCheck.CLASS_3_M_GI,
                    .NewCLASS_3_M_TF = newPatient_ToothCheck.CLASS_3_M_TF, .OldCLASS_3_M_TF = oldPatient_ToothCheck.CLASS_3_M_TF,
                    .NewCLASS_4_D_COMPOSITE = newPatient_ToothCheck.CLASS_4_D_COMPOSITE, .OldCLASS_4_D_COMPOSITE = oldPatient_ToothCheck.CLASS_4_D_COMPOSITE,
                    .NewCLASS_4_D_GI = newPatient_ToothCheck.CLASS_4_D_GI, .OldCLASS_4_D_GI = oldPatient_ToothCheck.CLASS_4_D_GI,
                    .NewCLASS_4_D_TF = newPatient_ToothCheck.CLASS_4_D_TF, .OldCLASS_4_D_TF = oldPatient_ToothCheck.CLASS_4_D_TF,
                    .NewCLASS_4_M_COMPOSITE = newPatient_ToothCheck.CLASS_4_M_COMPOSITE, .OldCLASS_4_M_COMPOSITE = oldPatient_ToothCheck.CLASS_4_M_COMPOSITE,
                    .NewCLASS_4_M_GI = newPatient_ToothCheck.CLASS_4_M_GI, .OldCLASS_4_M_GI = oldPatient_ToothCheck.CLASS_4_M_GI,
                    .NewCLASS_4_M_TF = newPatient_ToothCheck.CLASS_4_M_TF, .OldCLASS_4_M_TF = oldPatient_ToothCheck.CLASS_4_M_TF,
                    .NewCLASS_4_INCISAL = newPatient_ToothCheck.CLASS_4_INCISAL, .OldCLASS_4_INCISAL = oldPatient_ToothCheck.CLASS_4_INCISAL,
                    .NewCLASS_5_AMALGAM = newPatient_ToothCheck.CLASS_5_AMALGAM, .OldCLASS_5_AMALGAM = oldPatient_ToothCheck.CLASS_5_AMALGAM,
                    .NewCLASS_5_COMPOSITE = newPatient_ToothCheck.CLASS_5_COMPOSITE, .OldCLASS_5_COMPOSITE = oldPatient_ToothCheck.CLASS_5_COMPOSITE,
                    .NewCLASS_5_GI = newPatient_ToothCheck.CLASS_5_GI, .OldCLASS_5_GI = oldPatient_ToothCheck.CLASS_5_GI,
                    .NewCLASS_5_TF = newPatient_ToothCheck.CLASS_5_TF, .OldCLASS_5_TF = oldPatient_ToothCheck.CLASS_5_TF,
                    .NewCRACK = newPatient_ToothCheck.CRACK, .OldCRACK = oldPatient_ToothCheck.CRACK,
                    .NewCROWN_LENGTHENING = newPatient_ToothCheck.CROWN_LENGTHENING, .OldCROWN_LENGTHENING = oldPatient_ToothCheck.CROWN_LENGTHENING,
                    .NewDIRECT_PULP_CAPPING = newPatient_ToothCheck.DIRECT_PULP_CAPPING, .OldDIRECT_PULP_CAPPING = oldPatient_ToothCheck.DIRECT_PULP_CAPPING,
                    .NewEXTRACTION = newPatient_ToothCheck.EXTRACTION, .OldEXTRACTION = oldPatient_ToothCheck.EXTRACTION,
                    .NewFACING_DIRECT_VENEERS = newPatient_ToothCheck.FACING_DIRECT_VENEERS, .OldFACING_DIRECT_VENEERS = oldPatient_ToothCheck.FACING_DIRECT_VENEERS,
                    .NewFIBER_POST = newPatient_ToothCheck.FIBER_POST, .OldFIBER_POST = oldPatient_ToothCheck.FIBER_POST,
                    .NewFISSURE_SEALENT = newPatient_ToothCheck.FISSURE_SEALENT, .OldFISSURE_SEALENT = oldPatient_ToothCheck.FISSURE_SEALENT,
                    .NewHEMISECTION = newPatient_ToothCheck.HEMISECTION, .OldHEMISECTION = oldPatient_ToothCheck.HEMISECTION,
                    .NewIMPLANT = newPatient_ToothCheck.IMPLANT, .OldIMPLANT = oldPatient_ToothCheck.IMPLANT,
                    .NewINDIRECT_PULP_CAPPING = newPatient_ToothCheck.INDIRECT_PULP_CAPPING, .OldINDIRECT_PULP_CAPPING = oldPatient_ToothCheck.INDIRECT_PULP_CAPPING,
                    .NewINDIRECT_VENEERS = newPatient_ToothCheck.INDIRECT_VENEERS, .OldINDIRECT_VENEERS = oldPatient_ToothCheck.INDIRECT_VENEERS,
                    .NewMETAL_POST = newPatient_ToothCheck.METAL_POST, .OldMETAL_POST = oldPatient_ToothCheck.METAL_POST,
                    .NewMTA_BULK_FLOW = newPatient_ToothCheck.MTA_BULK_FLOW, .OldMTA_BULK_FLOW = oldPatient_ToothCheck.MTA_BULK_FLOW,
                    .NewPARTIAL_PULPOTOMY = newPatient_ToothCheck.PARTIAL_PULPOTOMY, .OldPARTIAL_PULPOTOMY = oldPatient_ToothCheck.PARTIAL_PULPOTOMY,
                    .NewPERIAPICAL_LESION = newPatient_ToothCheck.PERIAPICAL_LESION, .OldPERIAPICAL_LESION = oldPatient_ToothCheck.PERIAPICAL_LESION,
                    .NewPULPOTOMY = newPatient_ToothCheck.PULPOTOMY, .OldPULPOTOMY = oldPatient_ToothCheck.PULPOTOMY,
                    .NewRCC_TF = newPatient_ToothCheck.RCC_TF, .OldRCC_TF = oldPatient_ToothCheck.RCC_TF,
                    .NewRCO_TF = newPatient_ToothCheck.RCO_TF, .OldRCO_TF = oldPatient_ToothCheck.RCO_TF,
                    .NewRCF_AMALGAM = newPatient_ToothCheck.RCF_AMALGAM, .OldRCF_AMALGAM = oldPatient_ToothCheck.RCF_AMALGAM,
                    .NewRCF_COMPOSITE = newPatient_ToothCheck.RCF_COMPOSITE, .OldRCF_COMPOSITE = oldPatient_ToothCheck.RCF_COMPOSITE,
                    .NewRCF_GI = newPatient_ToothCheck.RCF_GI, .OldRCF_GI = oldPatient_ToothCheck.RCF_GI,
                    .NewREDO_AMALGAM = newPatient_ToothCheck.REDO_AMALGAM, .OldREDO_AMALGAM = oldPatient_ToothCheck.REDO_AMALGAM,
                    .NewREDO_COMPOSITE = newPatient_ToothCheck.REDO_COMPOSITE, .OldREDO_COMPOSITE = oldPatient_ToothCheck.REDO_COMPOSITE,
                    .NewREDO_GI = newPatient_ToothCheck.REDO_GI, .OldREDO_GI = oldPatient_ToothCheck.REDO_GI,
                    .NewREDO_RCT = newPatient_ToothCheck.REDO_RCT, .OldREDO_RCT = oldPatient_ToothCheck.REDO_RCT,
                    .NewRC_MED_TF = newPatient_ToothCheck.RC_MED_TF, .OldRC_MED_TF = oldPatient_ToothCheck.RC_MED_TF,
                    .NewROOT_CARIES = newPatient_ToothCheck.ROOT_CARIES, .OldROOT_CARIES = oldPatient_ToothCheck.ROOT_CARIES,
                    .NewSTAINLESS_STEEL_CROWN = newPatient_ToothCheck.STAINLESS_STEEL_CROWN, .OldSTAINLESS_STEEL_CROWN = oldPatient_ToothCheck.STAINLESS_STEEL_CROWN,
                    .NewTEMPORARY_CROWN = newPatient_ToothCheck.TEMPORARY_CROWN, .OldTEMPORARY_CROWN = oldPatient_ToothCheck.TEMPORARY_CROWN,
                    .NewTEMPORARY_FILLING = newPatient_ToothCheck.TEMPORARY_FILLING, .OldTEMPORARY_FILLING = oldPatient_ToothCheck.TEMPORARY_FILLING,
                    .NewCheckDate = newPatient_ToothCheck.CheckDate, .OldCheckDate = oldPatient_ToothCheck.CheckDate,
                    .NewCheckNotes = newPatient_ToothCheck.CheckNotes, .OldCheckNotes = oldPatient_ToothCheck.CheckNotes
                                          }

            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_ToothCheck] SET 
			[ToothNum] = @NewToothNum,
			[PatientID] = @NewPatientID,
			[ToothName] = @NewToothName,
			[ISEXIST] = @NewISEXIST,
			[ABCESS_DRAINAGE] = @NewABCESS_DRAINAGE,
			[APICECTOMY_PIC] = @NewAPICECTOMY_PIC,
			[APEXIFICATION] = @NewAPEXIFICATION,
			[BUILD_UP_COM] = @NewBUILD_UP_COM,
			[BUILD_UP_ACR] = @NewBUILD_UP_ACR,
			[BUILD_UP_GI] = @NewBUILD_UP_GI,
			[CLASS_1_AMALGAM] = @NewCLASS_1_AMALGAM,
			[CLASS_1_COMPOSITE] = @NewCLASS_1_COMPOSITE,
			[CLASS_1_GI] = @NewCLASS_1_GI,
			[CLASS_1_TF] = @NewCLASS_1_TF,
			[CLASS_2_D_AMALGAM] = @NewCLASS_2_D_AMALGAM,
			[CLASS_2_D_COMPOSITE] = @NewCLASS_2_D_COMPOSITE,
			[CLASS_2_D_GI] = @NewCLASS_2_D_GI,
			[CLASS_2_D_TF] = @NewCLASS_2_D_TF,
			[CLASS_2_M_AMALGAM] = @NewCLASS_2_M_AMALGAM,
			[CLASS_2_M_COMPOSITE] = @NewCLASS_2_M_COMPOSITE,
			[CLASS_2_M_GI] = @NewCLASS_2_M_GI,
			[CLASS_2_M_TF] = @NewCLASS_2_M_TF,
			[CLASS_2_MOD_AMALGAM] = @NewCLASS_2_MOD_AMALGAM,
			[CLASS_2_MOD_COMPOSITE] = @NewCLASS_2_MOD_COMPOSITE,
			[CLASS_2_MOD_GI] = @NewCLASS_2_MOD_GI,
			[CLASS_2_MOD_TF] = @NewCLASS_2_MOD_TF,
			[CLASS_3_D_COMPOSITE] = @NewCLASS_3_D_COMPOSITE,
			[CLASS_3_D_GI] = @NewCLASS_3_D_GI,
			[CLASS_3_D_TF] = @NewCLASS_3_D_TF,
			[CLASS_3_M_COMPOSITE] = @NewCLASS_3_M_COMPOSITE,
			[CLASS_3_M_GI] = @NewCLASS_3_M_GI,
			[CLASS_3_M_TF] = @NewCLASS_3_M_TF,
			[CLASS_4_D_COMPOSITE] = @NewCLASS_4_D_COMPOSITE,
			[CLASS_4_D_GI] = @NewCLASS_4_D_GI,
			[CLASS_4_D_TF] = @NewCLASS_4_D_TF,
			[CLASS_4_M_COMPOSITE] = @NewCLASS_4_M_COMPOSITE,
			[CLASS_4_M_GI] = @NewCLASS_4_M_GI,
			[CLASS_4_M_TF] = @NewCLASS_4_M_TF,
			[CLASS_4_INCISAL] = @NewCLASS_4_INCISAL,
			[CLASS_5_AMALGAM] = @NewCLASS_5_AMALGAM,
			[CLASS_5_COMPOSITE] = @NewCLASS_5_COMPOSITE,
			[CLASS_5_GI] = @NewCLASS_5_GI,
			[CLASS_5_TF] = @NewCLASS_5_TF,
			[CRACK] = @NewCRACK,
			[CROWN_LENGTHENING] = @NewCROWN_LENGTHENING,
			[DIRECT_PULP_CAPPING] = @NewDIRECT_PULP_CAPPING,
			[EXTRACTION] = @NewEXTRACTION,
			[FACING_DIRECT_VENEERS] = @NewFACING_DIRECT_VENEERS,
			[FIBER_POST] = @NewFIBER_POST,
			[FISSURE_SEALENT] = @NewFISSURE_SEALENT,
			[HEMISECTION] = @NewHEMISECTION,
			[IMPLANT] = @NewIMPLANT,
			[INDIRECT_PULP_CAPPING] = @NewINDIRECT_PULP_CAPPING,
			[INDIRECT_VENEERS] = @NewINDIRECT_VENEERS,
			[METAL_POST] = @NewMETAL_POST,
			[MTA_BULK_FLOW] = @NewMTA_BULK_FLOW,
			[PARTIAL_PULPOTOMY] = @NewPARTIAL_PULPOTOMY,
			[PERIAPICAL_LESION] = @NewPERIAPICAL_LESION,
			[PULPOTOMY] = @NewPULPOTOMY,
			[RCC_TF] = @NewRCC_TF,
			[RCO_TF] = @NewRCO_TF,
			[RCF_AMALGAM] = @NewRCF_AMALGAM,
			[RCF_COMPOSITE] = @NewRCF_COMPOSITE,
			[RCF_GI] = @NewRCF_GI,
			[REDO_AMALGAM] = @NewREDO_AMALGAM,
			[REDO_COMPOSITE] = @NewREDO_COMPOSITE,
			[REDO_GI] = @NewREDO_GI,
			[REDO_RCT] = @NewREDO_RCT,
			[RC_MED_TF] = @NewRC_MED_TF,
			[ROOT_CARIES] = @NewROOT_CARIES,
			[STAINLESS_STEEL_CROWN] = @NewSTAINLESS_STEEL_CROWN,
			[TEMPORARY_CROWN] = @NewTEMPORARY_CROWN,
			[TEMPORARY_FILLING] = @NewTEMPORARY_FILLING,
			[CheckDate] = @NewCheckDate,
			[CheckNotes] = @NewCheckNotes			 WHERE 
			[ToothNum] = @OldToothNum And
			[PatientID] = @OldPatientID And
			[ToothName] = @OldToothName", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Update1(oldPatient_ToothCheck As Patient_ToothCheck, newPatient_ToothCheck As Patient_ToothCheck) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            ' Define the SQL query for updating records
            Dim query As String = "
                    UPDATE [Patient_ToothCheck]
                    SET 
			            [ToothNum] = @NewToothNum,
			            [PatientID] = @NewPatientID,
			            [ToothName] = @NewToothName,
			            [APICECTOMY] = @NewAPICECTOMY,
			            [ABCESS_DRAINAGE] = @NewABCESS_DRAINAGE,
			            [APEXIFICATION] = @NewAPEXIFICATION,
			            [ISEXIST] = @NewISEXIST,
			            [BUCCAL_SURFACE] = @NewBUCCAL_SURFACE,
			            [BUILD_UP_COM] = @NewBUILD_UP_COM,
			            [BUILD_UP_ACR] = @NewBUILD_UP_ACR,
			            [BUILD_UP_GI] = @NewBUILD_UP_GI,
			            [CLASS_1_AMALGAM] = @NewCLASS_1_AMALGAM,
			            [CLASS_1_COMPOSITE] = @NewCLASS_1_COMPOSITE,
			            [CLASS_1_GI_RESTORATION] = @NewCLASS_1_GI_RESTORATION,
			            [CLASS_2_D_AMALGAM] = @NewCLASS_2_D_AMALGAM,
			            [CLASS_2_D_COMPOSITE] = @NewCLASS_2_D_COMPOSITE,
			            [CLASS_2_D_GI_RESTORATION] = @NewCLASS_2_D_GI_RESTORATION,
			            [CLASS_2_M_AMALGAM] = @NewCLASS_2_M_AMALGAM,
			            [CLASS_2_M_COMPOSITE] = @NewCLASS_2_M_COMPOSITE,
			            [CLASS_2_M_GI_RESTORATION] = @NewCLASS_2_M_GI_RESTORATION,
			            [CLASS_2_MOD_AMALGAM] = @NewCLASS_2_MOD_AMALGAM,
			            [CLASS_2_MOD_COMPOSITE] = @NewCLASS_2_MOD_COMPOSITE,
			            [CLASS_2_MOD_GI_RESTORATION] = @NewCLASS_2_MOD_GI_RESTORATION,
			            [CLASS_3_D_AMALGAM] = @NewCLASS_3_D_AMALGAM,
			            [CLASS_3_D_COMPOSITE] = @NewCLASS_3_D_COMPOSITE,
			            [CLASS_3_D_GI_RESTORATION] = @NewCLASS_3_D_GI_RESTORATION,
			            [CLASS_3_M_AMALGAM] = @NewCLASS_3_M_AMALGAM,
			            [CLASS_3_M_COMPOSITE] = @NewCLASS_3_M_COMPOSITE,
			            [CLASS_3_M_GI_RESTORATION] = @NewCLASS_3_M_GI_RESTORATION,
			            [CLASS_4_D_AMALGAM] = @NewCLASS_4_D_AMALGAM,
			            [CLASS_4_D_COMPOSITE] = @NewCLASS_4_D_COMPOSITE,
			            [CLASS_4_D_GI_RESTORATION] = @NewCLASS_4_D_GI_RESTORATION,
			            [CLASS_4_M_AMALGAM] = @NewCLASS_4_M_AMALGAM,
			            [CLASS_4_M_COMPOSITE] = @NewCLASS_4_M_COMPOSITE,
			            [CLASS_4_M_GI_RESTORATION] = @NewCLASS_4_M_GI_RESTORATION,
			            [CLASS_4_INCISAL] = @NewCLASS_4_INCISAL,
			            [CLASS_5_AMALGAM] = @NewCLASS_5_AMALGAM,
			            [CLASS_5_COMPOSITE] = @NewCLASS_5_COMPOSITE,
			            [CLASS_5_GI_RESTORATION] = @NewCLASS_5_GI_RESTORATION,
			            [CROWN_LENGTHENING] = @NewCROWN_LENGTHENING,
			            [DIRECT_PULP_CAPPING] = @NewDIRECT_PULP_CAPPING,
			            [EXTRACTION] = @NewEXTRACTION,
			            [FACING_DIRECT_VENEERS] = @NewFACING_DIRECT_VENEERS,
			            [FIBER_POST] = @NewFIBER_POST,
			            [FISSURE_SEALENT] = @NewFISSURE_SEALENT,
			            [GI_RESTORATION] = @NewGI_RESTORATION,
			            [HEMISECTION] = @NewHEMISECTION,
			            [INDIRECT_PULP_CAPPING] = @NewINDIRECT_PULP_CAPPING,
			            [INDIRECT_VENEERS] = @NewINDIRECT_VENEERS,
			            [METAL_POST] = @NewMETAL_POST,
			            [MTA_BULK_FLOW] = @NewMTA_BULK_FLOW,
			            [PALATAL_SURFACE] = @NewPALATAL_SURFACE,
			            [PARTIAL_PULPOTOMY] = @NewPARTIAL_PULPOTOMY,
			            [PERIAPICAL_LESION] = @NewPERIAPICAL_LESION,
			            [PULPOTOMY] = @NewPULPOTOMY,
			            [PULPOTOMY_MTA] = @NewPULPOTOMY_MTA,
			            [RCC] = @NewRCC,
			            [RCF_AMALGAM] = @NewRCF_AMALGAM,
			            [RCF_COMPOSITE] = @NewRCF_COMPOSITE,
			            [RCF_GI] = @NewRCF_GI,
			            [RCO] = @NewRCO,
			            [RCT] = @NewRCT,
			            [RCT_NECROTIC] = @NewRCT_NECROTIC,
			            [REDO_AMALGAM] = @NewREDO_AMALGAM,
			            [REDO_COMPOSITE] = @NewREDO_COMPOSITE,
			            [REDO_GI] = @NewREDO_GI,
			            [REDO_RCT] = @NewREDO_RCT,
			            [ROOT_CANAL_MEDICAMENT] = @NewROOT_CANAL_MEDICAMENT,
			            [ROOT_CARIES] = @NewROOT_CARIES,
			            [STAINLESS_STEEL_CROWN] = @NewSTAINLESS_STEEL_CROWN,
			            [TEMPORARY_ACRYL] = @NewTEMPORARY_ACRYL,
			            [TF] = @NewTF,
			            [CheckDate] = @NewCheckDate,
			            [CheckNotes] = @NewCheckNotes
                    WHERE 
                        [ToothNum] = @OldToothNum
                        AND [PatientID] = @OldPatientID
                        AND [ToothName] = @OldToothName
                        ;
                "

            ' Create the parameters object with all fields
            Dim parameters = New With {
                    .NewToothNum = newPatient_ToothCheck.ToothNum, .OldToothNum = oldPatient_ToothCheck.ToothNum,
                    .NewPatientID = newPatient_ToothCheck.PatientID, .OldPatientID = oldPatient_ToothCheck.PatientID,
                    .NewToothName = newPatient_ToothCheck.ToothName, .OldToothName = oldPatient_ToothCheck.ToothName,
                    .NewISEXIST = newPatient_ToothCheck.ISEXIST, .OldISEXIST = oldPatient_ToothCheck.ISEXIST,
                    .NewABCESS_DRAINAGE = newPatient_ToothCheck.ABCESS_DRAINAGE, .OldABCESS_DRAINAGE = oldPatient_ToothCheck.ABCESS_DRAINAGE,
                    .NewAPICECTOMY_PIC = newPatient_ToothCheck.APICECTOMY_PIC, .OldAPICECTOMY_PIC = oldPatient_ToothCheck.APICECTOMY_PIC,
                    .NewAPEXIFICATION = newPatient_ToothCheck.APEXIFICATION, .OldAPEXIFICATION = oldPatient_ToothCheck.APEXIFICATION,
                    .NewBUILD_UP_COM = newPatient_ToothCheck.BUILD_UP_COM, .OldBUILD_UP_COM = oldPatient_ToothCheck.BUILD_UP_COM,
                    .NewBUILD_UP_ACR = newPatient_ToothCheck.BUILD_UP_ACR, .OldBUILD_UP_ACR = oldPatient_ToothCheck.BUILD_UP_ACR,
                    .NewBUILD_UP_GI = newPatient_ToothCheck.BUILD_UP_GI, .OldBUILD_UP_GI = oldPatient_ToothCheck.BUILD_UP_GI,
                    .NewCLASS_1_AMALGAM = newPatient_ToothCheck.CLASS_1_AMALGAM, .OldCLASS_1_AMALGAM = oldPatient_ToothCheck.CLASS_1_AMALGAM,
                    .NewCLASS_1_COMPOSITE = newPatient_ToothCheck.CLASS_1_COMPOSITE, .OldCLASS_1_COMPOSITE = oldPatient_ToothCheck.CLASS_1_COMPOSITE,
                    .NewCLASS_1_GI = newPatient_ToothCheck.CLASS_1_GI, .OldCLASS_1_GI = oldPatient_ToothCheck.CLASS_1_GI,
                    .NewCLASS_1_TF = newPatient_ToothCheck.CLASS_1_TF, .OldCLASS_1_TF = oldPatient_ToothCheck.CLASS_1_TF,
                    .NewCLASS_2_D_AMALGAM = newPatient_ToothCheck.CLASS_2_D_AMALGAM, .OldCLASS_2_D_AMALGAM = oldPatient_ToothCheck.CLASS_2_D_AMALGAM,
                    .NewCLASS_2_D_COMPOSITE = newPatient_ToothCheck.CLASS_2_D_COMPOSITE, .OldCLASS_2_D_COMPOSITE = oldPatient_ToothCheck.CLASS_2_D_COMPOSITE,
                    .NewCLASS_2_D_GI = newPatient_ToothCheck.CLASS_2_D_GI, .OldCLASS_2_D_GI = oldPatient_ToothCheck.CLASS_2_D_GI,
                    .NewCLASS_2_D_TF = newPatient_ToothCheck.CLASS_2_D_TF, .OldCLASS_2_D_TF = oldPatient_ToothCheck.CLASS_2_D_TF,
                    .NewCLASS_2_M_AMALGAM = newPatient_ToothCheck.CLASS_2_M_AMALGAM, .OldCLASS_2_M_AMALGAM = oldPatient_ToothCheck.CLASS_2_M_AMALGAM,
                    .NewCLASS_2_M_COMPOSITE = newPatient_ToothCheck.CLASS_2_M_COMPOSITE, .OldCLASS_2_M_COMPOSITE = oldPatient_ToothCheck.CLASS_2_M_COMPOSITE,
                    .NewCLASS_2_M_GI = newPatient_ToothCheck.CLASS_2_M_GI, .OldCLASS_2_M_GI = oldPatient_ToothCheck.CLASS_2_M_GI,
                    .NewCLASS_2_M_TF = newPatient_ToothCheck.CLASS_2_M_TF, .OldCLASS_2_M_TF = oldPatient_ToothCheck.CLASS_2_M_TF,
                    .NewCLASS_2_MOD_AMALGAM = newPatient_ToothCheck.CLASS_2_MOD_AMALGAM, .OldCLASS_2_MOD_AMALGAM = oldPatient_ToothCheck.CLASS_2_MOD_AMALGAM,
                    .NewCLASS_2_MOD_COMPOSITE = newPatient_ToothCheck.CLASS_2_MOD_COMPOSITE, .OldCLASS_2_MOD_COMPOSITE = oldPatient_ToothCheck.CLASS_2_MOD_COMPOSITE,
                    .NewCLASS_2_MOD_GI = newPatient_ToothCheck.CLASS_2_MOD_GI, .OldCLASS_2_MOD_GI = oldPatient_ToothCheck.CLASS_2_MOD_GI,
                    .NewCLASS_2_MOD_TF = newPatient_ToothCheck.CLASS_2_MOD_TF, .OldCLASS_2_MOD_TF = oldPatient_ToothCheck.CLASS_2_MOD_TF,
                    .NewCLASS_3_D_COMPOSITE = newPatient_ToothCheck.CLASS_3_D_COMPOSITE, .OldCLASS_3_D_COMPOSITE = oldPatient_ToothCheck.CLASS_3_D_COMPOSITE,
                    .NewCLASS_3_D_GI = newPatient_ToothCheck.CLASS_3_D_GI, .OldCLASS_3_D_GI = oldPatient_ToothCheck.CLASS_3_D_GI,
                    .NewCLASS_3_D_TF = newPatient_ToothCheck.CLASS_3_D_TF, .OldCLASS_3_D_TF = oldPatient_ToothCheck.CLASS_3_D_TF,
                    .NewCLASS_3_M_COMPOSITE = newPatient_ToothCheck.CLASS_3_M_COMPOSITE, .OldCLASS_3_M_COMPOSITE = oldPatient_ToothCheck.CLASS_3_M_COMPOSITE,
                    .NewCLASS_3_M_GI = newPatient_ToothCheck.CLASS_3_M_GI, .OldCLASS_3_M_GI = oldPatient_ToothCheck.CLASS_3_M_GI,
                    .NewCLASS_3_M_TF = newPatient_ToothCheck.CLASS_3_M_TF, .OldCLASS_3_M_TF = oldPatient_ToothCheck.CLASS_3_M_TF,
                    .NewCLASS_4_D_COMPOSITE = newPatient_ToothCheck.CLASS_4_D_COMPOSITE, .OldCLASS_4_D_COMPOSITE = oldPatient_ToothCheck.CLASS_4_D_COMPOSITE,
                    .NewCLASS_4_D_GI = newPatient_ToothCheck.CLASS_4_D_GI, .OldCLASS_4_D_GI = oldPatient_ToothCheck.CLASS_4_D_GI,
                    .NewCLASS_4_D_TF = newPatient_ToothCheck.CLASS_4_D_TF, .OldCLASS_4_D_TF = oldPatient_ToothCheck.CLASS_4_D_TF,
                    .NewCLASS_4_M_COMPOSITE = newPatient_ToothCheck.CLASS_4_M_COMPOSITE, .OldCLASS_4_M_COMPOSITE = oldPatient_ToothCheck.CLASS_4_M_COMPOSITE,
                    .NewCLASS_4_M_GI = newPatient_ToothCheck.CLASS_4_M_GI, .OldCLASS_4_M_GI = oldPatient_ToothCheck.CLASS_4_M_GI,
                    .NewCLASS_4_M_TF = newPatient_ToothCheck.CLASS_4_M_TF, .OldCLASS_4_M_TF = oldPatient_ToothCheck.CLASS_4_M_TF,
                    .NewCLASS_4_INCISAL = newPatient_ToothCheck.CLASS_4_INCISAL, .OldCLASS_4_INCISAL = oldPatient_ToothCheck.CLASS_4_INCISAL,
                    .NewCLASS_5_AMALGAM = newPatient_ToothCheck.CLASS_5_AMALGAM, .OldCLASS_5_AMALGAM = oldPatient_ToothCheck.CLASS_5_AMALGAM,
                    .NewCLASS_5_COMPOSITE = newPatient_ToothCheck.CLASS_5_COMPOSITE, .OldCLASS_5_COMPOSITE = oldPatient_ToothCheck.CLASS_5_COMPOSITE,
                    .NewCLASS_5_GI = newPatient_ToothCheck.CLASS_5_GI, .OldCLASS_5_GI = oldPatient_ToothCheck.CLASS_5_GI,
                    .NewCLASS_5_TF = newPatient_ToothCheck.CLASS_5_TF, .OldCLASS_5_TF = oldPatient_ToothCheck.CLASS_5_TF,
                    .NewCRACK = newPatient_ToothCheck.CRACK, .OldCRACK = oldPatient_ToothCheck.CRACK,
                    .NewCROWN_LENGTHENING = newPatient_ToothCheck.CROWN_LENGTHENING, .OldCROWN_LENGTHENING = oldPatient_ToothCheck.CROWN_LENGTHENING,
                    .NewDIRECT_PULP_CAPPING = newPatient_ToothCheck.DIRECT_PULP_CAPPING, .OldDIRECT_PULP_CAPPING = oldPatient_ToothCheck.DIRECT_PULP_CAPPING,
                    .NewEXTRACTION = newPatient_ToothCheck.EXTRACTION, .OldEXTRACTION = oldPatient_ToothCheck.EXTRACTION,
                    .NewFACING_DIRECT_VENEERS = newPatient_ToothCheck.FACING_DIRECT_VENEERS, .OldFACING_DIRECT_VENEERS = oldPatient_ToothCheck.FACING_DIRECT_VENEERS,
                    .NewFIBER_POST = newPatient_ToothCheck.FIBER_POST, .OldFIBER_POST = oldPatient_ToothCheck.FIBER_POST,
                    .NewFISSURE_SEALENT = newPatient_ToothCheck.FISSURE_SEALENT, .OldFISSURE_SEALENT = oldPatient_ToothCheck.FISSURE_SEALENT,
                    .NewHEMISECTION = newPatient_ToothCheck.HEMISECTION, .OldHEMISECTION = oldPatient_ToothCheck.HEMISECTION,
                    .NewIMPLANT = newPatient_ToothCheck.IMPLANT, .OldIMPLANT = oldPatient_ToothCheck.IMPLANT,
                    .NewINDIRECT_PULP_CAPPING = newPatient_ToothCheck.INDIRECT_PULP_CAPPING, .OldINDIRECT_PULP_CAPPING = oldPatient_ToothCheck.INDIRECT_PULP_CAPPING,
                    .NewINDIRECT_VENEERS = newPatient_ToothCheck.INDIRECT_VENEERS, .OldINDIRECT_VENEERS = oldPatient_ToothCheck.INDIRECT_VENEERS,
                    .NewMETAL_POST = newPatient_ToothCheck.METAL_POST, .OldMETAL_POST = oldPatient_ToothCheck.METAL_POST,
                    .NewMTA_BULK_FLOW = newPatient_ToothCheck.MTA_BULK_FLOW, .OldMTA_BULK_FLOW = oldPatient_ToothCheck.MTA_BULK_FLOW,
                    .NewPARTIAL_PULPOTOMY = newPatient_ToothCheck.PARTIAL_PULPOTOMY, .OldPARTIAL_PULPOTOMY = oldPatient_ToothCheck.PARTIAL_PULPOTOMY,
                    .NewPERIAPICAL_LESION = newPatient_ToothCheck.PERIAPICAL_LESION, .OldPERIAPICAL_LESION = oldPatient_ToothCheck.PERIAPICAL_LESION,
                    .NewPULPOTOMY = newPatient_ToothCheck.PULPOTOMY, .OldPULPOTOMY = oldPatient_ToothCheck.PULPOTOMY,
                    .NewRCC_TF = newPatient_ToothCheck.RCC_TF, .OldRCC_TF = oldPatient_ToothCheck.RCC_TF,
                    .NewRCO_TF = newPatient_ToothCheck.RCO_TF, .OldRCO_TF = oldPatient_ToothCheck.RCO_TF,
                    .NewRCF_AMALGAM = newPatient_ToothCheck.RCF_AMALGAM, .OldRCF_AMALGAM = oldPatient_ToothCheck.RCF_AMALGAM,
                    .NewRCF_COMPOSITE = newPatient_ToothCheck.RCF_COMPOSITE, .OldRCF_COMPOSITE = oldPatient_ToothCheck.RCF_COMPOSITE,
                    .NewRCF_GI = newPatient_ToothCheck.RCF_GI, .OldRCF_GI = oldPatient_ToothCheck.RCF_GI,
                    .NewREDO_AMALGAM = newPatient_ToothCheck.REDO_AMALGAM, .OldREDO_AMALGAM = oldPatient_ToothCheck.REDO_AMALGAM,
                    .NewREDO_COMPOSITE = newPatient_ToothCheck.REDO_COMPOSITE, .OldREDO_COMPOSITE = oldPatient_ToothCheck.REDO_COMPOSITE,
                    .NewREDO_GI = newPatient_ToothCheck.REDO_GI, .OldREDO_GI = oldPatient_ToothCheck.REDO_GI,
                    .NewREDO_RCT = newPatient_ToothCheck.REDO_RCT, .OldREDO_RCT = oldPatient_ToothCheck.REDO_RCT,
                    .NewRC_MEDICAMENT_TF = newPatient_ToothCheck.RC_MED_TF, .OldRC_MEDICAMENT_TF = oldPatient_ToothCheck.RC_MED_TF,
                    .NewROOT_CARIES = newPatient_ToothCheck.ROOT_CARIES, .OldROOT_CARIES = oldPatient_ToothCheck.ROOT_CARIES,
                    .NewSTAINLESS_STEEL_CROWN = newPatient_ToothCheck.STAINLESS_STEEL_CROWN, .OldSTAINLESS_STEEL_CROWN = oldPatient_ToothCheck.STAINLESS_STEEL_CROWN,
                    .NewTEMPORARY_CROWN = newPatient_ToothCheck.TEMPORARY_CROWN, .OldTEMPORARY_CROWN = oldPatient_ToothCheck.TEMPORARY_CROWN,
                    .NewTEMPORARY_FILLING = newPatient_ToothCheck.TEMPORARY_FILLING, .OldTEMPORARY_FILLING = oldPatient_ToothCheck.TEMPORARY_FILLING,
                    .NewCheckDate = newPatient_ToothCheck.CheckDate, .OldCheckDate = oldPatient_ToothCheck.CheckDate,
                    .NewCheckNotes = newPatient_ToothCheck.CheckNotes, .OldCheckNotes = oldPatient_ToothCheck.CheckNotes
                                          }


            Try
                ' Execute the query and check for affected rows
                Dim affectedRows As Integer = conn.Execute(query, parameters)
                Return affectedRows > 0
            Catch ex As Exception
                ' Log or display the error message
                MessageBox.Show("Error updating record: " & ex.Message)
                Return False
            End Try
        End Using
    End Function

    Public Function Delete(ByVal clsPatient_ToothCheck As Patient_ToothCheck) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [Patient_ToothCheck] 
			WHERE ToothNum = @ToothNum AND PatientID = @PatientID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.ToothNum = clsPatient_ToothCheck.ToothNum, .PatientID = clsPatient_ToothCheck.PatientID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
    Public Function GetPatient(ByVal PatientID As Integer) As Patient
        Dim parent As Patient = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientId = PatientID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

End Class








'Imports System
'Imports System.Collections.Generic
'Imports System.Text
'Imports System.Data
'Imports System.Data.SqlClient
'Imports System.Collections
'Imports System.Configuration
'Imports Dapper
'Imports System.Windows.Forms

'Public Class Patient_ToothCheckDATA

'    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

'    'Public Function Clone() As Patient_ToothCheck
'    '    Return DirectCast(Me.MemberwiseClone(), Patient_ToothCheck)
'    'End Function


'    Public Function SelectAll() As IEnumerable(Of Patient_ToothCheck)
'        Using conn As New SqlConnection(ConnectionString)
'            conn.Open()
'            Return conn.Query(Of Patient_ToothCheck)("SELECT * FROM Patient_ToothCheck")
'        End Using
'    End Function


'    Public Function Select_Record(ByVal clsPatient_ToothCheck As Patient_ToothCheck) As Patient_ToothCheck
'        Using conn As New SqlConnection(ConnectionString)
'            Dim sql As String = "Select * FROM Patient_ToothCheck WHERE ToothNum = @ToothNum And PatientID = @PatientID"
'            Return conn.QuerySingleOrDefault(Of Patient_ToothCheck)(sql, New With {.ToothNum = clsPatient_ToothCheck.ToothNum, .PatientID = clsPatient_ToothCheck.PatientID})
'        End Using
'    End Function

'    Public Function Add(ByVal clsPatient_ToothCheck As Patient_ToothCheck) As Boolean
'        Dim RowsAffected As Integer = 0
'        Using conn As New SqlConnection(ConnectionString)
'            Dim sql As String = "INSERT INTO Patient_ToothCheck (ToothNum, PatientID, ToothName, ABCESS_DRAINAGE, IsExist, Build_Up_Com, Build_Up_Acr, Build_Up_GI,
'                                Class_1_Amalgam, Class_1_Composite, Class_2_D_Amalgam, Class_2_D_Composite, Class_2_M_Amalgam, Class_2_M_Composite, 
'                                Class_2_MOD_Amalgam, Class_2_MOD_Composite, Class_3_D_Amalgam, Class_3_D_Composite, Class_3_M_Amalgam, Class_3_M_Composite, 
'                                Class_4_D_Amalgam, Class_4_D_Composite, Class_4_M_Amalgam, Class_4_M_Composite, Class_4_Incisal, Class_5_Amalgam, Class_5_Composite,
'                                Crown_Lengthening, Direct_Pulpcapping, EXTRACTION, Facing_Direct_Veneers, FIBER_POST, Fissure_Sealent, GI_Restoration, GI_M, GI, 
'                                GI_D, Hemisection, Indirect_Pulpcapping, Indirect_Veneers, Metal_Post, MTA_Bulk_Flow, PULPOTOMY, PULPOTOMY_MTA, Partial_Pulpotomy, 
'                                Root_Canal_Medicament, RCC, RCF_Amalgam, RCF_Composite, RCO, RCT, REDO_Amalgam, REDO_Composite, REDO_RCT, RCT_NECROTIC, 
'                                Stainless_Steel_Crown, Temporary_Acryl, TF, CheckDate, CheckNotes)
'                                VALUES 
'                                (@ToothNum, @PatientID, @ToothName, @ABCESS_DRAINAGE, @IsExist, @Build_Up_Com, @Build_Up_Acr, @Build_Up_GI, @Class_1_Amalgam, 
'                                 @Class_1_Composite, @Class_2_D_Amalgam, @Class_2_D_Composite, @Class_2_M_Amalgam, @Class_2_M_Composite, @Class_2_MOD_Amalgam,
'                                 @Class_2_MOD_Composite, @Class_3_D_Amalgam, @Class_3_D_Composite, @Class_3_M_Amalgam, @Class_3_M_Composite, @Class_4_D_Amalgam,
'                                 @Class_4_D_Composite, @Class_4_M_Amalgam, @Class_4_M_Composite, @Class_4_Incisal, @Class_5_Amalgam, @Class_5_Composite, 
'                                 @Crown_Lengthening, @Direct_Pulpcapping, @EXTRACTION, @Facing_Direct_Veneers, @FIBER_POST, @Fissure_Sealent, @GI_Restoration, 
'                                 @GI_M, @GI, @GI_D, @Hemisection, @Indirect_Pulpcapping, @Indirect_Veneers, @Metal_Post, @MTA_Bulk_Flow, @PULPOTOMY, 
'                                 @PULPOTOMY_MTA, @Partial_Pulpotomy, @Root_Canal_Medicament, @RCC, @RCF_Amalgam, @RCF_Composite, @RCO, @RCT, @REDO_Amalgam, 
'                                 @REDO_Composite, @REDO_RCT, @RCT_NECROTIC, @Stainless_Steel_Crown, @Temporary_Acryl, @TF, @CheckDate, @CheckNotes)"
'            RowsAffected = conn.Execute(sql, New With {.ToothNum = clsPatient_ToothCheck.ToothNum, .PatientID = clsPatient_ToothCheck.PatientID, .ToothName = clsPatient_ToothCheck.ToothName, .ABCESS_DRAINAGE = clsPatient_ToothCheck.ABCESS_DRAINAGE, .IsExist = clsPatient_ToothCheck.IsExist, .Build_Up_Com = clsPatient_ToothCheck.BUILD_UP_COM, .Build_Up_Acr = clsPatient_ToothCheck.BUILD_UP_ACR, .Build_Up_GI = clsPatient_ToothCheck.BUILD_UP_GI, .Class_1_Amalgam = clsPatient_ToothCheck.CLASS_1_AMALGAM, .Class_1_Composite = clsPatient_ToothCheck.CLASS_1_COMPOSITE, .Class_2_D_Amalgam = clsPatient_ToothCheck.CLASS_2_D_AMALGAM, .Class_2_D_Composite = clsPatient_ToothCheck.CLASS_2_D_COMPOSITE, .Class_2_M_Amalgam = clsPatient_ToothCheck.CLASS_2_M_AMALGAM, .Class_2_M_Composite = clsPatient_ToothCheck.CLASS_2_M_COMPOSITE, .Class_2_MOD_Amalgam = clsPatient_ToothCheck.CLASS_2_MOD_AMALGAM, .Class_2_MOD_Composite = clsPatient_ToothCheck.CLASS_2_MOD_COMPOSITE, .Class_3_D_Amalgam = clsPatient_ToothCheck.CLASS_3_D_AMALGAM, .Class_3_D_Composite = clsPatient_ToothCheck.CLASS_3_D_COMPOSITE, .Class_3_M_Amalgam = clsPatient_ToothCheck.CLASS_3_M_AMALGAM, .Class_3_M_Composite = clsPatient_ToothCheck.CLASS_3_M_COMPOSITE, .Class_4_D_Amalgam = clsPatient_ToothCheck.CLASS_4_D_AMALGAM, .Class_4_D_Composite = clsPatient_ToothCheck.CLASS_4_D_COMPOSITE, .Class_4_M_Amalgam = clsPatient_ToothCheck.CLASS_4_M_AMALGAM, .Class_4_M_Composite = clsPatient_ToothCheck.CLASS_4_M_COMPOSITE, .Class_4_Incisal = clsPatient_ToothCheck.CLASS_4_INCISAL, .Class_5_Amalgam = clsPatient_ToothCheck.CLASS_5_AMALGAM, .Class_5_Composite = clsPatient_ToothCheck.CLASS_5_COMPOSITE, .Crown_Lengthening = clsPatient_ToothCheck.CROWN_LENGTHENING, .Direct_Pulpcapping = clsPatient_ToothCheck.DIRECT_PULPCAPPING, ._EXTRACTION = clsPatient_ToothCheck.EXTRACTION, .Facing_Direct_Veneers = clsPatient_ToothCheck.FACING_DIRECT_VENEERS, .FIBER_POST = clsPatient_ToothCheck.FIBER_POST, .Fissure_Sealent = clsPatient_ToothCheck.FISSURE_SEALENT, .GI_Restoration = clsPatient_ToothCheck.GI_RESTORATION, .GI_M = clsPatient_ToothCheck.GI_M, .GI = clsPatient_ToothCheck.GI, .GI_D = clsPatient_ToothCheck.GI_D, .Hemisection = clsPatient_ToothCheck.HEMISECTION, .Indirect_Pulpcapping = clsPatient_ToothCheck.INDIRECT_PULPCAPPING, .Indirect_Veneers = clsPatient_ToothCheck.INDIRECT_VENEERS, .Metal_Post = clsPatient_ToothCheck.METAL_POST, .MTA_Bulk_Flow = clsPatient_ToothCheck.MTA_BULK_FLOW, .PULPOTOMY = clsPatient_ToothCheck.PULPOTOMY, .PULPOTOMY_MTA = clsPatient_ToothCheck.PULPOTOMY_MTA, .Partial_Pulpotomy = clsPatient_ToothCheck.PARTIAL_PULPOTOMY, .Root_Canal_Medicament = clsPatient_ToothCheck.ROOT_CANAL_MEDICAMENT, .RCC = clsPatient_ToothCheck.RCC, .RCF_Amalgam = clsPatient_ToothCheck.RCF_AMALGAM, .RCF_Composite = clsPatient_ToothCheck.RCF_COMPOSITE, .RCO = clsPatient_ToothCheck.RCO, .RCT = clsPatient_ToothCheck.RCT, .REDO_Amalgam = clsPatient_ToothCheck.REDO_AMALGAM, .REDO_Composite = clsPatient_ToothCheck.REDO_COMPOSITE, .REDO_RCT = clsPatient_ToothCheck.REDO_RCT, .RCT_NECROTIC = clsPatient_ToothCheck.RCT_NECROTIC, .Stainless_Steel_Crown = clsPatient_ToothCheck.STAINLESS_STEEL_CROWN, .Temporary_Acryl = clsPatient_ToothCheck.TEMPORARY_ACRYL, .TF = clsPatient_ToothCheck.TF, .CheckDate = clsPatient_ToothCheck.CheckDate, .CheckNotes = clsPatient_ToothCheck.CheckNotes})
'            Return RowsAffected > 0
'        End Using
'    End Function

'    Public Function Update(oldPatient_ToothCheck As Patient_ToothCheck, newPatient_ToothCheck As Patient_ToothCheck) As Boolean
'        Using conn As New SqlConnection(ConnectionString)
'            Dim parameters = New With {
'                .NewToothNum = newPatient_ToothCheck.ToothNum, .OldToothNum = oldPatient_ToothCheck.ToothNum, .NewPatientID = newPatient_ToothCheck.PatientID, .OldPatientID = oldPatient_ToothCheck.PatientID, .NewToothName = newPatient_ToothCheck.ToothName, .OldToothName = oldPatient_ToothCheck.ToothName, .NewABCESS_DRAINAGE = newPatient_ToothCheck.ABCESS_DRAINAGE, .OldABCESS_DRAINAGE = oldPatient_ToothCheck.ABCESS_DRAINAGE, .NewIsExist = newPatient_ToothCheck.IsExist, .OldIsExist = oldPatient_ToothCheck.IsExist, .NewBuild_Up_Com = newPatient_ToothCheck.BUILD_UP_COM, .OldBuild_Up_Com = oldPatient_ToothCheck.BUILD_UP_COM, .NewBuild_Up_Acr = newPatient_ToothCheck.BUILD_UP_ACR, .OldBuild_Up_Acr = oldPatient_ToothCheck.BUILD_UP_ACR, .NewBuild_Up_GI = newPatient_ToothCheck.BUILD_UP_GI, .OldBuild_Up_GI = oldPatient_ToothCheck.BUILD_UP_GI, .NewClass_1_Amalgam = newPatient_ToothCheck.CLASS_1_AMALGAM, .OldClass_1_Amalgam = oldPatient_ToothCheck.CLASS_1_AMALGAM, .NewClass_1_Composite = newPatient_ToothCheck.CLASS_1_COMPOSITE, .OldClass_1_Composite = oldPatient_ToothCheck.CLASS_1_COMPOSITE, .NewClass_2_D_Amalgam = newPatient_ToothCheck.CLASS_2_D_AMALGAM, .OldClass_2_D_Amalgam = oldPatient_ToothCheck.CLASS_2_D_AMALGAM, .NewClass_2_D_Composite = newPatient_ToothCheck.CLASS_2_D_COMPOSITE, .OldClass_2_D_Composite = oldPatient_ToothCheck.CLASS_2_D_COMPOSITE, .NewClass_2_M_Amalgam = newPatient_ToothCheck.CLASS_2_M_AMALGAM, .OldClass_2_M_Amalgam = oldPatient_ToothCheck.CLASS_2_M_AMALGAM, .NewClass_2_M_Composite = newPatient_ToothCheck.CLASS_2_M_COMPOSITE, .OldClass_2_M_Composite = oldPatient_ToothCheck.CLASS_2_M_COMPOSITE, .NewClass_2_MOD_Amalgam = newPatient_ToothCheck.CLASS_2_MOD_AMALGAM, .OldClass_2_MOD_Amalgam = oldPatient_ToothCheck.CLASS_2_MOD_AMALGAM, .NewClass_2_MOD_Composite = newPatient_ToothCheck.CLASS_2_MOD_COMPOSITE, .OldClass_2_MOD_Composite = oldPatient_ToothCheck.CLASS_2_MOD_COMPOSITE, .NewClass_3_D_Amalgam = newPatient_ToothCheck.CLASS_3_D_AMALGAM, .OldClass_3_D_Amalgam = oldPatient_ToothCheck.CLASS_3_D_AMALGAM, .NewClass_3_D_Composite = newPatient_ToothCheck.CLASS_3_D_COMPOSITE, .OldClass_3_D_Composite = oldPatient_ToothCheck.CLASS_3_D_COMPOSITE, .NewClass_3_M_Amalgam = newPatient_ToothCheck.CLASS_3_M_AMALGAM, .OldClass_3_M_Amalgam = oldPatient_ToothCheck.CLASS_3_M_AMALGAM, .NewClass_3_M_Composite = newPatient_ToothCheck.CLASS_3_M_COMPOSITE, .OldClass_3_M_Composite = oldPatient_ToothCheck.CLASS_3_M_COMPOSITE, .NewClass_4_D_Amalgam = newPatient_ToothCheck.CLASS_4_D_AMALGAM, .OldClass_4_D_Amalgam = oldPatient_ToothCheck.CLASS_4_D_AMALGAM, .NewClass_4_D_Composite = newPatient_ToothCheck.CLASS_4_D_COMPOSITE, .OldClass_4_D_Composite = oldPatient_ToothCheck.CLASS_4_D_COMPOSITE, .NewClass_4_M_Amalgam = newPatient_ToothCheck.CLASS_4_M_AMALGAM, .OldClass_4_M_Amalgam = oldPatient_ToothCheck.CLASS_4_M_AMALGAM, .NewClass_4_M_Composite = newPatient_ToothCheck.CLASS_4_M_COMPOSITE, .OldClass_4_M_Composite = oldPatient_ToothCheck.CLASS_4_M_COMPOSITE, .NewClass_4_Incisal = newPatient_ToothCheck.CLASS_4_INCISAL, .OldClass_4_Incisal = oldPatient_ToothCheck.CLASS_4_INCISAL, .NewClass_5_Amalgam = newPatient_ToothCheck.CLASS_5_AMALGAM, .OldClass_5_Amalgam = oldPatient_ToothCheck.CLASS_5_AMALGAM, .NewClass_5_Composite = newPatient_ToothCheck.CLASS_5_COMPOSITE, .OldClass_5_Composite = oldPatient_ToothCheck.CLASS_5_COMPOSITE, .NewCrown_Lengthening = newPatient_ToothCheck.CROWN_LENGTHENING, .OldCrown_Lengthening = oldPatient_ToothCheck.CROWN_LENGTHENING, .NewDirect_Pulpcapping = newPatient_ToothCheck.DIRECT_PULPCAPPING, .OldDirect_Pulpcapping = oldPatient_ToothCheck.DIRECT_PULPCAPPING, .NewEXTRACTION = newPatient_ToothCheck.EXTRACTION, .OldEXTRACTION = oldPatient_ToothCheck.EXTRACTION, .NewFacing_Direct_Veneers = newPatient_ToothCheck.FACING_DIRECT_VENEERS, .OldFacing_Direct_Veneers = oldPatient_ToothCheck.FACING_DIRECT_VENEERS, .NewFIBER_POST = newPatient_ToothCheck.FIBER_POST, .OldFIBER_POST = oldPatient_ToothCheck.FIBER_POST, .NewFissure_Sealent = newPatient_ToothCheck.FISSURE_SEALENT, .OldFissure_Sealent = oldPatient_ToothCheck.FISSURE_SEALENT, .NewGI_Restoration = newPatient_ToothCheck.GI_RESTORATION, .OldGI_Restoration = oldPatient_ToothCheck.GI_RESTORATION, .NewGI_M = newPatient_ToothCheck.GI_M, .OldGI_M = oldPatient_ToothCheck.GI_M, .NewGI = newPatient_ToothCheck.GI, .OldGI = oldPatient_ToothCheck.GI, .NewGI_D = newPatient_ToothCheck.GI_D, .OldGI_D = oldPatient_ToothCheck.GI_D, .NewHemisection = newPatient_ToothCheck.HEMISECTION, .OldHemisection = oldPatient_ToothCheck.HEMISECTION, .NewIndirect_Pulpcapping = newPatient_ToothCheck.INDIRECT_PULPCAPPING, .OldIndirect_Pulpcapping = oldPatient_ToothCheck.INDIRECT_PULPCAPPING, .NewIndirect_Veneers = newPatient_ToothCheck.INDIRECT_VENEERS, .OldIndirect_Veneers = oldPatient_ToothCheck.INDIRECT_VENEERS, .NewMetal_Post = newPatient_ToothCheck.METAL_POST, .OldMetal_Post = oldPatient_ToothCheck.METAL_POST, .NewMTA_Bulk_Flow = newPatient_ToothCheck.MTA_BULK_FLOW, .OldMTA_Bulk_Flow = oldPatient_ToothCheck.MTA_BULK_FLOW, .NewPULPOTOMY = newPatient_ToothCheck.PULPOTOMY, .OldPULPOTOMY = oldPatient_ToothCheck.PULPOTOMY, .NewPULPOTOMY_MTA = newPatient_ToothCheck.PULPOTOMY_MTA, .OldPULPOTOMY_MTA = oldPatient_ToothCheck.PULPOTOMY_MTA, .NewPartial_Pulpotomy = newPatient_ToothCheck.PARTIAL_PULPOTOMY, .OldPartial_Pulpotomy = oldPatient_ToothCheck.PARTIAL_PULPOTOMY, .NewRoot_Canal_Medicament = newPatient_ToothCheck.ROOT_CANAL_MEDICAMENT, .OldRoot_Canal_Medicament = oldPatient_ToothCheck.ROOT_CANAL_MEDICAMENT, .NewRCC = newPatient_ToothCheck.RCC, .OldRCC = oldPatient_ToothCheck.RCC, .NewRCF_Amalgam = newPatient_ToothCheck.RCF_AMALGAM, .OldRCF_Amalgam = oldPatient_ToothCheck.RCF_AMALGAM, .NewRCF_Composite = newPatient_ToothCheck.RCF_COMPOSITE, .OldRCF_Composite = oldPatient_ToothCheck.RCF_COMPOSITE, .NewRCO = newPatient_ToothCheck.RCO, .OldRCO = oldPatient_ToothCheck.RCO, .NewRCT = newPatient_ToothCheck.RCT, .OldRCT = oldPatient_ToothCheck.RCT, .NewREDO_Amalgam = newPatient_ToothCheck.REDO_AMALGAM, .OldREDO_Amalgam = oldPatient_ToothCheck.REDO_AMALGAM, .NewREDO_Composite = newPatient_ToothCheck.REDO_COMPOSITE, .OldREDO_Composite = oldPatient_ToothCheck.REDO_COMPOSITE, .NewREDO_RCT = newPatient_ToothCheck.REDO_RCT, .OldREDO_RCT = oldPatient_ToothCheck.REDO_RCT, .NewRCT_NECROTIC = newPatient_ToothCheck.RCT_NECROTIC, .OldRCT_NECROTIC = oldPatient_ToothCheck.RCT_NECROTIC, .NewStainless_Steel_Crown = newPatient_ToothCheck.STAINLESS_STEEL_CROWN, .OldStainless_Steel_Crown = oldPatient_ToothCheck.STAINLESS_STEEL_CROWN, .NewTemporary_Acryl = newPatient_ToothCheck.TEMPORARY_ACRYL, .OldTemporary_Acryl = oldPatient_ToothCheck.TEMPORARY_ACRYL, .NewTF = newPatient_ToothCheck.TF, .OldTF = oldPatient_ToothCheck.TF, .NewCheckDate = newPatient_ToothCheck.CheckDate, .OldCheckDate = oldPatient_ToothCheck.CheckDate, .NewCheckNotes = newPatient_ToothCheck.CheckNotes, .OldCheckNotes = oldPatient_ToothCheck.CheckNotes
'                                      }
'            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_ToothCheck] SET [ToothNum] = @NewToothNum, [PatientID] = @NewPatientID, [ToothName] = @NewToothName, [ABCESS_DRAINAGE] = @NewABCESS_DRAINAGE, [IsExist] = @NewIsExist, [Build_Up_Com] = @NewBuild_Up_Com, [Build_Up_Acr] = @NewBuild_Up_Acr, [Build_Up_GI] = @NewBuild_Up_GI, [Class_1_Amalgam] = @NewClass_1_Amalgam, [Class_1_Composite] = @NewClass_1_Composite, [Class_2_D_Amalgam] = @NewClass_2_D_Amalgam, [Class_2_D_Composite] = @NewClass_2_D_Composite, [Class_2_M_Amalgam] = @NewClass_2_M_Amalgam, [Class_2_M_Composite] = @NewClass_2_M_Composite, [Class_2_MOD_Amalgam] = @NewClass_2_MOD_Amalgam, [Class_2_MOD_Composite] = @NewClass_2_MOD_Composite, [Class_3_D_Amalgam] = @NewClass_3_D_Amalgam, [Class_3_D_Composite] = @NewClass_3_D_Composite, [Class_3_M_Amalgam] = @NewClass_3_M_Amalgam, [Class_3_M_Composite] = @NewClass_3_M_Composite, [Class_4_D_Amalgam] = @NewClass_4_D_Amalgam, [Class_4_D_Composite] = @NewClass_4_D_Composite, [Class_4_M_Amalgam] = @NewClass_4_M_Amalgam, [Class_4_M_Composite] = @NewClass_4_M_Composite, [Class_4_Incisal] = @NewClass_4_Incisal, [Class_5_Amalgam] = @NewClass_5_Amalgam, [Class_5_Composite] = @NewClass_5_Composite, [Crown_Lengthening] = @NewCrown_Lengthening, [Direct_Pulpcapping] = @NewDirect_Pulpcapping, [EXTRACTION] = @NewEXTRACTION, [Facing_Direct_Veneers] = @NewFacing_Direct_Veneers, [FIBER_POST] = @NewFIBER_POST, [Fissure_Sealent] = @NewFissure_Sealent, [GI_Restoration] = @NewGI_Restoration, [GI_M] = @NewGI_M, [GI] = @NewGI, [GI_D] = @NewGI_D, [Hemisection] = @NewHemisection, [Indirect_Pulpcapping] = @NewIndirect_Pulpcapping, [Indirect_Veneers] = @NewIndirect_Veneers, [Metal_Post] = @NewMetal_Post, [MTA_Bulk_Flow] = @NewMTA_Bulk_Flow, [PULPOTOMY] = @NewPULPOTOMY, [PULPOTOMY_MTA] = @NewPULPOTOMY_MTA, [Partial_Pulpotomy] = @NewPartial_Pulpotomy, [Root_Canal_Medicament] = @NewRoot_Canal_Medicament, [RCC] = @NewRCC, [RCF_Amalgam] = @NewRCF_Amalgam, [RCF_Composite] = @NewRCF_Composite, [RCO] = @NewRCO, [RCT] = @NewRCT, [REDO_Amalgam] = @NewREDO_Amalgam, [REDO_Composite] = @NewREDO_Composite, [REDO_RCT] = @NewREDO_RCT, [RCT_NECROTIC] = @NewRCT_NECROTIC, [Stainless_Steel_Crown] = @NewStainless_Steel_Crown, [Temporary_Acryl] = @NewTemporary_Acryl, [TF] = @NewTF, [CheckDate] = @NewCheckDate, [CheckNotes] = @NewCheckNotes WHERE [ToothNum] = @OldToothNum AND [PatientID] = @OldPatientID AND [ToothName] = @OldToothName AND [ABCESS_DRAINAGE] = @OldABCESS_DRAINAGE AND [IsExist] = @OldIsExist AND [Build_Up_Com] = @OldBuild_Up_Com AND [Build_Up_Acr] = @OldBuild_Up_Acr AND [Build_Up_GI] = @OldBuild_Up_GI AND [Class_1_Amalgam] = @OldClass_1_Amalgam AND [Class_1_Composite] = @OldClass_1_Composite AND [Class_2_D_Amalgam] = @OldClass_2_D_Amalgam AND [Class_2_D_Composite] = @OldClass_2_D_Composite AND [Class_2_M_Amalgam] = @OldClass_2_M_Amalgam AND [Class_2_M_Composite] = @OldClass_2_M_Composite AND [Class_2_MOD_Amalgam] = @OldClass_2_MOD_Amalgam AND [Class_2_MOD_Composite] = @OldClass_2_MOD_Composite AND [Class_3_D_Amalgam] = @OldClass_3_D_Amalgam AND [Class_3_D_Composite] = @OldClass_3_D_Composite AND [Class_3_M_Amalgam] = @OldClass_3_M_Amalgam AND [Class_3_M_Composite] = @OldClass_3_M_Composite AND [Class_4_D_Amalgam] = @OldClass_4_D_Amalgam AND [Class_4_D_Composite] = @OldClass_4_D_Composite AND [Class_4_M_Amalgam] = @OldClass_4_M_Amalgam AND [Class_4_M_Composite] = @OldClass_4_M_Composite AND [Class_4_Incisal] = @OldClass_4_Incisal AND [Class_5_Amalgam] = @OldClass_5_Amalgam AND [Class_5_Composite] = @OldClass_5_Composite AND [Crown_Lengthening] = @OldCrown_Lengthening AND [Direct_Pulpcapping] = @OldDirect_Pulpcapping AND [EXTRACTION] = @OldEXTRACTION AND [Facing_Direct_Veneers] = @OldFacing_Direct_Veneers AND [FIBER_POST] = @OldFIBER_POST AND [Fissure_Sealent] = @OldFissure_Sealent AND [GI_Restoration] = @OldGI_Restoration AND [GI_M] = @OldGI_M AND [GI] = @OldGI AND [GI_D] = @OldGI_D AND [Hemisection] = @OldHemisection AND [Indirect_Pulpcapping] = @OldIndirect_Pulpcapping AND [Indirect_Veneers] = @OldIndirect_Veneers AND [Metal_Post] = @OldMetal_Post AND [MTA_Bulk_Flow] = @OldMTA_Bulk_Flow AND [PULPOTOMY] = @OldPULPOTOMY AND [PULPOTOMY_MTA] = @OldPULPOTOMY_MTA AND [Partial_Pulpotomy] = @OldPartial_Pulpotomy AND [Root_Canal_Medicament] = @OldRoot_Canal_Medicament AND [RCC] = @OldRCC AND [RCF_Amalgam] = @OldRCF_Amalgam AND [RCF_Composite] = @OldRCF_Composite AND [RCO] = @OldRCO AND [RCT] = @OldRCT AND [REDO_Amalgam] = @OldREDO_Amalgam AND [REDO_Composite] = @OldREDO_Composite AND [REDO_RCT] = @OldREDO_RCT AND [RCT_NECROTIC] = @OldRCT_NECROTIC AND [Stainless_Steel_Crown] = @OldStainless_Steel_Crown AND [Temporary_Acryl] = @OldTemporary_Acryl AND [TF] = @OldTF AND [CheckDate] = @OldCheckDate AND [CheckNotes] = @OldCheckNotes", parameters)
'            Return affectedRows > 0
'        End Using
'    End Function

'    Public Function Update1(oldPatient_ToothCheck As Patient_ToothCheck, newPatient_ToothCheck As Patient_ToothCheck) As Boolean
'        Using conn As New SqlConnection(ConnectionString)
'            ' Define the SQL query for updating records
'            Dim query As String = "
'            UPDATE [Patient_ToothCheck]
'            SET 
'                [ToothNum] = @NewToothNum,
'                [PatientID] = @NewPatientID,
'                [ToothName] = @NewToothName,
'                [ABCESS_DRAINAGE] = @NewABCESS_DRAINAGE,
'                [IsExist] = @NewIsExist,
'                [BUILD_UP_COM] = @NewBUILD_UP_COM,
'                [BUILD_UP_ACR] = @NewBUILD_UP_ACR,
'                [BUILD_UP_GI] = @NewBUILD_UP_GI,
'                [CLASS_1_AMALGAM] = @NewCLASS_1_AMALGAM,
'                [CLASS_1_COMPOSITE] = @NewCLASS_1_COMPOSITE,
'                [CLASS_2_D_AMALGAM] = @NewCLASS_2_D_AMALGAM,
'                [CLASS_2_D_COMPOSITE] = @NewCLASS_2_D_COMPOSITE,
'                [CLASS_2_M_AMALGAM] = @NewCLASS_2_M_AMALGAM,
'                [CLASS_2_M_COMPOSITE] = @NewCLASS_2_M_COMPOSITE,
'                [CLASS_2_MOD_AMALGAM] = @NewCLASS_2_MOD_AMALGAM,
'                [CLASS_2_MOD_COMPOSITE] = @NewCLASS_2_MOD_COMPOSITE,
'                [CLASS_3_D_AMALGAM] = @NewCLASS_3_D_AMALGAM,
'                [CLASS_3_D_COMPOSITE] = @NewCLASS_3_D_COMPOSITE,
'                [CLASS_3_M_AMALGAM] = @NewCLASS_3_M_AMALGAM,
'                [CLASS_3_M_COMPOSITE] = @NewCLASS_3_M_COMPOSITE,
'                [CLASS_4_D_AMALGAM] = @NewCLASS_4_D_AMALGAM,
'                [CLASS_4_D_COMPOSITE] = @NewCLASS_4_D_COMPOSITE,
'                [CLASS_4_M_AMALGAM] = @NewCLASS_4_M_AMALGAM,
'                [CLASS_4_M_COMPOSITE] = @NewCLASS_4_M_COMPOSITE,
'                [CLASS_4_INCISAL] = @NewCLASS_4_INCISAL,
'                [CLASS_5_AMALGAM] = @NewCLASS_5_AMALGAM,
'                [CLASS_5_COMPOSITE] = @NewCLASS_5_COMPOSITE,
'                [CROWN_LENGTHENING] = @NewCROWN_LENGTHENING,
'                [DIRECT_PULPCAPPING] = @NewDIRECT_PULPCAPPING,
'                [EXTRACTION] = @NewEXTRACTION,
'                [FACING_DIRECT_VENEERS] = @NewFACING_DIRECT_VENEERS,
'                [FIBER_POST] = @NewFIBER_POST,
'                [FISSURE_SEALENT] = @NewFISSURE_SEALENT,
'                [GI_RESTORATION] = @NewGI_RESTORATION,
'                [GI_M] = @NewGI_M,
'                [GI] = @NewGI,
'                [GI_D] = @NewGI_D,
'                [HEMISECTION] = @NewHEMISECTION,
'                [INDIRECT_PULPCAPPING] = @NewINDIRECT_PULPCAPPING,
'                [INDIRECT_VENEERS] = @NewINDIRECT_VENEERS,
'                [METAL_POST] = @NewMETAL_POST,
'                [MTA_BULK_FLOW] = @NewMTA_BULK_FLOW,
'                [PULPOTOMY] = @NewPULPOTOMY,
'                [PULPOTOMY_MTA] = @NewPULPOTOMY_MTA,
'                [PARTIAL_PULPOTOMY] = @NewPARTIAL_PULPOTOMY,
'                [ROOT_CANAL_MEDICAMENT] = @NewROOT_CANAL_MEDICAMENT,
'                [RCC] = @NewRCC,
'                [RCF_AMALGAM] = @NewRCF_AMALGAM,
'                [RCF_COMPOSITE] = @NewRCF_COMPOSITE,
'                [RCO] = @NewRCO,
'                [RCT] = @NewRCT,
'                [REDO_AMALGAM] = @NewREDO_AMALGAM,
'                [REDO_COMPOSITE] = @NewREDO_COMPOSITE,
'                [REDO_RCT] = @NewREDO_RCT,
'                [RCT_NECROTIC] = @NewRCT_NECROTIC,
'                [STAINLESS_STEEL_CROWN] = @NewSTAINLESS_STEEL_CROWN,
'                [TEMPORARY_ACRYL] = @NewTEMPORARY_ACRYL,
'                [TF] = @NewTF,
'                [CheckDate] = @NewCheckDate,
'                [CheckNotes] = @NewCheckNotes
'            WHERE 
'                [ToothNum] = @OldToothNum
'                AND [PatientID] = @OldPatientID
'                AND [ToothName] = @OldToothName
'                AND [ABCESS_DRAINAGE] = @OldABCESS_DRAINAGE
'                AND [IsExist] = @OldIsExist;
'        "

'            ' Create the parameters object with all fields
'            Dim parameters = New With {
'    .NewToothNum = newPatient_ToothCheck.ToothNum, .OldToothNum = oldPatient_ToothCheck.ToothNum,
'    .NewPatientID = newPatient_ToothCheck.PatientID, .OldPatientID = oldPatient_ToothCheck.PatientID,
'    .NewToothName = newPatient_ToothCheck.ToothName, .OldToothName = oldPatient_ToothCheck.ToothName,
'    .NewABCESS_DRAINAGE = newPatient_ToothCheck.ABCESS_DRAINAGE, .OldABCESS_DRAINAGE = oldPatient_ToothCheck.ABCESS_DRAINAGE,
'    .NewIsExist = newPatient_ToothCheck.IsExist, .OldIsExist = oldPatient_ToothCheck.IsExist,
'    .NewBUILD_UP_COM = newPatient_ToothCheck.BUILD_UP_COM, .OldBUILD_UP_COM = oldPatient_ToothCheck.BUILD_UP_COM,
'    .NewBUILD_UP_ACR = newPatient_ToothCheck.BUILD_UP_ACR, .OldBUILD_UP_ACR = oldPatient_ToothCheck.BUILD_UP_ACR,
'    .NewBUILD_UP_GI = newPatient_ToothCheck.BUILD_UP_GI, .OldBUILD_UP_GI = oldPatient_ToothCheck.BUILD_UP_GI,
'    .NewCLASS_1_AMALGAM = newPatient_ToothCheck.CLASS_1_AMALGAM, .OldCLASS_1_AMALGAM = oldPatient_ToothCheck.CLASS_1_AMALGAM,
'    .NewCLASS_1_COMPOSITE = newPatient_ToothCheck.CLASS_1_COMPOSITE, .OldCLASS_1_COMPOSITE = oldPatient_ToothCheck.CLASS_1_COMPOSITE,
'    .NewCLASS_2_D_AMALGAM = newPatient_ToothCheck.CLASS_2_D_AMALGAM, .OldCLASS_2_D_AMALGAM = oldPatient_ToothCheck.CLASS_2_D_AMALGAM,
'    .NewCLASS_2_D_COMPOSITE = newPatient_ToothCheck.CLASS_2_D_COMPOSITE, .OldCLASS_2_D_COMPOSITE = oldPatient_ToothCheck.CLASS_2_D_COMPOSITE,
'    .NewCLASS_2_M_AMALGAM = newPatient_ToothCheck.CLASS_2_M_AMALGAM, .OldCLASS_2_M_AMALGAM = oldPatient_ToothCheck.CLASS_2_M_AMALGAM,
'    .NewCLASS_2_M_COMPOSITE = newPatient_ToothCheck.CLASS_2_M_COMPOSITE, .OldCLASS_2_M_COMPOSITE = oldPatient_ToothCheck.CLASS_2_M_COMPOSITE,
'    .NewCLASS_2_MOD_AMALGAM = newPatient_ToothCheck.CLASS_2_MOD_AMALGAM, .OldCLASS_2_MOD_AMALGAM = oldPatient_ToothCheck.CLASS_2_MOD_AMALGAM,
'    .NewCLASS_2_MOD_COMPOSITE = newPatient_ToothCheck.CLASS_2_MOD_COMPOSITE, .OldCLASS_2_MOD_COMPOSITE = oldPatient_ToothCheck.CLASS_2_MOD_COMPOSITE,
'    .NewCLASS_3_D_AMALGAM = newPatient_ToothCheck.CLASS_3_D_AMALGAM, .OldCLASS_3_D_AMALGAM = oldPatient_ToothCheck.CLASS_3_D_AMALGAM,
'    .NewCLASS_3_D_COMPOSITE = newPatient_ToothCheck.CLASS_3_D_COMPOSITE, .OldCLASS_3_D_COMPOSITE = oldPatient_ToothCheck.CLASS_3_D_COMPOSITE,
'    .NewCLASS_3_M_AMALGAM = newPatient_ToothCheck.CLASS_3_M_AMALGAM, .OldCLASS_3_M_AMALGAM = oldPatient_ToothCheck.CLASS_3_M_AMALGAM,
'    .NewCLASS_3_M_COMPOSITE = newPatient_ToothCheck.CLASS_3_M_COMPOSITE, .OldCLASS_3_M_COMPOSITE = oldPatient_ToothCheck.CLASS_3_M_COMPOSITE,
'    .NewCLASS_4_D_AMALGAM = newPatient_ToothCheck.CLASS_4_D_AMALGAM, .OldCLASS_4_D_AMALGAM = oldPatient_ToothCheck.CLASS_4_D_AMALGAM,
'    .NewCLASS_4_D_COMPOSITE = newPatient_ToothCheck.CLASS_4_D_COMPOSITE, .OldCLASS_4_D_COMPOSITE = oldPatient_ToothCheck.CLASS_4_D_COMPOSITE,
'    .NewCLASS_4_M_AMALGAM = newPatient_ToothCheck.CLASS_4_M_AMALGAM, .OldCLASS_4_M_AMALGAM = oldPatient_ToothCheck.CLASS_4_M_AMALGAM,
'    .NewCLASS_4_M_COMPOSITE = newPatient_ToothCheck.CLASS_4_M_COMPOSITE, .OldCLASS_4_M_COMPOSITE = oldPatient_ToothCheck.CLASS_4_M_COMPOSITE,
'    .NewCLASS_4_INCISAL = newPatient_ToothCheck.CLASS_4_INCISAL, .OldCLASS_4_INCISAL = oldPatient_ToothCheck.CLASS_4_INCISAL,
'    .NewCLASS_5_AMALGAM = newPatient_ToothCheck.CLASS_5_AMALGAM, .OldCLASS_5_AMALGAM = oldPatient_ToothCheck.CLASS_5_AMALGAM,
'    .NewCLASS_5_COMPOSITE = newPatient_ToothCheck.CLASS_5_COMPOSITE, .OldCLASS_5_COMPOSITE = oldPatient_ToothCheck.CLASS_5_COMPOSITE,
'    .NewCROWN_LENGTHENING = newPatient_ToothCheck.CROWN_LENGTHENING, .OldCROWN_LENGTHENING = oldPatient_ToothCheck.CROWN_LENGTHENING,
'    .NewDIRECT_PULPCAPPING = newPatient_ToothCheck.DIRECT_PULPCAPPING, .OldDIRECT_PULPCAPPING = oldPatient_ToothCheck.DIRECT_PULPCAPPING,
'    .NewEXTRACTION = newPatient_ToothCheck.EXTRACTION, .OldEXTRACTION = oldPatient_ToothCheck.EXTRACTION,
'    .NewFACING_DIRECT_VENEERS = newPatient_ToothCheck.FACING_DIRECT_VENEERS, .OldFACING_DIRECT_VENEERS = oldPatient_ToothCheck.FACING_DIRECT_VENEERS,
'    .NewFIBER_POST = newPatient_ToothCheck.FIBER_POST, .OldFIBER_POST = oldPatient_ToothCheck.FIBER_POST,
'    .NewFISSURE_SEALENT = newPatient_ToothCheck.FISSURE_SEALENT, .OldFISSURE_SEALENT = oldPatient_ToothCheck.FISSURE_SEALENT,
'    .NewGI_RESTORATION = newPatient_ToothCheck.GI_RESTORATION, .OldGI_RESTORATION = oldPatient_ToothCheck.GI_RESTORATION,
'    .NewGI_M = newPatient_ToothCheck.GI_M, .OldGI_M = oldPatient_ToothCheck.GI_M,
'    .NewGI = newPatient_ToothCheck.GI, .OldGI = oldPatient_ToothCheck.GI,
'    .NewGI_D = newPatient_ToothCheck.GI_D, .OldGI_D = oldPatient_ToothCheck.GI_D,
'    .NewHEMISECTION = newPatient_ToothCheck.HEMISECTION, .OldHEMISECTION = oldPatient_ToothCheck.HEMISECTION,
'    .NewINDIRECT_PULPCAPPING = newPatient_ToothCheck.INDIRECT_PULPCAPPING, .OldINDIRECT_PULPCAPPING = oldPatient_ToothCheck.INDIRECT_PULPCAPPING,
'    .NewINDIRECT_VENEERS = newPatient_ToothCheck.INDIRECT_VENEERS, .OldINDIRECT_VENEERS = oldPatient_ToothCheck.INDIRECT_VENEERS,
'    .NewMETAL_POST = newPatient_ToothCheck.METAL_POST, .OldMETAL_POST = oldPatient_ToothCheck.METAL_POST,
'    .NewMTA_BULK_FLOW = newPatient_ToothCheck.MTA_BULK_FLOW, .OldMTA_BULK_FLOW = oldPatient_ToothCheck.MTA_BULK_FLOW,
'    .NewPULPOTOMY = newPatient_ToothCheck.PULPOTOMY, .OldPULPOTOMY = oldPatient_ToothCheck.PULPOTOMY,
'    .NewPULPOTOMY_MTA = newPatient_ToothCheck.PULPOTOMY_MTA, .OldPULPOTOMY_MTA = oldPatient_ToothCheck.PULPOTOMY_MTA,
'    .NewPARTIAL_PULPOTOMY = newPatient_ToothCheck.PARTIAL_PULPOTOMY, .OldPARTIAL_PULPOTOMY = oldPatient_ToothCheck.PARTIAL_PULPOTOMY,
'    .NewROOT_CANAL_MEDICAMENT = newPatient_ToothCheck.ROOT_CANAL_MEDICAMENT, .OldROOT_CANAL_MEDICAMENT = oldPatient_ToothCheck.ROOT_CANAL_MEDICAMENT,
'    .NewRCC = newPatient_ToothCheck.RCC, .OldRCC = oldPatient_ToothCheck.RCC,
'    .NewRCF_AMALGAM = newPatient_ToothCheck.RCF_AMALGAM, .OldRCF_AMALGAM = oldPatient_ToothCheck.RCF_AMALGAM,
'    .NewRCF_COMPOSITE = newPatient_ToothCheck.RCF_COMPOSITE, .OldRCF_COMPOSITE = oldPatient_ToothCheck.RCF_COMPOSITE,
'    .NewRCO = newPatient_ToothCheck.RCO, .OldRCO = oldPatient_ToothCheck.RCO,
'    .NewRCT = newPatient_ToothCheck.RCT, .OldRCT = oldPatient_ToothCheck.RCT,
'    .NewREDO_AMALGAM = newPatient_ToothCheck.REDO_AMALGAM, .OldREDO_AMALGAM = oldPatient_ToothCheck.REDO_AMALGAM,
'    .NewREDO_COMPOSITE = newPatient_ToothCheck.REDO_COMPOSITE, .OldREDO_COMPOSITE = oldPatient_ToothCheck.REDO_COMPOSITE,
'    .NewREDO_RCT = newPatient_ToothCheck.REDO_RCT, .OldREDO_RCT = oldPatient_ToothCheck.REDO_RCT,
'    .NewRCT_NECROTIC = newPatient_ToothCheck.RCT_NECROTIC, .OldRCT_NECROTIC = oldPatient_ToothCheck.RCT_NECROTIC,
'    .NewSTAINLESS_STEEL_CROWN = newPatient_ToothCheck.STAINLESS_STEEL_CROWN, .OldSTAINLESS_STEEL_CROWN = oldPatient_ToothCheck.STAINLESS_STEEL_CROWN,
'    .NewTEMPORARY_ACRYL = newPatient_ToothCheck.TEMPORARY_ACRYL, .OldTEMPORARY_ACRYL = oldPatient_ToothCheck.TEMPORARY_ACRYL,
'    .NewTF = newPatient_ToothCheck.TF, .OldTF = oldPatient_ToothCheck.TF,
'    .NewCheckDate = newPatient_ToothCheck.CheckDate, .OldCheckDate = oldPatient_ToothCheck.CheckDate,
'    .NewCheckNotes = newPatient_ToothCheck.CheckNotes, .OldCheckNotes = oldPatient_ToothCheck.CheckNotes
'}


'            Try
'                ' Execute the query and check for affected rows
'                Dim affectedRows As Integer = conn.Execute(query, parameters)
'                Return affectedRows > 0
'            Catch ex As Exception
'                ' Log or display the error message
'                MessageBox.Show("Error updating record: " & ex.Message)
'                Return False
'            End Try
'        End Using
'    End Function




'    Public Function Delete(ByVal clsPatient_ToothCheck As Patient_ToothCheck) As Boolean
'        Dim deleteStatement As String =
'        "DELETE FROM [Patient_ToothCheck] 
'			WHERE ToothNum = @ToothNum AND PatientID = @PatientID"
'        Using connection As SqlConnection = DentistXDATA.GetConnection()
'            connection.Open()
'            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.ToothNum = clsPatient_ToothCheck.ToothNum, .PatientID = clsPatient_ToothCheck.PatientID})
'            Return affectedRows > 0
'        End Using
'    End Function


'    'Methods to get parents and childs
'    Public Function GetPatient(ByVal PatientID As Integer) As Patient
'        Dim parent As Patient = Nothing
'        Using conn As New SqlConnection(ConnectionString)
'            Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
'            Try
'                conn.Open()
'                parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientID = PatientID})
'            Catch ex As Exception
'                ' Handle exceptions
'            Finally
'                If conn.State = ConnectionState.Open Then conn.Close()
'            End Try
'        End Using
'        Return parent
'    End Function

'End Class
