AC_DEFUN([CHECK_ATKSHARP],
[
        PKG_CHECK_MODULES(ATKSHARP, atk-sharp-1.0)
        AC_SUBST(ATK_LIBS)
])