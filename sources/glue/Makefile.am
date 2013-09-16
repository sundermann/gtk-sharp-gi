lib_LTLIBRARIES = libgtksharpglue-3.8.2.la

libgtksharpglue_3_8_2_la_SOURCES =

nodist_libgtksharpglue_3_8_2_la_SOURCES = generated.c

libgtksharpglue_3_8_2_la_LDFLAGS = -module -avoid-version -no-undefined

libgtksharpglue_3_8_2_la_LIBADD = $(GTK_LIBS)

INCLUDES = $(GTK_CFLAGS) -I$(top_srcdir)

libgtksharpglue.dll: $(libgtksharpglue_3_8_2_la_OBJECTS) libgtksharpglue.rc libgtksharpglue.def
	./build-dll libgtksharpglue $(VERSION)

CLEANFILES = lib*.a lib*.dll

EXTRA_DIST =
