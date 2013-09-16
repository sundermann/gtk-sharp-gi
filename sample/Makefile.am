SUBDIRS =

DOTNET_TARGETS=
DOTNET_ASSEMBLY=

TARGETS = gtk-hello-world.exe async-sample.exe button.exe calendar.exe menu.exe treeviewdemo.exe managedtreeviewdemo.exe nodeviewdemo.exe treemodeldemo.exe actions.exe spawn.exe registerprop.exe gexceptiontest.exe

DEBUGS = $(addsuffix .mdb, $(TARGETS))

assemblies = $(top_builddir)/out/gtk-sharp.dll 

references=$(addprefix -r:, $(assemblies))
references += $(MONOCAIRO_LIBS) $(GDKSHARP_LIBS) $(GLIBSHARP_LIBS) $(ATKSHARP_LIBS) $(PANGOSHARP_LIBS)

noinst_SCRIPTS = $(TARGETS)
CLEANFILES = $(TARGETS) $(DEBUGS)

gtk-hello-world.exe: $(srcdir)/HelloWorld.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:gtk-hello-world.exe $(references) $(srcdir)/HelloWorld.cs

async-sample.exe: $(srcdir)/AsyncSample.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:async-sample.exe $(references) $(srcdir)/AsyncSample.cs

button.exe: $(srcdir)/ButtonApp.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:button.exe $(references) $(srcdir)/ButtonApp.cs

calendar.exe: $(srcdir)/CalendarApp.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:calendar.exe $(references) $(srcdir)/CalendarApp.cs

menu.exe: $(srcdir)/Menu.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:menu.exe $(references) $(srcdir)/Menu.cs

scribble-xinput.exe: $(srcdir)/ScribbleXInput.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:scribble-xinput.exe $(references) $(srcdir)/ScribbleXInput.cs

treeviewdemo.exe: $(srcdir)/TreeViewDemo.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:treeviewdemo.exe $(references) $(srcdir)/TreeViewDemo.cs

managedtreeviewdemo.exe: $(srcdir)/ManagedTreeViewDemo.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:managedtreeviewdemo.exe $(references) $(srcdir)/ManagedTreeViewDemo.cs

nodeviewdemo.exe: $(srcdir)/NodeViewDemo.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:nodeviewdemo.exe $(references) $(srcdir)/NodeViewDemo.cs

treemodeldemo.exe: $(srcdir)/TreeModelDemo.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:treemodeldemo.exe $(references) $(srcdir)/TreeModelDemo.cs

dotnet_references = $(references) $(addprefix -r:, $(DOTNET_ASSEMBLY)) -r:System.Drawing.dll

drawing-sample.exe: $(srcdir)/DrawingSample.cs $(assemblies) $(DOTNET_ASSEMBLIES)
	$(CSC) $(CSFLAGS) -out:drawing-sample.exe $(dotnet_references) $(srcdir)/DrawingSample.cs

actions.exe: $(srcdir)/Actions.cs
	$(CSC) $(CSFLAGS) -unsafe -out:actions.exe $(references) $(srcdir)/Actions.cs

spawn.exe: $(srcdir)/SpawnTests.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:spawn.exe $(references) $(srcdir)/SpawnTests.cs

registerprop.exe: $(srcdir)/PropertyRegistration.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:registerprop.exe $(references) $(srcdir)/PropertyRegistration.cs

gexceptiontest.exe: $(srcdir)/GExceptionTest.cs $(assemblies)
	$(CSC) $(CSFLAGS) -out:gexceptiontest.exe $(references) $(srcdir)/GExceptionTest.cs

EXTRA_DIST = 				\
	HelloWorld.cs			\
	Assistant.cs			\
	AsyncSample.cs			\
	ButtonApp.cs			\
	CalendarApp.cs			\
	Subclass.cs			\
	Menu.cs				\
	Scribble.cs			\
	ScribbleXInput.cs		\
	SpawnTests.cs			\
	TreeModelDemo.cs		\
	TreeViewDemo.cs			\
	ManagedTreeViewDemo.cs		\
	NativeInstantiationTest.cs	\
	NodeViewDemo.cs			\
	GExceptionTest.cs		\
	CairoSample.cs			\
	TestDnd.cs			\
	CustomCellRenderer.cs		\
	DrawingSample.cs		\
	CustomWidget.cs			\
	CustomScrollableWidget.cs	\
	Actions.cs			\
	PropertyRegistration.cs		\
	PolarFixed.cs

