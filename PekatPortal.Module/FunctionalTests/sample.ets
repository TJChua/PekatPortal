;#DropDB PekatPortalEasyTest

#Application PekatPortal.Win
#Application PekatPortal.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef PekatPortal.Win

*Action About...
 
*Action OK

#EndIf

#IfDef PekatPortal.Web
*Action Navigation(My Details)
#EndIf
