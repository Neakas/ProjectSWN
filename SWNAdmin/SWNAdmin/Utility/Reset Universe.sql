Delete from InnerMoonlets
Delete from MajorMoons
Delete from OuterMoonlets
Delete from Planets
Delete from Stars
Delete from StarSystems

DBCC CHECKIDENT('InnerMoonlets', RESEED, 0)
DBCC CHECKIDENT('OuterMoonlets', RESEED, 0)
DBCC CHECKIDENT('MajorMoons', RESEED, 0)
DBCC CHECKIDENT('Planets', RESEED, 0)
DBCC CHECKIDENT('Stars', RESEED, 0)
DBCC CHECKIDENT('StarSystems', RESEED, 0)