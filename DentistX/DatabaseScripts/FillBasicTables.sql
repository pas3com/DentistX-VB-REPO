--USE [DentistX]
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 
GO
INSERT [dbo].[Groups] ([GroupID], [GroupName]) VALUES (1, N'Admins')
GO
INSERT [dbo].[Groups] ([GroupID], [GroupName]) VALUES (2, N'Doctors')
GO
INSERT [dbo].[Groups] ([GroupID], [GroupName]) VALUES (3, N'Secretary')
GO
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO

SET IDENTITY_INSERT [dbo].[MedicineGroups] ON 
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (1, N'المضادات الحيوية')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (2, N'المسكنات')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (3, N'مضادات الفطريات')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (4, N'مضادات التشنج العضلي')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (5, N'مدرات اللعاب')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (6, N'الغسولات الفموية')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (7, N'ادوية التقرحات الفموية')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (8, N'ادوية امراض اللثة')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (9, N'ادوية التسنين عند الاطفال')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (10, N'الفيتامينات')
GO
INSERT [dbo].[MedicineGroups] ([MedicineID], [MedicineFamily]) VALUES (11, N'معاجين الاسنان')
GO
SET IDENTITY_INSERT [dbo].[MedicineGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicineFamily] ON 
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (1, 1, N'Penicillins')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (2, 1, N'Macrolide')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (3, 1, N'Cephalosporins')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (4, 1, N'Tetracyclines')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (5, 1, N'Clindamycin')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (6, 1, N'Drugs against anaerobes')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (7, 1, N'Lincomycin')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (8, 1, N'Quinolone')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (9, 2, N'Analgesics and Antipyretics')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (10, 2, N'Anti- Inflamatory analgesic')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (12, 6, N'Mundiodone')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (13, 6, N'Chlorhexidine Gluconate')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (14, 6, N'Cetylpyridinium Chloride')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (15, 3, N'Nystatin')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (17, 3, N'Miconazole')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (18, 3, N'Fluconazole')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (19, 6, N'العشبيه')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (20, 11, N'sensitive')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (21, 11, N'عشبيه')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (22, 7, N'Dequalinium Chloride')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (23, 7, N'--')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (24, 9, N'Benzocaine')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (25, 4, N'Muscle Relaxants')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (26, 11, N'لاصق اطقم')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (27, 6, N'chlorhexidine 0.2-Xylitol 1')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (28, 1, N'Ceftrixone')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (29, 11, N'white')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (30, 6, N'Sensitive Antibacterial')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (31, 10, N'Vitamin+Calcum')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (32, 10, N'VITAMIN')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (33, 7, N'natural ingredient')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (34, 5, N'pilocarpine')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (35, 8, N'Chlorohexidine- 2%')
GO
INSERT [dbo].[MedicineFamily] ([SubCatID], [MedicineID], [MedicineSubCat]) VALUES (36, 4, N'Tizanidine')
GO
SET IDENTITY_INSERT [dbo].[MedicineFamily] OFF
GO
SET IDENTITY_INSERT [dbo].[MedScienceFamily] ON 
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (1, 1, N'Amoxycillin')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (2, 1, N'Ampicilln+Flucloxacillin')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (3, 1, N'Amoxycillin+Clavulanic Acid')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (4, 2, N'Erythromycin')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (5, 6, N'Metronidazole')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (6, 6, N'Tinidazole')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (7, 3, N'Cephalexin')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (8, 3, N'Cefadroxil')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (9, 8, N'Ciprofloxacin HC1')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (10, 4, N'Doxycyclime HCL')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (11, 5, N'Clindamycin HCL')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (12, 9, N'Parace+Codeine phos')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (15, 9, N'Dextro HCL Mefe Acid')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (16, 10, N'Diclofenac Potassium')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (17, 10, N'Diclofenac Sodium')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (18, 10, N'Diflunisal')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (19, 10, N'Ibuprofen')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (20, 10, N'Etoricoxib')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (21, 10, N'Piroxicam')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (22, 10, N'Naproxan sodium')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (23, 10, N'Naproxen')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (24, 10, N'Meloxicam')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (26, 12, N'Iodocare')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (27, 13, N'Chlorhexidine Gluconate')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (28, 14, N'Cetylpyridinium Chloride')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (30, 15, N'Nystatin')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (31, 17, N'Miconazole')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (32, 18, N'Fluconazole')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (33, 19, N'اعشاب')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (34, 20, N'sensitive')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (35, 21, N'Herb')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (36, 22, N'Dequalinium Chloride')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (37, 23, N'--')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (38, 24, N'Benzocaine')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (39, 25, N'Orphenadrine Citrate par')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (40, 25, N'Dextropropoxyphene napsylate par')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (41, 10, N'Celecoxib')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (42, 3, N'cefuroxime')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (43, 8, N'Levofloxacin')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (44, 3, N'Cifixime')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (45, 2, N'Azithromycine')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (46, 2, N'Roxithromycine')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (47, 2, N'Clarithromycine')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (48, 9, N'tramadol')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (49, 9, N'tramadol +paractamol')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (50, 26, N'لاصق اطقم')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (51, 27, N'Dentarex Forte')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (52, 28, N'Ceftrixone')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (53, 9, N'paractamol Caffeine')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (54, 29, N'florodine white')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (55, 30, N'Fluorodine')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (56, 31, N'Vitamin+Calcum')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (57, 29, N'fluorodine sensitive')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (58, 32, N'-')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (59, 3, N'Cefpodoxime')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (60, 3, N'Cefidinir')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (61, 10, N'Lornoxicam')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (62, 33, N'aphta care plus')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (63, 34, N'pilocarpine')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (64, 35, N'PerioKin gel')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (65, 8, N'moxifloxacin')
GO
INSERT [dbo].[MedScienceFamily] ([ScincID], [SubCatID], [ScienceName]) VALUES (66, 36, N'Tizanidine')
GO
SET IDENTITY_INSERT [dbo].[MedScienceFamily] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicineItems] ON 
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (1, 1, N'Amoxitid', N'BPC', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (2, 1, N'Amoxicare', N'PLC', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (3, 1, N'Moxepharm', N'Jepharm', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (5, 2, N'Magnacillin', N'BPC', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (6, 2, N'Magnacillin forte', N'BPC', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (7, 2, N'Megacare', N'BLC', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (8, 2, N'Megacare forte', N'BLC', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (9, 3, N'Clamoxin', N'Jepharm', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (10, 3, N'Ogmin', N'BPC', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (11, 3, N'Curam', N'Novartis', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (12, 5, N'Entogyl', N'Jepharm', N'لا يعطى للحوامل والمرضعات ومرضى الكلى')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (14, 5, N'Metrazole', N'BPC', N'لا يعطى للحوامل والمرضعات ومرضى الكلى')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (15, 6, N'Tinogyn', N'Jepharm', N'لا يعطى للحوامل والمرضعات ومرضى الكلى')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (16, 7, N'Cefacare', N'PLC', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (17, 7, N'Cefalex', N'BPC', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (18, 8, N'Cefadrox', N'BPC', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين امن للحوامل')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (19, 9, N'Ceprox', N'BPC', N'لا يعطى لمرضى الكلى والكبد والمرضع والاطفال  يعطى لمن لديه حساسيه عالبنسلين دون 12')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (20, 9, N'Ciprocare', N'PLC', N'لا يعطى لمرضى الكلى والكبد والمرضع والاطفال   يعطى لمن لديه حساسيه عالبنسلين دون 12')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (22, 10, N'Doxal', N'BJP', N'لا يعطى للحوامل والمرضعات والاطفال')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (23, 10, N'Doxypharm', N'Jepharm', N'لا يعطى للحوامل والمرضعات والاطفال')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (24, 9, N'Floxin', N'Jepharm', N'لا يعطى لمرضى الكلى والكبد والمرضع والاطفال يعطى لمن لديه حساسيه عالبنسلين دون 12')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (25, 7, N'Jeflex', N'Jepharm', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (26, 11, N'--------', N'BPC', N'لا يعطى للاطفال وللذين يعانون من مشاكل في الامعاء والكبد')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (27, 11, N'Denacine', N'BJP', N'لا يعطى للاطفال وللذين يعانون من مشاكل في الامعاء والكبد')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (28, 8, N'Biodroxil', N'Novartis', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين امن للحوامل')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (29, 4, N'Erythrotab', N'BPC', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (30, 12, N'Algonal', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (31, 12, N'Codamol', N'BJP', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (32, 12, N'Paracod', N'BLC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (38, 15, N'Ponsan plus', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (39, 16, N'Anaflam', N'BJP', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (40, 16, N'Cataflam', N'Novartis', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (43, 17, N'Voryn', N'PLC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (44, 17, N'Voltaren', N'N0vartis', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (45, 18, N'Dolacare', N'PLC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (46, 19, N'Isofen', N'BJP', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (47, 19, N'Trufen', N'Jepharm', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (48, 19, N'Ultrafen', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (49, 19, N'Ultrafen LC', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (50, 20, N'Tericox', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (51, 21, N'Pirox', N'Jepharm', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (52, 21, N'Roxin', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (53, 22, N'Naproxan', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (54, 23, N'Naprex', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (55, 24, N'Movalis', N'PLC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (58, 26, N'Iodocare', N'PLC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (59, 27, N'Gargarol', N'BJP', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (60, 27, N'Gingival', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (61, 28, N'Septoral', N'Jepharm', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (62, 30, N'Candistan', N'BPC', N'الرضع 2ملم 4مرات-الاطفال والكبار 4-6ملم 4مرات نصف الجرعه في كل جهه فتره العلاج 24ساعه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (63, 31, N'Daktazole', N'Jepharm', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (64, 32, N'Flucan', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (65, 33, N'Silca', N'm', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (66, 34, N'SILCA sensitive', N'm', N'للاسنان الحساسه يحتوي املاح البوتاسيوم لاغلاق مجسات الالم')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (67, 34, N'Sensudayne', N'm', N'للاسنان الحساسه يحتوي املاح البوتاسيوم لاغلاق مجسات الالم')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (68, 35, N'Silca .', N'h', N'يقوي اللثه - وقايه ضد التسوس ويقي من النزيف')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (69, 36, N'Dequapal', N'Jepharm', N'للتقرحات الفمويه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (70, 37, N'Gigipaint', N'BJP', N'للتقرحات الفمويه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (72, 38, N'Baby gel', N'Jepharm', N'لالم التسنين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (73, 39, N'Blgesic', N'Jepharm', N'مضاد التشنج العضلي')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (74, 40, N'Dologesic', N'BPC', N'مضاد التشنج العضلي')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (75, 17, N'Diclofen+Decort', N'Jepharm', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (76, 17, N'Voltaren+Decort', N'Novartis', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (77, 41, N'Coxib', N'jepharm', N'بعد الجراحه ولا مانع من اعطائه لمرضى الضغط والازمه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (78, 42, N'Zinnat', N'Gsk', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (79, 39, N'Relaxon', N'BJP', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (80, 17, N'Diclofen', N'Jepharm', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (81, 17, N'Rufenal', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (82, 17, N'Rufenal+Decort', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (84, 41, N'Celex', N'BJP', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (85, 43, N'Levox', N'Jepharm', N'لا يعطى لمرضى الكلى والكبد والمرضع والاطفال دون 18')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (86, 43, N'Tavanic', N'Aventis', N'لا يعطى لمرضى الكلى والكبد والمرضع والاطفال دون 18')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (87, 42, N'Zinex', N'BPC', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (88, 42, N'Zinaxim', N'Jepharm', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (89, 44, N'Rizacef', N'PLC', N'لا يعطى لمرضى الكلى وللذين لديهم حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (90, 4, N'Erythrocare', N'PLC', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (91, 45, N'Azicare', N'PLC', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (92, 45, N'Zitrocin', N'Jepharm', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (93, 45, N'Azimex', N'BPC', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (94, 46, N'Myrox', N'BJP', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام لا يعطى خلال الرضاعه ولم لديه خلل حاد في الكبد وللحامل يعطى بعد الشهر الثالث ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (95, 47, N'Klaricare', N'PLC', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (96, 47, N'KLarimax', N'BJP', N'يعطى لمن لديه حساسيه من البنسلين . ويؤخد مع الطعام ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (97, 32, N'Dican', N'PLC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (98, 48, N'tramal', N'PLC', N' كل 20 نقطه تساوي100 ملغم  \  SEVER PAIN \ 50 tab فوار')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (99, 49, N'Zaldiar', N'PLC', N'mild to moderat to sever')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (100, 22, N'Point', N'Trima', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (102, 50, N'Steradent     Ultra 3', N'hh', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (103, 51, N'Dentarex Forte', N'Gilco اسرائلي', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (104, 16, N'Toleran', N'p', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (105, 3, N'Augmantin', N'TEVA', N'لا تستعمل اذا كان المريض لديه حساسيه من البنسلين')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (106, 52, N'Ceftrixone', N'TEVA', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (107, 54, N'florodine oxygen white', N'mult', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (108, 55, N'Fluorodine sensitive', N'i', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (109, 53, N'Paramol Plus', N'بيت جالا', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (110, 56, N'CALCIVD', N'القدس', N'علاج نقص الكالسيوم وفيتانين د وهشاشه العظام والعظام الرخوه ولتقويتها لاقصى حد. لا يؤخذ معه الحليب والقهوه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (111, 34, N'Fluorodine sensitive', N'h', N'للاسنان الحساسه يحتوي املاح البوتاسيوم لاغلاق مجسات الالم')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (112, 58, N'ORAL D', N'--', N'لتاخير التسنين يعطى جرعتين في الشهر كل اسبوعين جرعه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (113, 59, N'Cefpo', N'berzeit', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (114, 17, N'Swiss Relief', N'zizar', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (115, 60, N'Adcef', N's', N'لمعالجة الالتهابات الناجمه عن الجراثيم الحساسيه للسيفدنير')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (116, 19, N'Nurofen', N'Nurofen', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (117, 49, N'XTRAM PLUS', N'PLC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (118, 61, N'xefo', N'SIAM COM.', N'امن لمرضى الضغط المنتظمين
وامن لمرضى الكلى بنسبه معينه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (119, 62, N'APHTA CARE', N'SIAM COM.', N'for aphthas ulcer')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (120, 63, N'----', N'----', N'ينصح بأكل البوظة و شرب الماء بجرعات قليلة متكررة')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (121, 50, N'Kin 0ro', N'cream', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (122, 34, N'SensiKin  gel', N'-', N' فعاليه في علاج حساسيه الاسنان خصوصا بعد التبيض والتنظيف والحشوات التجميليه يستخدم  م2 --5   ايام فقط')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (124, 64, N'PerioKin gel', N'-', N'جل لعلاج التهاب اللثه الحاد *لعلاج ضمور اللثة *لإلتئام الأنسجة ما بعد العمليات الجراحية والزراعة*لعلاج تقرحات الفم واللثه ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (125, 34, N'Biorepair', N'-', N'يقوم بالصلاح طبقة المينا وترميمها * حمايه لعاج السن* يخفف حساسية الاسنان ')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (126, 61, N'OXIDUR', N'BPC', N'امن لمرضى الضغط المنتظمين
وامن لمرضى الكلى بنسبه معينه')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (127, 16, N'Joflam', N'j', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (128, 65, N'moxiflox', N'pharmacare plc', N'containdication in pregnancy, breastfeeding and children < 16 year')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (129, 3, N'Amoxiclav', N'TEVA', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (130, 3, N'Moclav', N'sandoz', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (131, 43, N'LEVOFOX', N'SAMA', N'لا يعطى لمرضى الكلى والكبد والمرضع والاطفال دون 18')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (132, 66, N'Sirdalud', N'Novartis', N'يعطى اول 3 ايام حبه قبل النوم ويزاد حسب الحاجه   ممنوع للحامل والرضع')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (133, 52, N'Cefixon', N'BPC', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (134, 43, N'Voloxal', N'BPC', N'لا بعطى لمرضى الكلى والكبد والمرضع والاطفال دون 18')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (135, 19, N'Advil forte', N'-', N'')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (136, 20, N'Etoricoxib Taro', N'taro', N'غير امن للحوامل ومرضى القلب والكلى')
GO
INSERT [dbo].[MedicineItems] ([MedicineItemID], [ScincID], [CommName], [Company], [Notes]) VALUES (137, 16, N'Anaflam Powder', N'BJP', N'')
GO
SET IDENTITY_INSERT [dbo].[MedicineItems] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicineShape] ON 
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (1, 1, N'250 susp', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (2, 1, N'250 cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (3, 1, N'500 cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (4, 1, N'750 mg cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (5, 2, N'1000 mg tab', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (6, 2, N'500 mg cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (7, 2, N'250 mg cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (8, 2, N'250 mg susp', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (9, 2, N'400 mg susp', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (12, 3, N'500 mg cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (13, 3, N'750 mg cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (14, 3, N'250 mg susp', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (15, 3, N'400 mg susp', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (18, 5, N'cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (19, 5, N'susp', N'60 ml')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (20, 6, N'cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (21, 6, N'susp', N'60 ml')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (22, 7, N'susp', N'60 ml')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (23, 8, N'susp', N'60 ml')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (24, 7, N'cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (25, 8, N'cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (26, 9, N'875 tab', N'14 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (27, 9, N'500 tab', N'20 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (29, 9, N'400 susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (30, 9, N'250 susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (31, 9, N'125 susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (32, 10, N'875 tab', N'14 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (33, 10, N'500 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (34, 10, N'400 susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (35, 10, N'250 susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (36, 10, N'125 susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (37, 11, N'625 mg tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (38, 11, N'312.5 susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (39, 12, N'500 tab', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (40, 12, N'250 tab', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (41, 19, N'500 mg', N'10')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (42, 20, N'750 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (43, 20, N'500 mg tab', N'15 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (44, 24, N'500 mg tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (46, 22, N'100 mg tab', N'10')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (47, 23, N'100  mg tab', N'10')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (48, 26, N'150 mg cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (49, 26, N'300 mg cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (50, 27, N'150 mg cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (51, 27, N'300 mg cap', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (52, 28, N'500 mg cap', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (53, 28, N'250 mg susp', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (54, 15, N'500 mg tab', N'4 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (55, 29, N'250 mg tab', N'24 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (56, 30, N'tab', N'30')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (57, 31, N'tab', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (58, 32, N'tab', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (64, 38, N'tab', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (65, 39, N'50 mg', N'10')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (66, 40, N'50 mg', N'10')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (69, 43, N'50 mg', N'30')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (70, 45, N'500 mg', N'8')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (71, 46, N'400 mg', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (72, 46, N'200 mg', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (73, 47, N'600 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (74, 47, N'400 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (75, 47, N'200 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (76, 48, N'600 mg', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (77, 48, N'400 mg', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (78, 48, N'200mg', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (79, 49, N'400 mg cap', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (80, 53, N'275 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (81, 54, N'500 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (82, 54, N'250 mg tab', N'20 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (83, 55, N'15 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (84, 55, N'7.5  mg tab', N'20 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (87, 16, N'500 mg', N'16 cap')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (88, 16, N'250 mg cap', N'16 cap')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (89, 17, N'250 mg cap', N'16 cap')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (90, 17, N'500 mg cap', N'16 cap')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (91, 18, N'500 mg tab', N'12 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (92, 46, N'susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (93, 47, N'susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (94, 48, N'susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (97, 58, N'mouth gargle', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (98, 59, N'mouth wash', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (99, 60, N'mouth wash', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (100, 61, N'mouth wash', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (101, 12, N'125 sup', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (102, 25, N'750 mg cap', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (103, 25, N'500 mg cap', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (104, 25, N'250 mg cap', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (105, 25, N'250 mg susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (106, 25, N'125 mg susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (107, 50, N'120 mg tab', N'7 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (108, 14, N'500 mg tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (109, 62, N'Drops', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (110, 63, N'Oral gel', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (111, 64, N'caps', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (112, 65, N'Herb mouth wash', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (113, 66, N'toothpaste', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (114, 67, N'toothpaste', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (115, 68, N'Herb tooth paste', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (117, 50, N'60 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (118, 69, N'paint', N'10 ml')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (121, 72, N'-', N'15 mg')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (122, 73, N'tap', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (123, 74, N'tap', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (124, 44, N'Amp 75 mg', N'Novartis')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (125, 44, N'100 mg tab', N'Novartis')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (126, 75, N'Amp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (127, 76, N'Amp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (128, 77, N'200 mg cap', N'10')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (129, 77, N'100 mg cap', N'20')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (130, 45, N'250', N'16')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (131, 93, N'500 mg cap', N'3 capللبالغين ')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (132, 93, N'250 mg cap', N'6 cap  للبالغين')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (133, 87, N'500 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (134, 87, N'250 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (136, 91, N'500 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (137, 91, N'250 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (138, 95, N'250 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (139, 95, N'500 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (140, 92, N'500 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (141, 92, N'250 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (142, 93, N'250 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (143, 93, N'500 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (144, 96, N'250 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (145, 96, N'500 TAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (146, 82, N'Amp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (147, 79, N'tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (148, 20, N'250 tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (149, 98, N'50 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (150, 98, N'50 cap', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (151, 98, N'100 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (152, 98, N'150 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (153, 99, N'tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (154, 100, N'275 tab', N'24')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (155, 102, N'لاصق اطقم', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (157, 103, N'mouth wash', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (158, 104, N'50 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (159, 105, N'500 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (160, 105, N'250 syr', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (161, 106, N'1 g   Amp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (162, 94, N'300 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (163, 94, N'150 mg tab', N'10 tab')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (164, 107, N'toothpaste', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (165, 108, N'mouth wash', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (166, 109, N'tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (167, 98, N'Drops', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (168, 110, N'600 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (169, 111, N'toothpaste', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (170, 112, N'=', N'كل اسبوعين جرعه')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (171, 80, N'Emulgel', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (172, 113, N'100 Mg Tab.', N'10')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (173, 113, N'200 mg tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (174, 114, N'Cab', NULL)
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (175, 115, N'300 cap', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (176, 115, N'250   susp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (177, 97, N'CAP', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (178, 116, N'Quick', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (179, 117, N'TAB', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (180, 118, N'10 tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (181, 119, N'jel', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (182, 119, N'spray', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (183, 120, N'----', N'---')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (184, 121, N'craem', N'f')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (185, 122, N'GEL', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (186, 124, N'-', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (187, 125, N'sensitve teeth', N'-')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (188, 126, N'8 MG', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (189, 127, N'50 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (190, 128, N'tab 400 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (191, 129, N'875', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (192, 130, N'500 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (193, 130, N'400 mg\5ml', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (194, 131, N'500', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (195, 131, N'250', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (196, 92, N'200  Susp', N'SUSP')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (197, 132, N'2 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (198, 132, N'4 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (199, 133, N'1 g Amp', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (200, 85, N'500 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (201, 85, N'750 mg', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (202, 135, N'400', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (203, 135, N'200', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (204, 136, N'90 mg tab', N'')
GO
INSERT [dbo].[MedicineShape] ([ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo]) VALUES (205, 137, N'50  mg', N'')
GO
SET IDENTITY_INSERT [dbo].[MedicineShape] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicineDoze] ON 
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (1, 1, N'3 x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (2, 2, N'3 x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (3, 3, N'3 x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (4, 4, N'2x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (5, 5, N'2x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (6, 6, N'3x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (7, 7, N'3x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (9, 8, N'3x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (10, 9, N'3x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (12, 12, N'3 x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (13, 13, N'2 x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (14, 14, N'3 x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (15, 15, N'3 x dlyقبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (18, 18, N'1cap x 4 dly قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (19, 19, N'5ml x 4 dly قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (21, 20, N'1cap x 4 dly قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (22, 21, N'5 ml x 4 dly قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (23, 22, N'5 ml x 4 dly before meals')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (24, 23, N'5 ml x 4 dly قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (27, 24, N'1cap x 4 dly before meals')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (28, 25, N'1cap x 4 dly قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (29, 26, N'1 tab x2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (30, 27, N'1 tab x3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (32, 29, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (33, 30, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (34, 31, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (35, 32, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (36, 33, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (37, 34, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (38, 35, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (39, 36, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (40, 37, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (41, 38, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (42, 39, N'1 tab 2xdly بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (43, 40, N'3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (44, 42, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (45, 43, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (46, 41, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (47, 44, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (49, 46, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (50, 47, N'1 tab 2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (51, 48, N'1 x 4')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (52, 49, N'1 x 4')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (53, 50, N'1 x 4')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (54, 51, N'1 x 4')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (55, 52, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (56, 53, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (57, 54, N'1 x 2 x 2 d')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (58, 55, N'4 X dly-1 hr قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (59, 56, N'1 tab x 3 dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (60, 57, N'1 x 3 ---10 tab')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (61, 58, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (70, 64, N'3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (71, 65, N'1tab 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (72, 66, N'1tab 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (75, 69, N'1 tab 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (76, 71, N'1 tab 3 x dlyبعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (77, 72, N'1 tab 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (78, 76, N'1capl 2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (79, 77, N'1capl 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (80, 78, N'1capl 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (81, 79, N'1 cap 3 x dly بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (82, 80, N'1tab 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (83, 81, N'1tab 2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (84, 83, N'once dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (85, 84, N'once dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (88, 87, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (89, 88, N'4 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (90, 89, N'4 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (92, 91, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (93, 92, N'5 ml x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (94, 93, N'5 ml x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (95, 94, N'5 ml x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (97, 97, N'3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (98, 98, N'3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (99, 99, N'3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (100, 100, N'3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (101, 101, N'1  x  2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (102, 102, N'1  x  2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (103, 103, N'1  x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (104, 104, N'1  x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (105, 105, N'1  x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (106, 106, N'1  x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (108, 90, N'1 X 4')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (109, 107, N'1 x  1  بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (111, 73, N'1 X 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (112, 74, N'1 X 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (113, 75, N'1 X 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (114, 108, N'1  x  3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (115, 109, N' 1ml 4 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (116, 110, N'4 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (117, 111, N'1 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (118, 112, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (119, 113, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (120, 114, N'1 x 2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (121, 115, N'1  x  2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (123, 117, N'1  x 2 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (124, 118, N'عند اللزوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (127, 121, N'3-4 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (128, 122, N'1 tab 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (129, 123, N'1 tab 3 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (130, 124, N'1  x  1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (131, 125, N'1 x 1 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (132, 126, N'1 x 1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (133, 127, N'1 x 1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (134, 128, N'1 x 2 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (135, 129, N'1 x 2 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (136, 70, N'1X2 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (137, 131, N'1 x 1 x 3 قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (138, 132, N'1 x 2 x 3 قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (139, 133, N'1tab 2 x dlyبعد الاكل  ')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (140, 134, N'1tab 2 x dly بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (141, 130, N'1X2 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (142, 146, N'1 x  1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (143, 147, N'1 x 3  بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (144, 148, N'1 x 2 dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (145, 136, N'1x1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (146, 137, N'1x2x1  then 1x1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (147, 149, N'1x2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (148, 150, N'1x2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (149, 151, N'1x2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (150, 152, N'1x1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (151, 153, N'S.O.S')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (152, 154, N'1 tab x 3 dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (153, 155, N'1X1 او عند اللزوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (154, 98, N'1 X 2 dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (155, 157, N'1 x 2 dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (156, 158, N'1 x 3 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (157, 159, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (158, 160, N'1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (160, 161, N'1 x 1 x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (161, 162, N'1 x 1 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (162, 163, N'1 x 2 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (163, 164, N'1x3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (164, 165, N'1x2 ')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (165, 166, N'1 x 3 tab')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (166, 167, N'            Drops x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (167, 168, N'1x1--1x2')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (168, 169, N'1  x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (169, 170, N'ابره مع كاس حليب كل اسبوعين  لمدة شهر')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (170, 171, N'دعك المكان المصاب 3- 4 مرات باليوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (171, 172, N'2 x dly')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (172, 172, N'2 x dly اثناء الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (173, 172, N'2 x dly اثناء الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (174, 173, N'2 x dly اثناء الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (175, 174, N'1 X 3 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (176, 175, N'1  x  2 قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (177, 176, N'1  x 1 قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (178, 177, N'كبسوله واحده قبل النوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (179, 178, N'1cap x 3 dly بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (180, 179, N'S.O.S')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (181, 180, N'1  x 2   بعد الاكل بساعتين')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (182, 181, N'عند اللزوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (183, 182, N'عند اللزوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (184, 183, N'---')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (185, 155, N'1 x 1')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (186, 184, N'1 x 1  او عند اللزوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (187, 169, N'1  X 2-3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (188, 185, N'1  X 2-3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (189, 186, N'يدلك بالاصبع 3 مرات ')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (190, 187, N'1  x 3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (191, 188, N'1  X 2 بعد الاكل بساعتين')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (192, 189, N'S . O.S بعد الاكل ')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (193, 190, N'1x1 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (194, 191, N'1X2   after meal')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (195, 192, N'1   x   3  بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (196, 193, N'1   x  3  بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (197, 194, N'1  X  1  بعد الكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (198, 195, N'1  X  1  بعد الكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (199, 140, N'1  X  1  قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (200, 141, N'1  X  1  قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (201, 196, N'1  X  1  قبل الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (202, 197, N'1  x 1 قبل النوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (203, 198, N'1  x 1 قبل النوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (204, 199, N'1   x   1   x  3')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (205, 202, N'1  x  3 or عند اللزوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (206, 203, N'1  x  3 or معند اللزو')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (207, 203, N'1  x  3 or عند اللزوم')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (208, 204, N'1*1 بعد الاكل')
GO
INSERT [dbo].[MedicineDoze] ([DozeID], [ShapeID], [Doze]) VALUES (209, 205, N'1   x  2')
GO
SET IDENTITY_INSERT [dbo].[MedicineDoze] OFF
GO
SET IDENTITY_INSERT [dbo].[Health] ON 
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (1, N'سليم')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (2, N'أزمة')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (3, N'سكري')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (4, N'الضغط')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (5, N'تصلب شرايين')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (6, N'القلب')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (7, N'التهاب الكبد الوبائي')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (8, N'الكلى')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (9, N'صرع')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (10, N'القلب,الضغط,السكري')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (11, N'الضغط,السكري')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (12, N'القلب,السكري')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (13, N'القلب,الضغط')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (14, N'تجلط في الدم')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (15, N'قلب مفتوح')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (16, N'شحنات كهربائية')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (17, N'حساسية على البنسلين')
GO
INSERT [dbo].[Health] ([HID], [HealthStat]) VALUES (18, N'تسارع في دقات القلب')
GO
SET IDENTITY_INSERT [dbo].[Health] OFF
GO
SET IDENTITY_INSERT [dbo].[ImplantBrand] ON 
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (1, N'ADIN')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (2, N'ALFABIO')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (3, N'ALFAGATE')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (4, N'ALTECHNO')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (5, N'B&B')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (6, N'BCI')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (7, N'BIOHORIZON')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (8, N'BIOLINE')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (9, N'BIOTEC')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (10, N'CORTEX')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (11, N'DITRON')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (12, N'GLOBAL')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (13, N'IMPLA-NUR')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (14, N'IVORY')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (15, N'MIS')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (16, N'ONE PIECE')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (17, N'ROOT')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (18, N'SMART')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (19, N'STRAUMAN')
GO
INSERT [dbo].[ImplantBrand] ([BrandID], [BrandName]) VALUES (20, N'TECNOLOGY')
GO
SET IDENTITY_INSERT [dbo].[ImplantBrand] OFF
GO
SET IDENTITY_INSERT [dbo].[ImplantDiameter] ON 
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (1, CAST(2.80 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (2, CAST(2.90 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (3, CAST(3.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (4, CAST(3.30 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (5, CAST(3.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (6, CAST(3.75 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (7, CAST(4.20 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (8, CAST(5.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantDiameter] ([DiameterID], [DiameterMM]) VALUES (9, CAST(6.00 AS Decimal(4, 2)))
GO
SET IDENTITY_INSERT [dbo].[ImplantDiameter] OFF
GO
SET IDENTITY_INSERT [dbo].[ImplantLength] ON 
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (1, CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (2, CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (3, CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (4, CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (5, CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (6, CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (7, CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantLength] ([LengthID], [LengthMM]) VALUES (8, CAST(20.00 AS Decimal(4, 2)))
GO
SET IDENTITY_INSERT [dbo].[ImplantLength] OFF
GO
SET IDENTITY_INSERT [dbo].[ImplantMeasure] ON 
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (5, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (6, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (7, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (8, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (9, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (10, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (11, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (12, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (13, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (14, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (15, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (16, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (17, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (18, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (19, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (20, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (21, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (22, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (23, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (24, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (25, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (26, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (27, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (28, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (29, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (30, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (31, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (32, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (33, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (34, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (35, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (36, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (37, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (38, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (39, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (40, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (41, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (42, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (43, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (44, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (45, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (46, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (47, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (48, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (49, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (50, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (51, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (52, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (53, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (54, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (55, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (56, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (57, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (58, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (59, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (60, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (61, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (62, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (63, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (64, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (65, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (66, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (67, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (68, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (69, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (70, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (71, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (72, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (73, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (74, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (75, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (76, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (77, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (78, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (79, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (80, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (81, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (82, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (83, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (84, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (85, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (86, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (87, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (88, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (89, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (90, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (91, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (92, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (93, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (94, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (95, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (96, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (97, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (98, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (99, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (100, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (101, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (102, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (103, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (104, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (105, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (106, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (107, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (108, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (109, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (110, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (111, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (112, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (113, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (114, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (115, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (116, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (117, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (118, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (119, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (120, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (121, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (122, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (123, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (124, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (125, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (126, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (127, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (128, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (129, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (130, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (131, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (132, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (133, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (134, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (135, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (136, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (137, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (138, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (139, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (140, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (141, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (142, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (143, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (144, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (145, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (146, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (147, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (148, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (149, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (150, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (151, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (152, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (153, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (154, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (155, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (156, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (157, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (158, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (159, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (160, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (161, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (162, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (163, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (164, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (165, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (166, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (167, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (168, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (169, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (170, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (171, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (172, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (173, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (174, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (175, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (176, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (177, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (178, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (179, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (180, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (181, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (182, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (183, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (184, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (185, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (186, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (187, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (188, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (189, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (190, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (191, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (192, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (193, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (194, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (195, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (196, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (197, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (198, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (199, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (200, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (201, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (202, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (203, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (204, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (205, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (206, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (207, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (208, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (209, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (210, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (211, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (212, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (213, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (214, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (215, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (216, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (217, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (218, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (219, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (220, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (221, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (222, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (223, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (224, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (225, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (226, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (227, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (228, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (229, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (230, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (231, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (232, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (233, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (234, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (235, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (236, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (237, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (238, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (239, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (240, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (241, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (242, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (243, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (244, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (245, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (246, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (247, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (248, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (249, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (250, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (251, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (252, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (253, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (254, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (255, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (256, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (257, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (258, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (259, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (260, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (261, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (262, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (263, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (264, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (265, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (266, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (267, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (268, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (269, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (270, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (271, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (272, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (273, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (274, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (275, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (276, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (277, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (278, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (279, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (280, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (281, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (282, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (283, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (284, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (285, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (286, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (287, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (288, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (289, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (290, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (291, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (292, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (293, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (294, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (295, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (296, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (297, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (298, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (299, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (300, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (301, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (302, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (303, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (304, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (305, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (306, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (307, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (308, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (309, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (310, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (311, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (312, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (313, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (314, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (315, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (316, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (317, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (318, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (319, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (320, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (321, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (322, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (323, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (324, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (325, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (326, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (327, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (328, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (329, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (330, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (331, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (332, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (333, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (334, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (335, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (336, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (337, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (338, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (339, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (340, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (341, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (342, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (343, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (344, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (345, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (346, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (347, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (348, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (349, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (350, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (351, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (352, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (353, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (354, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (355, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (356, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (357, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (358, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (359, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (360, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (361, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (362, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (363, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (364, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (365, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (366, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (367, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (368, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (369, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (370, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (371, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (372, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (373, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (374, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (375, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (376, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (377, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (378, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (379, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (380, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (381, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (382, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (383, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (384, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (385, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (386, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (387, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (388, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (389, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (390, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (391, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (392, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (393, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (394, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (395, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (396, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (397, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (398, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (399, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (400, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (401, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (402, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (403, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (404, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (405, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (406, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (407, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (408, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (409, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (410, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (411, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (412, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (413, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (414, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (415, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (416, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (417, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (418, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (419, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (420, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (421, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (422, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (423, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (424, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (425, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (426, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (427, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (428, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (429, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (430, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (431, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (432, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (433, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (434, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (435, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (436, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (437, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (438, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (439, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (440, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (441, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (442, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (443, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (444, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (445, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (446, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (447, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (448, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (449, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (450, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (451, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (452, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (453, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (454, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (455, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (456, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (457, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (458, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (459, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (460, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (461, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (462, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (463, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (464, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (465, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (466, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (467, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (468, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (469, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (470, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (471, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (472, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (473, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (474, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (475, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (476, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (477, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (478, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (479, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (480, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (481, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (482, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (483, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (484, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (485, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (486, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (487, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (488, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (489, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (490, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (491, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (492, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (493, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (494, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (495, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (496, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (497, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (498, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (499, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (500, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (501, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (502, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (503, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (504, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (505, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (506, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (507, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (508, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (509, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (510, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (511, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (512, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (513, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (514, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (515, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (516, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (517, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (518, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (519, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (520, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (521, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (522, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (523, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (524, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (525, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (526, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (527, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (528, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (529, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (530, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (531, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (532, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (533, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (534, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (535, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (536, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (537, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (538, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (539, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (540, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (541, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (542, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (543, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (544, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (545, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (546, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (547, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (548, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (549, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (550, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (551, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (552, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (553, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (554, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (555, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (556, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (557, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (558, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (559, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (560, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (561, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (562, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (563, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (564, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (565, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (566, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (567, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (568, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (569, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (570, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (571, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (572, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (573, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (574, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (575, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (576, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (577, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (578, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (579, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (580, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (581, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (582, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (583, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (584, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (585, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (586, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (587, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (588, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (589, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (590, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (591, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (592, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (593, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (594, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (595, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (596, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (597, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (598, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (599, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (600, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (601, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (602, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (603, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (604, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (605, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (606, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (607, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (608, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (609, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (610, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (611, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (612, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (613, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (614, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (615, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (616, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (617, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (618, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (619, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (620, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (621, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (622, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (623, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (624, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (625, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (626, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (627, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (628, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (629, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (630, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (631, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (632, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (633, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (634, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (635, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (636, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (637, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (638, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (639, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (640, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (641, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (642, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (643, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (644, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (645, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (646, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (647, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (648, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (649, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (650, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (651, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (652, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (653, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (654, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (655, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (656, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (657, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (658, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (659, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (660, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (661, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (662, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (663, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (664, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (665, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (666, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (667, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (668, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (669, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (670, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (671, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (672, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (673, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (674, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (675, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (676, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (677, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (678, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (679, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (680, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (681, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (682, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (683, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (684, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (685, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (686, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (687, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (688, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (689, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (690, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (691, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (692, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (693, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (694, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (695, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (696, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (697, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (698, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (699, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (700, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (701, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (702, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (703, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (704, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (705, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (706, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (707, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (708, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (709, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (710, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (711, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (712, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (713, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (714, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (715, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (716, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (717, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (718, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (719, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (720, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (721, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (722, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (723, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (724, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (725, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (726, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (727, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (728, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (729, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (730, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (731, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (732, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (733, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (734, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (735, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (736, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (737, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (738, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (739, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (740, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (741, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (742, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (743, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (744, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (745, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (746, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (747, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (748, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (749, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (750, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (751, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (752, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (753, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (754, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (755, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (756, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (757, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (758, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (759, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (760, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (761, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (762, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (763, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (764, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (765, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (766, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (767, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (768, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (769, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (770, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (771, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (772, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (773, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (774, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (775, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (776, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (777, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (778, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (779, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (780, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (781, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (782, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (783, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (784, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (785, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (786, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (787, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (788, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (789, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (790, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (791, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (792, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (793, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (794, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (795, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (796, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (797, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (798, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (799, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (800, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (801, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (802, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (803, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (804, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (805, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (806, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (807, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (808, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (809, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (810, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (811, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (812, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (813, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (814, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (815, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (816, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (817, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (818, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (819, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (820, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (821, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (822, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (823, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (824, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (825, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (826, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (827, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (828, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (829, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (830, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (831, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (832, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (833, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (834, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (835, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (836, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (837, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (838, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (839, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (840, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (841, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (842, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (843, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (844, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (845, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (846, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (847, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (848, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (849, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (850, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (851, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (852, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (853, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (854, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (855, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (856, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (857, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (858, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (859, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (860, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (861, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (862, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (863, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (864, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (865, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (866, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (867, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (868, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (869, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (870, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (871, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (872, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (873, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (874, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (875, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (876, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (877, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (878, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (879, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (880, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (881, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (882, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (883, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (884, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (885, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (886, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (887, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (888, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (889, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (890, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (891, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (892, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (893, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (894, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (895, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (896, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (897, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (898, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (899, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (900, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (901, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (902, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (903, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (904, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (905, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (906, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (907, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (908, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (909, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (910, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (911, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (912, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (913, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (914, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (915, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (916, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (917, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (918, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (919, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (920, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (921, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (922, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (923, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (924, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (925, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (926, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (927, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (928, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (929, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (930, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (931, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (932, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (933, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (934, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (935, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (936, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (937, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (938, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (939, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (940, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (941, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (942, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (943, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (944, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (945, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (946, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (947, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (948, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (949, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (950, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (951, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (952, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (953, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (954, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (955, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (956, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (957, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (958, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (959, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (960, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (961, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (962, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (963, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (964, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (965, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (966, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (967, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (968, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (969, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (970, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (971, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (972, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (973, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (974, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (975, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (976, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (977, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (978, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (979, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (980, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (981, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (982, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (983, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (984, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (985, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (986, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (987, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (988, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (989, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (990, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (991, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (992, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (993, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (994, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (995, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (996, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (997, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (998, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (999, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1000, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1001, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1002, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1003, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1004, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1005, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1006, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1007, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1008, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1009, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1010, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1011, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1012, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1013, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1014, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1015, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1016, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1017, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1018, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1019, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1020, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1021, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1022, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1023, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1024, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1025, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1026, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1027, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1028, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1029, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1030, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1031, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1032, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1033, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1034, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1035, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1036, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1037, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1038, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1039, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1040, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1041, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1042, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1043, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1044, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1045, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1046, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1047, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1048, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1049, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1050, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1051, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1052, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1053, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1054, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1055, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1056, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1057, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1058, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1059, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1060, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1061, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1062, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1063, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1064, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1065, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1066, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1067, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1068, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1069, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1070, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1071, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1072, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1073, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1074, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1075, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1076, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1077, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1078, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1079, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1080, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1081, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1082, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1083, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1084, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1085, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1086, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1087, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1088, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1089, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1090, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1091, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1092, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1093, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1094, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1095, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1096, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1097, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1098, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1099, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1100, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1101, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1102, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1103, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1104, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1105, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1106, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1107, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1108, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1109, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1110, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1111, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1112, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1113, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1114, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1115, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1116, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1117, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1118, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1119, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1120, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1121, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1122, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1123, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1124, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1125, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1126, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1127, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1128, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1129, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1130, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1131, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1132, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1133, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1134, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1135, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1136, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1137, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1138, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1139, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1140, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1141, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1142, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1143, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1144, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1145, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1146, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1147, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1148, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1149, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1150, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1151, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1152, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1153, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1154, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1155, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1156, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1157, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1158, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1159, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1160, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1161, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1162, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1163, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1164, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1165, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1166, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1167, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1168, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1169, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1170, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1171, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1172, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1173, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1174, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1175, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1176, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1177, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1178, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1179, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1180, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1181, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1182, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1183, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1184, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1185, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1186, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1187, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1188, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1189, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1190, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1191, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1192, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1193, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1194, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1195, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1196, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1197, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1198, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1199, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1200, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1201, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1202, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1203, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1204, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1205, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1206, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1207, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1208, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1209, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1210, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1211, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1212, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1213, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1214, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1215, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1216, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1217, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1218, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1219, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1220, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1221, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1222, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1223, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1224, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1225, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1226, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1227, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1228, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1229, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1230, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1231, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1232, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1233, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1234, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1235, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1236, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1237, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1238, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1239, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1240, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1241, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1242, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1243, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1244, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1245, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1246, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1247, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1248, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1249, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1250, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1251, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1252, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1253, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1254, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1255, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1256, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1257, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1258, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1259, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1260, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1261, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1262, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1263, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1264, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1265, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1266, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1267, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1268, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1269, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1270, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1271, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1272, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1273, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1274, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1275, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1276, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1277, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1278, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1279, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1280, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1281, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1282, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1283, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1284, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1285, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1286, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1287, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1288, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1289, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1290, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1291, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1292, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1293, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1294, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1295, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1296, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1297, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1298, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1299, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1300, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1301, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1302, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1303, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1304, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1305, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1306, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1307, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1308, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1309, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1310, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1311, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1312, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1313, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1314, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1315, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1316, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1317, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1318, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1319, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1320, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1321, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1322, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1323, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1324, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1325, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1326, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1327, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1328, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1329, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1330, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1331, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1332, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1333, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1334, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1335, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1336, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1337, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1338, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1339, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1340, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1341, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1342, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1343, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1344, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1345, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1346, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1347, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1348, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1349, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1350, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1351, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1352, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1353, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1354, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1355, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1356, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1357, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1358, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1359, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1360, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1361, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1362, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1363, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1364, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1365, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1366, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1367, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1368, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1369, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1370, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1371, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1372, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1373, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1374, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1375, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1376, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1377, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1378, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1379, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1380, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1381, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1382, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1383, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1384, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1385, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1386, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1387, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1388, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1389, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1390, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1391, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1392, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1393, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1394, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1395, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1396, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1397, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1398, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1399, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1400, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1401, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1402, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1403, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1404, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1405, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1406, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1407, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1408, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1409, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1410, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1411, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1412, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1413, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1414, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1415, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1416, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1417, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1418, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1419, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1420, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1421, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1422, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1423, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1424, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1425, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1426, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1427, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1428, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1429, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1430, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1431, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1432, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1433, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1434, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1435, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1436, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1437, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1438, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1439, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1440, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1441, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1442, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1443, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1444, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1445, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1446, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1447, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1448, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1449, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1450, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1451, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1452, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1453, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1454, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1455, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1456, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1457, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1458, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1459, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1460, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1461, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1462, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1463, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1464, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1465, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1466, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1467, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1468, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1469, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1470, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1471, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1472, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1473, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1474, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1475, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1476, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1477, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1478, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1479, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1480, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1481, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1482, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1483, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1484, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1485, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1486, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1487, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1488, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1489, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1490, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1491, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1492, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1493, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1494, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1495, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1496, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1497, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1498, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1499, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1500, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1501, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1502, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1503, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1504, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1505, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1506, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1507, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1508, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1509, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1510, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1511, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1512, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1513, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1514, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1515, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1516, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1517, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1518, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1519, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1520, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1521, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1522, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1523, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1524, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1525, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1526, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1527, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1528, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1529, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1530, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1531, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1532, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1533, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1534, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1535, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1536, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1537, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1538, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1539, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1540, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1541, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1542, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1543, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1544, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1545, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1546, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1547, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1548, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1549, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1550, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1551, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1552, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1553, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1554, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1555, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1556, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1557, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1558, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1559, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1560, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1561, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1562, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1563, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1564, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1565, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1566, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1567, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1568, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1569, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1570, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1571, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1572, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1573, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1574, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1575, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1576, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1577, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1578, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1579, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1580, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1581, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1582, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1583, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1584, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1585, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1586, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1587, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1588, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1589, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1590, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1591, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1592, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1593, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1594, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1595, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1596, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1597, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1598, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1599, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1600, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1601, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1602, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1603, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1604, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1605, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1606, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1607, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1608, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1609, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1610, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1611, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1612, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1613, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1614, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1615, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1616, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1617, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1618, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1619, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1620, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1621, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1622, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1623, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1624, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1625, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1626, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1627, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1628, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1629, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1630, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1631, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1632, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1633, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1634, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1635, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1636, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1637, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1638, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1639, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1640, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1641, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1642, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1643, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1644, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1645, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1646, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1647, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1648, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1649, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1650, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1651, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1652, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1653, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1654, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1655, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1656, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1657, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1658, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1659, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1660, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1661, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1662, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1663, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1664, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1665, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1666, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1667, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1668, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1669, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1670, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1671, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1672, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1673, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1674, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1675, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1676, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1677, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1678, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1679, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1680, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1681, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1682, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1683, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1684, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1685, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1686, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1687, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1688, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1689, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1690, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1691, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1692, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1693, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1694, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1695, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1696, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1697, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1698, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1699, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1700, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1701, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1702, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1703, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1704, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1705, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1706, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1707, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1708, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1709, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1710, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1711, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1712, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1713, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1714, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1715, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1716, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1717, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1718, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1719, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1720, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1721, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1722, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1723, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1724, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1725, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1726, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1727, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1728, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1729, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1730, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1731, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1732, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1733, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1734, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1735, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1736, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1737, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1738, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1739, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1740, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1741, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1742, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1743, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1744, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1745, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1746, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1747, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1748, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1749, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1750, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1751, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1752, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1753, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1754, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1755, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1756, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1757, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1758, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1759, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1760, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1761, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1762, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1763, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1764, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1765, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1766, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1767, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1768, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1769, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1770, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1771, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1772, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1773, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1774, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1775, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1776, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1777, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1778, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1779, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1780, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1781, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1782, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1783, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1784, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1785, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1786, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1787, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1788, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1789, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1790, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1791, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1792, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1793, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1794, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1795, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1796, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1797, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1798, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1799, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1800, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1801, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1802, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1803, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1804, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1805, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1806, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1807, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1808, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1809, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1810, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1811, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1812, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1813, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1814, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1815, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1816, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1817, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1818, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1819, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1820, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1821, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1822, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1823, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1824, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1825, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1826, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1827, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1828, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1829, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1830, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1831, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1832, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1833, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1834, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1835, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1836, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1837, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1838, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1839, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1840, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1841, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1842, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1843, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1844, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1845, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1846, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1847, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1848, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1849, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1850, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1851, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1852, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1853, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1854, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1855, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1856, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1857, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1858, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1859, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1860, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1861, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1862, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1863, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1864, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1865, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1866, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1867, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1868, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1869, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1870, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1871, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1872, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1873, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1874, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1875, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1876, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1877, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1878, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1879, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1880, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1881, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1882, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1883, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1884, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1885, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1886, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1887, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1888, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1889, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1890, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1891, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1892, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1893, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1894, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1895, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1896, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1897, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1898, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1899, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1900, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1901, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1902, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1903, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1904, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1905, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1906, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1907, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1908, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1909, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1910, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1911, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1912, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1913, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1914, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1915, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1916, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1917, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1918, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1919, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1920, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1921, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1922, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1923, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1924, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1925, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1926, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1927, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1928, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1929, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1930, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1931, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1932, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1933, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1934, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1935, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1936, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1937, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1938, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1939, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1940, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1941, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1942, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1943, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1944, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1945, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1946, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1947, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1948, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1949, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1950, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1951, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1952, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1953, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1954, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1955, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1956, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1957, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1958, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1959, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1960, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1961, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1962, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1963, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1964, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1965, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1966, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1967, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1968, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1969, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1970, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1971, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1972, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1973, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1974, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1975, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1976, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1977, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1978, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1979, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1980, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1981, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1982, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1983, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1984, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1985, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1986, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1987, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1988, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1989, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1990, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1991, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1992, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1993, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1994, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1995, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1996, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1997, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1998, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (1999, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2000, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2001, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2002, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2003, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2004, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2005, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2006, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2007, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2008, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2009, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2010, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2011, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2012, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2013, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2014, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2015, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2016, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2017, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2018, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2019, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2020, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2021, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2022, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2023, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2024, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2025, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2026, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2027, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2028, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2029, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2030, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2031, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2032, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2033, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2034, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2035, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2036, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2037, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2038, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2039, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2040, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2041, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2042, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2043, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2044, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2045, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2046, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2047, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2048, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2049, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2050, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2051, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2052, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2053, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2054, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2055, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2056, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2057, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2058, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2059, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2060, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2061, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2062, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2063, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2064, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2065, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2066, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2067, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2068, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2069, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2070, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2071, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2072, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2073, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2074, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2075, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2076, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2077, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2078, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2079, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2080, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2081, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2082, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2083, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2084, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2085, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2086, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2087, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2088, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2089, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2090, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2091, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2092, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2093, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2094, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2095, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2096, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2097, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2098, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2099, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2100, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2101, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2102, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2103, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2104, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2105, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2106, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2107, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2108, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2109, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2110, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2111, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2112, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2113, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2114, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2115, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2116, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2117, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2118, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2119, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2120, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2121, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2122, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2123, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2124, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2125, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2126, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2127, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2128, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2129, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2130, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2131, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2132, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2133, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2134, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2135, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2136, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2137, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2138, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2139, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2140, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2141, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2142, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2143, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2144, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2145, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2146, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2147, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2148, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2149, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2150, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2151, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2152, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2153, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2154, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2155, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2156, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2157, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2158, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2159, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2160, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2161, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2162, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2163, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2164, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2165, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2166, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2167, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2168, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2169, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2170, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2171, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2172, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2173, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2174, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2175, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2176, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2177, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2178, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2179, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2180, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2181, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2182, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2183, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2184, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2185, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2186, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2187, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2188, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2189, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2190, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2191, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2192, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2193, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2194, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2195, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2196, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2197, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2198, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2199, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2200, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2201, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2202, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2203, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2204, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2205, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2206, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2207, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2208, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2209, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2210, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2211, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2212, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2213, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2214, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2215, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2216, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2217, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2218, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2219, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2220, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2221, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2222, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2223, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2224, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2225, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2226, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2227, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2228, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2229, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2230, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2231, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2232, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2233, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2234, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2235, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2236, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2237, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2238, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2239, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2240, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2241, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2242, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2243, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2244, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2245, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2246, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2247, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2248, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2249, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2250, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2251, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2252, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2253, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2254, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2255, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2256, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2257, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2258, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2259, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2260, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2261, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2262, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2263, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2264, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2265, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2266, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2267, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2268, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2269, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2270, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2271, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2272, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2273, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2274, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2275, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2276, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2277, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2278, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2279, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2280, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2281, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2282, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2283, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2284, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2285, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2286, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2287, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2288, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2289, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2290, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2291, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2292, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2293, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2294, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2295, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2296, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2297, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2298, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2299, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2300, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2301, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2302, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2303, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2304, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2305, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2306, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2307, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2308, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2309, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2310, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2311, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2312, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2313, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2314, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2315, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2316, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2317, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2318, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2319, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2320, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2321, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2322, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2323, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2324, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2325, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2326, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2327, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2328, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2329, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2330, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2331, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2332, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2333, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2334, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2335, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2336, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2337, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2338, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2339, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2340, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2341, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2342, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2343, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2344, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2345, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2346, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2347, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2348, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2349, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2350, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2351, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2352, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2353, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2354, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2355, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2356, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2357, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2358, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2359, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2360, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2361, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2362, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2363, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2364, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2365, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2366, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2367, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2368, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2369, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2370, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2371, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2372, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2373, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2374, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2375, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2376, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2377, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2378, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2379, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2380, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2381, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2382, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2383, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2384, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2385, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2386, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2387, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2388, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2389, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2390, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2391, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2392, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2393, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2394, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2395, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2396, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2397, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2398, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2399, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2400, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2401, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2402, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2403, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2404, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2405, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2406, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2407, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2408, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2409, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2410, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2411, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2412, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2413, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2414, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2415, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2416, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2417, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2418, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2419, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2420, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2421, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2422, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2423, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2424, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2425, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2426, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2427, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2428, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2429, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2430, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2431, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2432, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2433, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2434, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2435, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2436, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2437, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2438, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2439, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2440, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2441, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2442, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2443, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2444, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2445, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2446, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2447, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2448, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2449, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2450, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2451, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2452, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2453, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2454, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2455, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2456, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2457, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2458, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2459, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2460, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2461, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2462, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2463, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2464, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2465, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2466, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2467, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2468, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2469, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2470, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2471, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2472, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2473, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2474, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2475, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2476, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2477, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2478, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2479, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2480, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2481, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2482, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2483, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2484, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2485, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2486, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2487, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2488, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2489, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2490, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2491, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2492, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2493, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2494, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2495, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2496, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2497, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2498, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2499, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2500, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2501, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2502, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2503, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2504, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2505, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2506, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2507, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2508, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2509, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2510, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2511, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2512, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2513, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2514, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2515, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2516, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2517, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2518, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2519, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2520, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2521, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2522, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2523, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2524, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2525, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2526, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2527, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2528, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2529, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2530, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2531, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2532, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2533, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2534, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2535, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2536, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2537, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2538, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2539, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2540, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2541, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2542, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2543, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2544, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2545, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2546, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2547, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2548, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2549, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2550, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2551, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2552, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2553, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2554, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2555, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2556, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2557, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2558, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2559, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2560, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2561, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2562, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2563, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2564, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2565, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2566, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2567, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2568, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2569, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2570, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2571, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2572, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2573, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2574, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2575, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2576, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2577, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2578, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2579, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2580, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2581, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2582, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2583, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2584, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2585, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2586, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2587, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2588, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2589, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2590, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2591, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2592, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2593, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2594, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2595, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2596, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2597, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2598, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2599, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2600, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2601, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2602, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2603, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2604, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2605, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2606, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2607, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2608, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2609, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2610, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2611, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2612, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2613, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2614, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2615, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2616, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2617, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2618, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2619, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2620, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2621, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2622, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2623, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2624, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2625, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2626, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2627, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2628, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2629, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2630, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2631, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2632, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2633, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2634, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2635, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2636, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2637, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2638, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2639, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2640, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2641, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2642, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2643, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2644, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2645, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2646, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2647, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2648, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2649, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2650, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2651, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2652, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2653, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2654, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2655, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2656, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2657, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2658, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2659, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2660, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2661, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2662, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2663, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2664, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2665, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2666, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2667, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2668, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2669, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2670, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2671, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2672, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2673, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2674, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2675, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2676, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2677, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2678, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2679, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2680, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2681, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2682, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2683, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2684, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2685, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2686, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2687, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2688, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2689, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2690, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2691, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2692, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2693, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2694, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2695, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2696, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2697, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2698, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2699, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2700, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2701, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2702, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2703, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2704, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2705, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2706, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2707, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2708, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2709, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2710, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2711, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2712, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2713, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2714, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2715, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2716, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2717, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2718, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2719, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2720, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2721, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2722, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2723, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2724, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2725, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2726, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2727, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2728, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2729, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2730, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2731, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2732, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2733, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2734, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2735, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2736, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2737, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2738, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2739, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2740, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2741, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2742, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2743, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2744, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2745, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2746, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2747, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2748, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2749, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2750, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2751, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2752, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2753, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2754, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2755, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2756, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2757, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2758, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2759, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2760, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2761, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2762, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2763, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2764, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2765, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2766, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2767, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2768, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2769, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2770, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2771, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2772, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2773, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2774, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2775, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2776, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2777, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2778, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2779, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2780, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2781, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2782, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2783, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2784, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2785, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2786, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2787, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2788, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2789, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2790, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2791, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2792, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2793, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2794, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2795, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2796, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2797, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2798, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2799, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2800, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2801, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2802, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2803, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2804, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2805, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2806, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2807, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2808, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2809, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2810, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2811, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2812, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2813, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2814, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2815, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2816, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2817, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2818, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2819, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2820, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2821, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2822, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2823, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2824, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2825, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2826, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2827, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2828, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2829, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2830, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2831, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2832, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2833, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2834, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2835, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2836, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2837, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2838, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2839, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2840, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2841, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2842, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2843, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2844, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2845, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2846, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2847, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2848, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2849, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2850, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2851, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2852, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2853, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2854, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2855, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2856, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2857, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2858, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2859, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2860, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2861, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2862, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2863, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2864, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2865, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2866, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2867, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2868, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2869, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2870, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2871, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2872, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2873, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2874, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2875, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2876, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2877, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2878, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2879, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2880, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2881, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2882, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2883, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2884, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2885, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2886, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2887, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2888, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2889, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2890, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2891, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2892, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2893, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2894, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2895, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2896, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2897, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2898, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2899, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2900, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2901, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2902, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2903, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2904, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2905, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2906, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2907, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2908, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2909, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2910, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2911, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2912, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2913, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2914, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2915, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2916, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2917, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2918, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2919, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2920, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2921, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2922, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2923, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2924, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2925, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2926, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2927, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2928, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2929, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2930, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2931, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2932, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2933, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2934, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2935, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2936, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2937, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2938, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2939, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2940, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2941, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2942, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2943, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2944, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2945, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2946, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2947, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2948, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2949, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2950, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2951, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2952, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2953, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2954, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2955, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2956, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2957, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2958, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2959, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2960, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2961, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2962, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2963, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2964, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2965, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2966, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2967, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2968, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2969, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2970, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2971, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2972, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2973, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2974, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2975, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2976, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2977, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2978, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2979, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2980, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2981, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2982, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2983, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2984, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2985, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2986, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2987, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2988, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2989, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2990, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2991, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2992, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2993, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2994, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2995, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2996, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2997, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2998, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (2999, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3000, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3001, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3002, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3003, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3004, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3005, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3006, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3007, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3008, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3009, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3010, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3011, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3012, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3013, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3014, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3015, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3016, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3017, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3018, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3019, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3020, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3021, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3022, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3023, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3024, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3025, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3026, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3027, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3028, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3029, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3030, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3031, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3032, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3033, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3034, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3035, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3036, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3037, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3038, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3039, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3040, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3041, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3042, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3043, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3044, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3045, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3046, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3047, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3048, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3049, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3050, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3051, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3052, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3053, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3054, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3055, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3056, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3057, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3058, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3059, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3060, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3061, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3062, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3063, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3064, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3065, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3066, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3067, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3068, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3069, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3070, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3071, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3072, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3073, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3074, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3075, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3076, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3077, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3078, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3079, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3080, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3081, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3082, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3083, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3084, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3085, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3086, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3087, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3088, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3089, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3090, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3091, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3092, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3093, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3094, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3095, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3096, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3097, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3098, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3099, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3100, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3101, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3102, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3103, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3104, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3105, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3106, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3107, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3108, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3109, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3110, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3111, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3112, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3113, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3114, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3115, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3116, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3117, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3118, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3119, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3120, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3121, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3122, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3123, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3124, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3125, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3126, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3127, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3128, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3129, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3130, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3131, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3132, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3133, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3134, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3135, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3136, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3137, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3138, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3139, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3140, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3141, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3142, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3143, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3144, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3145, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3146, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3147, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3148, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3149, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3150, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3151, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3152, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3153, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3154, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3155, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3156, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3157, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3158, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3159, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3160, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3161, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3162, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3163, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3164, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3165, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3166, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3167, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3168, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3169, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3170, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3171, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3172, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3173, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3174, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3175, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3176, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3177, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3178, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3179, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3180, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3181, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3182, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3183, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3184, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3185, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3186, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3187, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3188, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3189, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3190, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3191, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3192, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3193, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3194, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3195, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3196, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3197, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3198, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3199, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3200, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3201, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3202, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3203, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3204, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3205, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3206, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3207, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3208, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3209, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3210, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3211, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3212, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3213, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3214, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3215, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3216, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3217, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3218, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3219, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3220, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3221, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3222, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3223, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3224, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3225, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3226, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3227, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3228, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3229, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3230, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3231, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3232, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3233, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3234, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3235, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3236, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3237, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3238, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3239, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3240, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3241, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3242, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3243, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3244, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3245, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3246, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3247, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3248, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3249, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3250, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3251, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3252, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3253, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3254, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3255, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3256, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3257, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3258, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3259, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3260, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3261, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3262, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3263, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3264, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3265, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3266, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3267, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3268, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3269, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3270, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3271, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3272, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3273, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3274, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3275, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3276, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3277, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3278, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3279, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3280, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3281, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3282, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3283, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3284, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3285, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3286, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3287, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3288, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3289, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3290, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3291, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3292, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3293, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3294, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3295, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3296, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3297, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3298, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3299, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3300, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3301, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3302, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3303, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3304, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3305, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3306, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3307, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3308, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3309, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3310, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3311, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3312, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3313, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3314, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3315, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3316, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3317, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3318, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3319, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3320, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3321, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3322, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3323, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3324, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3325, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3326, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3327, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3328, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3329, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3330, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3331, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3332, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3333, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3334, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3335, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3336, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3337, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3338, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3339, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3340, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3341, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3342, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3343, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3344, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3345, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3346, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3347, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3348, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3349, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3350, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3351, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3352, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3353, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3354, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3355, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3356, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3357, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3358, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3359, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3360, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3361, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3362, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3363, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3364, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3365, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3366, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3367, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3368, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3369, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3370, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3371, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3372, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3373, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3374, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3375, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3376, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3377, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3378, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3379, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3380, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3381, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3382, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3383, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3384, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3385, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3386, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3387, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3388, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3389, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3390, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3391, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3392, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3393, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3394, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3395, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3396, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3397, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3398, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3399, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3400, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3401, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3402, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3403, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3404, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3405, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3406, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3407, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3408, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3409, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3410, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3411, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3412, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3413, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3414, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3415, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3416, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3417, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3418, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3419, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3420, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3421, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3422, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3423, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3424, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3425, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3426, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3427, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3428, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3429, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3430, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3431, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3432, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3433, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3434, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3435, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3436, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3437, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3438, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3439, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3440, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3441, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3442, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3443, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3444, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3445, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3446, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3447, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3448, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3449, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3450, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3451, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3452, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3453, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3454, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3455, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3456, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3457, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3458, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3459, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3460, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3461, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3462, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3463, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3464, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3465, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3466, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3467, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3468, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3469, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3470, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3471, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3472, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3473, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3474, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3475, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3476, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3477, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3478, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3479, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3480, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3481, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3482, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3483, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3484, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3485, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3486, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3487, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3488, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3489, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3490, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3491, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3492, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3493, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3494, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3495, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3496, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3497, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3498, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3499, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3500, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3501, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3502, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3503, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3504, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3505, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3506, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3507, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3508, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3509, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3510, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3511, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3512, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3513, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3514, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3515, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3516, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3517, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3518, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3519, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3520, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3521, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3522, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3523, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3524, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3525, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3526, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3527, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3528, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3529, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3530, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3531, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3532, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3533, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3534, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3535, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3536, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3537, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3538, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3539, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3540, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3541, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3542, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3543, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3544, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3545, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3546, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3547, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3548, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3549, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3550, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3551, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3552, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3553, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3554, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3555, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3556, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3557, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3558, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3559, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3560, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3561, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3562, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3563, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3564, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3565, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3566, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3567, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3568, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3569, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3570, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3571, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3572, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3573, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3574, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3575, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3576, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3577, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3578, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3579, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3580, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3581, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3582, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3583, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3584, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3585, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3586, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3587, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3588, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3589, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3590, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3591, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3592, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3593, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3594, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3595, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3596, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3597, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3598, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3599, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3600, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3601, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3602, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3603, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3604, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3605, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3606, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3607, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3608, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3609, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3610, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3611, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3612, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3613, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3614, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3615, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3616, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3617, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3618, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3619, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3620, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3621, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3622, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3623, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3624, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3625, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3626, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3627, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3628, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3629, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3630, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3631, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3632, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3633, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3634, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3635, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3636, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3637, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3638, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3639, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3640, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3641, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3642, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3643, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3644, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3645, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3646, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3647, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3648, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3649, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3650, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3651, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3652, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3653, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3654, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3655, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3656, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3657, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3658, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3659, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3660, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3661, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3662, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3663, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3664, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3665, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3666, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3667, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3668, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3669, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3670, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3671, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3672, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3673, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3674, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3675, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3676, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3677, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3678, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3679, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3680, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3681, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3682, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3683, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3684, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3685, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3686, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3687, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3688, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3689, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3690, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3691, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3692, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3693, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3694, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3695, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3696, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3697, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3698, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3699, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3700, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3701, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3702, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3703, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3704, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3705, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3706, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3707, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3708, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3709, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3710, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3711, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3712, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3713, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3714, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3715, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3716, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3717, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3718, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3719, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3720, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3721, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3722, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3723, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3724, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3725, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3726, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3727, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3728, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3729, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3730, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3731, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3732, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3733, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3734, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3735, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3736, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3737, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3738, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3739, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3740, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3741, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3742, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3743, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3744, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3745, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3746, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3747, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3748, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3749, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3750, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3751, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3752, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3753, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3754, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3755, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3756, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3757, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3758, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3759, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3760, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3761, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3762, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3763, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3764, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3765, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3766, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3767, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3768, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3769, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3770, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3771, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3772, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3773, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3774, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3775, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3776, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3777, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3778, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3779, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3780, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3781, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3782, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3783, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3784, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3785, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3786, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3787, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3788, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3789, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3790, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3791, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3792, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3793, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3794, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3795, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3796, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3797, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3798, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3799, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3800, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3801, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3802, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3803, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3804, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3805, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3806, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3807, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3808, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3809, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3810, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3811, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3812, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3813, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3814, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3815, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3816, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3817, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3818, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3819, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3820, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3821, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3822, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3823, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3824, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3825, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3826, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3827, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3828, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3829, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3830, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3831, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3832, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3833, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3834, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3835, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3836, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3837, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3838, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3839, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3840, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3841, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3842, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3843, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3844, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3845, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3846, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3847, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3848, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3849, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3850, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3851, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3852, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3853, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3854, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3855, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3856, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3857, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3858, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3859, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3860, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3861, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3862, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3863, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3864, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3865, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3866, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3867, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3868, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3869, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3870, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3871, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3872, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3873, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3874, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3875, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3876, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3877, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3878, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3879, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3880, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3881, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3882, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3883, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3884, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3885, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3886, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3887, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3888, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3889, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3890, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3891, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3892, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3893, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3894, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3895, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3896, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3897, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3898, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3899, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3900, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3901, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3902, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3903, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3904, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3905, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3906, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3907, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3908, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3909, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3910, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3911, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3912, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3913, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3914, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3915, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3916, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3917, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3918, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3919, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3920, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3921, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3922, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3923, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3924, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3925, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3926, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3927, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3928, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3929, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3930, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3931, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3932, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3933, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3934, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3935, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3936, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3937, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3938, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3939, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3940, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3941, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3942, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3943, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3944, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3945, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3946, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3947, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3948, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3949, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3950, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3951, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3952, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3953, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3954, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3955, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3956, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3957, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3958, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3959, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3960, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3961, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3962, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3963, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3964, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3965, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3966, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3967, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3968, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3969, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3970, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3971, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3972, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3973, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3974, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3975, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3976, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3977, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3978, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3979, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3980, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3981, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3982, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3983, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3984, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3985, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3986, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3987, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3988, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3989, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3990, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3991, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3992, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3993, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3994, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3995, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3996, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3997, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3998, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (3999, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4000, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4001, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4002, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4003, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4004, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4005, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4006, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4007, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4008, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4009, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4010, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4011, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4012, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4013, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4014, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4015, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4016, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4017, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4018, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4019, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4020, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4021, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4022, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4023, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4024, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4025, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4026, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4027, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4028, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4029, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4030, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4031, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4032, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4033, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4034, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4035, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4036, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4037, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4038, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4039, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4040, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4041, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4042, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4043, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4044, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4045, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4046, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4047, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4048, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4049, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4050, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4051, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4052, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4053, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4054, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4055, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4056, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4057, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4058, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4059, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4060, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4061, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4062, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4063, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4064, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4065, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4066, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4067, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4068, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4069, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4070, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4071, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4072, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4073, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4074, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4075, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4076, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4077, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4078, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4079, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4080, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4081, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4082, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4083, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4084, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4085, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4086, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4087, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4088, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4089, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4090, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4091, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4092, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4093, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4094, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4095, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4096, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4097, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4098, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4099, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4100, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4101, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4102, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4103, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4104, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4105, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4106, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4107, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4108, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4109, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4110, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4111, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4112, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4113, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4114, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4115, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4116, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4117, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4118, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4119, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4120, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4121, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4122, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4123, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4124, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4125, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4126, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4127, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4128, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4129, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4130, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4131, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4132, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4133, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4134, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4135, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4136, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4137, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4138, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4139, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4140, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4141, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4142, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4143, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4144, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4145, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4146, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4147, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4148, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4149, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4150, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4151, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4152, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4153, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4154, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4155, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4156, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4157, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4158, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4159, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4160, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4161, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4162, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4163, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4164, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4165, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4166, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4167, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4168, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4169, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4170, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4171, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4172, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4173, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4174, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4175, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4176, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4177, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4178, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4179, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4180, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4181, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4182, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4183, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4184, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4185, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4186, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4187, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4188, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4189, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4190, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4191, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4192, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4193, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4194, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4195, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4196, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4197, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4198, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4199, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4200, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4201, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4202, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4203, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4204, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4205, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4206, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4207, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4208, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4209, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4210, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4211, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4212, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4213, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4214, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4215, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4216, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4217, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4218, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4219, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4220, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4221, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4222, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4223, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4224, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4225, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4226, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4227, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4228, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4229, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4230, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4231, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4232, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4233, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4234, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4235, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4236, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4237, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4238, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4239, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4240, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4241, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4242, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4243, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4244, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4245, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4246, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4247, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4248, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4249, CAST(2.80 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4250, CAST(2.80 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4251, CAST(2.80 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4252, CAST(2.80 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4253, CAST(2.80 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4254, CAST(2.80 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4255, CAST(2.80 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4256, CAST(2.80 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4257, CAST(2.90 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4258, CAST(2.90 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4259, CAST(2.90 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4260, CAST(2.90 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4261, CAST(2.90 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4262, CAST(2.90 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4263, CAST(2.90 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4264, CAST(2.90 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4265, CAST(3.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4266, CAST(3.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4267, CAST(3.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4268, CAST(3.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4269, CAST(3.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4270, CAST(3.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4271, CAST(3.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4272, CAST(3.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4273, CAST(3.30 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4274, CAST(3.30 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4275, CAST(3.30 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4276, CAST(3.30 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4277, CAST(3.30 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4278, CAST(3.30 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4279, CAST(3.30 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4280, CAST(3.30 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4281, CAST(3.50 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4282, CAST(3.50 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4283, CAST(3.50 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4284, CAST(3.50 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4285, CAST(3.50 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4286, CAST(3.50 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4287, CAST(3.50 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4288, CAST(3.50 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4289, CAST(3.75 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4290, CAST(3.75 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4291, CAST(3.75 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4292, CAST(3.75 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4293, CAST(3.75 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4294, CAST(3.75 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4295, CAST(3.75 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4296, CAST(3.75 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4297, CAST(4.20 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4298, CAST(4.20 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4299, CAST(4.20 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4300, CAST(4.20 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4301, CAST(4.20 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4302, CAST(4.20 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4303, CAST(4.20 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4304, CAST(4.20 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4305, CAST(5.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4306, CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4307, CAST(5.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4308, CAST(5.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4309, CAST(5.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4310, CAST(5.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4311, CAST(5.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4312, CAST(5.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4313, CAST(6.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4314, CAST(6.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4315, CAST(6.00 AS Decimal(4, 2)), CAST(10.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4316, CAST(6.00 AS Decimal(4, 2)), CAST(11.50 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4317, CAST(6.00 AS Decimal(4, 2)), CAST(13.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4318, CAST(6.00 AS Decimal(4, 2)), CAST(16.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4319, CAST(6.00 AS Decimal(4, 2)), CAST(18.00 AS Decimal(4, 2)))
GO
INSERT [dbo].[ImplantMeasure] ([MeasureID], [DiameterMM], [LengthMM]) VALUES (4320, CAST(6.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(4, 2)))
GO
SET IDENTITY_INSERT [dbo].[ImplantMeasure] OFF
GO
SET IDENTITY_INSERT [dbo].[ImplantType] ON 
GO
INSERT [dbo].[ImplantType] ([TypeID], [TypeName], [IsSlim]) VALUES (1, N'BSI', 0)
GO
INSERT [dbo].[ImplantType] ([TypeID], [TypeName], [IsSlim]) VALUES (2, N'COMP', 1)
GO
SET IDENTITY_INSERT [dbo].[ImplantType] OFF
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (1, N'ABSCESS_DRAINAGE', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (2, N'ABUTMENT', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (3, N'APEXIFICATION', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (4, N'APICECTOMY', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (5, N'BUCCAL_SURFACE', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (6, N'BUILD_UP', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (7, N'BRIDGEMARK', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (8, N'BRIDGEL', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (9, N'BRIDGER', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (10, N'CLASS_1', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (11, N'CLASS_2_D', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (12, N'CLASS_2_M', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (13, N'CLASS_2_MOD', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (14, N'CLASS_3_D', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (15, N'CLASS_3_M', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (16, N'CLASS_4_D', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (17, N'CLASS_4_INCISAL', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (18, N'CLASS_4_M', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (19, N'CLASS_5', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (20, N'CRACK', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (21, N'CROWN', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (22, N'CROWN_FILL', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (23, N'CROWN_IMG', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (24, N'CROWN_LENGTHENING', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (25, N'CROWNS', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (26, N'DENTUREMARK', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (27, N'DIRECT_PULP_CAPPING', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (28, N'EXTRACTION', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (29, N'FIBER_POST', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (30, N'FISSURE_SEALENT', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (31, N'HEALING_CAP', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (32, N'HEMISECTION', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (33, N'IMPLANT', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (34, N'INDIRECT_PULP_CAPPING', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (35, N'METAL_POST', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (36, N'MTA_BULK_FLOW', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (37, N'PARTIAL_PULPOTOMY', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (38, N'PERIAPICAL_LESION', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (39, N'PULPOTOMY', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (40, N'RCT', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (41, N'ROOT_CARIES', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (42, N'SPACE_MAINTAINER', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (43, N'DENTUREMARK', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (44, N'DENTUREL', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (45, N'DENTURER', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (46, N'COMPOSITE STRIP', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Shapes] ([ShapeID], [ShapeName], [ShapeDetail], [OutID], [TopID], [INID], [ShapeColor]) VALUES (47, N'PRR', NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TblCities] ON 
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (2, N'قلقيلية')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (3, N'يافا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (4, N'جلجوليا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (6, N'بئر السبع')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (7, N'طولكرم')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (8, N'كفر قرع')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (13, N'جيت')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (15, N'كفر قاسم')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (16, N'اللد')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (17, N'الناصرة')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (20, N'باقه الحطب')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (21, N'الطيبه المثلث')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (23, N'بيت يام')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (24, N'اماتين ')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (26, N'الرملة')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (27, N'عزون')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (28, N'مغاره الظبعه')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (35, N'تايلند')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (37, N'رام الله')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (38, N'كفر برا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (40, N'جنين')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (42, N'كفر قدوم')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (43, N'بيت امين')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (44, N'الاردن')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (45, N'شفاعمر')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (47, N'عزبه الاشقر')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (48, N'حبله')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (51, N'نابلس')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (52, N'كفر لاقيف')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (55, N'اللنبي الياس')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (57, N'حجه')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (63, N'راس طيره')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (65, N'راس عطيه')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (73, N'عزبه سلمان')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (74, N'فرعطه')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (75, N'مردا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (76, N'الخليل')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (81, N'الفندق')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (82, N'كفر ثلث')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (84, N'الزرقاء')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (85, N'جين صافوت')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (88, N'عزون عتمه')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (89, N'الطيره المثلث')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (93, N'عرب الرمضين')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (96, N'عسله')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (101, N'جيوس')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (103, N'كفر زباد')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (106, N'بيتح تيكفا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (107, N'سنيريا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (108, N'نتانيا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (109, N'هرتسايا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (111, N'القدس')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (112, N'حولون')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (114, N'عطارة')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (115, N'طمرة')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (116, N'حبلة')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (117, N'عورتا')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (118, N'غزة')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (119, N'قلنسوة')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (120, N'بير السبع')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (121, N'رهط')
GO
INSERT [dbo].[TblCities] ([CityID], [CityName]) VALUES (122, N'المدور')
GO
SET IDENTITY_INSERT [dbo].[TblCities] OFF
GO
SET IDENTITY_INSERT [dbo].[TblMeasure] ON 
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (1, N'12')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (2, N'14')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (3, N'16')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (4, N'18')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (5, N'20')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (7, N'17X25')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (8, N'19X25')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (9, N'14X25')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (10, N'16X25')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (11, N'18X25')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (12, N'16X22')
GO
INSERT [dbo].[TblMeasure] ([MeasureID], [Measure]) VALUES (13, N'16x16')
GO
SET IDENTITY_INSERT [dbo].[TblMeasure] OFF
GO
SET IDENTITY_INSERT [dbo].[TblOtherTRT] ON 
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (1, N'Scaling and Polishing')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (2, N'Root Planning')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (3, N'Fluoride Varnish')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (4, N'Laser Bleaching (at clinic)')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (5, N'Home Bleaching')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (6, N'Diastema Closure')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (7, N'Space Maintainer')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (8, N'Gingivectomy')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (9, N'Crown Lengthening')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (10, N'Cyst Enucleation')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (11, N'Cyst Marsupialisation')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (12, N'Operculectomy')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (13, N'Labial Frenectomy')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (14, N'Buccal Frenectomy')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (15, N'Lingual Frenotomy')
GO
INSERT [dbo].[TblOtherTRT] ([TblOtherTrtID], [Trt]) VALUES (16, N'Lingual Frenectomy')
GO
SET IDENTITY_INSERT [dbo].[TblOtherTRT] OFF
GO
SET IDENTITY_INSERT [dbo].[TblTRT] ON 
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (37, N'Partial Pulpotomy')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (38, N'Pulpotomy')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (39, N'RCT')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (40, N'RCC')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (41, N'N\S (R C M)')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (42, N'RCO')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (43, N'RCF-Aalgam')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (44, N'RCF-COMP')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (45, N'Fiber Post')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (46, N'Metal Post')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (47, N'Composite Build Up')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (48, N'GI build up')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (49, N'Redo RCT')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (50, N'Redo Composite')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (51, N'Redo  Amalgam')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (52, N'TF(TemporaryFilling)')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (53, N'Fissure Sealent')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (54, N'Class1 comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (55, N'Class-2(m) comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (56, N'Class-2(D) comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (57, N'Class2(MOD) comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (58, N'Class3(M)comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (59, N'Class3(D) comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (60, N'Class4(M)comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (61, N'Class4(D)comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (62, N'Class-5 comp')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (63, N'Facing Direct Veneers')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (64, N'Diastima Closure')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (65, N'Indirect Veneerse')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (66, N'Indirect Pulpcapping')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (68, N'Direct Pulpcapping')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (69, N'Class-1 Amalg')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (70, N'Class-2(M) Amalg')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (71, N'Class-2(D) Amalg')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (72, N'Class-2(MOD) Amalg')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (73, N'Class-5 Amalg')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (74, N'EXTRA')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (75, N'Abcess Drainage')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (76, N'(SSC(Stainless Steel Crown')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (77, N'Space Maintainer')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (78, N'Temporary Acryl')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (79, N'Apexification')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (80, N'Apicectomy')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (81, N'Hemisection')
GO
INSERT [dbo].[TblTRT] ([TrtID], [Trt]) VALUES (82, N'crown lengthening')
GO
SET IDENTITY_INSERT [dbo].[TblTRT] OFF
GO
SET IDENTITY_INSERT [dbo].[TblTRTS] ON 
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (1, N'ABSCESS DRAINAGE', 0.0000, 1, NULL, NULL, N'ABSCESS DRAINAGE', N'0', N'ROOT', N'ALL', N'ALL', N'ABSCESS DRAINAGE', N'ABSCESS DRAINAGE', N'SURGERY', N'#80000000', N'#80000000', 0, 0, 1, N'#80000000', N'#80000000', 0)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (2, N'APEXIFICATION', 0.0000, 3, NULL, NULL, N'APEXIFICATION', N'3', N'ROOT', N'ALL', N'ALL', N'APEXIFICATION', N'APEXIFICATION', N'SURGERY', N'#80000000', N'#80000000', 2, 0, 2, N'#80000000', N'#80000000', 0)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (3, N'APICECTOMY', 0.0000, 4, NULL, NULL, N'APICECTOMY', N'1', N'ROOT', N'ALL', N'ALL', N'Apicectomy', N'APICECTOMY', N'SURGERY', N'#000000', N'#80000000', 1, 0, 3, N'#000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (4, N'BUCCAL SURFACE', 0.0000, 5, NULL, NULL, N'BUCCAL SURFACE', N'0', N'CROWN', N'ALL', N'ALL', N'BUCCAL SURFACE', NULL, NULL, N'#80000000', N'#80000000', 1, 0, 4, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (5, N'BUILD UP COM', 0.0000, 6, NULL, NULL, N'Build Up COMPOSITE', N'3', N'CROWN', N'ALL', N'ALL', N'Build Up COMPOSITE', N'BUILDING', N'CONSERVATIVE', N'#80000000', N'#80000000', 0, 0, 5, N'#80000000', N'#80000000', 0)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (6, N'BUILD UP ACR', 0.0000, 6, NULL, NULL, N'BUILD UP ACR', N'3', N'CROWN', N'ALL', N'ALL', N'Build Up ACRY ', N'BUILDING', N'CONSERVATIVE', N'#80C4BD97', N'#80000000', 1, 0, 6, N'#80C4BD97', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (7, N'BUILD UP GI', 0.0000, 6, NULL, NULL, N'BUILD UP GI', N'3', N'CROWN', N'ALL', N'ALL', N'Build Up GI', N'BUILDING', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 7, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (8, N'CLASS 1 AMALGAM', 0.0000, 10, NULL, NULL, N'CLASS 1 AMALGAM', N'0', N'CROWN', N'ALL', N'ALL', N'CLASS 1 AMALGAM', N'CLASS 1', N'CONSERVATIVE', N'#807F7F7F', N'#80000000', 1, 2, 8, N'#807F7F7F', N'#CC8E864C', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (9, N'CLASS 1 COMPOSITE', 0.0000, 10, NULL, NULL, N'CLASS 1 COMPOSITE', N'0', N'CROWN', N'ALL', N'4,5', N'Class1 comp', N'CLASS 1', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 9, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (10, N'CLASS 1 GI', 0.0000, 10, NULL, NULL, N'CLASS 1 GI', N'0', N'CROWN', N'ALL', N'4,5', N'Class1 GI', N'CLASS 1', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 10, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (11, N'CLASS 1 TF', 0.0000, 10, NULL, NULL, N'CLASS 1 TF', N'0', N'CROWN', N'ALL', N'4,5', N'Class1 TF', N'CLASS 1', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 11, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (12, N'CLASS 2 D AMALGAM', 0.0000, 11, NULL, NULL, N'CLASS 2 D AMALGAM', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'CLASS 2 D AMALGAM', N'CLASS 2', N'CONSERVATIVE', N'#807F7F7F', N'#80000000', 1, 0, 12, N'#807F7F7F', N'#808E864C', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (13, N'CLASS 2 D COMPOSITE', 0.0000, 11, NULL, NULL, N'CLASS 2 D COMPOSITE', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 D comp', N'CLASS 2', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 13, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (14, N'CLASS 2 D GI', 0.0000, 11, NULL, NULL, N'CLASS 2 D GI', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 D GI', N'CLASS 2', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 14, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (15, N'CLASS 2 D TF', 0.0000, 11, NULL, NULL, N'CLASS 2 D TF', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 D TF', N'CLASS 2', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 15, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (16, N'CLASS 2 M AMALGAM', 0.0000, 12, NULL, NULL, N'CLASS 2 M AMALGAM', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 M AMALG', N'CLASS 2', N'CONSERVATIVE', N'#807F7F7F', N'#80000000', 1, 2, 16, N'#807F7F7F', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (17, N'CLASS 2 M COMPOSITE', 0.0000, 12, NULL, NULL, N'CLASS 2 M COMPOSITE', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 M  COMP', N'CLASS 2', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 17, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (18, N'CLASS 2 M GI', 0.0000, 12, NULL, NULL, N'CLASS 2 M GI', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 M GI', N'CLASS 2', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 18, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (19, N'CLASS 2 M TF', 0.0000, 12, NULL, NULL, N'CLASS 2 M TF', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 M TF', N'CLASS 2', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 19, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (20, N'CLASS 2 MOD AMALGAM', 0.0000, 13, NULL, NULL, N'CLASS 2 MOD AMALGAM', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 MOD AMALG', N'CLASS 2', N'CONSERVATIVE', N'#807F7F7F', N'#80000000', 1, 2, 20, N'#807F7F7F', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (21, N'CLASS 2 MOD COMPOSITE', 0.0000, 13, NULL, NULL, N'CLASS 2 MOD COMPOSITE', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 MOD  COMP', N'CLASS 2', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 21, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (22, N'CLASS 2 MOD GI', 0.0000, 13, NULL, NULL, N'CLASS 2 MOD GI', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 MOD GI', N'CLASS 2', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 22, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (23, N'CLASS 2 MOD TF', 0.0000, 13, NULL, NULL, N'CLASS 2 MOD TF', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 MOD TF', N'CLASS 2', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 23, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (24, N'CLASS 3 D COMPOSITE', 0.0000, 14, NULL, NULL, N'CLASS 3 D COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 D  COMP', N'CLASS 3', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 24, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (25, N'CLASS 3 D GI', 0.0000, 14, NULL, NULL, N'CLASS 3 D GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 D GI', N'CLASS 3', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 25, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (26, N'CLASS 3 D TF', 0.0000, 14, NULL, NULL, N'CLASS 3 D TF', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 D TF', N'CLASS 3', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 26, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (27, N'CLASS 3 M COMPOSITE', 0.0000, 15, NULL, NULL, N'CLASS 3 M COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 M  COMP', N'CLASS 3', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 27, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (28, N'CLASS 3 M GI', 0.0000, 15, NULL, NULL, N'CLASS 3 M GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 M GI', N'CLASS 3', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 28, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (29, N'CLASS 3 M TF', 0.0000, 15, NULL, NULL, N'CLASS 3 M TF', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 M TF', N'CLASS 3', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 29, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (30, N'CLASS 4 D COMPOSITE', 0.0000, 16, NULL, NULL, N'CLASS 4 D COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 D  COMP', N'CLASS 4', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 30, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (31, N'CLASS 4 D GI', 0.0000, 16, NULL, NULL, N'CLASS 4 D GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 D GI', N'CLASS 4', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 31, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (32, N'CLASS 4 D TF', 0.0000, 16, NULL, NULL, N'CLASS 4 D TF', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 D TF', N'CLASS 4', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 32, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (33, N'CLASS 4 M COMPOSITE', 0.0000, 18, NULL, NULL, N'CLASS 4 M COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 M  COMP', N'CLASS 4', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 33, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (34, N'CLASS 4 M GI', 0.0000, 18, NULL, NULL, N'CLASS 4 M GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 M GI', N'CLASS 4', N'CONSERVATIVE', N'#80FFFFFF', N'#80000000', 1, 2, 34, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (35, N'CLASS 4 M TF', 0.0000, 18, NULL, NULL, N'CLASS 4 M TF', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 M TF', N'CLASS 4', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 35, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (36, N'CLASS 4 INCISAL', 0.0000, 17, NULL, NULL, N'CLASS 4 INCISAL', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'CLASS4  INCISAL', N'CLASS 4', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 36, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (37, N'CLASS 5 AMALGAM', 0.0000, 19, NULL, NULL, N'CLASS 5 AMALGAM', N'0', N'ROOT', N'ALL', N'4,5', N'Class5  AMALG', N'CLASS 5', N'CONSERVATIVE', N'#807F7F7F', N'#80000000', 1, 2, 37, N'#807F7F7F', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (38, N'CLASS 5 COMPOSITE', 0.0000, 19, NULL, NULL, N'CLASS 5 COMPOSITE', N'0', N'ROOT', N'ALL', N'ALL', N'Class5  COMP', N'CLASS 5', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 38, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (39, N'CLASS 5 GI', 0.0000, 19, NULL, NULL, N'CLASS 5 GI', N'0', N'ROOT', N'ALL', N'ALL', N'Class5 GI', N'CLASS 5', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 39, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (40, N'CLASS 5 TF', 0.0000, 19, NULL, NULL, N'CLASS 5 TF', N'0', N'ROOT', N'ALL', N'ALL', N'Class5 TF', N'CLASS 5', N'CONSERVATIVE', N'#80FFFF00', N'#80000000', 1, 2, 40, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (41, N'CRACK', 0.0000, 20, NULL, NULL, N'CRACK', N'0', N'BOTH', N'ALL', N'ALL', N'CRACK', N'DIAG', NULL, N'#80000000', N'#80000000', 1, 0, 41, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (42, N'CROWN LENGTHENING', 0.0000, 24, NULL, NULL, N'CROWN LENGTHENING', N'0', N'BOTH', N'ALL', N'ALL', N'CROWN LENGTHENING', N'C', N'SURGERY', N'#80FFFFFF', N'#80000000', 1, 0, 42, N'#80FFFFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (43, N'DIRECT PULP CAPPING', 0.0000, 27, NULL, NULL, N'DIRECT PULP CAPPING', N'0', N'ROOT', N'ALL', N'ALL', N'DIRECT PULPCAPPING', N'ENDO', N'ENDODONTICS', N'#801744FF', N'#80000000', 1, 0, 43, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (44, N'DENTUREMARK', 0.0000, 26, NULL, NULL, N'DENTUREMARK', N'8', N'BOTH', N'ALL', N'ALL', N'Diastima Closure', NULL, NULL, N'#93FFC719', N'#80000000', 1, 0, 44, N'#93FFC719', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (45, N'EXTRACTION', 0.0000, 28, NULL, NULL, N'EXTRACTION', N'4', N'BOTH', N'ALL', N'ALL', N'EXTRACTION', N'EXTRACTION', N'SURGERY', N'#80000000', N'#80000000', 1, 2, 45, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (46, N'FACING DIRECT VENEERS', 0.0000, 21, NULL, NULL, N'FACING DIRECT VENEERS', N'6', N'CROWN', N'ALL', N'ALL', N'FACING DIRECT VENEERS', N'DIRECT VENEERS', N'CONSERVATIVE', N'#80FAEBD7', N'#80000000', 1, 0, 46, N'#80FAEBD7', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (47, N'FIBER POST', 0.0000, 29, NULL, NULL, N'FIBER POST', N'0', N'BOTH', N'ALL', N'ALL', N'FIBER POST', N'POST', N'CONSERVATIVE', N'#80000000', N'#80000000', 1, 0, 47, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (48, N'FISSURE SEALENT', 0.0000, 30, NULL, NULL, N'FISSURE SEALENT', N'0', N'CROWN', N'ALL', N'4,5', N'FISSURE SEALENT', N'F', N'CONSERVATIVE', N'#80CCC1D9', N'#80000000', 1, 2, 48, N'#80CCC1D9', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (49, N'HEMISECTION', 0.0000, 32, NULL, NULL, N'HEMISECTION', N'2', N'ROOT', N'ALL', N'ALL', N'HEMISECTION', N'H', N'SURGERY', N'#FFE3EFFF', N'#80000000', 1, 0, 49, N'#FFE3EFFF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (50, N'IMPLANT', 0.0000, 33, NULL, NULL, N'IMPLANT', N'5', N'BOTH', N'ALL', N'ALL', N'IMPLANT', N'IMPLANT', N'SURGERY', N'#80000000', N'#80000000', 1, 0, 50, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (51, N'INDIRECT PULP CAPPING', 0.0000, 34, NULL, NULL, N'INDIRECT PULP CAPPING', N'0', N'ROOT', N'ALL', N'ALL', N'INDIRECT PULP CAPPING', N'PULP CAPPING', N'CONSERVATIVE', N'#8017E8FF', N'#80000000', 1, 2, 51, N'#8017E8FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (52, N'INDIRECT VENEERS', 0.0000, 21, NULL, NULL, N'INDIRECT VENEERS', N'6', N'CROWN', N'ALL', N'ALL', N'INDIRECT VENEERS', N'INDIRECT VENEERS', N'PROSTHODONTICS', N'#80E76F00', N'#80000000', 1, 0, 52, N'#80E76F00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (53, N'METAL POST', 0.0000, 35, NULL, NULL, N'METAL POST', N'0', N'BOTH', N'ALL', N'ALL', N'METAL POST', N'POST', N'CONSERVATIVE', N'#80000000', N'#80000000', 1, 0, 53, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (54, N'MTA BULK FLOW', 0.0000, 36, NULL, NULL, N'MTA BULK FLOW', N'0', N'MTA BULK FLOW', N'ALL', N'ALL', N'MTA BULK FLOW', NULL, NULL, N'#801744FF', N'#80000000', 1, 0, 54, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (55, N'PARTIAL PULPOTOMY', 0.0000, 37, NULL, NULL, N'PARTIAL PULPOTOMY', N'0', N'ROOT', N'ALL', N'ALL', N'PARTIAL PULPOTOMY', N'RCT', N'ENDODONTICS', N'#8092D050', N'#80000000', 1, 2, 55, N'#8092D050', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (56, N'PERIAPICAL LESION', 0.0000, 38, NULL, NULL, N'PERIAPICAL LESION', N'0', N'ROOT', N'ALL', N'ALL', N'PERIAPICAL LESION', N'P', NULL, N'#801744FF', N'#80000000', 1, 0, 56, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (57, N'PULPOTOMY', 0.0000, 39, NULL, NULL, N'PULPOTOMY', N'0', N'ROOT', N'ALL', N'3,4,5', N'PULPOTOMY', N'RCT', N'ENDODONTICS', N'#80F20C2E', N'#80000000', 1, 1, 57, N'#80F20C2E', N'#80000000', 2)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (58, N'RCC TF', 0.0000, 40, NULL, NULL, N'RCC TF', N'0', N'ROOT', N'ALL', N'ALL', N'RCC TF', N'RCT', N'ENDODONTICS', N'#80FFFF00', N'#80000000', 1, 0, 58, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (59, N'RCO TF', 0.0000, 40, NULL, NULL, N'RCO TF', N'0', N'ROOT', N'ALL', N'ALL', N'RCO TF', N'RCT', N'ENDODONTICS', N'#80F00000', N'#80000000', 1, 0, 59, N'#80F00000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (60, N'RCF AMALGAM', 0.0000, 40, NULL, NULL, N'RCF AMALGAM', N'0', N'ROOT', N'ALL', N'4,5', N'RCF AMALG', N'RCT', N'ENDODONTICS', N'#80706F6B', N'#80000000', 1, 2, 60, N'#80706F6B', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (61, N'RCF COMPOSITE', 0.0000, 40, NULL, NULL, N'RCF COMPOSITE', N'0', N'ROOT', N'ALL', N'ALL', N'RCF COMP', N'RCT', N'ENDODONTICS', N'#80C00000', N'#80000000', 1, 2, 61, N'#80C00000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (62, N'RCF GI', 0.0000, 40, NULL, NULL, N'RCF GI', N'0', N'ROOT', N'ALL', N'ALL', N'RCF GI', N'RCT', N'ENDODONTICS', N'#80FAC08F', N'#80000000', 1, 0, 62, N'#80FAC08F', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (63, N'REDO AMALGAM', 0.0000, 40, NULL, NULL, N'REDO AMALGAM', N'0', N'ROOT', N'ALL', N'1,2,3', N'REDO AMALG', N'RCT', N'ENDODONTICS', N'#80706F6B', N'#80000000', 1, 0, 63, N'#80706F6B', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (64, N'REDO COMPOSITE', 0.0000, 40, NULL, NULL, N'REDO COMPOSITE', N'0', N'ROOT', N'ALL', N'ALL', N'REDO COMP', N'RCT', N'ENDODONTICS', N'#80EEE8AA', N'#80000000', 1, 0, 64, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (65, N'REDO GI', 0.0000, 40, NULL, NULL, N'REDO GI', N'0', N'ROOT', N'ALL', N'ALL', N'REDO GI', N'RCT', N'ENDODONTICS', N'#80548DD4', N'#80000000', 1, 0, 65, N'#80548DD4', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (66, N'REDO RCT', 0.0000, 40, NULL, NULL, N'REDO RCT', N'0', N'ROOT', N'ALL', N'ALL', N'REDO RCT', N'RCT', N'ENDODONTICS', N'#80000000', N'#80000000', 1, 2, 66, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (67, N'RC MEDICAMENT TF', 0.0000, 40, NULL, NULL, N'RC MEDICAMENT TF', N'0', N'ROOT', N'ALL', N'ALL', N'N-S', N'RCT', N'ENDODONTICS', N'#80FFFF00', N'#80000000', 1, 2, 67, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (68, N'ROOT CARIES', 0.0000, 41, NULL, NULL, N'ROOT CARIES', N'0', N'ROOT', N'ALL', N'ALL', N'ROOT CARIES', N'RCT', N'ENDODONTICS', N'#801744FF', N'#80000000', 1, 0, 68, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (69, N'STAINLESS STEEL CROWN I', 0.0000, 23, NULL, NULL, N'STAINLESS STEEL CROWN', N'7', N'CROWN', N'ALL', N'4,5', N'SSC', N'CROWNS ON IMPLANT', N'PROSTHODONTICS', N'#8017E8FF', N'#80000000', 1, 2, 69, N'#8017E8FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (70, N'Space Maintainer', 0.0000, 42, NULL, NULL, N'Space Maintainer', N'0', N'Space Maintainer', N'ALL', N'ALL', N'Space Maintainer                                  ', NULL, NULL, N'#80000000', N'#80000000', 1, 0, 70, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (71, N'TEMPORARY CROWN I', 0.0000, 23, NULL, NULL, N'TEMPORARY CROWN', N'7', N'CROWN', N'ALL', N'ALL', N'TEMP CROWN', N'CROWNS ON IMPLANT', N'PROSTHODONTICS', N'#801744FF', N'#80000000', 1, 0, 71, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (72, N'TEMPORARY FILLING', 0.0000, 40, NULL, NULL, N'TEMPORARY FILLING', N'0', N'ROOT', N'ALL', N'ALL', N'TF', N'RCT', NULL, N'#80FFFF00', N'#80000000', 1, 2, 72, N'#80FFFF00', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (73, N'COMPOSITE STRIP', 0.0000, 46, NULL, NULL, N'COMPOSITE STRIP', N'0', N'COMPOSITE STRIP', N'ALL', N'1,2,3', N'COMPOSITE STRIP', NULL, NULL, N'#801744FF', N'#80000000', 1, 1, 73, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (74, N'PRR', 0.0000, 47, NULL, NULL, N'PRR', N'0', N'BOTH', N'ALL', N'4,5', N'PRR', NULL, N'PROSTHODONTICS', N'#801744FF', N'#80000000', 1, 1, 74, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (75, N'PULPECTOMY', 0.0000, 40, NULL, NULL, N'PULPECTOMY', N'0', N'ROOT', N'ALL', N'1,2,3', N'PULPECTOMY', N'RCT', N'ENDODONTICS', N'#80C0504D', N'#80000000', 1, 1, 75, N'#80C0504D', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (76, N'ZERCONIA CROWN I', 0.0000, 23, NULL, NULL, N'ZERCONIA CROWN', N'7', N'CROWN', N'ALL', N'ALL', N'ZERCONIA CROWN', N'CROWNS ON IMPLANT', N'PROSTHODONTICS', N'#806D7003', N'#80000000', 1, 0, 76, N'#806D7003', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (77, N'PFM CROWN I', 0.0000, 23, NULL, NULL, N'PFM CROWN', N'7', N'CROWN', N'ALL', N'ALL', N'PFM CROWN', N'CROWNS ON IMPLANT', N'PROSTHODONTICS', N'#80EEE8AA', N'#80000000', 1, 0, 77, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (78, N'EMAX CROWN I', 0.0000, 23, NULL, NULL, N'EMAX CROWN', N'7', N'CROWN', N'ALL', N'ALL', N'EMAX CROWN', N'CROWNS ON IMPLANT', N'PROSTHODONTICS', N'#80EEE8AA', N'#80000000', 1, 0, 78, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (79, N'HEALING CAP', 0.0000, 31, NULL, NULL, N'HEALING CAP', N'6', N'CROWN', N'ALL', N'ALL', N'HEALING CAP', N'IMPLANT COMPONENT', N'SURGERY', N'#80000000', N'#80000000', 1, 0, 79, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (80, N'ABUTMENT', 0.0000, 2, NULL, NULL, N'ABUTMENT', N'6', N'CROWN', N'ALL', N'ALL', N'ABUTMENT', N'IMPLANT COMPONENT', N'SURGERY', N'#80000000', N'#80000000', 1, 0, 80, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (81, N'CROWN', 0.0000, 21, NULL, NULL, N'CROWN', N'6', N'CROWN', N'ALL', N'ALL', N'CROWN', NULL, NULL, N'#80000000', N'#80000000', 1, 0, 81, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (82, N'CROWN_IMG', 0.0000, 23, NULL, NULL, N'CROWN_IMG', N'7', N'CROWN', N'ALL', N'ALL', N'CROWN', NULL, NULL, N'#80000000', N'#80000000', 1, 0, 82, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (83, N'BRIDGEMARK', 0.0000, 7, NULL, NULL, N'BRIDGEMARK', N'8', N'BOTH', N'ALL', N'ALL', N'BRIDGE', NULL, NULL, N'#80F469A2', N'#80000000', 1, 0, 83, N'#80F469A2', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (84, N'METAL CROWN I', 0.0000, 23, NULL, NULL, N'METAL CROWN', N'7', N'CROWN', N'ALL', N'ALL', N'METAL CROWN', N'CROWNS ON IMPLANT', N'PROSTHODONTICS', N'#807F7F7F', N'#80000000', 1, 0, 84, N'#807F7F7F', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (85, N'METAL BRIDGE', 0.0000, 23, NULL, NULL, N'METAL BRIDGE', N'8', N'CROWN', N'ALL', N'ALL', N'METAL BRIDGE', N'BRIDGE', N'PROSTHODONTICS', N'#809B9A9A', N'#80000000', 1, 0, 85, N'#809B9A9A', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (86, N'ZERCONIA BRIDGE', 0.0000, 23, NULL, NULL, N'ZERCONIA BRIDGE', N'8', N'CROWN', N'ALL', N'ALL', N'ZERCONIA BRIDGE', N'BRIDGE', N'PROSTHODONTICS', N'#80C6D9F0', N'#80000000', 1, 0, 86, N'#80C6D9F0', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (87, N'PFM BRIDGE', 0.0000, 23, NULL, NULL, N'PFM BRIDGE', N'8', N'CROWN', N'ALL', N'ALL', N'PFM BRIDGE', N'BRIDGE', N'PROSTHODONTICS', N'#80EEE8AA', N'#80000000', 1, 0, 87, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (88, N'EMAX BRIDGE', 0.0000, 23, NULL, NULL, N'EMAX BRIDGE', N'8', N'CROWN', N'ALL', N'ALL', N'EMAX BRIDGE', N'BRIDGE', N'PROSTHODONTICS', N'#80C6D9F0', N'#80000000', 1, 0, 88, N'#80C6D9F0', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (89, N'TEMP BRIDGE', 0.0000, 23, NULL, NULL, N'TEMP BRIDGE', N'8', N'CROWN', N'ALL', N'ALL', N'TEMP BRIDGE', N'BRIDGE', N'PROSTHODONTICS', N'#80000000', N'#80000000', 1, 0, 89, N'#80000000', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (90, N'COMPLETE DENTURE', 0.0000, 23, NULL, NULL, N'COMPLETE DENTURE', N'9', N'CROWN', N'ALL', N'ALL', N'CD', N'DENTURES', N'REMOVABLE APPLIANCES', N'#76B2A2C7', N'#80000000', 1, 0, 90, N'#76B2A2C7', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (91, N'METAL CROWN T', 0.0000, 23, NULL, NULL, N'METAL CROWN', N'3', N'CROWN', N'ALL', N'ALL', N'METAL CROWN', N'CROWNS ON TOOTH', N'PROSTHODONTICS', N'#807F7F7F', N'#80000000', 1, 0, 84, N'#807F7F7F', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (92, N'ZERCONIA CROWN T', 0.0000, 23, NULL, NULL, N'ZERCONIA CROWN', N'3', N'CROWN', N'ALL', N'ALL', N'ZERCONIA CROWN', N'CROWNS ON TOOTH', N'PROSTHODONTICS', N'#80C6D9F0', N'#80000000', 1, 0, 76, N'#80C6D9F0', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (93, N'PFM CROWN T', 0.0000, 23, NULL, NULL, N'PFM CROWN', N'3', N'CROWN', N'ALL', N'ALL', N'PFM CROWN', N'CROWNS ON TOOTH', N'PROSTHODONTICS', N'#80EEE8AA', N'#80000000', 1, 0, 77, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (94, N'EMAX CROWN T', 0.0000, 23, NULL, NULL, N'EMAX CROWN', N'3', N'CROWN', N'ALL', N'ALL', N'EMAX CROWN', N'CROWNS ON TOOTH', N'PROSTHODONTICS', N'#80C6D9F0', N'#80000000', 1, 0, 78, N'#80C6D9F0', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (95, N'STAINLESS STEEL CROWN T', 0.0000, 23, NULL, NULL, N'STAINLESS STEEL CROWN', N'3', N'CROWN', N'ALL', N'4,5', N'SSC', N'CROWNS ON TOOTH', N'PROSTHODONTICS', N'#807F7F7F', N'#80000000', 1, 2, 69, N'#807F7F7F', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (96, N'TEMPORARY CROWN T', 0.0000, 23, NULL, NULL, N'TEMPORARY CROWN', N'3', N'CROWN', N'ALL', N'ALL', N'TEMP CROWN', N'CROWNS ON TOOTH', N'PROSTHODONTICS', N'#801744FF', N'#80000000', 1, 0, 71, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (97, N'REMOVABLE PARTIAL DENTURE', 0.0000, 23, NULL, NULL, N'REMOVABLE PARTIAL DENTURE', N'9', N'CROWN', N'ALL', N'ALL', N'RPD', N'DENTURES', N'REMOVABLE APPLIANCES', N'#5EDBEEE3', N'#80000000', 1, 0, 91, N'#5EDBEEE3', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (98, N'REDO CLASS 1 COMPOSITE', 0.0000, 10, N'0', N'0', N'CLASS 1 COMPOSITE', N'0', N'CROWN', N'ALL', N'4,5', N'Class1 comp', N'CLASS 1', N'CONSERVATIVE', N'#CC8E864C', N'#80000000', 1, 2, 9, N'#CC8E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (99, N'REDO CLASS 1 AMALGAM', 0.0000, 10, N'0', N'0', N'CLASS 1 AMALGAM', N'0', N'CROWN', N'ALL', N'ALL', N'Class-1 Amalg', N'CLASS 1', N'CONSERVATIVE', N'#CC8E864C', N'#CC8E864C', 1, 2, 8, N'#CC8E864C', N'#CC8E864C', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (100, N'REDO CLASS 1 GI', 0.0000, 10, N'0', N'0', N'CLASS 1 GI', N'0', N'CROWN', N'ALL', N'4,5', N'Class1 GI', N'CLASS 1', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 10, N'#80CECAAB', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (101, N'REDO CLASS 2 D AMALGAM', 0.0000, 11, N'0', N'0', N'CLASS 2 D AMALGAM', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class-2(D) Amalg', N'CLASS 2', N'CONSERVATIVE', N'#808E864C', N'#808E864C', 1, 0, 12, N'#808E864C', N'#808E864C', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (102, N'REDO CLASS 2 D COMPOSITE', 0.0000, 11, N'0', N'0', N'CLASS 2 D COMPOSITE', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class-2(D) comp', N'CLASS 2', N'CONSERVATIVE', N'#808E864C', N'#80000000', 1, 2, 13, N'#808E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (103, N'REDO CLASS 2 D GI', 0.0000, 11, N'0', N'0', N'CLASS 2 D GI', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 D GI', N'CLASS 2', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 14, N'#80CECAAB', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (104, N'REDO CLASS 2 M AMALGAM', 0.0000, 12, N'0', N'0', N'CLASS 2 M AMALGAM', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class-2(M) Amalg', N'CLASS 2', N'CONSERVATIVE', N'#80706F6B', N'#80000000', 1, 2, 16, N'#80706F6B', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (105, N'REDO CLASS 2 M COMPOSITE', 0.0000, 12, N'0', N'0', N'CLASS 2 M COMPOSITE', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class-2(m) comp', N'CLASS 2', N'CONSERVATIVE', N'#808E864C', N'#80000000', 1, 2, 17, N'#808E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (106, N'REDO CLASS 2 M GI', 0.0000, 12, N'0', N'0', N'CLASS 2 M GI', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 M GI', N'CLASS 2', N'CONSERVATIVE', N'#80548DD4', N'#80000000', 1, 2, 18, N'#80548DD4', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (107, N'REDO CLASS 2 MOD AMALGAM', 0.0000, 13, N'0', N'0', N'CLASS 2 MOD AMALGAM', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class-2(MOD) Amalg', N'CLASS 2', N'CONSERVATIVE', N'#80706F6B', N'#80000000', 1, 2, 20, N'#80706F6B', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (108, N'REDO CLASS 2 MOD COMPOSITE', 0.0000, 13, N'0', N'0', N'CLASS 2 MOD COMPOSITE', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2(MOD) comp', N'CLASS 2', N'CONSERVATIVE', N'#80EEE8AA', N'#80000000', 1, 2, 21, N'#80EEE8AA', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (109, N'REDO CLASS 2 MOD GI', 0.0000, 13, N'0', N'0', N'CLASS 2 MOD GI', N'0', N'CROWN', N'4,5,6,7,8', N'4,5', N'Class2 MOD GI', N'CLASS 2', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 22, N'#80CECAAB', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (110, N'REDO CLASS 3 D COMPOSITE', 0.0000, 14, N'0', N'0', N'CLASS 3 D COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3(D) comp', N'CLASS 3', N'CONSERVATIVE', N'#808E864C', N'#80000000', 1, 2, 24, N'#808E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (111, N'REDO CLASS 3 D GI', 0.0000, 14, N'0', N'0', N'CLASS 3 D GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 D GI', N'CLASS 3', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 25, N'#80CECAAB', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (112, N'REDO CLASS 3 M COMPOSITE', 0.0000, 15, N'0', N'0', N'CLASS 3 M COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3(M)comp', N'CLASS 3', N'CONSERVATIVE', N'#808E864C', N'#80000000', 1, 2, 27, N'#808E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (113, N'REDO CLASS 3 M GI', 0.0000, 15, N'0', N'0', N'CLASS 3 M GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class3 M GI', N'CLASS 3', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 28, N'#80CECAAB', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (114, N'REDO CLASS 4 D COMPOSITE', 0.0000, 16, N'0', N'0', N'CLASS 4 D COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4(D)comp', N'CLASS 4', N'CONSERVATIVE', N'#808E864C', N'#80000000', 1, 2, 30, N'#808E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (115, N'REDO CLASS 4 D GI', 0.0000, 16, N'0', N'0', N'CLASS 4 D GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 D GI', N'CLASS 4', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 31, N'#80CECAAB', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (116, N'REDO CLASS 4 M COMPOSITE', 0.0000, 18, N'0', N'0', N'CLASS 4 M COMPOSITE', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4(M)comp', N'CLASS 4', N'CONSERVATIVE', N'#808E864C', N'#80000000', 1, 2, 33, N'#808E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (117, N'REDO CLASS 4 M GI', 0.0000, 18, N'0', N'0', N'CLASS 4 M GI', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'Class4 M GI', N'CLASS 4', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 34, N'#80CECAAB', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (118, N'REDO CLASS 4 INCISAL', 0.0000, 17, N'0', N'0', N'CLASS 4 INCISAL', N'0', N'CROWN', N'1,2,3', N'1,2,3', N'CLASS4  INCISAL', N'CLASS 4', N'CONSERVATIVE', N'#801744FF', N'#80000000', 1, 2, 36, N'#801744FF', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (119, N'REDO CLASS 5 AMALGAM', 0.0000, 19, N'0', N'0', N'CLASS 5 AMALGAM', N'0', N'CROWN', N'ALL', N'4,5', N'Class-5 Amalg', N'CLASS 5', N'CONSERVATIVE', N'#80706F6B', N'#80000000', 1, 2, 37, N'#80706F6B', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (120, N'REDO CLASS 5 COMPOSITE', 0.0000, 19, N'0', N'0', N'CLASS 5 COMPOSITE', N'0', N'CROWN', N'ALL', N'ALL', N'Class-5 comp', N'CLASS 5', N'CONSERVATIVE', N'#808E864C', N'#80000000', 1, 2, 38, N'#808E864C', N'#80000000', 1)
GO
INSERT [dbo].[TblTRTS] ([TrtID], [Trt], [TrtValue], [ShapeID], [TrtAr], [TrtArDetails], [TrtDetails], [TrtLVL], [TrtLoc], [ToothID], [ToothIDkID], [OldTrt], [TrtGroup], [ParentGroup], [TrtColor], [TrtBrdrClr], [TrtBrdrThick], [KidTrt], [TrtClrID], [DefFillColor], [DefBrdrColor], [DefBrdrThick]) VALUES (121, N'REDO CLASS 5 GI', 0.0000, 19, N'0', N'0', N'CLASS 5 GI', N'0', N'CROWN', N'ALL', N'ALL', N'Class5 GI', N'CLASS 5', N'CONSERVATIVE', N'#80CECAAB', N'#80000000', 1, 2, 39, N'#80CECAAB', N'#80000000', 1)
GO
SET IDENTITY_INSERT [dbo].[TblTRTS] OFF
GO
SET IDENTITY_INSERT [dbo].[TblWireType] ON 
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (1, N'Niti Lower')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (2, N'Niti Upper')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (3, N'Stainless Lower')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (4, N'Stainless Upper')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (6, N'Self-Ligating Upper')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (7, N'Self-Ligating Lower')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (8, N'ELASTIC 3\16')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (9, N'ELATIC1\8')
GO
INSERT [dbo].[TblWireType] ([TypeID], [WireType]) VALUES (10, N'ELASTIC1\4')
GO
SET IDENTITY_INSERT [dbo].[TblWireType] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitTypes] ON 
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (1, N'Routine Check-Ups and Cleanings', N'الفحوصات والتنظيفات الروتينية')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (2, N'X-ray and Diagnostic Exams', N'الأشعة السينية والفحوصات التشخيصية')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (3, N'Pediatric Dental Appointments', N'مواعيد طب أسنان الأطفال')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (4, N'Orthodontic Consultations and Adjustments', N'استشارات وتعديلات تقويم الأسنان')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (5, N'Periodontal Maintenance and Treatment', N'صيانة اللثة وعلاجها')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (6, N'Cosmetic Dentistry Consultations', N'استشارات طب الأسنان التجميلي')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (7, N'Oral Surgery Consultations', N'استشارات جراحة الفم')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (8, N'Prosthodontic Appointments', N'مواعيد طب الأسنان التعويضي')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (9, N'Endodontic Appointments (Root Canal Treatment)', N'مواعيد علاج لب الأسنان (علاج قناة الجذر)')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (10, N'Specialized Appointments (Sleep Apnea, TMJ, etc.)', N'المواعيد المتخصصة (انقطاع النفس النومي، مفصل الفك الصدغي، الخ.)')
GO
INSERT [dbo].[VisitTypes] ([VtID], [VisitType], [VisitTypeAr]) VALUES (11, N'Emergency Dental Visits', N'زيارات طب الأسنان الطارئة')
GO
SET IDENTITY_INSERT [dbo].[VisitTypes] OFF
GO
