// TextBuffer.cs - customizations to Gtk.TextBuffer.
// 
// Authors:  Mike Kestner  <mkestner@ximian.com>
//
// Copyright (c) 2004-2006  Novell, Inc.
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of version 2 of the Lesser GNU General 
// Public License as published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this program; if not, write to the
// Free Software Foundation, Inc., 59 Temple Place - Suite 330,
// Boston, MA 02111-1307, USA.

namespace Gtk {

	using System;
	using System.Runtime.InteropServices;

	public partial class TextBuffer {

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_text_buffer_set_text (IntPtr raw, IntPtr text, int len);

		public void Clear ()
		{
			Gtk.TextIter start = StartIter, end = EndIter;
			Delete (start, end);
		}

		// overload to paste clipboard contents at cursor editable by default.
		public void PasteClipboard (Gtk.Clipboard clipboard)
		{
			gtk_text_buffer_paste_clipboard(Handle, clipboard.Handle, IntPtr.Zero, true);
		}

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_text_buffer_insert (IntPtr raw, ref Gtk.TextIter iter, IntPtr text, int len);

		[Obsolete ("Replaced by 'ref TextIter iter' overload")]
		public void Insert (TextIter iter, string text)
		{
			Insert (ref iter, text);
		}

		public void Insert (ref Gtk.TextIter iter, string text)
		{
			IntPtr native = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_text_buffer_insert (Handle, ref iter, native, -1);
			GLib.Marshaller.Free (native);
		}

		[Obsolete ("Replaced by 'ref TextIter iter' overload")]
		public void InsertWithTags (TextIter iter, string text, params TextTag[] tags)
		{
			InsertWithTags (iter, text, tags);
		}

		public void InsertWithTagsByName (TextIter iter, string text, params string[] tagnames)
		{
			TextIter start;
			int offset = iter.Offset;
			Insert (ref iter, text);

			start = GetIterAtOffset (offset);
			iter = GetIterAtOffset (offset + text.Length);

			foreach (string tagname in tagnames) {
				TextTag tag = TagTable.Lookup (tagname);
				if (tag != null)
					this.ApplyTag (tagname, start, iter);
			}
		}

		[Obsolete("Use the TextBuffer.Text property's setter")]
		public void SetText (string text)
		{
			Text = text;
		}

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_text_buffer_insert_interactive(IntPtr raw, ref Gtk.TextIter iter, IntPtr text, int len, bool default_editable);

		public bool InsertInteractive(ref Gtk.TextIter iter, string text, bool default_editable)
		{
			IntPtr native = GLib.Marshaller.StringToPtrGStrdup (text);
			bool result = gtk_text_buffer_insert_interactive(Handle, ref iter, native, -1, default_editable);
			GLib.Marshaller.Free (native);
			return result;
		}

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_text_buffer_insert_interactive_at_cursor(IntPtr raw, IntPtr text, int len, bool default_editable);

		public bool InsertInteractiveAtCursor(string text, bool default_editable)
		{
			IntPtr native = GLib.Marshaller.StringToPtrGStrdup (text);
			bool result = gtk_text_buffer_insert_interactive_at_cursor(Handle, native, -1, default_editable);
			GLib.Marshaller.Free (native);
			return result;
		}

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_text_buffer_insert_at_cursor(IntPtr raw, IntPtr text, int len);

		public void InsertAtCursor(string text)
		{
			IntPtr native = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_text_buffer_insert_at_cursor(Handle, native, -1);
			GLib.Marshaller.Free (native);
		}

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_text_buffer_serialize (IntPtr raw, IntPtr content_buffer, IntPtr format, ref Gtk.TextIter start, ref Gtk.TextIter end, out UIntPtr length);

		public byte[] Serialize(Gtk.TextBuffer content_buffer, Gdk.Atom format, Gtk.TextIter start, Gtk.TextIter end)
		{
			UIntPtr length;
			IntPtr raw_ret = gtk_text_buffer_serialize (Handle, content_buffer == null ? IntPtr.Zero : content_buffer.Handle, format == null ? IntPtr.Zero : format.Handle, ref start, ref end, out length);
			if (raw_ret == IntPtr.Zero)
				return new byte [0];
			int sz = (int) (uint) length;
			byte[] ret = new byte [sz];
			Marshal.Copy (raw_ret, ret, 0, sz);
			return ret;
		}

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_text_buffer_get_serialize_formats(IntPtr raw, out int n_formats);

		[DllImport ("libgtk-win32-3.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_text_buffer_get_deserialize_formats(IntPtr raw, out int n_formats);

		public Gdk.Atom[] DeserializeFormats {
			get {
				int n_formats;
				IntPtr raw_ret = gtk_text_buffer_get_deserialize_formats(Handle, out n_formats);
				Gdk.Atom[] result = new Gdk.Atom [n_formats];
				for (int i = 0; i < n_formats; i++) {
					IntPtr format = Marshal.ReadIntPtr (raw_ret, i * IntPtr.Size);
					result [i] = format == IntPtr.Zero ? null : (Gdk.Atom) GLib.Opaque.GetOpaque (format, typeof (Gdk.Atom), false);
				}
				return result;
			}
		}

		public Gdk.Atom[] SerializeFormats {
			get {
				int n_formats;
				IntPtr raw_ret = gtk_text_buffer_get_serialize_formats(Handle, out n_formats);
				Gdk.Atom[] result = new Gdk.Atom [n_formats];
				for (int i = 0; i < n_formats; i++) {
					IntPtr format = Marshal.ReadIntPtr (raw_ret, i * IntPtr.Size);
					result [i] = format == IntPtr.Zero ? null : (Gdk.Atom) GLib.Opaque.GetOpaque (format, typeof (Gdk.Atom), false);
				}
				return result;
			}
		}
	}
}
