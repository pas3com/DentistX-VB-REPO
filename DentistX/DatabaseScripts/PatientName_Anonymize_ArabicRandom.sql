/*
  Anonymize dbo.Patient: unique 4-part Arabic-style dummy names + Sex + Age/BirthY.
  - PatientName is UNIQUE: each patient gets a distinct tuple (k1..k4) on fixed lists (no random picks).
  - Part 1: first ~half of patients = male first names; other half = female (split rounds fairly).
  - Parts 2–4: same (distinct) middle/family lists for all rows. Always four parts.
  - Sex: N'ذكر' / N'أنثى' per first list. BirthY: random age 5..60; Age = YEAR(GETDATE()) - BirthY.
  - Assignment is shuffled (ROW_NUMBER by NEWID), not in PatientID order.
  - Index order (mixed radix): the *last* name part (column 4) changes most often, like the first digit in an odometer;
    the first part changes slowest. This avoids the bug where the old order kept k4=0 for all 1000 rows (last part always the same).
  - Requires: CEILING(tot/2) <= (L1m*L2*L3*L4) and FLOOR(tot/2) <= (L1f*L2*L3*L4) with the listed sizes.
  Run on a backup or copy first. Adjust dbo if needed.
*/
SET NOCOUNT ON;
-- BEGIN TRANSACTION;

;WITH
RawFirstMale AS (SELECT s FROM (VALUES
    (N'محمد'),(N'أحمد'),(N'على'),(N'خالد'),(N'سعد'),(N'ياسر'),(N'ماجد'),
    (N'باسل'),(N'وائل'),(N'فهد'),(N'سامى'),(N'عمر'),(N'زياد'),(N'رامى'),
    (N'نادر'),(N'عادل'),(N'كريم'),(N'جمال'),(N'طارق'),(N'ناصر'),(N'هشام'),
    (N'عماد'),(N'منير'),(N'شادى'),(N'وصفي'),(N'جابر'),(N'لؤى'),(N'صهيب'),
    (N'رافع'),(N'حاتم'),(N'مروان'),(N'رامي'),(N'فارس'),(N'ماهر'),
    (N'ضياء'),(N'مؤمن'),(N'رعد'),(N'براء'),(N'أنس'),(N'يحيى'),(N'تامر')
) v(s)),
FirstMale AS (SELECT s, ROW_NUMBER() OVER (ORDER BY s) - 1 AS k FROM (SELECT DISTINCT s FROM RawFirstMale) d),
RawFirstFemale AS (SELECT s FROM (VALUES
    (N'فاطمة'),(N'مريم'),(N'نورة'),(N'سارة'),(N'ليلى'),(N'هند'),(N'سعاد'),
    (N'باسمة'),(N'دينا'),(N'لينا'),(N'رانيا'),(N'منى'),(N'هبة'),(N'نجلاء'),
    (N'أمل'),(N'رنا'),(N'غادة'),(N'سلمى'),(N'ياسمين'),(N'شيرين'),(N'إيمان'),
    (N'نادية'),(N'لانا'),(N'ريم'),(N'رشا'),(N'وفاء'),(N'مها'),(N'دعاء'),
    (N'يارا'),(N'جيهان'),(N'نرمين'),(N'آمنة'),(N'ميرفت'),(N'علياء'),
    (N'رغد'),(N'لطيفة'),(N'سمر'),(N'هاجر'),(N'رحاب')
) v(s)),
FirstFemale AS (SELECT s, ROW_NUMBER() OVER (ORDER BY s) - 1 AS k FROM (SELECT DISTINCT s FROM RawFirstFemale) d),
RawSecond AS (SELECT s FROM (VALUES
    (N'عبدالله'),(N'عبدالرحمن'),(N'يوسف'),(N'إبراهيم'),(N'حسن'),(N'حسام'),
    (N'محمود'),(N'ناصر'),(N'طارق'),(N'وائل'),(N'ياسر'),(N'رامي'),(N'كريم'),
    (N'سامر'),(N'ماهر'),(N'رافد'),(N'زياد'),(N'بدر'),(N'مازن'),(N'لؤي'),
    (N'رعد'),(N'ضياء'),(N'عماد'),(N'فاروق'),(N'مالك'),
    (N'عصام'),(N'وضاح'),(N'شهاب'),(N'غسان'),(N'حازم'),(N'ليث'),
    (N'نزار'),(N'جمال'),(N'صالح'),(N'هيثم'),
    (N'مروان'),(N'واصف'),(N'نائل'),(N'حاتم')
) v(s)),
SecondMale AS (SELECT s, ROW_NUMBER() OVER (ORDER BY s) - 1 AS k FROM (SELECT DISTINCT s FROM RawSecond) d),
RawThird AS (SELECT s FROM (VALUES
    (N'سالم'),(N'كريم'),(N'وائل'),(N'ناصر'),(N'حسن'),(N'فادي'),(N'رامي'),
    (N'ماجد'),(N'باسل'),(N'عمرو'),(N'طارق'),(N'سعد'),(N'نادر'),
    (N'رافع'),(N'زهير'),(N'وضاح'),(N'شادى'),(N'لؤى'),(N'فهد'),
    (N'سامي'),(N'هشام'),(N'رعد'),(N'ضياء'),(N'ياسر'),(N'مروان'),
    (N'كمال'),(N'نواف'),(N'راشد'),(N'رافد'),(N'صالح'),
    (N'مازن'),(N'ليث'),(N'وافي'),(N'زياد'),(N'عماد'),(N'نائل'),
    (N'فارس'),(N'بدر'),(N'وصفي'),(N'نزار'),(N'جابر')
) v(s)),
ThirdMale AS (SELECT s, ROW_NUMBER() OVER (ORDER BY s) - 1 AS k FROM (SELECT DISTINCT s FROM RawThird) d),
RawFourth AS (SELECT s FROM (VALUES
    (N'العجمى'),(N'المصرى'),(N'الشامى'),(N'الحسن'),(N'النمر'),(N'عثمان'),
    (N'عادل'),(N'السيد'),(N'عبدالغنى'),(N'عبدالحميد'),(N'عبدالعزيز'),
    (N'عبدالملك'),(N'عبدالفتاح'),(N'عبدالهادى'),(N'عبدالمنعم'),
    (N'عبدالجبار'),(N'عبدالسلام'),(N'عبدالوهاب'),(N'عبدالرحيم'),
    (N'عبدالكريم'),(N'عبدالمجيد'),(N'عبدالستار'),(N'عبدالخالق'),
    (N'عبدالعالى'),(N'عبدالغفار'),(N'عبدالودود'),(N'عبدالغفور'),
    (N'عبداللطيف'),(N'عبدالناصر'),(N'عبدالشكور'),(N'عبدالشافى'),
    (N'الخطيب'),(N'السورى'),(N'العراقى'),(N'اللبنانى'),(N'الفلسطينى')
) v(s)),
FourthMale AS (SELECT s, ROW_NUMBER() OVER (ORDER BY s) - 1 AS k FROM (SELECT DISTINCT s FROM RawFourth) d),
Lens AS (
    SELECT
        (SELECT MAX(k) + 1 FROM FirstMale)   AS l1m,
        (SELECT MAX(k) + 1 FROM FirstFemale) AS l1f,
        (SELECT MAX(k) + 1 FROM SecondMale)  AS l2,
        (SELECT MAX(k) + 1 FROM ThirdMale)   AS l3,
        (SELECT MAX(k) + 1 FROM FourthMale)  AS l4
),
Shuf AS (
    SELECT
        p.PatientID,
        ROW_NUMBER() OVER (ORDER BY NEWID(), p.PatientID) AS rn,
        COUNT(*) OVER () AS tot
    FROM dbo.Patient AS p
),
Slot AS (
    SELECT
        s.PatientID,
        s.tot,
        s.rn,
        (s.tot + 1) / 2                                 AS n_m,
        s.tot - (s.tot + 1) / 2                         AS n_f,
        CASE WHEN s.rn <= (s.tot + 1) / 2 THEN 1 ELSE 0 END AS is_m,
        CASE WHEN s.rn <= (s.tot + 1) / 2
             THEN s.rn - 1
             ELSE s.rn - 1 - (s.tot + 1) / 2
        END AS rem
    FROM Shuf AS s
),
Ix AS (
    SELECT
        sl.PatientID,
        sl.is_m,
        sl.rem,
        l.l1m,
        l.l1f,
        l.l2,
        l.l3,
        l.l4
    FROM Slot AS sl
    CROSS JOIN Lens AS l
),
Pick AS (
    SELECT
        x.PatientID,
        x.is_m,
        /* l4, l3, l2, l1: last part (family-style) varies fastest; first name varies slowest. */
        x.rem % x.l4 AS k4,
        (x.rem / x.l4) % x.l3 AS k3,
        (x.rem / x.l4 / x.l3) % x.l2 AS k2,
        (x.rem / x.l4 / x.l3 / x.l2)
            % (CASE x.is_m WHEN 1 THEN x.l1m ELSE x.l1f END) AS k1,
        YEAR(GETDATE()) - 5 - (ABS(CHECKSUM(NEWID())) % 56) AS byear
    FROM Ix AS x
),
X AS (
    SELECT
        pk.PatientID,
        pk.is_m,
        pk.byear,
        COALESCE(m.s, w.s) AS p1,
        f2.s AS p2,
        f3.s AS p3,
        f4.s AS p4
    FROM Pick AS pk
    LEFT JOIN FirstMale  AS m ON m.k = pk.k1 AND pk.is_m = 1
    LEFT JOIN FirstFemale AS w ON w.k = pk.k1 AND pk.is_m = 0
    INNER JOIN SecondMale AS f2 ON f2.k = pk.k2
    INNER JOIN ThirdMale  AS f3 ON f3.k = pk.k3
    INNER JOIN FourthMale AS f4 ON f4.k = pk.k4
    WHERE COALESCE(m.s, w.s) IS NOT NULL
)
UPDATE p
SET PatientName = x.p1 + N' ' + x.p2 + N' ' + x.p3 + N' ' + x.p4
  , Sex     = CASE WHEN x.is_m = 1 THEN N'ذكر' ELSE N'أنثى' END
  , BirthY  = x.byear
  , Age     = YEAR(GETDATE()) - x.byear
FROM dbo.Patient AS p
INNER JOIN X AS x ON x.PatientID = p.PatientID;

-- COMMIT TRANSACTION;
-- ROLLBACK TRANSACTION;

-- Optional: confirm no duplicate display names
-- SELECT PatientName, COUNT(*) AS c FROM dbo.Patient GROUP BY PatientName HAVING COUNT(*) > 1;
