namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

	public partial class IconInfo {

		[DllImport("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_icon_info_get_attach_points(IntPtr raw, out IntPtr points, out int n_points);

		public bool GetAttachPoints (out Gdk.Point[] points)
		{
			IntPtr pointsPtr;
			int count;

			bool ret = gtk_icon_info_get_attach_points (Handle, out pointsPtr, out count);

			points = new Gdk.Point[count];
			for (int i = 0; i < count; i++) {
				points[i] = Gdk.Point.New (Marshal.ReadIntPtr ((IntPtr)((long) pointsPtr + i * IntPtr.Size)));
			}

			GLib.Marshaller.Free (pointsPtr);
			return ret;
		}
	}
}
